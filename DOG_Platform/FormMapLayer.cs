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
using System.Xml;
using System.Xml.Linq;
using System.IO;
using DOGPlatform.XML;
using DOGPlatform.SVG;

namespace DOGPlatform
{
    ///流程设计：通过界面选择形成Layer配置样式及数据， 最后解析器解析xml形成图形。最好能在数据中藏一下原数据文件的位置
    /// 保存小层的静态井位，其它位置及数据位置信息都从这个表中得到。
    /// 原始数据主要是从 静态字典表和动态字典表提取，这两个表的分析计算非常重要。
    /// 
    /// 先准备数据，每个图层数据文件一个文件，便于增加和删除图层，成图，每次成图，重绘即可，图层的样式问题，可以再考虑，先把数据理清楚，不存XML，也不          便于检查错误，先数据，后解析转换坐标，成图。
    /// 每个面板勾选，然后对应的文件夹内对数据文件进行相应的处理
    /// 逻辑顺序：1 选择顶部小层名 2 选择底部显示小层名 3 选择时间（主要考虑不同时间井型的变化）
    /// 根据 小层名 筛选井号 存入listWellsMaper,显示时 应该显示选择层段顶的井位（或者提供可选），需要考虑斜井及断失的情况
    /// 
    /// 井确定后，可以叠加 不同的数据 例如 井点的属性啊，断层信息等。
    /// ！！！！xml文件名必须和SVG一致，要改一起改，这样可以保持图层修改叠加等。
    public partial class FormMapLayer : Form
    {
        //define地质静态井,存所有的井位，动态井位或者其它井位-位置数据都从这里获得
        List<ItemWellMapPosition> listWellsStatic = new List<ItemWellMapPosition>();
        //定义小层数据表链表，存储筛选当前的小层数据
        List<ItemDicLayerDataStatic> listLayersDataCurrentLayerStatic = new List<ItemDicLayerDataStatic>();
        //定义小层数据表链表，存储筛选当前层当前年月的小层动态数据
        List<ItemDicLayerDataDynamic> listLayersDataCurrentLayerDynamic = new List<ItemDicLayerDataDynamic>(); 

        List<string> ltStrSelectedLayers = new List<string>();
        string sSelectLayer;
        string sSelectYM = DateTime.Now.ToString("yyyyMM");

        int PageWidth = 2000;
        int PageHeight = 1500;
        string sUnit = "px"; 

        //首先初始化一个配置文件，然后具体到小层的时候，再复制这个文件与原文件形成配套配置文件，每次初始化的时候晴空

        string filePathXMLcurrentLayer; 
        public FormMapLayer()
        {
            InitializeComponent();
            InitFormLayerMap();
        }

        private void InitFormLayerMap()
        {
            if (cProjectData.ltStrProjectXCM.Count > 0) sSelectLayer = cProjectData.ltStrProjectXCM[0];
            List<string> ltStrStaticDataChoise = new List<string>();
            ltStrStaticDataChoise.Add("砂厚");
            ltStrStaticDataChoise.Add("有效厚度");
            ltStrStaticDataChoise.Add("孔隙度");
            ltStrStaticDataChoise.Add("渗透率");
            ltStrStaticDataChoise.Add("饱和度");
            cbbSelectedXCM.DataSource = cProjectData.ltStrProjectXCM;
            cbbSelectedYM.DataSource = cProjectData.ltStrProjectYM;
            cPublicMethodForm.inialComboBox(cbbUnit, new List<string>(new string[] { "px", "pt", "mm", "pc", "cm", "in" }));
            this.nUDrefX.Value = decimal.Parse(cProjectData.dfMapXrealRefer.ToString());
            this.nUDrefY.Value = decimal.Parse(cProjectData.dfMapYrealRefer.ToString());
            initialCbbScale();
            this.cbbScale.Text =(1000.0 / cProjectData.dfMapScale).ToString("0");
            checkCurrentXMLfile();
            initialDataFromXMLfile();
        }
        //初始化控件当新建工程或者打开工程时
        void initialCbbScale()
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
            cbbScale.Items.Clear();
            foreach (string sItem in listScale) cbbScale.Items.Add(sItem);
        }
        private void cbbSelectedXCM_SelectedIndexChanged(object sender, EventArgs e)
        {
            sSelectLayer = cbbSelectedXCM.SelectedItem.ToString(); 
        }


