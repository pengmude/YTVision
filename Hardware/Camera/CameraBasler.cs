using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basler.Pylon;
using Logger;

namespace YTVisionPro.Hardware.Camera
{
    public class CameraBasler : ICamera
    {
        /// <summary>
        /// 单个Basler相机对象
        /// </summary>
        private  Basler.Pylon.Camera _camera;

        /// <summary>
        /// 抓取图片事件
        /// </summary>
        public event EventHandler<Bitmap> CameraGrabEvent;

        /// <summary>
        /// 相机是否打开
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public CameraBrand Brand { get; set ; } = CameraBrand.Camera;

        /// <summary>
        /// 设备类型
        /// </summary>
        public DevType DevType { get; } = DevType.CAMERA;

        /// <summary>
        /// 设备ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set ; }

        /// <summary>
        /// 用户定义名称
        /// </summary>
        public string UserDefinedName { get ; set ; }

        /// <summary>
        /// 取流标记
        /// </summary>
        private bool OnGrabbing = false;

        /// <summary>
        /// 图片传输事件
        /// </summary>
        public event EventHandler<Bitmap> sendImage;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CameraBasler()
        {

        }
        /// <summary>
        /// 重载构造函数
        /// </summary>
        public CameraBasler(ICameraInfo info)
        {
            _camera = new Basler.Pylon.Camera(info);
        }

        /// <summary>
        /// 查找相机
        /// </summary>
        /// <returns></returns>
        public static List<ICameraInfo> FindCamera()
        {
            List<ICameraInfo> CameraInfoLists = new List<ICameraInfo>();
            // 枚举相机
            CameraInfoLists = CameraFinder.Enumerate();

            return CameraInfoLists;
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                // 打开相机事件绑定
                _camera.CameraOpened += Configuration.AcquireContinuous;
                _camera.Open();

                // 相机取流事件绑定
                _camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                IsOpen = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置触发模式
        /// </summary>
        /// <param name="triggerMode"></param>
        public void SetTriggerMode(bool triggerMode)
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

        /// <summary>
        /// 设置触发源
        /// </summary>
        /// <param name="triggerSource"></param>
        public bool SetTriggerSource(TriggerSource triggerSource)
        {
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
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
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
                CameraGrabEvent?.Invoke(this, bitmap);
            }
        }

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        public bool GrabOne()
        {
            try
            {
                _camera.ExecuteSoftwareTrigger();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 开始取流
        /// </summary>
        public bool StartGrabbing()
        {
            try
            {
                // 抓取每一帧图片处理之后继续抓取下一帧，抓取图片循环方式由相机内部自动管理
                _camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                OnGrabbing = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 关闭取流
        /// </summary>
        public bool StopGrabbing()
        {
            try
            {
                _camera.StreamGrabber.Stop();
                OnGrabbing = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="exposureTime"></param>
        public void SetExposureTime(string time)
        {
            try
            {
                _camera.Parameters[PLCamera.ExposureTimeAbs].SetValue(int.Parse(time));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gain"></param>
        public void SetGain(string gainValue)
        {
            try
            {
                _camera.Parameters[PLCamera.GainRaw].SetValue(int.Parse(gainValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                if (OnGrabbing)
                {
                    StopGrabbing();
                }
                _camera.Close();
                IsOpen = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
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
