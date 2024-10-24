using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using YTVisionPro.Node.AI.HTAI;
using YTVisionPro.Node.ImageSrc.Shot;
using YTVisionPro.Node.ImageSrc.ImageRead;
using YTVisionPro.Node.Tool.ImageShow;

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
            NodeImageShow.ImageShowChanged += NodeImageShow_ImageShowChanged;
            ParamFormShot.ImageShowChanged += NodeImageShow_ImageShowChanged;
        }

        private void NodeImageShow_ImageShowChanged(object sender, ImageShowPamra e)
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
        private void UpdatePictureBox(ImageShowPamra e)
        {
            if (Text == e.WinName)
                ytPictrueBox1.Image = e.Bitmap;
        }

        private void FrmSingleImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmImageViewer.CurWindowsNum--;
            e.Cancel = true;
            Hide();
        }
    }
    public class ImageShowPamra
    {
        public string WinName;
        public Bitmap Bitmap;
        public ImageShowPamra(string winname, Bitmap bitmap)
        {
            WinName = winname;
            Bitmap = bitmap;
        }
    }
}
