using TDJS_Vision.Device.Camera;
using Newtonsoft.Json;

namespace TDJS_Vision.Node._5_EquipmentCommunication.LightOpen
{
    public class NodeParamCameraIO : INodeParam
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
        /// 保持时间微秒
        /// </summary>
        public int HoldTime {  get; set; }
         
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
