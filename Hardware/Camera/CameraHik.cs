using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI.Win32;
using Sunny.UI;
using Logger;
using Newtonsoft.Json.Linq;

namespace YTVisionPro.Hardware.Camera
{
    /// <summary>
    /// 海康相机类
    /// </summary>
    public class CameraHik : ICamera
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        MyCamera.cbOutputExdelegate ImageCallback;
        /// <summary>
        /// 抓取图片事件
        /// </summary>
        public event EventHandler<Bitmap> CameraGrabEvent;

        /// <summary>
        /// 单个海康相机信息
        /// </summary>
        public MyCamera.MV_CC_DEVICE_INFO DeviceInfo;

        /// <summary>
        /// 单个海康相机对象
        /// </summary>
        private MyCamera HKMyCamera = new MyCamera();

        /// <summary>
        /// 取流标记
        /// </summary>
        public bool OnGrabbing = false;

        // 类实例个数
        private static int _newCount = 0;
        // 设备id
        private int _devId;

        /// <summary>
        /// 设备ID
        /// </summary>
        public int ID { get => _devId; }

        public CameraHik() 
        {
            _devId = ++Solution.DeviceCount;
            UserDefinedName = $"海康相机{++_newCount}";
        }

        /// <summary>
        /// 相机曝光
        /// </summary>
        public int ExposureTime { get; set; }

        /// <summary>
        /// 相机增益
        /// </summary>
        public int Gain { get; set; }

        /// <summary>
        /// 相机名
        /// </summary>
        public string CameraName {  get; set; }

        /// <summary>
        /// 设备是否启用
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 硬件硬件名称
        /// </summary>
        public string DevName { get => _getDevName(); }

