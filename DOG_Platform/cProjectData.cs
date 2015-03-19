using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using DOGPlatform.XML;

namespace DOGPlatform
{
    class cProjectData
    {
        public static List<string> ltStrProjectJH = new List<string>();
        public static List<string> ltStrProjectXCM = new List<string>();
        public static List<string> ltStrProjectYM = new List<string>();
        public static List<string> ltStrLogSeriers = new List<string>();


        //工程井信息，非常重要，包含工程中井的所有经常检索的信息，比如wellPath,wellLogHead,仍需要修改
        public static List<ItemWell> listProjectWell = new List<ItemWell>();
        

        public static cGridPara projectMesh = new cGridPara();
        public static double dfMapScale = 0.0;
        public static double dfMapXrealRefer = 0.0;
        public static double dfMapYrealRefer = 9000.0;

        public static string sAllLayer ="AllLayer";
        public static string sErrLineInfor = "";
        public static int INVALID = -999;
        public static string sTempTrackData = "";

        public static void clearProjectData()
        {
            ltStrProjectJH.Clear();
            ltStrLogSeriers.Clear();
            ltStrProjectXCM.Clear();
            ltStrProjectYM.Clear();
            listProjectWell.Clear();
            dfMapScale = 0.1;
            dfMapXrealRefer = 0.0;
            dfMapYrealRefer = 9000.0;

            sErrLineInfor = "";
            INVALID = -999;
            sTempTrackData = ""; 

        }
       
        public static void loadProjectData()
        {
            //ltStrProjectJH应用井名列表赋值
            try
            {
                cXMLProject.getProjectRefPointNode();
                getProjectJHFromXML();
                if (cProjectData.ltStrProjectJH.Count == 0)
                {
                    foreach (string _sJH in cIOinputWellHead.getLtStrJH()) ltStrProjectJH.Add(_sJH);
                    ltStrProjectJH.Sort();
                    setProjectWellsInfor();
                }
                getProjectLogSeriersFromXML();
                if (cProjectData.ltStrLogSeriers.Count == 0) setProjectGlobalLogSeriers();
                getProjectXCM();
                getProjectYMFromXML();
                if (cProjectData.ltStrProjectYM.Count == 0) setProjectYM(cPublicMethodBase.getYMLastMonth(DateTime.Now.ToString("yyyyMM")));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
       
        public static void getProjectJHFromXML()
        {
            cProjectData.ltStrProjectJH.Clear();
            cXMLProject.getLtStrJHFromNode();
            cProjectData.ltStrProjectJH.Sort();
        }
        public static void setProjectJH2XML()
        {
            cXMLProject.setProjectJHNode();
        }
       
        public static void getProjectXCM()
        {
            cProjectData.ltStrProjectXCM.Clear();
            //ltStrProjectXCM小层名列表赋值
            using (StreamReader sr = new StreamReader(cProjectManager.filePathInputLayerSeriers, System.Text.Encoding.Default))
            {
                int lineindex = 0;
                String line;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    lineindex++;
                    cProjectData.ltStrProjectXCM.Add(line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                }
            }

        }
        
      
        public static void setProjectYM(string yyyyMM)
        {
            setProjectYM(yyyyMM, DateTime.Now.ToString("yyyyMM"));
        }

        public static void setProjectYM(string minYM,string maxYM)
        {
            //ltStrProjectYM 生产年月列表赋值
            cProjectData.ltStrProjectYM.Clear();

            int iYMmin = int.Parse(minYM);
            int iYMmax = int.Parse(cPublicMethodBase.getYMLastMonth(maxYM)) ;

            while (iYMmin <= iYMmax)
            {
                if (iYMmin % 100 < 12)
                {
                    iYMmin = iYMmin + 1;
                }
                else
                {
                    iYMmin = iYMmin + 89;
                }
                cProjectData.ltStrProjectYM.Add(iYMmin.ToString());
            }

        }
     
        public static void setProjectWellsInfor()
        {
            listProjectWell.Clear();
            foreach (string _sJH in ltStrProjectJH) 
            {
                listProjectWell.Add(new ItemWell(_sJH));
            }
        }
        public static void getProjectLogSeriersFromXML()
        {
            cProjectData.ltStrLogSeriers.Clear();
            cXMLProject.getLtStrLogSeriersFromNode();
        }

        public static void getProjectYMFromXML()
        {
            cProjectData.ltStrProjectYM.Clear();
            cXMLProject.getProjectYMFromNode();
        }
         public static void setProjectGlobalLogSeriers()
        {
           ltStrLogSeriers.Clear();
           foreach (string _sJH in ltStrProjectJH)
           {
               string _wellDir = Path.Combine(cProjectManager.dirPathWellDir, _sJH);

               foreach (string _item in Directory.GetFiles(_wellDir, "*" + cProjectManager.fileExtensionWellLog))
               {
                   string _log = Path.GetFileNameWithoutExtension(_item);
                   if (ltStrLogSeriers.IndexOf(_log) < 0) ltStrLogSeriers.Add(_log);
               }  
           }
        }

    }
}
