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
            listBox1.Items.Add(defaultCase); // Чек дефолт
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
                listBox1.Items.Add(defaultCase);
                if (Convert.ToDouble(step2TB.Text) == 0.05)
                {
                    defaultCase = 2;
                }
            } //зависимость от введенных шагов
            listBox1.Items.Add(defaultCase); // Чек дефолт
            if (A11TB.Text == (0.505).ToString())
            {
                if (step2TB.Text == (0.01).ToString())
                {
                    if (step4TB.Text == (1).ToString())
                    {
                        defaultCase = 3;
                    }
                    if (step4TB.Text == (0.5).ToString())
                    {
                        defaultCase = 4;
                    }
                }
                if (step2TB.Text == (0.05).ToString())
                {
                    defaultCase = 5;
                }
                listBox1.Items.Add(defaultCase); // Чек дефолт
            }

            Cycles cikl = new Cycles();
            cikl.nCycle(ref OutputVisual, ref Output, defaultCase);
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
