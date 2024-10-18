using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Device.TCP
{
    internal interface ITcpDevice : IDevice
    {
        /// <summary>
        /// TCP设备参数
        /// </summary>
        TcpParam TcpParam { get; set; }
        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        event EventHandler<bool> ConnectStatusEvent;
        /// <summary>
        /// 硬件硬件名称
        /// </summary>
        string DevName { get; set; }
        /// <summary>
        /// 用户自定义设备名
        /// </summary>
        string UserDefinedName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        DevType DevType { get; set; }
        /// <summary>
        /// 设备品牌
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        DeviceBrand Brand { get; set; }

        string ClassName { get; set; }

        bool IsConnect { get; set; }
        /// <summary>
        /// 创建设备，反序列化用
        /// </summary>
        void CreateDevice();

        void Connect();

        void Disconnect();
    }
}
