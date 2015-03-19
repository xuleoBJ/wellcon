using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DOGPlatform.XML;

namespace DOGPlatform.SVG
{
    class cSVGSectionTrackLog : cSVGSectionTrack
    {
        public cSVGSectionTrackLog(int _iTrackWidth)
            : base(_iTrackWidth)
        {

        }
        public XmlElement gTrackLog(ItemLogHeadInfor ItemLogHeadInfor, trackLogDataList logDataList, float m_KB)
        {
            return gTrackLog(ItemLogHeadInfor.sJH, ItemLogHeadInfor.sLogName, logDataList.fListMD, logDataList.fListValue, m_KB, ItemLogHeadInfor.fLeftValue, ItemLogHeadInfor.fRightValue, ItemLogHeadInfor.sLogColor);
        }

        public XmlElement gTrackLog(string sJH, trackLogDataList logDataList, float m_KB)
        {
            ItemLogHeadInfor logHeadInfor = cXETrackLog.getLogHeadInfor(cProjectManager.xmlSectionCSS, sJH, logDataList.sLogName);
            return gTrackLog(logHeadInfor, logDataList, m_KB);
        }

        public XmlElement gXieTrack2VerticalLog(string sJH,  trackLogDataList logDataList, float m_KB)
        {
            ItemLogHeadInfor logHeadInfor = cXETrackLog.getLogHeadInfor(cProjectManager.xmlSectionCSS, sJH, logDataList.sLogName);
            return gXieTrack2VerticalLog(logHeadInfor, logDataList, m_KB);
        } 


        public XmlElement gTrackLog(string sJH, string sLogName, List<float> fListTVD, List<float> fListValue,
    float m_KB, float fLeftValue, float fRightValue, string sColorCurve)
        {
            XmlElement gLogTrack = svgDoc.CreateElement("g");
            gLogTrack.SetAttribute("id", sJH + "#" + sLogName);
            gLogTrack.SetAttribute("stroke", sColorCurve);
            gLogTrack.SetAttribute("stroke-width", "0.5");
            if (sLogName == null || fListTVD.Count == 0) return gLogTrack;

            double x0 = 0;
            double y0 = -m_KB + fListTVD[0];
            gLogTrack.AppendChild(gTrackLogCurve(x0, y0, fListTVD, fListValue, fLeftValue, fRightValue, sColorCurve));
           
            //添加道头名，X位置取道宽一半偏左，Y位置去深度最小值对应的海拔更低一些##添加道头名，X位置取道宽一半偏左，Y位置去深度最小值对应的海拔更低一些
          
            gLogTrack.AppendChild(gTrackLogHeadText(x0, y0, sLogName, sColorCurve));

            gLogTrack.AppendChild(gTrackLogHeadRuler(x0, y0, sColorCurve)); 
            //添加道标尺，X位置取道宽一半偏左，Y位置去深度最小值对应的海拔更低一些
            XmlElement curveHeadInfor = svgDoc.CreateElement("path");
            string sPath = "m0 " + (-m_KB - 5 + fListTVD.Min()).ToString() + " v5 h " + this.iTrackWidth.ToString() + " v-5";
            curveHeadInfor.SetAttribute("d", sPath);
            curveHeadInfor.SetAttribute("fill", "none");

            gLogTrack.AppendChild(curveHeadInfor);

            return gLogTrack;
        }


        public XmlElement gXieTrack2VerticalLog(ItemLogHeadInfor ItemLogHeadInfor, trackLogDataList logDataList, float m_KB)
        {
            List<ItemDicWellPath> listWellPath = cIOinputWellPath.getWellPathItemListByJHAndMDList(ItemLogHeadInfor.sJH, logDataList.fListMD);
            List<float> fListTVD =listWellPath.Select(p=>p.f_TVD).ToList();
            return gTrackLog(ItemLogHeadInfor.sJH, ItemLogHeadInfor.sLogName,fListTVD, logDataList.fListValue, m_KB, ItemLogHeadInfor.fLeftValue, ItemLogHeadInfor.fRightValue, ItemLogHeadInfor.sLogColor);
        }


