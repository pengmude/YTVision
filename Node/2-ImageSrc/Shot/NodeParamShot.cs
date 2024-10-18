using JsonSubTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using YTVisionPro.Device.Camera;
using YTVisionPro.Device.PLC;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.ImageSrc.Shot
{
    /// <summary>
    /// 相机节点参数
    /// </summary>
    internal class NodeParamShot : INodeParam
    {
        /// <summary>
        /// 使用的相机
        /// </summary>
        [JsonIgnore]
        public ICamera Camera { get; set; }
        /// <summary>
        /// 使用的相机名称
        /// </summary>
        public string CameraName {  get; set; }
        /// <summary>
        /// 触发方式
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TriggerSource TriggerSource { get; set; }
        /// <summary>
        /// 触发沿
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TriggerEdge TriggerEdge { get; set; }
        /// <summary>
        /// 触发延迟
        /// </summary>
        public int TriggerDelay { get; set; }
        /// <summary>
        /// 曝光时间
        /// </summary>
        public float ExposureTime { get; set; }
        /// <summary>
        /// 增益
        /// </summary>
        public float Gain { get; set; }
        /// <summary>
        /// 是否频闪
        /// </summary>
        public bool IsStrobing { get; set; }
        /// <summary>
        /// 图像显示窗口名称
        /// </summary>
        public string WindowName { get; set; }
    }

}
