using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logger;
using Newtonsoft.Json;
using TDJS_Vision.Forms.SolRunParam;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._1_Acquisition.ImageSource;
using TDJS_Vision.Node._3_Detection.TDAI.Parse;
using TDJS_Vision.Node._3_Detection.TDAI.Yolo8;

namespace TDJS_Vision.Node._3_Detection.TDAI
{
    public partial class ParamFormTDAI : FormBase, INodeParamForm
    {
        private NodeBase _node;
        AIInputInfo aIInputInfo;
        // 静态字段：确保所有实例共享同一个“锁”，控制 LoadModel 串行执行
        private static readonly SemaphoreSlim _loadModelLock = new SemaphoreSlim(1, 1);
        /// <summary>
        /// 节点参数
        /// </summary>
        public INodeParam Params { get; set; }

        public ParamFormTDAI()
        {
            InitializeComponent();
            SolRunParamControl.RefreshParamView += SolRunParamControl_RefreshParamView;
            ParseCommon.CloseAutoStudyEvent += ParamFormTDAI_CloseAutoStudyEvent;
        }
        /// <summary>
        /// 关闭对应自动学习事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamFormTDAI_CloseAutoStudyEvent(object sender, string e)
        {
            if (_node.NodeName == e)
            {
                if (Params is NodeParamTDAI param)
                {
                    param.IsAutoStudy = false; // 关闭自动学习
                    uiSwitch_Learning.Active = false; // 更新界面显示
                    LogHelper.AddLog(MsgLevel.Info, $"节点({param.NodeName})自动学习已关闭。", true);
                }
            }
            //// 使用 BeginInvoke 异步地执行UI操作
            //this.BeginInvoke(new MethodInvoker(() =>
            //{
            //    if (_node.NodeName == e)
            //    {
            //        if (Params is NodeParamTDAI param)
            //        {
            //            param.IsAutoStudy = false; // 关闭自动学习
            //            uiSwitch_Learning.Active = false; // 更新界面显示
            //            LogHelper.AddLog(MsgLevel.Info, $"节点({param.NodeName})自动学习已关闭。", true);
            //        }
            //    }
            //}));
        }

        /// <summary>
        /// 刷新从SolRunParamControl设置的参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SolRunParamControl_RefreshParamView(object sender, EventArgs e)
        {
            if(Params is NodeParamTDAI param)
            {
                uiSwitch_Learning.Active = param.IsAutoStudy;
            }
        }

        /// <summary>
        /// 用于节点参数界面需要订阅结果的情况调用
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
            _node = node;
        }

        /// <summary>
        /// 获取订阅的节点类型
        /// </summary>
        /// <returns></returns>
        private NodeType GetNodeType()
        {
            return nodeSubscription1.GetNodeType();
        }
        /// <summary>
        /// 获取图像
        /// </summary>
        /// <returns></returns>
        public OutputImage GetOutputImage()
        {
            OutputImage inputImage = null;
            var type = GetNodeType();
            try
            {
                if (type == NodeType.SharedVariable)
                {
                    // 获取共享变量中的图像
                    var obj = nodeSubscription1.GetValue<object>();
                    inputImage = (OutputImage)obj;
                }
                else
                {
                    // 获取图像源节点的图像
                    inputImage = nodeSubscription1.GetValue<OutputImage>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return inputImage;
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamTDAI param)
            {
                // 还原界面显示
                nodeSubscription1.SetText(param.Text1, param.Text2);
                textBoxConfigPath.Text = param.ConfigPath;
                SetComboBoxToEnumValue<ModelName>(comboBoxModelName, param.ModelName);
                uiSwitch_Learning.Active = param.IsAutoStudy;
                textBox_StudyNum.Text = param.StudyNum.ToString();
                textBox_studyPercentage.Text = param.StudyPercentage.ToString();
                uiSwitch_Convert.Active = param.NeedConvert;
                textBox_Scale.Text = param.Scale.ToString();
                if (!string.IsNullOrEmpty(param.ConfigPath) && File.Exists(param.ConfigPath))
                {
                    try
                    {
                        string text = File.ReadAllText(textBoxConfigPath.Text);
                        text = StringCipher.Decrypt(text);
                        // 反序列化配置
                        aIInputInfo = JsonConvert.DeserializeObject<AIInputInfo>(text);
                        // 还原该配置到节点参数
                        param.AIInputInfo = aIInputInfo;
                        // 保存静态全局检测项,用于AI结果发送信号
                        TDAI.DetectItemMap[$"{_node.ID}.{_node.NodeName}"] = aIInputInfo.DetectItems;
                    }
                    catch (Exception ex)
                    {
                        aIInputInfo = null;
                        LogHelper.AddLog(MsgLevel.Exception, $"AI配置文件解析失败！原因：{ex.Message}", true);
                    }
                }
                Task.Run(() => LoadModel(param));
            }
        }
        /// <summary>
        /// 设置ComboBox选中输入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox"></param>
        /// <param name="enumValue"></param>
        private void SetComboBoxToEnumValue<T>(ComboBox comboBox, T enumValue)
        {
            string enumString = enumValue.ToString();

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == enumString)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }

