using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOGPlatform.SVG
{
    class cSVGscript:cSVGBase
    {
        public cSVGscript(int iDX, int iDY)
            : base(800,1000, iDX, iDY)
        {
        
        }
  
        public string sPrefix = "<![CDATA[";
        public string sPostifx = "]]>";
    }
}
