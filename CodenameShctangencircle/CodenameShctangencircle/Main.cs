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
        
        private void Start_Click(object sender, EventArgs e)
        {
            
            Cycles cikl = new Cycles();
            cikl.nCycle(ref Output);
            foreach (string i in Output)
            {
                ResBox.Items.Add(i);
            }
        }

        
    }
}
