using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DOGPlatform.XML
{
    /// <summary>
    /// XMLLayerMap类及其继承主要是通过界面生成平面图的数据及样式的XML基础文件，SVG平面图解析器解析这个XML文件形成叠加图层的SVG文件，这样
    /// 成果图的扩展能力变强。
    /// </summary>
    class cXMLLayerMapBase : cXMLbase
    {
        //config mapLayer xml , 1 section is style, 2 section is data,svg or gdi parse xml in different methond,but only one source. 
        public static void creatLayerMapConfigXML(string xmlConfigLayerMap,string sXCM,string sYM,int iWidth,int iHeight)
        {
            //cProject.xmlConfigLayerMap = xmlConfigLayerMap;
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "GB2312", null);
            doc.AppendChild(dec);
            //创建一个根节点（一级）
            XmlElement root = doc.CreateElement("LayerMapConfig");
            doc.AppendChild(root);

            XmlNode node;
            XmlElement eleMent;

            node = doc.CreateElement("BaseInfor");

            eleMent = doc.CreateElement("FileInfor");
            eleMent.SetAttribute("LayerYearAndMonth", DateTime.Now.ToString());
            eleMent.InnerText = "FileName";
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("XCM");
            eleMent.InnerText = sXCM;
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("YM");
            eleMent.InnerText = sYM;
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("xRef");
            eleMent.InnerText = cProjectData.dfMapXrealRefer.ToString();
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("yRef");
            eleMent.InnerText = cProjectData.dfMapYrealRefer.ToString();
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("dfMapScale");
            eleMent.InnerText = cProjectData.dfMapScale.ToString();
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("width");
            eleMent.InnerText = iWidth.ToString();
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("height");
            eleMent.InnerText = iHeight.ToString(); 
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("r");
            eleMent.InnerText = "4";
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("rLineWidth");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("JHTextFontColor");
            eleMent.SetAttribute("value", "black");
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("JHTextFontSize");
            eleMent.InnerText = "10";
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("JHTextDX_Text"); //标注偏移
            eleMent.InnerText = "12";
            node.AppendChild(eleMent);
            //eleMent = doc.CreateElement("DY_Text"); //标注偏移
            //eleMent.SetAttribute("value", "10");
            node.AppendChild(eleMent);
            root.AppendChild(node);

            //定制Fault
            node = doc.CreateElement("FaultLine");
            //是否显示
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("color");
            eleMent.InnerText = "red";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("lineWidth");
            eleMent.InnerText = "2";
            node.AppendChild(eleMent);
            root.AppendChild(node);

            //定制比例尺
            node = doc.CreateElement("ScaleRuler");
            //是否显示
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("sacle");
            eleMent.InnerText = "1:500";
            node.AppendChild(eleMent);
            root.AppendChild(node);

            //定制指南针
            node = doc.CreateElement("Compass");
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            root.AppendChild(node);

            //定制边框
            node = doc.CreateElement("Mapframe");
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            root.AppendChild(node);

            doc.Save(xmlConfigLayerMap);
            //MessageBox.Show("设置保存。");
        }

        public static void setJHsize(string xmlFilePath, int iSize)
        {
            string nodePath = "/LayerMapConfig/JHText/fontSize";
            updateNodeValue(xmlFilePath, nodePath, iSize);
        }
        public static void setRdiusValueWellCircle(string xmlFilePath, int iR)
        {
            string nodePath = "/LayerMapConfig/WellSymbol/r";
            updateNodeValue(xmlFilePath, nodePath, iR); 
        }
        public static void setLineWidthWellCircle(string xmlFilePath, int iWidth)
        {
            string nodePath="/LayerMapConfig/WellSymbol/rLineWidth";
            updateNodeValue(xmlFilePath, nodePath, iWidth);
        }
       
        public static void setJH_Dxoffset(string xmlFilePath, int iOffset)
        {
            string nodePath="/LayerMapConfig/JHText/DX_Text";
            updateNodeValue(xmlFilePath, nodePath, iOffset);
        }

        public static void delBaseLayer(string filePathXmlLayerMap)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathXmlLayerMap);

            XmlNodeList xnList = xmlLayerMap.SelectNodes("/LayerMapConfig/BaseLayer");
            //或许Layer标签的节点
            foreach (XmlNode xn in xnList) xn.ParentNode.RemoveChild(xn);
           
            xmlLayerMap.Save(filePathXmlLayerMap);
        }

        public static void addBaseLayer2XML(string filePathXmlLayerMap, List<ItemWellMapPosition> listItemWell)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathXmlLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig");
            XmlNode nodeLayer = xmlLayerMap.CreateElement("BaseLayer");
            XmlAttribute nodeID = xmlLayerMap.CreateAttribute("id");
            nodeID.Value = "BaseLayer";
            nodeLayer.Attributes.Append(nodeID);
            XmlElement eleMent;

            //定制井位图属性
            //是否显示
            eleMent = xmlLayerMap.CreateElement("r");
            eleMent.InnerText = "4";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("rLineWidth");
            eleMent.InnerText = "1";
            nodeLayer.AppendChild(eleMent);
            //定制井数据
            //是否显示
            eleMent = xmlLayerMap.CreateElement("JHIsShow");
            eleMent.SetAttribute("value", "1");
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("fontColor");
            eleMent.SetAttribute("value", "black");
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("fontSize");
            eleMent.InnerText = "10";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("DX_Text"); //标注偏移
            eleMent.InnerText = "12";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("data"); //标注偏移
            foreach (ItemWellMapPosition item in listItemWell)
            {
                XmlElement itemdata = xmlLayerMap.CreateElement("item");
                itemdata.InnerText = ItemWellMapPosition.item2string(item);
                eleMent.AppendChild(itemdata);
            }
            nodeLayer.AppendChild(eleMent);

            currentNode.AppendChild(nodeLayer);
            xmlLayerMap.Save(filePathXmlLayerMap);
        }

        public static void delLayerFromXML(string filePathXmlLayerMap,string sID) 
        {

            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathXmlLayerMap);

            XmlNodeList xnList = xmlLayerMap.SelectNodes("/LayerMapConfig/Layer");
            //或许Layer标签的节点
            foreach (XmlNode xn in xnList)
            {
                if (xn.Attributes["id"].Value == sID) xn.ParentNode.RemoveChild(xn);
            }
            xmlLayerMap.Save(filePathXmlLayerMap);
        }
    }
}
