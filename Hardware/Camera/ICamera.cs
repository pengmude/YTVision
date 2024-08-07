using System.Drawing;

namespace YTVisionPro.Hardware.Camera
{

    //图像委托事件，用于传出参数
    public delegate void GetImageDelegate(Image Image);

    /// <summary>
    /// 相机基类
    /// </summary>
    public interface ICamera : IDevice
    {
        /// <summary>
        /// 相机是否打开
        /// </summary>
        bool IsOpen { get; set; }
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

        DevType DevType { get; }

        /// <summary>
        /// 根据相机序列号开启相机
        /// </summary>
        /// <param name="CamerName"></param>
        /// <returns></returns>
        bool Open();

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        void Close();

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        bool GrabOne();

        /// <summary>
        /// 启动为硬触发
        /// </summary>
        /// <returns></returns>
        bool GrapEncoder();

        /// <summary>
        /// 重连相机
        /// </summary>
        /// <param name="CamerName"></param>
        /// <returns></returns>
        bool Reconnect();

        void SetGain(int gainValue);

        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="ExposureTime"></param>
        /// <returns></returns>
        void SetExposureTime(int time);

    }
}
