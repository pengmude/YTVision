using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.Light
{
    public class NodeResultLight : INodeResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
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
