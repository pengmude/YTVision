using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node._1_Acquisition.ImageSource
{
    internal class NodeResultImageSource : INodeResult
    {
        public NodeStatus Status { get; set ; }
        public long RunTime { get ; set ; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 相机采集到的图像
        /// </summary>
        public Bitmap Bitmap { get; set; }
        /// <summary>
        /// 文件夹所有的图像
        /// </summary>
        public List<Bitmap> Bitmaps { get; set; }

    }
}