        public XmlElement gPathTrackLog(ItemLogHeadInfor ItemLogHeadInfor, trackLogDataList logDataList, float m_KB)
        {
            return gPathTrackLog(ItemLogHeadInfor.sJH, ItemLogHeadInfor.sLogName, logDataList.fListMD, logDataList.fListValue, m_KB, ItemLogHeadInfor.fLeftValue, ItemLogHeadInfor.fRightValue, ItemLogHeadInfor.sLogColor);
        }
        public XmlElement gPathTrackLog(string sJH, string sLogName, List<float> fListMD, List<float> fListValue,
           float m_KB, float fLeftValue, float fRightValue, string sColorCurve)
        {
            XmlElement gLogTrack = svgDoc.CreateElement("g");
            gLogTrack.SetAttribute("id", sJH + "#" + sLogName);
            gLogTrack.SetAttribute("stroke", sColorCurve);
            gLogTrack.SetAttribute("stroke-width", "0.5");
            if (sLogName == null || fListMD.Count == 0) return gLogTrack;
            string _points = "";
            
            List<ItemDicWellPath> listWellPath = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, fListMD);
            List<ItemDicWellPath> listWellPathHorzion = new List<ItemDicWellPath>();
            List<float> fListMDHorizina=new List<float>();
            List<float> fListValueHorizina=new List<float>();
            double x0 = listWellPath[0].f_dx;
            double y0 = -m_KB + listWellPath[0].f_TVD;
            for (int i = 0; i < fListMD.Count; i++)
            {
                ItemDicWellPath currentWellPath = listWellPath[i];
                double currentX = currentWellPath.f_dx;
                double currentY = -m_KB + currentWellPath.f_TVD;
                if (currentWellPath.f_incl <= 85)
                {
                    float _xView_f = 0.0f;
                    if (-500 <= fListValue[i] && fListValue[i] < 1000)
                    {
                        _xView_f = this.iTrackWidth * (fListValue[i] - fLeftValue) / (fRightValue - fLeftValue);
                        _points = _points + (currentX + _xView_f).ToString() + ',' + currentY.ToString() + " ";
                    }
                }
                else if (currentWellPath.f_incl >= 85&&i%3==0)
               {
                   listWellPathHorzion.Add(currentWellPath);
                   fListMDHorizina.Add(fListMD[i]);
                   fListValueHorizina.Add(fListValue[i]); 
                } 
            }
            XmlElement gLogPolyline = svgDoc.CreateElement("polyline");
            gLogPolyline.SetAttribute("fill", "none");
            gLogPolyline.SetAttribute("points", _points);
            gLogTrack.AppendChild(gLogPolyline);

            gLogTrack.AppendChild(gTrackLogHeadText(x0, y0, sLogName, sColorCurve));
            gLogTrack.AppendChild(gTrackLogHeadRuler(x0, y0, sColorCurve)); 

            return gLogTrack;
        }

        //水平井显示模块差好多，需要调整
        public XmlElement gPathHorinzalTrackLog(string sJH, string sLogName, List<float> fListMD, List<float> fListValue,
         float m_KB, float fLeftValue, float fRightValue, string sColorCurve)
        {
            XmlElement gLogTrack = svgDoc.CreateElement("g");
            gLogTrack.SetAttribute("id", sJH + "#" + sLogName);
            gLogTrack.SetAttribute("stroke", sColorCurve);
            gLogTrack.SetAttribute("stroke-width", "0.5");
            if (sLogName == null || fListMD.Count == 0) return gLogTrack;
            string _points = "";

            List<ItemDicWellPath> listWellPath = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, fListMD);
            List<ItemDicWellPath> listWellPathHorzion = new List<ItemDicWellPath>();
            List<float> fListMDHorizina = new List<float>();
            List<float> fListValueHorizina = new List<float>();
            double x0 = listWellPath[0].f_dx;
            double y0 = -m_KB + listWellPath[0].f_TVD;
            for (int i = 0; i < fListMD.Count; i++)
            {
                ItemDicWellPath currentWellPath = listWellPath[i];
                double currentX = currentWellPath.f_dx;
                double currentY = -m_KB + currentWellPath.f_TVD;
                if (currentWellPath.f_incl <= 70)
                {
                    float _xView_f = 0.0f;
                    if (-500 <= fListValue[i] && fListValue[i] < 1000)
                    {
                        _xView_f = this.iTrackWidth * (fListValue[i] - fLeftValue) / (fRightValue - fLeftValue);
                        _points = _points + (currentX + _xView_f).ToString() + ',' + currentY.ToString() + " ";
                    }
                }
                else if (currentWellPath.f_incl >= 85 && i % 3 == 0)
                {
                    listWellPathHorzion.Add(currentWellPath);
                    fListMDHorizina.Add(fListMD[i]);
                    fListValueHorizina.Add(fListValue[i]);
                }

            }
            XmlElement gLogPolyline = svgDoc.CreateElement("polyline");
            // gLogPolyline.SetAttribute("style", "stroke-width:1");
            //   gLogPolyline.SetAttribute("stroke", sColorCurve);
            gLogPolyline.SetAttribute("fill", "none");
            gLogPolyline.SetAttribute("points", _points);
            gLogTrack.AppendChild(gLogPolyline);

