using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Forms;

namespace DOGPlatform.SVG
{
    class cSVGBaseSection:cSVGBase
    {
        public List<string> colorList = new List<string>();

        public cSVGBaseSection(int width, int height, int iDX, int iDY)
            : base(width, height, iDX, iDY)
        {

        }
        public cSVGBaseSection(int width, int height, int iDX, int iDY, string sUnit)
            : base(width, height, iDX, iDY, sUnit)
        {

        }
        public XmlElement gScaleRuler(float m_scale)
        {
            XmlElement gScaleRuler = svgDoc.CreateElement("g");
            gScaleRuler.SetAttribute("id", "ScaleRuler");
            float _sacleUnit = Convert.ToSingle(1000.0 * m_scale);

            for (int i = 0; i < 4; i++)
            {
                XmlElement gRect = svgDoc.CreateElement("rect");
                gRect.SetAttribute("x", (_sacleUnit * 0.25 * i).ToString());
                gRect.SetAttribute("y", "0");
                gRect.SetAttribute("width", (_sacleUnit * 0.25).ToString());
                gRect.SetAttribute("height", "2");
                if (i % 2 == 0) gRect.SetAttribute("fill", "black");
                if (i % 2 == 1) gRect.SetAttribute("fill", "none");
                gRect.SetAttribute("stroke-width", "1");
                gRect.SetAttribute("stroke", "black");
                gScaleRuler.AppendChild(gRect);
            }
            for (int i = 0; i < 5; i++)
            {
                XmlElement gLine = svgDoc.CreateElement("line");
                gLine.SetAttribute("x1", (_sacleUnit * 0.25 * i).ToString());
                gLine.SetAttribute("y1", "0");
                gLine.SetAttribute("x2", (_sacleUnit * 0.25 * i).ToString());
                gLine.SetAttribute("y2", "-3");
                gLine.SetAttribute("stroke", "black");
                gLine.SetAttribute("stroke-width", "1");
                gScaleRuler.AppendChild(gLine);
            }

            for (int i = 0; i < 5; i++)
            {
                XmlElement gText = svgDoc.CreateElement("text");
                gText.SetAttribute("x", (_sacleUnit * 0.25 * i - 5).ToString());
                gText.SetAttribute("y", "-4");
                gText.SetAttribute("font-size", "8");
                gText.InnerText = (250 * i).ToString() + "m";
                gText.SetAttribute("fill", "black");
                gScaleRuler.AppendChild(gText);
            }

            return gScaleRuler;
        }
        public void addLithoSandPatternDefs()
        {
            XmlElement sandPattern = svgDoc.CreateElement("pattern");
            sandPattern.SetAttribute("id", "patternSand");
            sandPattern.SetAttribute("patternUnits", "userSpaceOnUse");
            sandPattern.SetAttribute("x", "0");
            sandPattern.SetAttribute("y", "0");
            sandPattern.SetAttribute("width", "4");
            sandPattern.SetAttribute("height", "4");
            sandPattern.SetAttribute("viewBox", "0 0 4 4");
            XmlElement gSandCircle = svgDoc.CreateElement("circle");
            gSandCircle.SetAttribute("cx", "2");
            gSandCircle.SetAttribute("cy", "2");
            gSandCircle.SetAttribute("r", "0.4");
            //gSandCircle.SetAttribute("stroke", "black");
            //gSandCircle.SetAttribute("style", "stroke-width:0.1");
            gSandCircle.SetAttribute("fill", "yellow");
            sandPattern.AppendChild(gSandCircle);
            svgDefs.AppendChild(sandPattern);
        }
        public void addLithoShalePatternDefs()
        {
            XmlElement shalePattern = svgDoc.CreateElement("pattern");
            shalePattern.SetAttribute("id", "patternShale");
            shalePattern.SetAttribute("x", "0");
            shalePattern.SetAttribute("y", "0");
            shalePattern.SetAttribute("width", "4");
            shalePattern.SetAttribute("height", "4");
            shalePattern.SetAttribute("viewBox", "0 0 4 4");
            XmlElement gPathShale = svgDoc.CreateElement("path");
            gPathShale.SetAttribute("d", "M 2 2 h2 ");
            gPathShale.SetAttribute("style", "stroke-width:0.1");
            gPathShale.SetAttribute("stroke", "Gray");
            shalePattern.AppendChild(gPathShale);
            svgDefs.AppendChild(shalePattern);

        }
        public void addLithoLimestonePatternDefs()
        {
            XmlElement limestonePattern = svgDoc.CreateElement("pattern");
            limestonePattern.SetAttribute("id", "patternLimestone");
            limestonePattern.SetAttribute("x", "0");
            limestonePattern.SetAttribute("y", "0");
            limestonePattern.SetAttribute("width", "4");
            limestonePattern.SetAttribute("height", "2");
            limestonePattern.SetAttribute("viewBox", "0 0 4 2");
            XmlElement gPathLimestone = svgDoc.CreateElement("path");
            gPathLimestone.SetAttribute("d", "M 0 0 h4 v1 h-1v1h-2v-1h2h-3v-1");
            gPathLimestone.SetAttribute("style", "stroke-width:0.1");
            gPathLimestone.SetAttribute("stroke", "Gray");
            gPathLimestone.SetAttribute("fill", "none");
            limestonePattern.AppendChild(gPathLimestone);
            svgDefs.AppendChild(limestonePattern);
        }
        public void addSandBodyPatternDefs()
        {

            XmlElement sandBodyPattern = svgDoc.CreateElement("pattern");
            sandBodyPattern.SetAttribute("id", "SandBody");
            sandBodyPattern.SetAttribute("patternUnits", "userSpaceOnUse");
            sandBodyPattern.SetAttribute("x", "0");
            sandBodyPattern.SetAttribute("y", "0");
            sandBodyPattern.SetAttribute("width", "10");
            sandBodyPattern.SetAttribute("height", "10");
            sandBodyPattern.SetAttribute("viewBox", "0 0 10 10");

            XmlElement gRect = svgDoc.CreateElement("rect");
            gRect.SetAttribute("x", "0");
            gRect.SetAttribute("y", "0");
            gRect.SetAttribute("width", "10");
            gRect.SetAttribute("height", "10");
            gRect.SetAttribute("style", "stroke:none");
            gRect.SetAttribute("fill", "yellow");
            sandBodyPattern.AppendChild(gRect);

            XmlElement gSandCircle = svgDoc.CreateElement("circle");
            gSandCircle.SetAttribute("cx", "5");
            gSandCircle.SetAttribute("cy", "5");
            gSandCircle.SetAttribute("r", "1");
            //gSandCircle.SetAttribute("stroke", "black");
            //gSandCircle.SetAttribute("style", "stroke-width:0.1");
            gSandCircle.SetAttribute("fill", "red");
            sandBodyPattern.AppendChild(gSandCircle);
            svgDefs.AppendChild(sandBodyPattern);
        }
        public XmlElement gSandBody(string d)
        {
            addSandBodyPatternDefs();
            XmlElement gSandBody = svgDoc.CreateElement("g");
            gSandBody.SetAttribute("ID", "sandBody");
            XmlElement gPath = svgDoc.CreateElement("path");
            gPath.SetAttribute("d", d);
            gPath.SetAttribute("fill", "url(#SandBody)");
            gPath.SetAttribute("stroke", "black");
            gPath.SetAttribute("stroke-width", "0.3");
            gSandBody.AppendChild(gPath);
            return gSandBody;

        }
        public void setLayerColor() //自动设置颜色带
        {
            for (int i = 0; i < cProjectData.ltStrProjectXCM.Count; i++)
            {
                int r = 255 - (i % 5) * 50;
                int g = (i % 5) * 50;
                int b = (i % 20) * 12;
                string _fillColor = "rgb(" + r.ToString() + "," + g.ToString() + "," + b.ToString() + ")";
                colorList.Add(_fillColor);
            }
        }
        
        
    }
}
