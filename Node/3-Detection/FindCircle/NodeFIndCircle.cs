using Logger;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._3_Detection.FindCircle
{
    /// <summary>
    /// 直线查找节点
    /// </summary>
    public class NodeFIndCircle : NodeBase
    {
        public NodeFIndCircle(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new NodeParamFormFindCircle(process, this);
            form.SetNodeBelong(this);
            ParamForm = form;
            Result = new NodeResultFindCircle();
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

            if (ParamForm is NodeParamFormFindCircle form)
            {
                if (ParamForm.Params is NodeParamFindCircle param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        string res = string.Empty;
                        var (Circle, image) = form.DetectCircle();
                        if (Circle == null || image == null) { throw new Exception("圆查找失败！"); }

                        if (!param.OKEnable)
                            res = "未启用";
                        else if (Circle.Radius >= param.OKMinR && Circle.Radius <= param.OKMaxR)
                            res = "OK";
                        else
                            res = "NG";

                        // 输出节点结果
                        ((NodeResultFindCircle)Result).OutputImage.Bitmaps = new List<Mat>() { image.ToMat()};
                        ((NodeResultFindCircle)Result).Result.Clear();
                        ((NodeResultFindCircle)Result).Result.Circles.Add(new ColorCircle(Circle, Color.Green));
                        ((NodeResultFindCircle)Result).Result.Texts.Add(new ColorText($"识别到的圆心：（{Circle.Center.X}, {Circle.Center.Y}）\n半径：{Circle.Radius}", Color.Green));

                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        Result.RunTime = time;
                        if (showLog)
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms，圆半径：{Circle.Radius} 像素, 圆心：({Circle.Center.X},{Circle.Center.Y}), 判定：{res}", true);

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
