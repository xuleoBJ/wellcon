using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOGPlatform.SVG
{
    class cSVGSectionTrack : cSVGBase
    {
        public cSVGSectionTrack() 
        {
            this.iTrackWidth = 15;
        }
        public cSVGSectionTrack(int _iTrackWidth)
        {
            this.iTrackWidth = _iTrackWidth;
        }
        public int iTrackWidth { get; set; }
    }
}
