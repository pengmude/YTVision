using Basler.Pylon;
using Logger;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Forms.ImageViewer;
using YTVisionPro.Device.Camera;
using YTVisionPro.Node;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace YTVisionPro.Node.ImageSrc.Shot
{
    internal class NodeShot : NodeBase
    {
        /// <summary>
        /// 自动重置事件，用于确保软触发或者硬触发
        /// 采集到图像才继续往下执行其他节点
        /// </summary>
        private AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private readonly object _lock = new object();
        /// <summary>
        /// 图像发布事件
        /// </summary>
        public static event EventHandler<ImageShowPamra> ImageShowChanged;

        public NodeShot(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormShot();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultShot();
            ParamFormShot.HardTriggerCompleted += ParamFormShot_HardTriggerCompleted;
        }
        /// <summary>
        /// 硬触发完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void ParamFormShot_HardTriggerCompleted(object sender, HardTriggerResult e)
        {
            // 设置图像结果
            ((NodeResultShot)Result).Bitmap = e.Bitmap;
            // 设置本节点的运行状态
            long time = SetRunResult(e.StartTime, NodeStatus.Successful);
            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
            // 继续在该节点往下执行
            await Process.RunForHardTrigger(this, (long)(DateTime.Now - e.StartTime).TotalMilliseconds);
        }

        /// <summary>
        /// 节点拍照的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Camera_PublishImageEvent(object sender, Bitmap e)
        {
            ((NodeResultShot)Result).Bitmap = e;
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

            if (ParamForm is ParamFormShot form)
            {
                if (form.Params is NodeParamShot param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        if (!param.Camera.IsOpen) { throw new Exception("相机尚未连接！"); }

                        if(param.TriggerSource != TriggerSource.SOFT) { throw new Exception("只有软触发拍照才能手动点击运行！"); }

                        param.Camera.PublishImageEvent += Camera_PublishImageEvent;
                        
                        // 频闪才需要每次设置相机参数，不频闪的话在参数界面设置一次即可
                        if (param.IsStrobing)
                        {
                            param.Camera.SetTriggerMode(true);
                            param.Camera.SetTriggerDelay(param.TriggerDelay);       // 设置触发延迟
                            param.Camera.SetExposureTime(param.ExposureTime);       // 设置曝光时间
                            param.Camera.SetGain(param.Gain);                       // 设置增益
                            param.Camera.SetTriggerSource(param.TriggerSource);     // 设置触发源
                            param.Camera.SetTriggerEdge(param.TriggerEdge);         // 设置硬触发边沿
                        }

                        param.Camera.GrabOne(); // 软触发
                        bool result = _autoResetEvent.WaitOne(1000);
                        if (!result) { throw new Exception("软触发取图超时！"); }

                        await Task.Run(() =>
                        {
                            // 发送采集到的图像
                            ImageShowChanged?.Invoke(this, new ImageShowPamra(param.WindowName, ((NodeResultShot)Result).Bitmap));
                        });

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
                        throw new Exception($"节点({ID}.{NodeName})运行失败，原因：{ex.Message}");
                    }
                    finally
                    {
                        param.Camera.PublishImageEvent -= Camera_PublishImageEvent;
                    }
                }
            }
            
        }
    }
}
