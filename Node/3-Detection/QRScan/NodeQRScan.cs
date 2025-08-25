using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._3_Detection.QRScan
{
    public class NodeQRScan : NodeBase
    {
        public NodeQRScan(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new NodeParamFormQRScan(process, this);
            form.SetNodeBelong(this);
            ParamForm = form;
            Result = new NodeResultQRScan();
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

            if (ParamForm is NodeParamFormQRScan form)
            {
                if (ParamForm.Params is NodeParamQRScan param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        var time = 0;
                        var res = ((NodeResultQRScan)Result);
                        var codes = await form.QRCodeDetect(false);

                        if (codes == null || codes.Length == 0)
                        {
                            codes = new string[] { "null          " }; //识别不到二维码默认赋“null”值
                        }

                        //输出结果
                        res.Codes = codes;
                        res.FirstCode = codes[0];
                        res.Result.Clear();
                        // 检查 codes 数组中是否有 null 元素
                        if (codes[0] == "null          ")
                        {
                            res.Result.Clear();
                            res.Result.Texts.Add(new ColorText("条形码/二维码识别结果：[空]", Color.Red));
                        }
                        else
                        {
                            for (int i = 0; i < codes.Length; i++)
                                res.Result.Texts.Add(new ColorText($"条形码/二维码识别结果{i + 1}：\n{codes[i]}", Color.Green));
                        }
                        time = SetRunResult(startTime, NodeStatus.Successful);
                        res.RunTime = time;
                        Result = res;
                        SetRunResult(startTime, NodeStatus.Successful);
                        if (showLog)
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms，二维码数据为:“{codes[0]}”", true);
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
