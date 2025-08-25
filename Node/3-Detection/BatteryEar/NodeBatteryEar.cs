using Logger;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Device.Modbus;
using Point = OpenCvSharp.Point;

namespace TDJS_Vision.Node._3_Detection.BatteryEar
{
    public class NodeBatteryEar : NodeBase
    {
        public NodeBatteryEar(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new NodeParamFormBatteryEar(process, this);
            form.SetNodeBelong(this);
            ParamForm = form;
            Result = new NodeResultBatteryEar();
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

            if (ParamForm is NodeParamFormBatteryEar form)
            {
                if (ParamForm.Params is NodeParamBatteryEar param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        // 获取图像
                        form.UpdataImage();

                        var (vals, rects, lines, img) = form.CalculateJierToMarkVerticalDistancesOnOriginal();
                        // 单位转换、补偿
                        vals = vals.Select(x => double.Parse((x * param.Scale + param.DeltaMM).ToString("F2"))).ToList();

                        // 查找对应设备
                        var device = (IModbus)Solution.Instance.AllDevices.Find(r => r.DevName == param.DeviceName);
                        if (device == null || !device.IsConnect)
                            throw new Exception("通信设备异常！");

                        // 是否OK, OK的话会有4条线
                        bool isOk = lines.Count() == 4;
                        if (isOk)
                        {
                            // 将 4 个 float 转换为 8 个 ushort，并按地址顺序排列
                            List<ushort> allData = new List<ushort>();

                            // 注意：param.Add1 = 200, Add2 = 202, Add3 = 204, Add4 = 206
                            // 所以数据顺序应为：vals[3], vals[2], vals[1], vals[0]
                            foreach (float val in new[] { (float)vals[3], (float)vals[2], (float)vals[1], (float)vals[0] })
                            {
                                ushort[] converted = FloatConvert2UshortArr(val);
                                allData.Add(converted[0]);
                                allData.Add(converted[1]);
                            }

                            // 只调用一次写入4个偏移量结果，起始地址为 200
                            device.WriteMultipleRegistersAsync(200, allData.ToArray());
                            Thread.Sleep(20);
                            // 写入视觉完成信号(1)和OK/NG信号(OK为1，NG为2)
                            device.WriteMultipleRegistersAsync(210, new ushort[] { 1, 1});
                        }
                        else
                        {
                            // 写入视觉完成信号(1)和OK/NG信号(OK为1，NG为2)
                            device.WriteMultipleRegistersAsync(210, new ushort[] { 1, 2 });
                        }

                        // 绘制图像
                        for (int i = 0; i < rects.Count; ++i)
                            Cv2.Rectangle(img, rects[i], Scalar.Blue, 5);
                        foreach (var line in lines)
                            Cv2.Line(img, line.P1, line.P2, Scalar.Red, 5);
                        //Cv2.PutText(img, $"D4:{vals[0]}mm  D2:{vals[2]}mm", new Point(100, 150), HersheyFonts.Italic, 5, Scalar.Green, 14);
                        //Cv2.PutText(img, $"D3:{vals[1]}mm  D1:{vals[3]}mm", new Point(100, 350), HersheyFonts.Italic, 5, Scalar.Green, 14);
                        //Cv2.PutText(img, isOk ? "OK" : "NG", new Point(img.Width - 600, 300), HersheyFonts.Italic, 10, isOk ? Scalar.Green : Scalar.Red, 20);

                        DrawTextWithBackground(img, $"D4:{vals[0]}mm  D2:{vals[2]}mm", new Point(50, 150), 5, Scalar.White, 14, isOk ? Scalar.Green : Scalar.Red);
                        DrawTextWithBackground(img, $"D3:{vals[1]}mm  D1:{vals[3]}mm", new Point(50, 350), 5, Scalar.White, 14, isOk ? Scalar.Green : Scalar.Red);
                        DrawTextWithBackground(img, isOk ? "OK" : "NG", new Point(img.Width - 650, 350), 14, Scalar.White, 50, isOk ? Scalar.Green : Scalar.Red);

                        // 输出结果
                        ((NodeResultBatteryEar)Result).IsOk = isOk;
                        ((NodeResultBatteryEar)Result).OutputImage.Bitmaps = new List<Mat> { img };
                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        Result.RunTime = time;
                        if (showLog)
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms）", true);
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
        public static ushort[] FloatConvert2UshortArr(float floatValue)
        {
            byte[] floatBytes = BitConverter.GetBytes(floatValue);
            ushort[] data = new ushort[2];

            data[0] = BitConverter.ToUInt16(floatBytes, 0);
            data[1] = BitConverter.ToUInt16(floatBytes, 2);

            return data;
        }
        /// <summary>
        /// 绘制带背景颜色的文本
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        /// <param name="textPosition"></param>
        /// <param name="fontSize"></param>
        /// <param name="lineWidth"></param>
        /// <param name="textColor"></param>
        /// <param name="bgColor"></param>
        public static void DrawTextWithBackground(Mat img, string text, Point textPosition,
        double fontSize, Scalar textColor, int lineWidth,  Scalar bgColor)
        {
            // 创建字体
            HersheyFonts fontFace = HersheyFonts.HersheyDuplex;

            // 获取文本尺寸（用于计算背景矩形）
            var textSize = Cv2.GetTextSize(text, fontFace, fontSize, lineWidth, out int baseline);
            int padding = 50; // 背景四周留白

            // 计算背景矩形的位置
            Point topLeft = new Point(textPosition.X - padding, textPosition.Y - textSize.Height - padding);
            Point bottomRight = new Point(textPosition.X + textSize.Width + padding, textPosition.Y + padding);

            // 绘制背景矩形
            Cv2.Rectangle(img, topLeft, bottomRight, bgColor, -1); // -1 表示填充

            // 在背景矩形上绘制文本
            Cv2.PutText(img, text, textPosition, fontFace, fontSize, textColor, lineWidth, LineTypes.AntiAlias);
        }
    }
}
