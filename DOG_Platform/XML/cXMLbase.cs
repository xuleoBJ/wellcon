using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace DOGPlatform.XML
{
    class cXMLbase
    {
        public static void updateNodeValue(string _xmlFilePath, string _nodePath, int _ivalue)
        {
            updateNodeValue(_xmlFilePath, _nodePath, _ivalue.ToString());
        }
        public static void updateNodeValue(string _xmlFilePath, string _nodePath, float _fValue)
        {
            updateNodeValue(_xmlFilePath, _nodePath, _fValue.ToString("0.0"));
        }

        public static void updateNodeValue(string _xmlFilePath, string _nodePath, string _sValue)
        {
            if (File.Exists(_xmlFilePath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(_xmlFilePath);
                XmlNode currentNode = xmlDoc.SelectSingleNode(_nodePath);
                if (currentNode != null) currentNode.InnerText = _sValue;
                xmlDoc.Save(_xmlFilePath);
            }
        }

        public static void addNode(string _xmlFilePath, string _nodePath, XmlNode _ele)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlFilePath);
            XmlNode currentNode = xmlDoc.SelectSingleNode(_nodePath);
            if (currentNode != null) currentNode.AppendChild(_ele);
            xmlDoc.Save(_xmlFilePath);
        }

        public static void delNodes(string xmlfilePath, string fullPath, string sTagNameRemoved)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlfilePath);
            XmlNode currentNode = xmlDoc.SelectSingleNode(fullPath);
            if (currentNode != null)
            {
                foreach (XmlNode _node in currentNode.SelectNodes(sTagNameRemoved))
                    currentNode.RemoveChild(_node);
            }
            xmlDoc.Save(xmlfilePath);

        }
        public static void setNodeInnerText(string xmlfilePath, string fullPath, string sInnerText)
        {
            int _indexSplit = fullPath.LastIndexOf('/');
            string _node = fullPath.Substring(_indexSplit + 1); //+1 delete/
            string _parent = fullPath.Substring(0, _indexSplit);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlfilePath);
            XmlNode currentNode = xmlDoc.SelectSingleNode(fullPath);
            if (currentNode != null) currentNode.InnerText = sInnerText;
            else
            {
                //节点没找到，应该插入节点
                XmlNode _parentNode = xmlDoc.SelectSingleNode(_parent);
                currentNode = xmlDoc.CreateNode(XmlNodeType.Element, _node, "");
                _parentNode.AppendChild(currentNode);

            }
            xmlDoc.Save(xmlfilePath);

        }

        public static string getNodeInnerText(string xmlfilePath, string fullPath)
        {
           
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlfilePath);
            XmlNode currentNode = xmlDoc.SelectSingleNode(fullPath);
            if (currentNode != null)  return currentNode.InnerText;
            else
            {
                //节点没找到，应该插入节点
                return "";

            }
        }

        public static List<string> splitNodeInnerText(string filePathxml, string fullPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePathxml);
            XmlNode currentNode = xmlDoc.SelectSingleNode(fullPath);

            string _data = "";
            if (currentNode != null)
            {
                _data = currentNode.InnerText;
            }
            return _data.Split(new Char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static XmlNode setNodeVisibleProperty(XmlDocument xmlDoc ,XmlNode xn, bool bShow)
        {

            var idAttribute = xn.Attributes["id"];
            var styleAttribute = xn.Attributes["style"];
            if (idAttribute != null)
            {
                if ( styleAttribute != null)
                {
                    if (bShow == false) styleAttribute.Value = "display:none;";
                    else styleAttribute.Value = "visibility:visible;";
                }

                if ( styleAttribute == null)
                {
                    styleAttribute = xmlDoc.CreateAttribute("style");
                    if (bShow == false) styleAttribute.Value = "display:none;";
                    else styleAttribute.Value = "visibility:visible;";
                    xn.Attributes.Append(styleAttribute);
                }
            }

            return xn; 
        }

    }
}
