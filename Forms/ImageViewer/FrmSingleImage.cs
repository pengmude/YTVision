using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using TDJS_Vision.Node._1_Acquisition.ImageSource;
using TDJS_Vision.Forms.SolRunParam;
using System;
using Logger;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace TDJS_Vision.Forms.ImageViewer
{
    public partial class FrmSingleImage : DockContent
    {
        public FrmSingleImage(string name)
        {
            InitializeComponent();
            this.Text = name;
            NodeImageShow.ImageShowChanged += NodeImageShow_ImageShowChanged;
            SolRunParamControl.ImageShowChanged += NodeImageShow_ImageShowChanged;
        }

        public static void ShowImage(string name, Mat mat)
        {
            var win = new FrmSingleImage(name);
            win.SetImage(mat);
            win.Show();
        }
        public void SetImage(Mat mat)
        {
            // 确保图像更新在 UI 线程中执行
            if (this.InvokeRequired)
            {
                this./*Begin*/Invoke(new MethodInvoker(() => { ytPictrueBox1.Image = BitmapConverter.ToBitmap(mat); }));
            }
            else
            {
                ytPictrueBox1.Image = BitmapConverter.ToBitmap(mat);
            }
        }

        private void NodeImageShow_ImageShowChanged(object sender, ImageShowPamra e)
        {
            if (Text != e.WinName)
                return;
            // 确保图像更新在 UI 线程中执行
            if (this.InvokeRequired)
            {
                this./*Begin*/Invoke(new MethodInvoker(() => { ytPictrueBox1.Image = e.Bitmap; }));
            }
            else
            {
                ytPictrueBox1.Image = e.Bitmap;
            }
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
