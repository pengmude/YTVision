using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.ResultProcessing.ResultSummarize
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
