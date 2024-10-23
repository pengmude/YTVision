using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.TCP.Client
{
    internal class NodeResultTCPClient : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 服务器返给客户端的数据
        /// </summary>
        public object ResponseData { get; set; }
    }
}
