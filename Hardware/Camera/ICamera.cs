using Basler.Pylon;
using MvCamCtrl.NET;
using MvCameraControl;
using System;
using System.Collections;
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
    internal interface ICamera : IDevice
    {
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
        void StartGrabbing();

        /// <summary>
        /// 停止取流
        /// </summary>
        /// <returns></returns>
        void StopGrabbing();

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
        void SetTriggerSource(TriggerSource triggerSource);

        /// <summary>
        /// 设置硬件触发时的触发沿
        /// </summary>
        /// <param name="triggerEdge"></param>
        /// <returns></returns>
        void SetTriggerEdge(TriggerEdge triggerEdge);

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        void GrabOne();

        /// <summary>
        ///  设置增益
        /// </summary>
        /// <param name="gainValue"></param>
        void SetGain(float gainValue);

        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="ExposureTime"></param>
        /// <returns></returns>
        void SetExposureTime(float time);

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        void Close();

        /// <summary>
        /// 释放相机资源
        /// </summary>
        void Dispose();
    }

    /// <summary>
    /// 各个品牌相机设备信息
    /// </summary>
    public struct CameraDevInfo
    {
        public IDeviceInfo Hik;
        public ICameraInfo Basler;
        public CameraDevInfo(IDeviceInfo hikInfo = null, ICameraInfo baslerInfo = null)
        {
            Hik = hikInfo;
            Basler = baslerInfo;
        }
    }


    /// <summary>
    /// 触发方式
    /// </summary>
    public enum TriggerSource 
    {
        Auto,
        SOFT,
        LINE0,
        LINE1,
        LINE2,
        LINE3,
        LINE4,
    }

    /// <summary>
    /// 硬触发设置的触发沿
    /// </summary>
    public enum TriggerEdge 
    {
        /// <summary>
        /// 上升沿
        /// </summary>
        Rising,
        /// <summary>
        /// 下降沿
        /// </summary>
        Falling,
        /// <summary>
        /// 包括上升沿和下降沿
        /// </summary>
        Any,
        /// <summary>
        /// 高电平
        /// </summary>
        Hight,
        /// <summary>
        /// 低电平
        /// </summary>
        Low
    }


    /// <summary>
    /// 相机品牌
    /// </summary>
    public enum CameraBrand 
    {
        /// <summary>
        /// 海康威视
        /// </summary>
        HiKVision,
        /// <summary>
        /// 巴斯勒
        /// </summary>
        Basler
    }


}
