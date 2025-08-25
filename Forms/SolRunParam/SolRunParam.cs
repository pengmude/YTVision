using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logger;
using Newtonsoft.Json;
using TDJS_Vision.Device.Camera;
using TDJS_Vision.Forms.ImageViewer;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._1_Acquisition.ImageSource;
using TDJS_Vision.Node._3_Detection.TDAI;
using static OpenCvSharp.ML.DTrees;

namespace TDJS_Vision.Forms.SolRunParam
{
    public partial class SolRunParamControl : UserControl
    {
        Process process;//显示哪个流程的参数
        SolRunData solRunDataInit = new SolRunData();//界面的初始参数
        SolRunData solRunDataNew = new SolRunData();//界面的最新参数
        ICamera _camera = null;
        TriggerSource triggerSrc;
        NodeTDAI _nodeTDAI;//一条流程下的所有AI节点
        NodeImageSource _nodeSource;//图像源节点
        string _windowsName; // 图像显示窗口名
        EventHandler<Bitmap> preEventHandler = null;//前一个活动的相机图像发布事件处理器
        public static event EventHandler<ImageShowPamra> ImageShowChanged; // 发布图像到窗口显示
        public static event EventHandler RefreshParamView; // 通知节点参数界面刷新事件
        public SolRunParamControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        /// <summary>
        /// 使用流程初始化
        /// </summary>
        /// <param name="process"></param>
        public void Init(Process process)
        {
            try
            {
                this.process = process;
                SetEnable(false);
                foreach (var node in process.Nodes)
                {
                    //获取相机触发模式和曝光
                    if (node is NodeImageSource nodeSource && node.ParamForm.Params
                        is NodeParamImageSoucre nodeParam)
                    {
                        _nodeSource = nodeSource;
                        if (nodeParam != null && nodeParam.Camera != null)
                        {
                            // 订阅相机图像发布事件
                            _camera = nodeParam.Camera;

                            try
                            {
                                //获取参数
                                solRunDataInit.ExposureTime = nodeParam.Camera.GetExposureTime().CurValue.ToString();
                                var (intV, flloatV) = nodeParam.Camera.GetGain();
                                //float四舍五入到0位小数，海康官方使用float类型存储增益会丢失精度，如设置2会变成2.000……或1.999……
                                var gain = intV == null ? (float)Math.Round(flloatV.CurValue, 0) : intV.CurValue;
                                solRunDataInit.Gain = gain.ToString();
                                triggerSrc = _camera.GetTriggerSource();
                                //设置参数
                                textBoxExposureTime.Text = solRunDataInit.ExposureTime.ToString();
                                textBoxGain.Text = solRunDataInit.Gain;
                                uiSwitchContinue.Active = nodeParam.Camera.GetTriggerSource() == TriggerSource.Auto ? true : false;
                                //设置控件可用性
                                textBoxExposureTime.Enabled = true;
                                textBoxGain.Enabled = true;
                                uiSwitchContinue.Enabled = true;
                                buttonOnce.Enabled = true;
                            }
                            catch (Exception) { }
                        }
                    }
                    //获取AI参数
                    if (node is NodeTDAI nodeTDAI &&
                        node.ParamForm.Params is NodeParamTDAI paramTDAI)
                    {
                        _nodeTDAI = nodeTDAI;//临时保存AI节点
                        if (paramTDAI != null)
                        {
                            //获取参数
                            solRunDataInit.ScoreThreshold = paramTDAI.AIInputInfo.ScoreThreshold.ToString();
                            solRunDataInit.ScoreNMS = paramTDAI.AIInputInfo.ScoreNMS.ToString();
                            solRunDataInit.IsAutoLearn = paramTDAI.IsAutoStudy;
                            solRunDataInit.DetectItems = paramTDAI.AIInputInfo.DetectItems;
                            //设置参数
                            textBoxScoreThreshold.Text = solRunDataInit.ScoreThreshold.ToString();
                            textBoxScoreNMS.Text = solRunDataInit.ScoreNMS.ToString();
                            uiSwitch_Learning.Active = solRunDataInit.IsAutoLearn;
                            //设置控件可用性
                            textBoxScoreThreshold.Enabled = true;
                            textBoxScoreNMS.Enabled = true;
                            uiSwitch_Learning.Enabled = true;
                        }
                    }
                    if (node is NodeImageShow nodeShow)
                    {
                        if (nodeShow.ParamForm.Params is NodeParamImageShow showParam)
                        {
                            _windowsName = showParam.WindowName;
                        }
                    }
                }
                try
                {
                    LoadDetectItems(solRunDataInit.DetectItems);
                }
                catch (Exception) { }
                //深拷贝
                solRunDataNew = SolRunData.Copy(solRunDataInit);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 加载检测内容
        /// </summary>
        /// <param name="info"></param>
        private void LoadDetectItems(List<DetectItemInfo> infos)
        {
            myDataGridViewForm1.LoadConfig(infos);
        }

        /// <summary>
        /// 设置控件可用性
        /// </summary>
        /// <param name="enable"></param>
        private void SetEnable(bool enable)
        {
            uiSwitchContinue.Enabled = enable;
            uiSwitch_Learning.Enabled = enable;
            buttonOnce.Enabled = enable;
            textBoxExposureTime.Enabled = enable;
            textBoxGain.Enabled = enable;
            textBoxScoreThreshold.Enabled = enable;
            textBoxScoreNMS.Enabled = enable;

            buttonOnce.Enabled = enable;
            buttonSingleImg.Enabled = enable;
            buttonCatalogueImg.Enabled = enable;
        }
        /// <summary>
        /// 单图点检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSingleImg_Click(object sender, EventArgs e)
        {
            //实现方法;找到流程中的图像源节点，将图像路径参数设置为当前选择的图像路径
            //最后运行流程即可，并恢复原来的图像路径
            foreach (var node in process.Nodes)
            {
                if (node is NodeImageSource nodeImage)
                {
                    if (nodeImage.ParamForm.Params is NodeParamImageSoucre nodeParam)
                    {
                        var imgSrc = nodeParam.ImageSource;
                        nodeParam.ImageSource = "本地图像";
                        //打开文件对话框，选择图像文件
                        openFileDialog1.Filter = "bmp图像|*.bmp|所有文件|*.*";
                        openFileDialog1.Title = "选择图像文件";
                        string modelOldPath = nodeParam.ImagePath;
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            //设置当前图像路径
                            nodeParam.ImagePath = openFileDialog1.FileName;
                            //运行流程
                            process.Run(false);
                            //恢复原来的图像路径
                            nodeParam.ImagePath = modelOldPath;
                        }
                        nodeParam.ImageSource = imgSrc;
                        nodeImage.ParamForm.Params = nodeParam;
                    }
                }
            }
        }
        /// <summary>
        /// 多图点检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCatalogueImg_Click(object sender, EventArgs e)
        {
            //实现方法;找到流程中的图像源节点，将图像路径参数设置为当前选择的图像目录
            foreach (var node in process.Nodes)
            {
                if (node is NodeImageSource nodeImage)
                {
                    if (nodeImage.ParamForm.Params is NodeParamImageSoucre nodeParam)
                    {
                        folderBrowserDialog1.Description = "选择图像文件夹";

                        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        {
                            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                            {
                                MessageBoxTD.Show("文件夹路径不能为空");
                                return;
                            }
                            // 只获取扩展名为 .bmp 的文件
                            string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.bmp");
                            // 临时保存原目录
                            var imgFiles = new List<string>(nodeParam.ImagePaths);
                            var imgPath = nodeParam.ImagePath;
                            var imageSource = nodeParam.ImageSource;
                            // 设置当前运行参数
                            nodeParam.ImagePath = null;
                            nodeParam.ImagePaths = new List<string>(files);
                            nodeParam.IsAutoLoop = true;
                            nodeImage.LastIndex = 0;
                            nodeParam.ImageSource = "本地图像";
                            //运行流程
                            for (int i = 0; i < files.Length; i++)
                            {
                                process.Run(false);
                                MessageBoxTD.Show($"图像{files[nodeImage.LastIndex]}检测完毕！");
                            }
                            //恢复原来的图像路径
                            nodeParam.ImagePaths = imgFiles;
                            nodeParam.ImagePath = imgPath;
                            nodeImage.ParamForm.Params = nodeParam;
                            nodeParam.ImageSource = imageSource;
                            nodeImage.ParamForm.Params = nodeParam;
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 保存所有参数到对应节点中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveAllParam_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfigs();
                new YTMessageBox.YTMessageBox("当前参数已成功保存！").Show();
            }
            catch (Exception)
            {
                MessageBoxTD.Show("参数保存失败，请检查参数是否设置无误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SaveConfigs()
        {
            try
            {
                if(_nodeSource != null)
                {
                    if (_nodeSource.ParamForm.Params is NodeParamImageSoucre imageSrcParams && textBoxExposureTime.Enabled)
                    {
                        imageSrcParams.ExposureTime = double.Parse(textBoxExposureTime.Text);
                        imageSrcParams.Gain = double.Parse(textBoxGain.Text);
                    }
                }
                if(_nodeTDAI != null)
                {
                    if (_nodeTDAI.ParamForm.Params is NodeParamTDAI aiParams && textBoxScoreThreshold.Enabled)
                    {
                        aiParams.AIInputInfo.ScoreThreshold = float.Parse(solRunDataNew.ScoreThreshold);
                        aiParams.AIInputInfo.ScoreNMS = float.Parse(solRunDataNew.ScoreNMS);
                        aiParams.IsAutoStudy = solRunDataNew.IsAutoLearn;
                        // 也要更新AI节点的检测项配置
                        aiParams.AIInputInfo.DetectItems = solRunDataNew.DetectItems;
                        var jsonStr = JsonConvert.SerializeObject(aiParams.AIInputInfo, Formatting.Indented);
                        jsonStr = StringCipher.Encrypt(jsonStr); // 加密AI配置内容
                        File.WriteAllText(aiParams.ConfigPath, jsonStr);
                    }
                }
                if (_nodeSource == null && _nodeTDAI == null)
                    return;
                //2025年5月27日 节点参数有更新，应该触发节点参数界面刷新当前设置的值
                RefreshParamView?.Invoke(this, EventArgs.Empty);
                Solution.Instance.Save(Solution.Instance.SolFileName);
                LogHelper.AddLog(MsgLevel.Info, $"当前参数设置已经成功保存到方案文件！路径：{Solution.Instance.SolFileName}", true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        CancellationTokenSource tokenSource;
        /// <summary>
        /// 连续采图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private async void uiSwitchContinue_ValueChanged(object sender, bool value)
        {
            if (_camera == null)
                return;
            if (value)
            {
                if (_camera.GetTriggerMode())
                    _camera.SetTriggerMode(false);
                if (!_camera.GetGrabStatus())
                    _camera.StartGrabbing();

                tokenSource = new CancellationTokenSource();
                // 在后台线程中运行循环
                await Task.Run(async () =>
                {
                    try
                    {
                        while (true)
                        {
                            // 检查取消请求
                            tokenSource.Token.ThrowIfCancellationRequested();
                            var img = _camera.GetOneFrameImage();
                            ImageShowChanged?.Invoke(this, new ImageShowPamra(_windowsName, img));
                            // 可以加个短暂延时避免 CPU 占用过高（可选）
                            await Task.Delay(5, tokenSource.Token);
                        }
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception ex) { }
                }, tokenSource.Token);
            }
            else
            {
                // 停止采集
                tokenSource.Cancel();
                if (!_camera.GetGrabStatus())
                    _camera.StartGrabbing();
                if (!_camera.GetTriggerMode())
                    _camera.SetTriggerMode(true);
            }
        }

        /// <summary>
        /// 单次采图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOnce_Click(object sender, EventArgs e)
        {
            if (uiSwitchContinue.Active)
                uiSwitchContinue.Active = false;
            if (!_camera.GetTriggerMode())
                _camera.SetTriggerMode(true);
            if (!_camera.GetGrabStatus())
                _camera.StartGrabbing();
            _camera.SetTriggerSource(TriggerSource.SOFT);//设置为软触发
            _camera.GrabOne();//软触发一次
            var img = _camera.GetOneFrameImage();
            ImageShowChanged?.Invoke(this, new ImageShowPamra(_windowsName, img));
            _camera.SetTriggerSource(triggerSrc);//触发后恢复原来的触发源
        }

        /// <summary>
        /// 参数设置完成时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxExposureTime_Leave(object sender, EventArgs e)
        {
            if (sender is Control con)
            {
                switch (con.Name)
                {
                    case "textBoxExposureTime":
                        if (float.TryParse(textBoxExposureTime.Text, out float exposure))
                        {
                            solRunDataNew.ExposureTime = textBoxExposureTime.Text;
                            _camera?.SetExposureTime(float.Parse(textBoxExposureTime.Text));
                        }
                        break;
                    case "textBoxGain":
                        if (float.TryParse(textBoxGain.Text, out float gain))
                        {
                            solRunDataNew.Gain = textBoxGain.Text;
                            _camera?.SetGain(float.Parse(textBoxGain.Text));
                        }
                        break;
                    case "textBoxScoreThreshold":
                        if (double.TryParse(textBoxScoreThreshold.Text, out double scoreThreshold))
                        {
                            solRunDataNew.ScoreThreshold = textBoxScoreThreshold.Text;
                        }
                        break;
                    case "textBoxScoreNMS":
                        if (double.TryParse(textBoxScoreNMS.Text, out double scoreNMS))
                        {
                            solRunDataNew.ScoreNMS = textBoxScoreNMS.Text;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 一键学习
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void uiSwitch_Learning_ValueChanged(object sender, bool value)
        {
            solRunDataNew.IsAutoLearn = value;
        }
        /// <summary>
        /// 编辑完表格时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myDataGridViewForm1_Leave(object sender, EventArgs e)
        {
            var infos = myDataGridViewForm1.GetConfig();
            if(infos != null)
                solRunDataNew.DetectItems = infos;
        }
        /// <summary>
        /// SolRunParam数据类
        /// </summary>
        public class SolRunData
        {
            public SolRunData() { }
            public string ExposureTime { get; set; }
            public string Gain { get; set; }
            public bool IsAutoLearn { get; set; } // 是否一键学习
            public string ScoreThreshold { get; set; }
            public string ScoreNMS { get; set; }
            public List<DetectItemInfo> DetectItems { get; set; } = new List<DetectItemInfo>();
            public static SolRunData Copy(SolRunData src)
            {
                var newData = new SolRunData();
                newData.ExposureTime = src.ExposureTime;
                newData.Gain = src.Gain;
                newData.IsAutoLearn = src.IsAutoLearn;
                newData.ScoreThreshold = src.ScoreThreshold;
                newData.ScoreNMS = src.ScoreNMS;
                src.DetectItems.ForEach(item => newData.DetectItems.Add(item));
                return newData;
            }
        }
    }
}
