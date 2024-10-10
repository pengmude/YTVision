using Logger;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using YTVisionPro.Hardware.Camera;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Node;

namespace YTVisionPro
{
    /// <summary>
    /// 检测流程类
    /// 一个方案可以拥有多个流程
    /// 每个流程也可以单独执行
    /// </summary>
    internal class Process
    {
        /// <summary>
        /// 流程包含的节点
        /// </summary>
        private List<NodeBase> _nodes = new List<NodeBase>();
        /// <summary>
        /// 流程名称
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 流程包含的节点
        /// </summary>
        public List<NodeBase> Nodes { get => _nodes;}

        /// <summary>
        /// 流程是否正在运行
        /// </summary>
        public bool IsRuning { get; set; } = false;

        /// <summary>
        /// 流程运行时间
        /// </summary>
        public long RunTime { get; private set; } = 0;

        /// <summary>
        /// 流程运行是否成功
        /// </summary>
        public bool Success { get; private set; }
        /// <summary>
        /// 流程是否启用
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="solution"></param>
        public Process(string processName)
        {
            ProcessName = processName;
        }

        public void AddNode(NodeBase node)
        {
            _nodes.Add(node);
        }

        /// <summary>
        /// 流程开始运行
        /// </summary>
        public async Task Run(bool isCyclical, CancellationToken token = default(CancellationToken))
        {
            if (Nodes.Count == 0)
                return;

            do
            {
                RunTime = 0;
                if (Enable)
                {
                    LogHelper.AddLog(MsgLevel.Info, $"-----------------------------------------------------  【{ProcessName}】（开始）  -----------------------------------------------------", true);
                    IsRuning = true;
                    Success = false;
                    foreach (var node in _nodes)
                    {
                        try
                        {
                            await node.Run(token);
                            RunTime += node.Result.RunTime;
                        }
                        catch (OperationCanceledException ex)
                        {
                            Success = false;
                            IsRuning = false;
                            RunTime += node.Result.RunTime;
                            LogHelper.AddLog(MsgLevel.Warn, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（运行中断）  ---------------------------------", true);
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            IsRuning = false;
                            RunTime += node.Result.RunTime;
                            LogHelper.AddLog(MsgLevel.Exception, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（失败）  ---------------------------------", true);
                            throw ex;
                        }
                    }

                    Success = true;
                    IsRuning = false;
                    LogHelper.AddLog(MsgLevel.Info, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（成功）  ---------------------------------", true);

                }
                if (isCyclical)
                {
                    await Task.Delay(Solution.Instance.RunInterval, token);
                }
            } while (isCyclical && !token.IsCancellationRequested);
        }
    }
}
