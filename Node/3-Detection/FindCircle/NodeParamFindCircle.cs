using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Forms.ShapeDraw;

namespace YTVisionPro.Node._3_Detection.FindCircle
{
    internal class NodeParamFindCircle : INodeParam
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
        /// 判断圆半径OK的开关
        /// </summary>
        public bool OKEnable { get; set; }
        /// <summary>
        /// 判断圆半径OK的下限
        /// </summary>
        public double OKMinR { get; set; }
        /// <summary>
        /// 判断圆半径OK的上限
        /// </summary>
        public double OKMaxR { get; set; }
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
        /// 传递给 Canny 边缘检测器的高阈值
        /// </summary>
        public int param1 { get; set; }
        /// <summary>
        /// 累加器阈值
        /// </summary>
        public int param2 { get; set; }

        /// <summary>
        /// 霍夫圆投票数
        /// </summary>
        public int Count {  get; set; }
        /// <summary>
        /// 霍夫圆最小半径
        /// </summary>
        public double MinLength {  get; set; }
        /// <summary>
        /// 霍夫圆最大半径
        /// </summary>
        public double MaxDistance { get; set; }
        /// <summary>
        /// 当前选择的圆
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public CircleSelection CircleSelection { get; set; }

        /// <summary>
        /// ROI（反序列化用）
        /// </summary>
        [JsonConverter(typeof(PolyConverter))]
        public ROI ROI { get; set; }
    }
    /// <summary>
    /// 圆类型
    /// </summary>
    public enum CircleSelection
    {
        /// <summary>
        /// 最大的圆
        /// </summary>
        Largest,
        /// <summary>
        /// 最小的圆
        /// </summary>
        Smallest,
        /// <summary>
        /// 最上面圆
        /// </summary>
        Topmost,
        /// <summary>
        /// 最下面圆
        /// </summary>
        Bottommost,
        /// <summary>
        /// 最左边圆
        /// </summary>
        Leftmost,
        /// <summary>
        /// 最右边圆
        /// </summary>
        Rightmost
    }
}
