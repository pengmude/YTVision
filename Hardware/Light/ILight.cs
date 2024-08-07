using System.IO.Ports;

namespace YTVisionPro.Hardware.Light
{
    public interface ILight : IDevice
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        string Id { get; set; }

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
        DevType DevType { get; }

        LightBrand Brand { get; }


        /// <summary>
        /// 光源是否打开
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>
        /// 光源亮度值
        /// </summary>
        int Brightness { get; set; }

        string PortName { get; set; }

        /// <summary>
        /// 通过串口去连接光源
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <param name="parity"></param>
        bool Connenct(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity);

        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 打开光源
        /// </summary>
        void TurnOn();

        /// <summary>
        /// 关闭光源
        /// </summary>
        void TurnOff();

        /// <summary>
        /// 设置光源亮度值（0-255）
        /// </summary>
        void SetValue(int value);
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
        RSEE
    }
}