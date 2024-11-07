using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node.ImageSrc.CameraIO
{
    internal class NodeCameraIO: NodeBase
    {

        public NodeCameraIO(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormCameraIO();
            Result = new NodeResultCameraIO();
            ParamForm.SetNodeBelong(this);
        }

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

            if (ParamForm is ParamFormCameraIO from)
            {
                if (ParamForm.Params is NodeParamCameraIO param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);
                        //获取AI检测条件
                        bool con = from.GetCondition();
                        if (con)
                        {
                            // 设置相机线选择器
                            param.Camera.SetLineSelector(param.LineSelector);
                            // 设置相机IO线模式
                            param.Camera.SetLineMode(param.LineMode);
                            // 设置使能启用
                            param.Camera.SetLineInverter(true);
                            await Task.Delay(100);
                            param.Camera.SetLineInverter(false);
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
