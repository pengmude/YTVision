using Logger;
using Sunny.UI;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Forms.测试窗口;
using YTVisionPro.Node.Tool.SleepTool;

namespace YTVisionPro.Node.Camera.HiK
{
    internal class NodeCamera : NodeBase
    {
        /// <summary>
        /// 自动重置事件，用于确保软触发或者硬触发
        /// 采集到图像才继续往下执行其他节点
        /// </summary>
        private AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

        public NodeCamera(string nodeName, Process process, NodeType nodeType) : base(nodeName, process, nodeType)
        {
            ParamForm = new ParamFormCamera();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultCamera();
        }

        /// <summary>
        /// 节点拍照的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Camera_PublishImageEvent(object sender, Bitmap e)
        {
            ((NodeResultCamera)Result).Bitmap = (Bitmap)(Image)e;
            _autoResetEvent.Set();
        }

        /// <summary>
        /// 重写相机节点Run
        /// </summary>
        public override async Task Run(CancellationToken token)
        {
            DateTime startTime = DateTime.Now;

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

            if (ParamForm is ParamFormCamera form)
            {
                if (form.Params is NodeParamCamera param)
                {
                    try
                    {
                        param.Camera.PublishImageEvent -= Camera_PublishImageEvent;
                        param.Camera.PublishImageEvent += Camera_PublishImageEvent;

                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);


                        param.Camera.SetTriggerDelay(param.TriggerDelay);       // 设置触发延迟（软、硬触发）
                        param.Camera.SetExposureTime(param.ExposureTime);       // 设置曝光时间（软、硬触发）
                        param.Camera.SetGain(param.Gain);                       // 设置增益（软、硬触发）
                        param.Camera.SetTriggerMode(true);                      // 设置为触发模式
                        param.Camera.StartGrabbing();                           // 开始取流（软、硬触发）
                        param.Camera.SetTriggerSource(param.TriggerSource);     // 设置触发源
                        switch (param.TriggerSource)
                        {
                            case Hardware.Camera.TriggerSource.Auto:
                                // 自动触发
                                break;
                            case Hardware.Camera.TriggerSource.SOFT:
                                // 软触发
                                param.Camera.GrabOne();                         // 软触发能直接获得图像
                                _autoResetEvent.WaitOne();
                                break;
                            default:
                                // 硬触发LINE0~LINE4
                                param.Camera.SetTriggerEdge(param.TriggerEdge); // 设置硬触发沿
                                _autoResetEvent.WaitOne();
                                break;
                        }

                        SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！", true);
                    }
                    catch (OperationCanceledException)
                    {
                        LogHelper.AddLog(MsgLevel.Warn, $"节点({ID}.{NodeName})运行取消！", true);
                        SetRunResult(startTime, NodeStatus.Unexecuted);
                        throw new OperationCanceledException($"节点({ID}.{NodeName})运行取消！");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            
        }
    }
}
