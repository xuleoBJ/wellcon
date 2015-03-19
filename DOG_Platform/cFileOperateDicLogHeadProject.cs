using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
namespace DOGPlatform
{
    class cFileOperateDicLogHeadProject 
    {
        public cFileOperateDicLogHeadProject()
        {
            if (File.Exists(cProjectManager.filePathLogHeadDicProject))
            {
                itemsProjectLogHead = readProjectLogDic2Struct();
            }
        }
        public List<ItemLogHeadInfor> getItemByJH(string sJH)
        {
            List<ItemLogHeadInfor> itemsProjectLogHeadByJH = new List<ItemLogHeadInfor>();
            if (File.Exists(cProjectManager.filePathLogHeadDicProject))
            {
                itemsProjectLogHeadByJH = readProjectLogDic2Struct(sJH);
            }
            return itemsProjectLogHeadByJH;
        }
        public  List<string> getLogNameList()
        {
            List<string>  ltStrLogNames = new List<string>();
                foreach (ItemLogHeadInfor item in itemsProjectLogHead)
                {
                    if (!ltStrLogNames.Contains(item.sLogName))
                        ltStrLogNames.Add(item.sLogName);
                }
                return ltStrLogNames;
        }
        public  List<ItemLogHeadInfor> itemsProjectLogHead { get; set; }
       
        public static List<ItemLogHeadInfor> readLogDic2Struct(string filePath) 
        {
            List<ItemLogHeadInfor> listItemLogHeadInfors = new List<ItemLogHeadInfor>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        ItemLogHeadInfor sttItemLogHeadInfor = new ItemLogHeadInfor();
                        string[] split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        sttItemLogHeadInfor.sJH = split[0];
                        sttItemLogHeadInfor.sLogName = split[2];
                        sttItemLogHeadInfor.sUnit = split[3];
                        sttItemLogHeadInfor.fLeftValue = float.Parse(split[4]);
                        sttItemLogHeadInfor.fRightValue = float.Parse(split[5]);
                        sttItemLogHeadInfor.iIsLog = int.Parse(split[6]);
                        sttItemLogHeadInfor.sLogColor = split[7];
                        sttItemLogHeadInfor.fLineWidth = float.Parse(split[8]);
                        sttItemLogHeadInfor.iLineType = int.Parse(split[9]);
                        listItemLogHeadInfors.Add(sttItemLogHeadInfor);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return listItemLogHeadInfors;
        }
        public static List<ItemLogHeadInfor> readLogDic2Struct(string filePath,string sJH)
        {
            List<ItemLogHeadInfor> listItemLogHeadInfors = new List<ItemLogHeadInfor>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        ItemLogHeadInfor sttItemLogHeadInfor = new ItemLogHeadInfor();
                        string[] split = line.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (split[0] == sJH)
                        {
                            sttItemLogHeadInfor.sJH = split[0];
                            sttItemLogHeadInfor.sLogName = split[2];
                            sttItemLogHeadInfor.sUnit = split[3];
                            sttItemLogHeadInfor.fLeftValue = float.Parse(split[4]);
                            sttItemLogHeadInfor.fRightValue = float.Parse(split[5]);
                            sttItemLogHeadInfor.iIsLog = int.Parse(split[6]);
                            sttItemLogHeadInfor.sLogColor = split[7];
                            sttItemLogHeadInfor.fLineWidth = float.Parse(split[8]);
                            sttItemLogHeadInfor.iLineType = int.Parse(split[9]);
                            listItemLogHeadInfors.Add(sttItemLogHeadInfor);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return listItemLogHeadInfors;
        }
        public static List<ItemLogHeadInfor> readProjectLogDic2Struct()
        {
            return readLogDic2Struct(cProjectManager.filePathLogHeadDicProject);
        }
        public static List<ItemLogHeadInfor> readProjectLogDic2Struct(string sJH)
        {
            return readLogDic2Struct(cProjectManager.filePathLogHeadDicProject, sJH);
        }
       
       
        
        public ItemLogHeadInfor selectLogHeadItem(string sJH, string sLogName)
        {
            ItemLogHeadInfor itemHeadInfor = new ItemLogHeadInfor();
            itemHeadInfor.sJH = sJH;
            itemHeadInfor.sLogName = sLogName;
            itemHeadInfor.sLogColor = "red";
            itemHeadInfor.fRightValue = 100.0f;
            itemHeadInfor.fLeftValue = 0.0f;
            return itemHeadInfor;
        }
        public  List<ItemLogHeadInfor> getLogHeadItems(string sJH)
        {
            List<ItemLogHeadInfor> ItemLogHeadInforList = itemsProjectLogHead.FindAll(p => p.sJH == sJH);

            return ItemLogHeadInforList;
        }
 
      
    }
}
