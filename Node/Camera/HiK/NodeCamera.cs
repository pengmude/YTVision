using Logger;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Forms.测试窗口;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Node.Light.PPX;

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
        public override void Run()
        {
            DateTime startTime = DateTime.Now;

            if (!Active)
            {
                SetRunStatus(startTime, true);
                return;
            }
            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunStatus(startTime, false);
                throw new Exception($"节点({NodeName})运行参数未设置或保存！");
            }

            var param = (NodeParamCamera)ParamForm.Params;

            param.Camera.SetTriggerMode(true); // 设置为触发模式
            param.Camera.SetTriggerSource(param.TriggerSource);    // 设置触发源
            if (param.TriggerSource == Hardware.Camera.TriggerSource.SOFT)
            {
                if(param.Plc == null || param.TriggerSignal.IsNullOrEmpty())
                {
                    // 软触发不需要等plc信号
                    param.Camera.GrabOne();
                }
                else
                {
                    // TODO:软触发需要plc给信号才拍照

                }
                SetRunStatus(startTime, false);
                return;
            }
            param.Camera.SetTriggerEdge(param.TriggerEdge);
            param.Camera.SetTriggerDelay(param.TriggerDelay);
            param.Camera.SetExposureTime(param.ExposureTime);
            param.Camera.SetGain(param.Gain);
            // TODO: 上面设置了相机参数，接下来处理根据软硬触发相机拍照的逻辑

        }

        private void SetRunStatus(DateTime startTime, bool isOk)
        {
            DateTime endTime = DateTime.Now;
            TimeSpan elapsed = endTime - startTime;
            long elapsedMi11iseconds = (long)elapsed.TotalMilliseconds;
            Result.RunTime = elapsedMi11iseconds;
            Result.Success = isOk;
            Result.RunStatusCode = isOk ? NodeRunStatusCode.OK : NodeRunStatusCode.UNKNOW_ERROR;
        }

    }
}
