using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cXMLLayerMapWellPieGraph
    {
        public static void addLayerPieGraph2XML(string filePathxmlLayerMap, string sIDLayer, float fRscale, List<string> ltStrColor, float fOpacity, List<string> ltStrRowLine)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig");
            XmlNode nodeLayer = xmlLayerMap.CreateElement("Layer");
            XmlAttribute nodeID = xmlLayerMap.CreateAttribute("id");
            nodeID.Value = sIDLayer;
            nodeLayer.Attributes.Append(nodeID);
            XmlAttribute nodeType = xmlLayerMap.CreateAttribute("LayerType");
            nodeType.Value = TypeLayer.LayerPieGraph.ToString();
            nodeLayer.Attributes.Append(nodeType);
            XmlElement eleMent;

            eleMent = xmlLayerMap.CreateElement("r");
            eleMent.InnerText = "3";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("sColor");
            eleMent.InnerText = string.Join("\t", ltStrColor);
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("fill-opacity");
            eleMent.InnerText = fOpacity.ToString();
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("fScaleR");
            eleMent.InnerText = fRscale.ToString();
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("textFontSize");
            eleMent.InnerText = "5";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("DX_Text"); //标注偏移
            eleMent.InnerText = "3";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("DY_Text"); //标注偏移
            eleMent.InnerText = "-3";
            nodeLayer.AppendChild(eleMent);

            eleMent = xmlLayerMap.CreateElement("data");
            for (int i = 0; i < ltStrRowLine.Count; i++)
            {
                XmlElement itemdata = xmlLayerMap.CreateElement("item");
                itemdata.InnerText = string.Join("\t", ltStrRowLine[i]);
                eleMent.AppendChild(itemdata);
            }
            nodeLayer.AppendChild(eleMent);

            currentNode.AppendChild(nodeLayer);
            xmlLayerMap.Save(filePathxmlLayerMap);
        }
    }
}
