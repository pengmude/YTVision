using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Basler.Pylon;
using JsonSubTypes;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Logger;

namespace YTVisionPro.Device.Camera
{
    internal class CameraBasler : ICamera
    {
        /// <summary>
        /// 单个Basler相机对象
        /// </summary>
        private Basler.Pylon.Camera _camera;

        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        public event EventHandler<bool> ConnectStatusEvent;

        /// <summary>
        /// 抓取图片事件
        /// </summary>
        public event EventHandler<Bitmap> PublishImageEvent;

        /// <summary>
        /// 相机是否打开
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceBrand Brand { get; set; } = DeviceBrand.Basler;

        /// <summary>
        /// 设备类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DevType DevType { get; set; } = DevType.CAMERA;
        public string ClassName { get; set; } = typeof(CameraBasler).FullName;

        /// <summary>
        /// 设备SN
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 用户定义名称
        /// </summary>
        public string UserDefinedName { get; set; }


        #region 反序列化专用函数

        /// <summary>
        /// 指定反序列化使用的构造函数
        /// </summary>
        [JsonConstructor]
        public CameraBasler() { }

        /// <summary>
        /// 先调用构造函数再调用创建设备函数
        /// </summary>
        public void CreateDevice()
        {
            foreach (var cameraInfo in CameraFinder.Enumerate())
            {
                if(cameraInfo[CameraInfoKey.SerialNumber] == SN)
                {
                    _camera = new Basler.Pylon.Camera(cameraInfo);
                    return;
                }
            }
        }

        #endregion

        /// <summary>
        /// 重载构造函数
        /// </summary>
        public CameraBasler(ICameraInfo info, string userName)
        {
            _camera = new Basler.Pylon.Camera(info);
            SN = info[CameraInfoKey.SerialNumber];
            DevName = info[CameraInfoKey.ModelName];
            UserDefinedName = userName;
        }

