﻿using System;
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
    class cSVGDocSection : cSVGBaseSection
    {

        public cSVGDocSection(int width,int height,int iDX, int iDY)
            : base( width, height, iDX, iDY)
        {
        
        }

        public cSVGDocSection(int width, int height, int iDX, int iDY,string sUnit)
            : base(width, height, iDX, iDY, sUnit)
        {

        }

        public List<string> ltStrJH_SVG = new List<string>();
        public List<double> dfListX_Real = new List<double>();
        public List<double> dfListY_Real = new List<double>();
        public List<float> fListKB_Real = new List<float>();
        public List<int> iListWellType_Real = new List<int>();
       
        public void  addgElement2LayerBase(XmlElement gElement,int idx)  //剖面图Y不能移动
        {
            string sTranslate = "translate(" + idx.ToString() + ",0)";
            gElement.SetAttribute("transform", sTranslate);
            XmlNode importNewsItem = svgDoc.ImportNode(gElement, true);
            this.gBaseLayerSVG.AppendChild(importNewsItem);
        }
        public void addgLayer(XmlElement gLayer, int ix)
        {
            base.addgLayer(gLayer, ix, 0);
        }
        public void addgElement2Layer(XmlElement gLayer, XmlElement gElement, int ix)
        {
            base.addgElement2Layer(gLayer, gElement, ix, 0);
        }
        public new  void addgElement2LayerBase(XmlElement gElement, int idx,int idy)  //剖面图Y不能移动
        {
            string sTranslate = "translate(" + idx.ToString() + "," + idy.ToString() + ")";
            gElement.SetAttribute("transform", sTranslate);
            XmlNode importNewsItem = svgDoc.ImportNode(gElement, true);
            this.gBaseLayerSVG.AppendChild(importNewsItem);
        }
      
        public void addgElement2LayerBase(XmlElement gElement, Point pt)
        {
            addgElement2LayerBase(gElement, pt.X, pt.Y);
        }

        
        /// <summary>
        /// 增加距离尺，需要2个参数，因为有水平缩放，所以要传2个值 一个是线的数值 另一个是画的线的长度
        /// </summary>
        /// <param name="distanceValue">距离尺的数值</param>
        /// <param name="lineDistantce">画的线的长度</param>
        /// <returns></returns>
        public XmlElement gWellDistanceRuler (double distanceValue,double lineDistantce)
        {
            XmlElement gDistanceRuler = svgDoc.CreateElement("g");
            gDistanceRuler.SetAttribute("id", "idWellDistance");
            XmlElement curveHeadInfor = svgDoc.CreateElement("path");
            string sPath = "m 5 -5" + " v 5 h " + (lineDistantce - 30).ToString() + " v-5";
            curveHeadInfor.SetAttribute("d", sPath);
            curveHeadInfor.SetAttribute("fill", "none");
            curveHeadInfor.SetAttribute("stroke", "black");
            gDistanceRuler.AppendChild(curveHeadInfor);
            XmlElement distanceText = svgDoc.CreateElement("text");
            distanceText.SetAttribute("x", (lineDistantce*0.45).ToString());
            distanceText.SetAttribute("y", (- 5).ToString());
            distanceText.SetAttribute("font-size", "20");
            distanceText.SetAttribute("fill", "red");
            distanceText.InnerText = distanceValue.ToString() + "m";
            gDistanceRuler.AppendChild(distanceText);
            return gDistanceRuler;
        }

         
 
    }
}
