using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Forms.ResultView;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._7_ResultProcessing.ResultSummarize
{
    public class NodeSummarize : NodeBase
    {
        public NodeSummarize(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormSummarize();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultSummarize();
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

            if(ParamForm is ParamFormSummarize form)
            {
                if (form.Params is NodeParamSummarize param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        var res1 = form.GetResult1();
                        var res2 = form.GetResult2();
                        var res3 = form.GetResult3();
                        var res4 = form.GetResult4();

                        AlgorithmResult[] results = new AlgorithmResult[4] { res1, res2, res3, res4 };
                        ((NodeResultSummarize)Result).SummaryResult = ResultSummarize(results, param);


                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        Result.RunTime = time;
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
                        throw new Exception($"节点({ID}.{NodeName})运行失败！原因:{ex.Message}");
                    }
                }
            }
            return new NodeReturn(NodeRunFlag.StopRun);
        }

        /// <summary>
        ///  汇总多个结果
        /// </summary>
        /// <param name="results"></param>
        /// <param name="param"></param>
        private AlgorithmResult ResultSummarize(AlgorithmResult[] results, NodeParamSummarize param)
        {
            List<AlgorithmResult> total = new List<AlgorithmResult>();
            for (int i = 0; i < results.Length; i++)
            {
                var res = results[i];
                var enable = param.Enables[i];
                if (!enable)
                    continue;
                if(res == null)
                    throw new ArgumentNullException("无法获取/解析检测结果！");
                total.Add(res);
            }

            return CombineResults(total);
        }
        public static AlgorithmResult CombineResults(List<AlgorithmResult> results)
        {
            if (results == null || results.Count == 0)
            {
                return new AlgorithmResult();
            }

            // 初始化 AlgorithmResult
            var aiResult = new AlgorithmResult();

            // 合并 DetectResults
            foreach (var result in results)
            {
                if (result?.DetectResults == null) continue;

                foreach (var kvp in result.DetectResults)
                {
                    if (!aiResult.DetectResults.ContainsKey(kvp.Key))
                    {
                        aiResult.DetectResults[kvp.Key] = new List<SingleDetectResult>();
                    }
                    aiResult.DetectResults[kvp.Key].AddRange(kvp.Value);
                }
            }

            // 合并 Rects
            foreach (var result in results)
            {
                if (result?.Rects != null)
                {
                    aiResult.Rects.AddRange(result.Rects);
                }
            }

            // 合并 RectsNgMap
            foreach (var result in results)
            {
                if (result?.RectsNgMap == null) continue;

                foreach (var kvp in result.RectsNgMap)
                {
                    if (!aiResult.RectsNgMap.ContainsKey(kvp.Key))
                    {
                        aiResult.RectsNgMap[kvp.Key] = new List<ColorRotatedRect>();
                    }
                    aiResult.RectsNgMap[kvp.Key].AddRange(kvp.Value);
                }
            }

            // 合并 Texts
            foreach (var result in results)
            {
                if (result?.Texts != null)
                {
                    aiResult.Texts.AddRange(result.Texts);
                }
            }

            // 合并 Lines
            foreach (var result in results)
            {
                if (result?.Lines != null)
                {
                    aiResult.Lines.AddRange(result.Lines);
                }
            }

            // 合并 Cirle
            foreach (var result in results)
            {
                if (result?.Circles != null)
                {
                    aiResult.Circles.AddRange(result.Circles);
                }
            }
            return aiResult;
        }
    }
}
