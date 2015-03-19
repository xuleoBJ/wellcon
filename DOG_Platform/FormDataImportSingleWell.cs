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
{
    public partial class FormDataImportSingleWell : Form
    {
        public FormDataImportSingleWell(string _sJH, TypeInputFile _fileType)
        {
            InitializeComponent();
            initializaForm(_sJH, _fileType);
        }
        string sJH = "";
        string filePathGeoEarthText = "";
        TypeInputFile fileType;

        void initializaForm(string _sJH, TypeInputFile _fileType)
        {
            fileType = _fileType;
            this.Text = _sJH+"导入"+_fileType.ToString();

            if(  _sJH!="")
            {
                this.sJH = _sJH;

                if (_fileType == TypeInputFile.井轨迹) 
                {
                    this.filePathGeoEarthText =
                        Path.Combine(cProjectManager.dirPathWellDir,_sJH,cProjectManager.fileNameInputWellPath);
                    if (!File.Exists(filePathGeoEarthText)) cIOinputWellPath.creatUserInputFileHead(_sJH); 
                }
                if (_fileType == TypeInputFile.吸水剖面)
                {
                    this.filePathGeoEarthText =
                       Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellProfile);
              //      if (!File.Exists(filePathGeoEarthText)) cIOinputInjectionProfile.creatUserInputFileHead(_sJH);
                }
               
            }
           
            cPublicMethodForm.loadDataGridViewWithGeoText(dgvDataTable, filePathGeoEarthText);
        }


        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.read2DataGridViewByTextFile(dgvDataTable);
        }


        private void tsmiDeleteCurrentLine_Click(object sender, EventArgs e)
        {
            int index = dgvDataTable.CurrentCell.RowIndex;
            dgvDataTable.Rows.RemoveAt(index); 
        }

        private void tsmiDataImport_Click(object sender, EventArgs e)
        {
            if (getListStringDataGridviewColoum(this.dgvDataTable, 0).Distinct().ToList().Count > 1)
            {
                MessageBox.Show("只能导入单井数据.");
            }
            else dataImported();  
            
        }

         List<string> getListStringDataGridviewColoum(DataGridView dgv,int iColumnIndex)
        {
            List<string> ltStrColumn=new List<string>();
             for (int j = 0; j < dgv.RowCount - 1; j++)
                {

                     if(dgvDataTable.Rows[j].Cells[iColumnIndex].Value != null) ltStrColumn.Add(dgv.Rows[j].Cells[iColumnIndex].Value.ToString()) ;
                     else  //如果是空值，填充上一个值
                     {
                         int _count=ltStrColumn.Count;
                         if(_count>=1) ltStrColumn.Add(ltStrColumn[_count-1]);
                         else ltStrColumn.Add("0");
                     }
             }
             return ltStrColumn;
        
        }

        void dataImported() 
        {
            bool isImpored = true;
            //只能导入单井数据
            if (fileType == TypeInputFile.井轨迹)
            {
                List<string> listLine = cPublicMethodForm.readDataGridView2ListLine(this.dgvDataTable);
                //写入用户输入文件
                string inputFilePath =
                      Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameInputWellPath);
          //      cPublicMethodForm.readDataGridView2TXTFile(this.dgvDataTable, inputFilePath);
                cPublicMethodForm.readDataGridView2geoTXTFileWithColoumHead(this.dgvDataTable, inputFilePath, "inputWellPath");
                //写入geoFile
                cIOinputWellPath.updateWellgeoFile(sJH); 
                cProjectData.setProjectWellsInfor();
            }
            else if (fileType == TypeInputFile.解释结论)
            {
                MessageBox.Show("导入解释结论需要编写");
                ////写入用户输入文件
                //cIOGeoEarthText.replaceDataLines2GeoEarTxtByFirstWord(this.filePathGeoEarthText, cPublicMethodForm.readDataGridView2ListLine(this.dgvDataTable), sJH);
                ////写入项目井文件夹
                //ItemWellHead wellHead = new ItemWellHead(sJH);

                //List<float> fListMD = new List<float>();
                //List<float> fListInc = new List<float>();
                //List<float> fListAzimuth = new List<float>();

                //fListMD = getListStringDataGridviewColoum(dgvDataTable, 1).ConvertAll(item => float.Parse(item));
                //fListInc = getListStringDataGridviewColoum(dgvDataTable, 2).ConvertAll(item => float.Parse(item));
                //fListAzimuth = getListStringDataGridviewColoum(dgvDataTable, 3).ConvertAll(item => float.Parse(item));


                //List<ItemWellPath> itemsWellPath = cIOinputWellPath.phzqf2Struct(wellHead.sJH, wellHead.dbX, wellHead.dbY, wellHead.fKB, fListMD, fListInc, fListAzimuth);

                //cIOinputWellPath.creatWellGeoFile(wellHead, itemsWellPath);

            }
            else
            {
                isImpored= false;
            }

            if (isImpored == true) MessageBox.Show("数据导入成功");
            else MessageBox.Show("数据未导入。");
        }

        private void tsmiCopyFromExcel_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.DataGridViewCellPaste(dgvDataTable);
        }

        private void FormDataImportSingleWell_Load(object sender, EventArgs e)
        {

        }

      

     
    }
}
