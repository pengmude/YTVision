using HslCommunication;
using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._5_EquipmentCommunication.PLCSoftTrigger
{
    public class NodeWaitSoftTrigger : NodeBase
    {
        public NodeWaitSoftTrigger(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormWaitSoftTrigger();
            Result = new NodeResultWaitSoftTrigger();
        }


        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task<NodeReturn> Run(CancellationToken token, bool showLog)
        {
            DateTime startTime = DateTime.Now;

            if (!Active)
            {
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return new NodeReturn(NodeRunFlag.StopRun);
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
                        base.CheckTokenCancel(token);

                        //如果没有连接则不运行
                        if (param.Plc == null || !param.Plc.IsConnect)
                            throw new Exception("PLC设备资源已释放或尚未连接！");//如果没有连接则不运行

                        // 监听拍照信号,支持取消
                        await Task.Run(() => GetShotSignalFromPlcAsync(param, token));

                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        Result.RunTime = time;
                        if (showLog)
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
                        return new NodeReturn(NodeRunFlag.ContinueRun);
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
            return new NodeReturn(NodeRunFlag.StopRun);
        }

        private async Task GetShotSignalFromPlcAsync(NodeParamWaitSoftTrigger param, CancellationToken token)
        {
            try
            {
                OperateResult<bool> readResult;
                OperateResult writeResult;
                // 读取拍照信号
                do
                {
                    base.CheckTokenCancel(token);
                    readResult = await param.Plc.ReadBoolAsync(param.Address);
                    Thread.Sleep(3);
                } while (!readResult.IsSuccess || !readResult.Content);  // 读取失败或读取不到拍照信号为true均需要重试

                // 重置拍照信号
                if (param.Reset)
                {
                    do
                    {
                        base.CheckTokenCancel(token);
                        writeResult = await param.Plc.WriteBoolAsync(param.Address, new bool[] { false });
                        Thread.Sleep(3);
                    } while (!writeResult.IsSuccess);
                }
            }
            catch (OperationCanceledException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
