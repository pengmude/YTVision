﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._7_ResultProcessing.GenerateExcelSpreadsheet
{
    internal class NodeResultGenerateExcel : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
