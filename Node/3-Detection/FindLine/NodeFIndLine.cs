﻿using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Node._4_Measurement.ParallelLines;

namespace YTVisionPro.Node._3_Detection.FindLine
{
    /// <summary>
    /// 直线查找节点
    /// </summary>
    internal class NodeFIndLine : NodeBase
    {
        public NodeFIndLine(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new NodeParamFormFindLine(process, this);
            form.SetNodeBelong(this);
            ParamForm = form;
            Result = new NodeResultFindLine();
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
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({NodeName})运行参数未设置或保存！");
            }

            if (ParamForm is NodeParamFormFindLine form)
            {
                if (ParamForm.Params is NodeParamFindLine param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        var (line, image) = form.DetectLine();
                        if (line == null || image == null) { throw new Exception("直线查找失败！"); }

                        ((NodeResultFindLine)Result).Line = line;
                        ((NodeResultFindLine)Result).OutputImage = image;

                        SetRunResult(startTime, NodeStatus.Successful);
                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms，直线长度为：{LineUtils.PointDistance(line.P1, line.P2).ToString("F2")} 像素)", true);
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
