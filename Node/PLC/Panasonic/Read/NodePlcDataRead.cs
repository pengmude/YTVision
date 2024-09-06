using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Node.Light.PPX;

namespace YTVisionPro.Node.PLC.Panasonic.Read
{
    internal class NodePlcDataRead : NodeBase
    {
        public NodePlcDataRead(string nodeName, Process process) : base(nodeName, process)
        {
            ParamForm = new ParamFormPlcDataRead();
            Result = new NodeResultPlcDataRead();
        }

        /// <summary>
        /// 节点运行
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

            var param = (NodeParamPlcDataRead)ParamForm.Params;
            if (Result is NodeResultPlcDataRead res)
            {
                try
                {
                    res.CodeText = param.Plc.ReadPLCData(param.Address, param.Length, DataType.STRING);

                    if (res.CodeText != null)
                    {
                        Result = res;
                        SetRunStatus(startTime, true);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({NodeName})运行成功！", true);
                    }
                    else
                    {
                        LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行失败！", true);
                        SetRunStatus(startTime, false);
                        throw new Exception($"节点({NodeName})运行失败！");
                    }
                        
                }
                catch(Exception)
                {
                    LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行失败！", true);
                    SetRunStatus(startTime, false);
                    throw new Exception($"节点({NodeName})运行失败！");
                }
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
