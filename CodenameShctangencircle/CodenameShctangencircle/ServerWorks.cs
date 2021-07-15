using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        void Serializer(Database.DataBlock data)
        {
            using (FileStream fileStream = new FileStream("SetInput.shc", FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, data);
            }
        }

        Database.DataBlock FillBlock(Database.DataBlock block)
        {
            block.o = Database.o;
            block.o1 = Database.o1;
            block.o2 = Database.o2;
            block.o3 = Database.o3;
            block.o4 = Database.o4;
            block.o5 = Database.o5;
            block.p1 = Database.p1;
            block.p2 = Database.p2;
            block.p3 = Database.p3;
            block.p4 = Database.p4;
            block.p5 = Database.p5;
            block.GridCount = Database.GridCount;
            block.Count = Database.Count;
            block.l1 = Database.l1;
            block.l2 = Database.l2;
            block.l3 = Database.l3;
            block.l4 = Database.l4;
            block.l5 = Database.l5;
            block.KE = Database.KE;
            block.KSR = Database.KSR;
            block.SDM = Database.SDM;
            block.VGU = Database.VGU;
            block.NGU = Database.NGU;
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
        GridBlock GetBlock()
        {
            GridBlock DeserializeBlock;
            using (FileStream fileStream = new FileStream("GetOutput.shc", FileMode.OpenOrCreate))
            {
                DeserializeBlock = binaryFormatter.Deserialize(fileStream) as GridBlock;
            }
            return DeserializeBlock;
        }

        void FillDG()
        {
            for(int i = 0; i < GetBlock().Summ.Count; i++)
            {
                main.r.FillSchoodDG(GetBlock().Summ[i], GetBlock().KE[i], GetBlock().LowerBorder[i], GetBlock().UpperBorder[i], GetBlock().o[i], GetBlock().LongestStep[i], GetBlock().NoRepeatAmount[i], GetBlock().Ns[i]);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string Ping;
            if(new Network(null, null).Ping(textBoxAddress.Text, out Ping))
            {
                label1.Text = Ping;
                textBoxAddress.Visible = false;
                Serializer(new Database.DataBlock());
                // Выгрузка
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
