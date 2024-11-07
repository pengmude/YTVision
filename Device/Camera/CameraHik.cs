using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Logger;
using MvCameraControl;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using JsonSubTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sunny.UI.Win32;
using YTVisionPro.Node.AI.HTAI;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace YTVisionPro.Device.Camera
{
    /// <summary>
    /// 海康相机类
    /// </summary>
    internal class CameraHik : ICamera
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        public event EventHandler<bool> ConnectStatusEvent;

        /// <summary>
        /// 抓取图片事件
        /// </summary>
        public event EventHandler<Bitmap> PublishImageEvent;

        /// <summary>
        /// 单个海康相机信息
        /// </summary>
        private MvCameraControl.IDevice device = null;

        /// <summary>
        /// 是否正在取流
        /// </summary>
        private bool _isGrabbing = false;

        /// <summary>
        /// 设备是否启用
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DevType DevType { get; set; } = DevType.CAMERA;

        /// <summary>
        /// 相机SN
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 相机品牌
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceBrand Brand { get; set; } = DeviceBrand.HikVision;
        public string ClassName { get; set; } = typeof(CameraHik).FullName;

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName {  get; set; }

        /// <summary>
        /// 用户定义名称
        /// </summary>
        public string UserDefinedName { get; set; }

        #region 反序列化相关函数

        [JsonConstructor]
        /// <summary>
        /// 无参构造提供给反序列化使用
        /// 反序列化相机对象步骤：
        /// 1.使用无参构造函数创建对象
        /// 2.通过反序列化得到的SN对比找到对应IDevice类相机对象
        /// 3.调用打开相机
        /// </summary>
        public CameraHik() { }

        /// <summary>
        /// 创建相机设备
        /// </summary>
        /// <param name="devIP"></param>
        /// <returns></returns>
        public void CreateDevice()
        {
            try
            {
                /*只枚举网口类型的相机*/
                List<IDeviceInfo> devInfoList = null;
                int ret = DeviceEnumerator.EnumDevices(DeviceTLayerType.MvGigEDevice, out devInfoList);
                if (ret == MvError.MV_OK)
                {
                    foreach (IGigEDeviceInfo devInfo in devInfoList)
                    {
                        if (devInfo.SerialNumber == SN)
                        {
                            device = DeviceFactory.CreateDevice(devInfo);
                            return;
                        }
                    }
                }
                throw new Exception("创建相机失败！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        /// <summary>
        /// 使用相机信息构造相机对象
        /// </summary>
        /// <param name="DeviceInfo"></param>
        internal CameraHik(IDeviceInfo devInfo, string userName)
        {
            try
            {
                device = DeviceFactory.CreateDevice(devInfo);
                DevName = GetDevNameByDevInfo(device.DeviceInfo);
                UserDefinedName = userName;
                if (devInfo is IGigEDeviceInfo info)
                {
                    SN = info.SerialNumber;
                }
                else
                    throw new Exception("暂时不支持非网口连接的相机！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 初始化SDK
        /// </summary>
        public static void InitSDK()
        {
            SDKSystem.Initialize();
        }

        /// <summary>
        /// 反初始化SDK
        /// </summary>
        public static void Finalize()
        {
            SDKSystem.Finalize();
        }

        /// <summary>
        /// 根据设备信息获取设备名称
        /// </summary>
        /// <param name="cameraDevInfo"></param>
        /// <returns></returns>
        public static string GetDevNameByDevInfo(IDeviceInfo cameraDevInfo)
        {
            string name = "";
            if (cameraDevInfo.UserDefinedName != "")
            {
                name = (cameraDevInfo.UserDefinedName + "(" + cameraDevInfo.SerialNumber + ")");
            }
            else
            {
                name = (cameraDevInfo.ManufacturerName + cameraDevInfo.ModelName + " (" + cameraDevInfo.SerialNumber + ")");
            }
            return name;
        }

        /// <summary>
        /// 查找相机
        /// </summary>
        /// <returns></returns>
        public static List<IDeviceInfo> FindCamera()
        {
            GC.Collect();
            int nRet;
            List<IDeviceInfo> infoList = null;
            nRet = DeviceEnumerator.EnumDevices(DeviceTLayerType.MvGigEDevice, out infoList);
            if (MvError.MV_OK != nRet)
            {
                LogHelper.AddLog(MsgLevel.Exception, "获取相机列表失败", true);
            }
            return infoList;
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="CamerSerialization"></param>
        /// <returns></returns>
        public bool Open()
        {
            if (device == null) throw new Exception("相机对象为空！");

            int nRet = 0;
            nRet = device.Open();
            if (nRet != MvError.MV_OK)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"相机（{UserDefinedName}）打开失败！原因：{nRet}", true);
                throw new Exception($"相机（{UserDefinedName}）打开失败！");
            }
            IsOpen = true;
            ConnectStatusEvent?.Invoke(this, true);
            //ch: 判断是否为gige设备 | en: Determine whether it is a GigE device
            if (device is IGigEDevice)
            {
                //ch: 转换为gigE设备 | en: Convert to Gige device
                IGigEDevice gigEDevice = device as IGigEDevice;

                // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
                int optionPacketSize;
                nRet = gigEDevice.GetOptimalPacketSize(out optionPacketSize);
                if (nRet != MvError.MV_OK)
                {
                    LogHelper.AddLog(MsgLevel.Warn, $"获取网络最佳包大小失败！{nRet}");
                    return false;
                }
                else
                {
                    nRet = device.Parameters.SetIntValue("GevSCPSPacketSize", (long)optionPacketSize);
                    LogHelper.AddLog(MsgLevel.Info, $"设置网络最佳包大小：{optionPacketSize}", true);
                    if (nRet != MvError.MV_OK)
                    {
                        LogHelper.AddLog(MsgLevel.Warn, $"设置网络最佳包大小失败！错误码：{nRet}");
                        return false;
                    }
                }
            }
            else
            {
                throw new Exception("暂不支持非GigE相机！");
            }
            // 注册回调函数
            device.StreamGrabber.FrameGrabedEvent -= FrameGrabedEventHandler;
            device.StreamGrabber.FrameGrabedEvent += FrameGrabedEventHandler;
            return true;
        }

        /// <summary>
        /// 取流回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameGrabedEventHandler(object sender, FrameGrabbedEventArgs e)
        {
            IImage inputImage = e.FrameOut.Image;
            uint nChannelNum = 0;
            PixelFormat m_bitmapPixelFormat = PixelFormat.Undefined;
            MvGvspPixelType dstPixelType = MvGvspPixelType.PixelType_Gvsp_Undefined;
            if (IsColorPixelFormat(e.FrameOut.Image.PixelType))
            {
                dstPixelType = MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
                m_bitmapPixelFormat = PixelFormat.Format24bppRgb;
                nChannelNum = 3;
            }
            else if (IsMonoPixelFormat(e.FrameOut.Image.PixelType))
            {
                dstPixelType = MvGvspPixelType.PixelType_Gvsp_Mono8;
                m_bitmapPixelFormat = PixelFormat.Format8bppIndexed;
                nChannelNum = 1;
            }
            //通过设置调色板从伪彩改为灰度
            if (nChannelNum == 1)
            {
                var pal = inputImage.ToBitmap().Palette;
                for (int j = 0; j < 256; j++)
                    pal.Entries[j] = Color.FromArgb(j, j, j);
                inputImage.ToBitmap().Palette = pal;

                PublishImageEvent?.Invoke(this, inputImage.ToBitmap());
            }
            else if (nChannelNum == 3)
            {
                Mat mat = new Mat((int)inputImage.Height, (int)inputImage.Width, MatType.CV_8UC1);
                //使用Marshal.Copy将像素数据复制到Mat的数据区域
                Marshal.Copy(inputImage.PixelData, 0, mat.Data, inputImage.PixelData.Length);
                Bitmap bitmap = BitmapConverter.ToBitmap(mat);
                PublishImageEvent?.Invoke(this, bitmap);
                mat.Dispose();
                return;
            }
        }

        /// <summary>
        /// 设置相机触发模式
        /// </summary>
        /// <param name="isTrigger"></param>
        public void SetTriggerMode(bool isTrigger)
        {
            if (device == null) throw new Exception("相机对象为空！");
            device.Parameters.SetEnumValue("TriggerMode", isTrigger ? 1u : 0u);
        }

        /// <summary>
        /// 设置软硬触发
        /// </summary>
        /// <param name="trigBySoft"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void SetTriggerSource(TriggerSource triggerSource)
        {
            if (device == null) throw new Exception("相机对象为空！");
            // ch:触发源选择:0 - Line0; | en:Trigger source select:0 - Line0;
            //           1 - Line1;
            //           2 - Line2;
            //           3 - Line3;
            //           4 - Counter;
            //           7 - Software;
            if (device.DeviceInfo.ManufacturerName == "Basler")
            {
                switch (triggerSource)
                {
                    case TriggerSource.SOFT:
                        device.Parameters.SetEnumValue("TriggerSource", 0);
                        break;
                    case TriggerSource.LINE1:
                        device.Parameters.SetEnumValue("TriggerSource", 1);
                        break;
                    case TriggerSource.LINE2:
                        device.Parameters.SetEnumValue("TriggerSource", 2);
                        break;
                    case TriggerSource.LINE3:
                        device.Parameters.SetEnumValue("TriggerSource", 3);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (triggerSource)
                {
                    case TriggerSource.SOFT:
                        device.Parameters.SetEnumValue("TriggerSource", 7);
                        break;
                    case TriggerSource.LINE0:
                        device.Parameters.SetEnumValue("TriggerSource", 0);
                        break;
                    case TriggerSource.LINE1:
                        device.Parameters.SetEnumValue("TriggerSource", 1);
                        break;
                    case TriggerSource.LINE2:
                        device.Parameters.SetEnumValue("TriggerSource", 2);
                        break;
                    case TriggerSource.LINE3:
                        device.Parameters.SetEnumValue("TriggerSource", 3);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 设置硬触发触发沿
        /// </summary>
        public void SetTriggerEdge(TriggerEdge triggerEdge)
        {
            if (device == null) throw new Exception("相机对象为空！");
            switch (triggerEdge)
            {
                case TriggerEdge.Rising:
                    device.Parameters.SetEnumValueByString("TriggerActivation", "RisingEdge");
                    break;
                case TriggerEdge.Falling:
                    device.Parameters.SetEnumValueByString("TriggerActivation", "FallingEdge");
                    break;
                case TriggerEdge.Low:
                    device.Parameters.SetEnumValueByString("TriggerActivation", "LevelLow");
                    break;
                case TriggerEdge.Hight:
                    device.Parameters.SetEnumValueByString("TriggerActivation", "LevelHigh");
                    break;
                case TriggerEdge.Any:
                    device.Parameters.SetEnumValueByString("TriggerActivation", "AnyEdge");
                    break;
            }
        }

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        public void GrabOne()
        {
            if (device == null) throw new Exception("相机对象为空！");
            device.Parameters.SetCommandValue("TriggerSoftware");
        }

        /// <summary>
        /// 获取曝光
        /// </summary>
        public float GetExposureTime()
        {
            if (device == null) throw new Exception("相机对象为空！");
            if (device.DeviceInfo.ManufacturerName == "Basler")
            {
                device.Parameters.GetFloatValue("ExposureTimeAbs", out IFloatValue exposureTime);
                return exposureTime.CurValue;
            }
            else
            {
                device.Parameters.GetFloatValue("ExposureTime", out IFloatValue exposureTime);
                return exposureTime.CurValue;
            }
        }

        /// <summary>
        /// 获取增益
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public float GetGain()
        {
            if (device == null) throw new Exception("相机对象为空！");

            if (device.DeviceInfo.ManufacturerName == "Basler")
            {
                device.Parameters.GetIntValue("GainRaw", out IIntValue gain);
                return gain.CurValue;
            }
            else
            {
                device.Parameters.GetFloatValue("Gain", out IFloatValue gain);
                return gain.CurValue;
            }
        }

        /// <summary>
        /// 获取触发延迟时间
        /// </summary>
        /// <returns></returns>
        public float GetTriggerDelay()
        {
            if (device == null) throw new Exception("相机对象为空！");
            if (device.DeviceInfo.ManufacturerName == "Basler")
            {
                device.Parameters.GetFloatValue("TriggerDelayAbs", out IFloatValue triggerDelay);
                return triggerDelay.CurValue;
            }
            else
            {
                device.Parameters.GetFloatValue("TriggerDelay", out IFloatValue triggerDelay);
                return triggerDelay.CurValue;
            }
        }

        /// <summary>
        /// 获取线路选择器
        /// </summary>
        /// <returns></returns>
        public IEnumValue GetLineSelector()
        {
            if (device == null) throw new Exception("相机对象为空！");
            IEnumValue enumValue;
            int Rnet;
            Rnet = device.Parameters.GetEnumValue("LineSelector", out enumValue);
            if (Rnet != MvError.MV_OK)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"获取线路选择器失败！错误码：{Rnet}", true);
            }
            return enumValue;
        }

        /// <summary>
        /// 设置线路
        /// </summary>
        /// <param name="line"></param>
        public void SetLineSelector(string line)
        {
            if (device == null) throw new Exception("相机对象为空！");
            int Rnet = 0;
            switch (line)
            {
                case "Line0":
                    Rnet = device.Parameters.SetEnumValue("LineSelector", 0);
                    break;
                case "Line1":
                    Rnet = device.Parameters.SetEnumValue("LineSelector", 1);
                    break;
               case "Line2":
                    Rnet = device.Parameters.SetEnumValue("LineSelector", 2);
                    break;
                default:
                    break;
            }
            if (Rnet != MvError.MV_OK)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"设置线路选择器失败！错误码：{Rnet}", true);
            }
        }

        /// <summary>
        /// 获取线路模式
        /// </summary>
        /// <returns></returns>
        public IEnumValue GetLineMode()
        { 
            if (device == null) throw new Exception("相机对象为空！");
            int Rnet = 0;
            IEnumValue enumValue;
            device.Parameters.GetEnumValue("LineMode", out enumValue);
            if (Rnet != MvError.MV_OK)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"获取线路模式失败！错误码：{Rnet}", true);
            }
            return enumValue;
        }

        /// <summary>
        /// 设置线路模式
        /// </summary>
        /// <param name="lineMode"></param>
        public void SetLineMode(string lineMode)
        {
            if (device == null) throw new Exception("相机对象为空！");
            int Rnet = 0;
            switch (lineMode)
            { 
                case "输入":
                    Rnet = device.Parameters.SetEnumValue("LineMode", 0);
                    break;
                case "输出":
                    Rnet = device.Parameters.SetEnumValue("LineMode", 8);
                    break;
                default:
                    break;
            }
            if (Rnet != MvError.MV_OK)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"设置线路模式失败！错误码：{Rnet}", true);
            }
        }

        /// <summary>
        /// 获取使能
        /// </summary>
        /// <returns></returns>
        public bool GetStrobeEnable() 
        { 
            if(device == null) throw new Exception("相机对象为空！");
            int Rnet = 0;
            bool enable;
            Rnet = device.Parameters.GetBoolValue("StrobeEnable", out enable);

            if (Rnet != MvError.MV_OK)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"设置线路源失败！错误码：{Rnet}", true);
            }

            return enable;
        }

        /// <summary>
        /// 设置使能
        /// </summary>
        /// <param name="enable"></param>
        public void SetStrobeEnable(bool enable)
        {
            if (device == null) throw new Exception("相机对象为空！");
            int Rnet = 0;

            Rnet = device.Parameters.SetBoolValue("StrobeEnable", enable);

            if (Rnet != MvError.MV_OK)
            { 
                LogHelper.AddLog(MsgLevel.Exception, $"设置线路源失败！错误码：{Rnet}", true);
            }
        }

        /// <summary>
        /// 设置线路反转
        /// </summary>
        /// <param name="inverter"></param>
        public void SetLineInverter(bool inverter)
        {
            if (device == null) throw new Exception("相机对象为空！");
            int Rnet = 0;
            Rnet = device.Parameters.SetBoolValue("LineInverter", inverter);
            if (Rnet != MvError.MV_OK)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"设置线路反转失败！错误码：{Rnet}", true);
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gainValue"></param>
        public void SetGain(float gainValue)
        {
            if (device == null) throw new Exception("相机对象为空！");
            device.Parameters.SetEnumValue("GainAuto", 0);
            if (device.DeviceInfo.ManufacturerName == "Basler")
            {
                device.Parameters.SetIntValue("GainRaw", (int)gainValue);
            }
            else
            {
                device.Parameters.SetFloatValue("Gain", gainValue);
            }
        }

        /// <summary>
        /// 设置曝光 
        /// </summary>
        /// <param name="time"></param>
        public void SetExposureTime(float time)
        {
            if (device == null) throw new Exception("相机对象为空！");
            device.Parameters.SetEnumValue("ExposureAuto", 0);

            if (device.DeviceInfo.ManufacturerName == "Basler")
            {
                device.Parameters.SetFloatValue("ExposureTimeAbs", time);
            }
            else
            {
                device.Parameters.SetFloatValue("ExposureTime", time);
            }
        }

        /// <summary>
        /// 设置触发延迟
        /// </summary>
        /// <param name="time">单位us</param>
        public void SetTriggerDelay(float time)
        {
            if (device == null) throw new Exception("相机对象为空！");

            if (device.DeviceInfo.ManufacturerName == "Basler")
            {
                device.Parameters.SetFloatValue("TriggerDelayAbs", time);
            }
            else
            {
                device.Parameters.SetFloatValue("TriggerDelay", time);
            }
        }

        /// <summary>
        /// 开始取流
        /// </summary>
        /// <returns></returns>
        public void StartGrabbing()
        {
            if (device == null) throw new Exception("相机对象为空！");
            if (!_isGrabbing)
            {
                device.StreamGrabber.StartGrabbing();
                _isGrabbing = true;
            }
        }

        /// <summary>
        /// 停止取流
        /// </summary>
        /// <returns></returns>
        public void StopGrabbing()
        {
            if (device == null) throw new Exception("相机对象为空！");
            device.StreamGrabber.StopGrabbing();
            _isGrabbing = false;
        }

        /// <summary>
        /// 获取相机取流状态
        /// </summary>
        /// <returns></returns>
        public bool GetGrabStatus()
        {
            if (device == null) throw new Exception("相机对象为空！");
            return _isGrabbing;
        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            if (device == null) throw new Exception("相机对象为空！");
            //先停止取流
            if (_isGrabbing)
            {
                StopGrabbing();
            }
            device.Close();
            IsOpen = false;
            ConnectStatusEvent?.Invoke(this, false);
        }

        /// <summary>
        /// 释放相机资源
        /// </summary>
        public void Dispose()
        {
            if (device != null)
            {
                if (device.IsConnected) { device.Close(); }
                device?.Dispose();
            }
        }

        /// <summary>
        /// 判断是否为彩色图像
        /// </summary>
        /// <param name="enType"></param>
        /// <returns></returns>
        private bool IsColorPixelFormat(MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_RGBA8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BGRA8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 判断图像是否是黑白图像
        /// </summary>
        /// <param name="enType"></param>
        /// <returns></returns>
        private bool IsMonoPixelFormat(MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;
                default:
                    return false;
            }
        }
    }
}
