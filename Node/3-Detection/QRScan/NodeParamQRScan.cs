using Newtonsoft.Json;
using OpenCvSharp;
using YTVisionPro.Forms.ShapeDraw;

namespace YTVisionPro.Node._3_Detection.QRScan
{
    internal class NodeParamQRScan : INodeParam
    {
        /// <summary>
        /// 订阅节点的名称
        /// </summary>
        public string Text1 { get; set; }
        /// <summary>
        /// 订阅节点的属性
        /// </summary>
        public string Text2 { get; set; }
        /// <summary>
        /// 显示更多参数开关
        /// </summary>
        public bool MoreParamsEnable { get; set; }
        /// <summary>
        /// 高斯模糊值
        /// </summary>
        public int GaussianBlur { get; set; }
        /// <summary>
        /// 是否使用直方图均衡化
        /// </summary>
        public bool ISHistogramEqualization { get; set; }
        /// <summary>
        /// 控制对比度限制的阈值
        /// </summary>
        public double ClipLimit { get; set; }
        /// <summary>
        /// 定义了用于计算直方图的局部区域的大小
        /// </summary>
        public Size TileGridSize { get; set; }
        /// <summary>
        /// ROI（反序列化用）
        /// </summary>
        [JsonConverter(typeof(PolyConverter))]
        public ROI ROI { get; set; }
    }
}
