namespace YTVisionPro.Node._5_EquipmentCommunication.ModbusSoftTrigger
{
    internal class NodeReusltModbusSoftTrigger : INodeResult
    {
        public NodeStatus Status { get ; set ; }
        public long RunTime { get ; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
