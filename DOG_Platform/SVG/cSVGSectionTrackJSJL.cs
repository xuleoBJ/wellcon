using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGSectionTrackJSJL : cSVGSectionTrack
    {
        public cSVGSectionTrackJSJL(int _iTrackWidth)
            : base(_iTrackWidth)
        {

        }
        public cSVGSectionTrackJSJL()
            : base()
        {

        }
        public XmlElement gXieTrack2VerticalJSJL(string sJH, trackJSJLDataList JSJLDataList, float m_KB)
        {
            List<ItemDicWellPath> listWellPathDS1 = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, JSJLDataList.fListDS1);
            List<ItemDicWellPath> listWellPathDS2 = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, JSJLDataList.fListDS2);
            List<float> fListTVD1 = listWellPathDS1.Select(p => p.f_TVD).ToList();
            List<float> fListTVD2 = listWellPathDS2.Select(p => p.f_TVD).ToList();
            return gTrackJSJL(sJH, fListTVD1, fListTVD2, JSJLDataList.iListJSJL,m_KB);
        }

        public XmlElement gPathTrackJSJL(string sJH, trackJSJLDataList JSJLDataList, float m_KB)
        {
            return gPathTrackJSJL(sJH, JSJLDataList.fListDS1, JSJLDataList.fListDS2, JSJLDataList.iListJSJL, m_KB);
        }

        public XmlElement gPathTrackJSJL(string sJH,List<float> fListTopMD, List<float> fListBottomMD, List<int> iListJSJL,float m_KB)
        {
            XmlElement gJSJLTrack = svgDoc.CreateElement("g");
            gJSJLTrack.SetAttribute("id", sJH + "#解释结论");
          
            List<ItemDicWellPath> listWellPathTop = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, fListTopMD);
            List<ItemDicWellPath> listWellPathBase = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, fListBottomMD);
            for (int i = 0; i < fListTopMD.Count; i++)
            {
                float _jsjl = iListJSJL[i];
                double x0 = listWellPathTop[i].f_dx;
                double y0 = -m_KB + listWellPathTop[i].f_TVD;
                double height = listWellPathBase[i].f_TVD - listWellPathTop[i].f_TVD;
                if (_jsjl == 1) gJSJLTrack.AppendChild(gPatternJSJLOil(x0, y0, height)); //oil
                else if (_jsjl == 2) gJSJLTrack.AppendChild(gPatternJSJLWater(x0, y0, height));//water
                else if (_jsjl == 3) gJSJLTrack.AppendChild(gPatternJSJLGas(x0, y0, height));//gas
                else if (_jsjl == 4) gJSJLTrack.AppendChild(gPatternJSJLDry(x0, y0, height));//dry
                else if (_jsjl == 5) gJSJLTrack.AppendChild(gPatternJSJLOilGas(x0, y0, height)); //##oilandgas/
                else if (_jsjl == 6) gJSJLTrack.AppendChild(gPatternJSJLOilWater(x0, y0, height)); //##OilWater 
                else if (_jsjl == 7) gJSJLTrack.AppendChild(gPatternJSJLGasWater(x0, y0, height)); //## //##GasWater
                else if (_jsjl == 8) gJSJLTrack.AppendChild(gPatternJSJLMinorOil(x0, y0, height));  //##MinorOil
                else if (_jsjl == 9) gJSJLTrack.AppendChild(gPatternJSJLMinorGas(x0, y0, height)); //##MinorGas 
                else if (_jsjl == 10) gJSJLTrack.AppendChild(gPatternJSJLMinorOilGas(x0, y0, height)); // //##MinorOilGas
                else if (_jsjl == 12) gJSJLTrack.AppendChild(gPatternJSJLCoal(x0, y0, height)); ////##Coal
                else gJSJLTrack.AppendChild(gPatternJSJLUnKnown(x0, y0, height));

            }

            return gJSJLTrack;
        }
       
        public  XmlElement gTrackJSJL(string sJH,List<float> fListTopTVD, List<float> fListBottomTVD, List<int> iListJSJL, float m_KB)
        {
            XmlElement gJSJLTrack = svgDoc.CreateElement("g");
            gJSJLTrack.SetAttribute("id", sJH + "#解释结论");
            for (int i = 0; i < fListTopTVD.Count; i++)
            {
                float _top = fListTopTVD[i];
                float _bottom = fListBottomTVD[i];
                float _jsjl = iListJSJL[i];
                float x0 = 0;
                float y0 = -m_KB + _top;
                float height = _bottom - _top;
                if (_jsjl == 1) gJSJLTrack.AppendChild(gPatternJSJLOil(x0,y0,height)); //oil
                else if (_jsjl == 2) gJSJLTrack.AppendChild(gPatternJSJLWater(x0,y0,height));//water
                else if (_jsjl == 3) gJSJLTrack.AppendChild(gPatternJSJLGas(x0,y0,height));//gas
                else if (_jsjl == 4)  gJSJLTrack.AppendChild(gPatternJSJLDry(x0,y0,height));//dry
                else if (_jsjl == 5) gJSJLTrack.AppendChild(gPatternJSJLOilGas(x0,y0,height)); //##oilandgas/
                else if (_jsjl == 6) gJSJLTrack.AppendChild(gPatternJSJLOilWater(x0,y0,height)); //##OilWater 
                else if (_jsjl == 7) gJSJLTrack.AppendChild(gPatternJSJLGasWater(x0,y0,height)); //## //##GasWater
                else if (_jsjl == 8)  gJSJLTrack.AppendChild(gPatternJSJLMinorOil(x0,y0,height));  //##MinorOil
                else if (_jsjl == 9)  gJSJLTrack.AppendChild(gPatternJSJLMinorGas(x0,y0,height)); //##MinorGas 
                else if (_jsjl == 10)  gJSJLTrack.AppendChild(gPatternJSJLMinorOilGas(x0,y0,height)); // //##MinorOilGas
                else if (_jsjl == 12)  gJSJLTrack.AppendChild(gPatternJSJLCoal(x0,y0,height)); ////##Coal
                else gJSJLTrack.AppendChild(gPatternJSJLUnKnown(x0, y0, height));

            }
            return gJSJLTrack;
        }
        public XmlElement gTrackJSJL(string sJH,trackJSJLDataList JSJLDataList, float m_KB)
        {
            return gTrackJSJL(sJH,JSJLDataList.fListDS1, JSJLDataList.fListDS2, JSJLDataList.iListJSJL, m_KB);
        }

        #region JSJLpattern
        XmlElement gPatternJSJLOil(double x0, double y0, double height)
        {
            XmlElement gJSJLRect = svgDoc.CreateElement("rect");
            gJSJLRect.SetAttribute("x", x0.ToString());
            gJSJLRect.SetAttribute("y", y0.ToString());
            gJSJLRect.SetAttribute("width", this.iTrackWidth.ToString());
            gJSJLRect.SetAttribute("height", height.ToString());
            gJSJLRect.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect.SetAttribute("stroke", "black");
            gJSJLRect.SetAttribute("fill", "red");
            return gJSJLRect;
        }

        XmlElement gPatternJSJLGas(double x0, double y0, double height)
        {
            XmlElement gJSJLRect = svgDoc.CreateElement("rect");
            gJSJLRect.SetAttribute("x", x0.ToString());
            gJSJLRect.SetAttribute("y", y0.ToString());
            gJSJLRect.SetAttribute("width", this.iTrackWidth.ToString());
            gJSJLRect.SetAttribute("height", height.ToString());
            gJSJLRect.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect.SetAttribute("stroke", "black");
            gJSJLRect.SetAttribute("fill", "yellow");
            return gJSJLRect;
        }

        XmlElement gPatternJSJLWater(double x0, double y0, double height)
        {
            XmlElement gJSJLRect = svgDoc.CreateElement("rect");
            gJSJLRect.SetAttribute("x", x0.ToString());
            gJSJLRect.SetAttribute("y", y0.ToString());
            gJSJLRect.SetAttribute("width", this.iTrackWidth.ToString());
            gJSJLRect.SetAttribute("height", height.ToString());
            gJSJLRect.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect.SetAttribute("stroke", "black");
            gJSJLRect.SetAttribute("fill", "blue");
            return gJSJLRect;
        }
        XmlElement gPatternJSJLDry(double x0, double y0, double height)
        {
            XmlElement gJSJLDry = svgDoc.CreateElement("g");
            XmlElement gJSJLRect = svgDoc.CreateElement("rect");
            gJSJLRect.SetAttribute("x", x0.ToString());
            gJSJLRect.SetAttribute("y", y0.ToString());
            gJSJLRect.SetAttribute("width", this.iTrackWidth.ToString());
            gJSJLRect.SetAttribute("height", height.ToString());
            gJSJLRect.SetAttribute("style", "stroke-width:0.2");
            gJSJLRect.SetAttribute("stroke", "black");
            gJSJLRect.SetAttribute("fill", "none");
            gJSJLDry.AppendChild(gJSJLRect);
            XmlElement gJSJLRect2 = svgDoc.CreateElement("rect");
            gJSJLRect2.SetAttribute("x", (x0 + 0.33 * this.iTrackWidth).ToString());
            gJSJLRect2.SetAttribute("y", y0.ToString());
            gJSJLRect2.SetAttribute("width", (this.iTrackWidth * 0.34).ToString());
            gJSJLRect2.SetAttribute("height", height.ToString());
            gJSJLRect2.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect2.SetAttribute("stroke", "black");
            gJSJLRect2.SetAttribute("fill", "none");
            gJSJLDry.AppendChild(gJSJLRect2);

            return gJSJLDry;
        }

        XmlElement gPatternJSJLOilGas(double x0, double y0, double height)
        {
            XmlElement gJSJLOG = svgDoc.CreateElement("g");
            XmlElement gJSJLTriUp = svgDoc.CreateElement("path");
            string d = "M " + x0.ToString() + " " + y0.ToString() + "h" + this.iTrackWidth.ToString() + "L " + x0.ToString() + " " + (y0 + height).ToString() + "Z";
            gJSJLTriUp.SetAttribute("d", d);
            gJSJLTriUp.SetAttribute("style", "stroke-width:0.01");
            gJSJLTriUp.SetAttribute("fill", "yellow");
            gJSJLOG.AppendChild(gJSJLTriUp);
            XmlElement gJSJLTriDown = svgDoc.CreateElement("path");
            d = "M " + x0.ToString() + " " + (y0 + height).ToString().ToString() + "h" + this.iTrackWidth.ToString() + "v  " + (-height).ToString() + "Z";
            gJSJLTriDown.SetAttribute("d", d);
            gJSJLTriDown.SetAttribute("style", "stroke-width:0.1");
            gJSJLTriDown.SetAttribute("fill", "red");
            gJSJLOG.AppendChild(gJSJLTriDown);
            return gJSJLOG;
        }

        XmlElement gPatternJSJLOilWater(double x0, double y0, double height) //##OilWater
        {
            XmlElement gJSJLOG = svgDoc.CreateElement("g");
            XmlElement gJSJLTriUp = svgDoc.CreateElement("path");
            string d = "M " + x0.ToString() + " " + y0.ToString() + "h" + this.iTrackWidth.ToString() + "L " + x0.ToString() + " " + (y0 + height).ToString() + "Z";
            gJSJLTriUp.SetAttribute("d", d);
            gJSJLTriUp.SetAttribute("style", "stroke-width:0.01");
            gJSJLTriUp.SetAttribute("fill", "red");
            gJSJLOG.AppendChild(gJSJLTriUp);
            XmlElement gJSJLTriDown = svgDoc.CreateElement("path");
            d = "M " + x0.ToString() + " " + (y0 + height).ToString().ToString() + "h" + this.iTrackWidth.ToString() + "v  " + (-height).ToString() + "Z";
            gJSJLTriDown.SetAttribute("d", d);
            gJSJLTriDown.SetAttribute("style", "stroke-width:0.1");
            gJSJLTriDown.SetAttribute("fill", "blue");
            gJSJLOG.AppendChild(gJSJLTriDown);
            return gJSJLOG;
        }
        XmlElement gPatternJSJLGasWater(double x0, double y0, double height) //##GasWater
        {
            XmlElement gJSJLOG = svgDoc.CreateElement("g");
            XmlElement gJSJLTriUp = svgDoc.CreateElement("path");
            string d = "M " + x0.ToString() + " " + y0.ToString() + "h" + this.iTrackWidth.ToString() + "L " + x0.ToString() + " " + (y0 + height).ToString() + "Z";
            gJSJLTriUp.SetAttribute("d", d);
            gJSJLTriUp.SetAttribute("style", "stroke-width:0.01");
            gJSJLTriUp.SetAttribute("fill", "yellow");
            gJSJLOG.AppendChild(gJSJLTriUp);
            XmlElement gJSJLTriDown = svgDoc.CreateElement("path");
            d = "M " + x0.ToString() + " " + (y0 + height).ToString().ToString() + "h" + this.iTrackWidth.ToString() + "v  " + (-height).ToString() + "Z";
            gJSJLTriDown.SetAttribute("d", d);
            gJSJLTriDown.SetAttribute("style", "stroke-width:0.1");
            gJSJLTriDown.SetAttribute("fill", "blue");
            gJSJLOG.AppendChild(gJSJLTriDown);
            return gJSJLOG;
        }
        XmlElement gPatternJSJLMinorOil(double x0, double y0, double height) //##MinorOil
        {
            XmlElement gJSJL = svgDoc.CreateElement("g");
            XmlElement gJSJLRect = svgDoc.CreateElement("rect");
            gJSJLRect.SetAttribute("x", x0.ToString());
            gJSJLRect.SetAttribute("y", y0.ToString());
            gJSJLRect.SetAttribute("width", (this.iTrackWidth * 0.5).ToString());
            gJSJLRect.SetAttribute("height", height.ToString());
            gJSJLRect.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect.SetAttribute("stroke", "red");
            gJSJLRect.SetAttribute("fill", "none");
            gJSJL.AppendChild(gJSJLRect);

            XmlElement gJSJLRect2 = svgDoc.CreateElement("rect");
            gJSJLRect2.SetAttribute("x", (x0 + this.iTrackWidth * 0.5).ToString());
            gJSJLRect2.SetAttribute("y", y0.ToString());
            gJSJLRect2.SetAttribute("width", (this.iTrackWidth * 0.5).ToString());
            gJSJLRect2.SetAttribute("height", height.ToString());
            gJSJLRect2.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect2.SetAttribute("stroke", "black");
            gJSJLRect2.SetAttribute("fill", "red");
            gJSJL.AppendChild(gJSJLRect2);
            return gJSJL;
        }


        XmlElement gPatternJSJLMinorGas(double x0, double y0, double height) //##MinorGs
        {
            XmlElement gJSJL = svgDoc.CreateElement("g");
            XmlElement gJSJLRect = svgDoc.CreateElement("rect");
            gJSJLRect.SetAttribute("x", x0.ToString());
            gJSJLRect.SetAttribute("y", y0.ToString());
            gJSJLRect.SetAttribute("width", (this.iTrackWidth * 0.5).ToString());
            gJSJLRect.SetAttribute("height", height.ToString());
            gJSJLRect.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect.SetAttribute("stroke", "black");
            gJSJLRect.SetAttribute("fill", "none");
            gJSJL.AppendChild(gJSJLRect);

            XmlElement gJSJLRect2 = svgDoc.CreateElement("rect");
            gJSJLRect2.SetAttribute("x", (x0 + this.iTrackWidth * 0.5).ToString());
            gJSJLRect2.SetAttribute("y", y0.ToString());
            gJSJLRect2.SetAttribute("width", (this.iTrackWidth * 0.5).ToString());
            gJSJLRect2.SetAttribute("height", height.ToString());
            gJSJLRect2.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect2.SetAttribute("stroke", "black");
            gJSJLRect2.SetAttribute("fill", "yellow");
            gJSJL.AppendChild(gJSJLRect2);
            return gJSJL;
        }

        XmlElement gPatternJSJLMinorOilGas(double x0, double y0, double height) //#差油气层
        {
            XmlElement gJSJL = svgDoc.CreateElement("g");
            XmlElement gJSJLRect = svgDoc.CreateElement("rect");
            gJSJLRect.SetAttribute("x", x0.ToString());
            gJSJLRect.SetAttribute("y", y0.ToString());
            gJSJLRect.SetAttribute("width", this.iTrackWidth.ToString());
            gJSJLRect.SetAttribute("height", height.ToString());
            gJSJLRect.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect.SetAttribute("fill", "black");
            gJSJLRect.SetAttribute("stroke", "red");
            gJSJL.AppendChild(gJSJLRect);

            XmlElement gJSJLTriUp = svgDoc.CreateElement("path");
            string d = "M " + x0.ToString() + " " + y0.ToString() + "h" + this.iTrackWidth.ToString() + "L " + x0.ToString() + " " + (y0 + height).ToString() + "Z";
            gJSJLTriUp.SetAttribute("d", d);
            gJSJLTriUp.SetAttribute("style", "stroke-width:0.1");
            gJSJLTriUp.SetAttribute("fill", "yellow");
            gJSJL.AppendChild(gJSJLTriUp);
            XmlElement gJSJLTriDown = svgDoc.CreateElement("path");
            d = "M " + x0.ToString() + " " + (y0 + height).ToString().ToString() + "h" + this.iTrackWidth.ToString() + "v  " + (-height).ToString() + "Z";
            gJSJLTriDown.SetAttribute("d", d);
            gJSJLTriDown.SetAttribute("style", "stroke-width:0.1");
            gJSJLTriDown.SetAttribute("fill", "red");
            gJSJL.AppendChild(gJSJLTriDown);
            return gJSJL;
        }

        XmlElement gPatternJSJLCoal(double x0, double y0, double height) //#煤层
        {
            XmlElement gJSJLRect = svgDoc.CreateElement("rect");
            gJSJLRect.SetAttribute("x", x0.ToString());
            gJSJLRect.SetAttribute("y", y0.ToString());
            gJSJLRect.SetAttribute("width", this.iTrackWidth.ToString());
            gJSJLRect.SetAttribute("height", height.ToString());
            gJSJLRect.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect.SetAttribute("stroke", "black");
            gJSJLRect.SetAttribute("fill", "black");
            return gJSJLRect;
        }

        XmlElement gPatternJSJLUnKnown(double x0, double y0, double height) //#未知
        {
            XmlElement gJSJL = svgDoc.CreateElement("g");
            XmlElement gJSJLRect = svgDoc.CreateElement("rect");
            gJSJLRect.SetAttribute("x", x0.ToString());
            gJSJLRect.SetAttribute("y", y0.ToString());
            gJSJLRect.SetAttribute("width", this.iTrackWidth.ToString());
            gJSJLRect.SetAttribute("height", height.ToString());
            gJSJLRect.SetAttribute("style", "stroke-width:0.1");
            gJSJLRect.SetAttribute("stroke", "red");
            gJSJLRect.SetAttribute("fill", "none");
            gJSJL.AppendChild(gJSJLRect);
            XmlElement gJSJLText = svgDoc.CreateElement("text");
            gJSJLText.SetAttribute("x", (x0 + this.iTrackWidth * 0.45).ToString());
            gJSJLText.SetAttribute("y", y0.ToString());
            gJSJLText.SetAttribute("fill", "red");
            gJSJLText.SetAttribute("font-size", "3");
            gJSJLText.SetAttribute("strole-width", "1");
            gJSJLText.InnerText = "?";
            gJSJL.AppendChild(gJSJLText);
            return gJSJL;
        }

        #endregion

    }
}
