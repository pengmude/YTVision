namespace YTVisionPro.Node._5_EquipmentCommunication.ModbusRead
{
    internal class NodeResultModbusRead : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        public object ReadData { get; set; }
    }
}
