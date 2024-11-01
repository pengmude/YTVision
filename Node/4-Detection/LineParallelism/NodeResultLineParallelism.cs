using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node._4_Detection.DetectionLineParallelism
{
    internal class NodeResultLineParallelism : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 两直线角度
        /// </summary>
        public double LinearDistance { get; set; }
        /// <summary>
        /// 两直线距离
        /// </summary>
        public double StraightLineAngle { get; set; }

    }
}
