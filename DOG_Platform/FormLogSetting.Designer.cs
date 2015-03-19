namespace DOGPlatform
{
    partial class FormLogSetting
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbgInfor = new System.Windows.Forms.TabPage();
            this.btnDefaultSetting = new System.Windows.Forms.Button();
            this.btnSaveSetting = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxUnit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbDashStyle = new System.Windows.Forms.ComboBox();
            this.tbxLogname = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.tbxJH = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxIsLogGrid = new System.Windows.Forms.CheckBox();
            this.nUDLineWidth = new System.Windows.Forms.NumericUpDown();
            this.label42 = new System.Windows.Forms.Label();
            this.nUDRightValue = new System.Windows.Forms.NumericUpDown();
            this.nUDLeftValue = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.cbbCurveColor = new System.Windows.Forms.ComboBox();
            this.tbgStatics = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tbgOperation = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.tbgData = new System.Windows.Forms.TabPage();
            this.dgvDataTable = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tbgInfor.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDLineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDRightValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDLeftValue)).BeginInit();
            this.tbgStatics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tbgOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tbgData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbgData);
            this.tabControl1.Controls.Add(this.tbgInfor);
            this.tabControl1.Controls.Add(this.tbgStatics);
            this.tabControl1.Controls.Add(this.tbgOperation);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(546, 512);
            this.tabControl1.TabIndex = 3;
            // 
            // tbgInfor
            // 
            this.tbgInfor.Controls.Add(this.btnDefaultSetting);
            this.tbgInfor.Controls.Add(this.btnSaveSetting);
            this.tbgInfor.Controls.Add(this.groupBox1);
            this.tbgInfor.Location = new System.Drawing.Point(4, 22);
            this.tbgInfor.Name = "tbgInfor";
            this.tbgInfor.Padding = new System.Windows.Forms.Padding(3);
            this.tbgInfor.Size = new System.Drawing.Size(538, 486);
            this.tbgInfor.TabIndex = 0;
            this.tbgInfor.Text = "信息";
            this.tbgInfor.UseVisualStyleBackColor = true;
            // 
            // btnDefaultSetting
            // 
            this.btnDefaultSetting.Location = new System.Drawing.Point(210, 222);
            this.btnDefaultSetting.Name = "btnDefaultSetting";
            this.btnDefaultSetting.Size = new System.Drawing.Size(75, 23);
            this.btnDefaultSetting.TabIndex = 3;
            this.btnDefaultSetting.Text = "缺省";
            this.btnDefaultSetting.UseVisualStyleBackColor = true;
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.Location = new System.Drawing.Point(66, 222);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(87, 23);
            this.btnSaveSetting.TabIndex = 3;
            this.btnSaveSetting.Text = "保存";
            this.btnSaveSetting.UseVisualStyleBackColor = true;
            this.btnSaveSetting.Click += new System.EventHandler(this.btnSaveSetting_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxUnit);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbbDashStyle);
            this.groupBox1.Controls.Add(this.tbxLogname);
            this.groupBox1.Controls.Add(this.label64);
            this.groupBox1.Controls.Add(this.tbxJH);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxIsLogGrid);
            this.groupBox1.Controls.Add(this.nUDLineWidth);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.nUDRightValue);
            this.groupBox1.Controls.Add(this.nUDLeftValue);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.cbbCurveColor);
            this.groupBox1.Location = new System.Drawing.Point(27, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 176);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // tbxUnit
            // 
            this.tbxUnit.Location = new System.Drawing.Point(317, 30);
            this.tbxUnit.Name = "tbxUnit";
            this.tbxUnit.Size = new System.Drawing.Size(82, 21);
            this.tbxUnit.TabIndex = 82;
            this.tbxUnit.TextChanged += new System.EventHandler(this.tbxUnit_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(282, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 81;
            this.label7.Text = "单位";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 80;
            this.label1.Text = "线型";
            // 
            // cbbDashStyle
            // 
            this.cbbDashStyle.BackColor = System.Drawing.SystemColors.Window;
            this.cbbDashStyle.FormattingEnabled = true;
            this.cbbDashStyle.Location = new System.Drawing.Point(174, 115);
            this.cbbDashStyle.Name = "cbbDashStyle";
            this.cbbDashStyle.Size = new System.Drawing.Size(68, 20);
            this.cbbDashStyle.TabIndex = 79;
            // 
            // tbxLogname
            // 
            this.tbxLogname.Location = new System.Drawing.Point(183, 30);
            this.tbxLogname.Name = "tbxLogname";
            this.tbxLogname.Size = new System.Drawing.Size(70, 21);
            this.tbxLogname.TabIndex = 78;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(136, 34);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(41, 12);
            this.label64.TabIndex = 77;
            this.label64.Text = "曲线名";
            // 
            // tbxJH
            // 
            this.tbxJH.Location = new System.Drawing.Point(56, 32);
            this.tbxJH.Name = "tbxJH";
            this.tbxJH.Size = new System.Drawing.Size(70, 21);
            this.tbxJH.TabIndex = 71;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 70;
            this.label2.Text = "井名";
            // 
            // cbxIsLogGrid
            // 
            this.cbxIsLogGrid.AutoSize = true;
            this.cbxIsLogGrid.Location = new System.Drawing.Point(291, 75);
            this.cbxIsLogGrid.Name = "cbxIsLogGrid";
            this.cbxIsLogGrid.Size = new System.Drawing.Size(72, 16);
            this.cbxIsLogGrid.TabIndex = 67;
            this.cbxIsLogGrid.Text = "对数刻度";
            this.cbxIsLogGrid.UseVisualStyleBackColor = true;
            // 
            // nUDLineWidth
            // 
            this.nUDLineWidth.AllowDrop = true;
            this.nUDLineWidth.DecimalPlaces = 1;
            this.nUDLineWidth.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nUDLineWidth.Location = new System.Drawing.Point(74, 113);
            this.nUDLineWidth.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUDLineWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nUDLineWidth.Name = "nUDLineWidth";
            this.nUDLineWidth.Size = new System.Drawing.Size(40, 21);
            this.nUDLineWidth.TabIndex = 66;
            this.nUDLineWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDLineWidth.ValueChanged += new System.EventHandler(this.nUDLineWidth_ValueChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(16, 115);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(53, 12);
            this.label42.TabIndex = 65;
            this.label42.Text = "曲线宽度";
            // 
            // nUDRightValue
            // 
            this.nUDRightValue.AllowDrop = true;
            this.nUDRightValue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUDRightValue.Location = new System.Drawing.Point(234, 74);
            this.nUDRightValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nUDRightValue.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.nUDRightValue.Name = "nUDRightValue";
            this.nUDRightValue.Size = new System.Drawing.Size(40, 21);
            this.nUDRightValue.TabIndex = 62;
            this.nUDRightValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nUDRightValue.ValueChanged += new System.EventHandler(this.nUDRightValue_ValueChanged);
            // 
            // nUDLeftValue
            // 
            this.nUDLeftValue.AllowDrop = true;
            this.nUDLeftValue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUDLeftValue.Location = new System.Drawing.Point(158, 74);
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
            this.nUDLeftValue.ValueChanged += new System.EventHandler(this.nUDLeftValue_ValueChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(204, 78);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(29, 12);
            this.label33.TabIndex = 59;
            this.label33.Text = "右值";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(128, 78);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(29, 12);
            this.label34.TabIndex = 60;
            this.label34.Text = "左值";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(16, 78);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(29, 12);
            this.label37.TabIndex = 57;
            this.label37.Text = "颜色";
            // 
            // cbbCurveColor
            // 
            this.cbbCurveColor.BackColor = System.Drawing.Color.Blue;
            this.cbbCurveColor.FormattingEnabled = true;
            this.cbbCurveColor.Location = new System.Drawing.Point(51, 74);
            this.cbbCurveColor.Name = "cbbCurveColor";
            this.cbbCurveColor.Size = new System.Drawing.Size(61, 20);
            this.cbbCurveColor.TabIndex = 56;
            this.cbbCurveColor.Click += new System.EventHandler(this.cbbCurveColor_Click);
            // 
            // tbgStatics
            // 
            this.tbgStatics.Controls.Add(this.textBox1);
            this.tbgStatics.Controls.Add(this.label3);
            this.tbgStatics.Controls.Add(this.textBox2);
            this.tbgStatics.Controls.Add(this.label4);
            this.tbgStatics.Controls.Add(this.textBox3);
            this.tbgStatics.Controls.Add(this.label5);
            this.tbgStatics.Controls.Add(this.textBox4);
            this.tbgStatics.Controls.Add(this.label6);
            this.tbgStatics.Controls.Add(this.chart1);
            this.tbgStatics.Location = new System.Drawing.Point(4, 22);
            this.tbgStatics.Name = "tbgStatics";
            this.tbgStatics.Padding = new System.Windows.Forms.Padding(3);
            this.tbgStatics.Size = new System.Drawing.Size(538, 486);
            this.tbgStatics.TabIndex = 1;
            this.tbgStatics.Text = "统计";
            this.tbgStatics.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(234, 66);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(70, 21);
            this.textBox1.TabIndex = 88;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 87;
            this.label3.Text = "最小值";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(93, 66);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(70, 21);
            this.textBox2.TabIndex = 86;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 85;
            this.label4.Text = "最大值";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(244, 26);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(70, 21);
            this.textBox3.TabIndex = 82;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(185, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 81;
            this.label5.Text = "曲线底深";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(93, 26);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(70, 21);
            this.textBox4.TabIndex = 80;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 79;
            this.label6.Text = "曲线顶深";
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(93, 103);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(352, 217);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tbgOperation
            // 
            this.tbgOperation.Controls.Add(this.button3);
            this.tbgOperation.Controls.Add(this.button2);
            this.tbgOperation.Controls.Add(this.numericUpDown1);
            this.tbgOperation.Location = new System.Drawing.Point(4, 22);
            this.tbgOperation.Name = "tbgOperation";
            this.tbgOperation.Size = new System.Drawing.Size(538, 486);
            this.tbgOperation.TabIndex = 2;
            this.tbgOperation.Text = "操作";
            this.tbgOperation.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(37, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 65;
            this.button3.Text = "去峰值";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(92, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 64;
            this.button2.Text = "抽稀重采样";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.AllowDrop = true;
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(37, 30);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(40, 21);
            this.numericUpDown1.TabIndex = 63;
            // 
            // tbgData
            // 
            this.tbgData.Controls.Add(this.dgvDataTable);
            this.tbgData.Location = new System.Drawing.Point(4, 22);
            this.tbgData.Name = "tbgData";
            this.tbgData.Padding = new System.Windows.Forms.Padding(3);
            this.tbgData.Size = new System.Drawing.Size(538, 486);
            this.tbgData.TabIndex = 3;
            this.tbgData.Text = "数据";
            this.tbgData.UseVisualStyleBackColor = true;
            // 
            // dgvDataTable
            // 
            this.dgvDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDataTable.Location = new System.Drawing.Point(3, 3);
            this.dgvDataTable.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDataTable.Name = "dgvDataTable";
            this.dgvDataTable.RowTemplate.Height = 27;
            this.dgvDataTable.Size = new System.Drawing.Size(532, 480);
            this.dgvDataTable.TabIndex = 3;
            // 
            // FormLogSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 512);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormLogSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "曲线设置";
            this.tabControl1.ResumeLayout(false);
            this.tbgInfor.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDLineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDRightValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDLeftValue)).EndInit();
            this.tbgStatics.ResumeLayout(false);
            this.tbgStatics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tbgOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tbgData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbgStatics;
        private System.Windows.Forms.TabPage tbgInfor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbDashStyle;
        private System.Windows.Forms.TextBox tbxLogname;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox tbxJH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbxIsLogGrid;
        private System.Windows.Forms.NumericUpDown nUDLineWidth;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.NumericUpDown nUDRightValue;
        private System.Windows.Forms.NumericUpDown nUDLeftValue;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox cbbCurveColor;
        private System.Windows.Forms.TabPage tbgOperation;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDefaultSetting;
        private System.Windows.Forms.TextBox tbxUnit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tbgData;
        private System.Windows.Forms.DataGridView dgvDataTable;

    }
}