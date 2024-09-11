using System;
using YTVisionPro.Hardware.Camera;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.Camera.HiK
{
    internal class NodeParamCamera : INodeParam
    {
        /// <summary>
        /// 使用的相机
        /// </summary>
        public ICamera Camera;
        /// <summary>
        /// 触发方式
        /// </summary>
        public TriggerSource TriggerSource;
        /// <summary>
        /// 触发沿
        /// </summary>
        public TriggerEdge TriggerEdge;
        /// <summary>
        /// 触发延迟
        /// </summary>
        public int TriggerDelay;
        /// <summary>
        /// 曝光时间
        /// </summary>
        public float ExposureTime;
        /// <summary>
        /// 增益
        /// </summary>
        public float Gain;
    }

}
