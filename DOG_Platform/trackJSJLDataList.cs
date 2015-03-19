using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class trackJSJLDataList
    {
        
            public List<float> fListDS1;
            public List<float> fListDS2;
            public List<int> iListJSJL;

            public static trackJSJLDataList setupDataListTrack(string filePath, float fTop, float fBase)
            {
                trackJSJLDataList trackDataListJSJL = new trackJSJLDataList();
                trackDataListJSJL.fListDS1 = new List<float>();
                trackDataListJSJL.fListDS2 = new List<float>();
                trackDataListJSJL.iListJSJL = new List<int>();
                if (!File.Exists(filePath)) return trackDataListJSJL;
                string sData = "";

                using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
                {
                    sData = sr.ReadToEnd();
                }

                string[] split = sData.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < split.Length; i = i + 3)
                {
                    float fCurrentTop = float.Parse(split[i]);
                    float fCurrentBase = float.Parse(split[i + 1]);
                    int iJSJL = int.Parse(split[i + 2]);
                    if (fTop <= fCurrentTop && fCurrentBase <= fBase)
                    {
                        trackDataListJSJL.fListDS1.Add(fCurrentTop);
                        trackDataListJSJL.fListDS2.Add(fCurrentBase);
                        trackDataListJSJL.iListJSJL.Add(iJSJL);
                    }
                }
                return trackDataListJSJL;
            }
        
    }
}
