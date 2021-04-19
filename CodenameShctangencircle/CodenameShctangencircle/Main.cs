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
        public List<string> DiafantOutput = new List<string>();

        private void Start_Click(object sender, EventArgs e)
        {
            
            Cycles cikl = new Cycles();
            cikl.nCycle(ref Output);
            foreach (string i in Output)
            {
                ResBox.Items.Add(i);
            }
        }

        private void combFindBTN_Click(object sender, EventArgs e)
        {
            try
            {
                CheckDiafant chk = new CheckDiafant();
                chk.check(Output, Convert.ToInt32(nKoefTB.Text), Convert.ToInt32(k1KoefTB.Text), Convert.ToInt32(k2KoefTB.Text), Convert.ToInt32(k3KoefTB.Text), Convert.ToInt32(k4KoefTB.Text), Convert.ToInt32(k5KoefTB.Text), ref DiafantOutput);
            }
            catch(System.FormatException)
            {

            }

            foreach (string i in DiafantOutput) DiafantResultBox.Items.Add(i);

        }
    }
}
