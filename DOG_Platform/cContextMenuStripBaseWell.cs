using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cContextMenuStripBaseWell
    {
        public ContextMenuStrip cms { get; set; }
       
        public cContextMenuStripBaseWell(ContextMenuStrip _cms)
        {
            this.cms = _cms;
        }

        public cContextMenuStripBaseWell(ContextMenuStrip _cms, TreeNode _tnSelected, string _sJH):this(_cms)
        {
            this.sJH = _sJH;
            this.tnSelected = _tnSelected;
        }
        public TreeNode tnSelected { get; set; }
        public string sJH { get; set; }
 
    }
}
