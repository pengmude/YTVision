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
        public NodeParamLight(ILight light, int brightness, bool open)
        {
            this.Light = light;
            this.Brightness = brightness;
            this.Open = open;
        }
        public ILight Light { get; set; }
        public int Brightness { get; set; } //光源值
        public bool Open { get; set; } //打开或关闭
    }
}
