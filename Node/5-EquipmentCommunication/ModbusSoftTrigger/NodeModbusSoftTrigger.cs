using Logger;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Device.Modbus;

namespace YTVisionPro.Node._5_EquipmentCommunication.ModbusSoftTrigger
{
    internal class NodeModbusSoftTrigger: NodeBase
    {

        public NodeModbusSoftTrigger(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormModbusSoftTrigger();
            Result = new NodeReusltModbusSoftTrigger();
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

            if (ParamForm is ParamFormModbusSoftTrigger form)
            {
                if (form.Params is NodeParamModbusSoftTrigger param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        //如果没有连接则不运行
                        if (!param.modbus.IsConnect)
                            throw new Exception("设备尚未连接！");

                        await Task.Run(() =>
                        {
                            // 监听拍照信号
                            GetShotSignalFromModbus(param);
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

        private void GetShotSignalFromModbus(NodeParamModbusSoftTrigger param)
        {
            bool[] readResult;
            var modbusPoll = param.modbus as ModbusPoll;
            // 读取拍照信号
            do
            {
                readResult = modbusPoll.ReadCoils(Convert.ToUInt16(param.Address), 1);
            } while (!readResult.All(b => b == true));  // 读取失败或读取不到拍照信号为true均需要重试
            // 重置拍照信号
            modbusPoll.WriteSingleCoil(Convert.ToUInt16(param.Address), false);
            LogHelper.AddLog(MsgLevel.Info, $"{param.Address}信号发送成功", true);
        }
    }
}
