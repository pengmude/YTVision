namespace YTVisionPro.Node._5_EquipmentCommunication.LightOpen
{
    internal class NodeResultCameraIO : INodeResult
    {
        public NodeStatus Status { get ; set ; }
        public long RunTime { get ; set ; }
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
