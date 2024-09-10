using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Node.Tool.ImageSave;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace YTVisionPro.Node.Tool.SleepTool
{
    internal class SleepTool : NodeBase
    {
        public SleepTool(string nodeName, Process process) : base(nodeName, process)
        {
            ParamForm = new NodeParamFormSleepTool();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultSleepTool();
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task Run(CancellationToken token)
        {
            DateTime startTime = DateTime.Now;

            // 参数合法性校验
            if (!Active)
            {
                SetRunStatus(startTime, true);
                return;
                //return Task.CompletedTask;
            }
            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行参数未设置或保存！", true);
                SetRunStatus(startTime, false);
                throw new Exception($"节点({ID}.{NodeName})运行参数未设置或保存！");
            }

            if (ParamForm is NodeParamFormSleepTool form)
            {
                if (form.Params is NodeParamSleepTool param)
                {
                    try
                    {
                        base.Run(token);

                        // 异步执行睡眠操作
                        await ExecuteSleepAsync(param.Time);
                        SetRunStatus(startTime, true);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！", true);
                    }
                    catch (OperationCanceledException)
                    {
                        LogHelper.AddLog(MsgLevel.Warn, $"节点({ID}.{NodeName})运行取消！", true);
                        SetRunStatus(startTime, false);
                        throw new OperationCanceledException($"节点({ID}.{NodeName})运行取消！");
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！原因:{ex.Message}", true);
                        SetRunStatus(startTime, false);
                        throw new Exception($"节点({ID}.{NodeName})运行失败，原因：{ex.Message}");
                    }
                }
            }

            //return Task.CompletedTask;
        }

        private async Task ExecuteSleepAsync(int timeInMilliseconds)
        {
            // 使用 Task.Delay 在后台线程上执行异步睡眠操作
            await Task.Delay(timeInMilliseconds);
        }

        /// <summary>
        /// 设置基本的运行结果
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="isOk"></param>
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
