
namespace YTVisionPro.Node._5_EquipmentCommunication.AIResultSendByPLC
{
    internal class NodeResultHTAISendSignal : INodeResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public NodeStatus Status { get; set; }
        /// <summary>
        /// 运行耗时
        /// </summary>
        public long RunTime { get; set; }
        /// <summary>
        /// 运行状态码
        /// </summary>
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
