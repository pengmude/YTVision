using System.Drawing;
using System.Windows.Forms;

namespace YTVisionPro.Forms.测试窗口
{
    internal partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public void SetBitMap(Bitmap bitmap)
        {
            pictureBox1.Image = bitmap;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {

        }
    }
}
