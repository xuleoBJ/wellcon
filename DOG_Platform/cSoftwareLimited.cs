using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOGPlatform
{
    class cSoftwareLimited
    {
        public static bool  limitedDay()
        {
            bool bValidDay = true;
            int iValidEndDay=20150630;
            int iToday = int.Parse(DateTime.Today.ToString("yyyyMMdd"));
            if (iToday>= iValidEndDay)
               bValidDay =false ;
            return bValidDay;
        }
    }
}
