using System.ComponentModel;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._7_ResultProcessing.ResultSummarize
{
    internal class NodeResultSummarize : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("算法汇总结果")]
        public ResultViewData SummaryResult { get; set; }
    }
}
