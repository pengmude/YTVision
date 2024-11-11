namespace YTVisionPro.Node._7_ResultProcessing.ImageSave
{
    internal class NodeResultImageSave : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
