
using System.ComponentModel;

namespace YTVisionPro.Node._5_EquipmentCommunication.AIResultSendByPLC
{
    internal class NodeResultHTAISendSignal : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
