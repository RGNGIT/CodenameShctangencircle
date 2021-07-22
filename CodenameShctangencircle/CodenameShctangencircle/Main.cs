using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text;
using WindowsInput;
using System.Xml.Serialization;

namespace CodenameShctangencircle
{

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            comboBoxa11TB.SelectedIndex = 0; 
            comboBox1.SelectedIndex = 2;
            buttonPause.Visible = false;
        }

        public List<string> Output = new List<string>();
        public List<string> OutputVisual = new List<string>();
        public List<string> stepOutput = new List<string>();
        bool fl = false;

        private void Start_Click(object sender, EventArgs e)
        {
            Output.Clear();
            OutputVisual.Clear();
            stepOutput.Clear();
            iterator = 1;
            if(fl)
            {
                r.Close();
                r = null; 
                clearFiles();
            }

            if (!fl)
            {
                fl = true;
            }

            Database.ClearStuff();
            checkKoefs();

            Cycles cikl = new Cycles();
            cikl.nCycle(ref OutputVisual, ref Output, Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value), defaultcase);
            Thread.Sleep(2000);
            button1_Click(sender, e); 
            Thread.Sleep(2000);
            Entry = comboBoxa11TB.SelectedItem.ToString();
            if (!checkBoxServer.Checked)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                ServerCycles();
            }
            buttonPause.Visible = true;
        }

        int defaultcase = 0;

        public IntPtr WinApi { get; private set; }

        void checkKoefs()
        {
            
            if (comboBox1.SelectedIndex == 0 && comboBoxa11TB.SelectedIndex == 0) { defaultcase = 0; step1 = 0.005; step2 = 0.01; step3 = 0.1; step4 = 1; step5 = 10; }
            if (comboBox1.SelectedIndex == 1 && comboBoxa11TB.SelectedIndex == 0) { defaultcase = 1; step1 = 0.005; step2 = 0.01; step3 = 0.1; step4 = 0.5; step5 = 10; }
            if (comboBox1.SelectedIndex == 2 && comboBoxa11TB.SelectedIndex == 0) { defaultcase = 2; step1 = 0.005; step2 = 0.05; step3 = 0.1; step4 = 1; step5 = 10; }
            if (comboBox1.SelectedIndex == 0 && comboBoxa11TB.SelectedIndex == 1) { defaultcase = 3; step1 = 0.005; step2 = 0.01; step3 = 0.1; step4 = 1; step5 = 10; }
            if (comboBox1.SelectedIndex == 1 && comboBoxa11TB.SelectedIndex == 1) { defaultcase = 4; step1 = 0.005; step2 = 0.01; step3 = 0.1; step4 = 0.5; step5 = 10; }
            if (comboBox1.SelectedIndex == 2 && comboBoxa11TB.SelectedIndex == 1) { defaultcase = 5; step1 = 0.005; step2 = 0.05; step3 = 0.1; step4 = 1; step5 = 10; } 
        
        }

        double step1, step2, step3, step4, step5;

        private void button1_Click(object sender, EventArgs e)
        {
            FindSteps fs = new FindSteps();
            fs.vFindSteps(Output, step1, step2, step3, step4, step5, ref stepOutput, Convert.ToDouble(comboBoxa11TB.SelectedItem.ToString()));
            r = new ResultForm();
        }

        double NoRepeatAmount, LongestStep, LowerBorder, UpperBorder, KE = 0;
       
        void GetFileVars(int iterator)
        {
            string[] arr = File.ReadAllLines($"Составленные{iterator}_0.txt");
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
            KE = Convert.ToDouble(Temp);
            
        }

        public ResultForm r;

        void GetArr(int iterator, out string[] arr) 
        {
            try
            {
                if (File.Exists($"Составленные{iterator}_0.txt"))
                {
                    arr = File.ReadAllLines($"Составленные{iterator}_0.txt");
                }
                else
                {
                    while (!File.Exists($"Составленные{iterator}_0.txt"))
                    {
                        Thread.Sleep(1000);
                    }
                    arr = File.ReadAllLines($"Составленные{iterator}_0.txt");
                }
            }
            catch (Exception)
            {
                GetArr(iterator, out arr);
            }
        }

        int globalCalcIter = 0;
        InputSimulator simulator = new InputSimulator();

        void ProgramCycles(int cycleI)
        {   
            IntPtr w = FindWindow(null, "Поиск лучшего набора и расчет характеристик"); 
            IntPtr frm1 = FindWindow(null, "Form1");
            // f = frm1;
            if (w.ToInt32() == 0) MessageBox.Show("Окно не найдено :c");
            ShowWindow(w, 9);
            SetForegroundWindow(w);
            
            simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RIGHT);
            for(int i = 0; i < 4; i++)
            {
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
            }
            for (int i = cycleI; i < Database.l1.Count; i++)
            {
                
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_A);
                Thread.Sleep(200);
                simulator.Keyboard.TextEntry(Entry);
                Thread.Sleep(200);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_A);
                Thread.Sleep(200);
                simulator.Keyboard.TextEntry(Database.l1[i] + Database.l2[i] + Database.l3[i] + Database.l4[i] + Database.l5[i]);
                Thread.Sleep(200);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_A);
                Thread.Sleep(200);
                SendKeys.SendWait(Database.o[i].ToString());
                for (int m = 0; m < 3; m++)
                {
                    simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                }
                Thread.Sleep(200);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                for (int m = 0; m < 7; m++)
                {
                    simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                }
                Thread.Sleep(200);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                Thread.Sleep(1000);

                string[] arr;

                GetArr(iterator, out arr);

                Thread.Sleep(200);
                GetFileVars(iterator);
                Thread.Sleep(200);

                              
                if (iterator % 10 == 0 && iterator != 0)
                {
                    iterator = 0;
                    for (int j = 1; j < 11; j++) File.Delete($"Составленные{j}_0.txt");
                }
                iterator++; globalCalcIter++;
                string Ns = ("n1 = " + Database.p1[i] + " n2 = " + Database.p2[i] + " n3 = " + Database.p3[i] + " n4 = " + Database.p4[i] + " n5 = " + Database.p5[i]).ToString();
                r.FillSchoodDG(Database.l1[i] + Database.l2[i] + Database.l3[i] + Database.l4[i] + Database.l5[i], KE.ToString(), LowerBorder.ToString(), UpperBorder.ToString(), Database.o[i].ToString(), LongestStep.ToString(), NoRepeatAmount.ToString(), Ns);
                
            }
            SendKeys.SendWait("%{F4}");
            MessageBox.Show("Рассчеты завершены.");
            r.FillBestResultsDG();
            
        }

        public string Entry;

        private void Справка_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.Show();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            r.Show();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > numericUpDown2.Value) numericUpDown2.Value = numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < numericUpDown1.Value) numericUpDown1.Value = numericUpDown2.Value;
        }

        Process s; IntPtr f;

        void ServerCycles()
        {
            ServerWorks serverWorks = new ServerWorks(this);
            serverWorks.Show();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Process myProcess = Process.Start(@"GaugeBlockv3-1.exe");
            Thread.Sleep(1000);
            s = myProcess;
            ProgramCycles(globalCalcIter);
        }

        bool isPaused = false;

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                iterator = 1; clearFiles();
                IntPtr w = FindWindow(null, "Поиск лучшего набора и расчет характеристик");
                ShowWindow(w, 9);
                SetForegroundWindow(w);
                SendKeys.SendWait("%{F4}");
                Process myProcess = Process.Start(@"GaugeBlockv3-1.exe");
                Thread.Sleep(1000);
                buttonPause.Text = "Пауза";
                ProgramCycles(globalCalcIter);
            } 
            else
            {
                isPaused = !isPaused;
                // iterator--;
                buttonPause.Text = "Продолжить";
            }

         
        }

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
            using(FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                container = xmlSerializer.Deserialize(fs) as ShctangenContainer;
            }
            for(int i = 0; i < container.n.Count; i++)
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
            for(int i = 0; i < container.A1q.Count; i++)
            {
                resultForm.dataGridViewRes.Rows.Add(
                    i + 1,
                    container.A1q[i],
                    container.A2q[i],
                    container.A3q[i],
                    container.A4q[i],
                    container.A5q[i]);
            }
            for(int i = 0; i < container.MesAmount.Count; i++)
            {
                resultForm.dataGridViewFin.Rows.Add(
                   container.MesAmount[i],
                   container.Number[i],
                   container.Size[i]);
            }
            for(int i = 0; i < container.MesAmountSch.Count; i++)
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

        int iterator = 1;

        void clearFiles()
        {
            try
            {
                if (File.Exists("Составленные1_0.txt")) File.Delete("Составленные1_0.txt");
                if (File.Exists("Составленные2_0.txt")) File.Delete("Составленные2_0.txt");
                if (File.Exists("Составленные3_0.txt")) File.Delete("Составленные3_0.txt");
                if (File.Exists("Составленные4_0.txt")) File.Delete("Составленные4_0.txt");
                if (File.Exists("Составленные5_0.txt")) File.Delete("Составленные5_0.txt");
                if (File.Exists("Составленные6_0.txt")) File.Delete("Составленные6_0.txt");
                if (File.Exists("Составленные7_0.txt")) File.Delete("Составленные7_0.txt");
                if (File.Exists("Составленные8_0.txt")) File.Delete("Составленные8_0.txt");
                if (File.Exists("Составленные9_0.txt")) File.Delete("Составленные9_0.txt");
                if (File.Exists("Составленные10_0.txt")) File.Delete("Составленные10_0.txt");
            }
            catch { }
        }

       

        private void Main_Load(object sender, EventArgs e)
        {
            clearFiles();
        }

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

    }
}
