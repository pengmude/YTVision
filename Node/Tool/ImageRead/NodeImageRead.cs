using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.ImageRead
{
    internal class NodeImageRead : NodeBase
    {
        public NodeImageRead(string nodeName, Process process) : base(nodeName, process) 
        {
            ParamForm = new ParamFormImageRead();
            Result = new NodeResultImageRead();
        }

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
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行参数未设置或保存！", true);
                SetRunStatus(startTime, false);
                throw new Exception($"节点({ID}.{NodeName})运行参数未设置或保存！");
            }
            
            var param = (NodeParamImageRead)ParamForm.Params;
            try
            {
                if (Result is NodeResultImageRead res)
                {
                    res.Bitmap = new Bitmap(param.ImagePath);
                    Result = res;
                    SetRunStatus(startTime, true);
                    LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！", true);
                }
            }
            catch (Exception)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！", true);
                SetRunStatus(startTime, false);
                throw new Exception($"节点({ID}.{NodeName})运行失败！");
            }

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
