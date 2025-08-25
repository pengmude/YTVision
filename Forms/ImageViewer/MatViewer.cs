using System;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using OpenCvSharp;

namespace TDJS_Vision.Forms.ImageViewer
{
    public partial class MatViewer : FormBase
    {
        public MatViewer(string name)
        {
            InitializeComponent();
            Text = name;
        }

        public static void ShowImage(string name, Mat mat)
        {
            var win = new MatViewer(name);
            win.SetImage(mat);
            win.ShowDialog();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
