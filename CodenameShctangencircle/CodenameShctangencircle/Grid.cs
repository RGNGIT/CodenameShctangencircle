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
    public partial class Grid : Form
    {

        private Main main;

        public Grid(Main main)
        {
            InitializeComponent();
            this.main = main;
            dataGridViewMain.Columns.Add("_cell1", "Сигма 1");
            dataGridViewMain.Columns.Add("_cell2", "Сигма 2");
            dataGridViewMain.Columns.Add("_cell3", "Сигма 3");
            dataGridViewMain.Columns.Add("_cell4", "Сигма 4");
            dataGridViewMain.Columns.Add("_cell5", "Сигма 5");
            BuildGrid();
        }

        class GridRow
        {
            public GridRow(string i1, string i2, string i3, string i4, string i5)
            { 
                cell1 = i1;
                cell2 = i2;
                cell3 = i3;
                cell4 = i4;
                cell5 = i5;
            }
            public string cell1;
            public string cell2;
            public string cell3;
            public string cell4;
            public string cell5;
        }

        void BuildGrid()
        {
            foreach(GridRow item in GridRows())
            {
                dataGridViewMain.Rows.Add(
                    item.cell1,
                    item.cell2,
                    item.cell3,
                    item.cell4,
                    item.cell5
                    );
            }
        }

        void SetCells(int index)
        {
            List<ComboBox> comboBoxes = new List<ComboBox>()
            {
                main.comboBox1,
                main.comboBox2,
                main.comboBox3,
                main.comboBox4,
                main.comboBox5,
            };
            for(int i = 0; i < comboBoxes.Count; i++)
            {
                dataGridViewMain.Rows[index].Selected = true;
                comboBoxes[i].SelectedItem = dataGridViewMain.Rows[index].Cells[i].Value;
            }
        }

        IEnumerable<GridRow> GridRows()
        {
            foreach(string i1 in main.comboBox1.Items)
            {
                foreach (string i2 in main.comboBox2.Items)
                {
                    foreach (string i3 in main.comboBox3.Items)
                    {
                        foreach (string i4 in main.comboBox4.Items)
                        {
                            foreach (string i5 in main.comboBox5.Items)
                            {
                                yield return new GridRow(i1, i2, i3, i4, i5);
                            }
                        }
                    }
                }
            }
        }
        private void dataGridViewMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetCells(e.RowIndex);
        }
    }
}
