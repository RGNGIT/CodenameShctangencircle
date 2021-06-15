using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text;
using WindowsInput;

namespace CodenameShctangencircle
{

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            comboBoxa11TB.SelectedIndex = 0; comboBox1.SelectedIndex = 2;
            /*dataGridViewCyclesRes.Columns.Add("_count", "Индекс");
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
            dataGridViewRes.Columns.Add("_l5", "A5q");*/
            
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
            //dataGridViewCyclesRes.Rows.Clear();
            //dataGridViewRes.Rows.Clear();
            //dataGridViewFin.Rows.Clear();
            iterator = 1;
            if(fl)
            {
                r.Close();
                r = null; clearFiles();
            }

            if (!fl)
            {
                fl = true;
            }

            Database.ClearStuff();
            checkKoefs();

            Cycles cikl = new Cycles();
            cikl.nCycle(ref OutputVisual, ref Output, Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value), defaultcase);
            //тут был грид циклов
            Thread.Sleep(2000);
            button1_Click(sender, e); Thread.Sleep(2000);
            //DrawTable();
            sas = comboBoxa11TB.SelectedItem.ToString();
            backgroundWorker1.RunWorkerAsync(); 
            
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
            //тут был грид степов
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
        ResultForm r;
        void ProgramCycles()
        {
            //DataFormats.Text, 
            InputSimulator simulator = new InputSimulator();
            IntPtr w = FindWindow(null, "Поиск лучшего набора и расчет характеристик"); IntPtr frm1 = FindWindow(null, "Form1");// if (frm1.ToInt32() == 0) MessageBox.Show("Окно не найдено :c");
            //BringWindowToTop(w); 
            f = frm1;
            if (w.ToInt32() == 0) MessageBox.Show("Окно не найдено :c");
            ShowWindow(w, 9);
            SetForegroundWindow(w);
            
            simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RIGHT);
            simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
            simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
            simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
            simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
            for (int i = 0; i < Database.l1.Count; i++)
            {
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_A);
                Thread.Sleep(200);
                //SendKeys.SendWait(sas); 
                simulator.Keyboard.TextEntry(sas); Thread.Sleep(200);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_A); Thread.Sleep(200);
                /*Thread thread = new Thread(() => Clipboard.SetText(Database.l1[i] + Database.l2[i] + Database.l3[i] + Database.l4[i] + Database.l5[i])); Thread.Sleep(200);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                Thread.Sleep(200);
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_V);*/
                simulator.Keyboard.TextEntry(Database.l1[i] + Database.l2[i] + Database.l3[i] + Database.l4[i] + Database.l5[i]); Thread.Sleep(200);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_A);
                Thread.Sleep(200); 
                SendKeys.SendWait(Database.o[i].ToString());
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                Thread.Sleep(200);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                Thread.Sleep(200);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.TAB);
                Thread.Sleep(1000); string[] arr;
                
                if (File.Exists($"Составленные{iterator}_0.txt"))
                    arr = File.ReadAllLines($"Составленные{iterator}_0.txt");
                else { 
                    while(!File.Exists($"Составленные{iterator}_0.txt"))
                        Thread.Sleep(1000);
                    arr = File.ReadAllLines($"Составленные{iterator}_0.txt");
                }

                Thread.Sleep(200);
                GetFileVars(iterator);
                Thread.Sleep(200);

                              
                if (iterator % 10 == 0)
                {
                    iterator = 0;
                    for (int j = 1; j < 11; j++) File.Delete($"Составленные{j}_0.txt");
                }
                iterator++;
                r.FillSchoodDG(Database.l1[i] + Database.l2[i] + Database.l3[i] + Database.l4[i] + Database.l5[i], KE.ToString(), LowerBorder.ToString(), UpperBorder.ToString(), Database.o[i].ToString(), LongestStep.ToString(), NoRepeatAmount.ToString());
                
            }
            SendKeys.SendWait("%{F4}");
            MessageBox.Show("Рассчеты завершены.");
            r.FillBestResultsDG();
            
        }
        string sas, sus, sos; List<int> o = Database.o;

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
            if (numericUpDown1.Value > numericUpDown2.Value) numericUpDown1.Value = numericUpDown2.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < numericUpDown1.Value) numericUpDown2.Value = numericUpDown1.Value;
        }

        Process s; IntPtr f;
        
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Process myProcess = Process.Start(@"GaugeBlockv3-1.exe"); Thread.Sleep(1000); s = myProcess;
           
            ProgramCycles();
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
