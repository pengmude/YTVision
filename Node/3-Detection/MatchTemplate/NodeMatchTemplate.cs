using Logger;
using OpenCvSharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node._3_Detection.MatchTemplate
{
    internal class NodeMatchTemplate : NodeBase
    {
        public NodeMatchTemplate(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new NodeParamFormMatchTemplate(process, this);
            form.SetNodeBelong(this);
            ParamForm = form;
            Result = new NodeResultMatchTemplate();
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

            if (ParamForm is NodeParamFormMatchTemplate form)
            {
                if (ParamForm.Params is NodeParamMatchTemplate param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        // 获取图像
                        form.UpdataImage();

                        // 执行模版匹配
                        var (bitmap, rect, score, isOk) = form.MatchTemplate(false);

                        // 输出结果
                        ((NodeResultMatchTemplate)Result).Bitmap = bitmap;
                        ((NodeResultMatchTemplate)Result).Rect = rect;
                        ((NodeResultMatchTemplate)Result).Score = score;
                        ((NodeResultMatchTemplate)Result).IsOk = isOk;

                        if (!isOk)
                            throw new Exception($"当前图像匹配得分为{score.ToString("F2")}, 低于该节点设置的得分阈值，请检查图像成像是否异常，或降低设置的得分阈值！");

                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms，匹配得分：{score.ToString("F2")}, 匹配是否成功: {isOk}", true);
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
