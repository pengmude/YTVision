using Logger;
using Sunny.UI;
using System;
using System.Windows.Forms;
using YTVisionPro.Device.Light;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace YTVisionPro.Node.LightControl
{
    internal partial class ParamFormLight : Form, INodeParamForm
    {
        private string _nodeName;
        private Process _process;

        public INodeParam Params { get; set; }

        public ParamFormLight(string nodeName, Process process)
        {
            InitializeComponent();
            Shown += ParamFormLight_Shown1;
            InitLightComboBox();
            _nodeName = nodeName;
            _process = process;
        }

        private void ParamFormLight_Shown1(object sender, EventArgs e)
        {
            InitLightComboBox();
        }

        /// <summary>
        /// 初始化光源下拉框
        /// </summary>
        /// <param name="lightBrand"></param>
        private void InitLightComboBox()
        {
            string text = comboBox1.Text;

            comboBox1.Items.Clear();
            comboBox1.Items.Add("[未设置]");
            // 初始化光源列表,只显示添加的光源COM号且是对应传入的品牌的
            foreach (var light in Solution.Instance.LightDevices)
            {
                comboBox1.Items.Add(light.UserDefinedName);
            }
            int index = comboBox1.Items.IndexOf(text);
            if(index == -1)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = index;
        }

        /// <summary>
        /// 用于节点参数界面需要订阅结果的情况调用
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node) { }

        /// <summary>
        /// 点击保存当前参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.IsNullOrEmpty() || comboBox1.Text == "[未设置]")
            {
                LogHelper.AddLog(MsgLevel.Warn, $"节点[{_process.ProcessName}.{_nodeName}]光源未设置！", true);
                MessageBox.Show($"节点[{_process.ProcessName}.{_nodeName}]光源未设置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                //查找当前选择的光源
                ILight light = null;
                foreach (var lightTmp in Solution.Instance.LightDevices)
                {
                    if (lightTmp.UserDefinedName == comboBox1.Text)
                    {
                        light = lightTmp;
                        break;
                    }
                }
                NodeParamLight nodeParamLight = new NodeParamLight();
                nodeParamLight.Light = light;
                nodeParamLight.LightName = light.UserDefinedName;
                nodeParamLight.Brightness = Convert.ToInt32(numericUpDownValue.Value);
                nodeParamLight.Time = Convert.ToInt32(numericUpDownTime.Value);
                Params = nodeParamLight;
                
                Hide();
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);
                MessageBox.Show("保存失败,请检查参数是否有误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 窗口每次显示时都要刷新下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamFormLight_Shown(object sender, EventArgs e)
        {
            InitLightComboBox();
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamLight param)
            {
                int index = comboBox1.Items.IndexOf(param.LightName);
                comboBox1.SelectedIndex = index == -1 ? 0 : index;
                numericUpDownValue.Value = param.Brightness;
                numericUpDownTime.Value = param.Time;
                // 还原光源对象
                foreach (var light in Solution.Instance.LightDevices)
                {
                    if(light.UserDefinedName == param.LightName)
                    {
                        param.Light = light;
                        break;
                    }
                }
            }
        }
    }
}
