using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DOGPlatform
{
    class cContextMenuStripInputWellsManager : cContextMenuStripBaseWell
    {
        public cContextMenuStripInputWellsManager(ContextMenuStrip _cms, TreeNode _tnSelected)
            : base(_cms)
        {
             this.tnSelected = _tnSelected;
        }
 
        public void setupContextMenuWellMangager()
        {
            setupTsmiImportProjectData();
            setupTsmiExportData();
            ToolStripMenuItem tsmiSelectAllWells = new ToolStripMenuItem();
            tsmiSelectAllWells.Text = "全选";
            tsmiSelectAllWells.Click += new System.EventHandler(tsmiSelectAllWells_Click);
            cms.Items.Add(tsmiSelectAllWells);
            ToolStripMenuItem tsmiUnSelectAllWells = new ToolStripMenuItem();
            tsmiUnSelectAllWells.Text = "全不选";
            tsmiUnSelectAllWells.Click += new System.EventHandler(tsmiUnSelectAllWells_Click);
            cms.Items.Add(tsmiUnSelectAllWells);
            ToolStripMenuItem tsmiColapseAll = new ToolStripMenuItem();
            tsmiColapseAll.Text = "收起";
            tsmiColapseAll.Click += new System.EventHandler(tsmiColapseAll_Click);
            cms.Items.Add(tsmiColapseAll);
            ToolStripMenuItem tsmiExpandAll = new ToolStripMenuItem();
            tsmiExpandAll.Text = "展开";
            cms.Items.Add(tsmiExpandAll);
            tsmiExpandAll.Click += new System.EventHandler(tsmiExpandAll_Click);
        }
        private void tsmiManager_Click(object sender, EventArgs e)
        {
            FormWellManager formDataInput = new FormWellManager();
            formDataInput.ShowDialog();
        }
        private void tsmiColapseAll_Click(object sender, EventArgs e)
        {
            this.tnSelected.Collapse();
        }
        private void tsmiExpandAll_Click(object sender, EventArgs e)
        {
            this.tnSelected.ExpandAll();
        }
        private void tsmiSelectAllWells_Click(object sender, EventArgs e)
        {
            foreach (TreeNode tn in tnSelected.Nodes)
            {
                tn.Checked = true;
            }
        }
        private void tsmiUnSelectAllWells_Click(object sender, EventArgs e)
        {
            foreach (TreeNode tn in tnSelected.Nodes)
            {
                tn.Checked = false;
            }
        }
        public void setupTsmiImportProjectData()
        {
            ToolStripMenuItem tsmiIMportProject = new ToolStripMenuItem();
            tsmiIMportProject.Text = "项目井数据管理";
            tsmiIMportProject.Click += new System.EventHandler(tsmiImportProject_Click);
            cms.Items.Add(tsmiIMportProject);
        }
        private void tsmiImportProject_Click(object sender, EventArgs e)
        {
            FormImportProjectData frmImportProject = new FormImportProjectData();
            frmImportProject.ShowDialog();
            cProjectData.setProjectWellsInfor();
        }

        public void setupTsmiExportData()
        {
            ToolStripMenuItem tsmiExportAllWells = new ToolStripMenuItem();
            tsmiExportAllWells.Text = "导出数据";
            //ToolStripMenuItem tsmiExportBatchWellLog = new ToolStripMenuItem();
            //tsmiExportBatchWellLog.Text = "导出多井曲线";
            //tsmiExportBatchWellLog.Click += new System.EventHandler(tsmiExportBatchWellLog_Click);
            //cms.Items.Add(tsmiExportBatchWellLog);
            //tsmiExportAllWells.DropDownItems.Add(tsmiExportBatchWellLog);
            cms.Items.Add(tsmiExportAllWells);
        }
        private void tsmiExportLog_Click(object sender, EventArgs e)
        {
            List<string> ltStrSelectJH = new List<string>();
            foreach (TreeNode tn in tnSelected.Nodes)
            {
                if (tn.Checked == true)
                {
                    ltStrSelectJH.Add(tn.Text);
                }
            }
            FormExportLog formExportlog = new FormExportLog(ltStrSelectJH);
            formExportlog.ShowDialog();
        }

        public void setupTsmiExportManyWellsLog()
        {
            ToolStripMenuItem tsmiExportBatchWellLog = new ToolStripMenuItem();
            tsmiExportBatchWellLog.Text = "导出多井曲线";
            tsmiExportBatchWellLog.Click += new System.EventHandler(tsmiExportBatchWellLog_Click);
            cms.Items.Add(tsmiExportBatchWellLog);
        }
        private void tsmiExportBatchWellLog_Click(object sender, EventArgs e)
        {DialogResult dialogResult = MessageBox.Show("请勾选所有需要导出的井号，勾选全局测井中需要导出的曲线 ", "批量导出曲线", MessageBoxButtons.YesNo);
        if (dialogResult == DialogResult.Yes)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowDialog();
            FormMain.ltTV_SelectedLogNames.Insert(0, "DEPTH");
            foreach (string _sJH in FormMain.ltTV_SelectedJH)
            {
                string _saveLogFilePath = Path.Combine(folderDlg.SelectedPath, _sJH + ".txt");
                cIOinputLog.selectLogSeriresFromProjectWellLog(_sJH, FormMain.ltTV_SelectedLogNames, _saveLogFilePath);

                MessageBox.Show(_sJH + "导出完成。");

            }
        }

        }
    }
}
