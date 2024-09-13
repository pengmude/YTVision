using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node.Light
{
    internal class NodeLight : NodeBase
    {
        /// <summary>
        /// 创建一个指定名称的节点
        /// </summary>
        /// <param name="nodeText"></param>
        public NodeLight(string nodeName, Process process, NodeType nodeType) : base(nodeName, process, nodeType)
        {
            ParamForm = new ParamFormLight(nodeName, process);
            Result = new NodeResultLight();
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
            if(ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({NodeName})运行参数未设置或保存！");
            }

            var param = (NodeParamLight)ParamForm.Params;

            try
            {
                SetStatus(NodeStatus.Unexecuted, "*");
                base.Run(token);

                param.Light.TurnOn(param.Brightness, param.Time);
                SetRunResult(startTime, NodeStatus.Successful);
                LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！", true);
            }
            catch(OperationCanceledException)
            {
                LogHelper.AddLog(MsgLevel.Warn, $"节点({ID}.{NodeName})运行取消！", true);
                SetRunResult(startTime, NodeStatus.Unexecuted);
                throw new OperationCanceledException($"节点({ID}.{NodeName})运行取消！");
            }
            catch (Exception)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({ID}.{NodeName})运行失败！");
            }
        }
    }
}
