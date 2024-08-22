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
        public NodeCamera(string nodeText, Process process) : base(process, new ParamFormCamera())
        {
            SetNodeText(nodeText);
            ParamForm.OnNodeParamChange += ParamForm_OnNodeParamChange;
            Result = new NodeResultCamera();
        }

        private void ParamForm_OnNodeParamChange(object sender, INodeParam e)
        {
            MessageBox.Show("子类");
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
