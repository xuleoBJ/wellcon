using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace DOGPlatform.SVG
{
    class cSingleWellDoc : cSVGBaseSection
    {

        public cSingleWellDoc(int width,int height, int iDX, int iDY)
            : base(width, height, iDX, iDY)
        {
        
        }

        int iTopDepth = 0;
        int iBottomDepth = 100;
        float fVScale = 1;
        public void initializeBaseMapInfor(int iShowedTopDepth,int iShowedBottomDepth,float dfscale)
        {
            iTopDepth = iShowedTopDepth;
            iBottomDepth = iShowedBottomDepth;
            fVScale = dfscale;
        }

        public void addgElement2LayerBase(XmlElement gElement, int iDx)  //剖面图Y不能移动
        {
            string sTranslate = "translate(" + (iDx + offsetXgEle).ToString() + " "+offsetYgEle.ToString()+")";
            gElement.SetAttribute("transform", sTranslate);
            this.gBaseLayerSVG.AppendChild(gElement);
        }
        public XmlElement gTrackText(List<float> fListDS1, List<float> fListDS2, List<string> ltStrValue, int m_iTrackwidth)
        {
            XmlElement gTextTrack = svgDoc.CreateElement("g");
            gTextTrack.SetAttribute("ID", "textTrack");
            for (int i = 0; i < ltStrValue.Count; i++)
            {
                float _top = fListDS1[i];
                float _bottom = fListDS2[i];
                string sText = ltStrValue[i];
                XmlElement gText = svgDoc.CreateElement("g");

                XmlElement gTextRect = svgDoc.CreateElement("rect");
                gTextRect.SetAttribute("x", "0");
                gTextRect.SetAttribute("y", (_top).ToString());
                gTextRect.SetAttribute("width", (m_iTrackwidth).ToString());
                gTextRect.SetAttribute("height", (_bottom - _top).ToString());
                gTextRect.SetAttribute("style", "stroke:black;stroke-width:0.1");
                gTextRect.SetAttribute("fill", "none");
                gText.AppendChild(gTextRect);
                XmlElement text = svgDoc.CreateElement("text");
                text.SetAttribute("x", "1");
                text.SetAttribute("y", (_top * 0.5 + _bottom * 0.5).ToString());
                text.SetAttribute("fill", "blue");
                text.SetAttribute("font-size", "6");
                text.SetAttribute("style", "strole-width:1");
                text.InnerText = sText;
                gText.AppendChild(text);
                gTextTrack.AppendChild(gText);
            }
            return gTextTrack;
        }
        /// <summary>增加地层道
        /// 增加地层道
        /// </summary>
        /// <param name="fListTVD1">
        /// 地层顶深
        /// </param>
        /// <param name="fListTVD2">
        /// 地层底深
        /// </param>
        /// <param name="ltStrXCM">
        /// 层段名
        /// </param>
        /// <param name="m_KB">
        /// 补心海拔
        /// </param>
        /// <returns>
        /// 地层道g
        /// </returns>
        public XmlElement gTrackLayerDepth(List<float> fListDS1, List<float> fListDS2, List<string> ltStrXCM, int m_iTrackwidth)
        {
            XmlElement gLayerDepthTrack= svgDoc.CreateElement("g");
            gLayerDepthTrack.SetAttribute("ID", "LayerTrackTrack");
           // setLayerColorByXML();
            for (int i = 0; i < ltStrXCM.Count; i++)
            {
                float _top = fListDS1[i];
                float _bottom = fListDS2[i];
                string sXCM = ltStrXCM[i];

                XmlElement gLayerDepthRect = svgDoc.CreateElement("rect");

                gLayerDepthRect.SetAttribute("x", "0");
                gLayerDepthRect.SetAttribute("y", (_top).ToString());
                gLayerDepthRect.SetAttribute("width", (m_iTrackwidth).ToString());
                gLayerDepthRect.SetAttribute("height", (_bottom - _top).ToString());
                gLayerDepthRect.SetAttribute("style", "stroke:black;stroke-width:0.1");
                if (cProjectData.ltStrProjectXCM.Contains(sXCM))
                {
                    int _iColorIndex = cProjectData.ltStrProjectXCM.IndexOf(sXCM);
                    gLayerDepthRect.SetAttribute("fill", colorList[_iColorIndex]);
                }
                else 
                {
                    gLayerDepthRect.SetAttribute("fill", "none");
                }


                gLayerDepthTrack.AppendChild(gLayerDepthRect);
                XmlElement textLayer = svgDoc.CreateElement("text");
                textLayer.SetAttribute("x", "1");
                textLayer.SetAttribute("y", (_top*0.5+ _bottom*0.5).ToString());
                textLayer.SetAttribute("fill", "blue");
                textLayer.SetAttribute("font-size", "6");
                textLayer.SetAttribute("style", "strole-width:1");
                textLayer.InnerText = sXCM;
                gLayerDepthTrack.AppendChild(textLayer);
            }

            return gLayerDepthTrack;
        }
        public XmlElement addTrackRect(int x,int y,int width,int height) 
        {
            XmlElement gTrackRect = svgDoc.CreateElement("rect");
            gTrackRect.SetAttribute("x", x.ToString());
            gTrackRect.SetAttribute("y", y.ToString());
            gTrackRect.SetAttribute("width", width.ToString());
            gTrackRect.SetAttribute("height", height.ToString());
            gTrackRect.SetAttribute("style", "stroke-width:0.1");
            gTrackRect.SetAttribute("stroke", "black");
            gTrackRect.SetAttribute("fill", "none");
            return gTrackRect;
        }
        public XmlElement addTrackRectWithTitle(string sTitle, int iTopDepth,int iBottomDepth, int width)
        {   int height=iBottomDepth-iTopDepth;
            XmlElement gReturn = svgDoc.CreateElement("g");
            XmlElement gTrackRect = svgDoc.CreateElement("rect");
            gTrackRect.SetAttribute("x", "0");
            gTrackRect.SetAttribute("y", iTopDepth.ToString());
            gTrackRect.SetAttribute("width", width.ToString());
            gTrackRect.SetAttribute("height", height.ToString());
            gTrackRect.SetAttribute("style", "stroke-width:0.5");
            gTrackRect.SetAttribute("stroke", "black");
            gTrackRect.SetAttribute("fill", "none");
            gReturn.AppendChild(gTrackRect);
            //XmlElement gTrackHeadRect = svgDoc.CreateElement("rect");
            //gTrackHeadRect.SetAttribute("x", "0");
            //gTrackHeadRect.SetAttribute("y", (iTopDepth-10).ToString());
            //gTrackHeadRect.SetAttribute("width", width.ToString());
            //gTrackHeadRect.SetAttribute("height", "10");
            //gTrackHeadRect.SetAttribute("style", "stroke-width:0.1");
            //gTrackHeadRect.SetAttribute("stroke", "black");
            //gTrackHeadRect.SetAttribute("fill", "none");
            //gReturn.AppendChild(gTrackHeadRect);
            XmlElement headRectText = svgDoc.CreateElement("text");
            headRectText.SetAttribute("x", (width*0.35).ToString());
            headRectText.SetAttribute("y", (iTopDepth-1).ToString());
            headRectText.SetAttribute("font-size", "4");
            headRectText.SetAttribute("fill", "red");
            headRectText.InnerText = sTitle;
            gReturn.AppendChild(headRectText);
            return gReturn;
        }
        public XmlElement addTrackItemLogHeadInfor(string sLogName,
          int iYPotion, int width, int m_iLeftValue, int m_iRightValue, string m_sColorCurve)
        {
            XmlElement gItemLogHeadInfor = svgDoc.CreateElement("g");
            XmlElement headRectText = svgDoc.CreateElement("text");
            headRectText.SetAttribute("x", (width * 0.4).ToString());
            headRectText.SetAttribute("y", (iYPotion - 3).ToString());
            headRectText.SetAttribute("font-size", "3");
            headRectText.SetAttribute("fill", m_sColorCurve);
            headRectText.InnerText = sLogName;
            gItemLogHeadInfor.AppendChild(headRectText);

            XmlElement leftValueText = svgDoc.CreateElement("text");
            leftValueText.SetAttribute("x", "2");
            leftValueText.SetAttribute("y", (iYPotion - 3).ToString());
            leftValueText.SetAttribute("font-size", "2");
            leftValueText.SetAttribute("fill", m_sColorCurve);
            leftValueText.InnerText = m_iLeftValue.ToString();
            gItemLogHeadInfor.AppendChild(leftValueText);

            XmlElement rightValueText = svgDoc.CreateElement("text");
            rightValueText.SetAttribute("x", (width - 5).ToString());
            rightValueText.SetAttribute("y", (iYPotion - 3).ToString());
            rightValueText.SetAttribute("font-size", "2");
            rightValueText.SetAttribute("fill", m_sColorCurve);
            rightValueText.InnerText = m_iRightValue.ToString();
            gItemLogHeadInfor.AppendChild(rightValueText);
            //添加道头刻度标尺，X位置取道宽一半偏左，Y位置去深度最小值对应的海拔更低一些
            XmlElement logHeadLine = svgDoc.CreateElement("path");
            string sPath = "m2 " + (iYPotion - 1).ToString() + " h " + (width - 4).ToString();
            logHeadLine.SetAttribute("d", sPath);
            logHeadLine.SetAttribute("style", "stroke-width:1");
            logHeadLine.SetAttribute("stroke", m_sColorCurve);
            logHeadLine.SetAttribute("fill", "none");
            gItemLogHeadInfor.AppendChild(logHeadLine);
            return gItemLogHeadInfor;
        }

        public XmlElement addTrackLogRectWithTitle(string sLogName,
            int iTopDepth,int iBottomDepth, int width,  int m_iLeftValue, int m_iRightValue, string m_sColorCurve)
        {
            int height=iBottomDepth-iTopDepth;
            XmlElement gLogHeadRect = svgDoc.CreateElement("g");
            XmlElement gTrackRect = svgDoc.CreateElement("rect");
            gTrackRect.SetAttribute("x", "0");
            gTrackRect.SetAttribute("y", iTopDepth.ToString());
            gTrackRect.SetAttribute("width", width.ToString());
            gTrackRect.SetAttribute("height", height.ToString());
            gTrackRect.SetAttribute("style", "stroke-width:0.5");
            gTrackRect.SetAttribute("stroke", "black");
            gTrackRect.SetAttribute("fill", "none");
            gLogHeadRect.AppendChild(gTrackRect);
            //XmlElement gTrackHeadRect = svgDoc.CreateElement("rect");
            //gTrackHeadRect.SetAttribute("x", "0");
            //gTrackHeadRect.SetAttribute("y", (iTopDepth- 10).ToString());
            //gTrackHeadRect.SetAttribute("width", width.ToString());
            //gTrackHeadRect.SetAttribute("height", "10");
            //gTrackHeadRect.SetAttribute("style", "stroke-width:0.1");
            //gTrackHeadRect.SetAttribute("stroke", "black");
            //gTrackHeadRect.SetAttribute("fill", "none");
            //gLogHeadRect.AppendChild(gTrackHeadRect);

            //XmlElement headLogInfor= addTrackItemLogHeadInfor(sLogName,
            //  iTopDepth, width, m_iLeftValue,  m_iRightValue, m_sColorCurve);
            //gLogHeadRect.AppendChild(headLogInfor);
            return gLogHeadRect;
        }
        public XmlElement addTrackVerticalGrid(int x, int y, int width, int height)
        {
            XmlElement gVerticalGrid = svgDoc.CreateElement("g");
            for (int i = 1; i <= 10; i++)
            {
                XmlElement gSubVerticalGrid = svgDoc.CreateElement("rect");

                gSubVerticalGrid.SetAttribute("x", x.ToString());
                gSubVerticalGrid.SetAttribute("y", y.ToString());
                gSubVerticalGrid.SetAttribute("width", (width * 0.1*i).ToString());
                gSubVerticalGrid.SetAttribute("height", height.ToString());
                gSubVerticalGrid.SetAttribute("style", "stroke-width:0.5");
                gSubVerticalGrid.SetAttribute("stroke", "lightgrey");
                gSubVerticalGrid.SetAttribute("fill", "none");
                gVerticalGrid.AppendChild(gSubVerticalGrid);
                
            }
            return gVerticalGrid;
        }

        public XmlElement addTrackVerticalGridLog(int x, int y, int width, int height)
        {
            XmlElement gVerticalGrid = svgDoc.CreateElement("g");
            for (int i = 1; i <= 10; i++)
            {
                XmlElement gSubVerticalGrid = svgDoc.CreateElement("rect");

                gSubVerticalGrid.SetAttribute("x", x.ToString());
                gSubVerticalGrid.SetAttribute("y", y.ToString());
                gSubVerticalGrid.SetAttribute("width", (width * 0.1 * i).ToString());
                gSubVerticalGrid.SetAttribute("height", height.ToString());
                gSubVerticalGrid.SetAttribute("style", "stroke-width:0.5");
                gSubVerticalGrid.SetAttribute("stroke", "black");
                gSubVerticalGrid.SetAttribute("fill", "none");
                gVerticalGrid.AppendChild(gSubVerticalGrid);

            }
            return gVerticalGrid;
        }
        public XmlElement addTrackHorizonalGrid(int iDS1Showed, int iDS2Showed,int iTrackWidth)
        {
            XmlElement gHorizonalGrid = svgDoc.CreateElement("g");
            int iStartY = Convert.ToInt16(Math.Ceiling( Convert.ToDouble(iDS1Showed) / 10)) * 10;

            while (iStartY <= iDS2Showed)
            {
                XmlElement gSubHorizonalGrid = svgDoc.CreateElement("line");

                gSubHorizonalGrid.SetAttribute("x1", "0");
                gSubHorizonalGrid.SetAttribute("y1", iStartY.ToString());
                gSubHorizonalGrid.SetAttribute("x2", iTrackWidth.ToString());
                gSubHorizonalGrid.SetAttribute("y2", iStartY.ToString());
                gSubHorizonalGrid.SetAttribute("style", "stroke-width:0.5");
                gSubHorizonalGrid.SetAttribute("stroke", "Gray");
                gSubHorizonalGrid.SetAttribute("fill", "none");
                gHorizonalGrid.AppendChild(gSubHorizonalGrid);
                iStartY += 10;

            }
            return gHorizonalGrid;
        }

        public XmlElement gTrackJSJL(List<float> fListTopTVD, List<float> fListBottomTVD, List<int> iListJSJL, int m_iTrackwidth)
        {
            XmlElement gJSJLTrack = svgDoc.CreateElement("g");
            gJSJLTrack.SetAttribute("ID", "JSJLTrack");
            
            for (int i = 0; i < fListTopTVD.Count; i++)
            {
                float _top = fListTopTVD[i];
                float _bottom = fListBottomTVD[i];
                float _jsjl = iListJSJL[i];

                if (_jsjl == 1)
                {

                    XmlElement gJSJLRect = svgDoc.CreateElement("rect");
                    gJSJLRect.SetAttribute("x", "0");
                    gJSJLRect.SetAttribute("y",  _top.ToString());
                    gJSJLRect.SetAttribute("width", m_iTrackwidth.ToString());
                    gJSJLRect.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect.SetAttribute("style", "stroke-width:0.01");
                    gJSJLRect.SetAttribute("stroke", "none");
                    gJSJLRect.SetAttribute("fill", "red");
                    gJSJLTrack.AppendChild(gJSJLRect);

                }//oil
                else if (_jsjl == 2)
                {
                    XmlElement gJSJLRect = svgDoc.CreateElement("rect");
                    gJSJLRect.SetAttribute("x", "0");
                    gJSJLRect.SetAttribute("y",  _top.ToString());
                    gJSJLRect.SetAttribute("width", m_iTrackwidth.ToString());
                    gJSJLRect.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect.SetAttribute("style", "stroke-width:0.01");
                    gJSJLRect.SetAttribute("stroke", "none");
                    gJSJLRect.SetAttribute("fill", "blue");
                    gJSJLTrack.AppendChild(gJSJLRect);
                }  //water
                else if (_jsjl == 3)
                {
                    XmlElement gJSJLRect = svgDoc.CreateElement("rect");
                    gJSJLRect.SetAttribute("x", "0");
                    gJSJLRect.SetAttribute("y", _top.ToString());
                    gJSJLRect.SetAttribute("width", (m_iTrackwidth).ToString());
                    gJSJLRect.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect.SetAttribute("style", "stroke-width:0.01");
                    gJSJLRect.SetAttribute("stroke", "none");
                    gJSJLRect.SetAttribute("fill", "yellow");
                    gJSJLTrack.AppendChild(gJSJLRect);
                }  //gas
                else if (_jsjl == 4)
                {
                    XmlElement gJSJLRect = svgDoc.CreateElement("rect");
                    gJSJLRect.SetAttribute("x", "0");
                    gJSJLRect.SetAttribute("y",  _top.ToString());
                    gJSJLRect.SetAttribute("width", m_iTrackwidth.ToString());
                    gJSJLRect.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect.SetAttribute("style", "stroke-width:0.1");
                    gJSJLRect.SetAttribute("stroke", "black"); 
                    gJSJLRect.SetAttribute("fill", "none");
                    gJSJLTrack.AppendChild(gJSJLRect);
                    XmlElement gJSJLDryLine = svgDoc.CreateElement("path");
                    string d = "M " + (0.33 * m_iTrackwidth).ToString()+" " + _top.ToString()  + "v" + (_bottom - _top).ToString() + "h" + (0.34 * m_iTrackwidth).ToString() + "v-" + (_bottom - _top).ToString();
                    gJSJLDryLine.SetAttribute("d", d);
                    gJSJLDryLine.SetAttribute("style", "stroke-width:0.1");
                    gJSJLDryLine.SetAttribute("stroke", "black");
                    gJSJLDryLine.SetAttribute("fill", "none");
                    gJSJLTrack.AppendChild(gJSJLDryLine);
                }  //dry
                else if (_jsjl == 5)
                {
                    XmlElement gJSJLTriUp = svgDoc.CreateElement("path");
                    string d = "M 0 " +  _top.ToString() + "h" + m_iTrackwidth.ToString() + "L0 " +  _bottom.ToString() + "Z";
                    gJSJLTriUp.SetAttribute("d", d);
                    gJSJLTriUp.SetAttribute("style", "stroke-width:0.1");
                    gJSJLTriUp.SetAttribute("fill", "yellow");
                    gJSJLTrack.AppendChild(gJSJLTriUp);
                    XmlElement gJSJLTriDown = svgDoc.CreateElement("path");
                    d = "M 0 " + _bottom.ToString() + "h" + m_iTrackwidth.ToString() + "v  " + (_top - _bottom).ToString() + "Z";
                    gJSJLTriDown.SetAttribute("d", d);
                    gJSJLTriDown.SetAttribute("style", "stroke-width:0.1");
                    gJSJLTriDown.SetAttribute("fill", "red");
                    gJSJLTrack.AppendChild(gJSJLTriDown);
                }  //##oilandgas
                else if (_jsjl == 6)//##OilWater
                {
                    XmlElement gJSJLTriUp = svgDoc.CreateElement("path");
                    string d = "M 0 " +  _top.ToString() + "h" + m_iTrackwidth.ToString() + "L0 " + _bottom.ToString() + "Z";
                    gJSJLTriUp.SetAttribute("d", d);
                    gJSJLTriUp.SetAttribute("style", "stroke-width:0.1");
                    gJSJLTriUp.SetAttribute("fill", "red");
                    gJSJLTrack.AppendChild(gJSJLTriUp);
                    XmlElement gJSJLTriDown = svgDoc.CreateElement("path");
                    d = "M 0 " +  _bottom.ToString() + "h" + m_iTrackwidth.ToString() + "v  " + (_top - _bottom).ToString() + "Z";
                    gJSJLTriDown.SetAttribute("d", d);
                    gJSJLTriDown.SetAttribute("style", "stroke-width:0.01");
                    gJSJLTriDown.SetAttribute("fill", "blue");
                    gJSJLTrack.AppendChild(gJSJLTriDown);
                }
                else if (_jsjl == 7)
                {
                    XmlElement gJSJLTriUp = svgDoc.CreateElement("path");
                    string d = "M 0 " +  _top.ToString() + "h" + m_iTrackwidth.ToString() + "L0 " +_bottom.ToString() + "Z";
                    gJSJLTriUp.SetAttribute("d", d);
                    gJSJLTriUp.SetAttribute("style", "stroke-width:0.1");
                    gJSJLTriUp.SetAttribute("fill", "yellow");
                    gJSJLTrack.AppendChild(gJSJLTriUp);
                    XmlElement gJSJLTriDown = svgDoc.CreateElement("path");
                    d = "M 0 " + _bottom.ToString() + "h" + (m_iTrackwidth).ToString() + "v  " + (_top - _bottom).ToString() + "Z";
                    gJSJLTriDown.SetAttribute("d", d);
                    gJSJLTriDown.SetAttribute("style", "stroke-width:0.1");
                    gJSJLTriDown.SetAttribute("fill", "blue");
                    gJSJLTrack.AppendChild(gJSJLTriDown);
                } //##GasWater
                else if (_jsjl == 8)
                {
                    XmlElement gJSJLRect = svgDoc.CreateElement("rect");
                    gJSJLRect.SetAttribute("x", "0");
                    gJSJLRect.SetAttribute("y", _top.ToString());
                    gJSJLRect.SetAttribute("width", (m_iTrackwidth * 0.5).ToString());
                    gJSJLRect.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect.SetAttribute("style", "stroke-width:0.1");
                    gJSJLRect.SetAttribute("stroke", "red");
                    gJSJLRect.SetAttribute("fill", "none");
                    gJSJLTrack.AppendChild(gJSJLRect);

                    XmlElement gJSJLRect2 = svgDoc.CreateElement("rect");
                    gJSJLRect2.SetAttribute("x", (m_iTrackwidth * 0.5).ToString());
                    gJSJLRect2.SetAttribute("y",  _top.ToString());
                    gJSJLRect2.SetAttribute("width", (m_iTrackwidth * 0.5).ToString());
                    gJSJLRect2.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect2.SetAttribute("style", "stroke-width:0.1");
                    gJSJLRect2.SetAttribute("stroke", "red");
                    gJSJLRect2.SetAttribute("fill", "red");
                    gJSJLTrack.AppendChild(gJSJLRect2);
                }  //##MinorOil
                else if (_jsjl == 9)
                {
                    XmlElement gJSJLRect = svgDoc.CreateElement("rect");
                    gJSJLRect.SetAttribute("x", "0");
                    gJSJLRect.SetAttribute("y",  _top.ToString());
                    gJSJLRect.SetAttribute("width", (m_iTrackwidth * 0.5).ToString());
                    gJSJLRect.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect.SetAttribute("style", "stroke-width:0.1");
                    gJSJLRect.SetAttribute("fill", "none");
                    gJSJLRect.SetAttribute("stroke", "yellow");
                    gJSJLTrack.AppendChild(gJSJLRect);

                    XmlElement gJSJLRect2 = svgDoc.CreateElement("rect");
                    gJSJLRect2.SetAttribute("x", (m_iTrackwidth * 0.5).ToString());
                    gJSJLRect2.SetAttribute("y",  _top.ToString());
                    gJSJLRect2.SetAttribute("width", (m_iTrackwidth * 0.5).ToString());
                    gJSJLRect2.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect2.SetAttribute("style", "stroke-width:0.1");
                    gJSJLRect2.SetAttribute("stroke", "yellow");
                    gJSJLRect2.SetAttribute("fill", "yellow");
                    gJSJLTrack.AppendChild(gJSJLRect2);
                }  //##MinorGas
                else if (_jsjl == 10)
                {
                    XmlElement gJSJLRect = svgDoc.CreateElement("rect");
                    gJSJLRect.SetAttribute("x", "0");
                    gJSJLRect.SetAttribute("y", _top.ToString());
                    gJSJLRect.SetAttribute("width", (m_iTrackwidth).ToString());
                    gJSJLRect.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect.SetAttribute("style", "stroke-width:0.1");
                    gJSJLRect.SetAttribute("fill", "none");
                    gJSJLRect.SetAttribute("stroke", "red");
                    gJSJLTrack.AppendChild(gJSJLRect);

                    XmlElement gJSJLTriUp = svgDoc.CreateElement("path");
                    string d = "M" + (m_iTrackwidth * 0.5).ToString() + " " +  _top.ToString() + "h" + (m_iTrackwidth * 0.5).ToString() + "L " + (m_iTrackwidth * 0.5).ToString() + " " +  _bottom.ToString() + "Z";
                    gJSJLTriUp.SetAttribute("d", d);
                    gJSJLTriUp.SetAttribute("style", "stroke-width:0.1");
                    gJSJLTriUp.SetAttribute("fill", "yellow");
                    gJSJLTrack.AppendChild(gJSJLTriUp);
                    XmlElement gJSJLTriDown = svgDoc.CreateElement("path");
                    d = "M" + (m_iTrackwidth * 0.5).ToString() + " " + _bottom.ToString() + "h" + (m_iTrackwidth * 0.5).ToString() + "v  " + (_top - _bottom).ToString() + "Z";
                    gJSJLTriDown.SetAttribute("d", d);
                    gJSJLTriDown.SetAttribute("style", "stroke-width:0.1");
                    gJSJLTriDown.SetAttribute("fill", "red");
                    gJSJLTrack.AppendChild(gJSJLTriDown);
                } //##MinorOilGas
                else if (_jsjl == 12)
                {
                    XmlElement gJSJLRect = svgDoc.CreateElement("rect");
                    gJSJLRect.SetAttribute("x", "0");
                    gJSJLRect.SetAttribute("y", _top.ToString());
                    gJSJLRect.SetAttribute("width", (m_iTrackwidth).ToString());
                    gJSJLRect.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect.SetAttribute("style", "stroke-width:0.1");
                    gJSJLRect.SetAttribute("fill", "black");
                    gJSJLTrack.AppendChild(gJSJLRect);
                }  //##Coal
                else
                {
                    XmlElement gJSJLRect = svgDoc.CreateElement("rect");
                    gJSJLRect.SetAttribute("x", "0");
                    gJSJLRect.SetAttribute("y",  _top.ToString());
                    gJSJLRect.SetAttribute("width", m_iTrackwidth.ToString());
                    gJSJLRect.SetAttribute("height", (_bottom - _top).ToString());
                    gJSJLRect.SetAttribute("style", "stroke-width:0.1");
                    gJSJLRect.SetAttribute("stroke", "red");
                    gJSJLRect.SetAttribute("fill", "none");
                    gJSJLTrack.AppendChild(gJSJLRect);
                    XmlElement gJSJLText = svgDoc.CreateElement("text");
                    gJSJLText.SetAttribute("x", (m_iTrackwidth * 0.45).ToString());
                    gJSJLText.SetAttribute("y",  _bottom.ToString());
                    gJSJLText.SetAttribute("fill", "red");
                    gJSJLText.SetAttribute("font-size", "3");
                    gJSJLText.SetAttribute("strole-width", "1");
                    gJSJLText.InnerText = "?";
                    gJSJLTrack.AppendChild(gJSJLText);
                }
                
            }

            return gJSJLTrack;
        }
        public XmlElement gTrackLitho(List<float> fListTopTVD, List<float> fListBottomTVD, List<int> iListLitho, int m_iTrackwidth)
        {
            addLithoSandPatternDefs();
            addLithoShalePatternDefs();
            addLithoLimestonePatternDefs();
            XmlElement gLithoTrack = svgDoc.CreateElement("g");
            gLithoTrack.SetAttribute("ID", "LithoTrack");
       
            for (int i = 0; i < fListTopTVD.Count; i++)
            {
                float _top = fListTopTVD[i];
                float _bottom = fListBottomTVD[i];
                float _iLitho = iListLitho[i];

        
                XmlElement gLithoRect = svgDoc.CreateElement("rect");
                gLithoRect.SetAttribute("x", "0");
                gLithoRect.SetAttribute("y", _top.ToString());
                gLithoRect.SetAttribute("width", (m_iTrackwidth).ToString());
                gLithoRect.SetAttribute("height", (_bottom - _top).ToString());
                gLithoRect.SetAttribute("style", "stroke-width:0.1");
                gLithoRect.SetAttribute("stroke", "black");

                
                //XmlElement gLithoUse = svgDoc.CreateElement("use");
                //gLithoUse.SetAttribute("x", "0");
                //gLithoUse.SetAttribute("y", (-m_KB + _top).ToString());
                //XmlAttribute lithoNode = svgDoc.CreateAttribute("xlink", "href", "http://www.w3.org/1999/xlink");
                if (_iLitho == 1) {
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
        public XmlElement gTrackPerfoation(List<float> fListTop, List<float> fListBottom, int m_iTrackwidth)
        {
            XmlElement gPeforationTrack = svgDoc.CreateElement("g");
            gPeforationTrack.SetAttribute("ID", "PeforationTrack");
            for (int i = 0; i < fListTop.Count; i++)
            {
                float _top = fListTop[i];
                float _bottom = fListBottom[i];
                XmlElement gPeforationInterval = svgDoc.CreateElement("path");
                string sPath = "m0 " + _top.ToString() + " h" + m_iTrackwidth.ToString() + "h-" + (0.5 * m_iTrackwidth).ToString() + "v" + (_bottom - _top).ToString() + "h" + (0.5 * m_iTrackwidth).ToString() + " h-" + m_iTrackwidth.ToString();
                gPeforationInterval.SetAttribute("d", sPath);
                gPeforationInterval.SetAttribute("stroke-width", "1");
                gPeforationInterval.SetAttribute("stroke", "red");
                gPeforationInterval.SetAttribute("fill", "none");
                gPeforationTrack.AppendChild(gPeforationInterval);
            }

            return gPeforationTrack;
        }
        public XmlElement gTrackDepthRuler(int m_minElevationDepth, int m_maxElevationDepth, int m_tickInveral_main)
        {
            XmlElement gElevationRuler = svgDoc.CreateElement("g");
            gElevationRuler.SetAttribute("ID", "DepthRuler");
            XmlElement gLine = svgDoc.CreateElement("line");
            gLine.SetAttribute("x1", "0");
            gLine.SetAttribute("y1", (-m_minElevationDepth).ToString());
            gLine.SetAttribute("x2", "0");
            gLine.SetAttribute("y2", (-m_maxElevationDepth).ToString());
            gLine.SetAttribute("stroke", "blue");
            gLine.SetAttribute("stroke-width", "1");
            gElevationRuler.AppendChild(gLine);
            int iCurrentDepth = (Convert.ToInt16(m_minElevationDepth) / m_tickInveral_main + 1) * m_tickInveral_main;
            while (iCurrentDepth <= m_maxElevationDepth)
            {
                XmlElement gDepthTick = svgDoc.CreateElement("path");
                gDepthTick.SetAttribute("style", "stroke:blue;stroke-width:0.5");
                string d = "M0 " + (-iCurrentDepth).ToString() + " h 3 ";
                gDepthTick.SetAttribute("d", d);
                gElevationRuler.AppendChild(gDepthTick);
                XmlElement gTickText = svgDoc.CreateElement("text");
                gTickText.SetAttribute("x", "3");
                gTickText.SetAttribute("y", (-iCurrentDepth).ToString());
                gTickText.SetAttribute("fill", "black");
                gTickText.SetAttribute("font-size", "8");
                gTickText.SetAttribute("strole-width", "1");
                gTickText.InnerText = iCurrentDepth.ToString();
                gElevationRuler.AppendChild(gTickText);
                iCurrentDepth = iCurrentDepth + m_tickInveral_main;
            }

            return gElevationRuler;



        }
        public XmlElement gMDRuler(float m_minMesureDepth, float m_maxMesureDepth, int m_tickInveral_main, int m_tickInveral_min)
        {

            XmlElement gMDRuler = svgDoc.CreateElement("g");
            gMDRuler.SetAttribute("ID", "id_MDruler");
       
            int iCurrentDepth = (Convert.ToInt16(m_minMesureDepth) / m_tickInveral_main + 1) * m_tickInveral_main;
            while (iCurrentDepth <= m_maxMesureDepth)
            {
                XmlElement gDepthTick = svgDoc.CreateElement("path");
                gDepthTick.SetAttribute("style", "stroke:black;stroke-width:0.3");
                string d = "M0 " + ( iCurrentDepth.ToString()) + " h 3 ";
                gDepthTick.SetAttribute("d", d);
                gMDRuler.AppendChild(gDepthTick);

                XmlElement gTickText = svgDoc.CreateElement("text");
                gTickText.SetAttribute("x", "3");
                gTickText.SetAttribute("y", (iCurrentDepth).ToString());
                gTickText.SetAttribute("fill", "black");
                gTickText.SetAttribute("font-size", "3");
                gTickText.SetAttribute("strole-width", "1");
                gTickText.InnerText = iCurrentDepth.ToString();
                gMDRuler.AppendChild(gTickText);
                iCurrentDepth = iCurrentDepth + m_tickInveral_main;
            }
      
            int m_iValueStartTinyTick = cPublicMethodBase.getCeilingNumer(m_minMesureDepth, m_tickInveral_min);
            while (m_iValueStartTinyTick <= m_maxMesureDepth)
            {
                XmlElement gDepthMinTick = svgDoc.CreateElement("path");
                gDepthMinTick.SetAttribute("style", "stroke:black;stroke-width:0.3");
                string dDepthMinTick = "M0 " + (m_iValueStartTinyTick.ToString()) + " h 2 ";
                gDepthMinTick.SetAttribute("d", dDepthMinTick);
                gMDRuler.AppendChild(gDepthMinTick);
                m_iValueStartTinyTick = m_iValueStartTinyTick + m_tickInveral_min;
            }
            XmlElement gWellBoleLine = svgDoc.CreateElement("line");
            gWellBoleLine.SetAttribute("x1", "0");
            gWellBoleLine.SetAttribute("y1", ( m_minMesureDepth).ToString());
            gWellBoleLine.SetAttribute("x2", "0");
            gWellBoleLine.SetAttribute("y2", ( m_maxMesureDepth).ToString());
            gWellBoleLine.SetAttribute("stroke", "black");
            gWellBoleLine.SetAttribute("stroke-width", "0.3");
            gMDRuler.AppendChild(gWellBoleLine);
   
            return gMDRuler;
        }
        public XmlElement gTrackLog(string sLogName, List<float> fListTVD, List<float> fListValue,
            int m_iLeftValue, int m_iRightValue, string m_sColorCurve, int m_iTrackwidth)
        {
            XmlElement gLogTrack = svgDoc.CreateElement("g");
            gLogTrack.SetAttribute("ID", sLogName);
            string _points = "";
            float _xView_f = 0f;
            for (int i = 0; i < fListTVD.Count; i=i+2)
            {
                if (-500 <= fListValue[i] && fListValue[i] < 1000) 
                {
                    _xView_f = m_iTrackwidth * (fListValue[i] - m_iLeftValue) / (m_iRightValue - m_iLeftValue);
                }
                else
                {
                    _xView_f = m_iLeftValue;
                 
                }
                _points = _points + (_xView_f).ToString() + ',' +fListTVD[i].ToString() + " ";

            }
            XmlElement gLogPolyline = svgDoc.CreateElement("polyline");
            gLogPolyline.SetAttribute("style", "stroke-width:1");
            gLogPolyline.SetAttribute("stroke", m_sColorCurve);
            gLogPolyline.SetAttribute("fill", "none");
            gLogPolyline.SetAttribute("points", _points);
            gLogTrack.AppendChild(gLogPolyline);
 
            return gLogTrack;
        }

        public XmlElement gTrackScatter(string sName, List<float> fListTVD, List<float> fListValue,
            int m_iLeftValue, int m_iRightValue, string m_sColorCurve, int m_iTrackwidth)
        {
            XmlElement gScatterLogTrack = svgDoc.CreateElement("g");
            gScatterLogTrack.SetAttribute("ID", sName);

            for (int i = 0; i < fListTVD.Count; i++)
            {
                float _xView_f = 0f;
                if (-500 <= fListValue[i] && fListValue[i] < 1000)
                {
                    _xView_f = m_iTrackwidth * (fListValue[i] - m_iLeftValue) / (m_iRightValue - m_iLeftValue);
                }
                else
                {
                    _xView_f = m_iLeftValue;
                }
                string d = "M0 " + fListTVD[i].ToString() + " h" + _xView_f;
                XmlElement gLogPath = svgDoc.CreateElement("path");
                gLogPath.SetAttribute("style", "stroke-width:1");
                gLogPath.SetAttribute("stroke", m_sColorCurve);
                gLogPath.SetAttribute("fill", "none");
                gLogPath.SetAttribute("d", d);
                gScatterLogTrack.AppendChild(gLogPath);
            }
         
            return gScatterLogTrack;
        }
        
    }
}
