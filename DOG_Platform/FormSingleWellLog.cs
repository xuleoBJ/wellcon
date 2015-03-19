using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using DOGPlatform.XML;
using DOGPlatform.SVG;

namespace DOGPlatform
{
    public partial class FormSingleWellLog : Form
    {
        public FormSingleWellLog()
        {
            InitializeComponent();
            initializeMyControl();
        }

        void initializeMyControl() 
        {
            cPublicMethodForm.inialComboBox(cbbJH, cProjectData.ltStrProjectJH);
            cPublicMethodForm.inialComboBox(cbbLogName, cProjectData.ltStrLogSeriers);
           
            List<string> ltStrTrackType = Enum.GetNames(typeof(TypeTrack)).ToList();
            cPublicMethodForm.inialComboBox(cbbTrack, ltStrTrackType);

        }

        string sJHSelected;  //井号
        int iDS1Showed = 100;   //绘制的顶深
        int iDS2Showed = 1000;  //绘制的底深
        float dfscale = 2;
        int iIndexTrack = 0; //track0是深度道
        List<int> iListTrackWidth = new List<int>();
        string fileDrawSourceInfor = cProjectManager.dirPathTemp + "SW_singleWell.txt";
        string filepathStyleXML = cProjectManager.dirPathTemp + "style_singleWell.xml";
        string filepathDataXML = cProjectManager.dirPathTemp + "data_singleWell.xml";

        void initializaSourceMapFile()
        {
            sJHSelected = cbbJH.SelectedItem.ToString();
            iDS1Showed = int.Parse(tbxTopInput.Text);
            iDS2Showed = int.Parse(tbxBottomInput.Text);
            dfscale=1000/Convert.ToSingle( nUDVScale.Value);
            
            iListTrackWidth.Clear();
            iIndexTrack = 0;
            cXMLSingleWell.generateDataXMLmodel(filepathDataXML,sJHSelected);
            cXMLSingleWell.generateStyleXMLModel(filepathStyleXML, iDS1Showed, iDS2Showed, dfscale); 
            StreamWriter sw = new StreamWriter(fileDrawSourceInfor, false, Encoding.UTF8);
            sw.Close();
        }

        private void btnGenerateDataByInputDepth_Click(object sender, EventArgs e)
        {
            initializaSourceMapFile();
            treeviewTrackCollection.Nodes.Clear();
            TreeNode tnode = new TreeNode();
            tnode.Text = TypeTrack.深度道.ToString();
            tnode.Name = "track" + iIndexTrack.ToString();
            tnode.Tag = TypeTrack.深度道;
            //默认加个深度道
            cXMLSingleWell.addTrackStyleXMLModel(filepathStyleXML, tnode.Text, tnode.Name, tnode.Tag);
            
            treeviewTrackCollection.Nodes.Add(tnode);
        }

        private void tbxTopInput_TextChanged(object sender, EventArgs e)
        {
            cPublicMethodForm.inputTextBoxIntOnly(tbxTopInput);
        }

        private void tbxBottomInput_TextChanged(object sender, EventArgs e)
        {
            cPublicMethodForm.inputTextBoxIntOnly(tbxBottomInput);
        }

        private void addLithoTrack(string sIDTrack, cSingleWellDoc cSingleWell, int iDS1Showed, int iDS2Showed, int iTrackWidth)
        {
            XmlElement returnElemment;
            trackLithoDataList sttTrackDataListLitho = cDirDataSourceSingleWell.getTrackDataLitho(fileDrawSourceInfor, iDS1Showed, iDS2Showed);
            returnElemment = cSingleWell.gTrackLitho(sttTrackDataListLitho.fListDS1, sttTrackDataListLitho.fListDS2, sttTrackDataListLitho.iListLithoType, iTrackWidth);
            cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
        }

        private void btnMakeSection_Click(object sender, EventArgs e)
        {
            string filenameSVGMap = sJHSelected + "-单井分析图.svg";
            string filepathSingleWellGraph = cMakeSingleWellGraph.makeSingleWellGraph(filenameSVGMap, filepathStyleXML, filepathDataXML);
            FormWebNavigation formSVGView = new FormWebNavigation(cProjectManager.dirPathMap + filenameSVGMap);
            formSVGView.Show();
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            //此处必须修改保存到项目位置
            SaveFileDialog sfdModelPath = new SaveFileDialog();
            //设置文件类型 
            sfdModelPath.Title = " 请保存模板名：";
            sfdModelPath.Filter = "模板文件|*.xml";

            //设置默认文件类型显示顺序 
            sfdModelPath.FilterIndex = 1;

            sfdModelPath.RestoreDirectory = true;
            
            if (sfdModelPath.ShowDialog() == DialogResult.OK)
            { 
               string filepath=sfdModelPath.FileName;
               File.Copy(filepathStyleXML, filepath, true);
            }
            
        }
       
