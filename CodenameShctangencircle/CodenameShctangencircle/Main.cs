using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            int defaultCase = 0;
            if (Convert.ToDouble(A11TB.Text) == 0.500)
            {
                if (Convert.ToDouble(step2TB.Text) == 0.01)
                {
                    if (Convert.ToDouble(step4TB.Text) == 1)
                    {
                        defaultCase = 0;
                    }
                    if (Convert.ToDouble(step4TB.Text) == 0.5)
                    {
                        defaultCase = 1;
                    }
                }
                if (Convert.ToDouble(step2TB.Text) == 0.05)
                {
                    defaultCase = 2;
                }
            } //зависимость от введенных шагов
            if (Convert.ToDouble(A11TB.Text) == 0.505)
            {
                if (Convert.ToDouble(step2TB.Text) == 0.01)
                {
                    if (Convert.ToDouble(step4TB.Text) == 1)
                    {
                        defaultCase = 3;
                    }
                    if (Convert.ToDouble(step4TB.Text) == 0.5)
                    {
                        defaultCase = 4;
                    }
                }
                if (Convert.ToDouble(step2TB.Text) == 0.05)
                {
                    defaultCase = 5;
                }
            }

            Cycles cikl = new Cycles();

            switch (defaultCase) 
            {
                case 0: { label1.Text = "Будут использованы слудующие коэфициенты: -99; 0,5K1; 0,1K2; 0,1K3; 0,1K4"; break; }
                case 1: { label1.Text = "Будут использованы слудующие коэфициенты: -99; 0,5K1; 0,1K2; 0,2K3; 0,05K4"; break; }
                case 2: { label1.Text = "Будут использованы слудующие коэфициенты: -99; 0,1K1; 0,5K2; 0,1K3; 0,1K4"; break; }
                case 3: { label1.Text = "Будут использованы слудующие коэфициенты: -100; 0,5K1; 0,1K2; 0,1K3; 0,1K4"; break; }
                case 4: { label1.Text = "Будут использованы слудующие коэфициенты: -100; 0,5K1; 0,1K2; 0,2K3; 0,05K4"; break; }
                case 5: { label1.Text = "Будут использованы слудующие коэфициенты: -100; 0,1K1; 0,5K2; 0,1K3; 0,1K4"; break; }
            }
            

            cikl.nCycle(ref OutputVisual, ref Output, defaultCase, Convert.ToInt32(N1textBox.Text), Convert.ToInt32(N2textBox.Text));
            foreach (string i in OutputVisual)
            {
                ResBox.Items.Add(i);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            FindSteps fs = new FindSteps();
            
            fs.vFindSteps(Output, Convert.ToDouble(step1TB.Text), Convert.ToDouble(step2TB.Text), Convert.ToDouble(step3TB.Text), Convert.ToDouble(step4TB.Text), Convert.ToDouble(step5TB.Text), ref stepOutput, Convert.ToDouble(A11TB.Text));
            foreach (string st in stepOutput) StepsResultBox.Items.Add(st);
        }
    }
}
