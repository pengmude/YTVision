using System;
using System.Windows.Forms;
using YTVisionPro.Hardware.Light;

namespace YTVisionPro.Forms.ProcessNew
{
    public partial class FormLightSettings : Form
    {
        /// <summary>
        /// 光源运行参数
        /// </summary>
        public RunParamsLight RunParams = new RunParamsLight();

        public FormLightSettings(LightBrand lightBrand)
        {
            InitializeComponent();

            // 初始化光源列表,只显示一种品牌的光源
            foreach (var dev in Solution.Instance.LightDevices)
            {
                if (dev is ILight light && lightBrand == light.Brand)
                {
                    comboBox1.Items.Add(light.PortName);
                }
            }
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
            RunParams.Brightness = trackBar1.Value;
            RunParams.PortName = comboBox1.Text.Contains("COM") ? comboBox1.Text : string.Empty;
        }
    }
}
