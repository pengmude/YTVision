using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Timers;
using YTVisionPro.Forms.LightAdd;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace YTVisionPro.Hardware.Light
{
    /// <summary>
    /// 串口号对应的锐视句柄
    /// </summary>
    internal class ComHandle
    {
        public string ComPort;
        public int Handle;
        public ComHandle(string comPort, int handle)
        {
            ComPort = comPort;
            Handle = handle;
        }
    }

    /// <summary>
    /// 锐视光源型号
    /// </summary>
    public enum RseeDeviceType
    {
        PM_D,
        P_MDPS_24W75
    }

    /// <summary>
    /// 锐视光源控制类
    /// </summary>
    internal class LightRsee : ILight
    {
        /// <summary>
        /// 光源设置参数
        /// </summary>
        public LightParam LightParam { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        private int _devId;

        /// <summary>
        /// 光源品牌-锐视
        /// </summary>
        public LightBrand Brand { get; } = LightBrand.RSEE;

        /// <summary>
        /// 光源亮度值
        /// </summary>
        public int Brightness { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int ID { get => _devId; }

        /// <summary>
        /// 硬件硬件名称
        /// </summary>
        public string DevName { get; }

        /// <summary>
        /// 用户自定义设备名
        /// </summary>
        public string UserDefinedName { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public DevType DevType { get; } = DevType.LIGHT;

        private RseeDeviceType RseeDeviceType { get; set; }

        /// <summary>
        /// 光源是否打开
        /// </summary>
        public bool IsOpen { get; private set; }

        /// <summary>
        /// 光源串口是否打开
        /// </summary>
        public bool IsComOpen { get; private set; }

        /// <summary>
        /// 串口通信句柄
        /// </summary>
        int ComHandle = 0;

        private static List<ComHandle> Com2HandleList = new List<ComHandle>();

        private Timer _timer;

        public LightRsee(LightParam lightParam)
        {
            _devId = ++Solution.DeviceCount;
            DevName = lightParam.LightName;
            UserDefinedName = DevName;
            LightParam = lightParam;
            try
            {
                foreach (var item in Com2HandleList)
                {
                    if (item.ComPort == lightParam.Port)
                    {
                        ComHandle = item.Handle;
                        break;
                    }
                }
                if (ComHandle == 0)
                    Connenct();
                else
                    IsComOpen = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            this.RseeDeviceType = lightParam.RseeDeviceType;
        }


        /// <summary>
        /// 连接光源串口
        /// </summary>
        public void Connenct()
        {
            if (IsComOpen) { return; }
            try
            {
                ComHandle = RseeController_OpenCom(LightParam.Port, LightParam.BaudRate, true);
                if (ComHandle == 0)
                {
                    IsComOpen = false;
                }
                else
                {
                    IsComOpen = true;
                    var item = new ComHandle(LightParam.Port, ComHandle);
                    Com2HandleList.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Disconnect()
        {
            RseeController_CloseCom(LightParam.Port, ComHandle);

            ComHandle = 0;
            IsComOpen = false;
            
            // 移除占用的串口
            ComHandle toRemove = null;
            foreach (var item in Com2HandleList)
            {
                if (item.ComPort == LightParam.Port)
                {
                    toRemove = item;
                    break;
                }
            }
            Com2HandleList.Remove(toRemove);
        }

        /// <summary>
        /// 打开光源
        /// </summary>
        public void TurnOn(int value, int time = -1)
        {
            try
            {
                if (!IsComOpen)
                    Connenct();
                SetValue(value);
                IsOpen = true;

                // -1为常亮

                if (time == -1)
                    return;
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer.Dispose();
                    _timer = null;
                }
                _timer = new Timer(time);
                _timer.Elapsed += OnTimedEvent;
                _timer.Start();
            }
            catch (Exception ex)
            {
                IsOpen = false;
                throw ex;
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            TurnOff();
        }

        /// <summary>
        /// 关闭光源
        /// </summary>
        public void TurnOff()
        {
            try
            {
                if (!IsComOpen)
                    Connenct();
                SetValue(0);
                IsOpen = false;
            }
            catch (Exception ex)
            {
                IsOpen = true;
                throw ex;
            }
        }

        /// <summary>
        /// 设置光源亮度
        /// </summary>
        /// <param name="value"></param>
        private void SetValue(int value)
        {
            if (this.RseeDeviceType == RseeDeviceType.PM_D)
            {
                if (IsComOpen)
                {
                    RseeController_PM_D_BRTSetChannel(ComHandle, LightParam.Channel, value);
                }
                else
                {
                    throw new Exception($"{LightParam.LightName}对应串口{LightParam.Port}尚未打开！");
                }
                Brightness = value;
                return;
            }
            if (this.RseeDeviceType == RseeDeviceType.P_MDPS_24W75)
            {
                if (IsComOpen)
                {
                    RseeController_MDPS_24W75_BRTSetChannel(ComHandle, LightParam.Channel, value);
                }
                else
                {
                    throw new Exception($"{LightParam.LightName}对应串口{LightParam.Port}尚未打开！");
                }
                Brightness = value;
            }
        }


        /// <summary>
        /// 连接光源串口
        /// </summary>
        /// <param name="com">串口号</param>
        /// <param name="baud">波特率</param>
        /// <param name="overloop">false：同步，true：异步</param>
        /// <returns>返回值：串口句柄</returns>
        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_OpenCom", CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_OpenCom(string com, int baud, bool overloop);

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="com">com - 串口号</param>
        /// <param name="com_handle">com_handle - 串口句柄</param>
        /// <returns>true - 关闭成功 false - 关闭失败</returns>
        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_CloseCom", CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_CloseCom(string com, int com_handle);

        /// <summary>
        /// 设置光源亮度(PM_D)
        /// </summary>
        /// <param name="com">com - 串口句柄</param>
        /// <param name="channel">channel - 通道序号，范围：1-6</param>
        /// <param name="range">range - 亮度等级，范围：0-999</param>
        /// <returns>
        ///  0 - 设置发送成功
        /// -1 - 串口未初始化
        /// -2 - 输入参数范围有误
        /// </returns>
        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_PM_D_BRTSetChannel", CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_PM_D_BRTSetChannel(int com, int channel, int range);

        /// <summary>
        /// 设置光源亮度(P_MDPS_24W75)
        /// </summary>
        /// <param name="com">com - 串口句柄</param>
        /// <param name="channel">channel - 通道序号，范围：1-4</param>
        /// <param name="range">range - 亮度等级，范围：0-999</param>
        /// <returns>
        ///  0 - 设置发送成功 
        /// -1 - 串口未初始化 
        /// -2 - 输入参数范围有误
        /// </returns>
        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_MDPS_24W75_BRTSetChannel", CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_MDPS_24W75_BRTSetChannel(int com, int channel, int range);
    }
}
