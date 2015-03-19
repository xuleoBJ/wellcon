using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
namespace DOGPlatform.SVG
{
    class cSVGSectionTrackLayer : cSVGSectionTrack
    {
        public List<string> colorList = new List<string>();
       

        public cSVGSectionTrackLayer() 
        {

            //配置颜色 需要修改
        //    if (File.Exists(cProjectManager.xmlProject)) setLayerColorByXML();
        //    else setLayerColor();
            setLayerColor();
            iTextSize = 6;
        }

        public cSVGSectionTrackLayer(int _iTrackWidth)
            : base(_iTrackWidth)
        {
            setLayerColor();
            iTextSize = 6;
        }
     
        public int iTextSize { get; set; }

        public void setLayerColor() //自动设置颜色带
        {
            colorList = new List<string>() { "#FFFF66", "#FF99FF", "#FFE4C4", "#FFEBCD", "#F5DEB3", "#FF8C00", "#FFFACD", "#FFE4B5", "#FFDAB9", "#FF83FA", "#FF8C00", "#FF6EB4", "#FF7F50", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#FFA07A", "#7CFC00", "#ADFF2F", "#AFEEEE", "#87CEEB", "#FFF68F", "#FFEFD5", "#FFE4E1", "#FFDEAD", "#FFC1C1", "#FFD700", "#FFBBFF", "#FFAEB9", "#FF83FA", "#FFE1FF", "#FCFCFC", "#FAFAD2", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#E0FFFF", "#FFFF66", "#FF99FF", "#FFE4C4", "#FFEBCD", "#F5DEB3", "#FF8C00", "#FFFACD", "#FFE4B5", "#FFDAB9", "#FF83FA", "#FF8C00", "#FF6EB4", "#FF7F50", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#FFA07A", "#7CFC00", "#ADFF2F", "#AFEEEE", "#87CEEB", "#FFF68F", "#FFEFD5", "#FFE4E1", "#FFDEAD", "#FFC1C1", "#FFD700", "#FFBBFF", "#FFAEB9", "#FF83FA", "#FFE1FF", "#FCFCFC", "#FAFAD2", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#E0FFFF", "#FFFF66", "#FF99FF", "#FFE4C4", "#FFEBCD", "#F5DEB3", "#FF8C00", "#FFFACD", "#FFE4B5", "#FFDAB9", "#FF83FA", "#FF8C00", "#FF6EB4", "#FF7F50", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#FFA07A", "#7CFC00", "#ADFF2F", "#AFEEEE", "#87CEEB", "#FFF68F", "#FFEFD5", "#FFE4E1", "#FFDEAD", "#FFC1C1", "#FFD700", "#FFBBFF", "#FFAEB9", "#FF83FA", "#FFE1FF", "#FCFCFC", "#FAFAD2", "#F7F7F7", "#F5DEB3", "#F0FFF0", "#EEEEE0", "#EEE685", "#EEB422", "#F2F2F2", "#E0FFFF" };

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
        public XmlElement gTrackLayerDepth(string sJH,List<float> fListDS1, List<float> fListDS2, List<string> ltStrXCM, float m_KB)
        {
            XmlElement gLayerDepthTrack = svgDoc.CreateElement("g");
            gLayerDepthTrack.SetAttribute("id", sJH+"#层序");

            setLayerColor();
            for (int i = 0; i < ltStrXCM.Count; i++)
            {
                float _top = fListDS1[i];
                float _bottom = fListDS2[i];
                string sXCM = ltStrXCM[i];
                double x0 = 0;
                double y0 = -m_KB + _top;
                double height = _bottom - _top;
                if (height > 0) gLayerDepthTrack.AppendChild(gPatternLayer(x0, y0, height, sXCM));
            }
            return gLayerDepthTrack;
        }
        XmlElement gPatternLayer(double x0, double y0, double height,string _sXCM)
        {
            XmlElement gLayer = svgDoc.CreateElement("g");
            XmlElement gLayerDepthRect = svgDoc.CreateElement("rect");
            gLayerDepthRect.SetAttribute("id", "idLayer#" + _sXCM);
            //gLayerDepthRect.SetAttribute("onmouseover", "this.style.stroke = '#ff0000'; this.style['stroke-width'] = 0.5;");
            //gLayerDepthRect.SetAttribute("onmouseout", "this.style.stroke = 'black'; this.style['stroke-width'] = 0.1;");
            gLayerDepthRect.SetAttribute("x", x0.ToString());
            gLayerDepthRect.SetAttribute("y", y0.ToString());
            gLayerDepthRect.SetAttribute("width", this.iTrackWidth.ToString());
            gLayerDepthRect.SetAttribute("height", height.ToString());
            gLayerDepthRect.SetAttribute("style", "stroke:black;stroke-width:0.1");
            if (cProjectData.ltStrProjectXCM.Contains(_sXCM))
            {
                int _iColorIndex = cProjectData.ltStrProjectXCM.IndexOf(_sXCM);
                gLayerDepthRect.SetAttribute("fill", colorList[_iColorIndex]);
            }
            else
            {
                gLayerDepthRect.SetAttribute("fill", "none");
            }
            gLayer.AppendChild(gLayerDepthRect);
            XmlElement textLayer = svgDoc.CreateElement("text");
            gLayerDepthRect.SetAttribute("id", "idText#"+_sXCM );
            //gLayerDepthRect.SetAttribute("onmouseover", "this.style.font-size= '8'");
            //gLayerDepthRect.SetAttribute("onmouseout", "this.style.fill = 'blue'; this.style.font-size= '6'; ");
            textLayer.SetAttribute("x", (x0+3).ToString());
            textLayer.SetAttribute("y", (y0 + 0.5 * height).ToString());
            textLayer.SetAttribute("fill", "black");
            textLayer.SetAttribute("font-size", iTextSize.ToString());
            textLayer.SetAttribute("style", "stroke-width:1");
            textLayer.InnerText = _sXCM;
            gLayer.AppendChild(textLayer);
            return gLayer;
        }
        public XmlElement gXieTrack2VerticalLayerDepth(string sJH, trackLayerDepthDataList layerDepthDataList, float m_KB)
        {
            List<ItemDicWellPath> listWellPathDS1 = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, layerDepthDataList.fListDS1);
            List<ItemDicWellPath> listWellPathDS2 = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, layerDepthDataList.fListDS2);
            List<float> fListTVD1 = listWellPathDS1.Select(p => p.f_TVD).ToList();
            List<float> fListTVD2 = listWellPathDS2.Select(p => p.f_TVD).ToList();
            return gTrackLayerDepth(sJH, fListTVD1, fListTVD2, layerDepthDataList.ltStrXCM, m_KB);
        }
        public XmlElement gPathTrackLayerDepth(string sJH, List<float> fListDS1, List<float> fListDS2, List<string> ltStrXCM, float m_KB)
        {
            XmlElement gLayerDepthTrack = svgDoc.CreateElement("g");
            gLayerDepthTrack.SetAttribute("id", sJH + "#层序");
            setLayerColor();
            List<ItemDicWellPath> listWellPathTop = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, fListDS1);
             List<ItemDicWellPath> listWellPathBase = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, fListDS2);
            for (int i = 0; i < ltStrXCM.Count; i++)
            {
                string sXCM = ltStrXCM[i];
                double x0 = listWellPathTop[0].f_dx;
                double y0 = -m_KB + listWellPathTop[i].f_TVD;
                double height = listWellPathBase[i].f_TVD - listWellPathTop[i].f_TVD;
                if(height>0) gLayerDepthTrack.AppendChild(gPatternLayer(x0, y0, height, sXCM));
            }

            return gLayerDepthTrack;
        }
        public XmlElement gPathTrackLayerDepth(string sJH, trackLayerDepthDataList layerDepthDataList, float m_KB)
        {
            return gPathTrackLayerDepth(sJH, layerDepthDataList.fListDS1, layerDepthDataList.fListDS2, layerDepthDataList.ltStrXCM, m_KB);
        }
        public XmlElement gTrackLayerDepth(string sJH,trackLayerDepthDataList layerDepthDataList, float m_KB)
        {
            return gTrackLayerDepth(sJH,layerDepthDataList.fListDS1, layerDepthDataList.fListDS2, layerDepthDataList.ltStrXCM, m_KB);
        }
        
    }
}
