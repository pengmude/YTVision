using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            ParamForm.OnNodeParamChange += ParamForm_OnNodeParamChange;
            Result = new NodeResultCamera();
            ((NodeResultCamera)Result).Bitmap = new System.Drawing.Bitmap(@"C:\Users\pengm\Pictures\NOIMAGE.bmp");
        }

        /// <summary>
        /// 节点参数改变时更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamForm_OnNodeParamChange(object sender, INodeParam e)
        {
            Params = e;
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
            if (Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunStatus(startTime, false);
                throw new Exception($"节点({NodeName})运行参数未设置或保存！");
            }

            var param = (NodeParamCamera)Params;
            param.Camera.SetTriggerMode(true); // 触发模式
            param.Camera.SetTriggerSource(param.TriggerSource);    // 触发源
            if (param.TriggerSource == Hardware.Camera.TriggerSource.SOFT)
            {
                MessageBox.Show("软触发还没实现呢……");
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
