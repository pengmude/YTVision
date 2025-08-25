using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._5_EquipmentCommunication.TcpClient
{
    public class NodeTCPClient : NodeBase
    {
        private Process _process;
        public NodeTCPClient(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new ParamFormTCPClient();
            form.SetNodeBelong(this);
            form.RunHandler += RunHandler;
            ParamForm = form;
            Result = new NodeResultTCPClient();
            _process = process;
        }

        /// <summary>
        /// 节点界面点击执行TCP客户端发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task RunHandler(object sender, EventArgs e)
        {
            await Run(CancellationToken.None, _process.ShowLog);
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

            var param = (NodeParamTCPClient)ParamForm.Params;

            try
            {
                SetStatus(NodeStatus.Unexecuted, "*");
                base.CheckTokenCancel(token);

                //如果没有连接则不运行
                if (!param.Device.IsConnect)
                    throw new Exception($"TCP设备（{param.Device.DevName}）尚未连接！");

                ((ParamFormTCPClient)ParamForm).SetResult("");    // 先清空界面的响应结果

                //发起请求
                SendRequest(param, startTime);

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
                throw new Exception($"节点({ID}.{NodeName})运行失败！原因:{ex.Message}");
            }
        }

        /// <summary>
        /// 客户端发起请求
        /// </summary>
        /// <param name="param"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        private async Task SendRequest(NodeParamTCPClient param, DateTime startTime)
        {
            var form = ((ParamFormTCPClient)ParamForm);

            // 需要条件发起请求
            if (param.NeedsCondition)
            {
                if (form.GetCondition())
                {
                    // 条件为真发送的内容
                    await ((Device.TCP.TCPClient)param.Device).SendMessage(param.SendContentTrue);
                }
                else
                {
                    //条件为假发送的内容
                    await ((Device.TCP.TCPClient)param.Device).SendMessage(param.SendContentFalse);
                }
            }
            else
            {
                // 不需要条件发送的内容
                await ((Device.TCP.TCPClient)param.Device).SendMessage(param.NoConditionContent);
            }

            if (param.IsWaitingForResponse)
            {
                // 接收响应结果
                string data = await((Device.TCP.TCPClient)param.Device).ReceiveMessage();

                // 设置响应结果给界面
                ((ParamFormTCPClient)ParamForm).SetResult(data);

                // 设置数据给节点结果
                if (Result is NodeResultTCPClient result)
                {
                    result.ResponseData = data;
                    Result = result;
                }
                long time = SetRunResult(startTime, NodeStatus.Successful);
                LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms, 返回结果：{data})", true);
            }
            else
            {
                long time = SetRunResult(startTime, NodeStatus.Successful);
                LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms", true);
            }
        }
    }
}
