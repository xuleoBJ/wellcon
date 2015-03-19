using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cMakeSingleWellGraph
    {
        public static string makeSingleWellGraph(string filenameSVGMap, string filepathStyleXML, string filepathDataXML) 
        {
            List<int> iListTrackWidth = new List<int>();

            XDocument XsingleWellRootData = XDocument.Load(filepathDataXML);
            XDocument XsingleWellRootStyle = XDocument.Load(filepathStyleXML);

            int iDS1ShowDepthXML = int.Parse(XsingleWellRootStyle.Element("StyleModel").Element("iShowedTop").Value);
            int iDS2ShowDepthXML = int.Parse(XsingleWellRootStyle.Element("StyleModel").Element("iShowedBottom").Value);
            float fVScaleXML = float.Parse(XsingleWellRootStyle.Element("StyleModel").Element("fVScale").Value);
            string sJHSelected = XsingleWellRootData.Element("DataModel").Element("WellName").Value;

            cSingleWellDoc cSingleWell = new cSingleWellDoc(800,10000,20, 20);
            cSingleWell.initializeBaseMapInfor(iDS1ShowDepthXML, iDS2ShowDepthXML, fVScaleXML);
            iListTrackWidth.Clear();


            XmlElement returnElemment;
           
            XElement XTrackStyleCollect = XsingleWellRootStyle.Element("StyleModel").Element("TrackCollection");
            IEnumerable<XElement> trackStyleElements =
                                                     from el_style in XTrackStyleCollect.Descendants()
                                                     where el_style.Name == "Track"
                                                     select el_style;

            foreach (XElement el_style in trackStyleElements)
            {
                string sTrackTpye = el_style.Element("trackType").Value;
                string sTrackID = el_style.Element("trackID").Value;
                string sTrackName = el_style.Element("trackName").Value;
                int iTrackWidth = int.Parse(el_style.Element("trackWidth").Value);
                if (el_style.Element("trackType").Value == TypeTrack.深度道.ToString())
                {
                    iTrackWidth = 15;
                    returnElemment = cSingleWell.addTrackRectWithTitle("深度", iDS1ShowDepthXML, iDS2ShowDepthXML, iTrackWidth);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    returnElemment = cSingleWell.gMDRuler(iDS1ShowDepthXML, iDS2ShowDepthXML, 10, 5);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    iListTrackWidth.Add(iTrackWidth);
                }
                if (el_style.Element("trackType").Value == TypeTrack.解释结论道.ToString())
                {
                    iTrackWidth = 10;
                    returnElemment = cSingleWell.addTrackRectWithTitle(sTrackName, iDS1ShowDepthXML, iDS2ShowDepthXML, iTrackWidth);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    string sData = "";
                    XElement XTrackDataCollect = XsingleWellRootData.Element("DataModel").Element("TrackCollectionData");
                    IEnumerable<XElement> trackDataElements =
                                                             from el_data in XTrackDataCollect.Descendants()
                                                             where el_data.Name == "Track"
                                                             select el_data;
                    foreach (XElement el_data in trackDataElements)
                    {
                        if (el_data.Element("trackID").Value == sTrackID)
                        {
                            sData = el_data.Element("data").Value;
                        }
                    }
                    trackJSJLDataList sttTrackDataListJSJL = cDirDataSourceSingleWell.getTrackJSJLDataList(sData, iDS1ShowDepthXML, iDS2ShowDepthXML);
                    if (sttTrackDataListJSJL.fListDS1 != null)
                    {
                        returnElemment = cSingleWell.gTrackJSJL(sttTrackDataListJSJL.fListDS1, sttTrackDataListJSJL.fListDS2, sttTrackDataListJSJL.iListJSJL, iTrackWidth);
                        cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    }

                    iListTrackWidth.Add(iTrackWidth);
                }
                if (el_style.Element("trackType").Value == TypeTrack.地层道.ToString())
                {
                    iTrackWidth = 15;
                    returnElemment = cSingleWell.addTrackRectWithTitle(sTrackName, iDS1ShowDepthXML, iDS2ShowDepthXML, iTrackWidth);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());

                    string sData = "";
                    XElement XTrackDataCollect = XsingleWellRootData.Element("DataModel").Element("TrackCollectionData");
                    IEnumerable<XElement> trackDataElements =
                                                             from el_data in XTrackDataCollect.Descendants()
                                                             where el_data.Name == "Track"
                                                             select el_data;
                    foreach (XElement el_data in trackDataElements)
                    {
                        if (el_data.Element("trackID").Value == sTrackID)
                        {
                            sData = el_data.Element("data").Value;
                        }

                    }
                    trackLayerDepthDataList sttTrackDataListLayerDepth = cDirDataSourceSingleWell.getTrackLayerDataList(sData, iDS1ShowDepthXML, iDS2ShowDepthXML);

                    if (sttTrackDataListLayerDepth.fListDS1 != null)
                    {
                        returnElemment = cSingleWell.gTrackLayerDepth(sttTrackDataListLayerDepth.fListDS1,
                        sttTrackDataListLayerDepth.fListDS2, sttTrackDataListLayerDepth.ltStrXCM, iTrackWidth);
                        cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    }

                    iListTrackWidth.Add(iTrackWidth);
                }
                if (el_style.Element("trackType").Value == TypeTrack.射孔道.ToString())
                {
                    iTrackWidth = 10;
                    returnElemment = cSingleWell.addTrackRectWithTitle(sTrackName, iDS1ShowDepthXML, iDS2ShowDepthXML, iTrackWidth);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    string sData = "";
                    XElement XTrackDataCollect = XsingleWellRootData.Element("DataModel").Element("TrackCollectionData");
                    IEnumerable<XElement> trackDataElements =
                                                             from el_data in XTrackDataCollect.Descendants()
                                                             where el_data.Name == "Track"
                                                             select el_data;
                    foreach (XElement el_data in trackDataElements)
                    {
                        if (el_data.Element("trackID").Value == sTrackID)
                        {
                            sData = el_data.Element("data").Value;
                        }

                    }
                    trackInputPerforationDataList sttTrackDataListPerforation = cDirDataSourceSingleWell.gettrackInputPerforationDataList(sData, iDS1ShowDepthXML, iDS2ShowDepthXML);
                    if (sttTrackDataListPerforation.fListDS1 != null)
                    {
                        returnElemment = cSingleWell.gTrackPerfoation(sttTrackDataListPerforation.fListDS1, sttTrackDataListPerforation.fListDS2, iTrackWidth);
                        cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    }

                    iListTrackWidth.Add(iTrackWidth);
                }
                if (el_style.Element("trackType").Value == TypeTrack.岩性道.ToString())
                {
                    iTrackWidth = 15;
                    returnElemment = cSingleWell.addTrackRectWithTitle("岩性", iDS1ShowDepthXML, iDS2ShowDepthXML, iTrackWidth);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());

                    string sData = el_style.Element("data").Value;
                    trackLithoDataList sttTrackDataListLitho = cDirDataSourceSingleWell.getTrackLithoDataList(sData, iDS1ShowDepthXML, iDS2ShowDepthXML);
                    if (sttTrackDataListLitho.fListDS1 != null)
                    {
                        returnElemment = cSingleWell.gTrackLitho(sttTrackDataListLitho.fListDS1, sttTrackDataListLitho.fListDS2, sttTrackDataListLitho.iListLithoType, iTrackWidth);
                        cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    }
                    iListTrackWidth.Add(iTrackWidth);
                }
                if (el_style.Element("trackType").Value == TypeTrack.文本道.ToString())
                {
                    iTrackWidth = 15;
                    returnElemment = cSingleWell.addTrackRectWithTitle("文本道", iDS1ShowDepthXML, iDS2ShowDepthXML, iTrackWidth);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    string sData = el_style.Element("data").Value;
                    trackTextDataList sttTrackDataListText = cDirDataSourceSingleWell.getTrackTextDataList(sData, iDS1ShowDepthXML, iDS2ShowDepthXML);
                    if (sttTrackDataListText.fListDS1 != null)
                    {
                        returnElemment = cSingleWell.gTrackText(sttTrackDataListText.fListDS1, sttTrackDataListText.fListDS2, sttTrackDataListText.ltStrText, iTrackWidth);
                        cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    }
                    iListTrackWidth.Add(iTrackWidth);
                }
                if (el_style.Element("trackType").Value == TypeTrack.离散数据道.ToString())
                {
                    iTrackWidth = 30;

                    returnElemment = cSingleWell.addTrackVerticalGrid(0, iDS1ShowDepthXML, iTrackWidth, iDS2ShowDepthXML - iDS1ShowDepthXML);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    returnElemment = cSingleWell.addTrackHorizonalGrid(iDS1ShowDepthXML, iDS2ShowDepthXML, iTrackWidth);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    returnElemment = cSingleWell.addTrackItemLogHeadInfor("杆状图", iDS1ShowDepthXML, iTrackWidth, 0, 100, "red");

                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    string sData = el_style.Element("data").Value;
                    trackScatterDataList sttTrackDataListScatter = cDirDataSourceSingleWell.getTrackScatterDataList(sData, iDS1ShowDepthXML, iDS2ShowDepthXML);
                    if (sttTrackDataListScatter.fListDS1 != null)
                    {
                        int iLeftValue = 0;
                        int iRightValue = 100;
                        string sColor = "red";
                        returnElemment = cSingleWell.gTrackScatter("散点图", sttTrackDataListScatter.fListDS1, sttTrackDataListScatter.fListValue, iLeftValue, iRightValue, sColor, iTrackWidth);
                        cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    }
                    iListTrackWidth.Add(iTrackWidth);
                }
                if (el_style.Element("trackType").Value == TypeTrack.曲线道.ToString())
                {
                    IEnumerable<XElement> trackStyleElementsLog =
                                                     from el_log in el_style.Descendants()
                                                     where el_log.Name == "Log"
                                                     select el_log;

                    iTrackWidth = int.Parse(el_style.Element("trackWidth").Value);
                    returnElemment = cSingleWell.addTrackVerticalGrid(0, iDS1ShowDepthXML, iTrackWidth, iDS2ShowDepthXML - iDS1ShowDepthXML);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    returnElemment = cSingleWell.addTrackHorizonalGrid(iDS1ShowDepthXML, iDS2ShowDepthXML, iTrackWidth);
                    cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    int iLogNum = 0;
                    foreach (XElement el_log in trackStyleElementsLog)
                    {
                        iLogNum++;
                        string sLogname = el_log.Element("LogName").Value;
                        int iLeftValue = int.Parse(el_log.Element("leftValue").Value);
                        int iRightValue = int.Parse(el_log.Element("rightValue").Value);
                        string sColor = el_log.Element("curveColor").Value;
                        if (iLogNum == 1)
                        {
                            returnElemment = cSingleWell.addTrackLogRectWithTitle(sLogname, iDS1ShowDepthXML, iDS2ShowDepthXML, iTrackWidth,
                                 iLeftValue, iRightValue, sColor);
                            cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                        }

                        returnElemment = cSingleWell.addTrackItemLogHeadInfor(sLogname, iDS1ShowDepthXML - 6 * (iLogNum - 1), iTrackWidth,
          iLeftValue, iRightValue, sColor);
                        cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());

                        trackLogDataList sttTrackDataListLog = cDirDataSourceSingleWell.getLogSeriers(sJHSelected, sLogname, iDS1ShowDepthXML, iDS2ShowDepthXML);
                        returnElemment = cSingleWell.gTrackLog(sLogname, sttTrackDataListLog.fListMD, sttTrackDataListLog.fListValue,
                            iLeftValue, iRightValue, sColor, iTrackWidth);
                        cSingleWell.addgElement2LayerBase(returnElemment, iListTrackWidth.Sum());
                    }
                    iListTrackWidth.Add(iTrackWidth);
                }

            }
            cSingleWell.makeSVGfile(cProjectManager.dirPathMap + filenameSVGMap);
            return cProjectManager.dirPathMap + filenameSVGMap;
        }
    }
}
