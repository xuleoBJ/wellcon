using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class cDirDataSourceSingleWell
    {
        static string sLayDepthFileMark = "#$LayerDepthTrack";
        static string sJSJLFileMark = "#$JSJLTrack";
        static string sPerforationFileMark = "#$PerforationTrack";
        static string sLithoFileMark = "#$LithoTrack";
        static string sTextFileMark = "#$TextTrack";
        public static int addJSJL(string sJHSelected, int iDS1Showed,int iDS2Showed,int iIndexTrack,string filePath) 
        {
            string sContent = cIOinputJSJL.selectIntervalDataFromJSJLByJHAndDepth(sJHSelected, iDS1Showed, iDS2Showed);
            write2File(iIndexTrack, sContent, filePath, sJSJLFileMark);
            return iIndexTrack++;
        }

        public static int addJSJL(string sJHSelected, int iIndexTrack, string filePath)
        {
            string sContent = cIOinputJSJL.selectIntervalDataFromJSJLByJHAndDepth(sJHSelected, 0, 10000);
            write2File(iIndexTrack, sContent, filePath, sJSJLFileMark);
            return iIndexTrack++;
        }
        public static int addText(string filepathSource, int iIndexTrack, string filePath)
        {
            string sContent = "";
            using (StreamReader sr = new StreamReader(filepathSource, Encoding.Default))
            {
                String line;
                int iLineindex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    iLineindex++;
                    sContent+=line+"\r\n";
                }
            }

            write2File(iIndexTrack, sContent, filePath, sTextFileMark);
            return iIndexTrack++;
        }

        public static int addLitho(string filepathSource, int iIndexTrack, string filePath)
        {
            string sContent = "";
            using (StreamReader sr = new StreamReader(filepathSource, Encoding.Default))
            {
                String line;
                int iLineindex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    iLineindex++;
                    sContent += line + "\r\n";
                }
            }

            write2File(iIndexTrack, sContent, filePath, sLithoFileMark);
            return iIndexTrack++;
        }

        public static int addLayerDepth(string sJHSelected, int iDS1Showed, int iDS2Showed, int iIndexTrack,  string filePath)
        {
            cIOinputLayerDepth cSelectLayerDepth = new cIOinputLayerDepth();
            string sContent = cSelectLayerDepth.selectIntervalDataFromLayerDepthByJHAndDepth(sJHSelected, iDS1Showed, iDS2Showed);
            write2File( iIndexTrack, sContent, filePath, sLayDepthFileMark);
            return iIndexTrack++;
        }

        public static int addLayerDepth(string sJHSelected,  int iIndexTrack, string filePath)
        {
            cIOinputLayerDepth cSelectLayerDepth = new cIOinputLayerDepth();
            string sContent = cSelectLayerDepth.selectIntervalDataFromLayerDepthByJHAndDepth(sJHSelected, 0, 10000);
            write2File(iIndexTrack, sContent, filePath, sLayDepthFileMark);
            return iIndexTrack++;
        }

        public static int addPerforation(string sJHSelected, int iDS1Showed, int iDS2Showed, int iIndexTrack, string filePath)
        {
            string sContent = cIOinputWellPerforation.selectPerforation2String(sJHSelected);
             write2File(iIndexTrack, sContent, filePath, sPerforationFileMark);
             return iIndexTrack++;
        }

        private static void write2File(int iIndexTrack, string sContent, string filePath, string sMark) 
        {
            StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8);
            sw.WriteLine(sMark + "\t" + "Track" + iIndexTrack.ToString());
            sw.Write(sContent);
            sw.WriteLine("#$End" + sMark);
            sw.Close();
        }

        public static trackLayerDepthDataList getTrackDataLayerDepth(string filePath)
        {
            trackLayerDepthDataList sttLayerDepth = new trackLayerDepthDataList();
            sttLayerDepth.fListDS1 = new List<float>();
            sttLayerDepth.fListDS2 = new List<float>();
            sttLayerDepth.ltStrXCM = new List<string>();
          
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                string[] split;
                int iReadIndex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    if (line.StartsWith(sLayDepthFileMark) == true) iReadIndex = 1;
                    else if (line.StartsWith("#$End" + sLayDepthFileMark) == true) iReadIndex = 2;
                    else if (iReadIndex >= 2) break;
                    else if (iReadIndex == 1)
                    {
                        split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        sttLayerDepth.ltStrXCM.Add(split[1]);
                        sttLayerDepth.fListDS1.Add(float.Parse(split[2]));
                        sttLayerDepth.fListDS2.Add(float.Parse(split[3]));

                    }

                }
            }
            return sttLayerDepth;
        }

        public static trackLayerDepthDataList getTrackDataLayerDepth(string filePath, string sIDTrack)
        {
            trackLayerDepthDataList sttLayerDepth = new trackLayerDepthDataList();
            sttLayerDepth.fListDS1 = new List<float>();
            sttLayerDepth.fListDS2 = new List<float>();
            sttLayerDepth.ltStrXCM = new List<string>();

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                string[] split;
                int iReadIndex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (line.StartsWith(sLayDepthFileMark) == true && split[1] == sIDTrack) iReadIndex = 1;
                    else if (line.StartsWith("#$End" + sLayDepthFileMark) == true) iReadIndex = 2;
                    else if (iReadIndex >= 2) break;
                    else if (iReadIndex == 1)
                    {
                        
                        sttLayerDepth.ltStrXCM.Add(split[1]);
                        sttLayerDepth.fListDS1.Add(float.Parse(split[2]));
                        sttLayerDepth.fListDS2.Add(float.Parse(split[3]));

                    }

                }
            }
            return sttLayerDepth;
        }

        public static trackJSJLDataList getTrackDataJSJL(string filePath)
        {
            trackJSJLDataList sttJSJL = new trackJSJLDataList();
            sttJSJL.fListDS1 = new List<float>();
            sttJSJL.fListDS2 = new List<float>();
            sttJSJL.iListJSJL = new List<int>();

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                string[] split;
                int iReadIndex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    if (line.StartsWith(sJSJLFileMark) == true) iReadIndex = 1;
                    else if (line.StartsWith("#$End" + sJSJLFileMark) == true) iReadIndex = 2;
                    else if (iReadIndex >= 2) break;
                    else if (iReadIndex == 1)
                    {
                        split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        sttJSJL.fListDS1.Add(float.Parse(split[1]));
                        sttJSJL.fListDS2.Add(float.Parse(split[2]));
                        sttJSJL.iListJSJL.Add(int.Parse(split[3]));

                    }

                }
            }
            return sttJSJL;
        }

        public static trackJSJLDataList getTrackDataJSJL(string filePath,string sIDTrack)
        {
            trackJSJLDataList sttJSJL = new trackJSJLDataList();
            sttJSJL.fListDS1 = new List<float>();
            sttJSJL.fListDS2 = new List<float>();
            sttJSJL.iListJSJL = new List<int>();

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                string[] split;
                int iReadIndex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (line.StartsWith(sJSJLFileMark) == true && split[1] == sIDTrack) iReadIndex = 1;
                    else if (line.StartsWith("#$End" + sJSJLFileMark) == true) iReadIndex = 2;
                    else if (iReadIndex >= 2) break;
                    else if (iReadIndex == 1)
                    {
                        sttJSJL.fListDS1.Add(float.Parse(split[1]));
                        sttJSJL.fListDS2.Add(float.Parse(split[2]));
                        sttJSJL.iListJSJL.Add(int.Parse(split[3]));

                    }

                }
            }
            return sttJSJL;
        }

        public static trackJSJLDataList getTrackDataJSJL(string filePath, int iDS1Showed, int iDS2Showed)
        {
            trackJSJLDataList sttJSJL = new trackJSJLDataList();
            sttJSJL.fListDS1 = new List<float>();
            sttJSJL.fListDS2 = new List<float>();
            sttJSJL.iListJSJL = new List<int>();

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                string[] split;
                int iReadIndex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    if (line.StartsWith(sJSJLFileMark) == true) iReadIndex = 1;
                    else if (line.StartsWith("#$End" + sJSJLFileMark) == true) iReadIndex = 2;
                    else if (iReadIndex >= 2) break;
                    else if (iReadIndex == 1)
                    {
                        split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        float _top = float.Parse(split[1]);
                        float _bottom = float.Parse(split[2]);

                        if (!(iDS2Showed <= _top || iDS1Showed >= _bottom))
                        {
                            sttJSJL.fListDS1.Add(float.Parse(split[1]));
                            sttJSJL.fListDS2.Add(float.Parse(split[2]));
                            sttJSJL.iListJSJL.Add(int.Parse(split[3]));
                        }


                    }

                }
            }
            return sttJSJL;
        }

        public static trackInputPerforationDataList getTrackDataPerforation(string filePath)
        {
            trackInputPerforationDataList sttPeforation = new trackInputPerforationDataList();
            sttPeforation.fListDS1 = new List<float>();
            sttPeforation.fListDS2 = new List<float>();
       
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                string[] split;
                int iReadIndex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    if (line.StartsWith(sPerforationFileMark) == true) iReadIndex = 1;
                    else if (line.StartsWith("#$End" + sPerforationFileMark) == true) iReadIndex = 2;
                    else if (iReadIndex >= 2) break;
                    else if (iReadIndex == 1)
                    {
                        split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        sttPeforation.fListDS1.Add(float.Parse(split[1]));
                        sttPeforation.fListDS2.Add(float.Parse(split[2]));

                    }

                }
            }
            return sttPeforation;
        }

        public static trackInputPerforationDataList getTrackDataPerforation(string filePath, string sIDTrack)
        {
            trackInputPerforationDataList sttPeforation = new trackInputPerforationDataList();
            sttPeforation.fListDS1 = new List<float>();
            sttPeforation.fListDS2 = new List<float>();

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                string[] split;
                int iReadIndex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (line.StartsWith(sPerforationFileMark) == true && split[1] == sIDTrack) iReadIndex = 1;
                    else if (line.StartsWith("#$End" + sPerforationFileMark) == true) iReadIndex = 2;
                    else if (iReadIndex >= 2) break;
                    else if (iReadIndex == 1)
                    {
                        sttPeforation.fListDS1.Add(float.Parse(split[1]));
                        sttPeforation.fListDS2.Add(float.Parse(split[2]));
                    }

                }
            }
            return sttPeforation;
        }

        public static trackInputPerforationDataList getTrackDataPerforation(string filePath, int iDS1Showed, int iDS2Showed)
        {
            trackInputPerforationDataList sttPeforation = new trackInputPerforationDataList();
            sttPeforation.fListDS1 = new List<float>();
            sttPeforation.fListDS2 = new List<float>();

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                string[] split;
                int iReadIndex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    if (line.StartsWith(sPerforationFileMark) == true) iReadIndex = 1;
                    else if (line.StartsWith("#$End" + sPerforationFileMark) == true) iReadIndex = 2;
                    else if (iReadIndex >= 2) break;
                    else if (iReadIndex == 1)
                    {
                        split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        float _top = float.Parse(split[1]);
                        float _bottom = float.Parse(split[2]);

                        if (!(iDS2Showed <= _top || iDS1Showed >= _bottom))
                        {
                            sttPeforation.fListDS1.Add(float.Parse(split[1]));
                            sttPeforation.fListDS2.Add(float.Parse(split[2]));
                        }

            

                    }

                }
            }
            return sttPeforation;
        }

        public static trackLithoDataList getTrackDataLitho(string filePath, int iDS1Showed, int iDS2Showed)
        {
            trackLithoDataList sttLitho = new trackLithoDataList();
            sttLitho.fListDS1 = new List<float>();
            sttLitho.fListDS2 = new List<float>();
            sttLitho.iListLithoType = new List<int>();

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                string[] split;
                int iReadIndex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    if (line.StartsWith(sLithoFileMark) == true) iReadIndex = 1;
                    else if (line.StartsWith("#$End" + sLithoFileMark) == true) iReadIndex = 2;
                    else if (iReadIndex >= 2) break;
                    else if (iReadIndex == 1  )
                    {
                        split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        float _top = float.Parse(split[0]);
                        float _bottom = float.Parse(split[1]);

                        if(!(iDS2Showed <= _top || iDS1Showed >= _bottom)){
                            sttLitho.fListDS1.Add(_top);
                            sttLitho.fListDS2.Add(_bottom);
                            sttLitho.iListLithoType.Add(int.Parse(split[2]));
                        }
                    }

                }
            }
            return sttLitho;
        }

        public static trackTextDataList getTrackDataText(string filePath, int iDS1Showed, int iDS2Showed)
        {
            trackTextDataList sttText = new trackTextDataList();
            sttText.fListDS1 = new List<float>();
            sttText.fListDS2 = new List<float>();
            sttText.ltStrText  = new List<string>();

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                string[] split;
                int iReadIndex = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    if (line.StartsWith(sTextFileMark) == true) iReadIndex = 1;
                    else if (line.StartsWith("#$End" + sTextFileMark) == true) iReadIndex = 2;
                    else if (iReadIndex >= 2) break;
                    else if (iReadIndex == 1)
                    {
                        split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        float _top = float.Parse(split[0]);
                        float _bottom = float.Parse(split[1]);

                        if (!(iDS2Showed <= _top || iDS1Showed >= _bottom))
                        {
                            sttText.fListDS1.Add(_top);
                            sttText.fListDS2.Add(_bottom);
                            sttText.ltStrText.Add(split[2]);
                        }
                    }

                }
            }
            return sttText;
        }

        public static trackLogDataList getLogSeriers(string sJHSelected, string sLogname, int iDS1Showed, int iDS2Showed) 
        {
            trackLogDataList sttTrackLogDataList = new trackLogDataList();
            sttTrackLogDataList.fListMD = new List<float>();
            sttTrackLogDataList.fListValue = new List<float>();
            
            string filePathLogTXT = Path.Combine(cProjectManager.dirPathLog, sJHSelected + "_$#log");
            if (File.Exists(filePathLogTXT))
            {

                using (StreamReader sr = new StreamReader(filePathLogTXT, Encoding.UTF8))
                {
                    String line;
                    string[] split;
                    int iLineIndex = 0;
                    int iIndex = 0;
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        iLineIndex++;
                        split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (iLineIndex == 1)
                        {
                           
                            iIndex = split.ToList().IndexOf(sLogname);
                        }
                        else if (iLineIndex % 2 == 0 ) //抽稀一半点数。 
                        {
                            float fDepth = float.Parse(split[0]);
                            if (fDepth >= iDS1Showed && fDepth <= iDS2Showed)
                            {
                                sttTrackLogDataList.fListMD.Add(fDepth);
                                if (iIndex >= 0)
                                    sttTrackLogDataList.fListValue.Add(float.Parse(split[iIndex]));
                                else
                                    sttTrackLogDataList.fListValue.Add(-999);
                            }
                        }
                    }
                }
            }
            return sttTrackLogDataList;
        }

        public static trackJSJLDataList getTrackJSJLDataList(string sData, int iDS1Showed, int iDS2Showed)
        {
            trackJSJLDataList sttTrackDataList = new trackJSJLDataList();
            sttTrackDataList.fListDS1 = new List<float>();
            sttTrackDataList.fListDS2 = new List<float>();
            sttTrackDataList.iListJSJL = new List<int>();

            string[] split = sData.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i = i + 3)
            {  
                float top= float.Parse(split[i]);
                float bottom=float.Parse(split[i + 1]);
                if ( iDS1Showed<=top &&bottom<= iDS2Showed)
                {
                    sttTrackDataList.fListDS1.Add(top);
                    sttTrackDataList.fListDS2.Add(bottom);
                    sttTrackDataList.iListJSJL.Add(int.Parse(split[i + 2]));
                }
            }
            return sttTrackDataList;
        }

        public static trackInputPerforationDataList gettrackInputPerforationDataList(string sData, int iDS1Showed, int iDS2Showed)
        {
            trackInputPerforationDataList sttTrackDataList = new trackInputPerforationDataList();
            sttTrackDataList.fListDS1 = new List<float>();
            sttTrackDataList.fListDS2 = new List<float>();
   

            string[] split = sData.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i = i + 3)
            {  
                float top= float.Parse(split[i]);
                float bottom=float.Parse(split[i + 1]);
                if (!(iDS2Showed <= top || iDS1Showed >= bottom))
                {
                    sttTrackDataList.fListDS1.Add(top);
                    sttTrackDataList.fListDS2.Add(bottom);
                }
            }
            return sttTrackDataList;
        }

        public static trackLithoDataList getTrackLithoDataList(string sData, int iDS1Showed, int iDS2Showed)
        {
            trackLithoDataList sttTrackDataList = new trackLithoDataList();
            sttTrackDataList.fListDS1 = new List<float>();
            sttTrackDataList.fListDS2 = new List<float>();
            sttTrackDataList.iListLithoType = new List<int>();

            string[] split = sData.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i = i + 3)
            {
                float top = float.Parse(split[i]);
                float bottom = float.Parse(split[i + 1]);
                if (!(iDS2Showed <= top || iDS1Showed >= bottom))
                {
                    sttTrackDataList.fListDS1.Add(top);
                    sttTrackDataList.fListDS2.Add(bottom);
                    sttTrackDataList.iListLithoType.Add(int.Parse(split[i + 2]));
                }
            }
            return sttTrackDataList;
        }

        public static trackTextDataList getTrackTextDataList(string sData, int iDS1Showed, int iDS2Showed)
        {
            trackTextDataList sttTrackDataList = new trackTextDataList();
            sttTrackDataList.fListDS1 = new List<float>();
            sttTrackDataList.fListDS2 = new List<float>();
            sttTrackDataList.ltStrText = new List<string>();

            string[] split = sData.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i = i + 3)
            {
                float top = float.Parse(split[i]);
                float bottom = float.Parse(split[i + 1]);
                if (!(iDS2Showed <= top || iDS1Showed >= bottom))
                {
                    sttTrackDataList.fListDS1.Add(top);
                    sttTrackDataList.fListDS2.Add(bottom);
                    sttTrackDataList.ltStrText.Add(split[i + 2]);
                }
            }
            return sttTrackDataList;
        }

        public static trackScatterDataList getTrackScatterDataList(string sData, int iDS1Showed, int iDS2Showed)
        {
            trackScatterDataList sttTrackDataList = new trackScatterDataList();
            sttTrackDataList.fListDS1 = new List<float>();
            sttTrackDataList.fListValue = new List<float>();

            string[] split = sData.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i = i + 2)
            {
                float top = float.Parse(split[i]);
                float value = float.Parse(split[i + 1]);
                if (iDS1Showed <= top && top <= iDS2Showed)
                {
                    sttTrackDataList.fListDS1.Add(top);
                    sttTrackDataList.fListValue.Add(value);
                }
            }
            return sttTrackDataList;
        }
        public static trackLayerDepthDataList  getTrackLayerDataList(string sData, int iDS1Showed, int iDS2Showed)
        {
            trackLayerDepthDataList sttTrackDataList = new trackLayerDepthDataList();
            sttTrackDataList.fListDS1 = new List<float>();
            sttTrackDataList.fListDS2 = new List<float>();
            sttTrackDataList.ltStrXCM = new List<string>();

            string[] split = sData.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i = i + 3)
            {  
                float top= float.Parse(split[i]);
                float bottom=float.Parse(split[i + 1]);
                if ( iDS1Showed<=top &&bottom<= iDS2Showed)
                {
                    sttTrackDataList.fListDS1.Add(top);
                    sttTrackDataList.fListDS2.Add(bottom);
                    sttTrackDataList.ltStrXCM.Add(split[i + 2]);
                }
            }
            return sttTrackDataList;
        }
        
    }
}
