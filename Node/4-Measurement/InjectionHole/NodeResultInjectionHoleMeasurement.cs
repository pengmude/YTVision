using System.Drawing;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._4_Measurement.InjectionHole
{
    internal class NodeResultInjectionHoleMeasurement : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 输出图像
        /// </summary>
        public Bitmap OutputImage { get; set; }
        /// <summary>
        /// 找到的圆半径判定结果
        /// </summary>
        public ResultViewData Result { get; set; }
    }
}
