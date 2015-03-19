#region << 版 本 注 释 >>
/*
 * ========================================================================
 * Copyright(c) 2014 Xuleo,Riped, All Rights Reserved.
 * ========================================================================
 *  许磊，联系电话13581625021，qq：38643987

 * ========================================================================
*/
#endregion
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
    //成图方法 
    // 1. 选择井
    // 2. 根据层位或者海拔确定深度段深度
    // 3. 根据深度提取成图原始数据文件，放在临时文件下
    // 4. 读取各个临时文件，成图
    //
    public partial class FormWellSectionGeology : Form
    {
        string dirSectionData = Path.Combine(cProjectManager.dirPathTemp, "sectionGeoTemp");
        enum typeFlatted
        {
            海拔深度,
            顶面拉平,
            底面拉平,
        }
        List<string> ltStrSelectedJH = new List<string>();  //联井剖面井号
        //存储绘图剖面数据结构
        List<ItemWellSection> listWellsSection = new List<ItemWellSection>();
     
        public FormWellSectionGeology()
        {
            InitializeComponent();
        }
        private void FormNewWellSection_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitFormWellsGroupControl();
        }

        private void InitFormWellsGroupControl()
        {
            cPublicMethodForm.inialListBox(lbxJH, cProjectData.ltStrProjectJH);
            cPublicMethodForm.inialComboBox(cbbTopXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbBottomXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbLogName, cProjectData.ltStrLogSeriers);
            dgvLayerColorSetting.Columns.Add("Layer", "小层名");
            dgvLayerColorSetting.Columns.Add("Color", "颜色");
            for (int i = 0; i < cProjectData.ltStrProjectXCM.Count; i++)
            {
                string _sItem = cProjectData.ltStrProjectXCM[i];
                dgvLayerColorSetting.Rows.Add(_sItem);
                dgvLayerColorSetting.Rows[i].Cells[1].Style.BackColor = Color.Red;
            }
            cPublicMethodForm.inialComboBox(cbbUnit, new List<string>(new string[] { "mm", "pt", "px", "pc", "cm", "in"}));
        }

        private void btn_addWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.transferItemFromleftListBox2rightListBox(lbxJH, lbxJHSeclected);
        }
        private void btn_deleteWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.deleteSlectedItemFromListBox(lbxJHSeclected);
        }
        private void btn_upWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.upItemInListBox(lbxJHSeclected);
        }
        private void btn_downWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.downItemInListBox(lbxJHSeclected);
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

        private void btnSectionData_Click(object sender, EventArgs e)
        {
            setDepthIntervalShowedBYLayer();
        }

        void initializeTreeViewWellCollection()
        {
            this.tvWellSectionCollection.Nodes.Clear();
            for (int i = 0; i < ltStrSelectedJH.Count; i++)
            {
                TreeNode tnWell = new TreeNode();
                tnWell.Text = ltStrSelectedJH[i];
                tnWell.Name = ltStrSelectedJH[i];
                tnWell.Nodes.Add("左侧曲线");
                tnWell.Nodes.Add("右侧曲线");
                tvWellSectionCollection.Nodes.Add(tnWell);
            }
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

        private void btnGenerateDataByInputDepth_Click(object sender, EventArgs e)
        {
            setDepthIntervalShowedBYElevationDepth();
        }

        private void setDepthIntervalShowedBYElevationDepth()
        {
            updateSelectedListJH();
            int iTopElevation = int.Parse(this.tbxTopElevationInput.Text);
            int iBottomElevation = int.Parse(this.tbxBottomElevationInput.Text);

            initializeTreeViewWellCollection();
            for (int i = 0; i < ltStrSelectedJH.Count; i++)
            {
                ItemWellSection _wellSection = new ItemWellSection(ltStrSelectedJH[i], 0, 0);
                //海拔转成md
                _wellSection.fShowedDepthTop = _wellSection.fKB - iTopElevation;
                _wellSection.fShowedDepthBase = _wellSection.fKB - iBottomElevation;
                listWellsSection.Add(_wellSection);
            }
            cXDocSection.generateSectionCssXML();
            generateSectionDataDirectory();
        }

        void openExitGraph()
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlSectionCSS);
            XElement XWellCollect = sectionMapXML.Element("SectionMap").Element("WellCollection");

            foreach (XElement el in XWellCollect.Elements())
            {
                string sJH = el.Attribute("id").Value;
                double dbX = double.Parse(el.Element("X").Value);
                double dbY = double.Parse(el.Element("Y").Value);
                float fKB = float.Parse(el.Element("KB").Value);
                float fTopShowed = float.Parse(el.Element("fShowedTop").Value);
                float fBaseShowed = float.Parse(el.Element("fShowedBottom").Value);
            }
        }


        int ElevationRulerTop = 0;
        int ElevationRulerBase = -5000;
        int PageWidth = 3000;
        int PageHeight = 5000;
        string sUnit ="px"; 
     
       
        void generateSectionGraph(typeFlatted flatType, string filenameSVGMap,bool bView)
        {
            //xml存数据不合适 因为会有大量的井数据，但是可以存个样式，样式搭配数据，样式里可以有道宽，这样做到数据和样式的分离，成图解析器解析样式就OK。
           
            //根据默认值定义section的页面大小 及标题
            if(cbbUnit.SelectedIndex>=0) sUnit= cbbUnit.SelectedItem.ToString();
            cSVGDocSection svgSection = new cSVGDocSection(PageWidth, PageWidth, 0, 0, sUnit);
               string sTitle=string.Join("-", listWellsSection.Select(p => p.sJH).ToList()) + "剖面图";
            svgSection.addSVGTitle(sTitle, 100, 100); 
            XmlElement returnElemment;

            //定义每口井绘制的位置坐标，剖面图y=0，井组分析x，y是井点坐标变换值
            List<Point> PListWellPositon = new List<Point>();
            List<List<cSVGSectionTrackConnect.itemViewLayerDepth>> listConnectView = new List<List<cSVGSectionTrackConnect.itemViewLayerDepth>>();
            List<int> listArrangeDistance = new List<int>();
            int iWellStartXPosition = 100;
            for (int i = 0; i < this.listWellsSection.Count; i++)
            {
                ItemWellSection itemWell = listWellsSection[i];
                //传入的深度与绘制无关，传入的海拔就是正，绘制时海拔向下为正
                if (flatType ==typeFlatted.海拔深度) itemWell.fDepthFlatted = itemWell.fKB;
                if (flatType == typeFlatted.顶面拉平) itemWell.fDepthFlatted = itemWell.fKB - itemWell.fShowedDepthTop;
                if (flatType == typeFlatted.底面拉平) itemWell.fDepthFlatted = itemWell.fKB - itemWell.fShowedDepthBase;
             
                if (rdbPlaceByEqual.Checked == true) PListWellPositon.Add(new Point(100+200*i*trackBarWellDistance.Value/10,0));
                if (rdbPlaceBYWellDistance.Checked == true)
                {
                    //第一口井Xview从100开始
                    if (i == 0) PListWellPositon.Add(new Point(iWellStartXPosition, 0));
                    else
                    {
                        //这块距离是距离上一口井的相对距离
                        int iDistance = Convert.ToInt16(c2DGeometryAlgorithm.calDistance2D(listWellsSection[i].dbX, listWellsSection[i].dbY, listWellsSection[i - 1].dbX, listWellsSection[i - 1].dbY)) * trackBarWellDistance.Value / 10;
                        listArrangeDistance.Add(iDistance);
                        //注意加上基准点的100,注意井距做了一定的缩放，通过trackbar
                        PListWellPositon.Add(new Point(iWellStartXPosition + listArrangeDistance.Sum(), 0));
                    }
                }
            }

            //画井距尺
            for (int i = 0; i < this.listWellsSection.Count-1; i++)
            {
              
                if (rdbPlaceBYWellDistance.Checked == true)
                {
                    //距离是2口相邻井的距离
                    int iDistance = Convert.ToInt16(c2DGeometryAlgorithm.calDistance2D(listWellsSection[i].dbX, listWellsSection[i].dbY, listWellsSection[i+1].dbX, listWellsSection[i+1].dbY));
                    returnElemment = svgSection.gWellDistanceRuler(iDistance, PListWellPositon[i + 1].X - PListWellPositon[i].X);
                    svgSection.addgElement2LayerBase(returnElemment, PListWellPositon[i].X, (int)listWellsSection[0].fDepthFlatted);//拉到同一水平线
                    //画井距尺
                }
            }
         
            //海拔深度时 增加海拔尺，拉平不要海拔尺
            if (flatType == typeFlatted.海拔深度)
            {
                int iScaleElevationRuler = 50;
                cSVGSectionTrackElevationRuler cElevationRuler = new cSVGSectionTrackElevationRuler();
                returnElemment = cElevationRuler.gElevationRuler(ElevationRulerTop, ElevationRulerBase, iScaleElevationRuler);
                svgSection.addgElement2LayerBase(returnElemment, 0);
            }

            //根据井序列循环添加井剖面
            for (int i = 0; i < listWellsSection.Count; i++)
            {
                string sJH = listWellsSection[i].sJH;

                List<ItemDicWellPath> currentWellPathList = cProjectData.listProjectWell.Find(p => p.sJH == sJH).WellPathList;
                float fTopShowed = listWellsSection[i].fShowedDepthTop;
                float fBaseShowed = listWellsSection[i].fShowedDepthBase;
                float fDepthFlatted = listWellsSection[i].fDepthFlatted;

                cSVGSectionWell currentWell = new cSVGSectionWell(sJH);

                if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                {
                    ItemDicWellPath wellPathTop = cIOinputWellPath.getWellPathItemByJHAndMD(sJH, fTopShowed);
                    ItemDicWellPath wellPathBase = cIOinputWellPath.getWellPathItemByJHAndMD(sJH, fBaseShowed);
                    returnElemment = currentWell.gWellCone(sJH, wellPathTop.f_TVD ,wellPathBase.f_TVD, fDepthFlatted, 10, 5);
                }
                else returnElemment = currentWell.gWellCone(sJH, fTopShowed, fBaseShowed, fDepthFlatted, 10, 5);
                currentWell.addTrack(returnElemment, 0);

                //增加地层道
                trackLayerDepthDataList trackDataListLayerDepth = cIOWellSection.trackDataListLayerDepth(sJH, dirSectionData, fTopShowed, fBaseShowed);
                int iTrackWidth = 15;
                cSVGSectionTrackLayer layerTrack = new cSVGSectionTrackLayer(iTrackWidth);
                layerTrack.iTextSize = 6;
                if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                    returnElemment = layerTrack.gXieTrack2VerticalLayerDepth(sJH, trackDataListLayerDepth, fDepthFlatted); 
                else returnElemment = layerTrack.gTrackLayerDepth(sJH,trackDataListLayerDepth, fDepthFlatted);
                currentWell.addTrack(returnElemment, iTrackWidth);

                //增加联井的view
                if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                    listConnectView.Add(cSVGSectionTrackConnect.getListViewXieTrack2VerticalLayerConnect(sJH,  PListWellPositon[i].X, trackDataListLayerDepth, fDepthFlatted));
                else listConnectView.Add(cSVGSectionTrackConnect.getListViewLayerConnect(sJH, PListWellPositon[i].X, trackDataListLayerDepth, fDepthFlatted));

                //增加解释结论道
                trackJSJLDataList trackDataListJSJL = cIOWellSection.trackDataListJSJL(sJH,dirSectionData, fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackJSJL JSJLTrack = new cSVGSectionTrackJSJL(iTrackWidth);
                if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2) 
                    returnElemment = JSJLTrack.gXieTrack2VerticalJSJL(sJH, trackDataListJSJL, fDepthFlatted);
                 else returnElemment = JSJLTrack.gTrackJSJL(sJH,trackDataListJSJL, fDepthFlatted);
                currentWell.addTrack(returnElemment, -iTrackWidth);

                //增加射孔道
                trackInputPerforationDataList trackDataListPerforation =cIOWellSection.trackDataListPerforation(sJH,dirSectionData,  fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackPeforation perforationTrack = new cSVGSectionTrackPeforation(iTrackWidth);
                if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                    returnElemment = perforationTrack.gXieTrack2VerticalPerforation(sJH, trackDataListPerforation, fDepthFlatted);
                 else returnElemment = perforationTrack.gTrackPerforation(sJH,trackDataListPerforation, fDepthFlatted);
                currentWell.addTrack(returnElemment, -2 * iTrackWidth);

                //增加吸水剖面
                trackProfileDataList trackDataListProfile = cIOWellSection.trackDataListProfile(sJH,dirSectionData, fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackProfile profileTrack = new cSVGSectionTrackProfile(iTrackWidth);
                returnElemment = profileTrack.gTrackProfile(sJH, trackDataListProfile, fDepthFlatted);
                if (currentWellPathList.Count > 2)
                    returnElemment = profileTrack.gXieTrack2VerticalProfile(sJH, trackDataListProfile, fDepthFlatted);
                else returnElemment = profileTrack.gTrackProfile(sJH, trackDataListProfile, fDepthFlatted);
                currentWell.addTrack(returnElemment, 15);

                //增加左边曲线
                string fileLeftLogScrPath = Path.Combine(dirSectionData, sJH + "\\left");
                foreach (string fileLog in Directory.GetFileSystemEntries(fileLeftLogScrPath))
                {
                    trackLogDataList trackDataListLeftLog = trackLogDataList.setupDataListTrackLog(fileLog, fTopShowed, fBaseShowed);
                    iTrackWidth = 15;

                    cSVGSectionTrackLog logTrack = new cSVGSectionTrackLog(iTrackWidth);
                    if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                        returnElemment = logTrack.gXieTrack2VerticalLog(sJH, trackDataListLeftLog, fDepthFlatted);
                    else returnElemment = logTrack.gTrackLog(sJH, trackDataListLeftLog, fDepthFlatted);
                    currentWell.addTrack(returnElemment, -30);
                }

                //增加右边曲线
                string fileRightLogScrPath = Path.Combine(dirSectionData, sJH + "\\right");
                foreach (string fileLog in Directory.GetFileSystemEntries(fileRightLogScrPath))
                {
                    trackLogDataList trackDataListRightLog = trackLogDataList.setupDataListTrackLog(fileLog, fTopShowed, fBaseShowed);
                    iTrackWidth = 15;
                    cSVGSectionTrackLog logTrack = new cSVGSectionTrackLog(iTrackWidth);
                    if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                        returnElemment = logTrack.gXieTrack2VerticalLog(sJH, trackDataListRightLog, fDepthFlatted);
                    else returnElemment = logTrack.gTrackLog(sJH,  trackDataListRightLog, fDepthFlatted);

                    currentWell.addTrack(returnElemment, iTrackWidth);
                } 
                svgSection.addgElement2LayerBase(currentWell.gWell,  PListWellPositon[i].X);
            }

            //同名小层连线的实现 在绘制小层layertrack的时候，把小层的顶底深的绘制点记录，然后此处当做polyline绘制
            bool bConnect = this.cbxConnectSameLayerName.Checked;

            if (bConnect == true)
            {
                XmlElement gConnectLayer=svgSection.gLayerElement("connectLine");
                svgSection.addgLayer(gConnectLayer, 0);
                List<string> listXCM = listConnectView[0].Select(p => p.sXCM).ToList();
                foreach (string xcm in listXCM)
                {
                    List<cSVGSectionTrackConnect.itemViewLayerDepth> ListLayerViewLayerDepth = new List<cSVGSectionTrackConnect.itemViewLayerDepth>();
                    for (int i = 0; i < listConnectView.Count; i++) ListLayerViewLayerDepth.Add(listConnectView[i].Find(p => p.sXCM == xcm));
                    cSVGSectionTrackConnect layerConnect = new cSVGSectionTrackConnect();
                    returnElemment = layerConnect.gConnectPath(ListLayerViewLayerDepth);
                    svgSection.addgElement2Layer(gConnectLayer, returnElemment, 0);  
                }
            }

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
            string filenameSVGMap;

            if (this.tbxTitle.Text == "")
            {
                if (ltStrSelectedJH.Count < 6) filenameSVGMap = "剖面图_"+string.Join("-", ltStrSelectedJH.ToArray()) + ".svg"; 
                else filenameSVGMap = "剖面图_"+string.Join("-", ltStrSelectedJH.GetRange(0, 5)) + ".svg"; 
            
            }
            else filenameSVGMap = this.tbxTitle.Text + ".svg";

            if (rdbFlattedByDepth.Checked == true) generateSectionGraph(typeFlatted.海拔深度, filenameSVGMap,false ) ; 
            else if (rdbFlattedByTopDepth.Checked == true) generateSectionGraph(typeFlatted.顶面拉平, filenameSVGMap,false );
            else if (rdbFlattedByBaseDepth.Checked == true) generateSectionGraph(typeFlatted.底面拉平, filenameSVGMap,false);

        }

        private void btnAddLayerDepth_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH) cIOWellSection.addSectionDataLayerDepth(sJH, dirSectionData); //提取所选井段数据存入绘图目录下保存
                addTreeViewWellSectionCollection("地层");
                cXDocSection.addTrackLayer(cProjectManager.xmlSectionCSS, "idLayer",20);
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }

        private void btnAddJSJLTrack_Click(object sender, EventArgs e)
        {
            if (listWellsSection.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH) cIOWellSection.addSectionDataJSJL(sJH, dirSectionData); //提取所选井段数据存入绘图目录下保存 
                addTreeViewWellSectionCollection("解释结论");
             
                cXDocSection.addTrackJSJL(cProjectManager.xmlSectionCSS, "idJSJL", 20);
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }

        void addLogData(int iLeftOrRight, string sSelectedLogName)
        {
            //iLeftOrRight, 0 左 1 右
            foreach (string sJH in ltStrSelectedJH)
            {
                string filePath = Path.Combine(dirSectionData, sJH + "\\left\\" + sSelectedLogName + ".txt");
                if (iLeftOrRight == 1)
                {
                    filePath = Path.Combine(dirSectionData, sJH + "\\right\\" + sSelectedLogName + ".txt");
                }
                cIOWellSection.addLog(sJH, sSelectedLogName, filePath);
            }
            foreach (TreeNode wellNote in tvWellSectionCollection.Nodes)
            {
                TreeNode tnLog = new TreeNode();
                tnLog.Text = sSelectedLogName;
                wellNote.Nodes[iLeftOrRight].Nodes.Add(tnLog);
            }

            tvWellSectionCollection.ExpandAll();
           
            string sLogName=this.cbbLogName.SelectedItem.ToString();
            string sLogColor=cPublicMethodBase.getRGB(cbbLogColor.BackColor);
            float fRightValue=Convert.ToSingle(nUDLogRightValue.Value);
            float fLeftValue=Convert.ToSingle(nUDLogLeftValue.Value);
            cIOWellSection.addLogProperty(sLogName, sLogColor,fLeftValue, fRightValue, iLeftOrRight);
        }
        
        void deleteLogData(int iLeftOrRight, string sSelectedLogName)
        {

            foreach (string sJH in ltStrSelectedJH)
            {
                string filePath = Path.Combine(dirSectionData, sJH + "\\leftLog.txt");
                if (iLeftOrRight == (int)LeftOrRight.right)
                {
                    filePath = Path.Combine(dirSectionData, sJH + "\\rightLog.txt");
                }

            }
            foreach (TreeNode wellNote in tvWellSectionCollection.Nodes)
            {
                if (iLeftOrRight == (int)LeftOrRight.left)
                {
                    TreeNode tnLeftLog = new TreeNode();
                    tnLeftLog.Text = "左侧曲线";
                    tnLeftLog.Name = LeftOrRight.left.ToString();
                    tnLeftLog.Nodes.Add(sSelectedLogName);
                    wellNote.Nodes.Add(tnLeftLog);
                }
                else
                {
                    TreeNode tnRightLog = new TreeNode();
                    tnRightLog.Text = "右侧曲线";
                    tnRightLog.Name = LeftOrRight.right.ToString();
                    tnRightLog.Nodes.Add(sSelectedLogName);
                    wellNote.Nodes.Add(tnRightLog);
                }
            }
            tvWellSectionCollection.ExpandAll();
        }
        private void btnAddLeftLogTrack_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0 && cbbLogName.SelectedIndex >= 0)
            {
                string sSelectedLogName = this.cbbLogName.SelectedItem.ToString();
                if(rdbLeft.Checked==true) addLogData(0, sSelectedLogName);
                else addLogData(1, sSelectedLogName); 
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }

        }
        private void cbbLeftLogColor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbLogColor);
        }


        private void tvwWellSectionCollection_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectNode = tvWellSectionCollection.SelectedNode;
            ContextMenuStrip cmsSection = new System.Windows.Forms.ContextMenuStrip();
            tvWellSectionCollection.ContextMenuStrip = cmsSection;
            string _sJH = "";
            string _fileLogScrPath = "";
            string _sLogName = "";
            cContextMenuStripWellSection cContextMenuStrip;

            switch (selectNode.Level)
            {
                case 0:
                    break;
                case 1:
                    _sJH = selectNode.Parent.Text;
                    switch (selectNode.Text)
                    {
                        case "左侧曲线":
                            _fileLogScrPath = Path.Combine(dirSectionData, _sJH + "\\left");
                            cContextMenuStrip = new cContextMenuStripWellSection
                                            (cmsSection, selectNode, _sJH, _sLogName, _fileLogScrPath);
                            cContextMenuStrip.setupTsmiLogAdd();
                            cmsSection = cContextMenuStrip.cms;
                            break;
                        case "右侧曲线":
                            _fileLogScrPath = Path.Combine(dirSectionData, _sJH + "\\right");
                            cContextMenuStrip = new cContextMenuStripWellSection
                                            (cmsSection, selectNode, _sJH, _sLogName, _fileLogScrPath);
                            cContextMenuStrip.setupTsmiLogAdd();
                            cmsSection = cContextMenuStrip.cms;
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    _sJH = selectNode.Parent.Parent.Text;
                    _sLogName = selectNode.Text;
                    _fileLogScrPath = Path.Combine(dirSectionData, _sJH + "\\right");
                    if (selectNode.Parent.Parent.Text == "左侧曲线")
                    {
                        _fileLogScrPath = Path.Combine(dirSectionData, _sJH + "\\left");
                    }

                    cContextMenuStrip = new cContextMenuStripWellSection
                         (cmsSection, selectNode, _sJH, _sLogName, _fileLogScrPath);
                    cContextMenuStrip.setupTsmiLogDelete();
                    cContextMenuStrip.setupTsmiLogSetting();
                    cmsSection = cContextMenuStrip.cms;
                    break;
                case 3:
                    MessageBox.Show(selectNode.Text);
                    break;
                default:
                    break;
            }
        }

        void addTreeViewWellSectionCollection(string treenodeText)
        {
            foreach (TreeNode wellNote in tvWellSectionCollection.Nodes)
            {
                TreeNode tn = new TreeNode(treenodeText);
                if (!cPublicMethodForm.NodeExists(wellNote, treenodeText)) wellNote.Nodes.Add(tn);
            }
            tvWellSectionCollection.ExpandAll();
        }

        private void btnAddPeforation_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH) cIOWellSection.addSectionDataPerforation(sJH, dirSectionData); //提取所选井段数据存入绘图目录下保存
                addTreeViewWellSectionCollection("射孔");
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLayerColorSetting.CurrentCell.ColumnIndex == 1)
            {
                ColorDialog colorDialog1 = new ColorDialog();
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    dgvLayerColorSetting.CurrentCell.Style.BackColor = colorDialog1.Color;
            }
        }

        private void cbbBottomXCM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbTopXCM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbBottomXCM.Items.Count > 0)
                this.cbbBottomXCM.SelectedIndex = cbbTopXCM.SelectedIndex;
        }

        private void btnAddProfile_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH) cIOWellSection.addSectionDataProfile(sJH, dirSectionData); //提取所选井段数据存入绘图目录下保存 
                addTreeViewWellSectionCollection("吸水");
            }
            else MessageBox.Show("请先确认深度段。");
        }

        private void btnGenerateDataByBaseDepth_Click(object sender, EventArgs e)
        {
            setDepthIntervalShowedBYBaseDepth();
        }

        private void setDepthIntervalShowedBYBaseDepth()
        {
            updateSelectedListJH();

            initializeTreeViewWellCollection();
            List<ItemWellHead> listWellHead= cIOinputWellHead.readWellHead2Struct();
           for (int i = 0; i < ltStrSelectedJH.Count; i++)
            {
                ItemWellSection _wellSection = new ItemWellSection(ltStrSelectedJH[i], 0, 0);
                _wellSection.fShowedDepthTop = 0;
                _wellSection.fShowedDepthBase = listWellHead.Find(p => p.sJH == ltStrSelectedJH[i]).fWellBase;
                listWellsSection.Add(_wellSection); 
            }
            cXDocSection.generateSectionCssXML();
            generateSectionDataDirectory(); 
           
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string tempSVGViewfilepath = Path.Combine(cProjectManager.dirPathTemp, "#view.svg");
            if (rdbFlattedByDepth.Checked == true) generateSectionGraph(typeFlatted.海拔深度, tempSVGViewfilepath, true);
            else if (rdbFlattedByTopDepth.Checked == true) generateSectionGraph(typeFlatted.顶面拉平, tempSVGViewfilepath, true);
            else if (rdbFlattedByBaseDepth.Checked == true) generateSectionGraph(typeFlatted.底面拉平, tempSVGViewfilepath, true);
            FormWebNavigation formSVGView = new FormWebNavigation(tempSVGViewfilepath);
            formSVGView.Show();
        }

        private void nUDElevationRulerTop_ValueChanged(object sender, EventArgs e)
        {
            ElevationRulerTop = Convert.ToInt16(nUDElevationRulerTop.Value);
        }

        private void nUDElevationRulerBottom_ValueChanged(object sender, EventArgs e)
        {
            ElevationRulerBase = Convert.ToInt16(nUDElevationRulerBottom.Value);
        }

        private void nUDPageWidth_ValueChanged(object sender, EventArgs e)
        {
            PageWidth = Convert.ToInt16(nUDPageWidth.Value);
        }

        private void nUDPageHeight_ValueChanged(object sender, EventArgs e)
        {
            PageHeight = Convert.ToInt16(nUDPageHeight.Value);
        }

        private TreeNode FindNode(TreeNode tnParent, string strValue)
        {

            if (tnParent == null) return null;

            if (tnParent.Text == strValue) return tnParent;

            TreeNode tnRet = null;

            foreach (TreeNode tn in tnParent.Nodes)
            {

                tnRet = FindNode(tn, strValue);
                if (tnRet != null) break;
            }

            return tnRet;

        }
        private void btnDeleteLog_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0 && cbbLogName.SelectedIndex >= 0)
            {
                string sSelectedLogName = this.cbbLogName.SelectedItem.ToString();
                foreach (string _sJH in ltStrSelectedJH)
                {
                    string _fileLogScrPath = "";
                    if (rdbLeft.Checked == true) _fileLogScrPath = Path.Combine(dirSectionData, _sJH + "\\left");
                    else _fileLogScrPath = Path.Combine(dirSectionData, _sJH + "\\right");
                    cIOWellSection.delLog(_fileLogScrPath, sSelectedLogName);
                }
                foreach (TreeNode wellNote in tvWellSectionCollection.Nodes)
                {
                    TreeNode tnRet = null;
                    foreach (TreeNode tn in wellNote.Nodes)
                    {
                        tnRet = FindNode(tn, sSelectedLogName);
                        if (tnRet != null) { tnRet.Remove(); break; }
                    }
                }

                tvWellSectionCollection.ExpandAll();
            }

        }

      


    }
}
