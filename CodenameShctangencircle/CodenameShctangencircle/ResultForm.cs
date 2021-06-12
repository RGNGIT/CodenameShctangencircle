
using System;
using System.Windows.Forms;

namespace CodenameShctangencircle
{
    public partial class ResultForm : Form
    {
        public ResultForm()
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
            dataGridViewRes.Columns.Add("_l1", "А1q");
            dataGridViewRes.Columns.Add("_l2", "A2q");
            dataGridViewRes.Columns.Add("_l3", "A3q");
            dataGridViewRes.Columns.Add("_l4", "A4q");
            dataGridViewRes.Columns.Add("_l5", "A5q");
            for (int i = 0; i < Database.Count.Count; i++)
            {
                dataGridViewRes.Rows.Add(
                    Database.Count[i],
                    Database.l1[i],
                    Database.l2[i],
                    Database.l3[i],
                    Database.l4[i],
                    Database.l5[i]);
            }
            for (int i = 0; i < Database.GridCount.Count; i++)
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
            DrawTable();
        }
        void DrawTable()
        {
            string CheckDif = string.Empty;
            for (int i = 0, j = 0; i < dataGridViewCyclesRes.Rows.Count - 1; i++)
            {
                if (CheckDif == dataGridViewCyclesRes.Rows[i].Cells[1].Value.ToString())
                {
                    j++;
                }
                else
                {
                    j = 1;
                    CheckDif = dataGridViewCyclesRes.Rows[i].Cells[1].Value.ToString();
                }
                dataGridViewFin.Rows.Add(dataGridViewCyclesRes.Rows[i].Cells[1].Value,
                    j,
                    dataGridViewRes.Rows[i].Cells[1].Value.ToString() +
                    dataGridViewRes.Rows[i].Cells[2].Value.ToString() +
                    dataGridViewRes.Rows[i].Cells[3].Value.ToString() +
                    dataGridViewRes.Rows[i].Cells[4].Value.ToString() +
                    dataGridViewRes.Rows[i].Cells[5].Value.ToString());
            }
        }

        string countSDM(string sizes)
        {
            string sdm = String.Empty;
            return sdm;
        }

        public void FillSchoodDG(string Sizes, string KE, string NGU, string VGU, string N, string KSR) 
        {
            string sdm = "sas";
            DataGridSchool.Rows.Add(N, Sizes, KSR, sdm, VGU, NGU, KE);
        }
    }
}
