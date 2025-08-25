using Logger;
using Sunny.UI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static TDJS_Vision.Node._7_ResultProcessing.ImageSave.ImageQueueProcessor;

namespace TDJS_Vision.Node._7_ResultProcessing.ImageSave
{
    public class NodeImageSave : NodeBase
    {
        ImageQueueProcessor processor = new ImageQueueProcessor(4);

        public NodeImageSave(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType) 
        {
            ParamForm = new ParamFormImageSave();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultImageSave();;
            // 开始处理队列
            processor.StartProcessing();
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
                        base.CheckTokenCancel(token);

                        // 参数获取订阅控件的值
                        if (param.IsBarCode)
                        {
                            param.Barcode = form.GetBarCode();
                            if (param.Barcode.IsNullOrEmpty())
                                throw new Exception($"获取条码结果失败！");
                        }
                        if (param.NeedOkNg)
                            param.AlgorithmResult = form.GetAiResult();
                        param.Image = form.GetImage();

                        // 参数合法性判断
                        if (param.SavePath.IsNullOrEmpty())
                            throw new Exception($"存图路径未设置！");
                        if (param.Image == null)
                            throw new Exception($"订阅的图片对象为空！");

                        // 异步队列存图
                        await SaveImage(param);
                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        Result.RunTime = time;
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

        private async Task SaveImage(NodeParamSaveImage param)
        {
            DateTime time = DateTime.Now;
            // 1.存图路径（不包含图片名称）
            string imageSavePath;

            // 2.拼接日期路径
            string nowDataTime = time.ToString("yyyyMMdd");
            imageSavePath = Path.Combine(param.SavePath, nowDataTime);

            // 3.图片名称
            string imageName = param.IsBarCode ? param.Barcode : time.ToString("yyyy-MM-dd_HH-mm-ss-fff");
            if (param.NeedCompress)
                imageName += "-CAN"; // 压缩图


            // 4.是否区分早晚班
            if (param.IsDayNight)
            {
                string dayNight = IsDayWorking(time.TimeOfDay, param.DayDataTime, param.NightDataTime) ? "早班" : "晚班";
                imageSavePath = Path.Combine(imageSavePath, dayNight);
            }

            List<string> paths = new List<string>();

            // 5.是否区分OK/NG
            if (param.NeedOkNg)
            {
                if (param.AlgorithmResult == null)
                    throw new Exception($"无法获取/解析AI检测结果！");

                // 根据AI检测总结果判断创建OK/NG文件夹
                bool allAreOk = param.AlgorithmResult.DetectResults.Count > 0 && param.AlgorithmResult.DetectResults.All(pair => pair.Value.All(v => v.IsOk));
                string okNg = allAreOk ? "OK" : "NG";
                imageSavePath = Path.Combine(imageSavePath, okNg);

                //要保存什么图片
                switch (param.ImageTypeToSave)
                {
                    case ImageTypeToSave.OkAndNg:
                        if (allAreOk)
                        {
                            paths.Add(imageSavePath);
                        }
                        else
                        {
                            foreach (var pair in param.AlgorithmResult.DetectResults)
                            {
                                if (!pair.Value.TrueForAll(item => item.IsOk))
                                    paths.Add(Path.Combine(imageSavePath, pair.Key));
                            }
                        }
                        break;
                    case ImageTypeToSave.OnlyOk:
                        if (allAreOk)
                        {
                            paths.Add(imageSavePath);
                        }
                        break;
                    case ImageTypeToSave.OnlyNg:
                        if (!allAreOk)
                        {
                            foreach (var pair in param.AlgorithmResult.DetectResults)
                            {
                                if (!pair.Value.TrueForAll(item => item.IsOk))
                                    paths.Add(Path.Combine(imageSavePath, pair.Key));
                            }
                        }
                        break;
                    default:
                        break;
                }

                foreach (var path in paths)
                {
                    SaveImageTask saveImageTask = await Task.Run(() =>
                    {
                        return QueueImageForSave(param.Image, path, imageName, param.NeedCompress, param.CompressValue);
                    });
                    processor.EnqueueImage(saveImageTask);
                }
            }
            else
            {
                SaveImageTask saveImageTask = await Task.Run(() =>
                {
                    return QueueImageForSave(param.Image, imageSavePath, imageName, param.NeedCompress, param.CompressValue);
                });
                processor.EnqueueImage(saveImageTask);
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

            // 生成保存图片的绝对路径
            string fileName = GetFileName(savePath, imageName);

            // 创建保存任务
            SaveImageTask saveTask = new SaveImageTask
            {
                Image = image,
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
            string fullPath = Path.Combine(filePath, newFileName + ".jpg");
            if (File.Exists(fullPath))
            {
                int suffix = 1;
                do
                {
                    newFileName = $"{fileName}_{suffix}";
                    fullPath = Path.Combine(filePath, newFileName + ".jpg");
                    suffix++;
                }
                while (File.Exists(fullPath));
            }
            return fullPath;
        }
       
    }

    public class ImageQueueProcessor
    {
        private readonly BlockingCollection<SaveImageTask> _imageQueue = new BlockingCollection<SaveImageTask>();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly int _workerCount; // 工作线程的数量

        public struct SaveImageTask
        {
            public Bitmap Image { get; set; }
            public string Path { get; set; }
            public bool NeedCompress { get; set; } //是否需要压缩
            public long CompressValue { get; set; } //压缩阈值
        }

        public ImageQueueProcessor(int workerCount)
        {
            if (workerCount <= 0) throw new ArgumentOutOfRangeException(nameof(workerCount), "Worker count must be greater than zero.");
            _workerCount = workerCount;
        }

        //开始存图
        public void StartProcessing()
        {
            for (int i = 0; i < _workerCount; i++)
            {
                Task.Run(() => ProcessImages(_cancellationTokenSource.Token));
            }
        }

        //添加任务
        public void EnqueueImage(SaveImageTask saveImagedata)
        {
            if (saveImagedata.Image == null) throw new ArgumentNullException(nameof(saveImagedata), "Cannot enqueue a null image.");
            //添加任务(生产者生产数据)
            _imageQueue.Add(saveImagedata);
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
                    //处理图片(压缩图片)
                    SaveWithCompress(saveImagedata.Image, saveImagedata.Path, saveImagedata.NeedCompress, saveImagedata.CompressValue);
                }
                catch (OperationCanceledException)
                {
                    // 如果取消了任务，则忽略这个异常
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"压缩图像失败: {ex.Message}");
                }
                finally
                {
                    saveImagedata.Image.Dispose();
                }
            }
        }

        // 保存图片并压缩
        private void SaveWithCompress(Bitmap bitmap, string imagePath, bool isCompress, long compressValue = 100)
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
