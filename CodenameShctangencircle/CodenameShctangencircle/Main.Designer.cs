
namespace CodenameShctangencircle
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.A11TB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.step5TB = new System.Windows.Forms.TextBox();
            this.step4TB = new System.Windows.Forms.TextBox();
            this.step3TB = new System.Windows.Forms.TextBox();
            this.step2TB = new System.Windows.Forms.TextBox();
            this.step1TB = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ResBox = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.StepsResultBox = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1448, 426);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.A11TB);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.step5TB);
            this.tabPage1.Controls.Add(this.step4TB);
            this.tabPage1.Controls.Add(this.step3TB);
            this.tabPage1.Controls.Add(this.step2TB);
            this.tabPage1.Controls.Add(this.step1TB);
            this.tabPage1.Controls.Add(this.Start);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1440, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Параметры";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // A11TB
            // 
            this.A11TB.Location = new System.Drawing.Point(6, 51);
            this.A11TB.Name = "A11TB";
            this.A11TB.Size = new System.Drawing.Size(79, 20);
            this.A11TB.TabIndex = 7;
            this.A11TB.Text = "0.500";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // step5TB
            // 
            this.step5TB.Location = new System.Drawing.Point(146, 6);
            this.step5TB.Name = "step5TB";
            this.step5TB.Size = new System.Drawing.Size(29, 20);
            this.step5TB.TabIndex = 5;
            this.step5TB.Text = "10";
            // 
            // step4TB
            // 
            this.step4TB.Location = new System.Drawing.Point(111, 6);
            this.step4TB.Name = "step4TB";
            this.step4TB.Size = new System.Drawing.Size(29, 20);
            this.step4TB.TabIndex = 4;
            this.step4TB.Text = "1";
            // 
            // step3TB
            // 
            this.step3TB.Location = new System.Drawing.Point(76, 6);
            this.step3TB.Name = "step3TB";
            this.step3TB.Size = new System.Drawing.Size(29, 20);
            this.step3TB.TabIndex = 3;
            this.step3TB.Text = "0.1";
            // 
            // step2TB
            // 
            this.step2TB.Location = new System.Drawing.Point(41, 6);
            this.step2TB.Name = "step2TB";
            this.step2TB.Size = new System.Drawing.Size(29, 20);
            this.step2TB.TabIndex = 2;
            this.step2TB.Text = "0.05";
            // 
            // step1TB
            // 
            this.step1TB.Location = new System.Drawing.Point(6, 6);
            this.step1TB.Name = "step1TB";
            this.step1TB.Size = new System.Drawing.Size(29, 20);
            this.step1TB.TabIndex = 1;
            this.step1TB.Text = "0.005";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(674, 371);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(88, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "Создать файл";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ResBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1440, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Результаты циклов";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ResBox
            // 
            this.ResBox.FormattingEnabled = true;
            this.ResBox.Location = new System.Drawing.Point(6, 6);
            this.ResBox.Name = "ResBox";
            this.ResBox.Size = new System.Drawing.Size(756, 381);
            this.ResBox.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.StepsResultBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1440, 400);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Результаты подбора";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // StepsResultBox
            // 
            this.StepsResultBox.FormattingEnabled = true;
            this.StepsResultBox.HorizontalScrollbar = true;
            this.StepsResultBox.Location = new System.Drawing.Point(-4, 0);
            this.StepsResultBox.Name = "StepsResultBox";
            this.StepsResultBox.Size = new System.Drawing.Size(1438, 407);
            this.StepsResultBox.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(599, 66);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 8;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1472, 452);
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.ListBox ResBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox StepsResultBox;
        private System.Windows.Forms.TextBox step5TB;
        private System.Windows.Forms.TextBox step4TB;
        private System.Windows.Forms.TextBox step3TB;
        private System.Windows.Forms.TextBox step2TB;
        private System.Windows.Forms.TextBox step1TB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox A11TB;
        private System.Windows.Forms.ListBox listBox1;
    }
}

