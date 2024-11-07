using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Device.Camera;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.ImageSrc.CameraIO
{
    internal class NodeParamCameraIO:INodeParam
    {
        /// <summary>
        /// 相机
        /// </summary>
        [JsonIgnore]
        public ICamera Camera { get; set; }

        /// <summary>
        /// 相机名称
        /// </summary>
        public string CameraName { get; set; }

        /// <summary>
        /// 线路
        /// </summary>
        public string LineSelector { get; set; }
         
        /// <summary>
        /// 线路模式
        /// </summary>
        public string LineMode { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 节点结果
        /// </summary>
        public string NodeResult { get; set; }

        public bool Condition { get; set; }
    }
}
