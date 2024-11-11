namespace YTVisionPro.Node._6_LogicTool.SleepTool
{
    internal class NodeResultSleepTool : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
