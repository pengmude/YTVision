using System.Drawing;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.Camera.HiK.WaitHardTrigger
{
    internal class NodeParamWaitHardTrigger : INodeParam
    {
        /// <summary>
        /// 硬触发采图
        /// </summary>
        public Bitmap Image { get; set; }
    }
}
