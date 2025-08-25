using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._1_Acquisition.ImageSource;

namespace TDJS_Vision.Node._2_ImagePreprocessing.ImageSplit
{
    public partial class ParamFormImageSplit : FormBase, INodeParamForm
    {
        private readonly Process _process;//所属流程
        private readonly NodeBase _node;//所属节点
        private Bitmap _image;
        public INodeParam Params { get; set; }

        public ParamFormImageSplit(Process process, NodeBase nodeBase)
        {
            InitializeComponent();
            _process = process;
            _node = nodeBase;
        }

        public void SetParam2Form()
        {
            if (Params is NodeParamImageSplit param)
            {
                nodeSubscription1.SetText(param.Text1, param.Text2);
                numericUpDownRows.Value = param.Rows;
                numericUpDownCols.Value = param.Cols;
                Hide();
            }
        }

        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 获取订阅的图像设置到显示控件中
        /// </summary>
        public Bitmap GetImage()
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = nodeSubscription1.GetValue<OutputImage>().Bitmaps[0].ToBitmap();
            }
            catch (Exception)
            {
                throw new Exception("订阅的图像为null！");
            }
            return bitmap;
        }

        /// <summary>
        /// 刷新图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                await _process.RunForUpdateImages(_node);
                _image = GetImage();
                if (_image != null)
                {
                    pictureBoxShowImage.Image = _image;
                }
            }
            catch (Exception)
            {
                MessageBoxTD.Show("刷新图像失败！请检查是否订阅正确的结果或前面节点运行存在异常！");
            }
        }


        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            NodeParamImageSplit param = new NodeParamImageSplit();
            param.Text1 = nodeSubscription1.GetText1();
            param.Text2 = nodeSubscription1.GetText2();
            param.Rows = (int)numericUpDownRows.Value;
            param.Cols = (int)numericUpDownCols.Value;
            Params = param;

            Hide();
        }

        /// <summary>
        /// 绘制分割线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImplement_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap tmp = (Bitmap)_image.Clone();
                tmp = DrawSplitLines(tmp, (int)numericUpDownRows.Value, (int)numericUpDownCols.Value);
                pictureBoxShowImage.Image = tmp;
            }
            catch (Exception)
            {
                MessageBoxTD.Show("预览失败，请检查参数！");
            }
        }

        /// <summary>
        /// 在图像上画线，标识 m 行 n 列的分割位置。
        /// </summary>
        /// <param name="originalImage">原始图像。</param>
        /// <param name="rows">行数。</param>
        /// <param name="cols">列数。</param>
        /// <returns>包含分割线的图像。</returns>
        public static Bitmap DrawSplitLines(Bitmap originalImage, int rows, int cols)
        {
            if (originalImage == null)
                throw new ArgumentNullException(nameof(originalImage), "原始图像不能为空。");

            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("行数和列数必须是正整数。");

            // 创建一个新的 24 位 RGB 图像
            Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                // 将原始图像绘制到新图像上
                g.DrawImage(originalImage, Point.Empty);

                using (Pen pen = new Pen(Color.Cyan, 5))
                {
                    int baseHeight = originalImage.Height / rows;
                    int baseWidth = originalImage.Width / cols;

                    // 画水平线
                    for (int row = 1; row < rows; row++)
                    {
                        int y = row * baseHeight;
                        g.DrawLine(pen, 0, y, newImage.Width, y);
                    }

                    // 画垂直线
                    for (int col = 1; col < cols; col++)
                    {
                        int x = col * baseWidth;
                        g.DrawLine(pen, x, 0, x, newImage.Height);
                    }
                }
            }

            return newImage;
        }
    }
   
}
