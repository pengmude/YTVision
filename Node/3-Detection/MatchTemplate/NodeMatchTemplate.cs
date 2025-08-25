using Logger;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._3_Detection.MatchTemplate
{
    public class NodeMatchTemplate : NodeBase
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
                        var (bitmap, rectList, scoreList, isOk) = form.MatchTemplate(false);

                        // 输出结果
                        ((NodeResultMatchTemplate)Result).OutputImage.Bitmaps = new List<Mat>() { bitmap.ToMat() };
                        ((NodeResultMatchTemplate)Result).OutputImage.Rectangles = rectList;
                        ((NodeResultMatchTemplate)Result).Result.Clear();
                        ((NodeResultMatchTemplate)Result).Result.Texts.Add(new ColorText($"模版匹配结果个数：{rectList.Count}个", Color.Green));
                        for (int i = 0; i < rectList.Count; i++)
                        {
                            ((NodeResultMatchTemplate)Result).Result.Rects.Add(new ColorRotatedRect(rectList[i]));
                            ((NodeResultMatchTemplate)Result).Result.Texts.Add(new ColorText($"模版匹配结果{i+1}：位置（{rectList[i].X}, {rectList[i].Y}） 宽{rectList[i].Width} 高{rectList[i].Height}", Color.Green));
                        }

                        if (!isOk)
                            throw new Exception($"当前图像匹配得分或者结果个数不足，请降低设置的得分阈值再次尝试！");

                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        Result.RunTime = time;
                        if (showLog)
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms，当前结果最低匹配得分：{scoreList[0].ToString("F2")}, 匹配是否成功: {isOk}", true);
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
    }
}
