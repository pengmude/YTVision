using System.ComponentModel;
using System.Drawing;

namespace YTVisionPro.Node._2_ImagePreprocessing.ImageRotate
{
    internal class NodeResultImageRotate : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        /// <summary>
        /// 旋转后的图片
        /// </summary>
        [DisplayName("旋转后图像")]
        public Bitmap Image { get; set; }
    }
}
