using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodenameShctangencircle
{
    public partial class ServerWorks : Form
    {
        public ServerWorks(Main main)
        {
            InitializeComponent();
            this.main = main;
        }

        Main main;

        void Serializer(DataBlock data)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream("SetInput.shc", FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, data);
            }
        }

        DataBlock FillBlock()
        {
            DataBlock block = new DataBlock()
            {
            Entry = main.Entry,
            o = Database.o,
            o1 = Database.o1,
            o2 = Database.o2,
            o3 = Database.o3,
            o4 = Database.o4,
            o5 = Database.o5,
            p1 = Database.p1,
            p2 = Database.p2,
            p3 = Database.p3,
            p4 = Database.p4,
            p5 = Database.p5,
            GridCount = Database.GridCount,
            Count = Database.Count,
            l1 = Database.l1,
            l2 = Database.l2,
            l3 = Database.l3,
            l4 = Database.l4,
            l5 = Database.l5,
            KE = Database.KE,
            KSR = Database.KSR,
            SDM = Database.SDM,
            VGU = Database.VGU,
            NGU = Database.NGU,
        };
            return block;
    }

        static System.Net.NetworkCredential credential = new System.Net.NetworkCredential()
        {
            UserName = "testuser",
            Password = "12345678"
        };

        bool Listen()
        {
            try
            {
                File.WriteAllBytes("GetOutput.shc", new Network(credential, textBoxAddress.Text).GetInput(new Uri($"ftp://{textBoxAddress.Text}/files/ShctangenNetwork/Output.shc")));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ShctangenNetwork.GridBlock GetBlock()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            ShctangenNetwork.GridBlock DeserializeBlock;
            using (FileStream fileStream = new FileStream("GetOutput.shc", FileMode.OpenOrCreate))
            {
                DeserializeBlock = binaryFormatter.Deserialize(fileStream) as ShctangenNetwork.GridBlock;
            }
            return DeserializeBlock;
        }

        void FillDG()
        {
            for(int i = 0; i < GetBlock().Summ.Count; i++)
            {
                main.r.FillSchoodDG(GetBlock().Summ[i], GetBlock().KE[i], GetBlock().LowerBorder[i], GetBlock().UpperBorder[i], GetBlock().o[i], GetBlock().LongestStep[i], GetBlock().NoRepeatAmount[i], GetBlock().Ns[i]);
            }
            main.r.FillBestResultsDG();
            main.r.Show();
            new Network(credential, textBoxAddress.Text).Delete(new Uri($"ftp://{textBoxAddress.Text}/files/ShctangenNetwork/Output.shc"));
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string Ping;
            if(new Network(null, null).Ping(textBoxAddress.Text, out Ping))
            {
                label1.Text = Ping + "\nИдут удаленные расчеты...\nПо завершении откроется окно с результатами";
                textBoxAddress.Visible = false;
                Serializer(FillBlock());
                // Выгрузка
                new Network(credential, textBoxAddress.Text).SendOutput(File.ReadAllBytes("SetInput.shc"));
                while (true)
                {
                    if(Listen())
                    {
                        break;
                    }
                }
                FillDG();
            }
        }
    }
}
