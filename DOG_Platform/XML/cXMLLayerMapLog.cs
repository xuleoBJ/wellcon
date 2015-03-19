using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DOGPlatform.XML
{
    class cXMLLayerMapLog:cXMLbase
    {
        public static void setLogFaceTrackWidth(string xmlFilePath, int iTrackWidth)
        {
            XDocument xmlLayerMap = XDocument.Load(xmlFilePath);
            xmlLayerMap.Element("LayerMapConfig").Element("LogFace").Element("trackWidth").Value = iTrackWidth.ToString("0");
            xmlLayerMap.Save(xmlFilePath);
        }
        public static void setLogFaceLineWidth(string xmlFilePath, float fLineWidth)
        {
            XDocument xmlLayerMap = XDocument.Load(xmlFilePath);
            xmlLayerMap.Element("LayerMapConfig").Element("LogFace").Element("lineWidth").Value = fLineWidth.ToString("0.0");
            xmlLayerMap.Save(cProjectManager.xmlConfigLayerMap);
        }

        public static void setLogFaceVScale(string filePathxmlLayerMap, float fVScale)
        {
            XDocument xmlLayerMap = XDocument.Load(filePathxmlLayerMap);
            xmlLayerMap.Element("LayerMapConfig").Element("LogFace").Element("fVScale").Value = fVScale.ToString("0.0");
            xmlLayerMap.Save(filePathxmlLayerMap);
        }
    }
}
