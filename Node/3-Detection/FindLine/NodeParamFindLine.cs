using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Forms.ShapeDraw;

namespace YTVisionPro.Node._3_Detection.FindLine
{
    internal class NodeParamFindLine : INodeParam
    {
        /// <summary>
        /// 订阅节点的名称
        /// </summary>
        public string Text1 {  get; set; }
        /// <summary>
        /// 订阅节点的属性
        /// </summary>
        public string Text2 { get; set; }
        /// <summary>
        /// 显示更多参数开关
        /// </summary>
        public bool MoreParamsEnable {  get; set; }
        /// <summary>
        /// 高斯模糊值
        /// </summary>
        public int GaussianBlur {  get; set; }
        /// <summary>
        /// 边缘检测弱边缘阈值
        /// </summary>
        public double Threshold1 {  get; set; }
        /// <summary>
        /// 边缘检测强边缘阈值
        /// </summary>
        public double Threshold2 { get; set; }
        /// <summary>
        /// 是否启用边缘检测L2系数
        /// </summary>
        public bool IsOpenL2 {  get; set; }
        /// <summary>
        /// 霍夫直线投票数
        /// </summary>
        public int Count {  get; set; }
        /// <summary>
        /// 霍夫直线最短长度
        /// </summary>
        public double MinLength {  get; set; }
        /// <summary>
        /// 霍夫直线两直线最大距离
        /// </summary>
        public double MaxDistance { get; set; }
        /// <summary>
        /// 当前选择的直线
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LineSelection LineSelection { get; set; }

        /// <summary>
        /// ROI（反序列化用）
        /// </summary>
        [JsonConverter(typeof(PolyConverter))]
        public ROI ROI { get; set; }
    }
    /// <summary>
    /// 直线类型
    /// </summary>
    public enum LineSelection
    {
        /// <summary>
        /// 最长那条
        /// </summary>
        Longest,
        /// <summary>
        /// 最短那条
        /// </summary>
        Shortest,
        /// <summary>
        /// 最上面那条
        /// </summary>
        Topmost,
        /// <summary>
        /// 最下面那条
        /// </summary>
        Bottommost,
        /// <summary>
        /// 最左边一条
        /// </summary>
        Leftmost,
        /// <summary>
        /// 最右边那条
        /// </summary>
        Rightmost
    }
}
