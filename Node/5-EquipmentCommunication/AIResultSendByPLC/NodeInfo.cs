
namespace YTVisionPro.Node._5_EquipmentCommunication.AIResultSendByPLC
{
    public class NodeInfo
    {
        public string NodeName { get; set; }
        public int IdentifyType { get; set; }
        public int[] area_thresholds { get; set; }
        /// <summary>
        /// 功能码：0代表quexianjiance，1代表dingwei，2代表fenlei
        /// </summary>
        public int NodeType { get; set; }
        /// <summary>
        /// 从0开始，每一个数字代表一个节点
        /// </summary>
        public int BatchSize { get; set; }
        public int ModelChannels { get; set; }
        public string[] CutRules { get; set; }
        public bool Rotate { get; set; }
        public int NodeId { get; set; }
        /// <summary>
        /// 数据来源于上一个NodeType的节点是多少
        /// </summary>
        public int ParentsNodeId { get; set; }
        public int ModelWidth { get; set; }
        public double nms_threash { get; set; }
        public int ModelHeight { get; set; }
        public int distance_thresh { get; set; }
        public double score_thresh { get; set; }
        public bool SliceMode { get; set; }
        public bool Torch { get; set; }
        public bool Correct { get; set; }
        /// <summary>
        /// 当前节点的缺陷种类
        /// </summary>
        public string[] ClassNames { get; set; }
    }
}
