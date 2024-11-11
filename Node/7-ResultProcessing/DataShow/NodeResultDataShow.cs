namespace YTVisionPro.Node._7_ResultProcessing.DataShow
{
    internal class NodeResultDataShow : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set ; }
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
