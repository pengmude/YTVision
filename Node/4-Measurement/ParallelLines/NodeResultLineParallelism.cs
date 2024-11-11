namespace YTVisionPro.Node._4_Detection.LineParallelism
{
    internal class NodeResultLineParallelism : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