        /// <summary>
        /// 查找相机
        /// </summary>
        /// <returns></returns>
        public static List<ICameraInfo> FindCamera()
        {
            return CameraFinder.Enumerate();
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            if (_camera == null)
            {
                LogHelper.AddLog(MsgLevel.Exception, "相机未初始化，无法启动", true);
                throw new InvalidOperationException("相机未初始化");
            }
            try
            {
                // 打开相机事件绑定
                _camera.Open();

                // 相机取流事件绑定
                _camera.StreamGrabber.ImageGrabbed -= OnImageGrabbed;
                _camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            IsOpen = true;
            ConnectStatusEvent?.Invoke(this, true);
            return true;
        }

        /// <summary>
        /// 设置触发模式
        /// </summary>
        /// <param name="triggerMode"></param>
        public void SetTriggerMode(bool triggerMode)
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                if (triggerMode)
                {
                    _camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
                }
                else
                {
                    _camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.Off);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置触发源
        /// </summary>
        /// <param name="triggerSource"></param>
        public void SetTriggerSource(TriggerSource triggerSource)
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                switch (triggerSource)
                {

                    case TriggerSource.SOFT:
                        _camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Software);
                        break;
                    case TriggerSource.LINE1:
                        _camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line1);
                        break;
                    case TriggerSource.LINE2:
                        _camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line2);
                        break;
                    case TriggerSource.LINE3:
                        _camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line3);
                        break;
                    case TriggerSource.LINE4:
                        _camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line4);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置硬件触发时的触发沿
        /// </summary>
        /// <param name="triggerEdge"></param>
        /// <returns></returns>
        public void SetTriggerEdge(TriggerEdge triggerEdge)
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {

                switch (triggerEdge)
                {
                    case TriggerEdge.Rising:
                        _camera.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.RisingEdge);
                        break;
                    case TriggerEdge.Falling:
                        _camera.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.FallingEdge);
                        break;
                    case TriggerEdge.Any:
                        _camera.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.AnyEdge);
                        break;
                    case TriggerEdge.Hight:
                        _camera.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.LevelHigh);
                        break;
                    case TriggerEdge.Low:
                        _camera.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.LevelLow);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 图像处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            uint nChannelNum = 0;
            PixelFormat m_bitmapPixelFormat = PixelFormat.Undefined;
            PixelType enType = PixelType.Undefined;
            IGrabResult grabResult = e.GrabResult;

            if (grabResult.GrabSucceeded)
            {   // 判断是否为彩色图片
                if (IsColorPixelFormat(grabResult.PixelTypeValue))
                {
                    enType = PixelType.RGB8packed;
                    m_bitmapPixelFormat = PixelFormat.Format24bppRgb;
                    nChannelNum = 3;
                } // 判断是否为黑白图片
                else if (IsMonoPixelFormat(grabResult.PixelTypeValue))
                {
                    enType = PixelType.Mono8;
                    m_bitmapPixelFormat = PixelFormat.Format8bppIndexed;
                    nChannelNum = 1;
                }
                else
                {
                    return;
                }

                // 创建位图
                Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, m_bitmapPixelFormat);

                // 锁定位图的位，以便将图像数据直接写入内存
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                        ImageLockMode.ReadWrite, bitmap.PixelFormat);
                // 拷贝图像数据到位图
                if (nChannelNum == 1)
                {
                    // 单通道图像（灰度图像）
                    byte[] pixelData = grabResult.PixelData as byte[];
                    Marshal.Copy(pixelData, 0, bitmapData.Scan0, pixelData.Length);

                    // 设置调色板，使其显示为灰度图
                    var pal = bitmap.Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        pal.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    bitmap.Palette = pal;
                }
                else if (nChannelNum == 3)
                {
                    // 多通道图像（RGB图像）
                    byte[] pixelData = grabResult.PixelData as byte[];
                    Marshal.Copy(pixelData, 0, bitmapData.Scan0, pixelData.Length);
                }

                // 解锁位图
                bitmap.UnlockBits(bitmapData);

                // 发布图片给调用方
                PublishImageEvent?.Invoke(this, bitmap);
            }
        }

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        public void GrabOne()
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                _camera.ExecuteSoftwareTrigger();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 开始取流
        /// </summary>
        public void StartGrabbing()
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                if (!_camera.StreamGrabber.IsGrabbing)
                {
                    // 抓取每一帧图片处理之后继续抓取下一帧，抓取图片循环方式由相机内部自动管理
                    _camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭取流
        /// </summary>
        public void StopGrabbing()
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                _camera.StreamGrabber.Stop();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取相机取流状态
        /// </summary>
        /// <returns></returns>
        public bool GetGrabStatus()
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                return _camera.StreamGrabber.IsGrabbing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取曝光
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public float GetExposureTime()
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                return (float)_camera.Parameters[PLCamera.ExposureTimeAbs].GetValue();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取增益
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public float GetGain()
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {

                return (float)_camera.Parameters[PLCamera.GainRaw].GetValue();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取延迟触发时间
        /// </summary>
        /// <returns></returns>
        public float GetTriggerDelay()
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                return (float)_camera.Parameters[PLCamera.TriggerDelayAbs].GetValue();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="exposureTime"></param>
        public void SetExposureTime(float time)
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                _camera.Parameters[PLCamera.ExposureTimeAbs].SetValue(time);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gain"></param>
        public void SetGain(float gainValue)
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                _camera.Parameters[PLCamera.GainRaw].SetValue((long)gainValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置延迟触发
        /// </summary>
        /// <param name="time">单位：微秒</param>
        public void SetTriggerDelay(float time)
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                _camera.Parameters[PLCamera.TriggerDelayAbs].SetValue(time);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                _camera.StreamGrabber.Stop();
                _camera.Close();
                IsOpen = false;
                ConnectStatusEvent?.Invoke(this, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 释放相机资源
        /// </summary>
        public void Dispose()
        {
            if (_camera == null) throw new Exception("相机对象为空！");
            try
            {
                if (_camera.IsOpen) { _camera.Close(); }
                _camera?.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断是否为彩色图像
        /// </summary>
        /// <param name="enType"></param>
        /// <returns></returns>
        private bool IsColorPixelFormat(PixelType enType)
        {
            switch (enType)
            {
                case PixelType.RGB8packed:
                case PixelType.BGR8packed:
                case PixelType.RGBA8packed:
                case PixelType.BGRA8packed:
                case PixelType.YUV422packed:
                case PixelType.YUV422_YUYV_Packed:
                case PixelType.BayerGR8:
                case PixelType.BayerRG8:
                case PixelType.BayerGB8:
                case PixelType.BayerBG8:
                case PixelType.BayerGB10:
                case PixelType.BayerBG10:
                case PixelType.BayerRG10:
                case PixelType.BayerGR10:
                case PixelType.BayerGB12:
                case PixelType.BayerGB12Packed:
                case PixelType.BayerBG12:
                case PixelType.BayerBG12Packed:
                case PixelType.BayerRG12:
                case PixelType.BayerRG12Packed:
                case PixelType.BayerGR12:
                case PixelType.BayerGR12Packed:
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
        private bool IsMonoPixelFormat(PixelType enType)
        {
            switch (enType)
            {
                case PixelType.Mono8:
                case PixelType.Mono10:
                case PixelType.Mono10p:
                case PixelType.Mono12:
                case PixelType.Mono12p:
                    return true;
                default:
                    return false;
            }
        }

    }
}
