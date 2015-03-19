using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.XML
{
    class cXMLMapLayerText
    {
        public static void addWellTextNode2XML(string filePathxmlLayerMap, List<string> ltsJH, List<ItemWellLayerValue> ltWellValue)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig");
            //查找看是否存在节点，如果存在删除原来的xmlnode段，重新插入新段
         

            xmlLayerMap.Save(filePathxmlLayerMap);

        }
    }
}
