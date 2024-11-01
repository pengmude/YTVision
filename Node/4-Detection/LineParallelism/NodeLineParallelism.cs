using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static OpenCvSharp.ML.DTrees;
using static YTVisionPro.Node.AI.HTAI.HTAPI;
using YTVisionPro.Node.ImagePreprocessing.ImageCrop;
using Logger;
using YTVisionPro.Node._4_Detection.Caliper;

namespace YTVisionPro.Node._4_Detection.DetectionLineParallelism
{
    internal class NodeLineParallelism : NodeBase
    {
        public NodeLineParallelism(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new NodeParamFormLineParallelism();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultLineParallelism();
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

            if (ParamForm is NodeParamFormLineParallelism form)
            {
                if (ParamForm.Params is NodeParamLineParallelism param)
                {
                    try
                    {

                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        param.LinePoint = form.GetLineEndpoints();
                        form._straightLineAngle = GFraph.AngleOfLine(param.LinePoint);
                        form._linearDistance = GFraph.StraightLineDistance(param.LinePoint);
                        if (form._straightLineAngle == 0 || form._linearDistance == 0 )
                        {
                            throw new Exception("直线太少");
                        }

                        NodeResultLineParallelism nodeResultLineParallelism = new NodeResultLineParallelism();
                        nodeResultLineParallelism.LinearDistance = form._linearDistance;
                        nodeResultLineParallelism.StraightLineAngle = form._straightLineAngle; 

                        SetRunResult(startTime, NodeStatus.Successful);
                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        Result = nodeResultLineParallelism;
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
                        throw new Exception($"节点({ID}.{NodeName})运行失败，原因：{ex.Message}");
                    }
                }
            }
        }
    }
}
