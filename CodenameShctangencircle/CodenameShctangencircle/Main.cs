using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace CodenameShctangencircle
{

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            comboBoxa11TB.SelectedIndex = 0; 
            comboBox1.SelectedIndex = 2;
            comboBox2.SelectedIndex = 3;
            comboBox3.SelectedIndex = 3;
            comboBox4.SelectedIndex = 1;
            comboBox5.SelectedIndex = 3;
            comboBox4.SelectedIndex = 3;
            comboBox3.SelectedIndex = 2;
            comboBox2.SelectedIndex = 1;
            comboBox1.SelectedIndex = 1;
            beginStep.SelectedIndex = 1;
            buttonPause.Visible = false;
        }

        public List<string> Output = new List<string>();
        public List<string> OutputVisual = new List<string>();
        public List<string> stepOutput = new List<string>();
        bool fl = false;

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            Start.Text = "Идет расчёт...";
            stopwatch = new Stopwatch();
            stopwatch.Start();
            Output.Clear();
            OutputVisual.Clear();
            stepOutput.Clear();

            //если надо снова посчитать закрываем все прошлое
            if(fl)
            {
                r.Close();
                r = null; 
            }

            if (!fl)
            {
                fl = true;
            }

            Database.ClearStuff();

            Cycles cikl = new Cycles();

            cikl.nCycle(
                ref OutputVisual, ref Output,
                Convert.ToInt32(numericUpDown1.Value),
                Convert.ToInt32(numericUpDown2.Value),
                Convert.ToDouble(comboBox1.SelectedItem),
                Convert.ToDouble(comboBox2.SelectedItem),
                Convert.ToDouble(comboBox3.SelectedItem),
                Convert.ToDouble(comboBox4.SelectedItem),
                Convert.ToDouble(comboBox5.SelectedItem),
                Convert.ToDouble(comboBoxa11TB.SelectedItem));

            Thread.Sleep(2000);
            button1_Click(sender, e); 
            Thread.Sleep(2000);
            Entry = beginStep.SelectedItem.ToString();

            if (!checkBoxServer.Checked)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                ServerCycles(); //рудимент (не используется больше)
            }
            buttonPause.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Database.ClearStuff();

            Cycles cikl = new Cycles();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(Convert.ToDouble(comboBox1.Items[i]) < Convert.ToDouble(comboBox2.Items[j]))
                    for (int o = 0; o < 5; o++)
                    {
                        if(Convert.ToDouble(comboBox2.Items[j]) < Convert.ToDouble(comboBox3.Items[o]))
                        for(int p = 0; p < 5; p++)
                        {
                            if(Convert.ToDouble(comboBox3.Items[o]) < Convert.ToDouble(comboBox4.Items[p]))
                            for(int l = 0; l < 5; l++)
                            {
                                if(Convert.ToDouble(comboBox4.Items[p]) < Convert.ToDouble(comboBox5.Items[l]))
                                cikl.nCycle(
                                ref OutputVisual, ref Output,
                                35,
                                122,
                                Convert.ToDouble(comboBox1.Items[i]),
                                Convert.ToDouble(comboBox2.Items[j]),
                                Convert.ToDouble(comboBox3.Items[o]),
                                Convert.ToDouble(comboBox4.Items[p]),
                                Convert.ToDouble(comboBox5.Items[l]),
                                Convert.ToDouble(comboBoxa11TB.SelectedItem));
                            }
                        }
                    }
                }
            }

            foreach(string res in Database.noResultList)
            {
                Console.WriteLine(res);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindSteps fs = new FindSteps();
            fs.vFindSteps( //вызов метода подсчета размеров мер
                Output, 
                Convert.ToDouble(comboBox1.SelectedItem),
                Convert.ToDouble(comboBox2.SelectedItem),
                Convert.ToDouble(comboBox3.SelectedItem),
                Convert.ToDouble(comboBox4.SelectedItem),
                Convert.ToDouble(comboBox5.SelectedItem), 
                ref stepOutput, 
                Convert.ToDouble(comboBoxa11TB.SelectedItem.ToString()),
                Convert.ToDouble(beginStep.SelectedItem.ToString()));
            r = new ResultForm();
        }

        double NoRepeatAmount, LongestStep, LowerBorder, UpperBorder, KE = 0;

        public ResultForm r;

        int globalCalcIter = 0;

        void DebugOutput(List<string> Output)
        {
            foreach(string i in Output)
            {
                Console.WriteLine(i);
            }
        }

        void DoShkolnikCycle(int ii) //полностью скопированный функционал из старой программы расчета
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool flag = false;
            try
            {
                GaugeCalculations.CompositeStep = double.Parse(Entry.ToString().Replace('.', ','));
                flag = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Неправильно");
            }
            if (flag)
            {
                try
                {
                    int num = int.Parse(Database.o[ii].ToString());
                    string[] array = (Database.l1[ii] + Database.l2[ii] + Database.l3[ii] + Database.l4[ii] + Database.l5[ii]).ToString().Trim(new char[]
                    {
                ';'
                    }).Split(new char[]
                    {
                ' '
                    }, StringSplitOptions.RemoveEmptyEntries);
                    double[] array2 = new double[num];
                    for (int i = 0; i < num; i++)
                    {
                        array2[i] = double.Parse(array[i].Replace('.', ','));
                    }
                    int[] array3 = new int[5];
                    double[] array4 = new double[5];
                    double a = 0.0;
                    double amax = 0.0;
                    a = array2[0];
                    amax = array2[num - 1];
                    double num2 = Math.Round(Math.Abs(array2[1] - array2[0]), 4);
                    int num3 = 0;
                    array4[num3] = num2;
                    array3[num3]++;
                    bool complete = true;
                    try
                    {
                        for (int j = 1; j < num; j++)
                        {
                            if (Math.Abs(array2[j] - array2[j - 1] - array4[num3]) < 1E-06)
                            {
                                array3[num3]++;
                            }
                            else
                            {
                                num3++;
                                array4[num3] = Math.Round(Math.Abs(array2[j] - array2[j - 1]), 4);
                                array3[num3]++;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        complete = false;
                        MessageBox.Show("Для данного набора не удалось подсчитать набор дельта, значения универсальной функции вычислено не будет!");
                    }
                    GaugeCalculations.Sizes = array2;
                    GaugeCalculations.Sizes2 = array2;
                    GaugeCalculations.Sets.Clear();
                    GaugeCalculations.Sets.Add(array3);
                    GaugeCalculations.DefineVariables1(num, a, amax, array4, 0.005, complete);
                    GaugeCalculations.SummarySizes = GaugeCalculations.Sizes.Sum();
                    GaugeCalculations.DefineParameters(1, 1);
                    string kek = GaugeCalculations.Print1(GaugeCalculations.Sets.Last<int[]>(), true, complete);
                    NoRepeatAmount = GaugeCalculations.SummaryCount2;
                    LongestStep = GaugeCalculations.SequenceCount2;
                    LowerBorder = GaugeCalculations.SequenceBegin2;
                    UpperBorder = GaugeCalculations.SequenceEnd2;
                    KE = GaugeCalculations.KE2;
                    DebugOutput(new List<string> {
                        $"Iter: {ii}",
                        $"NoRepeatAmount: {GaugeCalculations.SummaryCount2}",
                        $"LongestStep: {GaugeCalculations.SequenceCount2}",
                        $"LowerBorder: {GaugeCalculations.SequenceBegin2}",
                        $"UpperBorder: {GaugeCalculations.SequenceEnd2}",
                        $"KE: {GaugeCalculations.KE2}",
                        $"Elapsed time of iteration: {sw.Elapsed}\n"
                    });
                    sw.Stop();
                }
                catch (Exception)
                {
                    MessageBox.Show("Введены некорректные данные или количество введенных мер не совпадает с N! Пожалуйста проверьте правильность значений!");
                }
            }
        }

        void ProgramCycles(int cycleI)
        {   
            for (int i = 0; i < Database.l1.Count; i++)
            {
                DoShkolnikCycle(i);
                string Ns = ("n1 = " + Database.p1[i] + " n2 = " + Database.p2[i] + " n3 = " + Database.p3[i] + " n4 = " + Database.p4[i] + " n5 = " + Database.p5[i]).ToString();
                r.FillSchoodDG(Database.l1[i] + Database.l2[i] + Database.l3[i] + Database.l4[i] + Database.l5[i], KE.ToString(), LowerBorder.ToString(), UpperBorder.ToString(), Database.o[i].ToString(), LongestStep.ToString(), NoRepeatAmount.ToString(), Ns);
            }
            MessageBox.Show("Рассчеты завершены.");
            r.FillBestResultsDG();
        }

        Stopwatch stopwatch;

        public string Entry;

        private void Справка_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.Show();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            r.Show();
            Start.Enabled = true;
            Start.Text = "Рассчет";
            Elapsed.Text = $"Предыдущие рассчеты завершились за {stopwatch.Elapsed}";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > numericUpDown2.Value) numericUpDown2.Value = numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < numericUpDown1.Value) numericUpDown1.Value = numericUpDown2.Value;
        }

        void ServerCycles() //рудимент (была идея сделать удаленный расчет)
        {
            ServerWorks serverWorks = new ServerWorks(this);
            serverWorks.Show();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ProgramCycles(globalCalcIter);
        }

        bool isPaused = false;

        #region InputCombos //предотвращение ошибок ввода коэфициентов
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Convert.ToDouble(comboBox1.SelectedItem) >= Convert.ToDouble(comboBox2.SelectedItem))
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(comboBox2.SelectedItem) >= Convert.ToDouble(comboBox3.SelectedItem))
            {
                comboBox2.SelectedIndex = 0;
            }
            if (Convert.ToDouble(comboBox2.SelectedItem) <= Convert.ToDouble(comboBox1.SelectedItem))
            {
                comboBox1.SelectedIndex = 0;
            }
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(comboBox3.SelectedItem) >= Convert.ToDouble(comboBox4.SelectedItem))
            {
                comboBox3.SelectedIndex = 0;
            }
            if (Convert.ToDouble(comboBox3.SelectedItem) <= Convert.ToDouble(comboBox2.SelectedItem))
            {
                comboBox2.SelectedIndex = 0;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(comboBox4.SelectedItem) >= Convert.ToDouble(comboBox5.SelectedItem))
            {
                comboBox4.SelectedIndex = 0;
            }
            if (Convert.ToDouble(comboBox4.SelectedItem) <= Convert.ToDouble(comboBox3.SelectedItem))
            {
                comboBox3.SelectedIndex = 0;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(comboBox5.SelectedItem) <= Convert.ToDouble(comboBox4.SelectedItem))
            {
                comboBox4.SelectedIndex = 0;
            }
        }
        #endregion

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            ResultForm resultForm = new ResultForm();
            ShctangenContainer container;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ShctangenContainer));
            string FileName = String.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "ShctangenFile|*.shc",
                Title = "Открыть файл с таблицами"
            };
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
            }
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
                {
                    container = xmlSerializer.Deserialize(fs) as ShctangenContainer;
                }

                for (int i = 0; i < container.n.Count; i++)
                {
                    resultForm.dataGridViewCyclesRes.Rows.Add(
                        i + 1,
                        container.n[i],
                        container.k1[i],
                        container.k2[i],
                        container.k3[i],
                        container.k4[i],
                        container.k5[i],
                        container.n1[i],
                        container.n2[i],
                        container.n3[i],
                        container.n4[i],
                        container.n5[i]);
                }
                for (int i = 0; i < container.A1q.Count; i++)
                {
                    resultForm.dataGridViewRes.Rows.Add(
                        i + 1,
                        container.A1q[i],
                        container.A2q[i],
                        container.A3q[i],
                        container.A4q[i],
                        container.A5q[i]);
                }
                for (int i = 0; i < container.MesAmount.Count; i++)
                {
                    resultForm.dataGridViewFin.Rows.Add(
                       container.MesAmount[i],
                       container.Number[i],
                       container.Size[i]);
                }
                for (int i = 0; i < container.MesAmountSch.Count; i++)
                {
                    resultForm.DataGridSchool.Rows.Add(
                        i + 1,
                        container.MesAmountSch[i],
                        container.SizeSch[i],
                        container.KSR[i],
                        container.SDM[i],
                        container.VGU[i],
                        container.NGU[i],
                        container.KE[i],
                        container.KSM[i],
                        container.nSch[i]);
                }
                resultForm.FillBestResultsDG();
                resultForm.Show();
            }
            catch(Exception) { }

        }
    }
}
