using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace YTVisionPro.Node._2_ImagePreprocessing.ImageSplit
{
    internal class NodeResultImageSplit : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 拆分图片集合
        /// </summary>
        [DisplayName("拆分图像集合")]
        public List<Bitmap> Bitmaps { get; set; }
    }
}