        /// <summary>
        /// 用户自定义设备名
        /// </summary>
        public string UserDefinedName { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public DevType DevType { get; } = DevType.CAMERA;

        /// <summary>
        /// 相机取流得到的一帧图像
        /// </summary>
        public Bitmap Bitmap { get; private set; }
       
        /// <summary>
        /// 获取设备名
        /// </summary>
        /// <returns></returns>
        private string _getDevName()
        {
            string camName = UserDefinedName;
            //网口相机
            if (DeviceInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(DeviceInfo.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                camName = gigeInfo.chModelName + "(" + gigeInfo.chSerialNumber + ")";

            }
            //usb接口相机
            else if (DeviceInfo.nTLayerType == MyCamera.MV_USB_DEVICE)
            {
                MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(DeviceInfo.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                camName = usbInfo.chModelName + "(" + usbInfo.chSerialNumber + ")";
            }
            return camName;
        }

        /// <summary>
        /// 查找相机
        /// </summary>
        /// <returns></returns>

        public static List<MyCamera.MV_CC_DEVICE_INFO> FindCamera()
        {
            int nRet;
            MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            List<MyCamera.MV_CC_DEVICE_INFO> mV_CC_DEVICE_INFOs = new List<MyCamera.MV_CC_DEVICE_INFO>();
            GC.Collect();
            nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);

            //获取相机名称 
            for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                mV_CC_DEVICE_INFOs.Add(device);
            }
            if (0 != nRet)
            {
                LogHelper.AddLog(MsgLevel.Exception, "获取相机列表失败", true);
            }
            if (m_pDeviceList.nDeviceNum == 0)
            {
                LogHelper.AddLog(MsgLevel.Info, "未找到USB或者网口相机", true);
            }
            LogHelper.AddLog(MsgLevel.Info, "相机枚举成功", true);
            return mV_CC_DEVICE_INFOs;
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
            // ch:打开设备 | en:Open device
            if (null == HKMyCamera)
            {
                HKMyCamera = new MyCamera();
                if (null == HKMyCamera)
                    throw new Exception("创建海康相机对象失败！");
            }
            //循环执行10次，用于连接相机
            for (int i = 0; i < 10; i++)
            {
                nRet = HKMyCamera.MV_CC_CreateDevice_NET(ref DeviceInfo);

                if (MyCamera.MV_OK != nRet)
                {
                    Thread.Sleep(1);
                    continue;
                }

                nRet = HKMyCamera.MV_CC_OpenDevice_NET();

                if (MyCamera.MV_OK != nRet)
                {
                    HKMyCamera.MV_CC_DestroyDevice_NET();
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
                throw new Exception("打开相机失败！");

            // ch:探测网络最佳包大小(只对GigE相机有效) 
            if (DeviceInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                int nPacketSize = HKMyCamera.MV_CC_GetOptimalPacketSize_NET();

                if (nPacketSize > 0)
                {
                    nRet = HKMyCamera.MV_CC_SetIntValueEx_NET("GevSCPSPacketSize", (uint)nPacketSize);

                    if (nRet != MyCamera.MV_OK)
                        throw new Exception("打开相机失败：设置网络最佳包大小失败！");
                }
                else
                    throw new Exception("打开相机失败：探测网络最佳包大小为0！");
            }

            //注册图像回调函数
            ImageCallback = new MyCamera.cbOutputExdelegate(Bit_Image);
            nRet = HKMyCamera.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, IntPtr.Zero);

            if (MyCamera.MV_OK != nRet)
                throw new Exception("打开相机失败：注册图像回调函数失败！");
            LogHelper.AddLog(MsgLevel.Info, $"打开相机{UserDefinedName}成功！", true);
            IsOpen = true;
            return true;
        }

        public void Bit_Image(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            int nRet;
            IntPtr pImageBuf = IntPtr.Zero;
            uint nChannelNum = 0;
            PixelFormat m_bitmapPixelFormat = PixelFormat.Undefined;
            MyCamera.MvGvspPixelType enType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;

            if (IsColorPixelFormat(pFrameInfo.enPixelType))
            {
                enType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
                m_bitmapPixelFormat = PixelFormat.Format24bppRgb;
                nChannelNum = 3;
            }
            else if (IsMonoPixelFormat(pFrameInfo.enPixelType))
            {
                enType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
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
                pImageBuf = Marshal.AllocHGlobal((int)(pFrameInfo.nWidth * pFrameInfo.nHeight * nChannelNum));
                if (IntPtr.Zero == pImageBuf)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"图像采集为空", true);
                    return;
                }
            }

            MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pData;//源数据
            stPixelConvertParam.nWidth = pFrameInfo.nWidth;//图像宽度
            stPixelConvertParam.nHeight = pFrameInfo.nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = pFrameInfo.enPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = pFrameInfo.nFrameLen;

            stPixelConvertParam.nDstBufferSize = (uint)(pFrameInfo.nWidth * pFrameInfo.nHeight * nChannelNum);
            stPixelConvertParam.pDstBuffer = pImageBuf;//转换后的数据
            stPixelConvertParam.enDstPixelType = enType;
            nRet = HKMyCamera.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MyCamera.MV_OK != nRet)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"图像转换异常", true);
                return;
            }

            Bitmap = new Bitmap((Int32)stPixelConvertParam.nWidth, (Int32)stPixelConvertParam.nHeight, m_bitmapPixelFormat);
            BitmapData bitmapData = Bitmap.LockBits(new Rectangle(0, 0, stPixelConvertParam.nWidth, stPixelConvertParam.nHeight), ImageLockMode.ReadWrite, Bitmap.PixelFormat);
            CopyMemory(bitmapData.Scan0, stPixelConvertParam.pDstBuffer, (UInt32)(bitmapData.Stride * Bitmap.Height));
            Bitmap.UnlockBits(bitmapData);

            //通过设置调色板从伪彩改为灰度
            if (nChannelNum == 1)
            {
                var pal = Bitmap.Palette;
                for (int j = 0; j < 256; j++)
                    pal.Entries[j] = System.Drawing.Color.FromArgb(j, j, j);
                Bitmap.Palette = pal;
            }
            //发布图片给调用方
            CameraGrabEvent?.Invoke(this, Bitmap);
        }

        /// <summary>
        /// 开始取流
        /// </summary>
        /// <returns></returns>
        public bool StartGrabbing()
        {
            // ch:开始采集 
            int nRet = HKMyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                throw new Exception($"相机{UserDefinedName}取流失败！");
            }
            LogHelper.AddLog(MsgLevel.Fatal, $"相机{UserDefinedName}取流成功！", true);
            OnGrabbing = true;
            return true;
        }

        /// <summary>
        /// 设置相机触发模式
        /// </summary>
        /// <param name="isTrigger"></param>
        public void SetTriggerMode(bool isTrigger)
        {
            int ret;
            if (isTrigger)
            {
                ret = HKMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                if (ret != MyCamera.MV_OK)
                    throw new Exception("打开海康相机触发模式失败！");
            }
            else
            {
                ret = HKMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
                if (ret != MyCamera.MV_OK)
                    throw new Exception("关闭海康相机触发模式失败！");
            }
        }

        /// <summary>
        /// 设置软硬触发
        /// </summary>
        /// <param name="trigBySoft"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool SetTriggerSource(TriggerSource triggerSource)
        {
            int nRet = MyCamera.MV_E_UNKNOW;
            try
            {

                switch (triggerSource)
                {
                    case TriggerSource.SOFT:
                        nRet = HKMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                        break;
                    case TriggerSource.LINE0:
                        nRet = HKMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
                        break;
                    case TriggerSource.LINE1:
                        nRet = HKMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE1);
                        break;
                    case TriggerSource.LINE2:
                        nRet = HKMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE2);
                        break;
                    case TriggerSource.LINE3:
                        nRet = HKMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE3);
                        break;
                    default:
                        break;
                }
                if (MyCamera.MV_OK != nRet) 
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        public bool GrabOne()
        {
            int nRet;
            //使用软触发命令
            nRet = HKMyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");

            if (MyCamera.MV_OK != nRet)
                return false;
            return true;
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gainValue"></param>
        public void SetGain(int gainValue)
        {
            HKMyCamera.MV_CC_SetEnumValue_NET("GainAuto", 0);
            int nRet = HKMyCamera.MV_CC_SetFloatValue_NET("Gain", gainValue);
            if (nRet != MyCamera.MV_OK)
                throw new Exception($"{UserDefinedName}增益设置失败");
            else
                LogHelper.AddLog(MsgLevel.Info, $"{UserDefinedName}增益设置成功", true); ;
        }

        /// <summary>
        /// 设置曝光 
        /// </summary>
        /// <param name="time"></param>
        public void SetExposureTime(int time)
        {
            HKMyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
            int nRet = HKMyCamera.MV_CC_SetFloatValue_NET("ExposureTime", time);
            if (nRet != MyCamera.MV_OK)
                throw new Exception($"{UserDefinedName}曝光设置失败");
            else
                LogHelper.AddLog(MsgLevel.Info, $"{UserDefinedName}曝光设置成功", true);
        }

        /// <summary>
        /// 停止取流
        /// </summary>
        /// <returns></returns>
        public bool StopGrabbing()
        {
            int nRet = HKMyCamera.MV_CC_StopGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
                return false;
            OnGrabbing = false;
            return true;
        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            //先停止取流
            if (OnGrabbing)
                StopGrabbing();
            // 关闭设备
            int nRet = HKMyCamera.MV_CC_CloseDevice_NET();
            if (nRet != MyCamera.MV_OK)
                return false;
            IsOpen = false;
            return true;
        }

        /// <summary>
        /// 销毁相机
        /// </summary>
        /// <returns></returns>
        public bool Destroy()
        {
            int nRet = HKMyCamera.MV_CC_DestroyDevice_NET();
            if (nRet != MyCamera.MV_OK)
            {
                return false;
            }
            return true;
        }
       
        /// <summary>
        /// 判断是否为彩色图像
        /// </summary>
        /// <param name="enType"></param>
        /// <returns></returns>
        private bool IsColorPixelFormat(MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGBA8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGRA8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
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
        private bool IsMonoPixelFormat(MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;
                default:
                    return false;
            }
        }

    }
}
