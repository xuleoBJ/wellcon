using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace DOGPlatform.SVG
{
    class cSVGSectionTrackElevationRuler : cSVGSectionTrack
    {
        public cSVGSectionTrackElevationRuler() 
        {
        
        }
        public XmlElement gElevationRuler(int ElevationDepthTop, int ElevationDepthBase, int m_tickInveral_main)
        {
            int iWidth = 40;
            XmlElement gElevationRuler = svgDoc.CreateElement("g");
            gElevationRuler.SetAttribute("id", "海拔尺");

            //加主轴
            XmlElement gLine = svgDoc.CreateElement("line");
            gLine.SetAttribute("x1", "0");
            gLine.SetAttribute("y1", (-ElevationDepthBase).ToString());
            gLine.SetAttribute("x2", "0");
            gLine.SetAttribute("y2", (-ElevationDepthTop).ToString());
            gLine.SetAttribute("stroke", "black");
            gLine.SetAttribute("stroke-width", "0.5");
            gElevationRuler.AppendChild(gLine);
            //加方框
            XmlElement gRect = svgDoc.CreateElement("rect");
            gRect.SetAttribute("x", "0");
            gRect.SetAttribute("y", (-ElevationDepthTop).ToString());
            gRect.SetAttribute("width", iWidth.ToString());
            gRect.SetAttribute("height", (ElevationDepthTop-ElevationDepthBase).ToString());
            gRect.SetAttribute("style", "stroke-width:0.5");
            gRect.SetAttribute("stroke", "black");
            gRect.SetAttribute("fill", "none");
            gElevationRuler.AppendChild(gRect);
            //当前深度
            int iCurrentDepth = (Convert.ToInt16(ElevationDepthBase) / m_tickInveral_main) * m_tickInveral_main;
            while (iCurrentDepth <= ElevationDepthTop)
            {
                XmlElement gDepthTick = svgDoc.CreateElement("path");
                gDepthTick.SetAttribute("stroke-width", "1");
                string d = "M " + iWidth.ToString()+" " + (-iCurrentDepth).ToString() + " h -8 ";
                if  (iCurrentDepth % m_tickInveral_main != 0)
                {
                    d = "M" + iWidth.ToString() + " " + (-iCurrentDepth).ToString() + " h -4 "; 
                }
                gDepthTick.SetAttribute("stroke", "black");
                gDepthTick.SetAttribute("d", d);
                gElevationRuler.AppendChild(gDepthTick);

                if (iCurrentDepth % m_tickInveral_main == 0)
                {
                    XmlElement gTickText = svgDoc.CreateElement("text");
                    gTickText.SetAttribute("x", "4");
                    gTickText.SetAttribute("y", (-iCurrentDepth+4).ToString());
                    gTickText.SetAttribute("fill", "black");
                    gTickText.SetAttribute("font-size", "12");
                    gTickText.SetAttribute("strole-width", "0.5");
                    gTickText.InnerText = iCurrentDepth.ToString();
                    gElevationRuler.AppendChild(gTickText);
                }
                iCurrentDepth = iCurrentDepth + m_tickInveral_main / 5; 

            }

            return gElevationRuler;

        }
    }
}
