using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DOGPlatform
{
    public partial class FormTrackLog : Form
    {
        string sIDTrack;
        string sLogName;
        string sJH;

        public FormTrackLog(string sJH, string sIDTrack, string sLogName)
        {
            this.sJH = sJH;
            this.sIDTrack = sIDTrack;
            this.sLogName = sLogName;
            InitializeComponent();
            initializaMycontrols();
        }
        void initializaMycontrols()
        {
            this.tbxTrackID.Text = sIDTrack;
            this.tbxLogname.Text = sLogName;
            this.tbxJH.Text = sJH;
            }

        private void cbbCurveColor_Click(object sender, EventArgs e)
        {
            //ColorDialog colorDialog1 = new ColorDialog();
            //if (colorDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    this.cbbCurveColor.BackColor = colorDialog1.Color;
            //    string sColor=cPublicMethodForm.getRGB(colorDialog1.Color);

            //    try
            //    {
            //        XDocument XsingleWellRoot = XDocument.Load(filePathSingleWellData);
            //        XElement XTrackCollect = XsingleWellRoot.Element("Model").Element("TrackCollect");
            //        IEnumerable<XElement> awElements =
            //                                                 from el in XTrackCollect.Descendants()
            //                                                 where el.Name == "Track"
            //                                                 select el;

            //        foreach (XElement el in awElements)
            //        {
            //            string sID = el.Attribute("id").Value;
            //            if (sID == this.sIDTrack)
            //            {
            //                IEnumerable<XElement> awElementsLog =
            //                                               from el_log in el.Descendants()
            //                                               where el_log.Name == "Log"
            //                                               select el_log;

            //                foreach (XElement el_log in awElementsLog)
            //                {
            //                    if (el_log.Element("LogName").Value == sLogName)
            //                    {
            //                        el_log.Element("curveColor").Value = sColor;
            //                        break;
            //                    }
            //                }

            //            }
            //        }
            //        XsingleWellRoot.Save(cProject.xmlConfigSingleWell);
            //    }
            //    catch (Exception e1) { MessageBox.Show(e1.ToString()); }
                
            //}
        }

        private void nUDLeftValue_ValueChanged(object sender, EventArgs e)
        {

            //try
            //{
            //    XDocument XsingleWellRoot = XDocument.Load(filePathSingleWellData);
            //    XElement XTrackCollect = XsingleWellRoot.Element("Model").Element("TrackCollect");
            //    IEnumerable<XElement> awElements =
            //                                             from el in XTrackCollect.Descendants()
            //                                             where el.Name == "Track"
            //                                             select el;

            //    foreach (XElement el in awElements)
            //    {
            //        string sID = el.Attribute("id").Value;
            //        if (sID == this.sIDTrack)
            //        {
            //            IEnumerable<XElement> awElementsLog =
            //                                           from el_log in el.Descendants()
            //                                           where el_log.Name == "Log"
            //                                           select el_log;

            //            foreach (XElement el_log in awElementsLog)
            //            {
            //                if (el_log.Element("LogName").Value == sLogName)
            //                {
            //                    el_log.Element("leftValue").Value = nUDLeftValue.Value.ToString("0");
            //                    break;
            //                }
            //            }

            //        }
            //    }
            //    XsingleWellRoot.Save(cProject.xmlConfigSingleWell);
            //}
            //catch (Exception e1) { MessageBox.Show(e1.ToString()); }
           
        }

        private void nUDRightValue_ValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    XDocument XsingleWellRoot = XDocument.Load(cProject.xmlConfigSingleWell);
            //    XElement XTrackCollect = XsingleWellRoot.Element("Model").Element("TrackCollect");
            //    IEnumerable<XElement> awElements =
            //                                             from el in XTrackCollect.Descendants()
            //                                             where el.Name == "Track"
            //                                             select el;

            //    foreach (XElement el in awElements)
            //    {
            //        string sID = el.Attribute("id").Value;
            //        if (sID == this.sIDTrack)
            //        {
            //            IEnumerable<XElement> awElementsLog =
            //                                           from el_log in el.Descendants()
            //                                           where el_log.Name == "Log"
            //                                           select el_log;

            //            foreach (XElement el_log in awElementsLog)
            //            {
            //                if (el_log.Element("LogName").Value == sLogName)
            //                {
            //                    el_log.Element("rightValue").Value = nUDRightValue.Value.ToString("0");
            //                    break;
            //                }
            //            }

            //        }
            //    }
            //    XsingleWellRoot.Save(cProject.xmlConfigSingleWell);
            //}
            //catch (Exception e1) { MessageBox.Show(e1.ToString()); }
        }
    }
}
