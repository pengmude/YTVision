using System.ComponentModel;

namespace YTVisionPro.Node._7_ResultProcessing.ImageSave
{
    internal class NodeResultImageSave : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
