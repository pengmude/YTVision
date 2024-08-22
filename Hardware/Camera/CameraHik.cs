using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Logger;
using MvCameraControl;
using Sunny.UI.Win32;

namespace YTVisionPro.Hardware.Camera
{
    /// <summary>
    /// 海康相机类
    /// </summary>
    internal class CameraHik : ICamera
    {
        /// <summary>
        /// 保存已创建的海康相机对象，便于相机对象数量为0时，自动调用SDK反初始化
        /// </summary>
        private List<MvCameraControl.IDevice> _hikList = new List<MvCameraControl.IDevice>();

        /// <summary>
        /// SDK初始化标记
        /// </summary>
        private static int _initSDKFlag = MvError.MV_E_RESOURCE;

        /// <summary>
        /// 抓取图片事件
        /// </summary>
        public event EventHandler<Bitmap> CameraGrabEvent;

        /// <summary>
        /// 单个海康相机信息
        /// </summary>
        private MvCameraControl.IDevice device = null;

        /// <summary>
        /// 取流标记
        /// </summary>
        private bool OnGrabbing = false;

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
        public CameraHik(IDeviceInfo devInfo)
        {
            try
            {
                InitSDK();
                device = DeviceFactory.CreateDevice(devInfo);
                DevInfo = devInfo;
                _hikList.Add(device); 
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Fatal, ex.Message, true);
                return;
            }
            finally
            {
                if (device != null)
                {
                    Dispose();
                }
            }
        }

        /// <summary>
        /// 初始化SDK
        /// </summary>
        private static void InitSDK()
        {
            if(_initSDKFlag != MvError.MV_OK)
                SDKSystem.Initialize();
        }

        /// <summary>
        /// 反初始化SDK
        /// </summary>
        private static void Finalize()
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
            bool flag = false;
            //循环执行10次，用于连接相机
            for (int i = 0; i < 10; i++)
            {
                if (MvError.MV_OK != nRet)
                {
                    Thread.Sleep(1);
                    continue;
                }

                nRet = device.Open();

                if (MyCamera.MV_OK != nRet)
                {
                    device.Dispose();
                    Thread.Sleep(5);
                    continue;
                }
                else
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
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
                    Console.WriteLine("Warning: Get Packet Size failed!", nRet);
                }
                else
                {
                    nRet = device.Parameters.SetIntValue("GevSCPSPacketSize", (long)optionPacketSize);
                    if (nRet != MvError.MV_OK)
                    {
                        Console.WriteLine("Warning: Set Packet Size failed!", nRet);
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
            // 发布图片给调用方
            CameraGrabEvent?.Invoke(this, e.FrameOut.Image.ToBitmap());
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
            if (_hikList.Count == 0)
            {
                Finalize();
                _initSDKFlag = MvError.MV_E_RESOURCE;
            }
        }
    }
}
