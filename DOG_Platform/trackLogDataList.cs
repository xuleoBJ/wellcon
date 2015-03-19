using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class trackLogDataList
    {
        public string sLogName;
        public List<float> fListMD;
        public List<float> fListValue;

        public static trackLogDataList setupDataListTrackLog(string filePath, float fTop, float fBase)
        {
            trackLogDataList trackDataListLog = new trackLogDataList();
            trackDataListLog.fListMD = new List<float>();
            trackDataListLog.fListValue = new List<float>();

            string sData = "";
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                sData = sr.ReadToEnd();
            }

            string[] split = sData.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 1)
            {
                int iStartPosition = 1; //读取的开始字符位置
                trackDataListLog.sLogName = split[0];
                int iInterval = 2; //定义抽稀比例
                for (int i = iStartPosition; i < split.Length; i = i + 2 * iInterval)
                {
                    float fMD = float.Parse(split[i]);
                    float fValue = float.Parse(split[i + 1]);
                    if (fTop <= fMD && fMD <= fBase)
                    {
                        trackDataListLog.fListMD.Add(fMD);
                        trackDataListLog.fListValue.Add(fValue);
                    }
                }
            }
            return trackDataListLog;
        }
    }
}
