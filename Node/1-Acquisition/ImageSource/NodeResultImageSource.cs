using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace YTVisionPro.Node._1_Acquisition.ImageSource
{
    internal class NodeResultImageSource : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set ; }

        [DisplayName("节点耗时")]
        public long RunTime { get ; set ; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 相机采集到的图像
        /// </summary>
        [DisplayName("图像")]
        public Bitmap Bitmap { get; set; }
        /// <summary>
        /// 文件夹所有的图像
        /// </summary>
        [DisplayName("图像集合")]
        public List<Bitmap> Bitmaps { get; set; }

    }
}
