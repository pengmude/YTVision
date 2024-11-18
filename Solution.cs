using Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Forms.CameraAdd;
using YTVisionPro.Forms.LightAdd;
using YTVisionPro.Forms.ModbusAdd;
using YTVisionPro.Forms.PLCAdd;
using YTVisionPro.Device;
using YTVisionPro.Device.Camera;
using YTVisionPro.Device.Light;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Device.PLC;
using YTVisionPro.Node;
using YTVisionPro.Device.TCP;
using YTVisionPro.Forms.TCPAdd;
using YTVisionPro.Node._3_Detection.HTAI;

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
        /// Modbus设备
        /// </summary>
        public List<IModbus> ModbusDevices => AllDevices.OfType<IModbus>().ToList();
        /// <summary>
        /// TCP设备
        /// </summary>
        public List<ITcpDevice> TcpDevices => AllDevices.OfType<ITcpDevice>().ToList();

        /// <summary>
        /// 方案是否正在运行
        /// </summary>
        public bool IsRunning { get; set; } = false;
        
        /// <summary>
        /// 方案总耗时
        /// </summary>
        public long RunTime {  get; set; } = -1;

        /// <summary>
        /// 流程运行间隔
        /// </summary>
        public int RunInterval { get; set; } = 0;

        /// <summary>
        /// 方案被修改
        /// </summary>
        public bool IsModify { get; set; } = false;

        /// <summary>
        /// 方案文件名
        /// </summary>
        public string SolFileName { get; set; }
        
        /// <summary>
        /// 方案AI模型数量,用来实现异步加载完对应数量的模型后通知方案全部加载完成
        /// </summary>
        public int SolAiModelNum { get; set; }
        
        /// <summary>
        /// 当前方案已加载的模型数量
        /// </summary>
        public int LoadedModelNum {  get; set; }

        /// <summary>
        /// 方案适配的软件版本
        /// </summary>
        public string SolVersion { get; set; } = VersionInfo.VersionInfo.GetExeVer();

        /// <summary>
        /// 方案运行取消令牌，嵌入到流程和节点中，实现对它们的控制
        /// </summary>
        public CancellationToken CancellationToken => _cancellationTokenSource.Token;

        /// <summary>
        /// 流程运行完更新流程界面和主界面的运行按钮Enable
        /// </summary>
        public EventHandler<ProcessRunResult> UpdateRunStatus;

        /// <summary>
        /// 新建方案、加载方案要清除结果窗口数据
        /// </summary>
        public EventHandler RemoveResultData;

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
            IsRunning = true;

            // 重置运行取消令牌
            Solution.Instance.ResetTokenSource();
            try
            {
                // 启动所有流程
                var tasks = new List<Task>();
                foreach (var process in AllProcesses)
                {
                    tasks.Add(process.Run(isCyclical));
                }
                // 等待所有流程完成
                await Task.WhenAll(tasks);
            }
            catch (Exception ex) { }

            IsRunning = false;
        }

        /// <summary>
        /// 方案运行停止
        /// </summary>
        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            IsRunning = false;
        }

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <returns></returns>
        public void Load(string configFile, bool flag)
        {
            try
            {
                if (File.Exists(configFile))
                {
                    ConfigHelper.SolLoad(configFile, flag);
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

        /// <summary>
        /// 重置方案
        /// </summary>
        public void SolReset()
        {
            try
            {
                // 释放旧方案的硬件资源（光源、相机、PLC、Modbus、TCP）
                foreach (var dev in Solution.Instance.AllDevices)
                {
                    if (dev is ILight light)
                        light.Disconnect();
                    if (dev is ICamera camera)
                        camera.Dispose();
                    if (dev is IPlc plc)
                        plc.Disconnect();
                    if (dev is IModbus modbus)
                        modbus.Disconnect();
                    if (dev is ITcpDevice tcpDev)
                        tcpDev.Disconnect();
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);
            }

            // 清空AI模型计数
            Solution.Instance.SolAiModelNum = 0;
            Solution.Instance.LoadedModelNum = 0;

            // 清空设备
            SingleLight.SingleLights.Clear();
            SingleCamera.SingleCameraList.Clear();
            SinglePLC.SinglePLCs.Clear();
            SingleModbus.SingleModbuss.Clear();
            SingleTcp.SingleTCPs.Clear();
            Solution.Instance.AllDevices.Clear();

            // 释放AI节点的内存
            foreach (var node in Solution.Instance.Nodes)
            {
                if (node is NodeHTAI nodeAi)
                {
                    nodeAi.ReleaseAIResult();
                    ((ParamFormHTAI)nodeAi.ParamForm).ReleaseAIHandle();
                }
            }

            // 清空流程和节点
            Solution.Instance.ProcessCount = 0;
            Solution.Instance.AllProcesses.Clear();
            Solution.Instance.NodeCount = 0;
            Solution.Instance.Nodes.Clear();

            // 发送清除结果窗口事件
            RemoveResultData?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
