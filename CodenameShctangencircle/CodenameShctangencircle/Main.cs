using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CodenameShctangencircle
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            dataGridViewCyclesRes.Columns.Add("_count", "Индекс");
            dataGridViewCyclesRes.Columns.Add("_n", "n");
            dataGridViewCyclesRes.Columns.Add("_k1", "k1");
            dataGridViewCyclesRes.Columns.Add("_k2", "k2");
            dataGridViewCyclesRes.Columns.Add("_k3", "k3");
            dataGridViewCyclesRes.Columns.Add("_k4", "k4");
            dataGridViewCyclesRes.Columns.Add("_k5", "k5");
            dataGridViewCyclesRes.Columns.Add("_n1", "n1");
            dataGridViewCyclesRes.Columns.Add("_n2", "n2");
            dataGridViewCyclesRes.Columns.Add("_n3", "n3");
            dataGridViewCyclesRes.Columns.Add("_n4", "n4");
            dataGridViewCyclesRes.Columns.Add("_n5", "n5");
            dataGridViewRes.Columns.Add("_count", "Индекс");
            dataGridViewRes.Columns.Add("_l1", "l1");
            dataGridViewRes.Columns.Add("_l2", "l2");
            dataGridViewRes.Columns.Add("_l3", "l3");
            dataGridViewRes.Columns.Add("_l4", "l4");
            dataGridViewRes.Columns.Add("_l5", "l5");
            dataGridViewRes.Columns.Add("_KE", "KE");
            dataGridViewRes.Columns.Add("_KSR", "KSR");
            dataGridViewRes.Columns.Add("_SDM", "SDM");
            dataGridViewRes.Columns.Add("_VGU", "VGU");
            dataGridViewRes.Columns.Add("_NGU", "NGU");
        }

        public List<string> Output = new List<string>();
        public List<string> OutputVisual = new List<string>();
        public List<string> stepOutput = new List<string>();

        private void Start_Click(object sender, EventArgs e)
        {
            checkKoefs();
            Cycles cikl = new Cycles();
            cikl.nCycle(ref OutputVisual, ref Output, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), defaultcase);
            for(int i = 0; i < Database.GridCount.Count; i++)
            {
                dataGridViewCyclesRes.Rows.Add(
                    Database.GridCount[i],
                    Database.o[i],
                    Database.o1[i],
                    Database.o2[i],
                    Database.o3[i],
                    Database.o4[i],
                    Database.o5[i],
                    Database.p1[i],
                    Database.p2[i],
                    Database.p3[i],
                    Database.p4[i],
                    Database.p5[i]);
            }
        }
        int defaultcase = 0;
        void checkKoefs()
        {
            if(Convert.ToDouble(comboBoxa11TB.SelectedItem.ToString()) == 0.500) 
            {
                if(Convert.ToDouble(step2TB.Text) == 0.01)
                {
                    if (Convert.ToDouble(step4TB.Text) == 1) { defaultcase = 0; }
                    else defaultcase = 1;
                }
                if (Convert.ToDouble(step2TB.Text) == 0.05) defaultcase = 2;
            }
            if (Convert.ToDouble(comboBoxa11TB.SelectedItem.ToString()) == 0.505)
            {
                if (Convert.ToDouble(step2TB.Text) == 0.01)
                {
                    if (Convert.ToDouble(step4TB.Text) == 1) { defaultcase = 3; }
                    else defaultcase = 4;
                }
                if (Convert.ToDouble(step2TB.Text) == 0.05) defaultcase = 5;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindSteps fs = new FindSteps();
            fs.vFindSteps(Output, Convert.ToDouble(step1TB.Text), Convert.ToDouble(step2TB.Text), Convert.ToDouble(step3TB.Text), Convert.ToDouble(step4TB.Text), Convert.ToDouble(step5TB.Text), ref stepOutput, Convert.ToDouble(comboBoxa11TB.SelectedItem.ToString()));
            for(int i = 0; i < Database.Count.Count; i++)
            {
                dataGridViewRes.Rows.Add(
                    Database.Count[i],
                    Database.l1[i],
                    Database.l2[i],
                    Database.l3[i],
                    Database.l4[i],
                    Database.l5[i],
                    Database.KE[i],
                    Database.KSR[i],
                    Database.SDM[i],
                    Database.VGU[i],
                    Database.NGU[i]);
            }
        }
    }
}
