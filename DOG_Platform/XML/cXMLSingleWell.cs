using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DOGPlatform.XML
{
    class cXMLSingleWell
    {
        public static void generateDataXMLmodel(string filePathSingleWellData,string sSelectedJH)
        {
            XDocument XDoc = new XDocument(
                      new XElement("DataModel",
                      new XElement("WellName", sSelectedJH),
                      new XElement("TrackCollectionData")
                  )
                  );
            XDoc.Save(filePathSingleWellData);
        }
        public static void addTrackDataXMLModel(string sSelectedJH, string filePathSingleWellData, string sTrackType, string sTrackID, object trackTag)
        {
            XElement trackNode = new XElement("Track");
            trackNode.Add(new XElement("trackName", trackTag.ToString()));
            trackNode.Add(new XElement("trackID", sTrackID));
            trackNode.Add(new XElement("trackType", sTrackType));
            string sData = "";
            if (sTrackType ==  TypeTrack.深度道.ToString())
            {

            }
            if (sTrackType ==  TypeTrack.地层道.ToString())
            {
                cIOinputLayerDepth cSelectLayerDepth = new cIOinputLayerDepth();
                sData = cSelectLayerDepth.selectLayerDepth2String(sSelectedJH);
             
            }
            if (sTrackType ==  TypeTrack.曲线道.ToString())
            {

            }
            if (sTrackType ==  TypeTrack.射孔道.ToString())
            {
                sData = cIOinputWellPerforation.selectPerforation2String(sSelectedJH);
      
            }
            if (sTrackType ==  TypeTrack.文本道.ToString())
            {
                //string sData = cOperateInputFileJSJL.selectTopBaseJSJL2StringByJH(sSelectedJH);
                //trackNode.Add(new XElement("data", sData));
            }
            if (sTrackType ==  TypeTrack.解释结论道.ToString())
            {
                sData = cIOinputJSJL.selectJSJL2String(sSelectedJH);
            }
            trackNode.Add(new XElement("data", sData));
            XDocument XsingleWellStyleRoot = XDocument.Load(filePathSingleWellData);
            XElement XNode = XsingleWellStyleRoot.Element("DataModel").Element("TrackCollectionData");
            XNode.Add(trackNode);
            XsingleWellStyleRoot.Save(filePathSingleWellData);
        }

        public static void generateStyleXMLModel(string filePathSingleWellData,int  iShowedTop,int  iShowedBottom, float fVScale)
        {
            XDocument XDoc = new XDocument(
                new XElement("StyleModel",
                          new XElement("iShowedTop", iShowedTop.ToString()),
                          new XElement("iShowedBottom", iShowedBottom.ToString()),
                          new XElement("fVScale", fVScale.ToString()),
                          new XElement("MapInfor"),
                          new XElement("TrackCollection")
                    )
                  );
            XDoc.Save(filePathSingleWellData);
        }
        public static void addTrackStyleXMLModel(string filePathSingleWellStyle, string sTrackType, string sTrackID, object trackTag) 
        {
            XElement trackNode = new XElement("Track");
           
            trackNode.Add(new XElement("trackID", sTrackID));
            trackNode.Add(new XElement("trackType", sTrackType));
            trackNode.Add(new XElement("trackName", trackTag.ToString()));
            trackNode.Add(new XElement("trackTitle", ""));
            trackNode.Add(new XElement("trackWidth", "30"));
            if (sTrackType ==  TypeTrack.深度道.ToString()) 
            {
                trackNode.Add(new XElement("mainScale", "10"));
                trackNode.Add(new XElement("minScale", "2"));
                trackNode.Add(new XElement("font-color", "black"));
            }
            if (sTrackType ==  TypeTrack.地层道.ToString())
            {
                trackNode.Add(new XElement("autoFill", "true"));
                trackNode.Add(new XElement("font-size", "6"));
                trackNode.Add(new XElement("font-color", "red"));
            }
            if (sTrackType ==  TypeTrack.曲线道.ToString())
            {
                trackNode.Add(new XElement("hasGrid", "true"));
                trackNode.Add(new XElement("logGrid", "true"));
                trackNode.Add(new XElement("gridLineWidth", "0.5"));
                trackNode.Add(new XElement("logTrack"));
            }
            if (sTrackType ==  TypeTrack.射孔道.ToString())
            {
                trackNode.Add(new XElement("lineWidth", "2"));
            }
            if (sTrackType ==  TypeTrack.文本道.ToString())
            {
                trackNode.Add(new XElement("font-size", "6"));
                trackNode.Add(new XElement("font-color", "red"));
            }
            if (sTrackType ==  TypeTrack.解释结论道.ToString())
            {
                
            }
            XDocument XsingleWellStyleRoot = XDocument.Load(filePathSingleWellStyle);
            XElement XNode = XsingleWellStyleRoot.Element("StyleModel").Element("TrackCollection");
            XNode.Add(trackNode);
            XsingleWellStyleRoot.Save(filePathSingleWellStyle);
        }
        public static void addTrackXMLStyleLogCurve(string filePathSingleWellStyle, string sIDTrack, string sLogName, string sColorName, int iLeftValue, int iRightValue, int iLineWidth)
        {
            //传入一个参数， sIDTrack
            try
            {
                XElement logTrackXElment = new XElement("Log",
                    new XElement("LogName", sLogName),
                    new XElement("lineWidth", iLineWidth),
                    new XElement("curveColor", sColorName),
                    new XElement("leftValue", iLeftValue.ToString()),
                    new XElement("rightValue", iRightValue.ToString())
                    );

                XDocument XsingleWellStyleRoot = XDocument.Load(filePathSingleWellStyle);
                XElement XTrackStyleCollect = XsingleWellStyleRoot.Element("StyleModel").Element("TrackCollection");
                IEnumerable<XElement> trackStyleElements =
                                                         from el_style in XTrackStyleCollect.Elements()
                                                         where el_style.Element("trackID").Value.Equals(sIDTrack)
                                                         select el_style;
                foreach (XElement xl in trackStyleElements) 
                {
                    xl.Add(logTrackXElment);
                }

                XsingleWellStyleRoot.Save(filePathSingleWellStyle);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void addXMLDataCurveLog(string filePathSingleWellStyle, string sIDTrack, string sLogName, string sColorName, int iLeftValue, int iRightValue, int iLineWidth)
        {
            try
            {
                //定义并从xml文件中加载节点（根节点）

                XElement XsingleWellRoot = XElement.Load(filePathSingleWellStyle);

                XElement XTrackCollect = XsingleWellRoot.Element("TrackCollect");


                IEnumerable<XElement> awElements =
                                                         from el in XTrackCollect.Descendants()
                                                         where (string)el.Attribute("id") == sIDTrack
                                                         select el;
                foreach (XElement el in awElements)
                {
                    XElement newNode = new XElement("Log",
                     new XElement("LogName", sLogName),
                     new XElement("lineWidth", iLineWidth),
                     new XElement("curveColor", sColorName),
                     new XElement("leftValue", iLeftValue.ToString()),
                     new XElement("rightValue", iRightValue.ToString())
                    );

                    el.Add(newNode);
                }

                XsingleWellRoot.Save(filePathSingleWellStyle);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void addTrackDataLayer(string filePathSingleWellData, int iIndexTrack, string sJHSelected, int iDS1Showed, int iDS2Showed)
        {
            XElement XsingleWellRoot = XElement.Load(filePathSingleWellData);
            XElement XNode = XsingleWellRoot.Element("TrackCollect");
            cIOinputLayerDepth cSelectLayerDepth = new cIOinputLayerDepth();
            string sData = cSelectLayerDepth.selectLayerDepth2String(sJHSelected);
            XElement newNode = new XElement("Track", new XAttribute("id", "Track" + iIndexTrack.ToString()),
                 new XElement("trackType", TypeTrack.地层道.ToString()),
                  new XElement("trackWidth", "20"),
                  new XElement("data", sData)
                   );

            XNode.Add(newNode);
            XsingleWellRoot.Save(filePathSingleWellData);
        }
        public static void addTrackDataJSJL(string filePathSingleWellData, int iIndexTrack, string sJHSelected)
        {
            XElement XsingleWellRoot = XElement.Load(filePathSingleWellData);
            XElement XNode = XsingleWellRoot.Element("TrackCollect");

            string sData = cIOinputJSJL.selectJSJL2String(sJHSelected);
            XElement newNode = new XElement("Track", new XAttribute("id", "Track" + iIndexTrack.ToString()),
                 new XElement("trackType", "JSJLTrack"),
                  new XElement("trackWidth", "20"),
                   new XElement("data", sData)
                   );
            XNode.Add(newNode);
            XsingleWellRoot.Save(filePathSingleWellData);
        }
        public static void addTrackDataText(string filePathSingleWellData, int iIndexTrack, string sJHSelected, string sData)
        {

            XElement XsingleWellRoot = XElement.Load(filePathSingleWellData);
            XElement XNode = XsingleWellRoot.Element("TrackCollect");
            XElement newNode = new XElement("Track", new XAttribute("id", "Track" + iIndexTrack.ToString()),
                 new XElement("trackType", TypeTrack.文本道.ToString()  ),
                  new XElement("trackWidth", "20"),
                   new XElement("data", sData)
                   );

            XNode.Add(newNode);
            XsingleWellRoot.Save(filePathSingleWellData);
        }
        public static void addTrackDataLitho(string filePathSingleWellData, int iIndexTrack, string sJHSelected, string sData)
        {

            XElement XsingleWellRoot = XElement.Load(filePathSingleWellData);
            XElement XNode = XsingleWellRoot.Element("TrackCollect");
            XElement newNode = new XElement("Track", new XAttribute("id", "Track" + iIndexTrack.ToString()),
                 new XElement("trackType",  TypeTrack.岩性道.ToString()),
                  new XElement("trackWidth", "20"),
                   new XElement("data", sData)
                   );

            XNode.Add(newNode);
            XsingleWellRoot.Save(filePathSingleWellData);
        }
        public static void addTrackDataScatter(string filePathSingleWellData, int iIndexTrack, string sJHSelected, string sData)
        {

            XElement XsingleWellRoot = XElement.Load(filePathSingleWellData);
            XElement XNode = XsingleWellRoot.Element("TrackCollect");
            XElement newNode = new XElement("Track", new XAttribute("id", "Track" + iIndexTrack.ToString()),
                 new XElement("trackType", TypeTrack.离散数据道.ToString()),
                  new XElement("trackWidth", "20"),
                   new XElement("data", sData)
                   );

            XNode.Add(newNode);
            XsingleWellRoot.Save(filePathSingleWellData);
        }
        public static void addTrackDataPerforation(string filePathSingleWellData, int iIndexTrack, string sJHSelected)
        {
            XElement XsingleWellRoot = XElement.Load(filePathSingleWellData);
            XElement XNode = XsingleWellRoot.Element("TrackCollect");
            string sData = cIOinputWellPerforation.selectPerforation2String(sJHSelected);
      
            XElement newNode = new XElement("Track", new XAttribute("id", "Track" + iIndexTrack.ToString()),
                 new XElement("trackType", TypeTrack.射孔道.ToString()),
                  new XElement("trackWidth", "20"),
                   new XElement("data", sData)
                   );

            XNode.Add(newNode);
            XsingleWellRoot.Save(filePathSingleWellData);
        }

      

        public static void deleteSelectTrack(string filePathSingleWellStyle,string sIDtrack) 
        {
            //根据sIDtrack查找XML并删除
            XDocument XsingleWellStyleRoot = XDocument.Load(filePathSingleWellStyle);
  
            XElement XTrackStyleCollect = XsingleWellStyleRoot.Element("StyleModel").Element("TrackCollection");
            IEnumerable<XElement> trackStyleElements =
                                                     from el_style in XTrackStyleCollect.Elements()
                                                     where el_style.Element("trackID").Value.Equals(sIDtrack)
                                                     select el_style;
            trackStyleElements.Remove();
            XsingleWellStyleRoot.Save(filePathSingleWellStyle);
        }

        public static void deleteSelectLogCurve(string filePathSingleWellStyle, string sIDtrack, string sLogName)
        {
            //根据sIDtrack查找XML并删除
            XDocument XsingleWellStyleRoot = XDocument.Load(filePathSingleWellStyle);

            XElement XTrackStyleCollect = XsingleWellStyleRoot.Element("StyleModel").Element("TrackCollection");
            IEnumerable<XElement> trackStyleElements =
                                                     from el_style in XTrackStyleCollect.Elements()
                                                     where el_style.Element("trackID").Value.Equals(sIDtrack)
                                                     select el_style;

            foreach (XElement trackXL in trackStyleElements)
            {
                IEnumerable<XElement> logXL =
                                           from xl in trackXL.Descendants("Log")
                                           where xl.Element("LogName").Value.Equals(sLogName)
                                           select xl;
                logXL.Remove();
            }
            
            XsingleWellStyleRoot.Save(filePathSingleWellStyle);
        }

        public static void upSelectTrack(string filePathSingleWellStyle,string sIDtrack) 
        {
            //根据sIDtrack查找XML并上移
            XDocument XsingleWellStyleRoot = XDocument.Load(filePathSingleWellStyle);
  
            XElement XTrackStyleCollect = XsingleWellStyleRoot.Element("StyleModel").Element("TrackCollection");
        
            List<XElement> listXTrackStyleCollect=XTrackStyleCollect.Elements().ToList();
            int indexXE=0;
            for (int i = 0; i < listXTrackStyleCollect.Count;i++ )
            {
                if (listXTrackStyleCollect[i].Element("trackID").Value.Equals(sIDtrack)) 
                {
                    indexXE = i;
                    break;
                }
            }
            if (indexXE > 0) 
            {
                XElement tempXE= listXTrackStyleCollect[indexXE];
                listXTrackStyleCollect[indexXE] = listXTrackStyleCollect[indexXE-1];
                listXTrackStyleCollect[indexXE - 1] = tempXE;
            }
            XTrackStyleCollect.Elements().Remove();
            for (int i = 0; i < listXTrackStyleCollect.Count; i++) 
            {
                XTrackStyleCollect.Add(listXTrackStyleCollect[i]);
            }
            XsingleWellStyleRoot.Save(filePathSingleWellStyle);
        }

        public static void downSelectTrack(string filePathSingleWellStyle, string sIDtrack)
        {
            //根据sIDtrack查找XML并上移
            XDocument XsingleWellStyleRoot = XDocument.Load(filePathSingleWellStyle);

            XElement XTrackStyleCollect = XsingleWellStyleRoot.Element("StyleModel").Element("TrackCollection");

            List<XElement> listXTrackStyleCollect = XTrackStyleCollect.Elements().ToList();
            int indexXE = listXTrackStyleCollect.Count - 1;
            for (int i = 0; i < listXTrackStyleCollect.Count; i++)
            {
                if (listXTrackStyleCollect[i].Element("trackID").Value.Equals(sIDtrack))
                {
                    indexXE = i;
                    break;
                }
            }
            if (indexXE < listXTrackStyleCollect.Count - 1)
            {
                XElement tempXE = listXTrackStyleCollect[indexXE];
                listXTrackStyleCollect[indexXE] = listXTrackStyleCollect[indexXE+ 1];
                listXTrackStyleCollect[indexXE+ 1] = tempXE;
            }
            XTrackStyleCollect.Elements().Remove();
            for (int i = 0; i < listXTrackStyleCollect.Count; i++)
            {
                XTrackStyleCollect.Add(listXTrackStyleCollect[i]);
            }
            XsingleWellStyleRoot.Save(filePathSingleWellStyle);
        }
    
    }
}
