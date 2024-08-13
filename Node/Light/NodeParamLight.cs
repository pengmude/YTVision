using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.Light
{
    public class NodeParamLight : INodeParam
    {
        public NodeParamLight()
        {
            
        }

        public NodeParamLight(string SerialNumber, int ChannelValue, int Brightness, bool open)
        {
            this.SerialNumber = SerialNumber;
            this.ChannelValue = ChannelValue;
            this.Brightness = Brightness;
            this.Open = open;
        }

        public string SerialNumber { get; set; } //串口号
        public int ChannelValue { get; set; } //通道数
        public int Brightness { get; set; } //光源值

        public bool Open { get; set; } //打开或关闭
    }
}
