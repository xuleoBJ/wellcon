﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOGPlatform
{
     struct ItemJSJL
    {
        public string sJH;
        public float fDS1;
        public float fDS2;
        public float fSandThickness;
        public float fNetPaySand;
        public float fKXD;
        public float fSTL;
        public float fBHD;
        public int iJSJL;

        public static string item2string(ItemJSJL item)
        {
            List<string> ltStrWrited = new List<string>();
            ltStrWrited.Add(item.sJH);
            ltStrWrited.Add(item.fDS1.ToString());
            ltStrWrited.Add(item.fDS2.ToString());
            ltStrWrited.Add(item.fSandThickness.ToString());
            ltStrWrited.Add(item.fNetPaySand.ToString());
            ltStrWrited.Add(item.fKXD.ToString());
            ltStrWrited.Add(item.fSTL.ToString());
            ltStrWrited.Add(item.fBHD.ToString());
            ltStrWrited.Add(item.iJSJL.ToString());
            return string.Join("\t", ltStrWrited.ToArray());
        }

        public static ItemJSJL parseLine(string line)
        {
            string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            ItemJSJL item = new ItemJSJL();
            if (split.Length >= 9)
            {
                item.sJH = split[0];
                item.fDS1 = 0.0f;
                float.TryParse(split[1], out item.fDS1);
                item.fDS2 = 0.0f;
                float.TryParse(split[2], out item.fDS2);
                item.fSandThickness = 0.0f;
                float.TryParse(split[3], out item.fSandThickness);
                item.fNetPaySand = 0.0f;
                float.TryParse(split[4], out item.fNetPaySand);
                item.fKXD = -999.0f;
                float.TryParse(split[5], out item.fKXD);
                item.fSTL = -999.0f;
                float.TryParse(split[6], out item.fSTL);
                item.fBHD = -999.0f;
                float.TryParse(split[7], out item.fBHD);
                string _sJSJL = codeReplace(split[8]);
                item.iJSJL = int.Parse(_sJSJL);
            }
            return item;
        }

        static public string codeReplace(string _sJSJL)
        {

            if (_sJSJL == "油层" || _sJSJL == "1") _sJSJL = "1";
            else if (_sJSJL == "水层" || _sJSJL == "2") _sJSJL = "2";
            else if (_sJSJL == "气层" || _sJSJL == "3") _sJSJL = "3";
            else if (_sJSJL == "干层" || _sJSJL == "4") _sJSJL = "4";
            else if (_sJSJL == "油气层" || _sJSJL == "5") _sJSJL = "5";
            else if (_sJSJL == "油水同层" || _sJSJL == "6") _sJSJL = "6";
            else if (_sJSJL.IndexOf("油水层") >= 0 || _sJSJL == "6") _sJSJL = "6";
            else if (_sJSJL == "气水层" || _sJSJL == "7") _sJSJL = "7";
            else if (_sJSJL == "差油层" || _sJSJL == "8") _sJSJL = "8";
            else if (_sJSJL == "差气层" || _sJSJL == "9") _sJSJL = "9";
            else if (_sJSJL == "煤层" || _sJSJL == "12") _sJSJL = "12";
            else if (_sJSJL.IndexOf("可疑") >= 0 || _sJSJL == "13") _sJSJL = "13";
            else _sJSJL = "0";
            return _sJSJL;
        } 
    }
}
