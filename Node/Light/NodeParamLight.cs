using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.Light
{
    public class NodeParamLight : INodeParam
    {
        public NodeParamLight(string SerialNumber, int ChannelValue, int Brightness)
        {
            this.SerialNumber = SerialNumber;
            this.ChannelValue = ChannelValue;
            this.Brightness = Brightness;
        }

        public string SerialNumber { get; } //串口号
        public int ChannelValue { get; } //通道数
        public int Brightness { get; } //光源值
    }
}
