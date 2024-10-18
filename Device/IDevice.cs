using JsonSubTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using YTVisionPro.Device.Light;

namespace YTVisionPro.Device
{
    internal interface IDevice
    {
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
        /// <summary>
        /// 创建设备，反序列化用
        /// </summary>
        void CreateDevice();
    }

    /// <summary>
    /// 设备类型：光源、相机和PLC
    /// </summary>
    public enum DevType
    {
        /// <summary>
        /// 光源
        /// </summary>
        LIGHT,
        /// <summary>
        /// 相机
        /// </summary>
        CAMERA,
        /// <summary>
        /// PLC
        /// </summary>
        PLC,
        /// <summary>
        /// Modbu主站
        /// </summary>
        ModbusPoll,
        /// <summary>
        /// Modbu从站
        /// </summary>
        ModbusSlave,
        /// <summary>
        /// Tcpf服务器
        /// </summary>
        TcpServer,
        /// <summary>
        /// Tcp客户端
        /// </summary>
        TcpClient
    }

    /// <summary>
    /// 设备品牌
    /// </summary>
    public enum DeviceBrand
    {
        /// <summary>
        /// 未知品牌
        /// </summary>
        Unknow,
        /// <summary>
        /// 磐鑫
        /// </summary>
        PPX,
        /// <summary>
        /// 锐视
        /// </summary>
        Rsee,
        /// <summary>
        /// 海康威视
        /// </summary>
        HikVision,
        /// <summary>
        /// 巴斯勒
        /// </summary>
        Basler,
        /// <summary>
        /// 松下
        /// </summary>
        Panasonic
    }
}
