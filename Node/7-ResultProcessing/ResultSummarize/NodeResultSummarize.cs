using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._7_ResultProcessing.ResultSummarize
{
    internal class NodeResultSummarize : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 汇总结果（AI和传统算法）
        /// </summary>
        public ResultViewData SummaryResult { get; set; }
    }
}
