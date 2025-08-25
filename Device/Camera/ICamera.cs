using Basler.Pylon;
using MvCameraControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using TDJS_Vision.Forms.ImageViewer;

namespace TDJS_Vision.Device.Camera
{
    /// <summary>
    /// 相机基类
    /// </summary>
    public interface ICamera : IDevice
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
        /// 相机是否连接
        /// </summary>
        bool IsOpen { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        string SN { get; }
        /// <summary>
        /// 相机品牌
        /// </summary>
        DeviceBrand Brand { get; set; }
        string ClassName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        DevType DevType { get; set; }
        /// <summary>
        /// 创建设备，反序列化用
        /// </summary>
        void CreateDevice();
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
        /// 获取相机取流状态
        /// </summary>
        /// <returns></returns>
        bool GetGrabStatus();

        /// <summary>
        /// 设置相机触发模式
        /// </summary>
        /// <param name="isTrigger"></param>
        void SetTriggerMode(bool isTrigger);

        /// <summary>
        /// 设置软硬触发
        /// </summary>
        /// <param name="triggerSource"></param>
        /// <returns></returns>
        void SetTriggerSource(TriggerSource triggerSource);

        Bitmap GetOneFrameImage();

        TriggerSource GetTriggerSource();
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
        void SetGain(double gainValue);

        /// <summary>
        /// 获取相机曝光
        /// </summary>
        /// <returns></returns>
        IFloatValue GetExposureTime();

        /// <summary>
        /// 获取相机增益
        /// </summary>
        /// <returns></returns>
        (IIntValue, IFloatValue) GetGain();

        /// <summary>
        /// 获取触发延迟
        /// </summary>
        /// <returns></returns>
        IFloatValue GetTriggerDelay();

        /// <summary>
        /// 获取线路选择器
        /// </summary>
        IEnumValue GetLineSelector();

        /// <summary>
        /// 设置线路选择器
        /// </summary>
        void SetLineSelector(string line);

        /// <summary>
        /// 获取线路模式
        /// </summary>
        /// <returns></returns>
        IEnumValue GetLineMode();

        /// <summary>
        /// 设置线路模式
        /// </summary>
        /// <param name="lineMode"></param>
        void SetLineMode(string lineMode);

        /// <summary>
        /// 获取使能
        /// </summary>
        /// <returns></returns>
        bool GetStrobeEnable();

        /// <summary>
        /// 设置使能
        /// </summary>
        /// <param name="enable"></param>
        void SetStrobeEnable(bool enable);

        /// <summary>
        /// 设置线路反转
        /// </summary>
        /// <param name="inverter"></param>
        void SetLineInverter(bool inverter);

        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="ExposureTime"></param>
        /// <returns></returns>
        void SetExposureTime(double time);

        /// <summary>
        /// 设置触发延迟
        /// </summary>
        /// <param name="time"></param>
        void SetTriggerDelay(double time);

        /// <summary>
        /// 获取触发模式
        /// </summary>
        /// <returns></returns>
        bool GetTriggerMode();

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
        public IDeviceInfo cameraInfo;
        public CameraDevInfo(IDeviceInfo Info)
        {
            cameraInfo = Info;
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
        Basler,
        /// <summary>
        /// 大恒相机
        /// </summary>
        DaHeng,
        /// <summary>
        /// 大华相机
        /// </summary>
        DaHua
    }
}
