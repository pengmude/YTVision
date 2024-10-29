using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node._3_ImagePreprocessing.ImageRotate
{
    internal class NodeResultImageRotate : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }

        /// <summary>
        /// 旋转后的图片
        /// </summary>
        public Bitmap Image { get; set; }
    }
}
