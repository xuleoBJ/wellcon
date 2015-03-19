using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOGPlatform
{
    class  ItemLogHeadInfor
    {
            public string sJH;
            public string sLogName;
            public string sUnit;
            public float fLeftValue;
            public float fRightValue;
            public int iIsLog;
            public string sLogColor;
            public float fLineWidth;
            public int iLineType;

            public static string item2Line(ItemLogHeadInfor itemLogHeadInfor)
            {
                List<string> ltStrWrited = new List<string>();
                ltStrWrited.Add(itemLogHeadInfor.sJH);
                ltStrWrited.Add(itemLogHeadInfor.sLogName);
                ltStrWrited.Add(itemLogHeadInfor.sUnit);
                ltStrWrited.Add(itemLogHeadInfor.fLeftValue.ToString());
                ltStrWrited.Add(itemLogHeadInfor.fRightValue.ToString());
                ltStrWrited.Add(itemLogHeadInfor.iIsLog.ToString());
                ltStrWrited.Add(itemLogHeadInfor.sLogColor);
                ltStrWrited.Add(itemLogHeadInfor.fLineWidth.ToString());
                ltStrWrited.Add(itemLogHeadInfor.iLineType.ToString());
                return string.Join("\t", ltStrWrited.ToArray());
            }
      
    }
}
