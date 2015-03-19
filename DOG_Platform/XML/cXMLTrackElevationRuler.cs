using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DOGPlatform.XML
{
    class cXMLTrackElevationRuler
    {
        public static void updateElevationRuler(int iElevationTop, int iElevationBottom, int iScale, string xmlPathSectionConfig)
        {
            XDocument sectionMapXML = XDocument.Load(xmlPathSectionConfig);
            sectionMapXML.Element("SectionMap").Element("ElevationRuler").Element("topElevationDepth").Value = iElevationTop.ToString("0");
            sectionMapXML.Element("SectionMap").Element("ElevationRuler").Element("bottomElevationDepth").Value = iElevationBottom.ToString("0");
            sectionMapXML.Element("SectionMap").Element("ElevationRuler").Element("MainScale").Value = iScale.ToString("0");
            sectionMapXML.Save(xmlPathSectionConfig);
        }
    }
}
