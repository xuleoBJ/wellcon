using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DOGPlatform
{
    public partial class FormConnect : FormWellsGroup
    {

        public FormConnect(List<string> listJH,string xcm)
        {
            InitializeComponent();
            initialForm(listJH,xcm);
        }

        void initialForm(List<string> listJH, string xcm) 
        {
            foreach (string sJH in listJH) lbxJHSeclected.Items.Add(sJH);
            this.cbbTopXCM.Text = xcm;
            this.cbbBottomXCM.SelectedIndex = cProjectData.ltStrProjectXCM.IndexOf(xcm) + 1;
            this.cbbTopXCM.Enabled = false;
        }
    }
}
