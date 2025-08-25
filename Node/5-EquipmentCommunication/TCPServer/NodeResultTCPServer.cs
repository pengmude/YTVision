using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._5_EquipmentCommunication.TcpServer
{
    public class NodeResultTCPServer : INodeResult
    {
        public int RunTime { get; set; }
        [DisplayName("响应数据")]
        public object ResponseData { get; set; }
    }
}
