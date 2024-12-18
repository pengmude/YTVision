﻿using System.ComponentModel;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Node._5_EquipmentCommunication.PanasonicRead
{
    internal class NodeResultPlcRead : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("读取结果")]
        public PlcResult<bool, int, string, byte[]> ReadData { get; set; }

        [DisplayName("二维码字符串")]
        public string Code { get; set; }
    }
}
