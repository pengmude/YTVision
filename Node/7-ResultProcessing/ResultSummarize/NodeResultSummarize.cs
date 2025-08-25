using System.ComponentModel;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._7_ResultProcessing.ResultSummarize
{
    public class NodeResultSummarize : INodeResult
    {
        public int RunTime { get; set; }
        [DisplayName("算法汇总结果")]
        public AlgorithmResult SummaryResult { get; set; }
    }
}
