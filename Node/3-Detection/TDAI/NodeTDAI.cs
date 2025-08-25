using Logger;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Node._1_Acquisition.ImageSource;
using TDJS_Vision.Node._3_Detection.TDAI.Parse;
using TDJS_Vision.Node._3_Detection.TDAI.Yolo8;

namespace TDJS_Vision.Node._3_Detection.TDAI
{
    public class NodeTDAI : NodeBase
    {
        public NodeTDAI(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormTDAI();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultTDAI();
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task<NodeReturn> Run(CancellationToken token, bool showLog)
        {
            DateTime startTime = DateTime.Now;

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

            if(ParamForm is ParamFormTDAI form)
            {
                if (form.Params is NodeParamTDAI param)
                {
                    if (Result is NodeResultTDAI res)
                    {
                        try
                        {
                            SetStatus(NodeStatus.Unexecuted, "*");
                            base.CheckTokenCancel(token);

                            // 清空上次检测结果
                            res.AlgorithmResult.Clear();

                            // 获取图像
                            OutputImage inputImage = form.GetOutputImage();

                            // 4种模型检出结果解析
                            int result_count = 0;
                            int keypoint_count = 0;

                            switch (param.Yolo8.ModelType)
                            {
                                case ModelType.DET:

                                    #region 执行推理

                                    List<DetResult> det_results = new List<DetResult>();

                                    // 刺破机颜色线序需要“截图节点”绘制的多个矩形来屏蔽双色线
                                    if (param.ModelName == ModelName.刺破机颜色线序)
                                    {
                                        var handle = param.Yolo8 as Yolo8Det;
                                        det_results = handle.Detect(inputImage.SrcImg, inputImage.Rectangles);
                                    }
                                    else
                                    {
                                        var handle = param.Yolo8 as Yolo8Det;
                                        for (int i = 0; i < inputImage.Bitmaps.Count; i++)
                                        {
                                            List<DetResult> tmp = new List<DetResult>();
                                            var img = inputImage.Bitmaps[i].Clone();
                                            if (inputImage.Rectangles.Count != 0)
                                                tmp = handle.Detect(img, inputImage.Rectangles[i].Location.X, inputImage.Rectangles[i].Location.Y);
                                            else
                                                tmp = handle.Detect(img);
                                            det_results.AddRange(tmp);
                                        }
                                    }

                                    #endregion

                                    #region 解析结果

                                    switch (param.ModelName)
                                    {
                                        case ModelName.超日TypeC焊锡:
                                            TypeCResultParse.Parse(det_results, result_count, param, ref res);
                                            break;
                                        case ModelName.超日连接器:
                                            Connector.Parse(det_results, result_count, param, ref res);
                                            break;
                                        case ModelName.中厚12类端子:
                                            TerminalClass12.Parse(det_results, result_count, param, ref res);
                                            break;
                                        case ModelName.刺破机:
                                            PiercingMachine.Parse(det_results, result_count, param, ref res);
                                            break;
                                        case ModelName.刺破机颜色线序:
                                            PiercingMachineLineSequence.Parse(det_results, result_count, param, ref res);
                                            break;
                                        default:
                                            throw new Exception("不存在的模型名称！");
                                    }
                                    #endregion

                                    break;

                                case ModelType.OBB:

                                    #region 执行推理

                                    List<ObbResult> obb_results = new List<ObbResult>();

                                    for (int i = 0; i < inputImage.Bitmaps.Count; i++)
                                    {
                                        List<ObbResult> tmp = new List<ObbResult>();
                                        var handle = param.Yolo8 as Yolo8Obb;
                                        var img = inputImage.Bitmaps[i].Clone();
                                        if (inputImage.Rectangles.Count != 0)
                                        {
                                            tmp = handle.Detect(img, inputImage.Rectangles[i].Location.X, inputImage.Rectangles[i].Location.Y);
                                        }
                                        else
                                        {
                                            tmp = handle.Detect(img);
                                        }
                                        obb_results.AddRange(tmp);
                                    }

                                    #endregion

                                    #region 解析结果
                                    // 对输出数据存在宽高互换的情况进行处理
                                    // (目前OBB模型检出的目标结果存在宽高互换的情况，实际角度应为“90°-检出角度”，顺时针为正)
                                    ParseCommon.ProcessingRotatedRects(ref obb_results);

                                    //超日项目的结果解析
                                    switch (param.ModelName)
                                    {
                                        case ModelName.超日线芯:
                                            LineCore.Parse(obb_results, result_count, param, ref res);
                                            break;
                                        case ModelName.超日胶壳:
                                            GelCoat.Parse(obb_results, result_count, param, ref res);
                                            break;
                                        default:
                                            throw new Exception("不存在的模型名称！");
                                    }

                                    #endregion

                                    break;

                                case ModelType.SEG:

                                    # region 执行推理
                                    List<SegResult> seg_results = new List<SegResult>();

                                    for (int i = 0; i < inputImage.Bitmaps.Count; i++)
                                    {
                                        List<SegResult> tmp = new List<SegResult>();
                                        var handle = param.Yolo8 as Yolo8Seg;
                                        var img = inputImage.Bitmaps[i].Clone();
                                        if (inputImage.Rectangles.Count != 0)
                                        {
                                            tmp = handle.Detect(img, inputImage.Rectangles[i].Location.X, inputImage.Rectangles[i].Location.Y);
                                        }
                                        else
                                        {
                                            tmp = handle.Detect(img);
                                        }
                                        seg_results.AddRange(tmp);
                                    }
                                    #endregion

                                    #region 解析结果

                                    switch (param.ModelName)
                                    {
                                        case ModelName.线芯截面:
                                            LineCoreCrossSection.Parse(seg_results, result_count, param, ref res);
                                            break;
                                        default:
                                            break;
                                    }
                                    break;

                                    #endregion

                                case ModelType.POSE:

                                    // TODO: 根据项目需要解析Ai结果
                                    break;

                                default:
                                    break;
                            }

                            var time = SetRunResult(startTime, NodeStatus.Successful);
                            res.RunTime = time;
                            // 返回结果
                            Result = res;
                            if (showLog)
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
            }
            return new NodeReturn(NodeRunFlag.StopRun);
        }
    }
}
