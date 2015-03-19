using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DOGPlatform
{  ///
   /// 加新导入的测井曲线格式需要以下几个部分的修改
   /// 1. enum 类型加
   /// 2. 下拉选择列表加
   /// 3. 选择文件头inputLog类加
    ///4  寻找到数据行，调用读取数据数据的方法，注意输入数据其实行
    public partial class FormDataImportLog : Form
    {
        public FormDataImportLog(string _sJH)
        {
            InitializeComponent();
            initializeForm(_sJH);
        }
        enum FormatLogFile
        {
            forward1,
            ascii,
            list,
            las,
            las2,
        }

        string sJH="";
        string filePathSourceLogFile = "";
        FormatLogFile currentFormat = FormatLogFile.forward1;

        void initializeForm(string _sJH)
        {
            this.Text = _sJH;
            this.sJH=_sJH;
            List<string> logFormatText = new List<string>();
            logFormatText.Add(FormatLogFile.forward1.ToString());
            logFormatText.Add(FormatLogFile.ascii.ToString());
            logFormatText.Add(FormatLogFile.list.ToString());
            logFormatText.Add(FormatLogFile.las.ToString());
            logFormatText.Add(FormatLogFile.las2.ToString());
            cbbLogFormat.DataSource = logFormatText;
            this.cbbLogFormat.SelectedIndex = 0;
        }
        //必须跟下拉菜单的顺序一样
        private void cbbLogFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentFormat = (FormatLogFile)cbbLogFormat.SelectedIndex;
        }

        private void btnOpenEX_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd_logFilePath = new OpenFileDialog();

            ofd_logFilePath.Title = sJH;
            ofd_logFilePath.Filter = "所有文件|*.*|las文件|*.las|las2文件|*.las|txt文件|*.txt|list文件|*.list";
            //设置默认文件类型显示顺序 
            ofd_logFilePath.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            ofd_logFilePath.RestoreDirectory = true;
            if (ofd_logFilePath.ShowDialog() == DialogResult.OK)
            {
                filePathSourceLogFile=ofd_logFilePath.FileName;
                this.tbxUserFilePath.Text = filePathSourceLogFile;
                cPublicMethodForm.textboxViewText(this.tbxView, filePathSourceLogFile, 50);
            }
            
        }

        List<string> getListLogHeadByLogFormat(string filepath) 
        {
            List<string> listLogHeadColumn = new List<string>();
            if (currentFormat==FormatLogFile.forward1) listLogHeadColumn=cIOinputLog.getLogSerierNamesFromLogForward(filepath);
            if (currentFormat==FormatLogFile.ascii) listLogHeadColumn = cIOinputLog.getLogSerierNamesFromTXTLog(filepath);
            if (currentFormat == FormatLogFile.list) listLogHeadColumn = cIOinputLog.getLogSerierNamesFromListLog(filepath);
            if (currentFormat == FormatLogFile.las) listLogHeadColumn = cIOinputLog.getLogSerierNamesFromLasLog(filepath);
            if (currentFormat == FormatLogFile.las2) listLogHeadColumn = cIOinputLog.getLogSerierNamesFromLasV2Log(filepath);
            return listLogHeadColumn; 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //depth can't delete need deal
            cPublicMethodForm.deleteSelectedRowInDataGridView(dgvLog);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            importLog(); //导入时更新了全局测井曲线，然后主界面刷新
            this.Close();
        }
        void importLog() 
        {
            if (currentFormat == FormatLogFile.forward1) importTextLogText(7); 
            if (currentFormat == FormatLogFile.ascii) importTextLogText(2);
            if (currentFormat == FormatLogFile.list) importTextLogText(4); 
            if (currentFormat == FormatLogFile.las) importTextLogText(cIOinputLog.getDataStartLineOfLasLog(filePathSourceLogFile));
            if (currentFormat == FormatLogFile.las2) importTextLogText(cIOinputLog.getDataStartLineOfLasLog(filePathSourceLogFile)); 
        }
        void importTextLogText(int _iDataStartLine)
        {
            List<string> ltStrLogHead = new List<string>();
            List<int> ltIndexLog = new List<int>();

            for (int i = 0; i < dgvLog.Rows.Count - 1; i++)
            {
                ltStrLogHead.Add(dgvLog.Rows[i].Cells["logNameNew"].Value.ToString());
                ltIndexLog.Add(Convert.ToInt16(dgvLog.Rows[i].Cells["logNum"].Value) - 1); //指数比列多1
            }
            for (int i = 0; i < ltIndexLog.Count; i++)
            {
                string _logName = ltStrLogHead[i];
                int _indexLog = ltIndexLog[i];
                string _logFilePath = Path.Combine(cProjectManager.dirPathWellDir, sJH, _logName + cProjectManager.fileExtensionWellLog);
                List<string> _ltLogFileHead = new List<string>();
                _ltLogFileHead.Add("Depth");
                _ltLogFileHead.Add(_logName);
                string _firstLine = sJH + " " + _logName + " source:" + this.filePathSourceLogFile 
                    + " columnNum:" + (_indexLog+1).ToString() + " " + DateTime.Now.ToString(); 
                cIOGeoEarthText.creatFileGeoHeadText(_logFilePath, _firstLine, _ltLogFileHead);
                cIOGeoEarthText.addDataLines2GeoEarTxt(_logFilePath, cIOinputLog.readLogData(filePathSourceLogFile, _iDataStartLine, _indexLog));
            }

            //全局测井头更新
            foreach (string _s in ltStrLogHead)
                if (cProjectData.ltStrLogSeriers.IndexOf(_s) < 0) cProjectData.ltStrLogSeriers.Add(_s);
            
            //保留原输入测井数据,刚才导入时loghead删除了depth，保留文件时加上
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameInputWellLog);
            ltStrLogHead.Insert(0, "DEPTH");
            cIOGeoEarthText.creatFileGeoHeadText(filePath, sJH, ltStrLogHead);
            ltIndexLog.Insert(0, 0);
            cIOGeoEarthText.addDataLines2GeoEarTxt(filePath, cIOinputLog.readLogData(filePathSourceLogFile, _iDataStartLine, ltIndexLog));
            MessageBox.Show("导入完成。");
        }


        private void btnShowLogHead_Click(object sender, EventArgs e)
        {
            List<string> ltStrHead = getListLogHeadByLogFormat(filePathSourceLogFile);
            dgvLog.Rows.Clear();
            //column=index+1 and colunm 0 is depth
            for (int i = 0; i < ltStrHead.Count; i++)
            {
                this.dgvLog.Rows.Add(ltStrHead[i], (i + 2).ToString(), ltStrHead[i].ToUpper());
            }
        }

       


    }
}
