using Sunny.UI;
using System;
using System.Windows.Forms;
using YTVisionPro.Hardware.Camera;

namespace YTVisionPro.Node.Camera.HiK
{
    internal partial class ParamFormCamera : Form, INodeParamForm
    {
        public INodeParam Params { get; set; }
        public ParamFormCamera()
        {
            InitializeComponent();
            Shown += ParamFormCamera_Shown;
            //触发方式
            comboBoxType.SelectedIndex = 0;
            //触发沿
            comboBoxTriggerEdge.SelectedIndex = 0;
        }

        private void ParamFormCamera_Shown(object sender, EventArgs e)
        {
            InitNodeData();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="process"></param>
        /// <param name="node"></param>
        private void InitNodeData()
        {
            // 相机自定义名
            string text1 = comboBoxCamera.Text;
            comboBoxCamera.Items.Clear();
            comboBoxCamera.Items.Add("[未设置]");
            foreach (var camera in Solution.Instance.CameraDevices)
            {
                comboBoxCamera.Items.Add(camera.UserDefinedName);
            }
            int index1 = comboBoxCamera.Items.IndexOf(text1);
            if (index1 == -1)
                comboBoxCamera.SelectedIndex = 0;
            else
                comboBoxCamera.SelectedIndex = index1;
        }

        /// <summary>
        /// 用于节点参数界面需要订阅结果的情况调用
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node) { }

        /// <summary>
        /// 软硬触发切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 软触发
            if(0 == comboBoxType.SelectedIndex)
                comboBoxTriggerEdge.Enabled = false;
            // 硬触发（Line0-Line4）
            else
                comboBoxTriggerEdge.Enabled = true;
        }

        /// <summary>
        /// 保存运行参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            #region 参数合法校验

            int delay, exposureTime, gain;
            if (comboBoxCamera.Text == "[未设置]" || comboBoxCamera.Text.IsNullOrEmpty())
            {
                MessageBox.Show("相机为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                delay = int.Parse(textBox3.Text);
                exposureTime = int.Parse(textBox1.Text);
                gain = int.Parse(textBox2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("参数无法解析！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #endregion

            NodeParamCamera nodeParamCamera = new NodeParamCamera();

            #region 参数赋值

            //通过遍历方案设备查找选择的相机
            foreach (var camera in Solution.Instance.CameraDevices)
            {
                if(camera.UserDefinedName == comboBoxCamera.Text)
                {
                    nodeParamCamera.Camera = camera;
                }
            }

            //设置相机
            switch (comboBoxType.Text)
            {
                case "软触发":
                    nodeParamCamera.TriggerSource = TriggerSource.SOFT;
                    break;
                case "Line0":
                    nodeParamCamera.TriggerSource = TriggerSource.LINE0;
                    break;
                case "Line1":
                    nodeParamCamera.TriggerSource = TriggerSource.LINE1;
                    break;
                case "Line2":
                    nodeParamCamera.TriggerSource = TriggerSource.LINE2;
                    break;
                case "Line3":
                    nodeParamCamera.TriggerSource = TriggerSource.LINE3;
                    break;
                case "Line4":
                    nodeParamCamera.TriggerSource = TriggerSource.LINE4;
                    break;
            }

            //硬触发设置触发沿
            switch (comboBoxTriggerEdge.Text)
            {
                case "上升沿":
                    nodeParamCamera.TriggerEdge = TriggerEdge.Rising;
                    break;
                case "下降沿":
                    nodeParamCamera.TriggerEdge = TriggerEdge.Falling;
                    break;
                case "高电平":
                    nodeParamCamera.TriggerEdge = TriggerEdge.Hight;
                    break;
                case "低电平":
                    nodeParamCamera.TriggerEdge = TriggerEdge.Low;
                    break;
            }

            //触发延迟
            nodeParamCamera.TriggerDelay = delay;

            //曝光设置
            nodeParamCamera.ExposureTime = exposureTime;

            //增益设置
            nodeParamCamera.Gain = gain;

            #endregion

            Params = nodeParamCamera;
            
            Hide();
        }
        
        /// <summary>
        /// 选择相机改变事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 获取对应相机曝光和增益参数
            foreach (var camera in Solution.Instance.CameraDevices)
            {
                if (camera.UserDefinedName == comboBoxCamera.Text)
                {
                    textBox1.Text = camera.GetExposureTime().ToString();
                    textBox2.Text = camera.GetGain().ToString();
                }
            }
        }
    }
}
