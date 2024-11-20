using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node._6_LogicTool.SharedVariable
{
    internal class NodeSharedVariable : NodeBase
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
        public override async Task Run(CancellationToken token)
        {
            DateTime startTime = DateTime.Now;

            // 参数合法性校验
            if (!Active)
            {
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return;
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
                            var val = Solution.Instance.SharedVariable.GetValue(param.ReadName);
                            if (param.Flag)
                            {
                                switch (param.Type)
                                {
                                    case SharedVarTypeEnum.AllType:
                                    case SharedVarTypeEnum.Bool:
                                    case SharedVarTypeEnum.Int:
                                    case SharedVarTypeEnum.String:
                                    case SharedVarTypeEnum.Float:
                                    case SharedVarTypeEnum.Double:
                                    case SharedVarTypeEnum.Bitmap:
                                    case SharedVarTypeEnum.ResultViewData:
                                        throw new Exception("不是数组类型！");
                                    case SharedVarTypeEnum.BitmapArr:
                                        result.Value = ConvertToBitmapArray(val)[param.Index];
                                        break;
                                }
                            }
                            else
                            {
                                result.Value = Solution.Instance.SharedVariable.GetValue(param.ReadName);
                            }
                            result.Type = param.Type;
                            resStr = $"{result.Value}";
                            Result = result;

                            long time = SetRunResult(startTime, NodeStatus.Successful);
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms, 读到的共享变量为：“{resStr})”", true);
                        }
                        else
                        {
                            Solution.Instance.SharedVariable.SetValue(param.WriteName, form.GetSubValue());

                            long time = SetRunResult(startTime, NodeStatus.Successful);
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
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
