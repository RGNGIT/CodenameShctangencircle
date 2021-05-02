/*
public class Form1 : Form
{
    // Fields
    private IContainer components;
    private TabControl tabControl1;
    private TabPage tabOptimize;
    private TabPage tabChecking;
    private Button btnStartOptimize;
    private Button btnSaveSearch;
    private Button btnOpenSearch;
    private CheckBox chkbxAuto;
    private Label label10;
    private TextBox tbxAmax;
    private Label label1;
    private TextBox tbxN;
    private Label label2;
    private TextBox tbxA0;
    private Label label4;
    private Label label3;
    private Label label9;
    private TextBox tbxDelta1;
    private TextBox tbxDelta5;
    private Label label5;
    private Label label8;
    private TextBox tbxDelta2;
    private TextBox tbxDelta4;
    private Label label6;
    private Label label7;
    private TextBox tbxDelta3;
    private RichTextBox rtbMain;
    private Timer timer;
    private Label lblStatus;
    private TextBox tbxStep;
    private Label label11;
    private TextBox textBox1;
    private Label label13;
    private Button btnSaveParameters;
    private Button btnOpenSet;
    private Button btnDefineParameters;
    private OpenFileDialog openFileDialog1;
    public ProgressBar pBar;
    public Label lblOperation;
    private TextBox tbxSizes;
    private Label label14;
    private TextBox tbxNForCheck;
    private Label label12;
    private Button button1;
    private TextBox textBox2;
    private Button button2;
    private TextBox textBox3;
    private Label label15;

    // Methods
    public Form1()
    {
        this.InitializeComponent();
    }

    private unsafe void btnDefineParameters_Click(object sender, EventArgs e)
    {
        this.btnOpenSet.Enabled = false;
        this.btnDefineParameters.Enabled = false;
        this.btnSaveParameters.Enabled = false;
        this.tabOptimize.Enabled = false;
        this.lblStatus.Visible = true;
        this.lblStatus.Update();
        this.pBar.Visible = true;
        bool flag = false;
        try
        {
            GaugeCalculations.CompositeStep = double.Parse(this.textBox1.Text.Replace('.', ','));
            flag = true;
        }
        catch (Exception)
        {
            MessageBox.Show("Введен некорректный шаг! Пожалуйста проверьте его правильность!");
        }
        if (flag)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                int n = int.Parse(this.tbxNForCheck.Text);
                string[] strArray = this.tbxSizes.Text.Trim(new char[] { ';' }).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                double[] numArray = new double[n];
                int index = 0;
                while (true)
                {
                    if (index >= n)
                    {
                        int[] item = new int[5];
                        double[] d = new double[5];
                        double num3 = 0.0;
                        double amax = 0.0;
                        num3 = numArray[0];
                        amax = numArray[n - 1];
                        int num6 = 0;
                        d[num6] = Math.Round(Math.Abs((double) (numArray[1] - numArray[0])), 4);
                        int* numPtr1 = &(item[num6]);
                        numPtr1[0]++;
                        bool complete = true;
                        try
                        {
                            for (int i = 1; i < n; i++)
                            {
                                if (Math.Abs((double) ((numArray[i] - numArray[i - 1]) - d[num6])) < 1E-06)
                                {
                                    int* numPtr2 = &(item[num6]);
                                    numPtr2[0]++;
                                }
                                else
                                {
                                    num6++;
                                    d[num6] = Math.Round(Math.Abs((double) (numArray[i] - numArray[i - 1])), 4);
                                    int* numPtr3 = &(item[num6]);
                                    numPtr3[0]++;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            complete = false;
                            MessageBox.Show("Для данного набора не удалось подсчитать набор дельта, значение универсальной функции вычислено не будет!");
                        }
                        GaugeCalculations.Sizes = numArray;
                        GaugeCalculations.Sizes2 = numArray;
                        GaugeCalculations.Sets.Clear();
                        GaugeCalculations.Sets.Add(item);
                        GaugeCalculations.DefineVariables1(n, num3, amax, d, 0.005, complete);
                        GaugeCalculations.SummarySizes = GaugeCalculations.Sizes.Sum();
                        GaugeCalculations.DefineParameters(this.pBar, this.lblStatus, this.lblOperation, 1, 1);
                        stopwatch.Stop();
                        GaugeCalculations.SummaryTime = stopwatch.Elapsed;
                        GaugeCalculations.KE = GaugeCalculations.KE2;
                        GaugeCalculations.SequenceCount = GaugeCalculations.SequenceCount2;
                        GaugeCalculations.SequenceBegin = GaugeCalculations.SequenceBegin2;
                        GaugeCalculations.SequenceEnd = GaugeCalculations.SequenceEnd2;
                        GaugeCalculations.SummaryCount = GaugeCalculations.SummaryCount2;
                        this.rtbMain.Text = GaugeCalculations.Print1(GaugeCalculations.Sets.Last<int[]>(), true, complete);
                        break;
                    }
                    numArray[index] = double.Parse(strArray[index].Replace('.', ','));
                    index++;
                }
            }
            catch (MyException exception1)
            {
                MessageBox.Show(exception1.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Введены некорректные данные или количество введенных мер не совпадает с N! Пожалуйста проверьте правильность значений!");
            }
        }
        this.btnOpenSet.Enabled = true;
        this.btnDefineParameters.Enabled = true;
        this.btnSaveParameters.Enabled = true;
        this.tabOptimize.Enabled = true;
        this.lblStatus.Visible = false;
        this.lblStatus.Update();
        this.pBar.Visible = false;
    }

    private void btnOpenSearch_Click(object sender, EventArgs e)
    {
        if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            StreamReader reader = new StreamReader(this.openFileDialog1.FileName);
            int num = 4;
            int index = 0;
            string[][] strArray = new string[num][];
            while (true)
            {
                if (reader.EndOfStream || (index >= num))
                {
                    reader.DiscardBufferedData();
                    reader.Close();
                    break;
                }
                strArray[index] = reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                switch (index)
                {
                    case 0:
                        this.tbxN.Text = strArray[index][0];
                        break;

                    case 1:
                        this.tbxA0.Text = strArray[index][0];
                        this.tbxAmax.Text = strArray[index][1];
                        break;

                    case 2:
                        this.tbxDelta1.Text = strArray[index][0];
                        this.tbxDelta2.Text = strArray[index][1];
                        this.tbxDelta3.Text = strArray[index][2];
                        this.tbxDelta4.Text = strArray[index][3];
                        this.tbxDelta5.Text = strArray[index][4];
                        break;

                    case 3:
                        this.tbxStep.Text = strArray[index][0];
                        break;

                    default:
                        break;
                }
                index++;
            }
        }
    }

    private void btnOpenSet_Click(object sender, EventArgs e)
    {
        if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            StreamReader reader = new StreamReader(this.openFileDialog1.FileName);
            int num = 2;
            int index = 0;
            string[][] strArray = new string[num][];
            try
            {
                while (!reader.EndOfStream && (index < num))
                {
                    strArray[index] = reader.ReadLine().Trim(new char[] { ';' }).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    switch (index)
                    {
                        case 0:
                            this.tbxNForCheck.Text = strArray[index][0];
                            break;

                        case 1:
                            this.tbxSizes.Clear();
                            for (int i = 0; i < strArray[index].Length; i++)
                            {
                                this.tbxSizes.AppendText(strArray[index][i] + " ");
                            }
                            break;

                        default:
                            break;
                    }
                    index++;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("В исходном файле некорректные данные! Пожалуйста проверьте их правильность!");
            }
            reader.DiscardBufferedData();
            reader.Close();
        }
    }

    private void btnSaveParameters_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < 10; i++)
        {
            string name = "Составленные" + (i + 1);
            object[] objArray = new object[] { name, "_", 0, ".txt" };
            if (!File.Exists(string.Concat(objArray)))
            {
                this.CompositeWriter(name, true);
                return;
            }
            if (i == 9)
            {
                name = "Составленные" + 1;
                this.CompositeWriter(name, true);
                return;
            }
        }
    }

    private void btnSaveSearch_Click(object sender, EventArgs e)
    {
        int num = 0;
        while (true)
        {
            if (num < 10)
            {
                string name = "Набор" + (num + 1);
                object[] objArray = new object[] { name, "_", 0, ".txt" };
                if (!File.Exists(string.Concat(objArray)))
                {
                    this.SetWriter(name);
                }
                else
                {
                    if (num != 9)
                    {
                        num++;
                        continue;
                    }
                    name = "Набор" + 1;
                    this.SetWriter(name);
                }
            }
            if (this.chkbxAuto.Checked)
            {
                for (int i = 0; i < 10; i++)
                {
                    string name = "Составленные" + (i + 1);
                    object[] objArray2 = new object[] { name, "_", 0, ".txt" };
                    if (!File.Exists(string.Concat(objArray2)))
                    {
                        this.CompositeWriter(name);
                        return;
                    }
                    if (i == 9)
                    {
                        name = "Составленные" + 1;
                        this.CompositeWriter(name);
                        return;
                    }
                }
            }
            return;
        }
    }

    private void btnStartOptimize_Click(object sender, EventArgs e)
    {
        this.rtbMain.Clear();
        this.lblStatus.Visible = true;
        this.lblStatus.Text = "Подождите, идет расчет...";
        this.lblStatus.Update();
        this.pBar.Visible = true;
        this.btnOpenSearch.Enabled = false;
        this.btnSaveSearch.Enabled = false;
        this.btnStartOptimize.Enabled = false;
        this.tabChecking.Enabled = false;
        this.chkbxAuto.Enabled = false;
        bool flag = false;
        try
        {
            double[] d = new double[] { double.Parse(this.tbxDelta1.Text.Replace('.', ',')), double.Parse(this.tbxDelta2.Text.Replace('.', ',')), double.Parse(this.tbxDelta3.Text.Replace('.', ',')), double.Parse(this.tbxDelta4.Text.Replace('.', ',')), double.Parse(this.tbxDelta5.Text.Replace('.', ',')) };
            GaugeCalculations.DefineVariables(int.Parse(this.tbxN.Text), double.Parse(this.tbxA0.Text.Replace('.', ',')), double.Parse(this.tbxAmax.Text.Replace('.', ',')), d, double.Parse(this.tbxStep.Text.Replace('.', ',')));
            flag = true;
        }
        catch (MyException exception1)
        {
            MessageBox.Show(exception1.Message);
        }
        catch (Exception)
        {
            MessageBox.Show("Введены некорректные данные! Пожалуйста проверьте их правильность!");
        }
        if (flag)
        {
            GaugeCalculations.SearchBestSet(this.chkbxAuto.Checked, this.pBar, this.lblStatus, this.lblOperation, this.rtbMain);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\nОбщее время расчета (мм:сс:мс) = {0}:{1}:{2:###}\r\n", GaugeCalculations.SummaryTime.Minutes, GaugeCalculations.SummaryTime.Seconds, GaugeCalculations.SummaryTime.Milliseconds);
            if (this.chkbxAuto.Checked)
            {
                builder.Append("Общее число наборов с близким значением КЭ = " + GaugeCalculations.Sets.Count + "\r\n");
            }
            if (!this.chkbxAuto.Checked)
            {
                builder.Append("Общее число наборов = " + GaugeCalculations.Sets.Count + "\r\n");
            }
            this.rtbMain.Text = this.rtbMain.Text + builder.ToString();
        }
        this.lblStatus.Visible = false;
        this.lblStatus.Refresh();
        this.pBar.Visible = false;
        this.btnOpenSearch.Enabled = true;
        this.btnSaveSearch.Enabled = true;
        this.btnStartOptimize.Enabled = true;
        this.tabChecking.Enabled = true;
        this.chkbxAuto.Enabled = true;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        CompositeBlock structure = new CompositeBlock();
        MessageBox.Show(Marshal.SizeOf(structure).ToString());
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
        try
        {
            MessageBox.Show(GaugeCalculations.FindCompositeBlock(double.Parse(this.textBox2.Text.Replace('.', ','))));
        }
        catch (Exception)
        {
            MessageBox.Show("Введены некорректные данные!");
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        try
        {
            MessageBox.Show(GaugeCalculations.ShowCompositeBlock(int.Parse(this.textBox3.Text)));
        }
        catch (Exception)
        {
            MessageBox.Show("Введены некорректные данные!");
        }
    }

    public void CompositeWriter(string name)
    {
        if (GaugeCalculations.CompositeBlocks.Count > 0)
        {
            for (int i = 0; i < GaugeCalculations.TempFiles.Count; i++)
            {
                string sourceFileName = GaugeCalculations.TempFiles[i];
                object[] objArray = new object[] { name, "_", i, ".txt" };
                if (File.Exists(string.Concat(objArray)))
                {
                    File.Delete(string.Concat(new object[] { name, "_", i, ".txt" }));
                }
                File.Copy(sourceFileName, string.Concat(new object[] { name, "_", i, ".txt" }));
            }
        }
    }

    public void CompositeWriter(string name, bool b)
    {
        if (GaugeCalculations.CompositeBlocks.Count > 0)
        {
            if (!b)
            {
                for (int i = 0; i < GaugeCalculations.TempFiles.Count; i++)
                {
                    string sourceFileName = GaugeCalculations.TempFiles[i];
                    object[] objArray = new object[] { name, "_", i, ".txt" };
                    if (File.Exists(string.Concat(objArray)))
                    {
                        File.Delete(string.Concat(new object[] { name, "_", i, ".txt" }));
                    }
                    File.Copy(sourceFileName, string.Concat(new object[] { name, "_", i, ".txt" }));
                }
            }
            else
            {
                object[] objArray4 = new object[] { name, "_", 0, ".txt" };
                StreamWriter writer = new StreamWriter(string.Concat(objArray4), false);
                foreach (string str2 in GaugeCalculations.PrintComposite(GaugeCalculations.CompositeBlocks))
                {
                    writer.Write(str2);
                }
                writer.Close();
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new Container();
        this.tabControl1 = new TabControl();
        this.tabOptimize = new TabPage();
        this.label15 = new Label();
        this.button2 = new Button();
        this.textBox3 = new TextBox();
        this.textBox2 = new TextBox();
        this.button1 = new Button();
        this.tbxStep = new TextBox();
        this.label11 = new Label();
        this.btnSaveSearch = new Button();
        this.btnOpenSearch = new Button();
        this.chkbxAuto = new CheckBox();
        this.label10 = new Label();
        this.tbxAmax = new TextBox();
        this.label1 = new Label();
        this.tbxN = new TextBox();
        this.label2 = new Label();
        this.tbxA0 = new TextBox();
        this.label4 = new Label();
        this.label3 = new Label();
        this.label9 = new Label();
        this.tbxDelta1 = new TextBox();
        this.tbxDelta5 = new TextBox();
        this.label5 = new Label();
        this.label8 = new Label();
        this.tbxDelta2 = new TextBox();
        this.tbxDelta4 = new TextBox();
        this.label6 = new Label();
        this.label7 = new Label();
        this.tbxDelta3 = new TextBox();
        this.btnStartOptimize = new Button();
        this.tabChecking = new TabPage();
        this.label14 = new Label();
        this.tbxNForCheck = new TextBox();
        this.label12 = new Label();
        this.tbxSizes = new TextBox();
        this.textBox1 = new TextBox();
        this.label13 = new Label();
        this.btnSaveParameters = new Button();
        this.btnOpenSet = new Button();
        this.btnDefineParameters = new Button();
        this.rtbMain = new RichTextBox();
        this.pBar = new ProgressBar();
        this.timer = new Timer(this.components);
        this.lblStatus = new Label();
        this.openFileDialog1 = new OpenFileDialog();
        this.lblOperation = new Label();
        this.tabControl1.SuspendLayout();
        this.tabOptimize.SuspendLayout();
        this.tabChecking.SuspendLayout();
        base.SuspendLayout();
        this.tabControl1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
        this.tabControl1.Controls.Add(this.tabOptimize);
        this.tabControl1.Controls.Add(this.tabChecking);
        this.tabControl1.Location = new Point(12, 12);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new Size(0x2eb, 0xbc);
        this.tabControl1.TabIndex = 0;
        this.tabOptimize.Controls.Add(this.label15);
        this.tabOptimize.Controls.Add(this.button2);
        this.tabOptimize.Controls.Add(this.textBox3);
        this.tabOptimize.Controls.Add(this.textBox2);
        this.tabOptimize.Controls.Add(this.button1);
        this.tabOptimize.Controls.Add(this.tbxStep);
        this.tabOptimize.Controls.Add(this.label11);
        this.tabOptimize.Controls.Add(this.btnSaveSearch);
        this.tabOptimize.Controls.Add(this.btnOpenSearch);
        this.tabOptimize.Controls.Add(this.chkbxAuto);
        this.tabOptimize.Controls.Add(this.label10);
        this.tabOptimize.Controls.Add(this.tbxAmax);
        this.tabOptimize.Controls.Add(this.label1);
        this.tabOptimize.Controls.Add(this.tbxN);
        this.tabOptimize.Controls.Add(this.label2);
        this.tabOptimize.Controls.Add(this.tbxA0);
        this.tabOptimize.Controls.Add(this.label4);
        this.tabOptimize.Controls.Add(this.label3);
        this.tabOptimize.Controls.Add(this.label9);
        this.tabOptimize.Controls.Add(this.tbxDelta1);
        this.tabOptimize.Controls.Add(this.tbxDelta5);
        this.tabOptimize.Controls.Add(this.label5);
        this.tabOptimize.Controls.Add(this.label8);
        this.tabOptimize.Controls.Add(this.tbxDelta2);
        this.tabOptimize.Controls.Add(this.tbxDelta4);
        this.tabOptimize.Controls.Add(this.label6);
        this.tabOptimize.Controls.Add(this.label7);
        this.tabOptimize.Controls.Add(this.tbxDelta3);
        this.tabOptimize.Controls.Add(this.btnStartOptimize);
        this.tabOptimize.Location = new Point(4, 0x16);
        this.tabOptimize.Name = "tabOptimize";
        this.tabOptimize.Padding = new Padding(3);
        this.tabOptimize.Size = new Size(0x2e3, 0xa2);
        this.tabOptimize.TabIndex = 0;
        this.tabOptimize.Text = "Оптимизационный расчет";
        this.tabOptimize.UseVisualStyleBackColor = true;
        this.label15.AutoSize = true;
        this.label15.Location = new Point(0x1c8, 0x23);
        this.label15.Name = "label15";
        this.label15.Size = new Size(0xc1, 13);
        this.label15.TabIndex = 0x4b;
        this.label15.Text = "( рекомендуется размер до 100 мм )";
        this.button2.Location = new Point(0x235, 0x51);
        this.button2.Name = "button2";
        this.button2.Size = new Size(0xab, 0x17);
        this.button2.TabIndex = 0x4a;
        this.button2.Text = "Найти меру по номеру";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new EventHandler(this.button2_Click);
        this.textBox3.Location = new Point(0x1cb, 0x54);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new Size(100, 20);
        this.textBox3.TabIndex = 0x49;
        this.textBox3.Text = "0";
        this.textBox2.Location = new Point(0x1cb, 0x6b);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new Size(100, 20);
        this.textBox2.TabIndex = 0x48;
        this.textBox2.Text = "0";
        this.button1.Location = new Point(0x235, 0x68);
        this.button1.Name = "button1";
        this.button1.Size = new Size(0xab, 0x17);
        this.button1.TabIndex = 0x47;
        this.button1.Text = "Найти меру заданной длины";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new EventHandler(this.button1_Click_1);
        this.tbxStep.Location = new Point(0xfc, 0x51);
        this.tbxStep.Name = "tbxStep";
        this.tbxStep.Size = new Size(0x39, 20);
        this.tbxStep.TabIndex = 0x45;
        this.label11.AutoSize = true;
        this.label11.Location = new Point(6, 0x54);
        this.label11.Name = "label11";
        this.label11.Size = new Size(0xf8, 13);
        this.label11.TabIndex = 70;
        this.label11.Text = "Требуемый шаг ряда составленных размеров: ";
        this.btnSaveSearch.Location = new Point(0xfc, 0x68);
        this.btnSaveSearch.Name = "btnSaveSearch";
        this.btnSaveSearch.Size = new Size(0x9b, 0x17);
        this.btnSaveSearch.TabIndex = 0x44;
        this.btnSaveSearch.Text = "Сохранить в файл набор";
        this.btnSaveSearch.UseVisualStyleBackColor = true;
        this.btnSaveSearch.Click += new EventHandler(this.btnSaveSearch_Click);
        this.btnOpenSearch.Location = new Point(9, 0x68);
        this.btnOpenSearch.Name = "btnOpenSearch";
        this.btnOpenSearch.Size = new Size(0xdd, 0x17);
        this.btnOpenSearch.TabIndex = 0x43;
        this.btnOpenSearch.Text = "Открыть файл исходных параметров";
        this.btnOpenSearch.UseVisualStyleBackColor = true;
        this.btnOpenSearch.Click += new EventHandler(this.btnOpenSearch_Click);
        this.chkbxAuto.AutoSize = true;
        this.chkbxAuto.Location = new Point(0x79, 0x89);
        this.chkbxAuto.Name = "chkbxAuto";
        this.chkbxAuto.Size = new Size(0x145, 0x11);
        this.chkbxAuto.TabIndex = 0x42;
        this.chkbxAuto.Text = "Автоматически расчитать характеристики лучшего набора";
        this.chkbxAuto.UseVisualStyleBackColor = true;
        this.label10.AutoSize = true;
        this.label10.Location = new Point(0x265, 60);
        this.label10.Name = "label10";
        this.label10.Size = new Size(0x7e, 13);
        this.label10.TabIndex = 0x41;
        this.label10.Text = "(?1 < ?2 < ?3 < ?4 < ?5)";
        this.tbxAmax.Location = new Point(0x199, 0x20);
        this.tbxAmax.Name = "tbxAmax";
        this.tbxAmax.Size = new Size(0x26, 20);
        this.tbxAmax.TabIndex = 0x40;
        this.label1.AutoSize = true;
        this.label1.Location = new Point(6, 11);
        this.label1.Name = "label1";
        this.label1.Size = new Size(140, 13);
        this.label1.TabIndex = 0x31;
        this.label1.Text = "Общее количество мер N:";
        this.tbxN.Location = new Point(0x98, 8);
        this.tbxN.Name = "tbxN";
        this.tbxN.Size = new Size(0x26, 20);
        this.tbxN.TabIndex = 0x30;
        this.label2.AutoSize = true;
        this.label2.Location = new Point(0xc4, 11);
        this.label2.Name = "label2";
        this.label2.Size = new Size(0x4b, 13);
        this.label2.TabIndex = 50;
        this.label2.Text = "(N = 25 ...122)";
        this.tbxA0.Location = new Point(0xa9, 0x20);
        this.tbxA0.Name = "tbxA0";
        this.tbxA0.Size = new Size(0x26, 20);
        this.tbxA0.TabIndex = 0x33;
        this.label4.AutoSize = true;
        this.label4.Location = new Point(6, 0x23);
        this.label4.Name = "label4";
        this.label4.Size = new Size(0xa2, 13);
        this.label4.TabIndex = 0x34;
        this.label4.Text = "Размер наименьшей меры a0:";
        this.label3.AutoSize = true;
        this.label3.Location = new Point(0xd5, 0x23);
        this.label3.Name = "label3";
        this.label3.Size = new Size(0xb5, 13);
        this.label3.TabIndex = 0x35;
        this.label3.Text = "(a0 >= 0.5)     и наибольшей a max :";
        this.label9.AutoSize = true;
        this.label9.Location = new Point(0x217, 60);
        this.label9.Name = "label9";
        this.label9.Size = new Size(0x1d, 13);
        this.label9.TabIndex = 0x3f;
        this.label9.Text = "?5 =";
        this.tbxDelta1.Location = new Point(0xfc, 0x39);
        this.tbxDelta1.Name = "tbxDelta1";
        this.tbxDelta1.Size = new Size(0x26, 20);
        this.tbxDelta1.TabIndex = 0x36;
        this.tbxDelta5.Location = new Point(0x239, 0x39);
        this.tbxDelta5.Name = "tbxDelta5";
        this.tbxDelta5.Size = new Size(0x26, 20);
        this.tbxDelta5.TabIndex = 0x3e;
        this.label5.AutoSize = true;
        this.label5.Location = new Point(6, 60);
        this.label5.Name = "label5";
        this.label5.Size = new Size(0xee, 13);
        this.label5.TabIndex = 0x37;
        this.label5.Text = "Сочетание значений разницы размеров: ?1 =";
        this.label8.AutoSize = true;
        this.label8.Location = new Point(0x1c8, 60);
        this.label8.Name = "label8";
        this.label8.Size = new Size(0x1d, 13);
        this.label8.TabIndex = 0x3d;
        this.label8.Text = "?4 =";
        this.tbxDelta2.Location = new Point(330, 0x39);
        this.tbxDelta2.Name = "tbxDelta2";
        this.tbxDelta2.Size = new Size(0x26, 20);
        this.tbxDelta2.TabIndex = 0x38;
        this.tbxDelta4.Location = new Point(490, 0x39);
        this.tbxDelta4.Name = "tbxDelta4";
        this.tbxDelta4.Size = new Size(0x26, 20);
        this.tbxDelta4.TabIndex = 60;
        this.label6.AutoSize = true;
        this.label6.Location = new Point(0x128, 60);
        this.label6.Name = "label6";
        this.label6.Size = new Size(0x1d, 13);
        this.label6.TabIndex = 0x39;
        this.label6.Text = "?2 =";
        this.label7.AutoSize = true;
        this.label7.Location = new Point(0x177, 60);
        this.label7.Name = "label7";
        this.label7.Size = new Size(0x1d, 13);
        this.label7.TabIndex = 0x3b;
        this.label7.Text = "?3 =";
        this.tbxDelta3.Location = new Point(0x199, 0x39);
        this.tbxDelta3.Name = "tbxDelta3";
        this.tbxDelta3.Size = new Size(0x26, 20);
        this.tbxDelta3.TabIndex = 0x3a;
        this.btnStartOptimize.Location = new Point(9, 0x85);
        this.btnStartOptimize.Name = "btnStartOptimize";
        this.btnStartOptimize.Size = new Size(0x6b, 0x17);
        this.btnStartOptimize.TabIndex = 0;
        this.btnStartOptimize.Text = "Начать расчет";
        this.btnStartOptimize.UseVisualStyleBackColor = true;
        this.btnStartOptimize.Click += new EventHandler(this.btnStartOptimize_Click);
        this.tabChecking.Controls.Add(this.label14);
        this.tabChecking.Controls.Add(this.tbxNForCheck);
        this.tabChecking.Controls.Add(this.label12);
        this.tabChecking.Controls.Add(this.tbxSizes);
        this.tabChecking.Controls.Add(this.textBox1);
        this.tabChecking.Controls.Add(this.label13);
        this.tabChecking.Controls.Add(this.btnSaveParameters);
        this.tabChecking.Controls.Add(this.btnOpenSet);
        this.tabChecking.Controls.Add(this.btnDefineParameters);
        this.tabChecking.Location = new Point(4, 0x16);
        this.tabChecking.Name = "tabChecking";
        this.tabChecking.Padding = new Padding(3);
        this.tabChecking.Size = new Size(0x2e3, 0xa2);
        this.tabChecking.TabIndex = 1;
        this.tabChecking.Text = "Проверочный расчет";
        this.tabChecking.UseVisualStyleBackColor = true;
        this.label14.AutoSize = true;
        this.label14.Location = new Point(0x1a0, 0x22);
        this.label14.Name = "label14";
        this.label14.Size = new Size(0x4c, 13);
        this.label14.TabIndex = 60;
        this.label14.Text = "и сами меры:";
        this.tbxNForCheck.Location = new Point(0x161, 0x1f);
        this.tbxNForCheck.Name = "tbxNForCheck";
        this.tbxNForCheck.Size = new Size(0x39, 20);
        this.tbxNForCheck.TabIndex = 0x3b;
        this.label12.AutoSize = true;
        this.label12.Location = new Point(0xb5, 0x22);
        this.label12.Name = "label12";
        this.label12.Size = new Size(0xb0, 13);
        this.label12.TabIndex = 0x3a;
        this.label12.Text = "или введите количество мер N = ";
        this.tbxSizes.Location = new Point(9, 0x3a);
        this.tbxSizes.Name = "tbxSizes";
        this.tbxSizes.Size = new Size(0x2d4, 20);
        this.tbxSizes.TabIndex = 0x39;
        this.textBox1.Location = new Point(260, 12);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new Size(0x39, 20);
        this.textBox1.TabIndex = 0x37;
        this.label13.AutoSize = true;
        this.label13.Location = new Point(6, 13);
        this.label13.Name = "label13";
        this.label13.Size = new Size(0xf8, 13);
        this.label13.TabIndex = 0x38;
        this.label13.Text = "Требуемый шаг ряда составленных размеров: ";
        this.btnSaveParameters.Location = new Point(6, 0x54);
        this.btnSaveParameters.Name = "btnSaveParameters";
        this.btnSaveParameters.Size = new Size(0xf5, 0x17);
        this.btnSaveParameters.TabIndex = 0x36;
        this.btnSaveParameters.Text = "Сохранить в файл характеристики набора";
        this.btnSaveParameters.UseVisualStyleBackColor = true;
        this.btnSaveParameters.Click += new EventHandler(this.btnSaveParameters_Click);
        this.btnOpenSet.Location = new Point(9, 0x1d);
        this.btnOpenSet.Name = "btnOpenSet";
        this.btnOpenSet.Size = new Size(0xa6, 0x17);
        this.btnOpenSet.TabIndex = 0x35;
        this.btnOpenSet.Text = "Открыть файл с набором";
        this.btnOpenSet.UseVisualStyleBackColor = true;
        this.btnOpenSet.Click += new EventHandler(this.btnOpenSet_Click);
        this.btnDefineParameters.Location = new Point(6, 0x71);
        this.btnDefineParameters.Name = "btnDefineParameters";
        this.btnDefineParameters.Size = new Size(0x90, 0x17);
        this.btnDefineParameters.TabIndex = 0x34;
        this.btnDefineParameters.Text = "Начать расчет";
        this.btnDefineParameters.UseVisualStyleBackColor = true;
        this.btnDefineParameters.Click += new EventHandler(this.btnDefineParameters_Click);
        this.rtbMain.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
        this.rtbMain.Location = new Point(12, 0xeb);
        this.rtbMain.Name = "rtbMain";
        this.rtbMain.Size = new Size(0x2eb, 0xc0);
        this.rtbMain.TabIndex = 1;
        this.rtbMain.Text = "";
        this.pBar.Location = new Point(0x293, 0xce);
        this.pBar.Name = "pBar";
        this.pBar.Size = new Size(100, 0x17);
        this.pBar.TabIndex = 2;
        this.pBar.Visible = false;
        this.timer.Tick += new EventHandler(this.timer_Tick);
        this.lblStatus.AutoSize = true;
        this.lblStatus.Location = new Point(13, 0xd3);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new Size(0x8b, 13);
        this.lblStatus.TabIndex = 3;
        this.lblStatus.Text = "Подождите, идет расчет...";
        this.lblStatus.Visible = false;
        this.openFileDialog1.RestoreDirectory = true;
        this.lblOperation.AutoSize = true;
        this.lblOperation.Location = new Point(410, 0xd3);
        this.lblOperation.Name = "lblOperation";
        this.lblOperation.Size = new Size(0x29, 13);
        this.lblOperation.TabIndex = 4;
        this.lblOperation.Text = "label12";
        this.lblOperation.Visible = false;
        base.AutoScaleDimensions = new SizeF(6f, 13f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x303, 0x1c8);
        base.Controls.Add(this.lblOperation);
        base.Controls.Add(this.lblStatus);
        base.Controls.Add(this.pBar);
        base.Controls.Add(this.rtbMain);
        base.Controls.Add(this.tabControl1);
        base.Name = "Form1";
        this.Text = "Поиск лучшего набора и расчет характеристик";
        this.tabControl1.ResumeLayout(false);
        this.tabOptimize.ResumeLayout(false);
        this.tabOptimize.PerformLayout();
        this.tabChecking.ResumeLayout(false);
        this.tabChecking.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    private void SetWriter(string name)
    {
        if (GaugeCalculations.Sets.Count > 0)
        {
            for (int i = 0; i < GaugeCalculations.Sets.Count; i++)
            {
                object[] objArray = new object[] { name, "_", i, ".txt" };
                StreamWriter writer = new StreamWriter(string.Concat(objArray), false);
                writer.Write(GaugeCalculations.PrintSet(GaugeCalculations.Sets[i]));
                writer.Close();
            }
        }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        this.pBar.Value = (int) (GaugeCalculations.CurrentStep / 0x2710L);
        this.lblStatus.Text = GaugeCalculations.Name;
    }
}
*/