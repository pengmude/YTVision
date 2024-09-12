using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Hardware;
using YTVisionPro.Hardware.Camera;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Node;

namespace YTVisionPro
{
    internal class Solution
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

        /// <summary>
        /// 方案运行取消源，通过它控制方案的停止
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        #endregion

        #region 公有字段、属性

        /// <summary>
        /// 方案添加的设备总数
        /// </summary>
        public static int DeviceCount = 0;

        /// <summary>
        /// 方案已经添加的流程总数
        /// </summary>
        public static int ProcessCount = 0;

        /// <summary>
        /// 方案节点统计（包含删除的节点）
        /// </summary>
        public static int NodeCount = 0;

        /// <summary>
        /// 方案已经添加的节点
        /// </summary>
        public static List<NodeBase> Nodes = new List<NodeBase>();

        /// <summary>
        /// 所有设备
        /// </summary>
        public List<IDevice> Devices => _devices;

        /// <summary>
        /// 光源设备
        /// </summary>
        public List<ILight> LightDevices => _devices.OfType<ILight>().ToList();

        /// <summary>
        /// 相机设备
        /// </summary>
        public List<ICamera> CameraDevices => _devices.OfType<ICamera>().ToList();

        /// <summary>
        /// Plc设备
        /// </summary>
        public List<IPlc> PlcDevices => _devices.OfType<IPlc>().ToList();

        /// <summary>
        /// 方案是否正在运行
        /// </summary>
        public bool IsRunning { get; set; } = false;

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

        /// <summary>
        /// 方案运行取消令牌，嵌入到流程和节点中，实现对它们的控制
        /// </summary>
        public CancellationToken CancellationToken => _cancellationTokenSource.Token;

        #endregion

        #region 私有方法



        #endregion

        #region 公有方法

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

        #endregion


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

            foreach (var node in process.Nodes)
            {
                Solution.Nodes.Remove(node);
            }
        }

        /// <summary>
        /// 根据流程名称删除流程
        /// </summary>
        /// <param name="process"></param>
        public void RemoveProcess(string processName)
        {
            // 查找名称匹配的流程
            Process processToRemove = _allProcesses.Find(p => p.ProcessName == processName);

            if (processToRemove != null)
            {
                // 从列表中移除流程
                _allProcesses.Remove(processToRemove);

                foreach (var node in processToRemove.Nodes)
                {
                    Solution.Nodes.Remove(node);
                }
            }
        }

        /// <summary>
        /// 获取所有流程名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllProcessName()
        {
            List<string> result = new List<string>();
            foreach (var process in _allProcesses)
            {
                result.Add(process.ProcessName);
            }
            return result;
        }

        public async Task Run(bool isCyclical = false)
        {
            try
            {
                IsRunning = true;
                _cancellationTokenSource = new CancellationTokenSource();

                // 启动所有流程
                var tasks = new List<Task>();
                foreach (var process in _allProcesses)
                {
                    tasks.Add(process.Run(isCyclical, _cancellationTokenSource.Token));
                }
                // 等待所有流程完成
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException ex) { }
            catch (Exception ex) { }

            IsRunning = false;
        }

        /// <summary>
        /// 方案运行停止
        /// </summary>
        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
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
