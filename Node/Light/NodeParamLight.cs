using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.Light
{
    /// <summary>
    /// 光源节点参数
    /// </summary>
    public class NodeParamLight : INodeParam
    {
        /// <summary>
        /// 光源亮度
        /// </summary>
        public int Brightness;
        /// <summary>
        /// 光源所属串口号
        /// </summary>
        public string PortName;
        /// <summary>
        /// 光源所属通道
        /// </summary>
        public int Chanel;

        public NodeParamLight(string port, int chanel, int brightness) 
        {
            PortName = port;
            Chanel = chanel;
            Brightness = brightness;
        }
    }
}
