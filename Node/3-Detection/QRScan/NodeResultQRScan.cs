using System.ComponentModel;

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

        [DisplayName("识别结果集合")]
        public string[] Codes { get; set; }
    }
}
