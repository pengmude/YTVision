using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.Tool.ResultSummarize
{
    internal class NodeSummarize : NodeBase
    {
        public NodeSummarize(string nodeName, Process process, NodeType nodeType) : base(nodeName, process, nodeType)
        {
            ParamForm = new ParamFormSummarize();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultSummarize();
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

            if(ParamForm is ParamFormSummarize form)
                if(form.Params is NodeParamSummarize param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        param.AiResult = form.GetAiResult();
                        param.DetectResult = form.GetDetectResult();
                        ResultSummarize(param);

                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
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
        }

        /// <summary>
        /// 汇总结果
        /// </summary>
        /// <param name="param"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private void ResultSummarize(NodeParamSummarize param)
        {
            // 检查 param 是否为 null
            if (param == null)
                throw new ArgumentNullException("无法获取/解析检测结果！");
            if (param.AiResult == null || param.DetectResult == null)
                throw new Exception($"无法获取/解析检测结果！");
            if (Result is NodeResultSummarize res)
            {
                AiResult aiResult = new AiResult();
                aiResult.DeepStudyResult = param.AiResult.DeepStudyResult.Concat(param.DetectResult.DeepStudyResult).ToList();
                res.AllResult = aiResult;
                Result = res;
            }
            else
            {
                throw new InvalidOperationException("不是有效的参数！");
            }
        }
    }
}
