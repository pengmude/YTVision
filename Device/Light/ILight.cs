using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO.Ports;

namespace YTVisionPro.Device.Light
{
    internal interface ILight : IDevice
    {
        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        event EventHandler<bool> ConnectStatusEvent;
        /// <summary>
        /// 光源是否打开
        /// </summary>
        bool IsOpen { get; set; }
        /// <summary>
        /// 光源串口是否打开
        /// </summary>
        bool IsComOpen { get; set; }
        LightParam LightParam { get; set; }
        /// <summary>
        /// 光源品牌
        /// </summary>
        DeviceBrand Brand { get; }
        /// <summary>
        /// 设备类型
        /// </summary>
        DevType DevType { get; set; }
        string ClassName { get; set; }
        /// <summary>
        /// 光源亮度值
        /// </summary>
        int Brightness { get; set; }
        /// <summary>
        /// 创建设备，反序列化用
        /// </summary>
        void CreateDevice();

        /// <summary>
        /// 通过串口去连接光源
        /// </summary>
        void Connenct();

        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 打开光源
        /// </summary>
        void TurnOn(int value, int time = -1);

        /// <summary>
        /// 关闭光源
        /// </summary>
        void TurnOff();
    }

    public struct LightParam
    {
        /// <summary>
        /// 串口号
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public StopBits StopBits { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Parity Parity { get; set; }

        /// <summary>
        /// 光源所在通道
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 光源名称
        /// </summary>
        public string LightName { get; set; }


        /// <summary>
        /// 设备品牌
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceBrand Brand { get; set; }

        /// <summary>
        /// 锐视光源型号
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public RseeDeviceType RseeDeviceType { get; set; }

        /// <summary>
        /// 亮度值
        /// </summary>
        public int Value {  get; set; }
    }
}