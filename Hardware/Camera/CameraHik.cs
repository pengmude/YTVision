//using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Logger;
using MvCameraControl;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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
        /// 保存已创建的海康相机对象，便于相机对象数量为0时，自动调用SDK反初始化
        /// </summary>
        private List<MvCameraControl.IDevice> _hikList = new List<MvCameraControl.IDevice>();

        /// <summary>
        /// 抓取图片事件
        /// </summary>
        public event EventHandler<Bitmap> PublishImageEvent;

        /// <summary>
        /// 单个海康相机信息
        /// </summary>
        private MvCameraControl.IDevice device = null;

        /// <summary>
        /// 设备是否启用
        /// </summary>
        public bool IsOpen => device.IsConnected;

        /// <summary>
        /// 设备类型
        /// </summary>
        public DevType DevType { get; } = DevType.CAMERA;

        /// <summary>
        /// 相机ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 相机品牌
        /// </summary>
        public CameraBrand Brand { get; set; } = CameraBrand.HiKVision;

        /// <summary>
        /// 设备信息
        /// </summary>
        public IDeviceInfo DevInfo { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName => GetDevName(DevInfo);

        /// <summary>
        /// 根据设备信息获取设备名称
        /// </summary>
        /// <param name="cameraDevInfo"></param>
        /// <returns></returns>
        public static string GetDevName(IDeviceInfo cameraDevInfo)
        {
            string name = "";
            if (cameraDevInfo.UserDefinedName != "")
            {
                name = (cameraDevInfo.TLayerType.ToString() + ": " + cameraDevInfo.UserDefinedName + " (" + cameraDevInfo.SerialNumber + ")");
            }
            else
            {
                name = (cameraDevInfo.TLayerType.ToString() + ": " + cameraDevInfo.ManufacturerName + " " + cameraDevInfo.ModelName + " (" + cameraDevInfo.SerialNumber + ")");
            }
            return name;
        }

        /// <summary>
        /// 用户定义名称
        /// </summary>
        public string UserDefinedName { get; set; }

        /// <summary>
        /// 使用相机信息构造相机对象
        /// </summary>
        /// <param name="DeviceInfo"></param>
        public CameraHik(IDeviceInfo devInfo, string userName)
        {
            try
            {
                device = DeviceFactory.CreateDevice(devInfo);
                DevInfo = devInfo;
                _hikList.Add(device);
                UserDefinedName = userName;
            }
            catch (Exception ex)
            {
                Dispose();
                LogHelper.AddLog(MsgLevel.Fatal, ex.Message, true);
                return;
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
            nRet = device.Open();

            if (MvError.MV_OK != nRet)
            {
                LogHelper.AddLog(MsgLevel.Fatal, "打开相机失败！", true);
                MessageBox.Show("打开相机失败！");
                return false;
            }

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
                    if (nRet != MvError.MV_OK)
                    {
                        LogHelper.AddLog(MsgLevel.Warn, $"设置网络最佳包大小失败！错误码：{nRet}");
                        return false;
                    }
                }
            }
            // 连续模式
            device.Parameters.SetEnumValueByString("AcquisitionMode", "Continuous");
            // 关闭触发模式
            device.Parameters.SetEnumValue("TriggerMode", 0);
            // 设置适合缓存节点数量
            device.StreamGrabber.SetImageNodeNum(5);
            // 注册回调函数
            device.StreamGrabber.FrameGrabedEvent += FrameGrabedEventHandler;
            return true;
        }

        /// <summary>
        /// 取流回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void FrameGrabedEventHandler(object sender, FrameGrabbedEventArgs e)
        {
            int nRet;
            IImage inputImage = e.FrameOut.Image;
            IImage outImage;
            IntPtr pImageBuf = IntPtr.Zero;
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
            else
            {
                LogHelper.AddLog(MsgLevel.Warn, $"采集图像格式错误，不为彩色和黑白图像", true);
                return;
            }
            if (IntPtr.Zero == pImageBuf)
            {
                pImageBuf = Marshal.AllocHGlobal((int)(e.FrameOut.Image.Width * e.FrameOut.Image.Height * nChannelNum));
                if (IntPtr.Zero == pImageBuf)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"图像采集为空", true);
                    return;
                }
            }
            nRet = device.PixelTypeConverter.ConvertPixelType(inputImage, out outImage, dstPixelType);
            if (MvError.MV_OK != nRet)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"图像转换异常", true);
                return;
            }

            Bitmap Bitmap = new Bitmap((Int32)outImage.Width, (Int32)outImage.Height, m_bitmapPixelFormat);
            BitmapData bitmapData = Bitmap.LockBits(new Rectangle(0, 0, (Int32)outImage.Width, (Int32)outImage.Height), ImageLockMode.ReadWrite, Bitmap.PixelFormat);
            CopyMemory(bitmapData.Scan0, outImage.PixelDataPtr, (UInt32)(bitmapData.Stride * Bitmap.Height));
            Bitmap.UnlockBits(bitmapData);

            //通过设置调色板从伪彩改为灰度
            if (nChannelNum == 1)
            {
                var pal = Bitmap.Palette;
                for (int j = 0; j < 256; j++)
                    pal.Entries[j] = System.Drawing.Color.FromArgb(j, j, j);
                Bitmap.Palette = pal;
            }

            // 发布图片给调用方
            //PublishImageEvent?.Invoke(this, e.FrameOut.Image.ToBitmap());
            PublishImageEvent?.Invoke(this, Bitmap);
        }

        /// <summary>
        /// 开始取流
        /// </summary>
        /// <returns></returns>
        public void StartGrabbing()
        {
            device.StreamGrabber.StartGrabbing();
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
        /// 停止取流
        /// </summary>
        /// <returns></returns>
        public void StopGrabbing()
        {
            device.StreamGrabber.StopGrabbing();
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
        }

        /// <summary>
        /// 释放相机资源
        /// </summary>
        public void Dispose()
        {
            _hikList.Remove(device);
            device?.Dispose();
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
