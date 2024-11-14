﻿using System.ComponentModel;

namespace YTVisionPro.Node._5_EquipmentCommunication.ModbusRead
{
    internal class NodeResultModbusRead : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("读取结果")]
        public object ReadData { get; set; }
    }
}