            string _pointsHorizon = "";
            for (int i = 0; i < listWellPathHorzion.Count; i++)
            {
                ItemDicWellPath currentWellPath = listWellPathHorzion[i];
                double currentDX = currentWellPath.f_dx;
                double currentY = -m_KB + currentWellPath.f_TVD;

                float _yView_f = 0.0f;
                if (-500 <= fListValueHorizina[i] && fListValueHorizina[i] < 1000)
                {
                    _yView_f = this.iTrackWidth * (fListValueHorizina[i] - fLeftValue) / (fRightValue - fLeftValue);
                    _pointsHorizon = _pointsHorizon + currentDX.ToString() + ',' + (currentY - _yView_f).ToString() + " ";
                }
            }
            XmlElement gLogPolylineHorizinal = svgDoc.CreateElement("polyline");
            // gLogPolyline.SetAttribute("style", "stroke-width:1");
            //   gLogPolyline.SetAttribute("stroke", sColorCurve);
            gLogPolylineHorizinal.SetAttribute("fill", "none");
            gLogPolylineHorizinal.SetAttribute("points", _pointsHorizon);
            gLogTrack.AppendChild(gLogPolylineHorizinal);

            gLogTrack.AppendChild(gTrackLogHeadText(x0, y0, sLogName, sColorCurve));
            gLogTrack.AppendChild(gTrackLogHeadRuler(x0, y0, sColorCurve));

            return gLogTrack;
        }
         XmlElement gTrackLogHeadText(double x0,double y0,string sLogName,string sColor) 
        {
            //添加道头名，X位置取道宽一半偏左，Y位置去深度最小值对应的海拔更低一些##添加道头名，X位置取道宽一半偏左，Y位置去深度最小值对应的海拔更低一些
            XmlElement logNameText = svgDoc.CreateElement("text");
            logNameText.SetAttribute("x", (x0 + (this.iTrackWidth - 8) * 0.5).ToString());
            logNameText.SetAttribute("y", (y0 - 5 ).ToString());
            logNameText.SetAttribute("font-size", "6");
            logNameText.SetAttribute("fill", sColor);
            logNameText.InnerText = sLogName;
            return logNameText;
        }

         XmlElement gTrackLogHeadRuler(double x0, double y0, string sColor)
         {
             XmlElement curveHeadInfor = svgDoc.CreateElement("path");
             string sPath = "m " +x0 + " " + (y0 - 2 ).ToString() + " v2 h " + this.iTrackWidth.ToString() + " v-2";
             curveHeadInfor.SetAttribute("d", sPath);
             curveHeadInfor.SetAttribute("fill", "none");
             return curveHeadInfor;
         }

         XmlElement gTrackLogCurve(double x0, double y0, List<float> fListTVD, List<float> fListValue, double fLeftValue, double fRightValue, string sColor) 
         {
             string _points = "";
             for (int i = 0; i < fListTVD.Count; i++)
             {
                 double currentX = x0;
                 double currentY = y0 + fListTVD[i]-fListTVD[0];
                 if (-500 <= fListValue[i] && fListValue[i] < 1000)
                 {
                     currentX = x0 + this.iTrackWidth * (fListValue[i] - fLeftValue) / (fRightValue - fLeftValue);
                     _points = _points + currentX.ToString() + ',' + currentY.ToString() + " ";
                 }
             }
             XmlElement gLogPolyline = svgDoc.CreateElement("polyline");
             gLogPolyline.SetAttribute("style", "stroke-width:1");
             gLogPolyline.SetAttribute("stroke", sColor);
             gLogPolyline.SetAttribute("fill", "none");
             gLogPolyline.SetAttribute("points", _points);
             return gLogPolyline;
         
         }

      
    }
}
