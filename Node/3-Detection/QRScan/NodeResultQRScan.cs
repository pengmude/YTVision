using System.ComponentModel;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._3_Detection.QRScan
{
    internal class NodeResultQRScan : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("第一个二维码")]
        public string FirstCode { get; set; }

        [DisplayName("所有检出二维码")]
        public string[] Codes { get; set; }

        [DisplayName("算法结果")]
        public ResultViewData Result  { get; set; }
    }
}
