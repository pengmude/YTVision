using Logger;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._1_Acquisition.ImageSource
{
    public class NodeImageSource : NodeBase
    {
        /// <summary>
        /// 用于保存上次访问的图片索引
        /// </summary>
        public int LastIndex = 0;

        /// <summary>
        /// 当前图片索引
        /// </summary>
        private string _nextImagePath;

        /// <summary>
        /// 当前图片
        /// </summary>
        private Mat _mat;

        public NodeImageSource(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormImageSource(this);
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultImageSource();
        }
        public void LoadTestImage(NodeParamImageSoucre param, NodeResultImageSource res)
        {
            if (!string.IsNullOrEmpty(param.ImagePath))
            {
                // 单图模式
                _mat = LoadImage(param.ImagePath);
                res.OutputImage.Bitmaps = new List<Mat> { _mat };
                return;
            }

            // 多图模式
            if (param.ImagePaths == null || param.ImagePaths.Count == 0)
                throw new ArgumentException("未设置图像路径列表");

            string nextImagePath;

            if (param.IsAutoLoop)
            {
                // 自动循环模式：依次取下一张图像
                nextImagePath = param.ImagePaths[LastIndex];
                LastIndex = (LastIndex + 1) % param.ImagePaths.Count;
            }
            else
            {
                // 手动模式：重复使用上一张图像
                int index = LastIndex == 0 ? 0 : LastIndex - 1;
                nextImagePath = param.ImagePaths[index];
            }

            _mat = LoadImage(nextImagePath);
            res.OutputImage.Bitmaps = new List<Mat> { _mat };
        }

        // 封装图像加载逻辑
        private Mat LoadImage(string imagePath)
        {
            try
            {
                return Cv2.ImRead(imagePath, ImreadModes.Color);
            }
            catch (Exception ex)
            {
                throw new IOException($"无法加载图像：{imagePath}", ex);
            }
        }
        /// <summary>
        /// 节点运行方法
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
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({ID}.{NodeName})运行参数未设置或保存！");
            }

            if (ParamForm.Params is NodeParamImageSoucre param)
            {
                if (Result is NodeResultImageSource res)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);


                        if (param.ImageSource == "本地图像")
                        {
                            LoadTestImage(param, res);
                        }
                        else if (param.ImageSource == "相机")
                        {
                            if (param.Camera == null) throw new Exception("相机对象无效！");
                            if (!param.Camera.IsOpen) throw new Exception("相机尚未连接！");

                            // 是否需要每次设置相机参数
                            if (param.IsEveryTime)
                            {
                                param.Camera.SetTriggerMode(true);
                                param.Camera.SetTriggerDelay(param.TriggerDelay);
                                param.Camera.SetExposureTime(param.ExposureTime);
                                param.Camera.SetGain(param.Gain);
                                param.Camera.SetTriggerSource(param.TriggerSource);
                                param.Camera.SetTriggerEdge(param.TriggerEdge);
                            }
                            try
                            {
                                // 获取一帧图像并设置结果
                                res.OutputImage.Bitmaps = new List<Mat>() { param.Camera.GetOneFrameImage().ToMat() };
                            }
                            catch (Exception ex)
                            {
                                // 获取一帧图像并设置结果
                                res.OutputImage.Bitmaps = new List<Mat>() { null };
                            }
                        }
                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        Result.RunTime = time;
                        if (showLog)
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
                        if (res.OutputImage.Bitmaps.Count == 0 || res.OutputImage.Bitmaps[0] == null)
                            return new NodeReturn(NodeRunFlag.StopRun);
                        return new NodeReturn(NodeRunFlag.ContinueRun);
                    }
                    catch (OperationCanceledException)
                    {
                        LogHelper.AddLog(MsgLevel.Warn, $"节点({ID}.{NodeName})运行取消！", true);
                        SetRunResult(startTime, NodeStatus.Unexecuted);
                        throw new OperationCanceledException($"节点({ID}.{NodeName})运行取消！");
                    }
                    catch (Exception e)
                    {
                        LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！原因：{e.Message}", true);
                        SetRunResult(startTime, NodeStatus.Failed);
                        throw new Exception($"节点({ID}.{NodeName})运行失败！");
                    }
                }
            }

            return new NodeReturn(NodeRunFlag.StopRun);
        }
    }
}