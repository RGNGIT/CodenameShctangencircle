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
            dataGridView1.Rows.Add("Если A11=0,500 и (δ1=0,005; δ2=0,01; δ3=0,1; δ4=1; δ5=10), то", "n1 = (K1 - 99)", "n2 = (K2  -0,5K1)", "n3  = (K3 - 0,1K2)", "n4 = (K4 - 0,1K3)", "n5 = (K5-0,1K4)");
            dataGridView1.Rows.Add("Если A11=0,500 и (δ1=0,005; δ2=0,01; δ3=0,1; δ4=0,5; δ5=10), то", "n1 = (K1 - 99)", "n2 = (K2  -0,5K1)", "n3  = (K3 - 0,1K2)", "n4 = (K4 - 0,2K3)", "n5 = (K5-0,05K4)");
            dataGridView1.Rows.Add("Если A11=0,500 и (δ1=0,005; δ2=0,05; δ3=0,1; δ4=1; δ5=10), то ", "n1 = (K1 - 99)", "n2 = (K2  -0,1K1)", "n3  = (K3 - 0,5K2)", "n4 = (K4-0,1K3)", "n5 = (K5-0,1K4)");
            dataGridView1.Rows.Add("Если A11=0,505 и (δ1=0,005; δ2=0,01; δ3=0,1; δ4=1; δ5=10), то", "n1 = (K1 - 100)", "n2 = (K2  -0,5K1)", "n3  = (K3 - 0,1K2)", "n4 = (K4 - 0,1K3)", "n5 = (K5-0,1K4)");
            dataGridView1.Rows.Add("Если A11=0,505 и (δ1=0,005; δ2=0,01; δ3=0,1; δ4=0,5; δ5=10), то", "n1 = (K1 - 100)", "n2 = (K2  -0,5K1)", "n3  = (K3 - 0,1K2)", "n4 = (K4 - 0,2K3)", "n5 = (K5-0,05K4)");
            dataGridView1.Rows.Add("Если A11=0,505 и (δ1=0,005; δ2=0,05; δ3=0,1; δ4=1; δ5=10), то ", "n1 = (K1 - 100)", "n2 = (K2  -0,1K1)", "n3  = (K3 - 0,5K2)", "n4 = (K4-0,1K3)", "n5 = (K5-0,1K4)");
        }

        public List<string> Output = new List<string>();
        public List<string> OutputVisual = new List<string>();
        public List<string> stepOutput = new List<string>();

        private void Start_Click(object sender, EventArgs e)
        {
            Output.Clear();
            OutputVisual.Clear();
            stepOutput.Clear();
            dataGridViewCyclesRes.Rows.Clear();
            dataGridViewRes.Rows.Clear();
            dataGridViewFin.Rows.Clear();
            Database.ClearShit();
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
            button1_Click(sender, e);
            DrawTable();
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
            MessageBox.Show("Рассчеты завершены. Проверяйте вкладки выше");
        }

        void DrawTable()
        {
            string CheckDif = string.Empty;
            for(int i = 0, j = 0; i < dataGridViewCyclesRes.Rows.Count - 1; i++)
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

        double NoRepeatAmount, LongestStep, LowerBorder, UpperBorder, KE;

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
            sb = new StringBuilder(Temp);
            sb.Remove(0, 1);
            KE = Convert.ToDouble(sb.ToString());
            
        }

        void ProgramCycles()
        {
            InputSimulator simulator = new InputSimulator();
            IntPtr w = FindWindow(null, "Поиск лучшего набора и расчет характеристик"); IntPtr frm1 = FindWindow(null, "Form1"); if (frm1.ToInt32() == 0) MessageBox.Show("Окно не найдено :c");
            //BringWindowToTop(w); 
            f = frm1;
            if (w.ToInt32() == 0) MessageBox.Show("Окно не найдено :c");
            ShowWindow(w, 9);
            SetForegroundWindow(w);
            
            SendKeys.SendWait("{RIGHT}"); 
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}"); 
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            for (int i = 0; i < dataGridViewRes.Rows.Count-1; i++)
            {
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_A);
                Thread.Sleep(200);
                SendKeys.SendWait(sas);
                SendKeys.SendWait("{TAB}");
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_A);
                Thread thread = new Thread(() => Clipboard.SetData(DataFormats.Text, Database.l1[i] + Database.l2[i] + Database.l3[i] + Database.l4[i] + Database.l5[i]));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                Thread.Sleep(200);
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_V);
                SendKeys.SendWait("{TAB}");
                simulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.VK_A);
                Thread.Sleep(200); 
                SendKeys.SendWait(Database.o[i].ToString());
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                Thread.Sleep(200);
                SendKeys.SendWait("{ENTER}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}"); 
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}"); 
                SendKeys.SendWait("{TAB}"); 
                Thread.Sleep(200);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                SendKeys.SendWait("{TAB}");
                Thread.Sleep(4000);
                string[] arr = File.ReadAllLines($"Составленные{iterator}_0.txt");

                Thread.Sleep(200);
                GetFileVars(iterator);
                Thread.Sleep(200);

                              
                if (iterator % 10 == 0)
                {
                    iterator = 0;
                    for (int j = 1; j < 11; j++) File.Delete($"Составленные{j}_0.txt");
                }
            
                iterator++;
                
               
            }
            SendKeys.SendWait("%{F4}");
        }
        string sas, sus, sos; List<int> o = Database.o;
        Process s; IntPtr f;
        
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
      
            Process myProcess = Process.Start(@"GaugeBlockv3-1.exe"); Thread.Sleep(1000); s = myProcess;
            ProgramCycles();
        }
        int iterator = 1;

        private void Main_Load(object sender, EventArgs e)
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
