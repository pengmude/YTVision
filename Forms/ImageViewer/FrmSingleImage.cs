using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using YTVisionPro.Node.AI.HTAI;
using YTVisionPro.Node.ImageSrc.Shot;
using YTVisionPro.Node.ImageSrc.ImageRead;

namespace YTVisionPro.Forms.ImageViewer
{
    internal partial class FrmSingleImage : DockContent
    {
        public static int i = 0;
        public FrmSingleImage()
        {
            InitializeComponent();
            this.Text = $"图像窗口{++i}";
            NodeShot.ImageShowChanged += NodeImageShow_ImageShowChanged;
            NodeImageRead.ImageShowChanged += NodeImageShow_ImageShowChanged;
            NodeHTAI.ImageShowChanged += NodeImageShow_ImageShowChanged;
        }

        private void NodeImageShow_ImageShowChanged(object sender, Paramsa e)
        {
            // 确保图像更新在 UI 线程中执行
            if (this.InvokeRequired)
            {
                this./*Begin*/Invoke(new MethodInvoker(() => UpdatePictureBox(e)));
            }
            else
            {
                UpdatePictureBox(e);
            }
        }

        /// <summary>
        /// 更新 PictureBox 的方法
        /// </summary>
        /// <param name="e"></param>
        private void UpdatePictureBox(Paramsa e)
        {
            if (Text == e.Winname)
                ytPictrueBox1.Image = e.Bitmap;
        }

        private void FrmSingleImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmImageViewer.CurWindowsNum--;
            e.Cancel = true;
            Hide();
        }
    }
    public class Paramsa
    {
        public string Winname;
        public Bitmap Bitmap;
        public Paramsa(string winname, Bitmap bitmap)
        {
            Winname = winname;
            Bitmap = bitmap;
        }
    }
}
