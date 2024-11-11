namespace YTVisionPro.Node._5_EquipmentCommunication.LightOpen
{
    internal class NodeResultLight : INodeResult
    {
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
