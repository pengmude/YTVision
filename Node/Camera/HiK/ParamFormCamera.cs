using Sunny.UI;
using System;
using System.Windows.Forms;
using YTVisionPro.Hardware.Camera;

namespace YTVisionPro.Node.Camera.HiK
{
    internal partial class ParamFormCamera : Form, INodeParamForm
    {
        public event EventHandler<INodeParam> OnNodeParamChange;
        public ParamFormCamera()
        {
            InitializeComponent();
            Shown += ParamFormCamera_Shown;
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
            foreach (var camera in Solution.Instance.CameraDevices)
            {
                comboBox3.Items.Add(camera.UserDefinedName);
            }
            if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
            //触发方式
            comboBox1.SelectedIndex = 0;
            //触发沿
            comboBox2.SelectedIndex = 0;
            //信号源plc
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                comboBox4.Items.Add(plc.UserDefinedName);
            }
            if(comboBox4.Items.Count > 0) comboBox4.SelectedIndex = 0;
        }

        /// <summary>
        /// 软硬触发切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(0 != comboBox1.SelectedIndex)
            {
                comboBox2.Enabled = true;
                comboBox4.Enabled = false;
                textBox4.Enabled = false;
            }
            else
            {
                comboBox2.Enabled = false;
                comboBox4.Enabled = true;
                textBox4.Enabled = true;
            }
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
            if (comboBox3.Items.Count == 0 || comboBox3.Text.IsNullOrEmpty())
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
            if(comboBox1.SelectedIndex == 0 && textBox4.Text.IsNullOrEmpty())
            {
                MessageBox.Show("软触发模式下必须设置触发信号地址！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #endregion

            NodeParamCamera nodeParamCamera = new NodeParamCamera();

            #region 参数赋值

            //通过遍历方案设备查找选择的相机
            foreach (var camera in Solution.Instance.CameraDevices)
            {
                if(camera.UserDefinedName == comboBox3.Text)
                {
                    nodeParamCamera.Camera = camera;
                }
            }

            //设置相机
            switch (comboBox1.Text)
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

            // 软触发的信号源plc
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                if (plc.UserDefinedName == comboBox3.Text)
                {
                    nodeParamCamera.Plc = plc;
                }
            }

            //软触发设置触发信号
            nodeParamCamera.TriggerSignal = textBox4.Text;

            //硬触发设置触发沿
            switch (comboBox2.Text)
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

            OnNodeParamChange?.Invoke(this, nodeParamCamera);
        }
    }
}
