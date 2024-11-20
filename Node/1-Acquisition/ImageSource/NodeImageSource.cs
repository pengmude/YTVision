using Logger;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Device.Camera;
using YTVisionPro.Forms.ImageViewer;

namespace YTVisionPro.Node._1_Acquisition.ImageSource
{
    internal class NodeImageSource : NodeBase
    {
        /// <summary>
        /// 图像发布事件
        /// </summary>
        public static event EventHandler<ImageShowPamra> ImageShowChanged;
        /// <summary>
        /// 自动重置事件，用于确保软触发或者硬触发
        /// 采集到图像才继续往下执行其他节点
        /// </summary>
        private AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        /// <summary>
        /// 静态变量用于保存上次访问的图片索引
        /// </summary>
        public static int LastIndex = 0;
        /// <summary>
        /// 当前图片索引
        /// </summary>
        private string _nextImagePath;
        /// <summary>
        /// 当前图片
        /// </summary>
        private Bitmap _bitmap;
        // 读写锁保护相同图像文件的多线程访问安全
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        public NodeImageSource(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormImageSource(this);
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultImageSource();
            ParamFormImageSource.HardTriggerCompleted += ParamFormShot_HardTriggerCompleted;
        }
        
        /// <summary>
        /// 硬触发完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void ParamFormShot_HardTriggerCompleted(object sender, HardTriggerResult e)
        {
            // 流程没启用不运行
            if(!Process.Enable)
                return;
            // 设置图像结果
            ((NodeResultImageSource)Result).Bitmap = e.Bitmap;
            // 设置本节点的运行状态
            long time = SetRunResult(e.StartTime, NodeStatus.Successful);
            LogHelper.AddLog(MsgLevel.Info, $"-----------------------------------------------------  【{Process.ProcessName}】（开始）  -----------------------------------------------------", true);
            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
            try
            {
                // 继续在该节点往下执行
                await Process.RunForHardTrigger(this, (long)(DateTime.Now - e.StartTime).TotalMilliseconds);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 节点拍照的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CameraSoft_PublishImageEvent(object sender, Bitmap e)
        {
            ((NodeResultImageSource)Result).Bitmap = e;
            _autoResetEvent.Set();
        }

        /// <summary>
        /// 节点运行方法
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="OperationCanceledException"></exception>
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
                            if (param.ImagePath != null)
                            {
                                _bitmap = new Bitmap(param.ImagePath);
                                ImageShowChanged?.Invoke(this, new ImageShowPamra(param.WindowName, _bitmap));
                                res.Bitmap = _bitmap;
                            }
                            else
                            {
                                // 是否选择自动选择下一张图片
                                if (param.IsAutoLoop && param.ImagePaths.Count > 0)
                                {
                                    // 获取下一张图片
                                    _nextImagePath = param.ImagePaths[LastIndex];
                                    _bitmap = new Bitmap(_nextImagePath);
                                    ImageShowChanged?.Invoke(this, new ImageShowPamra(param.WindowName, _bitmap));
                                    res.Bitmap = _bitmap;
                                    // 计算下次运行的索引位置
                                    LastIndex = (LastIndex + 1) % param.ImagePaths.Count;
                                }
                                else
                                {
                                    if (LastIndex == 0)
                                    {
                                        _nextImagePath = param.ImagePaths[LastIndex];
                                        _bitmap = new Bitmap(_nextImagePath);
                                        ImageShowChanged?.Invoke(this, new ImageShowPamra(param.WindowName, _bitmap));
                                        res.Bitmap = _bitmap;
                                        return;
                                    }
                                    // 获取当前路径图片
                                    _nextImagePath = param.ImagePaths[LastIndex - 1];
                                    _bitmap = new Bitmap(_nextImagePath);
                                    ImageShowChanged?.Invoke(this, new ImageShowPamra(param.WindowName, _bitmap));
                                    res.Bitmap = _bitmap;
                                }

                            }
                            Result = res;
                        }
                        else if (param.ImageSource == "相机")
                        {
                            if (!param.Camera.IsOpen) { throw new Exception("相机尚未连接！"); }

                            if (param.TriggerSource != TriggerSource.SOFT) { throw new Exception("只有软触发拍照才能手动点击运行！"); }

                            // 解绑硬触发取流事件、绑定软触发
                            param.Camera.PublishImageEvent -= ((ParamFormImageSource)ParamForm).CameraHard_PublishImageEvent;
                            param.Camera.PublishImageEvent -= CameraSoft_PublishImageEvent;
                            param.Camera.PublishImageEvent += CameraSoft_PublishImageEvent;

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
                            bool result = _autoResetEvent.WaitOne(1000);// 软触发1s内取不到图也得返回
                            if (!result) { throw new Exception("软触发取图超时！"); }

                            await Task.Run(() =>
                            {
                                // 发送采集到的图像
                                ImageShowChanged?.Invoke(this, new ImageShowPamra(param.WindowName, ((NodeResultImageSource)Result).Bitmap));
                            });
                        }

                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
                    }
                    catch (OperationCanceledException)
                    {
                        LogHelper.AddLog(MsgLevel.Warn, $"节点({ID}.{NodeName})运行取消！", true);
                        SetRunResult(startTime, NodeStatus.Unexecuted);
                        throw new OperationCanceledException($"节点({ID}.{NodeName})运行取消！");
                    }
                    catch (Exception)
                    {
                        LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！", true);
                        SetRunResult(startTime, NodeStatus.Failed);
                        throw new Exception($"节点({ID}.{NodeName})运行失败！");
                    }
                }
            }
        }
    }
}
