using Logger;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node.TCP.Server
{
    internal class NodeTCPServer : NodeBase
    {
        public NodeTCPServer(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new ParamFormTCPServer();
            form.SetNodeBelong(this);
            form.RunHandler += RunHandler;
            ParamForm = form;
            Result = new NodeResultTCPServer();
        }
        /// <summary>
        /// 节点界面点击执行TCP客户端发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task RunHandler(object sender, EventArgs e)
        {
            await Run(CancellationToken.None);
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

            var param = (NodeParamTCPServer)ParamForm.Params;

            try
            {
                SetStatus(NodeStatus.Unexecuted, "*");
                base.Run(token);

                //如果没有连接则不运行
                if (!param.Sever.IsConnect)
                    throw new Exception($"TCP设备（{param.Sever.DevName}）尚未连接！");

                // 服务器响应给客户端的数据
                await Response(param);

                long time = SetRunResult(startTime, NodeStatus.Successful);
                LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms", true);
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
        /// 响应客户端
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task Response(NodeParamTCPServer param)
        {
            var server = (Device.TCP.TCPServer)param.Sever;
            TcpClient client = server.Ip2TcpClientDic[param.ClientIP];

            // 如果需要订阅条件
            if (param.NeedsCondition)
            {
                // 如果订阅的布尔值为true响应的内容
                if (((ParamFormTCPServer)ParamForm).GetCondition())
                {
                    await server.SendMessge2Client(client, param.ResponseContentTrue);
                }
                else
                {
                    await server.SendMessge2Client(client, param.ResponseContentFalse);
                }
            }
            else
            {
                await server.SendMessge2Client(client, param.NoConditionContent);
            }
        }
    }
}
