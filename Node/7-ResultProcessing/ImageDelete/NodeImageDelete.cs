using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node._7_ResultProcessing.ImageDelete
{
    internal class NodeImageDelete : NodeBase
    {
        public NodeImageDelete(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new NodeParamFormImageDelete();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultImageDelete();
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

            if (ParamForm is NodeParamFormImageDelete form)
            {
                if (ParamForm.Params is NodeParamImageDelete param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        SetRunResult(startTime, NodeStatus.Successful);
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
                        throw new Exception($"节点({ID}.{NodeName})运行失败，原因：{ex.Message}");
                    }
                }
            }
        }
    }
}
