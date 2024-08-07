﻿using System;
using System.Collections.Generic;
using System.Linq;
using YTVisionPro.Hardware;
using YTVisionPro.Hardware.Camera;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro
{
    public class Solution
    {
        #region 使用 Lazy<T> 实现线程安全的单例模式

        private static readonly Lazy<Solution> _lazy = new Lazy<Solution>(() => new Solution());

        private Solution() { }

        public static Solution Instance => _lazy.Value;

        #endregion

        #region 私有字段、属性

        /// <summary>
        /// 方案包含的所有流程
        /// </summary>
        private List<Process> _allProcesses = new List<Process>();

        /// <summary>
        /// 方案设备（光源、相机、plc）
        /// </summary>
        private readonly object _deviceLock = new object();
        private List<IDevice> _devices = new List<IDevice>();

        #endregion

        #region 公有字段、属性

        #region 设备添加、删除、访问设备列表
        public void AddDevice(IDevice device)
        {
            lock (_deviceLock)
            {
                _devices.Add(device);
            }
        }

        public void AddDeviceList(List<IDevice> devices)
        {
            lock (_deviceLock)
            {
                foreach (var dev in devices)
                {
                    _devices.Add(dev);
                }
            }
        }
        public void RemoveDevice(IDevice device)
        {
            lock (_deviceLock)
            {
                _devices.Remove(device);
            }
        }
        /// <summary>
        /// 所有设备
        /// </summary>
        public List<IDevice> Devices => _devices;
        /// <summary>
        /// 光源设备
        /// </summary>
        public List<ILight> LightDevices => _devices.OfType<ILight>().ToList();

        public List<ICamera> CameraDevices => _devices.OfType<ICamera>().ToList();

        public List<IPlc> PlcDevices => _devices.OfType<IPlc>().ToList();

        #endregion


        /// <summary>
        /// 方案是否正在运行
        /// </summary>
        public bool IsRuning { get; set; } = false;

        /// <summary>
        /// 方案被修改
        /// </summary>
        public bool IsModify { get; set; } = false;

        /// <summary>
        /// 方案运行间隔时间(ms)
        /// </summary>
        public int Interval { get; set; } = 100;

        /// <summary>
        /// 方案文件名
        /// </summary>
        public string SolFileName { get; set; }

        /// <summary>
        /// 方案适配的软件版本
        /// </summary>
        public string SolVersion { get; set; } = VersionInfo.VersionInfo.GetExeVer();

        #endregion

        #region 私有方法



        #endregion

        #region 公有方法

        /// <summary>
        /// 添加流程
        /// </summary>
        /// <param name="process"></param>
        public void AddProcess(Process process)
        {
            _allProcesses.Add(process);
        }

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="process"></param>
        public void RemoveProcess(Process process)
        {
            _allProcesses.Remove(process);
        }

        /// <summary>
        /// 方案执行
        /// </summary>
        public void Run()
        {

        }

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <returns></returns>
        public ErrorCode Load()
        {

            return ErrorCode.LOAD_SOL_FAIL;
        }

        /// <summary>
        /// 方案保存
        /// </summary>
        public void Save()
        {

        }

        #endregion
    }
}
