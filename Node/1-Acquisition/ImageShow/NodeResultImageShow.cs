using System.Drawing;

namespace YTVisionPro.Node._1_Acquisition.ImageSource
{
    internal class NodeResultImageShow : INodeResult
    {
        public long RunTime { get ; set; }
        public NodeRunStatusCode RunStatusCode { get ; set ; }
        public NodeStatus Status { get; set; }
        /// <summary>
        /// 相机采集到的图像
        /// </summary>
        public Bitmap Bitmap { get; set; }
    }
}