            // 如果没有找到匹配项，可以选择设置为 -1（不选中）
            comboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// 点击选择标签文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_chooseLabel_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "选择配置文件";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBoxConfigPath.Text = openFileDialog1.FileName;
                try
                {
                    string text = File.ReadAllText(textBoxConfigPath.Text);
                    text = StringCipher.Decrypt(text);
                    aIInputInfo = JsonConvert.DeserializeObject<AIInputInfo>(text);// 反序列化配置
                }
                catch (Exception ex)
                {
                    aIInputInfo = null;
                    LogHelper.AddLog(MsgLevel.Exception, $"AI配置文件解析失败！原因：{ex.Message}", true);
                }
            }
        }
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <returns></returns>
        private bool SaveParams()
        {
            try
            {
                if (string.IsNullOrEmpty(nodeSubscription1.GetText1())
                || string.IsNullOrEmpty(nodeSubscription1.GetText2())
                || string.IsNullOrEmpty(textBoxConfigPath.Text)
                || string.IsNullOrEmpty(textBox_StudyNum.Text)
                || string.IsNullOrEmpty(textBox_Scale.Text)
                )
                {
                    throw new Exception();
                }
                if (aIInputInfo == null)
                    throw new Exception("配置文件解析失败！");
                NodeParamTDAI nodeParamTDAI = new NodeParamTDAI();
                nodeParamTDAI.Text1 = nodeSubscription1.GetText1();
                nodeParamTDAI.Text2 = nodeSubscription1.GetText2();
                nodeParamTDAI.ConfigPath = textBoxConfigPath.Text;
                nodeParamTDAI.AIInputInfo = aIInputInfo;
                nodeParamTDAI.IsAutoStudy = uiSwitch_Learning.Active;
                nodeParamTDAI.StudyNum = int.Parse(textBox_StudyNum.Text);
                nodeParamTDAI.StudyPercentage = float.Parse(textBox_studyPercentage.Text);
                nodeParamTDAI.NeedConvert = uiSwitch_Convert.Active;
                nodeParamTDAI.Scale = float.Parse(textBox_Scale.Text);
                nodeParamTDAI.NodeName = _node.NodeName;

                // 保存静态全局检测项,用于AI结果发送信号
                TDAI.DetectItemMap[$"{_node.ID}.{_node.NodeName}"] = aIInputInfo.DetectItems;

                // 设置模型解析的名称
                switch (comboBoxModelName.Text)
                {
                    case "超日线芯":
                        nodeParamTDAI.ModelName = ModelName.超日线芯;
                        break;
                    case "超日胶壳":
                        nodeParamTDAI.ModelName = ModelName.超日胶壳;
                        break;
                    case "超日连接器":
                        nodeParamTDAI.ModelName = ModelName.超日连接器;
                        break;
                    case "超日TypeC焊锡":
                        nodeParamTDAI.ModelName = ModelName.超日TypeC焊锡;
                        break;
                    case "中厚12类端子":
                        nodeParamTDAI.ModelName = ModelName.中厚12类端子;
                        break;
                    case "线芯截面":
                        nodeParamTDAI.ModelName = ModelName.线芯截面;
                        break;
                    case "刺破机":
                        nodeParamTDAI.ModelName = ModelName.刺破机;
                        break;
                    case "刺破机颜色线序":
                        nodeParamTDAI.ModelName = ModelName.刺破机颜色线序;
                        break;
                    default:
                        throw new Exception($"未知的模型名称：{comboBoxModelName.Text}");
                }
                Params = nodeParamTDAI; 
                Task.Run(() => LoadModel(nodeParamTDAI));
            }
            catch (Exception e)
            {
                MessageBoxTD.Show($"参数设置异常，原因：{e.Message}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 加载模型
        /// </summary>
        private void LoadModel(NodeParamTDAI param)
        {
            try
            {
                LogHelper.AddLog(MsgLevel.Info, $"开始加载模型：{param.AIInputInfo.ModelInfo.ModelPath}", true);
                var modelInfo = param.AIInputInfo;

                // 如果是加密模型
                if (modelInfo.ModelInfo.IsEncrypted)
                {
                    if (modelInfo.ModelInfo.ModelType == ModelType.DET)
                    {
                        var detModel = new Yolo8Det();
                        detModel.Init(modelInfo.ModelInfo.ModelPath, modelInfo.ClassNames.ToArray(), modelInfo.ModelSize, modelInfo.ScoreThreshold, modelInfo.ScoreNMS);
                        param.Yolo8 = detModel;
                    }
                    else if (modelInfo.ModelInfo.ModelType == ModelType.OBB)
                    {
                        var obbModel = new Yolo8Obb();
                        obbModel.Init(modelInfo.ModelInfo.ModelPath, modelInfo.ClassNames.ToArray(), modelInfo.ModelSize, modelInfo.ScoreThreshold, modelInfo.ScoreNMS);
                        param.Yolo8 = obbModel;
                    }
                    else if (modelInfo.ModelInfo.ModelType == ModelType.SEG)
                    {
                        var segModel = new Yolo8Seg();
                        segModel.Init(modelInfo.ModelInfo.ModelPath, modelInfo.ClassNames.ToArray(), modelInfo.ModelSize, modelInfo.ScoreThreshold, modelInfo.ScoreNMS);
                        param.Yolo8 = segModel;
                    }
                    else if (modelInfo.ModelInfo.ModelType == ModelType.POSE)
                    {
                        var poseModel = new Yolo8Pose();
                        poseModel.Init(modelInfo.ModelInfo.ModelPath, modelInfo.ClassNames.ToArray(), modelInfo.ModelSize, modelInfo.ScoreThreshold, modelInfo.ScoreNMS, modelInfo.ModelInfo.KeyPointNum);
                        param.Yolo8 = poseModel;
                    }
                    else
                    {
                        throw new Exception($"模型类型({modelInfo.ModelInfo.ModelType})不支持！");
                    }
                }
                else
                    throw new NotImplementedException($"模型({modelInfo.ModelInfo.ModelPath})未加密，当前版本不支持加载未加密模型！");
                
                //统一注册模型句柄到模型管理
                ModelHandleManager.Add(param.Yolo8);

                LogHelper.AddLog(MsgLevel.Info, $"模型：{param.AIInputInfo.ModelInfo.ModelPath} 加载完成！", true);
            }
            catch (Exception e)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"加载模型: {param.AIInputInfo.ModelInfo.ModelPath} 失败！原因：{e.Message}", true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SaveParams())
                Hide();
        }
        /// <summary>
        /// 物理尺寸转换开关状态改变事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void uiSwitch_Convert_ValueChanged(object sender, bool value)
        {
            label_scale.Enabled = value;
            textBox_Scale.Enabled = value;
        }
        /// <summary>
        /// 一键学习开关状态改变事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void uiSwitch_Learning_ValueChanged(object sender, bool value)
        {
            label_studyNum.Enabled = value;
            textBox_StudyNum.Enabled = value;
            label_studyPercentage.Enabled = value;
            textBox_studyPercentage.Enabled = value;
        }
    }
}
