using Logger;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._6_LogicTool.If
{
    public class NodeIf : NodeBase
    {
        public NodeIf(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new NodeParamFormIf();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultElse();
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task<NodeReturn> Run(CancellationToken token, bool showLog)
        {
            DateTime startTime = DateTime.Now;

            // 参数合法性校验
            if (!Active)
            {
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return new NodeReturn(NodeRunFlag.StopRun);
            }
            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({ID}.{NodeName})运行参数未设置或保存！");
            }

            if (ParamForm is NodeParamFormIf form)
            {
                if (form.Params is NodeParamElse param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        // 检查条件是否为true
                        bool condition = false;
                        condition = form.GetCondition();

                        // 查找匹配的 Else 和 EndIf
                        FindElseAndEndIf(Process.Nodes, Process.Nodes.IndexOf(this), out int elseIndex, out int endIndex);

                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        Result.RunTime = time;
                        if (showLog)
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);

                        if (condition)
                        {
                            // 置true执行到Else节点会跳过
                            Process.SkipNextElseBlock = true;
                            // 条件为真：执行 if 后面的节点，直到 else 或 endif
                            return new NodeReturn(NodeRunFlag.ContinueRun, -1); // -1顺序执行下一个
                        }
                        else
                        {
                            Process.SkipNextElseBlock = false;
                            // 条件为假：跳过 if 分支，跳到 else 或 endif 后
                            return new NodeReturn(NodeRunFlag.ContinueRun, elseIndex != -1 ? elseIndex : endIndex);
                        }
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
        /// <summary>
        /// 查找 else 和 endif节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="ifIndex"></param>
        /// <param name="elseIndex"></param>
        /// <param name="endIndex"></param>
        public static void FindElseAndEndIf(List<NodeBase> nodes, int index, out int elseIndex, out int endIndex)
        {
            elseIndex = -1;
            endIndex = -1;
            int depth = 0;

            for (int i = index + 1; i < nodes.Count; i++)
            {
                var node = nodes[i];

                if (node.NodeType == NodeType.If)
                {
                    depth++;
                }
                else if (node.NodeType == NodeType.EndIf)
                {
                    if (depth == 0)
                    {
                        endIndex = i;
                        break;
                    }
                    depth--;
                }
                else if (node.NodeType == NodeType.Else && depth == 0 && elseIndex == -1)
                {
                    elseIndex = i;
                }
            }
        }
    }
}
