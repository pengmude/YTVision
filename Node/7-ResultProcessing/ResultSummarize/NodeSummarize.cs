using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Forms.ResultView;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._7_ResultProcessing.ResultSummarize
{
    internal class NodeSummarize : NodeBase
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
                        base.CheckTokenCancel(token);

                        var res1 = form.GetResult1();
                        var res2 = form.GetResult2();
                        var res3 = form.GetResult3();
                        var res4 = form.GetResult4();

                        ResultViewData[] results = new ResultViewData[4] { res1, res2, res3, res4 };
                        ((NodeResultSummarize)Result).SummaryResult =  ResultSummarize(results, param);


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
        private ResultViewData ResultSummarize(ResultViewData[] results, NodeParamSummarize param)
        {
            List<ResultViewData> total = new List<ResultViewData>();
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

        /// <summary>
        /// 合并多个算法结果
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static ResultViewData CombineResults(List<ResultViewData> results)
        {
            // 使用 SelectMany 将所有 SingleRowDataList 合并为一个列表
            List<SingleResultViewData> combinedSingleRowDataList = results
                .SelectMany(r => r.SingleRowDataList)
                .ToList();

            // 创建新的 ResultViewData 对象
            ResultViewData combinedResult = new ResultViewData
            {
                SingleRowDataList = combinedSingleRowDataList
            };

            return combinedResult;
        }
    }
}
