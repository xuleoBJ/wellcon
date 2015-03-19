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
    public partial class FormWellsGroup : Form
    {
        string dirSectionData = Path.Combine(cProjectManager.dirPathTemp, "sectionGroupTemp");
        List<string> ltStrSelectedJH = new List<string>();  //联井剖面井号
        //存储绘图剖面数据结构
        List<ItemWellSection> listWellsSection = new List<ItemWellSection>();
       
        public FormWellsGroup()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            InitFormWellsGroupControl();
        }
        private void InitFormWellsGroupControl()
        {
            cPublicMethodForm.inialListBox(lbxJH, cProjectData.ltStrProjectJH);
            cPublicMethodForm.inialComboBox(cbbTopXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbBottomXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbLeftLogName, cProjectData.ltStrLogSeriers);
            cPublicMethodForm.inialComboBox(cbbRightLogName, cProjectData.ltStrLogSeriers);
            initialCbbScale(this.cbbSacle);
            cPublicMethodForm.inialComboBox(cbbUnit, new List<string>(new string[] { "pt", "mm", "px", "pc", "cm", "in" }));
        }
        void initialCbbScale(ComboBox cbb)
        {
            List<string> listScale = new List<string>();
            listScale.Add("10000");
            listScale.Add("20000");
            listScale.Add("25000");
            listScale.Add("50000");
            listScale.Add("5000");
            listScale.Add("2000");
            listScale.Add("1000");
            listScale.Add("500");
            listScale.Add("250");
            listScale.Add("200");
            listScale.Add("100000");
            cbb.Items.Clear();
            foreach (string sItem in listScale) cbb.Items.Add(sItem);
            cbb.Text = (1000.0 / cProjectData.dfMapScale).ToString("0");
        }

        private void btn_addWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.transferItemFromleftListBox2rightListBox(lbxJH, lbxJHSeclected);
        }
        private void btn_deleteWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.deleteSlectedItemFromListBox(lbxJHSeclected);
        }

        private void btnSectionData_Click(object sender, EventArgs e)
        {

            setDepthIntervalShowedBYLayer();
        }
        void updateSelectedListJH()
        {
            listWellsSection.Clear();
            ltStrSelectedJH.Clear();
            foreach (object selecteditem in lbxJHSeclected.Items)
            {
                string strItem = selecteditem as String;
                ltStrSelectedJH.Add(strItem);
            }
        }
        void initializeTreeViewWellCollection()
        {
            this.tvwWellSectionCollection.Nodes.Clear();
            for (int i = 0; i < ltStrSelectedJH.Count; i++) tvwWellSectionCollection.Nodes.Add(ltStrSelectedJH[i]);
        }
        private void setDepthIntervalShowedBYLayer()
        {
            updateSelectedListJH();
            List<string> ltStrSelectedXCM = new List<string>();

            string sTopXCM = this.cbbTopXCM.SelectedItem.ToString();
            int iTopIndex = cProjectData.ltStrProjectXCM.IndexOf(sTopXCM);
            string sBottomXCM = this.cbbBottomXCM.SelectedItem.ToString();
            int iBottomIndex = cProjectData.ltStrProjectXCM.IndexOf(sBottomXCM);

            if (iBottomIndex - iTopIndex >= 0)
            {
                ltStrSelectedXCM = cProjectData.ltStrProjectXCM.GetRange(iTopIndex, iBottomIndex - iTopIndex + 1);
                initializeTreeViewWellCollection();
                int _up = Convert.ToInt16(this.nUDtopDepthUp.Value);
                int _down = Convert.ToInt16(this.nUDbottomDepthDown.Value);

                for (int i = 0; i < ltStrSelectedJH.Count; i++)
                {
                    ItemWellSection _wellSection = new ItemWellSection(ltStrSelectedJH[i], 0, 0);
                    //有可能上下层有缺失。。。所以这块的技巧是找出深度序列，取最大最小值
                    cIOinputLayerDepth fileLayerDepth = new cIOinputLayerDepth();
                    List<float> fListDS1Return = fileLayerDepth.selectDepthListFromLayerDepthByJHAndXCMList(ltStrSelectedJH[i], ltStrSelectedXCM);
                    if (fListDS1Return.Count > 0)  //返回值为空 说明所选层段整个缺失！
                    {
                        _wellSection.fShowedDepthTop = fListDS1Return.Min() - _up;
                        _wellSection.fShowedDepthBase = fListDS1Return.Max() + _down;
                    }

                    listWellsSection.Add(_wellSection);
                }
                cXDocSection.generateSectionCssXML();
                generateSectionDataDirectory();
            }
            else
            {
                MessageBox.Show("上层应该比下层选择高，请重新选择。");
            }
        }

        void generateSectionDataDirectory()
        {
            if (Directory.Exists(dirSectionData)) Directory.Delete(dirSectionData, true);
            Directory.CreateDirectory(dirSectionData);
            foreach (ItemWellSection item in listWellsSection)
            {
                string jhDir = Path.Combine(dirSectionData, item.sJH);
                Directory.CreateDirectory(jhDir);
                Directory.CreateDirectory(jhDir + "\\left");
                Directory.CreateDirectory(jhDir + "\\right");
            }
        }
        void generateSectionCssXML()
        {
            if (File.Exists(cProjectManager.xmlSectionCSS))
            {
                File.Delete(cProjectManager.xmlSectionCSS);
            }
            cXDocSection.generateXmlFile(cProjectManager.xmlSectionCSS);
        }
        
        private void lbxJHSeclected_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltStrSelectedJH.Clear();
            foreach (object selecteditem in lbxJHSeclected.Items)
            {
                string strItem = selecteditem as String;
                ltStrSelectedJH.Add(strItem);
            }
        }

        int PageWidth = 3000;
        int PageHeight = 5000;
        string sUnit = "pt";
        void generateSectionGraph(string filenameSVGMap,bool bView)
        {
            if (cbbUnit.SelectedIndex >= 0) sUnit = cbbUnit.SelectedItem.ToString();
            List<List<cSVGSectionTrackConnect.itemViewLayerDepth>> listConnectView = new List<List<cSVGSectionTrackConnect.itemViewLayerDepth>>();
            cSVGDocSection svgSection = new cSVGDocSection(PageWidth, PageHeight, 0, 0,sUnit);
            svgSection.addSVGTitle(string.Join("-", listWellsSection.Select(p => p.sJH).ToList()) + "井组分析图", 100, 100);

            XmlElement returnElemment;
            for (int i = 0; i < listWellsSection.Count; i++)
            {
                string sJH = listWellsSection[i].sJH;
                Point currentPositon = cCordinationTransform.getPointViewByJH(sJH);
                List<ItemDicWellPath> currentWellPathList = cProjectData.listProjectWell.Find(p => p.sJH == sJH).WellPathList;
                float fTopShowed = listWellsSection[i].fShowedDepthTop;
                float fBaseShowed = listWellsSection[i].fShowedDepthBase;
                float fDepthFlatted = listWellsSection[i].fShowedDepthTop;
                cSVGSectionWell currentWell = new cSVGSectionWell(sJH);
                if (currentWellPathList.Count <= 2)
                    returnElemment = currentWell.gWellCone(sJH, fTopShowed, fBaseShowed, fDepthFlatted, 10, 5);
                else
                {
                    returnElemment = currentWell.gPathWellCone(sJH, fTopShowed, fBaseShowed, fDepthFlatted, 10, 5);
                }
                currentWell.addTrack(returnElemment, 0);

                //增加地层道
                trackLayerDepthDataList trackDataListLayerDepth = cIOWellSection.trackDataListLayerDepth(sJH, dirSectionData, fTopShowed, fBaseShowed);
                int iTrackWidth = 15;
                cSVGSectionTrackLayer layerTrack = new cSVGSectionTrackLayer(iTrackWidth);
                layerTrack.iTextSize = 6;
                if (currentWellPathList.Count <= 2)
                    returnElemment = layerTrack.gTrackLayerDepth(sJH, trackDataListLayerDepth, fDepthFlatted);
                else returnElemment = layerTrack.gPathTrackLayerDepth(sJH, trackDataListLayerDepth, fDepthFlatted);
                currentWell.addTrack(returnElemment, iTrackWidth);

                //增加联井的view
                if (currentWellPathList.Count > 2)
                    listConnectView.Add(cSVGSectionTrackConnect.getListViewPathLayerConnect(sJH, currentPositon, trackDataListLayerDepth, fDepthFlatted));
                else listConnectView.Add(cSVGSectionTrackConnect.getListViewLayerConnect(sJH, currentPositon, trackDataListLayerDepth, fDepthFlatted));

                //增加解释结论道
                trackJSJLDataList trackDataListJSJL = cIOWellSection.trackDataListJSJL(sJH,dirSectionData, fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackJSJL JSJLTrack = new cSVGSectionTrackJSJL(iTrackWidth);
                if (currentWellPathList.Count <= 2)
                    returnElemment = JSJLTrack.gTrackJSJL(sJH, trackDataListJSJL, fDepthFlatted);
                else returnElemment = JSJLTrack.gPathTrackJSJL(sJH, trackDataListJSJL, fDepthFlatted);
                currentWell.addTrack(returnElemment, -iTrackWidth);

                //增加射孔道
                trackInputPerforationDataList trackDataListPerforation =cIOWellSection.trackDataListPerforation(sJH,dirSectionData,  fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackPeforation perforationTrack = new cSVGSectionTrackPeforation(iTrackWidth);
                if (currentWellPathList.Count <= 2)
                    returnElemment = perforationTrack.gTrackPerforation(sJH, trackDataListPerforation, fDepthFlatted);
                else returnElemment = perforationTrack.gPathTrackPerforation(sJH, trackDataListPerforation, fDepthFlatted);
                currentWell.addTrack(returnElemment, -2 * iTrackWidth);

                //增加吸水剖面
                trackProfileDataList trackDataListProfile = cIOWellSection.trackDataListProfile(sJH,dirSectionData, fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackProfile profileTrack = new cSVGSectionTrackProfile(iTrackWidth);
                returnElemment = profileTrack.gTrackProfile(sJH, trackDataListProfile, fDepthFlatted);
                if (currentWellPathList.Count <= 2) returnElemment = profileTrack.gTrackProfile(sJH, trackDataListProfile, fDepthFlatted);
                else returnElemment = profileTrack.gPathTrackProfile(sJH, trackDataListProfile, fDepthFlatted);
                currentWell.addTrack(returnElemment, 15);

                //增加左边曲线
                string fileLeftLogScrPath = Path.Combine(dirSectionData, sJH + "\\left");
                string[] fileList = Directory.GetFileSystemEntries(fileLeftLogScrPath);
                foreach (string fileLog in fileList)
                {
                    trackLogDataList trackDataListLeftLog = trackLogDataList.setupDataListTrackLog(fileLog, fTopShowed, fBaseShowed);
                    iTrackWidth = 15;
                    ItemLogHeadInfor itemHeadInfor = new ItemLogHeadInfor();
                    itemHeadInfor.sJH = sJH;
                    itemHeadInfor.sLogName = trackDataListLeftLog.sLogName;
                    itemHeadInfor.sLogColor = cPublicMethodBase.getRGB(cbbColorLeftLog.BackColor);
                    itemHeadInfor.fRightValue = Convert.ToSingle(nUDLeftLogRightValue.Value);
                    itemHeadInfor.fLeftValue = Convert.ToSingle(this.nUDLeftLogLeftValue.Value);
                    cSVGSectionTrackLog logTrack = new cSVGSectionTrackLog(iTrackWidth);
                    if (currentWellPathList.Count <= 2)
                        returnElemment = logTrack.gTrackLog(itemHeadInfor, trackDataListLeftLog, fDepthFlatted);
                    else returnElemment = logTrack.gPathTrackLog(itemHeadInfor, trackDataListLeftLog, fDepthFlatted);
                    currentWell.addTrack(returnElemment, -30);
                }
                //增加右边曲线
                string fileRightLogScrPath = Path.Combine(dirSectionData, sJH + "\\right");
                foreach (string fileLog in Directory.GetFileSystemEntries(fileRightLogScrPath))
                {
                    trackLogDataList trackDataListRightLog = trackLogDataList.setupDataListTrackLog(fileLog, fTopShowed, fBaseShowed);
                    iTrackWidth = 15;
                    ItemLogHeadInfor itemHeadInfor = new ItemLogHeadInfor();
                    itemHeadInfor.sJH = sJH;
                    itemHeadInfor.sLogName = trackDataListRightLog.sLogName;
                    itemHeadInfor.sLogColor = cPublicMethodBase.getRGB(cbbColorRightLog.BackColor);
                    itemHeadInfor.fRightValue = Convert.ToSingle(this.nUDRightLogRightValue.Value);
                    itemHeadInfor.fLeftValue = Convert.ToSingle(this.nUDRightLogLeftValue.Value);
                    cSVGSectionTrackLog logTrack = new cSVGSectionTrackLog(iTrackWidth);
                    if (currentWellPathList.Count <= 2)
                        returnElemment = logTrack.gTrackLog(itemHeadInfor, trackDataListRightLog, fDepthFlatted);
                    else returnElemment = logTrack.gPathTrackLog(itemHeadInfor, trackDataListRightLog, fDepthFlatted);
                    currentWell.addTrack(returnElemment, iTrackWidth);
                }
                svgSection.addgElement2LayerBase(currentWell.gWell, currentPositon);
            }

            //if ( cbxConnectSameLayerName.Checked== true)
            //{
            //    List<string> listXCM = listConnectView[0].Select(p => p.sXCM).ToList();
            //    foreach (string xcm in listXCM)
            //    {
            //        List<cSVGSectionTrackConnect.itemViewLayerDepth> ListLayerViewLayerDepth = new List<cSVGSectionTrackConnect.itemViewLayerDepth>();
            //        for (int i = 0; i < listConnectView.Count; i++) ListLayerViewLayerDepth.Add(listConnectView[i].Find(p => p.sXCM == xcm));
            //        cSVGSectionTrackConnect layerConnect = new cSVGSectionTrackConnect();
            //        returnElemment = layerConnect.gConnectPath(ListLayerViewLayerDepth);
            //        svgSection.addgElement2LayerBase(returnElemment, 0,0);
            //    }
            //}

            string fileSVG = Path.Combine(cProjectManager.dirPathMap, filenameSVGMap);
            svgSection.makeSVGfile(fileSVG);
            if (bView == false)
            {
                FormMain.filePathWebSVG = fileSVG;
                this.Close();
            }
        }
     
        private void btnMakeSection_Click(object sender, EventArgs e)
        {
            string _filenameSVG;
            if (this.tbxTitle.Text == "")
            {
                if (ltStrSelectedJH.Count < 6) _filenameSVG ="井组分析_"+ string.Join("-", ltStrSelectedJH.ToArray()) + ".svg";
                else _filenameSVG = "井组分析_" + string.Join("-", ltStrSelectedJH.GetRange(0, 5)) + ".svg";
            }
            else _filenameSVG = this.tbxTitle.Text + ".svg";
            generateSectionGraph(_filenameSVG,false);
           
        }

        private void btnAddJSJLTrack_Click(object sender, EventArgs e)
        {
            if (this.listWellsSection.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH) cIOWellSection.addSectionDataJSJL(sJH, dirSectionData); //提取所选井段数据存入绘图目录下保存 
               
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
                {
                    if (wellNote.Text != "深度尺")
                        wellNote.Nodes.Add("解释结论");
                }

                tvwWellSectionCollection.ExpandAll();
            }
            else MessageBox.Show("请先确认深度段。");
        }

        private void btnAddLayerDepth_Click(object sender, EventArgs e)
        {
            if (this.listWellsSection.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH) cIOWellSection.addSectionDataLayerDepth(sJH, dirSectionData); //提取所选井段数据存入绘图目录下保存
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes) wellNote.Nodes.Add("地层");
                tvwWellSectionCollection.ExpandAll();
            }
            else MessageBox.Show("请先确认深度段。");
        }

      private void btnAddPerforation_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH) cIOWellSection.addSectionDataPerforation(sJH, dirSectionData); //提取所选井段数据存入绘图目录下保存
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes) wellNote.Nodes.Add("射孔");
                tvwWellSectionCollection.ExpandAll(); 
            }
            else MessageBox.Show("请先确认深度段。");
        }
       
        private void lbxJHSeclected_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ltStrSelectedJH.Clear();
            foreach (object selecteditem in lbxJHSeclected.Items)
            {
                string strItem = selecteditem as String;
                ltStrSelectedJH.Add(strItem);
            }
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            lbxJHSeclected.Items.Clear();
        }

        private void btnScale_Click(object sender, EventArgs e)
        {
            cProjectData.dfMapScale =cProjectData.dfMapScale* 1.5F;
            MessageBox.Show("当前比例尺:" + cProjectData.dfMapScale.ToString());
        }

        private void tabControlFenceDiagram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcWellsGroup.SelectedIndex == 1 && File.Exists(cProjectManager.xmlConfigFenceDiagram )== false)
            { cXMLFenceDiagram.creatFenceDiagramSettingXML(cProjectManager.xmlConfigFenceDiagram); }
        }

    
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.inialListBox(lbxJHSeclected, cProjectData.ltStrProjectJH);
        }

        private void FormMapFence_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnLogTrackAddLeft_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0 && cbbLeftLogName.SelectedIndex >= 0)
            {
                string sSelectedLogName = this.cbbLeftLogName.SelectedItem.ToString();
                addLogData((int)LeftOrRight.left, sSelectedLogName);
            }
            else MessageBox.Show("请先确认深度段。");
        }

        private void btnLogTrackAddRight_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0 && cbbRightLogName.SelectedIndex >= 0)
            {
                string sSelectedLogName = this.cbbRightLogName.SelectedItem.ToString();
                addLogData((int)LeftOrRight.right, sSelectedLogName);
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }
    
        void addLogData(int iLeftOrRight, string sSelectedLogName)
        {
            foreach (string sJH in ltStrSelectedJH)
            {
                string filePath = Path.Combine(dirSectionData, sJH + "\\left\\" + sSelectedLogName + ".txt");
                if (iLeftOrRight == (int)LeftOrRight.right)
                {
                    filePath = Path.Combine(dirSectionData, sJH + "\\right\\" + sSelectedLogName + ".txt");
                }
                cIOinputLog.extractTextLog2File(sJH, sSelectedLogName, filePath);
            }
            foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
            {
                TreeNode tnLog = new TreeNode();
                tnLog.Text = sSelectedLogName;
                if (wellNote.Index > 0)
                {
                    wellNote.Nodes.Add(tnLog);
                }

            }
            tvwWellSectionCollection.ExpandAll();
        }

        private void cbbColorLeftLog_MouseClick(object sender, MouseEventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbColorLeftLog);
        }

        private void cbbColorRightLog_MouseClick(object sender, MouseEventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbColorRightLog);
        }

        private void tsCbbScale_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnBig_Click(object sender, EventArgs e)
        {
            cProjectData.dfMapScale = cProjectData.dfMapScale * 1.2F;
        }


        private void btnAddTrackProfile_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH) cIOWellSection.addSectionDataProfile(sJH, dirSectionData); //提取所选井段数据存入绘图目录下保存 
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes) wellNote.Nodes.Add("吸水剖面");
                tvwWellSectionCollection.ExpandAll();
            }
            else MessageBox.Show("请先确认深度段。");
        }

        private void cbbTopXCM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbBottomXCM.Items.Count>0)
            this.cbbBottomXCM.SelectedIndex = cbbTopXCM.SelectedIndex+1;
        }

        private void trackBarSacle_Scroll(object sender, EventArgs e)
        {
            //cProjectData.dfMapScale = cProjectData.dfMapScale * (trackBarSacle.Value / 5.0);
            //this.lblSacle.Text = (1000.0 / cProjectData.dfMapScale).ToString("0");
        }

        private void btnSacleBigger_Click(object sender, EventArgs e)
        {
            cProjectData.dfMapScale = cProjectData.dfMapScale * 1.2F;
            this.cbbSacle.Text = (1000.0 / cProjectData.dfMapScale).ToString("0");
        }

        private void btnSacleSmaller_Click(object sender, EventArgs e)
        {
            cProjectData.dfMapScale = cProjectData.dfMapScale * 0.8F;
            this.cbbSacle.Text = (1000.0 / cProjectData.dfMapScale).ToString("0");
        }

        private void cbbSacle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSacle.SelectedIndex >= 0)
            {
                float dfscale = float.Parse(cbbSacle.SelectedItem.ToString());
                cProjectData.dfMapScale = 1000 / dfscale;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string tempSVGViewfilepath = Path.Combine(cProjectManager.dirPathTemp, "#view.svg");
            generateSectionGraph(tempSVGViewfilepath,true);
            FormWebNavigation formSVGView = new FormWebNavigation(tempSVGViewfilepath);
            formSVGView.ShowDialog();
        }

        private void nUDPageWidth_ValueChanged(object sender, EventArgs e)
        {
            PageWidth = Convert.ToInt16(nUDPageWidth.Value);
        }

        private void nUDPageHeight_ValueChanged(object sender, EventArgs e)
        {
            PageHeight = Convert.ToInt16(nUDPageHeight.Value);
        }

      

    }
}
