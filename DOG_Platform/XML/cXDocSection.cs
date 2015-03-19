using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using System.IO;
using System.Xml;
namespace DOGPlatform.XML
{
    class cXDocSection
    {
    
        public static void generateSectionCssXML()
        {
            if (File.Exists(cProjectManager.xmlSectionCSS))
            {
                File.Delete(cProjectManager.xmlSectionCSS);
            }
            cXDocSection.generateXmlFile(cProjectManager.xmlSectionCSS);
        }

        public static void generateXmlFile( string xmlPathSectionConfig)
        {
            try
            {
                //定义一个XDocument结构
                XDocument XDoc =
                    new XDocument(
                            new XElement("SectionMap", new XAttribute("id", "id#SectionCss"),
                                     new XElement("Scale",
                                                     new XElement("vScale", "1"),
                                                     new XElement("hScale", "1")
                                                ),
                                        new XElement("WellCone", new XAttribute("id", "id#WellCone"),
                                                                new XElement("coneWidth", "3"),
                                                                new XElement("coneColor", "black"),
                                                                new XElement("radisHeadCircle", "4"),
                                                                new XElement("JHFontSize", "10"),
                                                                new XElement("JHFontColor", "red"),
                                                                new XElement("tickTextFontSize", "5"),
                                                                new XElement("tickTextFontColor", "black"),
                                                                new XElement("MainScale", "10")
                                                )
                            )
                );
                XDoc.Save(xmlPathSectionConfig);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        public static void addTrack(int iTrackType, int iTrackWidth)
        {

        }

        public static void addTrackText(string xmlFilePath, string id,int iTrackWidth)
        {
            XDocument xDoc = XDocument.Load(xmlFilePath);
            xDoc.Element("SectionMap").Add(cXETrackText.trackText(id, iTrackWidth));
            xDoc.Save(xmlFilePath);
        }

        public static void addTrackLog(string xmlFilePath, string id, int iTrackWidth, int iLeftorRight, string sLogName, double fLeftValue, double fRightValue, string sColor)
        {
            XDocument xDoc = XDocument.Load(xmlFilePath);
            xDoc.Element("SectionMap").Add(cXETrackLog.trackLog(id, iTrackWidth,  iLeftorRight,sLogName, fLeftValue, fRightValue, sColor));
            xDoc.Save(xmlFilePath);
        }

        public static void addTrackPerfoation(string xmlFilePath, string id, int iTrackWidth)
        {
            XDocument xDoc = XDocument.Load(xmlFilePath);
            xDoc.Element("SectionMap").Add(cXETrackPerforation.trackPerfoation(id, iTrackWidth));
            xDoc.Save(xmlFilePath);
        }


        public static void addTrackJSJL(string xmlFilePath, string id, int iTrackWidth)
        {
            XDocument xDoc = XDocument.Load(xmlFilePath);
            xDoc.Element("SectionMap").Add(cXETrackJSJL.trackJSJL(id, iTrackWidth));
            xDoc.Save(xmlFilePath);
        }


        public static void addTrackLayer(string xmlFilePath, string id, int iTrackWidth)
        {
            XDocument xDoc = XDocument.Load(xmlFilePath);
            xDoc.Element("SectionMap").Add(cXETrackLayer.trackLayer(id, iTrackWidth));
            xDoc.Save(xmlFilePath);

        }

        public static void addWell(string sJH, ItemWellHead WellHeadItem, float fDS1Showed, float fDS2Showed, string xmlPathSectionConfig)
        {
            XElement XSectionRoot = XElement.Load(xmlPathSectionConfig);
            XElement XNode = XSectionRoot.Element("WellCollection");
            XElement newNode = new XElement("Well", new XAttribute("id", sJH),
                           new XElement("X", WellHeadItem.dbX.ToString()),
                  new XElement("Y", WellHeadItem.dbY.ToString()),
                  new XElement("KB", WellHeadItem.fKB.ToString()),
                  new XElement("TypeWell", WellHeadItem.iWellType.ToString()),
                  new XElement("fShowedTop", fDS1Showed.ToString()),
                  new XElement("fShowedBottom", fDS2Showed.ToString())

                   );

            XNode.Add(newNode);
            XSectionRoot.Save(xmlPathSectionConfig);
        }

        public static void addWells(List<ItemWellSection> wellItems, string xmlPathSectionConfig)
        {
            XElement XSectionRoot = XElement.Load(xmlPathSectionConfig);
            XElement XNode = XSectionRoot.Element("WellCollection");
            foreach (ItemWellSection item in wellItems)
            {
                XElement newNode = new XElement("Well", new XAttribute("id", item.sJH),
                                     new XElement("X", item.dbX.ToString()),
                  new XElement("Y", item.dbY.ToString()),
                  new XElement("KB", item.fKB.ToString()),
                  new XElement("TypeWell", item.iWellType.ToString()),
                 new XElement("fShowedTop", item.fShowedDepthTop.ToString()),
                  new XElement("fShowedBottom", item.fShowedDepthBase.ToString())
                   );

                XNode.Add(newNode);
            }
            XSectionRoot.Save(xmlPathSectionConfig);
        }



      
    }


}
