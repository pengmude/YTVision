using Logger;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node.Tool.ImageSave
{
    internal class NodeImageSave : NodeBase
    {
        public NodeImageSave(string nodeName, Process process, NodeType nodeType) : base(nodeName, process, nodeType) 
        {
            ParamForm = new ParamFormImageSave();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultImageSave();
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
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({ID}.{NodeName})运行参数未设置或保存！");
            }

            if (ParamForm is ParamFormImageSave form)
            {
                if(form.Params is NodeParamSaveImage param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        // 参数获取订阅控件的值
                        if (param.IsBarCode)
                        {
                            param.Barcode = form.GetBarCode();
                            if (param.Barcode.IsNullOrEmpty())
                                throw new Exception($"获取条码结果失败！");
                        }
                        if (param.NeedOkNg)
                            param.AiResult = form.GetAiResult();
                        param.Image = form.GetImage();

                        // 参数合法性判断
                        if (param.SavePath.IsNullOrEmpty())
                            throw new Exception($"存图路径未设置！");
                        if (param.Image == null)
                            throw new Exception($"订阅的图片对象为空！");

                        // 保存
                        SaveImage(startTime, param);
                        SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！", true);
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

        private void SaveImage(DateTime time, NodeParamSaveImage param)
        {
            // 1.存图路径（不包含图片名称）
            string imageSavePath;

            // 2.拼接日期路径
            string nowDataTime = time.ToString("yyyyMMdd");
            imageSavePath = Path.Combine(param.SavePath, nowDataTime);

            // 3.图片名称
            string imageName = param.IsBarCode ? param.Barcode : time.ToString("yyyy-MM-dd HH-mm-ss");

            // 4.存图路径拼接
            List<string> paths = new List<string>();

            //区分早晚班
            if (param.IsDayNight)
            {
                string dayNight = IsDayWorking(time.TimeOfDay, param.DayDataTime, param.NightDataTime) ? "早班" : "晚班";
                imageSavePath = Path.Combine(imageSavePath, dayNight);
            }

            //区分OK/NG
            if (param.NeedOkNg)
            {
                if (param.AiResult == null)
                    throw new Exception($"无法获取/解析AI检测结果！");


                string okNg = param.AiResult.IsAllOk ? "OK" : "NG";
                imageSavePath = Path.Combine(imageSavePath, okNg);

                if (!param.AiResult.IsAllOk)
                {
                    foreach (var res in param.AiResult.DeepStudyResult)
                    {
                        // 默认只保存NG结果
                        if (!res.IsOk)
                            paths.Add(Path.Combine(imageSavePath, res.ClassName));
                    }
                }
                else
                {
                    paths.Add(imageSavePath);
                }
            }
            else
                paths.Add(imageSavePath);


            foreach (var path in paths)
            {
                Save(param.Image, path, imageName, param.NeedCompress, param.CompressValue);
            }

        }


        // 保存图片
        private void Save(Bitmap bitmap, string savePath, string imageName, bool needCompress, long compressValue = 100)
        {
            Directory.CreateDirectory(savePath); // 如果目录已存在，CreateDirectory 不会抛异常
            string fileName = GetFileName(savePath, imageName);
            SaveWithCompress(bitmap, fileName, needCompress, compressValue);
        }

        /// <summary>
        /// 判断是否早班
        /// </summary>
        /// <param name="time"></param>
        /// <param name="daydateTime"></param>
        /// <param name="nightdataTime"></param>
        /// <returns></returns>
        private bool IsDayWorking(TimeSpan time, DateTime daydateTime, DateTime nightdataTime)
        {
            TimeSpan dayStartTime = daydateTime.TimeOfDay;
            TimeSpan nightStartTime = nightdataTime.TimeOfDay;
            return time >= dayStartTime && time < nightStartTime;
        }


        // 保存图片
        private void Save(Bitmap bitmap, string savePath, string imageName, string subFolder, List<string> ALLNGList, bool isCompress, long compressValue = 100)
        {
            if (subFolder == "OK")
            {
                Directory.CreateDirectory(savePath); // 如果目录已存在，CreateDirectory 不会抛异常
                string imagePath = GetFileName(savePath, imageName);
                SaveWithCompress(bitmap, imagePath, isCompress, compressValue);
            }
            else
            {
                foreach (var item in ALLNGList)
                {
                    string itemSavePath = Path.Combine(savePath, item);
                    Directory.CreateDirectory(itemSavePath); // 如果目录已存在，CreateDirectory 不会抛异常
                    string imagePath = GetFileName(itemSavePath, imageName);
                    SaveWithCompress(bitmap, imagePath, isCompress, compressValue);
                }
            }

        }

        private string GetFileName(string filePath, string fileName)
        {
            string newFileName = fileName;
            string fullPath = Path.Combine(filePath, newFileName + ".bmp");
            if (File.Exists(fullPath))
            {
                int suffix = 1;
                do
                {
                    newFileName = $"{fileName}_{suffix}";
                    fullPath = Path.Combine(filePath, newFileName + ".bmp");
                    suffix++;
                }
                while (File.Exists(fullPath));
            }
            return fullPath;
        }

        private void SaveWithCompress(Bitmap bitmap, string imagePath, bool isCompress, long compressValue = 100)
        {
            if (isCompress)
            {
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, compressValue);
                ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
                bitmap.Save(imagePath, jpegCodec, encoderParams);
            }
            else
            {
                bitmap.Save(imagePath);
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
