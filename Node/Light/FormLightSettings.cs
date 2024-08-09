using System;
using System.Windows.Forms;
using YTVisionPro.Hardware.Light;

namespace YTVisionPro.Node.Light
{
    public partial class FormLightSettings : ParamSetFormBase
    {
        /// <summary>
        /// 参数改变事件，设置完参数后触发，给节点订阅
        /// </summary>
        public override event EventHandler<INodeParam> NodeParamChanged;

        public FormLightSettings(LightBrand lightBrand)
        {
            InitializeComponent();

            // 初始化光源列表,只显示添加的光源COM号且是对应传入的品牌的
            foreach (var dev in Solution.Instance.LightDevices)
            {
                if (dev is ILight light && lightBrand == light.Brand)
                {
                    comboBox1.Items.Add(light.PortName);
                }
            }
            if(comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// 滑动滑块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        /// <summary>
        /// 光源亮度值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value = int.Parse(textBox1.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("有效值为0-255");
                    return;
                }
                trackBar1.Value = value;

            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 点击保存当前参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //把设置好的参数传给光源节点NodeLight去更新结果
                NodeParamLight nodeParamLight = new NodeParamLight(comboBox1.Text, int.Parse(comboBox2.Text), trackBar1.Value);
                NodeParamChanged?.Invoke(this, nodeParamLight);
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败,请检查参数是否有误！");
            }
        }
    }
}
