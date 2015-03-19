using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.XML
{
    class cXMLLayerMapStatic : cXMLLayerMapBase
    {
        public static void addLayerWellPosition2XML(string filePathxmlLayerMap, string sIDLayer, List<ItemWellMapPosition> listItemWell)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig");
            XmlNode nodeLayer = xmlLayerMap.CreateElement("Layer");
            XmlAttribute nodeID = xmlLayerMap.CreateAttribute("id");
            nodeID.Value = sIDLayer;
            nodeLayer.Attributes.Append(nodeID);
            XmlAttribute nodeType = xmlLayerMap.CreateAttribute("LayerType");
            nodeType.Value = TypeLayer.LayerPosition.ToString();
            nodeLayer.Attributes.Append(nodeType);
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
            eleMent.InnerText = "1";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("fontColor");
            eleMent.InnerText ="black";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("fontSize");
            eleMent.InnerText = "10";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("DX_Text"); //标注偏移
            eleMent.InnerText = "12";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("data");
            foreach (ItemWellMapPosition item in listItemWell)
            {
                XmlElement itemdata = xmlLayerMap.CreateElement("item");
                itemdata.InnerText = ItemWellMapPosition.item2string(item);
                eleMent.AppendChild(itemdata);
            }
            nodeLayer.AppendChild(eleMent);

            currentNode.AppendChild(nodeLayer);
            xmlLayerMap.Save(filePathxmlLayerMap);
        }

        public static void addWellStaticDataDic2XML(string filePathxmlLayerMap,  string sXCM, List<ItemDicLayerDataStatic> listItemWell)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNodeList xnList = xmlLayerMap.SelectNodes("/LayerMapConfig/DataDicStatic");
            //或许Layer标签的节点
            foreach (XmlNode xn in xnList)
            {
                if ( xn.Attributes["XCM"].Value == sXCM) xn.ParentNode.RemoveChild(xn);
            }

            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig");
            XmlNode nodeLayer = xmlLayerMap.CreateElement("DataDicStatic");
            XmlAttribute nodeType = xmlLayerMap.CreateAttribute("XCM");
            nodeType.Value = sXCM;
            nodeLayer.Attributes.Append(nodeType);
            XmlElement eleMent;

            eleMent = xmlLayerMap.CreateElement("data");
            foreach (ItemDicLayerDataStatic item in listItemWell)
            {
                XmlElement itemdata = xmlLayerMap.CreateElement("item");
                itemdata.InnerText = ItemDicLayerDataStatic.item2string(item);
                eleMent.AppendChild(itemdata);
            }
            nodeLayer.AppendChild(eleMent);

            currentNode.AppendChild(nodeLayer);
            xmlLayerMap.Save(filePathxmlLayerMap);
        }
      
        public static void addStaticWellPos2XML(string filePathxmlLayerMap, string sXCM, List<ItemWellMapPosition> listItemWell)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNodeList xnList = xmlLayerMap.SelectNodes("/LayerMapConfig/DataWellPositionStatic");
            //或许Layer标签的节点
            foreach (XmlNode xn in xnList)
            {
                if (xn.Attributes["XCM"].Value == sXCM) xn.ParentNode.RemoveChild(xn);
            }

            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig");
            XmlNode nodeLayer = xmlLayerMap.CreateElement("DataWellPositionStatic");
            XmlAttribute nodeID = xmlLayerMap.CreateAttribute("XCM");
            nodeID.Value = sXCM;
            nodeLayer.Attributes.Append(nodeID);

            XmlElement eleMent;

            eleMent = xmlLayerMap.CreateElement("data");
            foreach (ItemWellMapPosition item in listItemWell)
            {
                XmlElement itemdata = xmlLayerMap.CreateElement("item");
                itemdata.InnerText = ItemWellMapPosition.item2string(item);
                eleMent.AppendChild(itemdata);
            }

            currentNode.AppendChild(nodeLayer);
            xmlLayerMap.Save(filePathxmlLayerMap);
        } 
       
    }
}
