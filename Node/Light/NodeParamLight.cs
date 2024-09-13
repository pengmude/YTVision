using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Hardware.Light;

namespace YTVisionPro.Node.Light
{
    internal class NodeParamLight : INodeParam
    {
        public NodeParamLight(ILight light, int brightness, int time)
        {
            this.Light = light;
            this.Brightness = brightness;
            this.Time = time;
        }
        public ILight Light { get; set; }
        public int Brightness { get; set; } //光源值
        public int Time { get; set; } //打开时长
    }
}
