using Logger;
using Sunny.UI;
using System;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Forms.测试窗口;

namespace YTVisionPro.Node.Camera.HiK
{
    internal class NodeCamera : NodeBase
    {
        public NodeCamera(string nodeName, Process process) : base(nodeName, process)
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
        private void Camera_PublishImageEvent(object sender, System.Drawing.Bitmap e)
        {
            ((NodeResultCamera)Result).Bitmap = e;
            Form3 form = new Form3();
            form.ShowDialog();
            form.SetBitMap(((NodeResultCamera)Result).Bitmap);
        }

        /// <summary>
        /// 重写相机节点Run
        /// </summary>
        public override Task Run(CancellationToken token)
        {
            DateTime startTime = DateTime.Now;

            if (!Active)
            {
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return Task.CompletedTask;
            }
            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({NodeName})运行参数未设置或保存！");
            }

            var param = (NodeParamCamera)ParamForm.Params;
            param.Camera.PublishImageEvent += Camera_PublishImageEvent;

            try
            {
                SetStatus(NodeStatus.Unexecuted, "*");
                base.Run(token);

                param.Camera.SetTriggerMode(true); // 设置为触发模式
                param.Camera.SetTriggerSource(param.TriggerSource);    // 设置触发源
                param.Camera.StartGrabbing();

                if (param.TriggerSource == Hardware.Camera.TriggerSource.SOFT)
                {
                    if (param.Plc == null || param.TriggerSignal.IsNullOrEmpty())
                    {
                        // 软触发不需要等plc信号
                        param.Camera.GrabOne();
                    }
                    else
                    {
                        // TODO:软触发需要plc给信号才拍照

                    }
                }
                param.Camera.SetTriggerEdge(param.TriggerEdge);
                param.Camera.SetTriggerDelay(param.TriggerDelay);
                param.Camera.SetExposureTime(param.ExposureTime);
                param.Camera.SetGain(param.Gain);
                // TODO: 上面设置了相机参数，接下来处理根据软硬触发相机拍照的逻辑


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


            return Task.CompletedTask;
        }
    }
}
