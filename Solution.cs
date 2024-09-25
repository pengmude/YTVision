//using MvCameraControl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private Solution() 
        {
            AllDevices = new List<IDevice>();
        }

        public static Solution Instance => _lazy.Value;

        #endregion

        #region 私有字段、属性

        /// <summary>
        /// 方案运行取消源，通过它控制方案的停止
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        #endregion

        #region 公有字段、属性

        /// <summary>
        /// 方案包含的所有流程
        /// </summary>
        public List<Process> AllProcesses = new List<Process>();

        /// <summary>
        /// 方案添加的设备总数
        /// </summary>
        public int DeviceCount => AllDevices.Count;

        /// <summary>
        /// 方案已经添加的流程总数
        /// </summary>
        public int ProcessCount = 0;

        /// <summary>
        /// 方案节点统计（包含删除的节点）
        /// </summary>
        public int NodeCount = 0;

        /// <summary>
        /// 方案已经添加的节点
        /// </summary>
        public List<NodeBase> Nodes = new List<NodeBase>();

        /// <summary>
        /// 所有设备
        /// </summary>
        public List<IDevice> AllDevices { get; set; }
        /// <summary>
        /// 光源设备
        /// </summary>
        public List<ILight> LightDevices => AllDevices.OfType<ILight>().ToList();

        /// <summary>
        /// 相机设备
        /// </summary>
        public List<ICamera> CameraDevices => AllDevices.OfType<ICamera>().ToList();

        /// <summary>
        /// Plc设备
        /// </summary>
        public List<IPlc> PlcDevices => AllDevices.OfType<IPlc>().ToList();

        /// <summary>
        /// 方案是否正在运行
        /// </summary>
        public bool IsRunning { get; set; } = false;

        /// <summary>
        /// 方案被修改
        /// </summary>
        public bool IsModify { get; set; } = false;

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

        #region 流程相关操作

        /// <summary>
        /// 添加流程
        /// </summary>
        /// <param name="process"></param>
        public void AddProcess(Process process)
        {
            AllProcesses.Add(process);
        }

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="process"></param>
        public void RemoveProcess(Process process)
        {
            AllProcesses.Remove(process);

            foreach (var node in process.Nodes)
            {
                Solution.Instance.Nodes.Remove(node);
            }
        }

        /// <summary>
        /// 根据流程名称删除流程
        /// </summary>
        /// <param name="process"></param>
        public void RemoveProcess(string processName)
        {
            // 查找名称匹配的流程
            Process processToRemove = AllProcesses.Find(p => p.ProcessName == processName);

            if (processToRemove != null)
            {
                // 从列表中移除流程
                AllProcesses.Remove(processToRemove);

                foreach (var node in processToRemove.Nodes)
                {
                    Solution.Instance.Nodes.Remove(node);
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
            foreach (var process in AllProcesses)
            {
                result.Add(process.ProcessName);
            }
            return result;
        }

        #endregion

        #region 方案运行/停止、配置加载/保存等相关操作

        /// <summary>
        /// 方案运行
        /// </summary>
        /// <param name="isCyclical">是否循环运行</param>
        /// <returns></returns>
        public async Task Run(bool isCyclical = false)
        {
            try
            {
                IsRunning = true;
                _cancellationTokenSource = new CancellationTokenSource();

                // 启动所有流程
                var tasks = new List<Task>();
                foreach (var process in AllProcesses)
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
        }

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <returns></returns>
        public void Load(string configFile)
        {
            try
            {
                if (File.Exists(configFile))
                {
                    ConfigHelper.SolLoad(configFile);
                }
                else
                {
                    throw new Exception("方案文件不存在！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"反序列化异常!原因：{ex.Message}");
            }
        }

        /// <summary>
        /// 方案保存
        /// </summary>
        public void Save(string solFile)
        {
            try
            {
                ConfigHelper.SolSave(solFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 控制停止运行的取消令牌

        /// <summary>
        /// 重置令牌源
        /// </summary>
        public void ResetTokenSource()
        {
            if(_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource = new CancellationTokenSource();
        }

        #endregion

        #endregion
    }
}
