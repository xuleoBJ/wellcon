using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DOGPlatform.XML
{
    class cXMLLayerMapGeoproperty : cXMLLayerMapBase
    {
        public static void delLayerGeoProperty(string filePathxmlLayerMap)
        {
            string parentNodePath = @"/LayerMapConfig";
            string _tagName = "WellGeologyProperty";
            delNodes(filePathxmlLayerMap, parentNodePath, _tagName);
        }
        public static void addLayerGeoProperty2XML(string filePathxmlLayerMap, string sIDLayer, List<string> listWellValueString)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig");
            XmlNode nodeLayer = xmlLayerMap.CreateElement("Layer");
            XmlAttribute nodeID = xmlLayerMap.CreateAttribute("id");
            nodeID.Value = sIDLayer;
            nodeLayer.Attributes.Append(nodeID);
            XmlAttribute nodeType = xmlLayerMap.CreateAttribute("LayerType");
            nodeType.Value = TypeLayer.LayerGeoProperty.ToString();
            nodeLayer.Attributes.Append(nodeType);

            XmlElement eleMent;
            //是否显示
            eleMent = xmlLayerMap.CreateElement("IsShow");
            eleMent.InnerText = "1";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("IsShowText");
            eleMent.InnerText = "1";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("dfscale");
            eleMent.InnerText = "2";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("textFontSize");
            eleMent.InnerText = "3";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("DX_Text"); //标注偏移
            eleMent.InnerText = "3";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("DY_Text"); //标注偏移
            eleMent.InnerText = "-3";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("data"); //标注偏移
            eleMent.InnerText = string.Join("\t", listWellValueString);
            nodeLayer.AppendChild(eleMent);

            currentNode.AppendChild(nodeLayer);
            xmlLayerMap.Save(filePathxmlLayerMap);
        } 

        public static void setLayerGeoglogyProperty_Dxoffset(string filePathxmlLayerMap, int iOffset)
        {
            XDocument xmlLayerMap = XDocument.Load(filePathxmlLayerMap);
            xmlLayerMap.Element("LayerMapConfig").Element("WellLayerGeologyPropery").Element("DX_Text").Value = iOffset.ToString("0");
            xmlLayerMap.Save(filePathxmlLayerMap);
        }
        public static void setLayerGeoglogyProperty_Dyoffset(string filePathxmlLayerMap, int iOffset)
        {
            XDocument xmlLayerMap = XDocument.Load(filePathxmlLayerMap);
            xmlLayerMap.Element("LayerMapConfig").Element("WellLayerGeologyPropery").Element("DY_Text").Value = iOffset.ToString("0");
            xmlLayerMap.Save(filePathxmlLayerMap);
        }
        public static void setLayerGeoglogyPropertyTextsize(string xmlFilePath, int iSize)
        {
            XDocument xmlLayerMap = XDocument.Load(xmlFilePath);
            xmlLayerMap.Element("LayerMapConfig").Element("WellLayerGeologyPropery").Element("fontSize").Value = iSize.ToString();
            xmlLayerMap.Save(xmlFilePath);
        }
        public static void setStaticDataVScale(string filePathxmlLayerMap, float fVScale)
        {
            XDocument xmlLayerMap = XDocument.Load(filePathxmlLayerMap);
            xmlLayerMap.Element("LayerMapConfig").Element("StaticData").Element("dfscale").Value = fVScale.ToString("0.0");
            xmlLayerMap.Save(filePathxmlLayerMap);
        }
    }
}
