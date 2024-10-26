namespace YTVisionPro.Node._4_Detection.ParallelismOf2Lines
{
    internal class NodeResultParallelismOf2Lines : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
