using System.Windows.Forms;

namespace YTVisionPro
{
    public partial class ProcessBar : Form
    {
        public ProcessBar()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(int value)
        {
            uiProcessBar1.Value = value;
        }

        public void SetTip(string text)
        {
            uiLabel1.Text = text;
        }
    }
}
