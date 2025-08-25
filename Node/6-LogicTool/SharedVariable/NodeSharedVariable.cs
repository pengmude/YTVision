using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._6_LogicTool.SharedVariable
{
    public class NodeSharedVariable : NodeBase
    {
        public NodeSharedVariable(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new NodeParamFormSharedVariable();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultSharedVariable();
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

            if (ParamForm is NodeParamFormSharedVariable form)
            {
                if (form.Params is NodeParamSharedVariable param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        string resStr = "";
                        var result = ((NodeResultSharedVariable)Result);

                        // 判断读还是写
                        if (param.IsRead)
                        {
                            if (param.Flag)
                            {
                                result.Value = Solution.Instance.SharedVariable.GetValue(param.ReadName, param.Index);
                            }
                            else
                            {
                                result.Value = Solution.Instance.SharedVariable.GetValue(param.ReadName);
                            }
                        }
                        else
                        {
                            Solution.Instance.SharedVariable.SetValue(param.WriteName, new SharedVarValue(form.GetSubValue(), form.GetSubValue().GetType()));
                        }

                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        Result.RunTime = time;
                        Result = result;
                        if(showLog)
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

        private static List<Bitmap> ConvertToBitmapArray(object obj)
        {
            if (obj is List<Bitmap> bitmapList)
            {
                return bitmapList;
            }
            else
            {
                throw new InvalidCastException("对象不是 List<Bitmap> 类型");
            }
        }
    }
}
