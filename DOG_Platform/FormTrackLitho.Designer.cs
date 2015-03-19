namespace DOGPlatform
{
    partial class FormTrackLitho
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbLogName = new System.Windows.Forms.ComboBox();
            this.tbxJH = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.nUDLeftValue = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.cbbCurveColor = new System.Windows.Forms.ComboBox();
            this.nUDTrackWidth = new System.Windows.Forms.NumericUpDown();
            this.label49 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.nUDExtractPoints_leftLog = new System.Windows.Forms.NumericUpDown();
            this.label39 = new System.Windows.Forms.Label();
            this.tbxTrackID = new System.Windows.Forms.TextBox();
            this.tbxLogname = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDLeftValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTrackWidth)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDExtractPoints_leftLog)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(480, 348);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(472, 322);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(83, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxLogname);
            this.groupBox1.Controls.Add(this.label64);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbbLogName);
            this.groupBox1.Controls.Add(this.tbxJH);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.nUDLeftValue);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.cbbCurveColor);
            this.groupBox1.Controls.Add(this.nUDTrackWidth);
            this.groupBox1.Controls.Add(this.label49);
            this.groupBox1.Location = new System.Drawing.Point(20, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "道宽和道名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 73;
            this.label3.Text = "颜色方案";
            // 
            // cbbLogName
            // 
            this.cbbLogName.FormattingEnabled = true;
            this.cbbLogName.Items.AddRange(new object[] {
            "SP",
            "GR"});
            this.cbbLogName.Location = new System.Drawing.Point(332, 46);
            this.cbbLogName.Name = "cbbLogName";
            this.cbbLogName.Size = new System.Drawing.Size(61, 20);
            this.cbbLogName.TabIndex = 72;
            this.cbbLogName.Text = "空白";
            // 
            // tbxJH
            // 
            this.tbxJH.Location = new System.Drawing.Point(68, 46);
            this.tbxJH.Name = "tbxJH";
            this.tbxJH.Size = new System.Drawing.Size(70, 21);
            this.tbxJH.TabIndex = 71;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 70;
            this.label2.Text = "井名";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(160, 131);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 67;
            this.checkBox1.Text = "岩性控制宽度";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // nUDLeftValue
            // 
            this.nUDLeftValue.AllowDrop = true;
            this.nUDLeftValue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUDLeftValue.Location = new System.Drawing.Point(94, 127);
            this.nUDLeftValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nUDLeftValue.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.nUDLeftValue.Name = "nUDLeftValue";
            this.nUDLeftValue.Size = new System.Drawing.Size(40, 21);
            this.nUDLeftValue.TabIndex = 61;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(35, 131);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(53, 12);
            this.label34.TabIndex = 60;
            this.label34.Text = "油气列宽";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(38, 92);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(29, 12);
            this.label37.TabIndex = 57;
            this.label37.Text = "颜色";
            // 
            // cbbCurveColor
            // 
            this.cbbCurveColor.BackColor = System.Drawing.Color.Blue;
            this.cbbCurveColor.FormattingEnabled = true;
            this.cbbCurveColor.Location = new System.Drawing.Point(73, 88);
            this.cbbCurveColor.Name = "cbbCurveColor";
            this.cbbCurveColor.Size = new System.Drawing.Size(61, 20);
            this.cbbCurveColor.TabIndex = 56;
            // 
            // nUDTrackWidth
            // 
            this.nUDTrackWidth.AllowDrop = true;
            this.nUDTrackWidth.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUDTrackWidth.Location = new System.Drawing.Point(190, 90);
            this.nUDTrackWidth.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nUDTrackWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUDTrackWidth.Name = "nUDTrackWidth";
            this.nUDTrackWidth.Size = new System.Drawing.Size(46, 21);
            this.nUDTrackWidth.TabIndex = 30;
            this.nUDTrackWidth.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(155, 93);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(29, 12);
            this.label49.TabIndex = 29;
            this.label49.Text = "道宽";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.nUDExtractPoints_leftLog);
            this.tabPage2.Controls.Add(this.label39);
            this.tabPage2.Controls.Add(this.tbxTrackID);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(472, 446);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "高级";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 75;
            this.label1.Text = "道ID";
            // 
            // nUDExtractPoints_leftLog
            // 
            this.nUDExtractPoints_leftLog.AllowDrop = true;
            this.nUDExtractPoints_leftLog.Location = new System.Drawing.Point(100, 84);
            this.nUDExtractPoints_leftLog.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUDExtractPoints_leftLog.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDExtractPoints_leftLog.Name = "nUDExtractPoints_leftLog";
            this.nUDExtractPoints_leftLog.Size = new System.Drawing.Size(40, 21);
            this.nUDExtractPoints_leftLog.TabIndex = 73;
            this.nUDExtractPoints_leftLog.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(30, 89);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(65, 12);
            this.label39.TabIndex = 72;
            this.label39.Text = "值抽稀点数";
            // 
            // tbxTrackID
            // 
            this.tbxTrackID.Location = new System.Drawing.Point(186, 46);
            this.tbxTrackID.Name = "tbxTrackID";
            this.tbxTrackID.Size = new System.Drawing.Size(70, 21);
            this.tbxTrackID.TabIndex = 70;
            // 
            // tbxLogname
            // 
            this.tbxLogname.Location = new System.Drawing.Point(180, 46);
            this.tbxLogname.Name = "tbxLogname";
            this.tbxLogname.Size = new System.Drawing.Size(70, 21);
            this.tbxLogname.TabIndex = 76;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(147, 49);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(29, 12);
            this.label64.TabIndex = 75;
            this.label64.Text = "道名";
            // 
            // FormTrackLitho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 348);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormTrackLitho";
            this.Text = "FormLithoTrack";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDLeftValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTrackWidth)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDExtractPoints_leftLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbLogName;
        private System.Windows.Forms.TextBox tbxJH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown nUDLeftValue;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox cbbCurveColor;
        private System.Windows.Forms.NumericUpDown nUDTrackWidth;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nUDExtractPoints_leftLog;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox tbxTrackID;
        private System.Windows.Forms.TextBox tbxLogname;
        private System.Windows.Forms.Label label64;
    }
}