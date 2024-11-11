namespace YTVisionPro.Node._5_EquipmentCommunication.PanasonicWirte
{
    internal class NodeResultPlcWrite : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
