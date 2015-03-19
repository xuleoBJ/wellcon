namespace DOGPlatform
{
    partial class FormWebNavigation
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.htmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHtml = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystemChoose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditInk = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControlSVGNavigation = new System.Windows.Forms.TabControl();
            this.tbgBaseLayerSVGView = new System.Windows.Forms.TabPage();
            this.webBrowserSVG = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.ToolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.ToolStripContainer1.ContentPanel.SuspendLayout();
            this.ToolStripContainer1.SuspendLayout();
            this.tabControlSVGNavigation.SuspendLayout();
            this.tbgBaseLayerSVGView.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(894, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem1,
            this.htmlToolStripMenuItem});
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.打开文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开文件ToolStripMenuItem1
            // 
            this.打开文件ToolStripMenuItem1.Name = "打开文件ToolStripMenuItem1";
            this.打开文件ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.打开文件ToolStripMenuItem1.Text = "打开文件";
            this.打开文件ToolStripMenuItem1.Click += new System.EventHandler(this.openSVGfile_Click);
            // 
            // htmlToolStripMenuItem
            // 
            this.htmlToolStripMenuItem.Name = "htmlToolStripMenuItem";
            this.htmlToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.htmlToolStripMenuItem.Text = "html";
            this.htmlToolStripMenuItem.Click += new System.EventHandler(this.htmlToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHtml});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "操作";
            // 
            // tsmiHtml
            // 
            this.tsmiHtml.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditInk,
            this.tsmiSystemChoose});
            this.tsmiHtml.Name = "tsmiHtml";
            this.tsmiHtml.Size = new System.Drawing.Size(152, 22);
            this.tsmiHtml.Text = "编辑";
            this.tsmiHtml.Click += new System.EventHandler(this.tsmiHtml_Click);
            // 
            // tsmiSystemChoose
            // 
            this.tsmiSystemChoose.Name = "tsmiSystemChoose";
            this.tsmiSystemChoose.Size = new System.Drawing.Size(152, 22);
            this.tsmiSystemChoose.Text = "系统选择";
            this.tsmiSystemChoose.Click += new System.EventHandler(this.tsmiSystemChoose_Click);
            // 
            // tsmiEditInk
            // 
            this.tsmiEditInk.Name = "tsmiEditInk";
            this.tsmiEditInk.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditInk.Text = "编辑";
            this.tsmiEditInk.Click += new System.EventHandler(this.tsmiEditInk_Click);
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(0, 22);
            // 
            // ToolStripContainer1
            // 
            // 
            // ToolStripContainer1.BottomToolStripPanel
            // 
            this.ToolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // ToolStripContainer1.ContentPanel
            // 
            this.ToolStripContainer1.ContentPanel.Controls.Add(this.tabControlSVGNavigation);
            this.ToolStripContainer1.ContentPanel.Size = new System.Drawing.Size(894, 598);
            this.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStripContainer1.Location = new System.Drawing.Point(0, 25);
            this.ToolStripContainer1.Name = "ToolStripContainer1";
            this.ToolStripContainer1.Size = new System.Drawing.Size(894, 645);
            this.ToolStripContainer1.TabIndex = 4;
            this.ToolStripContainer1.Text = "ToolStripContainer1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(894, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "当前对象:";
            // 
            // tabControlSVGNavigation
            // 
            this.tabControlSVGNavigation.Controls.Add(this.tbgBaseLayerSVGView);
            this.tabControlSVGNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSVGNavigation.Location = new System.Drawing.Point(0, 0);
            this.tabControlSVGNavigation.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlSVGNavigation.Name = "tabControlSVGNavigation";
            this.tabControlSVGNavigation.SelectedIndex = 0;
            this.tabControlSVGNavigation.Size = new System.Drawing.Size(894, 598);
            this.tabControlSVGNavigation.TabIndex = 1;
            this.tabControlSVGNavigation.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControlSVGNavigation_MouseClick);
            // 
            // tbgBaseLayerSVGView
            // 
            this.tbgBaseLayerSVGView.Controls.Add(this.webBrowserSVG);
            this.tbgBaseLayerSVGView.Location = new System.Drawing.Point(4, 22);
            this.tbgBaseLayerSVGView.Margin = new System.Windows.Forms.Padding(2);
            this.tbgBaseLayerSVGView.Name = "tbgBaseLayerSVGView";
            this.tbgBaseLayerSVGView.Padding = new System.Windows.Forms.Padding(2);
            this.tbgBaseLayerSVGView.Size = new System.Drawing.Size(886, 572);
            this.tbgBaseLayerSVGView.TabIndex = 1;
            this.tbgBaseLayerSVGView.Text = "View1";
            this.tbgBaseLayerSVGView.UseVisualStyleBackColor = true;
            // 
            // webBrowserSVG
            // 
            this.webBrowserSVG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserSVG.Location = new System.Drawing.Point(2, 2);
            this.webBrowserSVG.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowserSVG.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowserSVG.Name = "webBrowserSVG";
            this.webBrowserSVG.ScriptErrorsSuppressed = true;
            this.webBrowserSVG.Size = new System.Drawing.Size(882, 568);
            this.webBrowserSVG.TabIndex = 1;
            // 
            // FormWebNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 670);
            this.Controls.Add(this.ToolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormWebNavigation";
            this.Text = "图形窗口";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ToolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.ToolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.ToolStripContainer1.ContentPanel.ResumeLayout(false);
            this.ToolStripContainer1.ResumeLayout(false);
            this.ToolStripContainer1.PerformLayout();
            this.tabControlSVGNavigation.ResumeLayout(false);
            this.tbgBaseLayerSVGView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        private System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        private System.Windows.Forms.ToolStripContainer ToolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControlSVGNavigation;
        private System.Windows.Forms.TabPage tbgBaseLayerSVGView;
        private System.Windows.Forms.WebBrowser webBrowserSVG;
        private System.Windows.Forms.ToolStripMenuItem tsmiHtml;
        private System.Windows.Forms.ToolStripMenuItem htmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditInk;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemChoose;
    }
}