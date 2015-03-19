using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class trackProfileDataList
    {
        public List<float> fListDS1;
        public List<float> fListDS2;
        public List<float> fListPercent;
        public List<float> fListZRL;


        public static trackProfileDataList setupDataListTrack(string filePath, float fTop, float fBase)
        {
            trackProfileDataList trackDataList = new trackProfileDataList();
            trackDataList.fListDS1 = new List<float>();
            trackDataList.fListDS2 = new List<float>();
            trackDataList.fListPercent = new List<float>();
            trackDataList.fListZRL = new List<float>();
            if (File.Exists(filePath))
            {
                string sData = "";
                using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
                {
                    sData = sr.ReadToEnd();
                }

                string[] split = sData.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < split.Length; i = i + 9)
                {
                    string sJH = split[i];
                    float fCurrentTop = float.Parse(split[i + 2]);
                    float fCurrentBase = float.Parse(split[i + 3]);
                    float fZRL = float.Parse(split[i + 4]);
                   float fPercent =  float.Parse(split[i + 5]);

                    if (fTop <= fCurrentTop && fCurrentBase <= fBase)
                    {
                        trackDataList.fListDS1.Add(fCurrentTop);
                        trackDataList.fListDS2.Add(fCurrentBase);
                        trackDataList.fListPercent.Add(fPercent);
                        trackDataList.fListZRL.Add(fZRL);
                    }
                }
            }
            return trackDataList;
        }

    }
}
