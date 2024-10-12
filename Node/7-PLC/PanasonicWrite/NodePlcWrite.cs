﻿using HslCommunication;
using Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.PLC.PanasonicWirte
{
    internal class NodePlcWrite : NodeBase
    {
        public NodePlcWrite(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType) 
        {
            ParamForm = new ParamFormPlcWrite();
            Result = new NodeResultPlcWrite();
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

            var param = (NodeParamPlcWrite)ParamForm.Params;
            try
            {
                SetStatus(NodeStatus.Unexecuted, "*");
                base.Run(token);

                switch (param.Value)
                {
                    case bool bValue:
                        WriteAndVerify(param.Plc, param.Address, bValue, DataType.BOOL);
                        break;

                    case int iValue:
                        WriteAndVerify(param.Plc, param.Address, iValue, DataType.INT);
                        break;

                    case string sValue:
                        WriteAndVerify(param.Plc, param.Address, sValue, DataType.STRING);
                        break;
                }

                LogHelper.AddLog(MsgLevel.Info, $"{param.Address}信号发送成功", true);

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
                throw new Exception($"节点({ID}.{NodeName})运行失败！原因:{ex.Message}");
            }
        }

        private void WriteAndVerify<T>(IPlc plc, string address, T value, DataType dataType)
        {
            OperateResult res = new OperateResult();
            do
            {
                res = plc.WritePLCData(address, value);
            } while (!res.IsSuccess);
        }
    }
}