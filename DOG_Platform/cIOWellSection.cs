﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DOGPlatform.XML;
using System.Windows.Forms;
namespace DOGPlatform
{
    class cIOWellSection 
    {
      static  string fileNameSectionProfile = "profile.txt";
      static string fileNameSectionLayerDepth = "layerDepth.txt";
      static string fileNameSectionJSJL = "jsjl.txt";
      static string fileNameSectionPerforation = "inputPerforation.txt";
      
        public static void delLog(string srcDir,string sLogName)
        {
          string filePath=Path.Combine(srcDir,sLogName+".txt");
          if (File.Exists(filePath))  File.Delete(filePath); 
        }
         public static void addLog(string sJH,string sLogName,string filePath)
        {
            cIOinputLog.extractTextLog2File(sJH, sLogName, filePath);
        }
         public static void addSectionDataLayerDepth(string sJH, string dirSectionData) 
         {
             string filePath = Path.Combine(dirSectionData, sJH, fileNameSectionLayerDepth);
             cIOinputLayerDepth cSelectLayerDepth = new cIOinputLayerDepth();
             cSelectLayerDepth.selectSectionDrawData2File(sJH, filePath);
         }
         public static void addSectionDataJSJL(string sJH, string dirSectionData) 
         {
             string filePath = Path.Combine(dirSectionData, sJH, fileNameSectionJSJL);
             cIOinputJSJL.selectSectionDrawData2File(sJH, filePath);
         }
         public static void addSectionDataProfile(string sJH, string dirSectionData)
         {
             string filePath = Path.Combine(dirSectionData, sJH, fileNameSectionProfile);
             cIOinputInjectProfile.selectSectionDrawData2File(sJH, filePath);
         }
         
         public static void addSectionDataPerforation(string sJH, string dirSectionData) 
         {
             string filePath = Path.Combine(dirSectionData, sJH, fileNameSectionPerforation);
             cIOinputWellPerforation cSelectInputPerforation = new cIOinputWellPerforation();
             cSelectInputPerforation.selectSectionDrawData2File(sJH, filePath);
         }

         public static void addLogProperty(string sLogName, string sLogColor, float fRightValue, float fLeftValue, int iLeftOrRight) 
         {
             cXDocSection.addTrackLog(cProjectManager.xmlSectionCSS, "idLog#" + sLogName, 20, iLeftOrRight, sLogName, fLeftValue, fRightValue, sLogColor);
         }

         public static trackLayerDepthDataList trackDataListLayerDepth(string sJH, string dirSectionData,float  fTopShowed, float fBaseShowed )
         {
             string filePathLayer = Path.Combine(dirSectionData, sJH, fileNameSectionLayerDepth);
             return    trackLayerDepthDataList.setupDataListTrackLayerDepth(filePathLayer, fTopShowed, fBaseShowed);
         }
         public static trackJSJLDataList trackDataListJSJL(string sJH, string dirSectionData, float fTopShowed, float fBaseShowed)
         {
             string filePathJSJL = Path.Combine(dirSectionData, sJH, fileNameSectionJSJL);
             return trackJSJLDataList.setupDataListTrack(filePathJSJL, fTopShowed, fBaseShowed);
         }

         public static trackInputPerforationDataList trackDataListPerforation(string sJH, string dirSectionData, float fTopShowed, float fBaseShowed)
         {
         string filePathInputPerforation = Path.Combine(dirSectionData, sJH, fileNameSectionPerforation);
         return trackInputPerforationDataList.setupDataListTrack(filePathInputPerforation, fTopShowed, fBaseShowed);
         }


         public static trackProfileDataList trackDataListProfile(string sJH, string dirSectionData, float fTopShowed, float fBaseShowed)
         {
             string filePathProfile = Path.Combine(dirSectionData, sJH, fileNameSectionProfile);
             return trackProfileDataList.setupDataListTrack(filePathProfile, fTopShowed, fBaseShowed);
         }
        
      
        
    }
}
