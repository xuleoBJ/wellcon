#region << 版 本 注 释 >>
/*
 * ========================================================================
 * Copyright(c) 2014 Xuleo,Riped, All Rights Reserved.
 * ========================================================================
 *  许磊，联系电话13581625021，qq：38643987

 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Linq;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGBase
    {
        public XmlDocument svgDoc = new XmlDocument();
        public const string svgNS = "http://www.w3.org/1999/xlink";
        public const string inkNS =  "http://www.inkscape.org/namespaces/inkscape";
        public int offsetXgEle = 0;//define x offset of baselayer
        public int offsetYgEle = 0;
        public XmlElement svgRoot;
        public XmlDeclaration svgDec;
        public XmlElement gBaseLayerSVG;
        public XmlElement svgScript;
        public XmlElement svgCss;
        public XmlElement svgDefs;
        public XmlAttribute xLinkNode;

        public cSVGBase()
            : this(2000, 1500, 0, 0, "pt")
        {

        }
        public cSVGBase(int iDXg, int iDYg)
            : this(2000, 1500, iDXg, iDYg, "pt")
        {

        }
        public cSVGBase(int iWidth, int iHeight, int iDXg, int iDYg,string sUnit)
        {
            this.offsetXgEle = iDXg;
            this.offsetYgEle = iDYg;
            svgDec = svgDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            svgDoc.AppendChild(svgDec);
            svgRoot = svgDoc.CreateElement("svg");
            svgRoot.SetAttribute("xmlns:svg", "http://www.w3.org/2000/svg");
            svgRoot.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
            xLinkNode = svgDoc.CreateAttribute("xlink", "href", "http://www.w3.org/1999/xlink");
            svgRoot.Attributes.Append(xLinkNode);
            svgRoot.SetAttribute("xmlns:inkscape", "http://www.inkscape.org/namespaces/inkscape");
            svgRoot.SetAttribute("version", "1.1");
            svgRoot.SetAttribute("height",iHeight.ToString() + sUnit);
            svgRoot.SetAttribute("width",iWidth.ToString() + sUnit);
            string sViewBox = "0 0 " +iWidth.ToString() + " " + iHeight.ToString();
            svgRoot.SetAttribute("viewBox", sViewBox);
            svgDefs = svgDoc.CreateElement("defs");
            svgRoot.AppendChild(svgDefs);

            svgCss = svgDoc.CreateElement("style");
            svgRoot.AppendChild(svgCss);

            //svgScript = svgDoc.CreateElement("script");
            //svgScript.SetAttribute("type", "application/ecmascript");
            //XmlAttribute striptXL = svgDoc.CreateAttribute("xlink", "href", "http://www.w3.org/1999/xlink");
            //striptXL.Value = "xl.js";
            //svgScript.Attributes.Append(striptXL);
            //svgRoot.AppendChild(svgScript);
            //创建一个Layer
            gBaseLayerSVG = svgDoc.CreateElement("g");
            string sTranslate = "translate(" + offsetXgEle.ToString() + "," + offsetYgEle.ToString() + ")";
            gBaseLayerSVG.SetAttribute("transform", sTranslate);
            gBaseLayerSVG.SetAttribute("id", "BaseLayer");
            gBaseLayerSVG.SetAttribute("label",inkNS, "BaseLayer");
            gBaseLayerSVG.SetAttribute("groupmode",inkNS, "layer");
            gBaseLayerSVG.SetAttribute("xml:space", "preserve");
            svgRoot.AppendChild(gBaseLayerSVG);
            svgDoc.AppendChild(svgRoot);

        }



        public cSVGBase(int iWidth, int iHeight, int iDXg, int iDYg)
            : this(iWidth, iHeight, iDXg, iDYg,"pt")
        {
           
        }

        public void addgElement2LayerBase(XmlElement gElement, int ix, int iy)
        {
            string sTranslate = "translate(" + ix.ToString() + "," + iy.ToString() + ")";
            gElement.SetAttribute("transform", sTranslate);
            XmlNode importNewsItem = svgDoc.ImportNode(gElement, true);
            this.gBaseLayerSVG.AppendChild(importNewsItem);
        }

        public void addgElement2LayerBase(XmlElement gElement)
        {
            addgElement2LayerBase(gElement, 0, 0);
        }
        public void addgElement2Layer(XmlElement gLayer,XmlElement gElement, int ix, int iy)
        {
            string sTranslate = "translate(" + ix.ToString() + "," + iy.ToString() + ")";
            gElement.SetAttribute("transform", sTranslate);
            XmlNode importNewsItem =gLayer.OwnerDocument.ImportNode(gElement, true);
            gLayer.AppendChild(importNewsItem);
        }
        public void addgElement2Layer(XmlElement gLayer, XmlElement gElement)
        {
            addgElement2Layer(gLayer, gElement, 0, 0);
        }
        public void addgLayer(XmlElement gLayer, int ix, int iy)
        {
            string sTranslate = "translate(" + ix.ToString() + "," + iy.ToString() + ")";
            gLayer.SetAttribute("transform", sTranslate);
            this.svgRoot.AppendChild(gLayer);
        }
        public void addgLayer(XmlElement gLayer)
        {
            addgLayer( gLayer, this.offsetXgEle, this.offsetYgEle);
        }
        public XmlElement gLayerElement(string sLayerName) 
        {
            XmlElement gLayer = svgDoc.CreateElement("g");
            gLayer.SetAttribute("style", "visibility:visible;");
            string sTranslate = "translate(" + offsetXgEle.ToString() + "," + offsetYgEle.ToString() + ")";
            gLayer.SetAttribute("transform", sLayerName);
         
            gLayer.SetAttribute("id", sLayerName);
            gLayer.SetAttribute("lable", inkNS, sLayerName);
            gLayer.SetAttribute("groupmode", inkNS, "layer");
            gLayer.SetAttribute("xml:space", "preserve");
            return gLayer; 
        }

        public void addSVGTitle()
        {
            addSVGTitle("Title", 0, 0);
        }
        public void addSVGTitle(int ix, int iy)
        {
            addSVGTitle("Title", ix, iy);
        }
        public void addSVGTitle(string sTitle, int ix, int iy)
        {
            XmlElement svgTitle = svgDoc.CreateElement("text");
            svgTitle.SetAttribute("id", "idTitle");
            svgTitle.SetAttribute("x", ix.ToString());
            svgTitle.SetAttribute("y", iy.ToString());
            svgTitle.SetAttribute("font-size", "10pt");
            svgTitle.SetAttribute("fill", "red");
            svgTitle.InnerText = sTitle;
            svgRoot.AppendChild(svgTitle);
        }
        public void addSVGTitle(string sTitle)
        {
            addSVGTitle(sTitle, 0, 0);
        }


        public void makeSVGfile(string filePath)
        {
            svgDoc.Save(filePath);
        }


    }
}
