using Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Forms.ImageViewer;
using YTVisionPro.Node.Camera.HiK;
using YTVisionPro.Node.Tool.SleepTool;

namespace YTVisionPro.Node.Tool.ImageShow
{
    
    internal partial class NodeImageShow : NodeBase
    {
        public static event EventHandler<Paramsa> ImageShowChanged;

        private Paramsa _param = new Paramsa();

        public NodeImageShow(string nodeName, Process process, NodeType nodeType) : base(nodeName, process, nodeType)
        {
            ParamForm = new ParamFormImageShow();
            Result = new NodeResultImageShow();
            ParamForm.SetNodeBelong(this);
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
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return;
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
                        Bitmap image = form.GetImage();
                        _param.Winname = param.WindowName;
                        _param.Bitmap = image;
                        ImageShowChanged?.Invoke(this, _param);
                        SetRunResult(startTime, NodeStatus.Successful);
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
        }
    }
    public class Paramsa
    {
        public string Winname;
        public Bitmap Bitmap;
        public Paramsa() { }
        public Paramsa(string winname, Bitmap bitmap)
        {
            Winname = winname;
            Bitmap = bitmap;
        }
    }
}
