using HslCommunication;
using Logger;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Node.PLC.WaitSoftTrigger
{
    internal class NodeWaitSoftTrigger : NodeBase
    {
        public NodeWaitSoftTrigger(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormWaitSoftTrigger();
            Result = new NodeResultWaitSoftTrigger();
        }


        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task Run(CancellationToken token)
        {
            DateTime startTime = DateTime.Now;

            if (!Active)
            {
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return;
            }
            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({NodeName})运行参数未设置或保存！");
            }

            if (ParamForm is ParamFormWaitSoftTrigger form)
            {
                if (form.Params is NodeParamWaitSoftTrigger param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        await Task.Run(() =>
                        {
                            // 监听拍照信号
                            SendSignalToPlc(param);
                        });

                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
                    }
                    catch (OperationCanceledException)
                    {
                        LogHelper.AddLog(MsgLevel.Warn, $"节点({ID}.{NodeName})运行取消！", true);
                        SetRunResult(startTime, NodeStatus.Unexecuted);
                        throw new OperationCanceledException($"节点({ID}.{NodeName})运行取消！");
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！原因:{ex.Message}", true);
                        SetRunResult(startTime, NodeStatus.Failed);
                        throw new Exception($"节点({ID}.{NodeName})运行失败，原因：{ex.Message}");
                    }
                }
            }
        }

        private void SendSignalToPlc(NodeParamWaitSoftTrigger param)
        {
            bool flag = false;
            OperateResult readResult, writeResult;
            // 读取拍照信号
            do
            {
                readResult = param.Plc.ReadPLCData(param.Address, DataType.BOOL);
            } while (!readResult.IsSuccess || !((OperateResult<bool>)readResult).Content);  // 读取失败或读取不到拍照信号为true均需要重试
            // 重置拍照信号
            do
            {
                writeResult = param.Plc.WritePLCData(param.Address, false);
            } while (!writeResult.IsSuccess);
            LogHelper.AddLog(MsgLevel.Info, $"{param.Address}信号发送成功", true);
        }

    }
}
