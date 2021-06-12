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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent(); dataGridView1.Rows.Add("Если A11=0,500 и (δ1=0,005; δ2=0,01; δ3=0,1; δ4=1; δ5=10), то", "n1 = (K1 - 99)", "n2 = (K2  -0,5K1)", "n3  = (K3 - 0,1K2)", "n4 = (K4 - 0,1K3)", "n5 = (K5-0,1K4)");
            dataGridView1.Rows.Add("Если A11=0,500 и (δ1=0,005; δ2=0,01; δ3=0,1; δ4=0,5; δ5=10), то", "n1 = (K1 - 99)", "n2 = (K2  -0,5K1)", "n3  = (K3 - 0,1K2)", "n4 = (K4 - 0,2K3)", "n5 = (K5-0,05K4)");
            dataGridView1.Rows.Add("Если A11=0,500 и (δ1=0,005; δ2=0,05; δ3=0,1; δ4=1; δ5=10), то ", "n1 = (K1 - 99)", "n2 = (K2  -0,1K1)", "n3  = (K3 - 0,5K2)", "n4 = (K4-0,1K3)", "n5 = (K5-0,1K4)");
            dataGridView1.Rows.Add("Если A11=0,505 и (δ1=0,005; δ2=0,01; δ3=0,1; δ4=1; δ5=10), то", "n1 = (K1 - 100)", "n2 = (K2  -0,5K1)", "n3  = (K3 - 0,1K2)", "n4 = (K4 - 0,1K3)", "n5 = (K5-0,1K4)");
            dataGridView1.Rows.Add("Если A11=0,505 и (δ1=0,005; δ2=0,01; δ3=0,1; δ4=0,5; δ5=10), то", "n1 = (K1 - 100)", "n2 = (K2  -0,5K1)", "n3  = (K3 - 0,1K2)", "n4 = (K4 - 0,2K3)", "n5 = (K5-0,05K4)");
            dataGridView1.Rows.Add("Если A11=0,505 и (δ1=0,005; δ2=0,05; δ3=0,1; δ4=1; δ5=10), то ", "n1 = (K1 - 100)", "n2 = (K2  -0,1K1)", "n3  = (K3 - 0,5K2)", "n4 = (K4-0,1K3)", "n5 = (K5-0,1K4)");
        }
    }
}
