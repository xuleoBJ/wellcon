using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace DOGPlatform
{
    class cPublicMethodBase
    {
        
        public static void showErrInfor(string sRightNote)
        {
            if (cProjectData.sErrLineInfor== "")
            {
                MessageBox.Show(sRightNote);
            }
            else
            {
                cProjectData.sErrLineInfor= "数据可能错误如下：" + " \r\n" + cProjectData.sErrLineInfor;
                cPublicMethodForm.outputErrInfor2Text(cProjectData.sErrLineInfor);
            }

        }

        public static string getYMLastMonth(string sYYYYMM)
        {
            int iYM=int.Parse(sYYYYMM);
            if (iYM%100>1) return (iYM - 1).ToString(); //不是一月份
            else return (iYM/100 - 1).ToString()+"12";
        }
        private void LoadTreeViewFromXmlFile(string filename, TreeView trv)
        {
            // Load the XML document.
            XmlDocument xml_doc = new XmlDocument();
            xml_doc.Load(filename);
            // Add the root node's children to the TreeView.
            trv.Nodes.Clear();
            AddTreeViewChildNodes(trv.Nodes, xml_doc.DocumentElement);
            trv.CollapseAll();
        }
        private void AddTreeNode(XmlNode xmlNode, TreeNode treeNode)
        {
            XmlNode newNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i;
            if (xmlNode.HasChildNodes)
            {
                nodeList = xmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    newNode = xmlNode.ChildNodes[i];
                    treeNode.Nodes.Add(new TreeNode(newNode.Name));
                    tNode = treeNode.Nodes[i];
                    AddTreeNode(newNode, tNode);
                }
            }
            else treeNode.Text = (xmlNode.OuterXml).Trim();
        }

        private void AddTreeViewChildNodes(TreeNodeCollection parent_nodes, XmlNode xml_node)
        {
            foreach (XmlNode child_node in xml_node.ChildNodes)
            {
                // Make the new TreeView node.
                TreeNode new_node = parent_nodes.Add(child_node.Name);

                // Recursively make this node's descendants.
                AddTreeViewChildNodes(new_node.Nodes, child_node);

                // If this is a leaf node, make sure it's visible.
                if (new_node.Nodes.Count == 0) new_node.EnsureVisible();
            }
        }

        public static string getRGB(Color m_color)
        {
            string r = m_color.R.ToString();
            string g = m_color.G.ToString();
            string b = m_color.B.ToString();
            return "rgb(" + r + "," + g + "," + b + ")";
        }
        public static int getCeilingNumer(float fValue,int iInteval)
        {
             return Convert.ToInt16(Math.Ceiling(fValue) / iInteval + 1) * iInteval;
        }

        public static void printsErrLineInforIndex(int iIndexLine)
        {
            StreamWriter swNew = new StreamWriter(cProjectManager.filePathRunInfor, false, Encoding.UTF8);
            swNew.WriteLine(iIndexLine.ToString());
            swNew.Close();
        }

        public static void outputErrInfor2Text(string errInfor)
        {
            StreamWriter swNew = new StreamWriter(cProjectManager.filePathRunInfor, false, Encoding.UTF8);
            swNew.WriteLine(DateTime.Now.ToString());
            swNew.WriteLine(errInfor);
            swNew.Close();
            System.Diagnostics.Process.Start("notepad.exe", cProjectManager.filePathRunInfor);
        }

        public static List<string> ltStrJHInProject(List<string> ltStrJH )
        {
            for (int i = (ltStrJH.Count - 1); i >= 0; i--)
            {
                if (cProjectData.ltStrProjectJH.Contains(ltStrJH[i]) == false)
                    ltStrJH.RemoveAt(i);
            }
            return ltStrJH;
        }
      

    }
}