        private void btnAddLogTrack_Click(object sender, EventArgs e)
        {
            
            if (cbbLogName.SelectedIndex >= 0&&treeviewTrackCollection.SelectedNode!=null)
            {
                string sLogName = cbbLogName.SelectedItem.ToString();
                string sColor = cPublicMethodBase.getRGB(this.cbbLogColor.BackColor); 
                int iLeftVlaue =Convert.ToInt16( nUDLogLeftValue.Value);
                int iRightVlaue = Convert.ToInt16(nUDLogRightValue.Value);
                int iLineWidth = Convert.ToInt16(this.nUDLineWidth.Value);

                cXMLSingleWell.addTrackXMLStyleLogCurve(filepathStyleXML, treeviewTrackCollection.SelectedNode.Name,
                    sLogName,  sColor, iLeftVlaue, iRightVlaue, iLineWidth);
                //iIndexTrack++;
                if(treeviewTrackCollection.SelectedNode.Text == "曲线道")
                { 
                    TreeNode logNote = treeviewTrackCollection.SelectedNode;
                    TreeNode tLogNode = new TreeNode();
                    tLogNode.Text = sLogName;
                    tLogNode.Name = sLogName;
                    tLogNode.Tag = (TypeTrack)Enum.ToObject(typeof(TypeTrack), cbbTrack.SelectedIndex);
                    logNote.Nodes.Add(tLogNode);    
                }
                treeviewTrackCollection.ExpandAll();
            }
        }

        private void cbbLogColor_MouseClick(object sender, MouseEventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbLogColor);
        }

