using Logger;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDJS_Vision.Device.Camera;
using TDJS_Vision.Forms.SolRunParam;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Node._1_Acquisition.ImageSource
{
    public partial class ParamFormImageSource : FormBase, INodeParamForm
    {
        /// <summary>
        /// 窗口高度
        /// </summary>
        private int _formHeight;

        /// <summary>
        /// 节点参数
        /// </summary>
        public INodeParam Params { get; set; }

        private NodeBase _node;
        public ParamFormImageSource(NodeBase node)
        {
            InitializeComponent();
            // 获取最初窗口高度
            _formHeight = this.Height;
            _node = node;
            comboBoxImgSource.SelectedIndex = 0;
            SolRunParamControl.RefreshParamView += SolRunParamControl_RefreshParamView;
        }

        private void SolRunParamControl_RefreshParamView(object sender, EventArgs e)
        {
            if(Params is NodeParamImageSoucre paramSrc)
            {
                numericUpDownExposureTime.Value = (decimal)paramSrc.ExposureTime;
                numericUpDownGain.Value = (decimal)paramSrc.Gain;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public void SetParam2Form()
        {
            if (Params is NodeParamImageSoucre param)
            {
                comboBoxImgSource.Text = param.ImageSource;

                if (param.ImageSource == "本地图像")
                {
                    textBoxImgPath.Text = param.PathText;
                    checkBoxAuto.Checked = param.IsAutoLoop;
                    if (param.ImagePaths.Count != 0)
                    {
                        checkBoxAuto.Enabled = true;
                    }
                    // “选择目录”需要获取目录下所有图像文件
                    if (Directory.Exists(param.PathText))
                    {
                        var extensions = new[] { ".bmp", ".jpg", ".jpeg", ".png" };
                        var files = Directory.GetFiles(textBoxImgPath.Text)
                                             .Where(s => extensions.Any(e => s.EndsWith(e, StringComparison.OrdinalIgnoreCase)))
                                             .ToList();

                        param.ImagePaths = files;
                    }
                }
                else if (param.ImageSource == "相机")
                {
                    // 还原选中的相机 
                    comboBoxChoiceCamera.Items.Clear();
                    comboBoxChoiceCamera.Items.Add("[未设置]");
                    foreach (var camera in Solution.Instance.CameraDevices)
                    {
                        comboBoxChoiceCamera.Items.Add(camera.UserDefinedName);
                    }
                    int index = comboBoxChoiceCamera.Items.IndexOf(param.CameraName);
                    comboBoxChoiceCamera.SelectedIndex = index == -1 ? 0 : index;
                    // 还原选中的触发源
                    comboBoxTriggerMode.Text = param.TriggerSource.ToString();
                    // 硬触发沿
                    comboBoxTriggerEdge.Text = param.TriggerEdge.ToString();
                    //// 设置延迟、曝光、增益
                    numericUpDownTriggerDelay.Text = param.TriggerDelay.ToString();
                    numericUpDownExposureTime.Text = param.ExposureTime.ToString();
                    numericUpDownGain.Text = param.Gain.ToString();
                    // 是否频闪
                    int index1 = comboBoxStrobe.Items.IndexOf(param.IsEveryTime ? "是" : "否");
                    comboBoxStrobe.SelectedIndex = index1 == -1 ? 0 : index1;
                    // 还原节点使用的相机
                    foreach (var camera in Solution.Instance.CameraDevices)
                    {
                        if (camera.UserDefinedName == param.CameraName)
                        {
                            param.Camera = camera;
                            // 不是频闪应用可以在参数界面只设置一次,是频闪的话相机需要在节点运行时每次设置
                            if (!param.IsEveryTime)
                                SetCameraParams(param.Camera, param.TriggerSource, param.TriggerEdge, param.TriggerDelay
                                    , param.ExposureTime, param.Gain);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 节点订阅结果
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node){}

        /// <summary>
        /// 选择图像源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxImgSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 本地图像
            if (comboBoxImgSource.SelectedIndex == 0)
            {
                // 调整控件可视状态
                tableLayoutPanelChoiceImage.Visible = true;
                tableLayoutPanelCamera.Visible = false;
                // 设置窗口高度
                this.Height = _formHeight - tableLayoutPanelCamera.Height;
            }
            // 相机
            else if (comboBoxImgSource.SelectedIndex == 1)
            {
                tableLayoutPanelChoiceImage.Visible = false;
                tableLayoutPanelCamera.Visible = true;
                this.Height = _formHeight - tableLayoutPanelChoiceImage.Height;
                comboBoxTriggerMode.SelectedIndex = 0;
                comboBoxTriggerEdge.SelectedIndex = 0;
                comboBoxStrobe.SelectedIndex = 0;
                InitCameraList();
            }

        }

        #region 本地图像
        // 选择图像
        private void buttonChoiceImg_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "图片文件 (*.BMP, *.JPG, *.JPEG, *.PNG)|*.BMP;*.JPG;*.JPEG;*.PNG|所有文件 (*.*)|*.*";
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "请选择图片";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(openFileDialog1.FileName))
                {
                    MessageBoxTD.Show("文件夹路径不能为空");
                    return;
                }
                textBoxImgPath.Text = openFileDialog1.FileName;
                checkBoxAuto.Checked = false;
                checkBoxAuto.Enabled = false;
            }
        }
        // 选择文件夹
        private void buttonChoiceImageCatalog_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "选择文件夹";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                {
                    MessageBoxTD.Show("文件夹路径不能为空");
                    return;
                }
                textBoxImgPath.Text = folderBrowserDialog1.SelectedPath;
                checkBoxAuto.Enabled = true;
                ((NodeImageSource)_node).LastIndex = 0;
            }
        }
        #endregion

        #region 相机
        /// <summary>
        /// 初始化相机列表
        /// </summary>
        /// <param name="process"></param>
        /// <param name="node"></param>
        private void InitCameraList()
        {
            // 相机自定义名
            string text1 = comboBoxChoiceCamera.Text;
            comboBoxChoiceCamera.Items.Clear();
            comboBoxChoiceCamera.Items.Add("[未设置]");
            foreach (var camera in Solution.Instance.CameraDevices)
            {
                comboBoxChoiceCamera.Items.Add(camera.UserDefinedName);
            }
            int index1 = comboBoxChoiceCamera.Items.IndexOf(text1);
            if (index1 == -1)
                comboBoxChoiceCamera.SelectedIndex = 0;
            else
                comboBoxChoiceCamera.SelectedIndex = index1;
        }
        /// <summary>
        /// 软硬触发切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTriggerMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 软触发
            if (0 == comboBoxTriggerMode.SelectedIndex)
                comboBoxTriggerEdge.Enabled = false;
            // 硬触发（Line0-Line4）
            else
                comboBoxTriggerEdge.Enabled = true;
        }

        /// <summary>
        /// 相机参数设置
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="triggerSource"></param>
        /// <param name="triggerEdge"></param>
        /// <param name="delay"></param>
        /// <param name="exposureTime"></param>
        /// <param name="gain"></param>
        private void SetCameraParams(ICamera camera, TriggerSource triggerSource, TriggerEdge triggerEdge, int delay, double exposureTime, double gain)
        {
            camera.SetTriggerSource(triggerSource);     // 设置触发源
            camera.SetTriggerEdge(triggerEdge);         // 设置硬触发边沿
            if (triggerSource != TriggerSource.Auto)
                camera.SetTriggerMode(true);                // 设置触发模式（除了自动取流外均设置）
            camera.SetTriggerDelay(delay);              // 设置触发延迟
            camera.SetExposureTime(exposureTime);       // 设置曝光时间
            camera.SetGain(gain);                       // 设置增益
        }
        /// <summary>
        /// 选择相机改变事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxChoiceCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 获取对应相机曝光和增益参数
            foreach (var camera in Solution.Instance.CameraDevices)
            {
                if (camera.UserDefinedName == comboBoxChoiceCamera.Text)
                {
                    // 当前相机触发延迟
                    var _triggerDelay = camera.GetTriggerDelay();
                    numericUpDownTriggerDelay.Minimum = (decimal)_triggerDelay.Min;
                    numericUpDownTriggerDelay.Maximum = (decimal)_triggerDelay.Max;
                    labelTriggerDelay.Text = $"触发延迟(当前{_triggerDelay.CurValue})";

                    // 当前相机曝光
                    var _exposurTime = camera.GetExposureTime();
                    numericUpDownExposureTime.Minimum = (decimal)_exposurTime.Min;
                    numericUpDownExposureTime.Maximum = (decimal)_exposurTime.Max;
                    labelExposureTime.Text = $"曝光(当前{_exposurTime.CurValue})";

                    // 当前相机增益
                    var (gainInt, gainFloat) = camera.GetGain();
                    if (gainInt != null)
                    {
                        numericUpDownGain.Minimum = gainInt.Min;
                        numericUpDownGain.Maximum = gainInt.Max;
                        labelGain.Text = $"增益(当前{gainInt.CurValue})";
                    }
                    else
                    {
                        numericUpDownGain.Minimum = (decimal)gainFloat.Min;
                        numericUpDownGain.Maximum = (decimal)gainFloat.Max;
                        labelGain.Text = $"增益(当前{gainFloat.CurValue})";
                    }
                    return;
                }
            }
            labelTriggerDelay.Text = "触发延迟(us)";
            labelExposureTime.Text = "曝光(us)";
            labelGain.Text = "增益";
        }

        #endregion

        private bool SaveParams()
        {
            NodeParamImageSoucre _nodeParamImageSource = new NodeParamImageSoucre();
            _nodeParamImageSource.ImageSource = comboBoxImgSource.Text;
            if (comboBoxImgSource.SelectedIndex == 0)
            {
                if (string.IsNullOrEmpty(textBoxImgPath.Text))
                {
                    MessageBoxTD.Show("未选择图片");
                    LogHelper.AddLog(MsgLevel.Fatal, "未选择图片", true);
                    return false;
                }

                _nodeParamImageSource.PathText = textBoxImgPath.Text;
                // 判断是否为单张图片
                if (textBoxImgPath.Text.EndsWith(".bmp") || textBoxImgPath.Text.EndsWith(".jpg") 
                    || textBoxImgPath.Text.EndsWith(".jpeg") || textBoxImgPath.Text.EndsWith(".png")) 
                {
                    _nodeParamImageSource.ImagePath = this.textBoxImgPath.Text;
                }
                else
                {
                    var extensions = new[] { ".bmp", ".jpg", ".jpeg", ".png" }; // 你想要支持的所有扩展名
                    var files = Directory.GetFiles(textBoxImgPath.Text)
                                         .Where(s => extensions.Any(e => s.EndsWith(e, StringComparison.OrdinalIgnoreCase)))
                                         .ToList();

                    _nodeParamImageSource.ImagePaths = files;
                }

                _nodeParamImageSource.IsAutoLoop = checkBoxAuto.Checked;
            }
            else if (comboBoxImgSource.SelectedIndex == 1)
            {
                #region 参数合法校验

                int delay, exposureTime;
                float gain;
                if (comboBoxChoiceCamera.Text == "[未设置]" || comboBoxChoiceCamera.Text.IsNullOrEmpty())
                {
                    MessageBoxTD.Show("相机为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                delay = (int)numericUpDownTriggerDelay.Value;
                exposureTime = (int)numericUpDownExposureTime.Value;
                gain = (float)numericUpDownGain.Value;

                #endregion

                #region 参数赋值

                //通过遍历方案设备查找选择的相机
                foreach (var camera in Solution.Instance.CameraDevices)
                {
                    if (camera.UserDefinedName == comboBoxChoiceCamera.Text)
                    {
                        _nodeParamImageSource.Camera = camera;
                        break;
                    }
                }
                // 保存相机名称参与序列化
                _nodeParamImageSource.CameraName = comboBoxChoiceCamera.Text;

                //设置相机
                switch (comboBoxTriggerMode.Text)
                {
                    case "软触发":
                        _nodeParamImageSource.TriggerSource = TriggerSource.SOFT;
                        break;
                    case "Line0":
                        _nodeParamImageSource.TriggerSource = TriggerSource.LINE0;
                        break;
                    case "Line1":
                        _nodeParamImageSource.TriggerSource = TriggerSource.LINE1;
                        break;
                    case "Line2":
                        _nodeParamImageSource.TriggerSource = TriggerSource.LINE2;
                        break;
                    case "Line3":
                        _nodeParamImageSource.TriggerSource = TriggerSource.LINE3;
                        break;
                    case "Line4":
                        _nodeParamImageSource.TriggerSource = TriggerSource.LINE4;
                        break;
                }

                //硬触发设置触发沿
                switch (comboBoxTriggerEdge.Text)
                {
                    case "上升沿":
                        _nodeParamImageSource.TriggerEdge = TriggerEdge.Rising;
                        break;
                    case "下降沿":
                        _nodeParamImageSource.TriggerEdge = TriggerEdge.Falling;
                        break;
                    case "高电平":
                        _nodeParamImageSource.TriggerEdge = TriggerEdge.Hight;
                        break;
                    case "低电平":
                        _nodeParamImageSource.TriggerEdge = TriggerEdge.Low;
                        break;
                }

                //触发延迟
                _nodeParamImageSource.TriggerDelay = delay;
                //曝光设置
                _nodeParamImageSource.ExposureTime = exposureTime;
                //增益设置
                _nodeParamImageSource.Gain = gain;
                // 是否每次设置参数
                _nodeParamImageSource.IsEveryTime = comboBoxStrobe.Text == "否" ? false : true;

                #endregion

                // 不是频闪应用可以在参数界面只设置一次,是频闪的话相机需要在节点运行时每次设置
                if (!_nodeParamImageSource.IsEveryTime)
                    SetCameraParams(_nodeParamImageSource.Camera, _nodeParamImageSource.TriggerSource, _nodeParamImageSource.TriggerEdge, _nodeParamImageSource.TriggerDelay
                        , _nodeParamImageSource.ExposureTime, _nodeParamImageSource.Gain);

            }

            Params = _nodeParamImageSource;
            return true;
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(SaveParams())
                Hide();
        }

        private void ParamFormImageSource_Load(object sender, EventArgs e)
        {
            if(comboBoxImgSource.SelectedIndex == 1)
                InitCameraList();
        }
    }

    /// <summary>
    /// 硬触发结果
    /// </summary>
    public struct HardTriggerResult
    {
        public DateTime StartTime;
        public bool IsSuccess;
        public string CameraName;
        public Bitmap Bitmap;
        public HardTriggerResult(DateTime StartTime, bool IsSuccess, string cameraName, Bitmap bitmap)
        {
            this.StartTime = StartTime;
            this.IsSuccess = IsSuccess;
            this.CameraName = cameraName;
            this.Bitmap = bitmap;
        }
    }
}
