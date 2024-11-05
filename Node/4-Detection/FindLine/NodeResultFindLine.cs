using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node._4_Detection.FindLine
{
    internal class NodeResultFindLine : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 检测出来的直线
        /// </summary>
        public List<LineSegmentPoint> Lines { get; set; }
        /// <summary>
        /// 输出图像
        /// </summary>
        public Bitmap OutputImage { get; set; }
    }
}
