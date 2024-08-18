using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Forms.LightAdd;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Node.Light;
using YTVisionPro.Node.Light.PPX;
using YTVisionPro.Node.NodeDemo;

namespace YTVisionPro.Node.NodeLight.PPX
{
    public class NodeLight : NodeBase
    {
        /// <summary>
        /// 创建一个指定名称的节点
        /// </summary>
        /// <param name="nodeText"></param>
        public NodeLight(string nodeText, LightBrand brand) : base(new ParamFormLight(brand))
        {
            SetNodeText(nodeText);
            Result = new NodeResultLight();
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
            if(Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunStatus(startTime, false);
                throw new Exception($"节点({NodeName})运行参数未设置或保存！");
            }

            var param = (NodeParamLight)Params;


            // 打开操作
            if (param.Open) 
            {
                try
                {
                    param.Light.TurnOn(); 
                    SetRunStatus(startTime, true);
                    LogHelper.AddLog(MsgLevel.Info, $"节点({NodeName})运行成功！", true);
                }
                catch (Exception)
                {
                    LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行失败！", true);
                    SetRunStatus(startTime, false);
                    throw new Exception($"节点({NodeName})运行失败！");
                }
            }
            else
            {
                try
                {
                    param.Light.TurnOff();
                    SetRunStatus(startTime, true);
                    LogHelper.AddLog(MsgLevel.Info, $"节点({NodeName})运行成功！", true);
                }
                catch (Exception)
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
