using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YTVisionPro.Hardware.Camera
{
    /// <summary>
    /// 相机基类
    /// </summary>
    public interface ICamera : IDevice
    {
        /// <summary>
        /// 相机取流得到的一帧图像
        /// </summary>
        Bitmap Bitmap { get; }
        /// <summary>
        /// 相机是否打开
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        DevType DevType { get; }

        /// <summary>
        /// 开启相机
        /// </summary>
        /// <returns></returns>
        bool Open();

        /// <summary>
        /// 开始取流
        /// </summary>
        /// <returns></returns>
        bool StartGrabbing();

        /// <summary>
        /// 停止取流
        /// </summary>
        /// <returns></returns>
        bool StopGrabbing();

        /// <summary>
        /// 设置相机模式
        /// </summary>
        /// <param name="isTrigger"></param>
        void SetTriggerMode(bool isTrigger);

        /// <summary>
        /// 设置软硬触发
        /// </summary>
        /// <param name="triggerSource"></param>
        /// <returns></returns>
        bool SetTriggerSource(TriggerSource triggerSource);

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        bool GrabOne();

        /// <summary>
        ///  设置增益
        /// </summary>
        /// <param name="gainValue"></param>
        void SetGain(int gainValue);

        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="ExposureTime"></param>
        /// <returns></returns>
        void SetExposureTime(int time);

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        bool Close();
    }

    /// <summary>
    /// 触发源
    /// </summary>
    public enum TriggerSource 
    {
        SOFT,
        LINE0,
        LINE1,
        LINE2,
        LINE3,
    }


}
