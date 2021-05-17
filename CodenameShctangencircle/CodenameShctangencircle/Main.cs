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
        }

        public List<string> Output = new List<string>();
        public List<string> OutputVisual = new List<string>();
        public List<string> stepOutput = new List<string>();

        private void Start_Click(object sender, EventArgs e)
        {
            checkKoefs();
            Cycles cikl = new Cycles();
            cikl.nCycle(ref OutputVisual, ref Output, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), defaultcase);
            foreach (string i in OutputVisual)
            {
                ResBox.Items.Add(i);
            }
        }
        int defaultcase = 0;
        void checkKoefs()
        {
            if(Convert.ToDouble(a11TB.Text) == 0.500) 
            {
                if(Convert.ToDouble(step2TB.Text) == 0.01)
                {
                    if (Convert.ToDouble(step4TB.Text) == 1) { defaultcase = 0; }
                    else defaultcase = 1;
                }
                if (Convert.ToDouble(step2TB.Text) == 0.05) defaultcase = 2;
            }
            if (Convert.ToDouble(a11TB.Text) == 0.505)
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
            fs.vFindSteps(Output, Convert.ToDouble(step1TB.Text), Convert.ToDouble(step2TB.Text), Convert.ToDouble(step3TB.Text), Convert.ToDouble(step4TB.Text), Convert.ToDouble(step5TB.Text), ref stepOutput, Convert.ToDouble(a11TB.Text));
            foreach (string st in stepOutput) StepsResultBox.Items.Add(st);
        }
    }
}
