using Logger;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace YTVisionPro.Node._2_ImagePreprocessing.ImageRotate
{
    internal partial class NodeParamFormImageRotate : Form, INodeParamForm
    {
        private Mat originalImage;
        private float angle;
        private Process process;//所属流程
        private NodeBase node;//所属节点

        public NodeParamFormImageRotate(Process process, NodeBase nodeBase)
        {
            InitializeComponent();
            this.process = process;
            this.node = nodeBase;
        }

        public INodeParam Params { get; set; }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 反序列化恢复参数
        /// </summary>
        public void SetParam2Form()
        {
            if (Params is NodeParamImageRotate param)
            {
                nodeSubscription1.SetText(param.Text1, param.Text2);
                numericUpDown1.ValueChanged -= numericUpDown1_ValueChanged;
                numericUpDown1.Value = -(decimal)param.Angle;
                numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
                Hide();
            }
        }

        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            NodeParamImageRotate nodeParamImageRotate = new NodeParamImageRotate();
            nodeParamImageRotate.Text1 = nodeSubscription1.GetText1();
            nodeParamImageRotate.Text2 = nodeSubscription1.GetText2();
            nodeParamImageRotate.Angle = -float.Parse(numericUpDown1.Value.ToString());
            Params = nodeParamImageRotate;
            Hide();
        }

        /// <summary>
        /// 获取订阅的图像设置到显示控件中
        /// </summary>
        public Bitmap GetImage()
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = nodeSubscription1.GetValue<Bitmap>();
            }
            catch (Exception)
            {
                throw new Exception("订阅的图像为null！");
            }
            return bitmap;
        }

        /// <summary>
        /// 点击刷新图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                await process.RunForUpdateImages(node);
                pictureBox1.Image = GetImage();
            }
            catch (Exception)
            {
                LogHelper.AddLog(MsgLevel.Exception, "刷新图像失败！请检查是否订阅正确的结果或前面节点运行存在异常！", true);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // 获取旋转角度（顺时针）
                angle = -(float)this.numericUpDown1.Value; // 负值表示顺时针旋转           
                ImageRotate(BitmapConverter.ToMat(GetImage()), angle);
            }
            catch (Exception)
            {
                LogHelper.AddLog(MsgLevel.Exception, "刷新图像失败！请检查是否订阅正确的结果或前面节点运行存在异常！", true);
            }
        }

        public Bitmap ImageRotate(Mat srcImg, float angle)
        {
            if (srcImg == null)
                throw new Exception("输入图像为null！");
            // 计算旋转矩阵
            var center = new Point2f(srcImg.Width / 2, srcImg.Height / 2);
            var rotationMatrix = Cv2.GetRotationMatrix2D(center, angle, 1.0);

            // 计算旋转后的图像的边界框大小
            double cos = Math.Abs(rotationMatrix.At<double>(0, 0));
            double sin = Math.Abs(rotationMatrix.At<double>(0, 1));
            int newWidth = (int)(srcImg.Height * sin + srcImg.Width * cos);
            int newHeight = (int)(srcImg.Width * sin + srcImg.Height * cos);

            // 调整旋转矩阵的平移部分，以确保图像居中
            rotationMatrix.Set(0, 2, rotationMatrix.At<double>(0, 2) + (newWidth - srcImg.Width) / 2);
            rotationMatrix.Set(1, 2, rotationMatrix.At<double>(1, 2) + (newHeight - srcImg.Height) / 2);

            // 创建目标图像矩阵，并应用旋转变换
            Mat rotatedImage = new Mat();
            Cv2.WarpAffine(srcImg, rotatedImage, rotationMatrix, new OpenCvSharp.Size(newWidth, newHeight));

            // 显示旋转后的图像
            pictureBox1.Image = BitmapConverter.ToBitmap(rotatedImage);
            
            return (Bitmap)pictureBox1.Image;
        }
    }
}
