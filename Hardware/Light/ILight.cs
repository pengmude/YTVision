using System;
using System.IO.Ports;
using YTVisionPro.Forms.LightAdd;

namespace YTVisionPro.Hardware.Light
{
    internal interface ILight : IDevice
    {
        LightParam LightParam { get; set; }

        LightBrand Brand { get; }

        /// <summary>
        /// 光源亮度值
        /// </summary>
        int Brightness { get; set; }

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
        void TurnOn(int value);

        /// <summary>
        /// 关闭光源
        /// </summary>
        void TurnOff();
    }

    public enum LightBrand
    {
        /// <summary>
        /// 磐鑫
        /// </summary>
        PPX,
        /// <summary>
        /// 锐视
        /// </summary>
        RSEE,
        /// <summary>
        /// 未知品牌
        /// </summary>
        UNKNOW
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
        public StopBits StopBits { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
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
        /// 光源品牌
        /// </summary>
        public LightBrand Brand { get; set; }

        /// <summary>
        /// 锐视光源型号
        /// </summary>
        public RseeDeviceType RseeDeviceType { get; set; }

        /// <summary>
        /// 亮度值
        /// </summary>
        public int Value {  get; set; }
    }
}