using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cMenuStripDog
    {
        public cMenuStripDog(MenuStrip _menustrip) 
        {
            this.menuStrip = _menustrip;
        }
        public MenuStrip  menuStrip { get; set; }
    }
}
