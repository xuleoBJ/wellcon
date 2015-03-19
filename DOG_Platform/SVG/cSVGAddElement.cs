using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DOGPlatform.SVG
{
    class cSVGAddElement 
    {
        public static void addLayer(string filepathSVG)
        {
            XDocument XRoot = XDocument.Load(filepathSVG);
            XElement Xg = XRoot.Element("svg").Element("g");
            XElement gRect = new XElement("rect");
            gRect.Add(new XElement("x", "100"));
            gRect.Add(new XElement("y", "100"));
            gRect.Add(new XElement("width", "300"));
             gRect.Add(new XElement("height", "300"));
             gRect.Add(new XElement("stroke", "red"));
             gRect.Add(new XElement("stroke-width","1"));
             Xg.Add(gRect);
             XRoot.Save(filepathSVG);
    
        }
    }
  
}
