using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cContextMenuStripBaseLayer
    {
        public ContextMenuStrip cms { get; set; }
       
        public cContextMenuStripBaseLayer(ContextMenuStrip _cms)
        {
            this.cms = _cms;
        }

        public cContextMenuStripBaseLayer(ContextMenuStrip _cms, TreeNode _tnSelected, string _sXCM):this(_cms)
        {
            this.sXCM = _sXCM;
            this.tnSelected = _tnSelected;
        }
        public TreeNode tnSelected { get; set; }
        public string sXCM { get; set; }
    }
}
