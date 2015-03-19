using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class trackLayerDepthDataList
    {
        public List<string> ltStrXCM;
        public List<float> fListDS1;
        public List<float> fListDS2;

        public static trackLayerDepthDataList setupDataListTrackLayerDepth(string filePath, float fTop, float fBase)
        {
            trackLayerDepthDataList trackDataListLayerDepth = new trackLayerDepthDataList();
            trackDataListLayerDepth.fListDS1 = new List<float>();
            trackDataListLayerDepth.fListDS2 = new List<float>();
            trackDataListLayerDepth.ltStrXCM = new List<string>();

            string sData = "";
            if (filePath != "")
            {
                using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default)) sData = sr.ReadToEnd();
            }
            string[] split = sData.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i = i + 3)
            {
                float fCurrentTop = float.Parse(split[i]);
                float fCurrentBase = float.Parse(split[i + 1]);
                if (fTop <= fCurrentTop && fCurrentBase <= fBase)
                {
                    trackDataListLayerDepth.fListDS1.Add(fCurrentTop);
                    trackDataListLayerDepth.fListDS2.Add(fCurrentBase);
                    trackDataListLayerDepth.ltStrXCM.Add(split[i + 2]);
                }
            }
            return trackDataListLayerDepth;
        }
    }
}
