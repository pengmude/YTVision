using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node.Tool.ImageShow
{
    internal class NodeResultImageShow : INodeResult
    {
        public long RunTime { get ; set; }
        public NodeRunStatusCode RunStatusCode { get ; set ; }
        public NodeStatus Status { get; set; }
    }
    internal class ImageResult
    {
        /// <summary>
        /// 相机采集到的图像
        /// </summary>
        public Bitmap Bitmap { get; set; } = null;
        /// <summary>
        /// 自动重置
        /// </summary>
        //public AutoResetEvent AutoReset { get; set; } = new AutoResetEvent(false);
    }
}
