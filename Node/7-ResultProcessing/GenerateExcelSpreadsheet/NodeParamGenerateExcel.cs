﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node._7_ResultProcessing.GenerateExcelSpreadsheet
{
    internal class NodeParamGenerateExcel : INodeParam
    {
        // 反序列化还原界面参数用
        public string[] Texts = new string[20];
        public bool[] Enables = new bool[10];
        public string filePath;
    }
}
