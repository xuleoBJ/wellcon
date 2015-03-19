using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Forms;

namespace DOGPlatform.XML
{
    class cXMLProject : cXMLbase
    {
        public static void creatConfigXML(string xmlConfig)
        {
            try
            {
                //定义一个XDocument结构
                XDocument XDoc = new XDocument(
                new XElement("Config",
                    new XElement("JSJLCode",
                    new XElement("油层", "1"),
                    new XElement("水层", "2"),
                    new XElement("气层", "3"),
                    new XElement("干层", "4"),
                    new XElement("油气层", "5"),
                   new XElement("油水层", "6"),
                  new XElement("气水层", "7"),
                  new XElement("差油层", "8"),
                 new XElement("差气层", "9"),
                 new XElement("差油气层", "10"),
                 new XElement("煤层", "12"),
                 new XElement("可疑层", "13")
                ),

                new XElement("WellTypeCode",
                    new XElement("undefined", "0"),
                    new XElement("propose", "1"),
                    new XElement("dry", "2"),
                    new XElement("oil", "3"),
                   new XElement("minorOil", "4"),
                   new XElement("MinorGas", "6"),
                   new XElement("Platform", "8"),
                    new XElement("Injectwater", "15"),
                    new XElement("gas", "5"),
                new XElement("油井", "3"),
                new XElement("水井", "15"),
                new XElement("气井", "5")

                ),
                new XElement("LithoCode",
                new XElement("砂岩", "1"),
                new XElement("泥岩", "2"),
                new XElement("灰岩", "3"),
                new XElement("粗砂岩", "4"),
                 new XElement("细砂岩", "5"),
                 new XElement("粉砂岩", "6")
                ),
                 new XElement("ColorTable", new XAttribute("id", "idColorTable"),
                 new XElement("color0", "#FFFF66"),
                new XElement("color1", "#FF99FF"),
                new XElement("color2", "#FFE4C4"),
                new XElement("color3", "#FFEBCD"),
                new XElement("color4", "#F5DEB3"),
                new XElement("color5", "#FF8C00"),
                new XElement("color6", "#FFFACD"),
                new XElement("color7", " 	#FFE4B5"),
                new XElement("color8", "#FFDAB9"),
                new XElement("color9", "#FF83FA"),
                new XElement("color10", "#FF8C00"),
                new XElement("color11", "#FF6EB4"),
                new XElement("color12", "#FF7F50"),
                new XElement("color13", "#F7F7F7"),
                new XElement("color14", "#F5DEB3"),
                new XElement("color15", "#F0FFF0"),
                new XElement("color16", "#EEEEE0"),
                new XElement("color17", "#EEE685"),
                new XElement("color18", "#EEB422"),
                new XElement("color19", "#F2F2F2"),
                new XElement("color20", "#FFA07A"),
                new XElement("color21", "#7CFC00"),
                new XElement("color22", "#ADFF2F"),
                new XElement("color23", "#AFEEEE "),
                new XElement("color24", "#87CEEB")

                ),
                  new XElement("ColorTableYellow", new XAttribute("id", "idColorTableYellow"),
                new XElement("color1", "#FFF68F"),
                new XElement("color2", "#FFEFD5"),
                new XElement("color3", "#FFE4E1"),
                new XElement("color4", "#FFDEAD"),
                new XElement("color5", "#FFC1C1"),
                new XElement("color6", "#FFD700"),
                new XElement("color7", "#FFBBFF"),
                new XElement("color8", "#FFAEB9"),
                new XElement("color9", "#FF83FA"),
                new XElement("color10", "#FFE1FF")
                ),
                 new XElement("ColorTableBlue", new XAttribute("id", "idColorTableBlue"),
                new XElement("color1", "#FCFCFC"),
                new XElement("color2", "#FAFAD2"),
                new XElement("color3", "#F7F7F7"),
                new XElement("color4", "#F5DEB3"),
                new XElement("color5", "#F0FFF0"),
                new XElement("color6", "#EEEEE0"),
                new XElement("color7", "#EEE685"),
                new XElement("color8", "#EEB422"),
                new XElement("color9", "#F2F2F2"),
                new XElement("color10", "#E0FFFF")
                )
                )
                );
                XDoc.Save(xmlConfig);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        public static void creatProjectInforXML(string xmlPathProject)
        {
            try
            {
                //定义一个XDocument结构
                XDocument XDoc = new XDocument(
                new XElement("Project",
                new XElement("ProjectInfor",
                new XElement("Author", "xuleo"),
                new XElement("CreatedTime", DateTime.Now.ToShortDateString()),
                new XElement("comment", "comment")
                ),
                new XElement("ProjectJH"
                ),
                new XElement("ProjectLogSeriers"
                ),
                   new XElement("ProductYM"
                ),
                new XElement("WorkFlow",
                new XElement("ReadData", "1"),
                new XElement("DataVerify", "1"),
                new XElement("comment", "comment")
                )

                )
                );
                XDoc.Save(xmlPathProject);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void setProjectRefPointNode() //更新参考点
        {   
            setNodeInnerText(cProjectManager.xmlProject, "/Project/refY", cProjectData.dfMapYrealRefer.ToString());
            setNodeInnerText(cProjectManager.xmlProject, "/Project/refX", cProjectData.dfMapXrealRefer.ToString());
            setNodeInnerText(cProjectManager.xmlProject, "/Project/scale", cProjectData.dfMapScale.ToString("0.000"));
        }
        public static void getProjectRefPointNode() //更新参考点
        {
            double.TryParse(getNodeInnerText(cProjectManager.xmlProject, "/Project/refY"), out  cProjectData.dfMapYrealRefer);
            double.TryParse(getNodeInnerText(cProjectManager.xmlProject, "/Project/refX"), out  cProjectData.dfMapXrealRefer);
            double.TryParse(getNodeInnerText(cProjectManager.xmlProject, "/Project/scale"), out  cProjectData.dfMapScale); 
        }
        public static void setProjectJHNode() 
        {
            string sPath ="/Project/ProjectJH";
            string _data = string.Join(" ", cProjectData.ltStrProjectJH);
            setNodeInnerText(cProjectManager.xmlProject, sPath, _data);
        }
        public static void getLtStrJHFromNode()
        {
            string sPath = "/Project/ProjectJH";
            cProjectData.ltStrProjectJH = splitNodeInnerText(cProjectManager.xmlProject, sPath);
        }

        public static void delLogNameNode(string xmlDoc)
        {
            string parentNodePath = @"/LayerMapConfig/HorizonalWell";
            string _tagName = "WellIntervel";
            delNodes(xmlDoc, parentNodePath, _tagName);
        }

        public static void setProjectLogSeriersNode()
        {
             string sPath="/Project/ProjectLogSeriers";
              string _data = string.Join(" ", cProjectData.ltStrLogSeriers);
              setNodeInnerText(cProjectManager.xmlProject, sPath, _data);
        }

        public static void setProjectYMNode()
        {
              string sPath = "/Project/ProductYM";
              string _data = string.Join(" ", cProjectData.ltStrProjectYM); 
              setNodeInnerText(cProjectManager.xmlProject, sPath, _data);
        }
        
       
        public static void getLtStrLogSeriersFromNode()
        {
            string sPath="/Project/ProjectLogSeriers";
            cProjectData.ltStrLogSeriers = splitNodeInnerText(cProjectManager.xmlProject, sPath);
        }
        public static void getProjectYMFromNode()
        {
            string sPath = "/Project/ProductYM";
            cProjectData.ltStrProjectYM = splitNodeInnerText(cProjectManager.xmlProject, sPath);
        }
        
    }
}
