using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.LightControl
{
    internal class NodeResultLight : INodeResult
    {
        public NodeStatus Status { get; set; }
        /// <summary>
        /// 运行耗时
        /// </summary>
        public long RunTime { get; set; }
        /// <summary>
        /// 运行状态码
        /// </summary>
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
