using System;
using System.Windows.Forms;

namespace YTVisionPro.Forms.ProcessNew
{
    public partial class LightSettings : UserControl
    {
        public LightSettings()
        {
            InitializeComponent();
        }

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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }
    }
}
