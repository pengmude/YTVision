namespace YTVisionPro.Node._5_EquipmentCommunication.ModbusWrite
{
    internal class NodeResultModbusWrite : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        public object ReadData { get; set; }
    }
}