        private void treeviewTrackCollection_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string sItemSelected = treeviewTrackCollection.SelectedNode.Text;
                if (cProjectData.ltStrLogSeriers.Contains(sItemSelected) == true)
                {
                    FormTrackLog formSettingLogTrack = new FormTrackLog(sJHSelected,"Track" + treeviewTrackCollection.SelectedNode.Parent.Index .ToString(), sItemSelected);
                    formSettingLogTrack.ShowDialog();
                }
            }
            catch (Exception e1)
            {
            }
        }

        private void treeviewTrackCollection_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeviewTrackCollection.ContextMenuStrip = treeviewContextMenuStrip;
                treeviewContextMenuStrip.Show(treeviewTrackCollection, e.X, e.Y);
            }
        }

        private void treeviewTrackCollection_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeviewTrackCollection.SelectedNode != null)
            {
                if (treeviewTrackCollection.SelectedNode.Text == "曲线道") gbxAddLogTrack.Visible = true;
                if (!(treeviewTrackCollection.SelectedNode.Text == "曲线道")) gbxAddLogTrack.Visible = false;
            }
        }

        private void 删除选中道ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (treeviewTrackCollection.SelectedNode != null && treeviewTrackCollection.SelectedNode.Level==0)
            {
                DialogResult dialogResult = MessageBox.Show("是否确认删除选中道" + treeviewTrackCollection.SelectedNode.Text  + "，确认删除？", "删除选中井", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    cXMLSingleWell.deleteSelectTrack(filepathStyleXML, treeviewTrackCollection.SelectedNode.Name.ToString());
                    treeviewTrackCollection.SelectedNode.Remove();
                }
               
            }

            if (treeviewTrackCollection.SelectedNode != null && treeviewTrackCollection.SelectedNode.Level == 1)
            {
       
                //MessageBox.Show(treeviewTrackCollection.SelectedNode.Name);
                DialogResult dialogResult = MessageBox.Show("是否确认删除选中道" + treeviewTrackCollection.SelectedNode.Text + "，确认删除？", "删除选中井", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    cXMLSingleWell.deleteSelectLogCurve(filepathStyleXML,treeviewTrackCollection.SelectedNode.Parent.Name, treeviewTrackCollection.SelectedNode.Name.ToString());
                    treeviewTrackCollection.SelectedNode.Remove();
                }

            }

            treeviewTrackCollection.ExpandAll();
        }
        private void 选中道上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeviewTrackCollection.SelectedNode != null && treeviewTrackCollection.SelectedNode.Level == 0)
            {
                cPublicMethodForm.upTreeViewNote(this.treeviewTrackCollection);
                cXMLSingleWell.upSelectTrack(filepathStyleXML, treeviewTrackCollection.SelectedNode.Name.ToString());
                treeviewTrackCollection.ExpandAll();
            }
        }
        private void 选中道下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeviewTrackCollection.SelectedNode != null && treeviewTrackCollection.SelectedNode.Level == 0)
            {
                cPublicMethodForm.downTreeViewNote(this.treeviewTrackCollection);
                cXMLSingleWell.downSelectTrack(filepathStyleXML, treeviewTrackCollection.SelectedNode.Name.ToString());
                treeviewTrackCollection.ExpandAll();
            }
           
        }

        private void btnAddTrack_Click(object sender, EventArgs e)
        {
            if (iDS2Showed > iDS1Showed)
            {
                iIndexTrack++;
                TreeNode tnode = new TreeNode();
                tnode.Text = cbbTrack.SelectedItem.ToString();
                tnode.Name = "track" + iIndexTrack.ToString();
                tnode.Tag = (TypeTrack)Enum.ToObject(typeof(TypeTrack), cbbTrack.SelectedIndex);
                treeviewTrackCollection.Nodes.Add(tnode);
                if (cbbTrack.SelectedIndex == (int) TypeTrack.深度道)
                {
                    cXMLSingleWell.addTrackStyleXMLModel(filepathStyleXML, tnode.Text, tnode.Name, tnode.Tag);
                }
                if (cbbTrack.SelectedIndex == (int) TypeTrack.地层道)
                {
                    cXMLSingleWell.addTrackStyleXMLModel(filepathStyleXML, tnode.Text, tnode.Name, tnode.Tag);
                    cXMLSingleWell.addTrackDataXMLModel(sJHSelected, filepathDataXML, tnode.Text, tnode.Name, tnode.Tag);
                }
                if (cbbTrack.SelectedIndex == (int) TypeTrack.解释结论道)
                {
                    cXMLSingleWell.addTrackStyleXMLModel(filepathStyleXML, tnode.Text, tnode.Name, tnode.Tag);
                    cXMLSingleWell.addTrackDataXMLModel(sJHSelected, filepathDataXML, tnode.Text, tnode.Name, tnode.Tag);
                }
                if (cbbTrack.SelectedIndex == (int)TypeTrack.曲线道)
                {
                    cXMLSingleWell.addTrackStyleXMLModel(filepathStyleXML, tnode.Text, tnode.Name, tnode.Tag);
                }
                if (cbbTrack.SelectedIndex == (int)TypeTrack.射孔道)
                {
                    cXMLSingleWell.addTrackStyleXMLModel(filepathStyleXML, tnode.Text, tnode.Name, tnode.Tag);
                    cXMLSingleWell.addTrackDataXMLModel(sJHSelected, filepathDataXML, tnode.Text, tnode.Name, tnode.Tag);
                }
                if (cbbTrack.SelectedIndex == (int)TypeTrack.离散数据道)
                {
                    FormSingleLogImport formInputDataTableSingleWell = new FormSingleLogImport(TypeTrack.离散数据道.ToString());
                    formInputDataTableSingleWell.ShowDialog();
                    if (cProjectData.sTempTrackData != "")
                    {
                        cXMLSingleWell.addTrackDataScatter(filepathDataXML, iIndexTrack, sJHSelected, cProjectData.sTempTrackData);
                    }
                }
                if (cbbTrack.SelectedIndex == (int) TypeTrack.岩性道)
                {
                    FormSingleLogImport formInputDataTableSingleWell = new FormSingleLogImport(TypeTrack.岩性道.ToString());
                    cProjectData.sTempTrackData = "";
                    formInputDataTableSingleWell.ShowDialog();
                    if (cProjectData.sTempTrackData != "")
                    {
                        cXMLSingleWell.addTrackDataLitho(filepathDataXML, iIndexTrack, sJHSelected, cProjectData.sTempTrackData);
                    }
                }
                if (cbbTrack.SelectedIndex == (int) TypeTrack.文本道)
                {
                    FormSingleLogImport formInputDataTableSingleWell = new FormSingleLogImport(TypeTrack.文本道.ToString());
                    cProjectData.sTempTrackData = "";
                    formInputDataTableSingleWell.ShowDialog();
                    if (cProjectData.sTempTrackData != "")
                    {
                        cXMLSingleWell.addTrackDataText(filepathDataXML,iIndexTrack, sJHSelected, cProjectData.sTempTrackData);
                    }
                }
               
            }
            else
            {
                MessageBox.Show("底深必须大于顶深。");
            } 
     
        }
 
    }
}
