using Logger;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node.ResultProcessing.ImageSave
{
    internal class NodeImageSave : NodeBase
    {
        private readonly BlockingCollection<SaveImageTask> _imageQueue = new BlockingCollection<SaveImageTask>();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly int _workerCount = 4; // 工作线程的数量

        public struct SaveImageTask
        {
            public Bitmap Image { get; set; }
            public string Path { get; set; }
            public bool NeedCompress { get; set; } //是否需要压缩
            public long CompressValue { get; set; } //压缩阈值
        }

        public NodeImageSave(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType) 
        {
            ParamForm = new ParamFormImageSave();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultImageSave();
            StartProcessing();
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
                            param.ResultViewData = form.GetAiResult();
                        param.Image = form.GetImage();

                        // 参数合法性判断
                        if (param.SavePath.IsNullOrEmpty())
                            throw new Exception($"存图路径未设置！");
                        if (param.Image == null)
                            throw new Exception($"订阅的图片对象为空！");

                        // 保存
                        // 开始处理队列
                        SaveImage(param);                      
                        //processor.StopProcessing();
                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
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

        private async void SaveImage(NodeParamSaveImage param)
        {
            DateTime time = DateTime.Now;
            // 1.存图路径（不包含图片名称）
            string imageSavePath;

            // 2.拼接日期路径
            string nowDataTime = time.ToString("yyyyMMdd");
            imageSavePath = Path.Combine(param.SavePath, nowDataTime);

            // 3.图片名称
            string imageName = param.IsBarCode ? param.Barcode : time.ToString("yyyy-MM-dd HH-mm-ss");
            if (param.NeedCompress)
                imageName += "(压缩图)";

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
                if (param.ResultViewData == null)
                    throw new Exception($"无法获取/解析AI检测结果！");


                string okNg = param.ResultViewData.IsAllOk ? "OK" : "NG";
                imageSavePath = Path.Combine(imageSavePath, okNg);

                if (!param.ResultViewData.IsAllOk)
                {
                    foreach (var res in param.ResultViewData.SingleRowDataList)
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
                SaveImageTask saveImageTask = await Task.Run(() =>
                {
                    return QueueImageForSave(param.Image, path, imageName, param.NeedCompress, param.CompressValue);
                });
                EnqueueImage(saveImageTask);
            }
        }

        /// <summary>
        /// 生成任务
        /// </summary>
        /// <param name="image"></param>
        /// <param name="savePath"></param>
        /// <param name="imageName"></param>
        /// <param name="needCompress"></param>
        /// <param name="compressValue"></param>
        /// <returns></returns>
        private SaveImageTask QueueImageForSave(Bitmap image, string savePath, string imageName, bool needCompress, long compressValue = 100)
        {
            Directory.CreateDirectory(savePath);
            // 生成图片副本
            Bitmap clonedBitmap = (Bitmap)image.Clone();

            // 生成保存图片的绝对路径
            string fileName = GetFileName(savePath, imageName);

            // 创建保存任务
            SaveImageTask saveTask = new SaveImageTask
            {
                Image = clonedBitmap,
                Path = fileName,
                NeedCompress = needCompress,
                CompressValue = compressValue
            };

            return saveTask;
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

        //开始存图
        public void StartProcessing()
        {
            for (int i = 0; i < _workerCount; i++)
            {
                Task.Run(() => ProcessImages(_cancellationTokenSource.Token));
            }
        }

        /// <summary>
        /// 线程处理图片
        /// </summary>
        /// <param name="cancellationToken">令牌，标记线程是否取消执行</param>
        private void ProcessImages(CancellationToken cancellationToken)
        {
            //_imageQueue.GetConsumingEnumerable(cancellationToken):按顺序消费数据，并且可以在取消请求时停止消费。(消费者消费数据)
            foreach (var saveImagedata in _imageQueue.GetConsumingEnumerable(cancellationToken))
            {
                try
                {
                    SaveWithCompress(saveImagedata.Image, saveImagedata.Path, saveImagedata.NeedCompress, saveImagedata.CompressValue);
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"Error processing image: {ex.Message}");
                }
                finally
                {
                    saveImagedata.Image.Dispose();
                }
            }
        }

        //添加任务
        public void EnqueueImage(SaveImageTask saveImagedata)
        {
            if (saveImagedata.Image == null) throw new ArgumentNullException(nameof(saveImagedata), "Cannot enqueue a null image.");
            //添加任务(生产者生产数据)
            _imageQueue.Add(saveImagedata);
        }

        // 保存图片并压缩
        private void SaveWithCompress(Bitmap bitmap, string imagePath, bool isCompress, long compressValue = 100)
        {
            try
            {
                if (isCompress)
                {
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compressValue);
                    ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
                    bitmap.Save(imagePath, jpegCodec, encoderParams);
                }
                else
                {
                    bitmap.Save(imagePath);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"Error processing image: {ex.Message}");
            }
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
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

        public void StopProcessing()
        {
            _imageQueue.CompleteAdding(); // 标记不再添加新的元素
            _cancellationTokenSource.Cancel(); // 取消所有任务
        }
    }
}
