using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace DOGPlatform
{
    public partial class FormLogSetting : Form
    {
        public FormLogSetting(string _sJH,string _sLogName)
        {
            InitializeComponent();
            this.sJH = _sJH;
            this.sLogName = _sLogName;
            initialMycontrol();
        }

             void initialMycontrol()
        {
            tbxJH.Text = sJH;
            tbxJH.ReadOnly = true;
            tbxLogname.Text = sLogName;
            string logfilePath = Path.Combine(cProjectManager.dirPathWellDir, sJH, 
                sLogName+cProjectManager.fileExtensionWellLog);
            cPublicMethodForm.loadDataGridViewWithGeoText(this.dgvDataTable, logfilePath);
            List<string> ltStrDashStyle = new List<string>();
            ltStrDashStyle.Add(DashStyle.Custom.ToString());
            ltStrDashStyle.Add(DashStyle.Dash.ToString());
            ltStrDashStyle.Add(DashStyle.DashDot.ToString());
            ltStrDashStyle.Add(DashStyle.Dot.ToString());
            cPublicMethodForm.inialComboBox(cbbDashStyle, ltStrDashStyle);
        }
        public string sJH { get; set; }
        public string sLogName { get; set; }

   
        private void cbbCurveColor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbCurveColor);
           
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {

            if (tbxLogname.Text.Trim() != "")
            {
                Cursor.Current = Cursors.WaitCursor;
               
                MessageBox.Show("设置保存。");
                Cursor.Current = Cursors.Default;
                this.Close();
            }
            else 
            {
                MessageBox.Show("曲线名不能为空。");
            }
           
        
        }

        private void tbxUnit_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void nUDLeftValue_ValueChanged(object sender, EventArgs e)
        {
        }

        private void nUDRightValue_ValueChanged(object sender, EventArgs e)
        {
        }

        private void nUDLineWidth_ValueChanged(object sender, EventArgs e)
        {
        }

      


    }
}
