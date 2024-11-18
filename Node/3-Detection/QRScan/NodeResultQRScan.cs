﻿using System.ComponentModel;

namespace YTVisionPro.Node._3_Detection.QRScan
{
    internal class NodeResultQRScan : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("第一个读码结果")]
        public string FirstCode { get; set; }

        [DisplayName("读码结果集合")]
        public string[] Codes { get; set; }
    }
}