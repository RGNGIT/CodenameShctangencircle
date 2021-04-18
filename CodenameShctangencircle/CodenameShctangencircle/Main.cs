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

        void hi()
        {

        }

        private void Start_Click(object sender, EventArgs e)
        {
            List<string> Output = new List<string>();
            Cycles cikl = new Cycles();
            cikl.nCycle(ref Output);
            foreach (string i in Output)
            {
                ResBox.Items.Add(i);
            }
        }
    }
}
