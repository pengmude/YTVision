using Logger;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Forms.ImageViewer;

namespace TDJS_Vision.Node._1_Acquisition.ImageSource
{

    public partial class NodeImageShow : NodeBase
    {
        public static event EventHandler<ImageShowPamra> ImageShowChanged;

        public NodeImageShow(int id, string nodeName, Process process, NodeType nodeType) : base(id, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormImageShow();
            Result = new NodeResultImageShow();
            ParamForm.SetNodeBelong(this);
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task<NodeReturn> Run(CancellationToken token, bool showLog)
        {
            DateTime startTime = DateTime.Now;
            // 参数合法性校验
            if (!Active)
            {
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return new NodeReturn(NodeRunFlag.StopRun);
            }
            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({NodeName})运行参数未设置或保存！");
            }

            if(ParamForm is ParamFormImageShow form)
            {
                if(ParamForm.Params is NodeParamImageShow param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        Bitmap image = form.GetImage();
                        ImageShowChanged?.Invoke(this, new ImageShowPamra(param.WindowName, image));
                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        ((NodeResultImageShow)Result).RunTime = time;
                        if (showLog)
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
                        return new NodeReturn(NodeRunFlag.ContinueRun);
                    }
                    catch (OperationCanceledException)
                    {
                        LogHelper.AddLog(MsgLevel.Warn, $"节点({ID}.{NodeName})运行取消！", true);
                        SetRunResult(startTime, NodeStatus.Unexecuted);
                        throw new OperationCanceledException($"节点({ID}.{NodeName})运行取消！");
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！原因:{ex.Message}", true);
                        SetRunResult(startTime, NodeStatus.Failed);
                        throw new Exception($"节点({ID}.{NodeName})运行失败，原因：{ex.Message}");
                    }
                }
            }
            return new NodeReturn(NodeRunFlag.StopRun);
        }
    }
}
