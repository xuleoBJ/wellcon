using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGSectionTrackLitho : cSVGSectionTrack
    {
        //public void readLithoTrack(string filepathLithoTrack)
        //{
        //    ltStrJH_LithoTrack.Clear();
        //    fListDS1_LithoTrack.Clear();
        //    fListDS2_LithoTrack.Clear();
        //    iListLithoType_LithoTrack.Clear();

        //    using (StreamReader sr = new StreamReader(filepathLithoTrack, Encoding.UTF8))
        //    {
        //        String line;
        //        string[] split;
        //        while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
        //        {
        //            split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        //            ltStrJH_LithoTrack.Add(split[0]);
        //            fListDS1_LithoTrack.Add(float.Parse(split[1]));
        //            fListDS2_LithoTrack.Add(float.Parse(split[2]));
        //            iListLithoType_LithoTrack.Add(int.Parse(split[3]));
        //        }
        //    }
        //}
        public XmlElement gTrackLitho(List<float> fListTopTVD, List<float> fListBottomTVD, List<int> iListLitho, float m_KB, int m_iTrackwidth)
        {
            //addLithoSandPatternDefs();
            //addLithoShalePatternDefs();
            //addLithoLimestonePatternDefs();
            XmlElement gLithoTrack = svgDoc.CreateElement("g");
            gLithoTrack.SetAttribute("id", "idTrackLitho");

            for (int i = 0; i < fListTopTVD.Count; i++)
            {
                float _top = fListTopTVD[i];
                float _bottom = fListBottomTVD[i];
                float _iLitho = iListLitho[i];


                XmlElement gLithoRect = svgDoc.CreateElement("rect");
                gLithoRect.SetAttribute("x", "0");
                gLithoRect.SetAttribute("y", (-m_KB + _top).ToString());
                gLithoRect.SetAttribute("width", (m_iTrackwidth).ToString());
                gLithoRect.SetAttribute("height", (_bottom - _top).ToString());
                gLithoRect.SetAttribute("style", "stroke-width:0.1");
                gLithoRect.SetAttribute("stroke", "black");


                //XmlElement gLithoUse = svgDoc.CreateElement("use");
                //gLithoUse.SetAttribute("x", "0");
                //gLithoUse.SetAttribute("y", (-m_KB + _top).ToString());
                //XmlAttribute lithoNode = svgDoc.CreateAttribute("xlink", "href", "http://www.w3.org/1999/xlink");
                if (_iLitho == 1)
                {
                    gLithoRect.SetAttribute("fill", "url(#patternSand)");

                }
                else if (_iLitho == 2)  //water
                {
                    gLithoRect.SetAttribute("fill", "url(#patternShale)");

                }
                else if (_iLitho == 3) gLithoRect.SetAttribute("fill", "url(#patternLimestone)");

                //else if (_iLitho == 4) gLithoUse.SetAttribute("fill", "#999999");   //dry
                //else if (_iLitho == 5) gLithoUse.SetAttribute("fill", "#FF0099");   //##oilandgas
                //else if (_iLitho == 6) gLithoUse.SetAttribute("fill", "red");   //##OilWater
                //else if (_iLitho == 7) gLithoUse.SetAttribute("fill", "blue");   //##GasWater
                //else if (_iLitho == 9) gLithoUse.SetAttribute("fill", "blue");   //##MinorGas
                //else if (_iLitho == 12) gLithoUse.SetAttribute("fill", "black");   //##Coal

                gLithoTrack.AppendChild(gLithoRect);
            }

            return gLithoTrack;
        }
    }
}
