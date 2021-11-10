using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Drawing;
using Microsoft.Office.Interop.Word;
using System.Xml.Serialization;

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

        private void MergeGridviewCells(DataGridView DGV, int[] idx)
        {
            DataGridViewRow Prev = null;

            foreach (DataGridViewRow item in DGV.Rows)
            {
                if (Prev != null)
                {
                    string firstCellText = string.Empty;
                    string secondCellText = string.Empty;

                    foreach (int i in idx)
                    {
                        DataGridViewCell firstCell = Prev.Cells[i];
                        DataGridViewCell secondCell = item.Cells[i];

                        firstCellText = (firstCell != null && firstCell.Value != null ? firstCell.Value.ToString() : string.Empty);
                        secondCellText = (secondCell != null && secondCell.Value != null ? secondCell.Value.ToString() : string.Empty);

                        if (firstCellText == secondCellText)
                        {
                            secondCell.Style.ForeColor = Color.Transparent;
                        }
                        else
                        {
                            Prev = item;
                            break;
                        }
                    }
                }
                else
                {
                    Prev = item;
                }
            }
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
            string TempString = String.Empty;
            List<double> Temp = new List<double>();
            double sum = 0;
            bool FTS = true;
            foreach (char i in sizes)
            {
                if (i != ' ')
                {
                    TempString += i;
                    FTS = true;
                }
                if(i == ' ' && FTS)
                {
                    Temp.Add(Convert.ToDouble(TempString));
                    TempString = String.Empty;
                    FTS = false;
                }
            }
            foreach(double i in Temp)
            {
                sum += i;
            }
            return sum.ToString();
        }

        int schoolCounter = 1; 
        public void FillSchoodDG(string Sizes, string KE, string NGU, string VGU, string N, string KSR, string NoRepeatAmount, string Ns) 
        {
            DataGridSchool.Rows.Add(schoolCounter, N, Sizes, KSR, countSDM(Sizes), VGU, NGU, KE, NoRepeatAmount, Ns);
            schoolCounter++;
        }

        public void FillBestResultsDG()
        {
            if (DataGridSchool.Rows.Count - 1 != 0)
            {
                string N = DataGridSchool.Rows[0].Cells[1].Value.ToString(); double best = Convert.ToDouble(DataGridSchool.Rows[0].Cells[7].Value); int bestI = 0;
                for (int i = 0; i < DataGridSchool.Rows.Count - 1; i++)
                {
                    if (best < Convert.ToDouble(DataGridSchool.Rows[i].Cells[7].Value)) { bestI = i; best = Convert.ToDouble(DataGridSchool.Rows[i].Cells[7].Value); }
                    if (N != DataGridSchool.Rows[i].Cells[1].Value.ToString())
                    {
                        bestResultsDG.Rows.Add(DataGridSchool.Rows[bestI].Cells[0].Value,
                            DataGridSchool.Rows[bestI].Cells[1].Value,
                            DataGridSchool.Rows[bestI].Cells[2].Value,
                            DataGridSchool.Rows[bestI].Cells[3].Value,
                            DataGridSchool.Rows[bestI].Cells[4].Value,
                            DataGridSchool.Rows[bestI].Cells[5].Value,
                            DataGridSchool.Rows[bestI].Cells[6].Value,
                            DataGridSchool.Rows[bestI].Cells[7].Value,
                            DataGridSchool.Rows[bestI].Cells[8].Value,
                            DataGridSchool.Rows[bestI].Cells[9].Value
                            );

                        N = DataGridSchool.Rows[i].Cells[1].Value.ToString();
                        best = Convert.ToDouble(DataGridSchool.Rows[i].Cells[7].Value); bestI = i;

                    }

                    
                    if (i == DataGridSchool.Rows.Count - 2)
                    {
                        bestResultsDG.Rows.Add(DataGridSchool.Rows[bestI].Cells[0].Value,
                            DataGridSchool.Rows[bestI].Cells[1].Value,
                            DataGridSchool.Rows[bestI].Cells[2].Value,
                            DataGridSchool.Rows[bestI].Cells[3].Value,
                            DataGridSchool.Rows[bestI].Cells[4].Value,
                            DataGridSchool.Rows[bestI].Cells[5].Value,
                            DataGridSchool.Rows[bestI].Cells[6].Value,
                            DataGridSchool.Rows[bestI].Cells[7].Value,
                            DataGridSchool.Rows[bestI].Cells[8].Value,
                            DataGridSchool.Rows[bestI].Cells[9].Value);
                    }
                }
            }
        }

        private void SaveToDocBTN_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = "export.docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                Export_Data_To_Word(dataGridViewCyclesRes, sfd.FileName, 0);
            }
        }

        public void Export_Data_To_Word(DataGridView DGV, string filename, int Tabcase)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop

                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;

                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
             
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";
                        
                    }
                }

                //table format
                oRange.Text = oTemp;

                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                
                object ApplyBorders = true;
                object AutoFit = true;
                object ApplyColor = Color.Black;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, ref ApplyColor,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();
               
                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Times new Roman";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
                }

                //table style 
                //oDoc.Application.Selection.Tables[1].set_Style("Grid Table 1 - Accent 2");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
              
                oDoc.Application.Selection.Tables[1].Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;
                oDoc.Application.Selection.Tables[1].Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;
                oDoc.Application.Selection.Tables[1].Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
                oDoc.Application.Selection.Tables[1].Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
                //header text
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    switch (Tabcase)
                    {
                        case 0: headerRange.Text = "Результаты циклов подбора"; break;
                        case 1: headerRange.Text = "Результаты подбора размеров мер"; break;
                        case 2: headerRange.Text = "Итоговая таблица размеров мер"; break;
                        case 3: headerRange.Text = "Оптимальные наборы"; break;
                        case 4: headerRange.Text = "Лучшие наборы"; break;
                    }
                        
                    
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                //save the file
                oDoc.SaveAs2(filename);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = "export.docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                Export_Data_To_Word(dataGridViewRes, sfd.FileName, 1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = "export.docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                Export_Data_To_Word(dataGridViewFin, sfd.FileName, 2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = "export.docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                Export_Data_To_Word(DataGridSchool, sfd.FileName, 3);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = "export.docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                Export_Data_To_Word(bestResultsDG, sfd.FileName, 4);
            }
        }

        List<string> GridListReturner(DataGridView dataGrid, int Index)
        {
            List<string> Temp = new List<string>();
            for(int i = 0; i < dataGrid.Rows.Count - 1; i++)
            {
                Temp.Add(dataGrid.Rows[i].Cells[Index].Value.ToString());
            }
            return Temp;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ShctangenContainer));
            ShctangenContainer Container = new ShctangenContainer()
            {
                n = GridListReturner(dataGridViewCyclesRes, 1),
                k1 = GridListReturner(dataGridViewCyclesRes, 2),
                k2 = GridListReturner(dataGridViewCyclesRes, 3),
                k3 = GridListReturner(dataGridViewCyclesRes, 4),
                k4 = GridListReturner(dataGridViewCyclesRes, 5),
                k5 = GridListReturner(dataGridViewCyclesRes, 6),
                n1 = GridListReturner(dataGridViewCyclesRes, 7),
                n2 = GridListReturner(dataGridViewCyclesRes, 8),
                n3 = GridListReturner(dataGridViewCyclesRes, 9),
                n4 = GridListReturner(dataGridViewCyclesRes, 10),
                n5 = GridListReturner(dataGridViewCyclesRes, 11),
                A1q = GridListReturner(dataGridViewRes, 1),
                A2q = GridListReturner(dataGridViewRes, 2),
                A3q = GridListReturner(dataGridViewRes, 3),
                A4q = GridListReturner(dataGridViewRes, 4),
                A5q = GridListReturner(dataGridViewRes, 5),
                MesAmount = GridListReturner(dataGridViewFin, 0),
                Number = GridListReturner(dataGridViewFin, 1),
                Size = GridListReturner(dataGridViewFin, 2),
                MesAmountSch = GridListReturner(DataGridSchool, 1),
                SizeSch = GridListReturner(DataGridSchool, 2),
                KSR = GridListReturner(DataGridSchool, 3),
                SDM = GridListReturner(DataGridSchool, 4),
                VGU = GridListReturner(DataGridSchool, 5),
                NGU = GridListReturner(DataGridSchool, 6),
                KE = GridListReturner(DataGridSchool, 7),
                KSM = GridListReturner(DataGridSchool, 8),
                nSch = GridListReturner(DataGridSchool, 9)
            };
            SaveFileDialog saveFileDialog = new SaveFileDialog() 
            {
                Filter = "ShctangenFile|*.shc",
                Title = "Сохранить данные из таблиц"
            };
            string FileName = String.Empty;
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = saveFileDialog.FileName;
            }
            using(FileStream fs = new FileStream(FileName, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, Container);
            }
        }
    }
}
