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

namespace YTVisionPro.Hardware.Camera
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

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName {  get; set; }

        /// <summary>
        /// 用户定义名称
        /// </summary>
        public string UserDefinedName { get; set; }
        public string ClassName { get; set; } = typeof(CameraHik).FullName;


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
                int ret = DeviceEnumerator.EnumDevicesEx(DeviceTLayerType.MvGigEDevice, "Hikrobot", out devInfoList);
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
            nRet = DeviceEnumerator.EnumDevicesEx(DeviceTLayerType.MvGigEDevice | DeviceTLayerType.MvUsbDevice, "Hikrobot", out infoList);
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
            int nRet = 0;
            try
            {
                nRet = device.Open();
            }
            catch (Exception e)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"相机（{UserDefinedName}）打开失败！原因：{e.Message}", true);
                return false;
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
            // 连续模式
            device.Parameters.SetEnumValueByString("AcquisitionMode", "Continuous");
            // 设置触发模式
            SetTriggerMode(false);
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
            }

            PublishImageEvent?.Invoke(this, inputImage.ToBitmap());
        }

        /// <summary>
        /// 设置相机触发模式
        /// </summary>
        /// <param name="isTrigger"></param>
        public void SetTriggerMode(bool isTrigger)
        {
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
            // ch:触发源选择:0 - Line0; | en:Trigger source select:0 - Line0;
            //           1 - Line1;
            //           2 - Line2;
            //           3 - Line3;
            //           4 - Counter;
            //           7 - Software;
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

        /// <summary>
        /// 设置硬触发触发沿
        /// </summary>
        public void SetTriggerEdge(TriggerEdge triggerEdge)
        {
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
            device.Parameters.SetCommandValue("TriggerSoftware");
        }

        /// <summary>
        /// 获取曝光
        /// </summary>
        public float GetExposureTime()
        {
            device.Parameters.GetFloatValue("ExposureTime", out IFloatValue exposureTime);
            return exposureTime.CurValue;
        }

        /// <summary>
        /// 获取增益
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public float GetGain()
        {
            device.Parameters.GetFloatValue("Gain", out IFloatValue gain);
            return gain.CurValue;
        }

        /// <summary>
        /// 获取触发延迟时间
        /// </summary>
        /// <returns></returns>
        public float GetTriggerDelay()
        {
            device.Parameters.GetFloatValue("TriggerDelay", out IFloatValue triggerDelay);
            return triggerDelay.CurValue;
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gainValue"></param>
        public void SetGain(float gainValue)
        {
            device.Parameters.SetEnumValue("GainAuto", 0);
            device.Parameters.SetFloatValue("Gain", gainValue);
        }

        /// <summary>
        /// 设置曝光 
        /// </summary>
        /// <param name="time"></param>
        public void SetExposureTime(float time)
        {
            device.Parameters.SetEnumValue("ExposureAuto", 0); 
            device.Parameters.SetFloatValue("ExposureTime", time);
        }

        /// <summary>
        /// 设置触发延迟
        /// </summary>
        /// <param name="time">单位us</param>
        public void SetTriggerDelay(float time)
        {
            device.Parameters.SetFloatValue("TriggerDelay", time);
        }

        /// <summary>
        /// 开始取流
        /// </summary>
        /// <returns></returns>
        public void StartGrabbing()
        {
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
            device.StreamGrabber.StopGrabbing();
            _isGrabbing = false;
        }

        /// <summary>
        /// 获取相机取流状态
        /// </summary>
        /// <returns></returns>
        public bool GetGrabStatus()
        {
            return _isGrabbing;
        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            //先停止取流
            StopGrabbing();
            device.Close();
            IsOpen = false;
            ConnectStatusEvent?.Invoke(this, false);
        }

        /// <summary>
        /// 释放相机资源
        /// </summary>
        public void Dispose()
        {
            if(device != null)
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