        void addLayerWellProperty2Xml() 
        {
            List<string> ltValue = listLayersDataCurrentLayerStatic.Select(p => p.sJH + "\t" + p.dbX + "\t" + p.dbY + "\t" + p.fDCHD.ToString("0.0") 
                + "\t" + p.fSH.ToString() + "\t" + p.fYXHD.ToString()+"\t"+p.fSTL.ToString()).ToList() ;
            cXMLLayerMapGeoproperty.addLayerGeoProperty2XML(filePathXMLcurrentLayer,"geoLayer", ltValue); 
        }

        void addHorizonal() 
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(this.filePathXMLcurrentLayer);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig/HorizonalWell");
            XmlNodeList listIntervel = currentNode.SelectNodes("WellIntervel");

            for (int i = 0; i < listIntervel.Count; i++)
            {
                string _sInnerText = listIntervel[i].InnerText;
                string[] splitInnerText = _sInnerText.Split();
                string _sJH = splitInnerText[0];
                int _iWellType = int.Parse(splitInnerText[1]);

                Point pWellHead = new Point(int.Parse(splitInnerText[2]), int.Parse(splitInnerText[3]));
                Point pA = new Point(int.Parse(splitInnerText[4]), int.Parse(splitInnerText[5]));
                Point pB = new Point(int.Parse(splitInnerText[6]), int.Parse(splitInnerText[7]));
            }
        }

        void generateSVGfilemapByConfigxml()
        {
        
            if (cbbUnit.SelectedIndex >= 0) sUnit = cbbUnit.SelectedItem.ToString();
            //注意偏移量,偏移主要是为了好看 如果不偏移的话 就会绘到角落上,这时的偏移是整个偏移 后面的不用偏移了，相对偏移0，0
            int idx = 50;
            int idy = 50;
            cSVGDocLayerMap svgLayerMap = new cSVGDocLayerMap(filePathXMLcurrentLayer, PageWidth, PageHeight, idx, idy, sUnit);
            //add title 
            string sTitle = Path.GetFileNameWithoutExtension(filePathXMLcurrentLayer);
            svgLayerMap.addSVGTitle(sTitle, 50, 20);
            XmlElement returnElemment;
            //svg文件和XML对应的问题还要思考一下
            string filePathSVGLayerMap = Path.Combine(cProjectManager.dirPathMap, sTitle + ".svg");

            //这块需要处理覆盖问题。
            if (File.Exists(filePathSVGLayerMap)) File.Delete(filePathSVGLayerMap);

            //如果顶层面断层数据不为空的话 应该加上断层
            //读取当前顶层的断层数据
            string _topLayer = cbbSelectedXCM.SelectedItem.ToString();
            List<ItemFaultLine> listFaultLine = cIOinputLayerSerier.readInputFaultFile(_topLayer);
            foreach (ItemFaultLine line in listFaultLine)
            {
                returnElemment = svgLayerMap.gFaultline(line.ltPoints, "red", 2);
                svgLayerMap.addgElement2LayerBase(returnElemment);
            }

            //解析当前的XML配置文件，根据每个Layer标签的LayerType生成id为层名的图层，添加到svgLayer中去

            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathXMLcurrentLayer);

            XmlNode xnBaseLayer = xmlLayerMap.SelectSingleNode("/LayerMapConfig/BaseLayer");
            returnElemment = svgLayerMap.gWellsPositionFromXML(xnBaseLayer,"井");


            svgLayerMap.addgElement2LayerBase(returnElemment);
            XmlNodeList xnList = xmlLayerMap.SelectNodes("/LayerMapConfig/Layer");
            //或许Layer标签的节点
            foreach (XmlNode xn in xnList) 
            {
                string sID = xn.Attributes["id"].Value;

                //建立新层
                XmlElement gXMLLayer = svgLayerMap.gLayerElement(sID);
                svgLayerMap.addgLayer(gXMLLayer);
                //井位图层
                if (xn.Attributes["LayerType"].Value ==TypeLayer.LayerPosition.ToString())
                    returnElemment = svgLayerMap.gWellsPositionFromXML(xn,sID);
                //地质熟悉数据图层
                if (xn.Attributes["LayerType"].Value == TypeLayer.LayerGeoProperty.ToString())
                    returnElemment = svgLayerMap.gLayerWellsGeologyPropertyFromXML(xn, sID);
                //水平井图层
                if (xn.Attributes["LayerType"].Value == TypeLayer.LayerHorizonalInterval.ToString())
                    returnElemment = svgLayerMap.gHorizonalWellIntervelFromXML(xn, sID);
                //日产
                if (xn.Attributes["LayerType"].Value == TypeLayer.LayerDailyProduct.ToString())
                    returnElemment = svgLayerMap.gDailyProductFromXML(xn, sID, listWellsStatic, listLayersDataCurrentLayerDynamic);
                //累产饼图图层
                if (xn.Attributes["LayerType"].Value == TypeLayer.LayerSumProduct.ToString())
                    returnElemment = svgLayerMap.gSumProductFromXML(xn, sID, listWellsStatic, listLayersDataCurrentLayerDynamic);
                //井位柱状图图层
                if (xn.Attributes["LayerType"].Value == TypeLayer.LayerBarGraph.ToString())
                    returnElemment = svgLayerMap.gWellBarGraphFromXML(xn, sID, listWellsStatic);
                //井位饼图图层
                if (xn.Attributes["LayerType"].Value == TypeLayer.LayerPieGraph.ToString())
                    returnElemment = svgLayerMap.gWellPieGraphFromXML(xn, sID, listWellsStatic); 
                //新层加内容
                svgLayerMap.addgElement2Layer(gXMLLayer, returnElemment);
            }
         
            if (this.cbxScaleRulerShowed.Checked == true)
            {
                XmlElement gLayerScaleRuler = svgLayerMap.gLayerElement("比例尺");
                svgLayerMap.addgLayer(gLayerScaleRuler);
                returnElemment = svgLayerMap.gScaleRuler(0, 0);
                svgLayerMap.addgElement2Layer(gLayerScaleRuler, returnElemment, 100, 100);
            }

            if (this.cbxMapFrame.Checked == true)
            {
                returnElemment = svgLayerMap.gMapFrame(this.cbxGrid.Checked);
                svgLayerMap.addgElement2LayerBase(returnElemment);
            }

            if (this.cbxCompassShowed.Checked == true)
            {
                XmlElement gLayerCompass = svgLayerMap.gLayerElement("指南针");
                svgLayerMap.addgLayer(gLayerCompass);
                svgLayerMap.addgElement2Layer(gLayerCompass, svgLayerMap.gCompass(300, 100));
            }

            svgLayerMap.makeSVGfile(filePathSVGLayerMap);
            FormMain.filePathWebSVG = filePathSVGLayerMap;
            this.Close();
        }

        private void btnMakeLayerMap_Click(object sender, EventArgs e)
        {
            //用静态数据表，更新静态井位列表
            //静态、动态小层数据分别在tab面板更新。
            listWellsStatic.Clear();
            if (listLayersDataCurrentLayerStatic.Count > 0)
            {
                foreach (ItemDicLayerDataStatic item in listLayersDataCurrentLayerStatic)
                {
                    //由于可能计算小层数据表后又对井做修改 所以 必须判断小层数据表的井是否在项目井范围内
                    if (cProjectData.ltStrProjectJH.IndexOf(item.sJH) >= 0)
                    {
                        ItemWellMapPosition wellMapLayer = new ItemWellMapPosition(item);
                        listWellsStatic.Add(wellMapLayer);
                    }
                }
            }
            addLayerBase2XML(listWellsStatic); 
            generateSVGfilemapByConfigxml();
        }

        private void btnWellNameFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                this.lbl_JMFont.ForeColor = fontDialog.Color;
                this.lbl_JMFont.Font = fontDialog.Font;
            }
        }

        private void nUDWellCircle_R_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapBase.setRdiusValueWellCircle(filePathXMLcurrentLayer, Convert.ToInt16(nUDWellCircle_R.Value));
        }

        private void nUDJHtext_DX_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapBase.setJH_Dxoffset(filePathXMLcurrentLayer, Convert.ToInt16(nUDJHtext_DX.Value));
        }

        private void nUDJHFontSize_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapBase.setJHsize(filePathXMLcurrentLayer, Convert.ToInt16(nUDJHFontSize.Value));
        }

        private void nUDFaultLineWidth_ValueChanged(object sender, EventArgs e)
        {
            XDocument filePathXMLcurrentLayer = XDocument.Load(cProjectManager.xmlConfigLayerMap);
            filePathXMLcurrentLayer.Element("LayerMapConfig").Element("FaultLine").Element("lineWidth").Value = nUDFaultLineWidth.Value.ToString("0");
            filePathXMLcurrentLayer.Save(cProjectManager.xmlConfigLayerMap);
        }


        private void FormMapLayer_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void nUDCirleLineWidth_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapBase.setLineWidthWellCircle(filePathXMLcurrentLayer, Convert.ToInt16(this.nUDCirleLineWidth.Value));
        }

        void addLayerBase2XML(List<ItemWellMapPosition> listStaticWellPos)
        {
            cXMLLayerMapBase.delBaseLayer(filePathXMLcurrentLayer);
            //xml文件中加入BaseLayer绘图信息
            cXMLLayerMapBase.addBaseLayer2XML(filePathXMLcurrentLayer, listStaticWellPos);
            checkCurrentXMLfile();
        }

        void initialDataFromXMLfile() 
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathXMLcurrentLayer);
            XmlNode xnLayer = xmlLayerMap.SelectSingleNode("/LayerMapConfig/BaseInfor/XCM");
            if(xnLayer!=null) sSelectLayer = xnLayer.InnerText;
            XmlNode xnYM = xmlLayerMap.SelectSingleNode("/LayerMapConfig/BaseInfor/YM");
             if (xnYM != null) sSelectYM = xnYM.InnerText;

            //如果存在 静态数据节点 赋值
            
            XmlNode xnStatic = xmlLayerMap.SelectSingleNode("/LayerMapConfig/DataDicStatic");
            if (xnStatic!=null && xnStatic.Attributes["XCM"].Value == sSelectLayer) 
            {
                XmlNodeList xnlist = xnStatic.SelectNodes("data/item");
                //解析进List
                listLayersDataCurrentLayerStatic.Clear();
                foreach (XmlNode xn in xnlist)
                {
                    listLayersDataCurrentLayerStatic.Add(ItemDicLayerDataStatic.parseLine(xn.InnerText));
                }
            }

            //如果存在 存在动态数据节点 赋值
            XmlNode xnDynamic = xmlLayerMap.SelectSingleNode("/LayerMapConfig/DataDicDynamic");
            if (xnDynamic != null && xnDynamic.Attributes["XCM"].Value == sSelectLayer && xnDynamic.Attributes["YM"].Value == sSelectYM)
            {
                XmlNodeList xnlist = xnStatic.SelectNodes("item");
                //解析进List
                listLayersDataCurrentLayerDynamic.Clear();
                foreach (XmlNode xn in xnlist)
                {
                    listLayersDataCurrentLayerDynamic.Add(ItemDicLayerDataDynamic.parseLine(xn.InnerText));
                }
            }
            
        }

        void checkCurrentXMLfile()
        {
            //当前选中的地质层位的选中的年月的xml的文件路径，未选年月用当今年月
            filePathXMLcurrentLayer = Path.Combine(cProjectManager.dirPathLayerDir, sSelectLayer, sSelectLayer+"_"+sSelectYM + "LayerConfig.xml");
            //不存在，就建立年月的配置文件，
            //初始化小层属性配置文件，放在小层文件夹下，可以在小层文件夹内保留一个基本的配置文件格式，这样就是每个层的配置文件都是独立的，而且互不干扰，且可保留。
            if (!File.Exists(filePathXMLcurrentLayer))
                cXMLLayerMapBase.creatLayerMapConfigXML(filePathXMLcurrentLayer, sSelectLayer,sSelectYM,this.PageWidth, this.PageHeight);
            else  //存在解析图层
            {
                XmlDocument xmlLayerMap = new XmlDocument();
                xmlLayerMap.Load(filePathXMLcurrentLayer);

                XmlNodeList xnList = xmlLayerMap.SelectNodes("/LayerMapConfig/Layer");
                this.lbxLayer.Items.Clear();
                //或许Layer标签的节点
                foreach (XmlNode xn in xnList)
                {
                    string sID = xn.Attributes["id"].Value;
                    lbxLayer.Items.Add(sID);
                }
            }
        }

        //所有的孤立数据建立图层
        private void btnAddBaseLayer_Click(object sender, EventArgs e)
        {
            sSelectLayer = cbbSelectedXCM.SelectedItem.ToString();
            tbxTitle.Text = cbbSelectedXCM.Text + "小层平面图" + sSelectYM; 
         
            checkCurrentXMLfile(); 
            //从小层数据表获得数据
            listLayersDataCurrentLayerStatic = cIODicLayerDataStatic.readDicLayerData2struct().FindAll(p => p.sXCM == sSelectLayer);
            cXMLLayerMapStatic.addWellStaticDataDic2XML(filePathXMLcurrentLayer, sSelectLayer, listLayersDataCurrentLayerStatic);
        }

    


        private void cbbSelectedYM_SelectedIndexChanged(object sender, EventArgs e)
        {
            sSelectYM = cbbSelectedYM.SelectedItem.ToString();
        }


        private void lbxLayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (lbxLayer.SelectedIndex != -1)
                {
                    cXMLLayerMapBase.delLayerFromXML(filePathXMLcurrentLayer, lbxLayer.SelectedItem.ToString());
                    lbxLayer.Items.RemoveAt(lbxLayer.SelectedIndex);
                }
            }
        }

        private void 粘贴_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.DataGridViewCellPaste(this.dgvOuterDataLayerWellValues);
        }

        private void btnDelFromdgv_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.deleteSelectedRowInDataGridView(dgvOuterDataLayerWellValues);
        }

        private void btnAddWellValueLayer_Click(object sender, EventArgs e)
        {
            List<string> listColor = new List<string>() { "blue","red","green","#FFFF66", "#FF99FF", "#FFE4C4", "#FFEBCD", "#F5DEB3", "#FF8C00", "#FFFACD", "#FFE4B5", "#FFDAB9", "#FF83FA", "#FF8C00", "#FF6EB4", "#FF7F50", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#FFA07A", "#7CFC00", "#ADFF2F", "#AFEEEE", "#87CEEB", "#FFF68F", "#FFEFD5", "#FFE4E1", "#FFDEAD", "#FFC1C1", "#FFD700", "#FFBBFF", "#FFAEB9", "#FF83FA", "#FFE1FF", "#FCFCFC", "#FAFAD2", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#E0FFFF", "#FFFF66", "#FF99FF", "#FFE4C4", "#FFEBCD", "#F5DEB3", "#FF8C00", "#FFFACD", "#FFE4B5", "#FFDAB9", "#FF83FA", "#FF8C00", "#FF6EB4", "#FF7F50", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#FFA07A", "#7CFC00", "#ADFF2F", "#AFEEEE", "#87CEEB", "#FFF68F", "#FFEFD5", "#FFE4E1", "#FFDEAD", "#FFC1C1", "#FFD700", "#FFBBFF", "#FFAEB9", "#FF83FA", "#FFE1FF", "#FCFCFC", "#FAFAD2", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#E0FFFF", "#FFFF66", "#FF99FF", "#FFE4C4", "#FFEBCD", "#F5DEB3", "#FF8C00", "#FFFACD", "#FFE4B5", "#FFDAB9", "#FF83FA", "#FF8C00", "#FF6EB4", "#FF7F50", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#FFA07A", "#7CFC00", "#ADFF2F", "#AFEEEE", "#87CEEB", "#FFF68F", "#FFEFD5", "#FFE4E1", "#FFDEAD", "#FFC1C1", "#FFD700", "#FFBBFF", "#FFAEB9", "#FF83FA", "#FFE1FF", "#FCFCFC", "#FAFAD2", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#E0FFFF" };

            List<string> listColorUser = new List<string>(); //设置用户颜色值
            for (int col = 1; col < dgvOuterDataLayerWellValues.Columns.Count; col++)//first column is wellname so start to get from
            {
                string curColBackColor= cPublicMethodBase.getRGB(dgvOuterDataLayerWellValues.Columns[col].DefaultCellStyle.BackColor);
                if (curColBackColor != "rgb(0,0,0)") listColorUser.Add(curColBackColor);
                else 
                {
                    listColorUser.Add(listColor[col-1]);
                }
            } 
              
            List<string> listRowString = new List<string>();
            if (cPublicMethodForm.chekDataGridViewHasNullValue(dgvOuterDataLayerWellValues))
            {
                for (int rows = 0; rows < dgvOuterDataLayerWellValues.Rows.Count-1; rows++)
                {
                    List<string> ltStrCell = new List<string>();
                    for (int col = 0; col < dgvOuterDataLayerWellValues.Rows[rows].Cells.Count; col++)
                    {
                        ltStrCell.Add(dgvOuterDataLayerWellValues.Rows[rows].Cells[col].Value.ToString());
                    }
                    listRowString.Add(string.Join("\t",ltStrCell));
                }//end for read datagridview data

                string sIDLayer = "outerData";
                if (tbxDataLayerID.Text != "") sIDLayer = tbxDataLayerID.Text;
                //add bar data 2 layer
                if (rdbOuterDataLayerRect.Checked == true)
                {
                    cXMLLayerMapWellBarGraph.addLayerBarGraph2XML(filePathXMLcurrentLayer, sIDLayer , int.Parse(nUDOuterDataLayerRectWidth.Value.ToString("0")), listColorUser, float.Parse(tbxOutLayerfVscale.Text), Convert.ToSingle(nUDLayerOpcity.Value), listRowString);
                }
                if (rdbOuterDataLayerPie.Checked == true)
                    cXMLLayerMapWellPieGraph.addLayerPieGraph2XML(filePathXMLcurrentLayer, sIDLayer, float.Parse(tbxOuterDataLayerPiefR.Text), listColorUser,Convert.ToSingle(nUDLayerOpcity.Value), listRowString);
                 
            }// end if

            checkCurrentXMLfile();
        }


        private void btnOuterDataLayerAddCol_Click(object sender, EventArgs e)
        {
            this.dgvOuterDataLayerWellValues.Columns.Add("Column", "数值");
        }

        private void btnOuterDataLayerDelCol_Click(object sender, EventArgs e)
        {
            if (dgvOuterDataLayerWellValues.ColumnCount>2)
            dgvOuterDataLayerWellValues.Columns.RemoveAt(dgvOuterDataLayerWellValues.ColumnCount-1);
        }

        private void btnCellColor_Click(object sender, EventArgs e)
        {
            int idCol = dgvOuterDataLayerWellValues.SelectedCells[0].ColumnIndex;
            if (idCol >= 1)
            {
                ColorDialog colorDialog1 = new ColorDialog();
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    dgvOuterDataLayerWellValues.Columns[idCol].DefaultCellStyle.BackColor = colorDialog1.Color;
                }
            } 
        }
      
    }
}
