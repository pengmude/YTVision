using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node._5_EquipmentCommunication.PLCSoftTrigger
{
    internal class NodeResultWaitSoftTrigger : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 开始软触发拍照标记
        /// </summary>
        public bool ShotFlag { get; set; }
    }
}
