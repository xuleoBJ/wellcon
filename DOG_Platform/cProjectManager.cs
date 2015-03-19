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
    class cProjectManager
    {
        public static string dirProject = Path.GetTempPath();
        public static string dirPathUserData = dirProject;
        public static string dirPathUsedProjectData = dirProject;
        public static string dirPathMap = dirProject;
        public static string dirPathLog = dirProject;
        public static string dirPathTemp = dirProject;
        public static string dirPathWellDir = dirProject;
        public static string dirPathLayerDir = dirProject;
        public static string dirPathTemplate = dirProject;

        public static string filePathInkscape = @"C:\Program Files\Inkscape\inkscape.exe";
        public static string filePahtsvgPattern = @"C:\Program Files\\Inkscape\share\patterns\patterns.svg";
        public static string dirPahtInkExtension = @"C:\Program Files\Inkscape\share\extensions";        

        public static string filePathInputWellhead = Path.Combine(dirPathUserData, "$wellHead#.txt");
        public static string filePathInputLayerSeriers = Path.Combine(dirPathUserData, "$layerSerier#.txt");


        public static string filePathRunInfor = Path.Combine(dirPathUserData, "#Infor.txt");
        public static string filePathVoi = Path.Combine(dirPathUsedProjectData, "Voi");
        
        public static string fileNameGE = "GE";
        public static string fileNameInputLayerDepth = "$inputLayerDepth#.txt";
        public static string fileNameInputJSJL = "$inputJSJL#.txt";
        public static string fileNameInputWellPath = "$inputWellPath#.txt";
        public static string fileNameInputWellPerforation = "$inputWellPeforation#.txt";
        public static string fileNameInputWellProfile = "$inputWellProfile#.txt";
        public static string fileNameInputWellLog = "$inputWellLog#";
        public static string fileNameInputWellProduct = "$inputWellProduct#";
        public static string fileNameInputWellInject = "$inputWellInject#";

        public static string fileNameInputFaults = "$inputFaults#";


        public static string fileNameWellPerforation = "#perforation#";
        public static string fileNameWellPath = "#wellPath#";
        public static string fileNameWellJSJL = "#JSJL#";
        public static string fileNameWellLayerDepth = "#layerDepth#";
        public static string fileNameWellProfile = "#injectProfile#";
        public static string fileExtensionWellLog = ".log";
        public static string fileExtensionDynamic = ".dym";


        public static string fileExtensionConnect = ".con";

        public static string filePathLogHeadDicProject = "";
        public static string filePathLayerDataDic = "";
        public static string filePathPerforationDic = "";
        public static string filePathLayerSplitFactorDic = "$LayerSplitFactorDic$.txt";
        public static string filePathInterLayerHeterogeneity = "";
        public static string filePathInnerLayerHeterogeneity = "";
        public static string filePathReserver =  "储量.txt";

        public static string xmlProject = "";
        public static string xmlConfigLayerMap = "";
        public static string xmlConfigSection = "";
        public static string xmlConfigFenceDiagram = "";
        public static string xmlConfigProductMap = "";
        public static string xmlSectionCSS = "";

        
        public static void updateProjectDirection(string openXMLFile)
        {
            cProjectManager.xmlProject = openXMLFile;
            cProjectManager.dirProject = Path.GetDirectoryName(openXMLFile);
           dirPathUserData =Path.Combine(dirProject, "$UserData#");
           dirPathUsedProjectData =Path.Combine( dirProject , "$UsedProjectData$");
           dirPathMap =Path.Combine (dirProject , "$Map$");
           dirPathWellDir = Path.Combine(dirProject, "$Well$");
           dirPathLayerDir = Path.Combine(dirProject, "$Layer$");
           dirPathTemp = Path.Combine(dirProject, "$Temp$");
           dirPathTemplate = Path.Combine(dirProject, "$Template$");

           filePathInputWellhead =Path.Combine(dirPathUserData ,  "$wellHead#.txt");
           filePathInputLayerSeriers =Path.Combine(dirPathUserData ,  "$layerSerier#.txt");


            filePathRunInfor =Path.Combine(dirPathUserData ,  "#Infor.txt");
            filePathVoi = Path.Combine(cProjectManager.dirPathUsedProjectData, "Voi");

            filePathLayerDataDic = Path.Combine( dirPathUsedProjectData , "$LayerDataDic$.txt");
            filePathLayerSplitFactorDic = Path.Combine(dirPathUsedProjectData ,"$LayerSplitFactorDic$.txt");
            filePathInterLayerHeterogeneity =Path.Combine( dirPathUsedProjectData ,"垂向非均质.txt");
            filePathInnerLayerHeterogeneity =Path.Combine( dirPathUsedProjectData , "层内非均质.txt");
            filePathReserver = Path.Combine(cProjectManager.dirPathUsedProjectData, "储量.txt");
           
 
            xmlConfigLayerMap = Path.Combine(dirPathTemplate, "$ConfigLayerMap#.XML");
            xmlConfigSection = Path.Combine(dirPathTemplate, "$ConfigSection#.XML");
            xmlConfigFenceDiagram = Path.Combine(dirPathTemplate, "$ConfigFenceDiagram#.XML");
            xmlConfigProductMap = Path.Combine(dirPathTemplate, "$ConfigProductMap#.XML");
            xmlSectionCSS = Path.Combine(dirPathTemplate, "$SectionCss#.XML");
        }
      
        public  static bool creatProject()
        {
            SaveFileDialog sfdProjectPath = new SaveFileDialog();
            //设置文件类型 
            sfdProjectPath.Title = " 请输入保存数据的位置：";
            sfdProjectPath.Filter = "xl文件|*.xl";
            //设置默认文件类型显示顺序 
            sfdProjectPath.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfdProjectPath.RestoreDirectory = true;
            //创建工程数据文件夹 
            if (sfdProjectPath.ShowDialog() == DialogResult.OK)
            {
                string newProjectFilePath = sfdProjectPath.FileName;
           
                cXMLProject.creatProjectInforXML(newProjectFilePath);
                cProjectManager.updateProjectDirection(newProjectFilePath);
               
                if (Directory.Exists(dirPathUserData))
                {
                    Directory.Delete(dirPathUserData, true);
                }
                System.IO.Directory.CreateDirectory(dirPathUserData);

                List<string> ltStrDataSourceFiles = new List<string>();
                ltStrDataSourceFiles.Add(cProjectManager.filePathInputWellhead);
                ltStrDataSourceFiles.Add(cProjectManager.filePathInputLayerSeriers);
                ltStrDataSourceFiles.Add(cProjectManager.filePathRunInfor);

                foreach (string sItem in ltStrDataSourceFiles)
                {
                    FileStream fs = new FileStream(sItem, FileMode.Create);
                    fs.Close();
                }
                if (Directory.Exists(dirPathUsedProjectData))
                {
                    Directory.Delete(dirPathUsedProjectData, true);
                }
                System.IO.Directory.CreateDirectory(dirPathUsedProjectData);

                if (Directory.Exists(dirPathMap))
                {
                    Directory.Delete(dirPathMap, true);
                }
                System.IO.Directory.CreateDirectory(dirPathMap);

                if (!Directory.Exists(dirPathTemp))
                {
                    System.IO.Directory.CreateDirectory(dirPathTemp);
                }

                if (!Directory.Exists(dirPathTemplate))
                {
                    System.IO.Directory.CreateDirectory(dirPathTemplate);
                }
                cProjectData.listProjectWell.Clear();
                cProjectData.ltStrProjectJH.Clear();
                
                
                MessageBox.Show("项目建立。", "提示");
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool loadProjectData()
        {
            OpenFileDialog ofdProjectPath = new OpenFileDialog();

            ofdProjectPath.Title = " 打开项目：";
            ofdProjectPath.Filter = "xl文件|*.xl|所有文件|*.*\\";
            //设置默认文件类型显示顺序 
            ofdProjectPath.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            ofdProjectPath.RestoreDirectory = true;

            if (ofdProjectPath.ShowDialog() == DialogResult.OK)
            {
                cProjectManager.updateProjectDirection(ofdProjectPath.FileName);
                cProjectData.loadProjectData();
                cProjectData.setProjectWellsInfor();
                return true;
            }
            else
            {
                return false;
            }

        }
        public static void  saveProject()
        {
            //save all staticData into project
            cXMLProject.setProjectRefPointNode();
            cXMLProject.setProjectJHNode();
            cXMLProject.setProjectLogSeriersNode();
            cXMLProject.setProjectYMNode();
        }
        public static void createWellDir(string sJH)
        {
            if (!Directory.Exists(cProjectManager.dirPathWellDir)) System.IO.Directory.CreateDirectory(cProjectManager.dirPathWellDir);

            string dirJH = Path.Combine(cProjectManager.dirPathWellDir, sJH);
            if (!Directory.Exists(dirJH)) Directory.CreateDirectory(dirJH); 
         
            string _fileWellPath = Path.Combine(dirPathWellDir, "#wellPath#");
            if (!File.Exists(_fileWellPath)) cIOinputWellPath.creatVerticalWellPathGeoFile(sJH);
          
        }

        public static void delWellFromProject(string sJH) 
        {
            DialogResult dialogResult = MessageBox.Show("当前选中井为：" + sJH + "，确认删除且不可恢复？", "删除选中井", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                cIOinputWellHead.deleteJHFromWellHead(sJH);
                cProjectData.ltStrProjectJH.Remove(sJH);
                string dirPath = Path.Combine(cProjectManager.dirPathWellDir, sJH);
                Directory.Delete(dirPath, true);
                cProjectData.setProjectWellsInfor(); 
            }
           
        }
        public static void updateWellInfor2Project(ItemWellHead sttNewWell)
        {
            cIOinputWellHead.updateWellHead(sttNewWell);
            cProjectManager.createWellDir(sttNewWell.sJH);
            MessageBox.Show(sttNewWell.sJH + "入库成功。");
            cProjectData.setProjectWellsInfor();
            if (cProjectData.ltStrProjectJH.IndexOf(sttNewWell.sJH)<0) cProjectData.ltStrProjectJH.Add(sttNewWell.sJH);

        }
        public static void createLayerDir()
        {
            if (!Directory.Exists(cProjectManager.dirPathLayerDir))
            {
                System.IO.Directory.CreateDirectory(cProjectManager.dirPathLayerDir);
            }
            foreach (string _sXCM in cProjectData.ltStrProjectXCM)
            {
                string _dirXCM = Path.Combine(cProjectManager.dirPathLayerDir, _sXCM);
                if (!Directory.Exists(_dirXCM))
                {
                    Directory.CreateDirectory(_dirXCM);
                }
            }
        }

        public static void saveProeject2otherDirectionary() 
        {
            SaveFileDialog sfdProjectPath = new SaveFileDialog();
            //设置文件类型 
            sfdProjectPath.Title = " 请输入保存数据的位置：";
            sfdProjectPath.Filter = "xl文件|*.xl";
            //设置默认文件类型显示顺序 
            sfdProjectPath.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfdProjectPath.RestoreDirectory = true;
            if (sfdProjectPath.ShowDialog() == DialogResult.OK)
            {
                string newProjectFilePath = sfdProjectPath.FileName;
                string DestinationPath = Path.GetDirectoryName(newProjectFilePath);
                string SourcePath = dirProject;
                foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
                string oldProjectName=Path.Combine(DestinationPath,Path.GetFileName(xmlProject));
                if (System.IO.File.Exists(newProjectFilePath)) System.IO.File.Delete(newProjectFilePath);
                System.IO.File.Move(oldProjectName, newProjectFilePath);
                MessageBox.Show("保存完毕。");
            }
        }
       
    }
}
