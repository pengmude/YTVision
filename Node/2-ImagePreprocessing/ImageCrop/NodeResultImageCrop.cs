using System.ComponentModel;
using System.Drawing;

namespace YTVisionPro.Node._2_ImagePreprocessing.ImageCrop
{
    internal class NodeResultImageCrop : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("裁切图像")]
        public Bitmap Image { get; set; }

        [DisplayName("裁切矩形")]
        public Rectangle Rectangle { get; set; }
    }
}
