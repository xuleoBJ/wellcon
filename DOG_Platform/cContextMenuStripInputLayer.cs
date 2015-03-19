using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cContextMenuStripInputLayer : cContextMenuStripBaseLayer
    {
        public cContextMenuStripInputLayer(ContextMenuStrip _cms, TreeNode _tnSelected, string _sXCM)
            : base(_cms, _tnSelected, _sXCM)
        {
            tsmiImportLayers.Text = "导入分层数据";
            tsmiImportLayers.Click += new System.EventHandler(tsmiImportLayers_Click);
            tsmiImportFaultLine.Text = "断层线导入";
            tsmiImportFaultLine.Click += new System.EventHandler(tsmiImportFaultLine_Click);
        }
        ToolStripMenuItem tsmiImportFaultLine = new ToolStripMenuItem();
        ToolStripMenuItem tsmiImportLayers = new ToolStripMenuItem();
        private void tsmiImportLayers_Click(object sender, EventArgs e)
        {
            FormImportProjectData frmImportLayers = new FormImportProjectData();
            frmImportLayers.ShowDialog();
        }
        public void setupTsmi()
        {
            cms.Items.Add(tsmiImportLayers);
            cms.Items.Add(tsmiImportFaultLine);
        }
        private void tsmiImportFaultLine_Click(object sender, EventArgs e)
        {
           
            string _fileNameOperated = "#faults#.txt";
            string _filePath =Path.Combine(cProjectManager.dirPathLayerDir,this.sXCM,_fileNameOperated);
            List<string> _ltStrHead=new List<string>();
            _ltStrHead.Add("断层号");
            _ltStrHead.Add("上盘/下盘");
            _ltStrHead.Add("X");
            _ltStrHead.Add("Y");
            if (!File.Exists(_filePath))
            {
                cIOGeoEarthText.creatFileGeoHeadText(_filePath,_fileNameOperated,_ltStrHead);
            }
            FormDataImportLayer _form = new FormDataImportLayer(this.sXCM,TypeInputFile.分层数据);
            _form.ShowDialog();
        }
        public void setupTsmiImportContour()
        {
            ToolStripMenuItem tsmiImportContour = new ToolStripMenuItem();
            tsmiImportContour.Text = "等值线导入";
            tsmiImportContour.Click += new System.EventHandler(tsmiImportContour_Click);
            cms.Items.Add(tsmiImportContour);
        }

        private void tsmiImportContour_Click(object sender, EventArgs e)
        {
            string _fileNameOperated = "#contour#.txt";
            string _filePath = Path.Combine(cProjectManager.dirPathLayerDir, this.sXCM, _fileNameOperated);
            List<string> _ltStrHead = new List<string>();
            _ltStrHead.Add("X");
            _ltStrHead.Add("Y");
            _ltStrHead.Add("值");
            if (!File.Exists(_filePath))
            {
                cIOGeoEarthText.creatFileGeoHeadText(_filePath, _fileNameOperated, _ltStrHead);
            }
            //FormImportDataTable _form = new FormImportDataTable(_filePath);
            //_form.ShowDialog();
        }
    }
}
