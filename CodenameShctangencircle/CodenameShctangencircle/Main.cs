using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.ComponentModel;

namespace CodenameShctangencircle
{

    public partial class Main : Form
    {
        public Main()
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
            dataGridViewRes.Columns.Add("_l1", "l1");
            dataGridViewRes.Columns.Add("_l2", "l2");
            dataGridViewRes.Columns.Add("_l3", "l3");
            dataGridViewRes.Columns.Add("_l4", "l4");
            dataGridViewRes.Columns.Add("_l5", "l5");
            dataGridViewRes.Columns.Add("_KE", "KE");
            dataGridViewRes.Columns.Add("_KSR", "KSR");
            dataGridViewRes.Columns.Add("_SDM", "SDM");
            dataGridViewRes.Columns.Add("_VGU", "VGU");
            dataGridViewRes.Columns.Add("_NGU", "NGU");
        }

        public List<string> Output = new List<string>();
        public List<string> OutputVisual = new List<string>();
        public List<string> stepOutput = new List<string>();

        private void Start_Click(object sender, EventArgs e)
        {
            checkKoefs();
            Cycles cikl = new Cycles();
            cikl.nCycle(ref OutputVisual, ref Output, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), defaultcase);
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
        }
        int defaultcase = 0;

        public IntPtr WinApi { get; private set; }

        void checkKoefs()
        {
            if (Convert.ToDouble(comboBoxa11TB.SelectedItem.ToString()) == 0.500)
            {
                if (Convert.ToDouble(step2TB.Text) == 0.01)
                {
                    if (Convert.ToDouble(step4TB.Text) == 1) { defaultcase = 0; }
                    else defaultcase = 1;
                }
                if (Convert.ToDouble(step2TB.Text) == 0.05) defaultcase = 2;
            }
            if (Convert.ToDouble(comboBoxa11TB.SelectedItem.ToString()) == 0.505)
            {
                if (Convert.ToDouble(step2TB.Text) == 0.01)
                {
                    if (Convert.ToDouble(step4TB.Text) == 1) { defaultcase = 3; }
                    else defaultcase = 4;
                }
                if (Convert.ToDouble(step2TB.Text) == 0.05) defaultcase = 5;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindSteps fs = new FindSteps();
            fs.vFindSteps(Output, Convert.ToDouble(step1TB.Text), Convert.ToDouble(step2TB.Text), Convert.ToDouble(step3TB.Text), Convert.ToDouble(step4TB.Text), Convert.ToDouble(step5TB.Text), ref stepOutput, Convert.ToDouble(comboBoxa11TB.SelectedItem.ToString()));
            for (int i = 0; i < Database.Count.Count; i++)
            {
                dataGridViewRes.Rows.Add(
                    Database.Count[i],
                    Database.l1[i],
                    Database.l2[i],
                    Database.l3[i],
                    Database.l4[i],
                    Database.l5[i],
                    Database.KE[i],
                    Database.KSR[i],
                    Database.SDM[i],
                    Database.VGU[i],
                    Database.NGU[i]);
            }
        }

        double NoRepeatAmount, LongestStep, LowerBorder, UpperBorder, KE;

        void GetFileVars()
        {
            string[] arr = File.ReadAllLines($"Составленные1_0.txt");
            StringBuilder sb;
            string Temp = string.Empty;
            foreach (char i in arr[1])
            {
                if (Char.IsDigit(i))
                {
                    Temp += i;
                }
            }
            NoRepeatAmount = Convert.ToDouble(Temp);
            Temp = string.Empty;
            foreach (char i in arr[3])
            {
                if (Char.IsDigit(i))
                {
                    Temp += i;
                }
            }
            LongestStep = Convert.ToDouble(Temp);
            Temp = string.Empty;
            foreach (char i in arr[5])
            {
                if (Char.IsDigit(i) || i == ',')
                {
                    Temp += i;
                }
            }
            sb = new StringBuilder(Temp);
            sb.Remove(0, 1);
            LowerBorder = Convert.ToDouble(sb.ToString());
            Temp = string.Empty;
            foreach (char i in arr[7])
            {
                if (Char.IsDigit(i) || i == ',')
                {
                    Temp += i;
                }
            }
            sb = new StringBuilder(Temp);
            sb.Remove(0, 1);
            UpperBorder = Convert.ToDouble(sb.ToString());
            Temp = string.Empty;
            foreach (char i in arr[10])
            {
                if (Char.IsDigit(i) || i == ',')
                {
                    Temp += i;
                }
            }
            sb = new StringBuilder(Temp);
            sb.Remove(0, 1);
            KE = Convert.ToDouble(sb.ToString());
            labelTest.Text = $"{NoRepeatAmount}||{LongestStep}||{LowerBorder}||{UpperBorder}||{KE}";
        }

        void ProgramCycles()
        {

            IntPtr w = FindWindow(null, "Поиск лучшего набора и расчет характеристик"); IntPtr frm1 = FindWindow(null, "Form1"); if (frm1.ToInt32() == 0) MessageBox.Show("Окно не найдено :c");
            //BringWindowToTop(w); 
            f = frm1;
            if (w.ToInt32() == 0) MessageBox.Show("Окно не найдено :c");
            ShowWindow(w, 9);
            SetForegroundWindow(w);
            //myProcess.Kill(); 
            //DataFormats.Text, Database.l1[i] + Database.l2[i] + Database.l3[i] + Database.l4[i] + Database.l5[i]
            SendKeys.SendWait("{RIGHT}"); SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}"); SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            for (int i = 0; i < dataGridViewRes.Rows.Count; i++)
            {
                SendKeys.SendWait("^a"); Thread.Sleep(200); SendKeys.SendWait(sas); SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("^a"); 
                Thread thread = new Thread(() => Clipboard.SetData(DataFormats.Text, sus));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                Thread.Sleep(200);
                SendKeys.SendWait("^v");
                SendKeys.SendWait("{TAB}"); SendKeys.SendWait("^a"); Thread.Sleep(200); SendKeys.SendWait(sas);
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{ENTER}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}"); SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}"); SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{ENTER}");
                SendKeys.SendWait("{TAB}");
                Thread.Sleep(1000);
                string[] arr = File.ReadAllLines($"Составленные{iterator}_0.txt");
                /*
                foreach (string s in arr)
                {
                    labelTest.Text += $"{s}\n";
                }
                */
                //GetFileVars();
                // dataGridAllResults.Rows.Add(Database.Count[i], Database.l1[i] + Database.l2[i] + Database.l3[i] + Database.l4[i] + Database.l5[i], "f");
                iterator++;
                //SendKeys.SendWait("%{F4}");
                //SetForegroundWindow(f);
                // SendKeys.SendWait("{TAB}");
            }
            SendKeys.SendWait("%{F4}");
        }
        string sas, sus;
        Process s; IntPtr f;
        private void button2_Click(object sender, EventArgs e)
        {
            sas = comboBoxa11TB.SelectedItem.ToString();
            sus = Database.l1[0] + Database.l2[0] + Database.l3[0] + Database.l4[0] + Database.l5[0];
            backgroundWorker1.RunWorkerAsync();




        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Process myProcess = Process.Start(@"GaugeBlockv3-1.exe"); Thread.Sleep(1000); s = myProcess;
            ProgramCycles();
        }
        int iterator = 1;
        #region SendKeysToOtherWindow

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string IpClassName, string IpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetActiveWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);


        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr handle, int nCmdShow);
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            GetFileVars();
        }
    }
}
