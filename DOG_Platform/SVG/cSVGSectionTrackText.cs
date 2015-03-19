using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGSectionTrackText : cSVGSectionTrack
    {
        //public void readTextTrack(string filepathText)
        //{
        //    ltStrJH_TextTrack.Clear();
        //    fListDS1_TextTrack.Clear();
        //    ltStrText_TextTrack.Clear();
        //    using (StreamReader sr = new StreamReader(filepathText, Encoding.UTF8))
        //    {
        //        String line;
        //        string[] split;
        //        while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
        //        {
        //            split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        //            ltStrJH_TextTrack.Add(split[0]);
        //            fListDS1_TextTrack.Add(float.Parse(split[1]));
        //            ltStrText_TextTrack.Add(split[2]);
        //        }
        //    }
        //}
        public XmlElement gTrackText(List<float> fListTVD, List<string> ltStrValue, float m_KB)
        {
            XmlElement gTextTrack = svgDoc.CreateElement("g");
            gTextTrack.SetAttribute("ID", "idTrackText");
            for (int i = 0; i < fListTVD.Count; i++)
            {
                XmlElement gTextTick = svgDoc.CreateElement("path");
                gTextTick.SetAttribute("style", "stroke:blue;stroke-width:0.5");
                string d = "M0 " + (-m_KB + fListTVD[i]).ToString() + " h 4 ";
                gTextTick.SetAttribute("d", d);
                gTextTrack.AppendChild(gTextTick);
                XmlElement textTickText = svgDoc.CreateElement("text");
                textTickText.SetAttribute("x", "0");
                textTickText.SetAttribute("y", (-m_KB + fListTVD[i]).ToString());
                textTickText.SetAttribute("fill", "blue");
                textTickText.SetAttribute("font-size", "4");
                textTickText.SetAttribute("style", "strole-width:1");
                textTickText.InnerText = ltStrValue[i];
                gTextTrack.AppendChild(textTickText);
            }

            return gTextTrack;
        }
    
    }
}
