using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace DOGPlatform
{
    class cFileOperateDicLogHeadGlobe:cFileOperateDicLogHeadProject
    {
        public cFileOperateDicLogHeadGlobe():base()
        {
          
        }
        public List<ItemLogHeadInfor> itemsGlobeLogHead { get; set; }
        public static List<ItemLogHeadInfor> readGlobeLogDic2Struct() 
        {
            List<ItemLogHeadInfor> _itemsGlobe = new List<ItemLogHeadInfor>();
            
            return _itemsGlobe; 
        }
       
        public static void generateGlobeDicLog(string filePath)
        {
            cProjectData.sErrLineInfor = "";
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
            List<string> ltStrHeadColoum = new List<string>();  //测井字典表
            //ltStrHeadColoum.Add("井名");
            //ltStrHeadColoum.Add("所在列");
            //ltStrHeadColoum.Add("系列名");
            //ltStrHeadColoum.Add("单位");
            //ltStrHeadColoum.Add("左值");
            //ltStrHeadColoum.Add("右值");
            //ltStrHeadColoum.Add("对数坐标");
            //ltStrHeadColoum.Add("曲线颜色");
            //ltStrHeadColoum.Add("曲线宽度");
            //ltStrHeadColoum.Add("线型");
            //sw.WriteLine(string.Join("\t", ltStrHeadColoum.ToArray()));

            List<string> ltStrLogName = new List<string>();
            List<string> ltStrLogUnit = new List<string>();
            List<float> fListLeftValue = new List<float>();
            List<float> fListRightValue = new List<float>();
            List<int> iListIsLogGrid = new List<int>();
            List<string> ltStrLogColor = new List<string>();
            List<float> fListLineWidth = new List<float>();
            List<int> iListLineType = new List<int>();

            ltStrLogName.Add("SP");
            ltStrLogUnit.Add("mv");
            fListLeftValue.Add(0F);
            fListRightValue.Add(100F);
            iListIsLogGrid.Add(0);
            ltStrLogColor.Add(Color.Blue.Name.ToString());
            fListLineWidth.Add(0.8F);
            iListLineType.Add(1);

            ltStrLogName.Add("GR");
            ltStrLogUnit.Add("API");
            fListLeftValue.Add(0F);
            fListRightValue.Add(100F);
            iListIsLogGrid.Add(0);
            ltStrLogColor.Add(Color.Red.Name.ToString());
            fListLineWidth.Add(0.8F);
            iListLineType.Add(1);

            ltStrLogName.Add("CAL");
            ltStrLogUnit.Add("cm");
            fListLeftValue.Add(0F);
            fListRightValue.Add(50F);
            iListIsLogGrid.Add(0);
            ltStrLogColor.Add(Color.Black.Name.ToString());
            fListLineWidth.Add(0.8F);
            iListLineType.Add(2);

            ltStrLogName.Add("CNL");
            ltStrLogUnit.Add("-");
            fListLeftValue.Add(0F);
            fListRightValue.Add(50F);
            iListIsLogGrid.Add(0);
            ltStrLogColor.Add(Color.Black.Name.ToString());
            fListLineWidth.Add(0.8F);
            iListLineType.Add(0);

            ltStrLogName.Add("DEN");
            ltStrLogUnit.Add("g/cm3");
            fListLeftValue.Add(1.8F);
            fListRightValue.Add(2.3F);
            iListIsLogGrid.Add(0);
            ltStrLogColor.Add(Color.Black.Name.ToString());
            fListLineWidth.Add(0.8F);
            iListLineType.Add(0);

            ltStrLogName.Add("AC");
            ltStrLogUnit.Add("m/s");
            fListLeftValue.Add(150F);
            fListRightValue.Add(250F);
            iListIsLogGrid.Add(0);
            ltStrLogColor.Add(Color.Black.Name.ToString());
            fListLineWidth.Add(0.8F);
            iListLineType.Add(0);

            ltStrLogName.Add("DT");
            ltStrLogUnit.Add("us/m");
            fListLeftValue.Add(150F);
            fListRightValue.Add(250F);
            iListIsLogGrid.Add(0);
            ltStrLogColor.Add(Color.Black.Name.ToString());
            fListLineWidth.Add(0.8F);
            iListLineType.Add(0);

            ltStrLogName.Add("RT");
            ltStrLogUnit.Add("欧姆");
            fListLeftValue.Add(0.2F);
            fListRightValue.Add(200F);
            iListIsLogGrid.Add(1);
            ltStrLogColor.Add(Color.Black.Name.ToString());
            fListLineWidth.Add(0.8F);
            iListLineType.Add(1);

            ltStrLogName.Add("RLLD");
            ltStrLogUnit.Add("欧姆");
            fListLeftValue.Add(0.2F);
            fListRightValue.Add(200F);
            iListIsLogGrid.Add(1);
            ltStrLogColor.Add(Color.Black.Name.ToString());
            fListLineWidth.Add(0.8F);
            iListLineType.Add(1);

            ltStrLogName.Add("RLLS");
            ltStrLogUnit.Add("欧姆");
            fListLeftValue.Add(0.2F);
            fListRightValue.Add(200F);
            iListIsLogGrid.Add(1);
            ltStrLogColor.Add(Color.Black.Name.ToString());
            fListLineWidth.Add(0.8F);
            iListLineType.Add(1);

           
            sw.Close();
        }
    }
}
