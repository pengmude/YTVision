using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Logger;
using Point = OpenCvSharp.Point;

namespace YTVisionPro.Node._4_Detection.FindCircle
{
    internal partial class NodeParamFormFindCircle : Form, INodeParamForm
    {
        private Process process;//所属流程
        private NodeBase node;//所属节点
        private CircleSelection curLineSelection; // 当前选择的圆
        public NodeParamFormFindCircle(Process process, NodeBase nodeBase)
        {
            InitializeComponent();
            this.process = process;
            this.node = nodeBase;
            comboBox1.SelectedIndex = 0;
            imageROIEditControl1.SetROIType2Draw(Forms.ShapeDraw.ROIType.Circle);
            Shown += NodeParamFormFindLine_Shown;
        }

        private void NodeParamFormFindLine_Shown(object sender, EventArgs e)
        {
            UpdataImage();
        }

        public INodeParam Params { get; set; }

        public void SetParam2Form()
        {
            if(Params is NodeParamFindCircle param)
            {
                nodeSubscription1.SetText(param.Text1, param.Text2);
                checkBoxMoreParams.Checked = param.MoreParamsEnable;
                checkBoxOKEnable.Checked = param.OKEnable;
                textBoxOKMinR.Text = param.OKMinR.ToString();
                textBoxOKMaxR.Text = param.OKMaxR.ToString();
                textBoxBlurSize.Text = param.GaussianBlur.ToString();
                textBoxThreshold1.Text = param.Threshold1.ToString();
                textBoxThreshold2.Text = param.Threshold2.ToString();
                checkBoxUseL2.Checked = param.IsOpenL2;
                textBox1.Text = param.param1.ToString();
                textBox2.Text = param.param2.ToString();    
                textBoxCount.Text = param.Count.ToString();
                textBoxMinR.Text = param.MinLength.ToString();
                textBoxMaxR.Text = param.MaxDistance.ToString();
                switch (param.CircleSelection)
                {
                    case CircleSelection.Largest:
                        comboBox1.SelectedIndex = 0;
                        break;
                    case CircleSelection.Smallest:
                        comboBox1.SelectedIndex = 1;
                        break;
                    case CircleSelection.Topmost:
                        comboBox1.SelectedIndex = 2;
                        break;
                    case CircleSelection.Bottommost:
                        comboBox1.SelectedIndex = 3;
                        break;
                    case CircleSelection.Leftmost:
                        comboBox1.SelectedIndex = 4;
                        break;
                    case CircleSelection.Rightmost:
                        comboBox1.SelectedIndex = 5;
                        break;
                    default:
                        break;
                }
                //还原ROI
                imageROIEditControl1.SetROI(param.ROI);

                // TODO: 修复必须得显示一下参数窗口再运行截取的图像才是正确的区域，
                // 原因未知，估计是和ROI管理类构造需要传入pictrueBox有关
                Show();
                Hide();
            }
        }

        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 点击更多参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanel2.Visible = checkBoxMoreParams.Checked;
        }

        /// <summary>
        /// 点击刷新图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button3_Click(object sender, EventArgs e)
        {
            await process.RunForUpdateImages(node);
            UpdataImage();
        }

        /// <summary>
        /// 获取订阅的图像设置到显示控件中
        /// </summary>
        public void UpdataImage()
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = nodeSubscription1.GetValue<Bitmap>();
            }
            catch (Exception)
            {
                bitmap = null;
            }
            imageROIEditControl1.SetImage(bitmap);
        }

        /// <summary>
        /// 点击执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                DetectCircle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"检测异常：{ex.Message}");
                LogHelper.AddLog(MsgLevel.Exception, $"节点({node.ID}.{node.Name})检测异常，原因：{ex.Message}", true);
            }
        }

        /// <summary>
        /// 点击保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(SaveParams())
                Hide();
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <returns></returns>
        private bool SaveParams()
        {
            try
            {
                NodeParamFindCircle nodeParamFindCircle = new NodeParamFindCircle();
                nodeParamFindCircle.Text1 = nodeSubscription1.GetText1();
                nodeParamFindCircle.Text2 = nodeSubscription1.GetText2();
                nodeParamFindCircle.MoreParamsEnable = checkBoxMoreParams.Checked;
                nodeParamFindCircle.OKEnable = checkBoxOKEnable.Checked;
                nodeParamFindCircle.OKMinR = double.Parse(textBoxOKMinR.Text);
                nodeParamFindCircle.OKMaxR = double.Parse(textBoxOKMaxR.Text);
                nodeParamFindCircle.GaussianBlur = int.Parse(textBoxBlurSize.Text);
                nodeParamFindCircle.Threshold1 = double.Parse(textBoxThreshold1.Text);
                nodeParamFindCircle.Threshold2 = double.Parse(textBoxThreshold2.Text);
                nodeParamFindCircle.param1 = int.Parse(textBox1.Text);
                nodeParamFindCircle.param2 = int.Parse(textBox2.Text);
                nodeParamFindCircle.IsOpenL2 = checkBoxUseL2.Checked;
                nodeParamFindCircle.Count = int.Parse(textBoxCount.Text);
                nodeParamFindCircle.MinLength = int.Parse(textBoxMinR.Text);
                nodeParamFindCircle.MaxDistance = int.Parse(textBoxMaxR.Text);
                nodeParamFindCircle.CircleSelection = curLineSelection;
                nodeParamFindCircle.ROI = imageROIEditControl1.GetROI();
                Params = nodeParamFindCircle;
            }
            catch (Exception)
            {
                MessageBox.Show("参数设置异常，请检查参数设置是否合理！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测圆
        /// </summary>
        public (CircleSegment, Bitmap) DetectCircle()
        {
            CircleSegment Circle = new CircleSegment();
            try
            {
                // 更新输入图像和获取ROI图像
                pictureBoxCanny.Image = null;
                pictureBoxResult1.Image = null;
                pictureBoxResult2.Image = null;
                UpdataImage();
                Bitmap bitmap = imageROIEditControl1.GetROIImages();

                // 图像格式转换
                Mat image = new Mat();
                if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                {
                    image = BitmapConverter.ToMat(bitmap).CvtColor(ColorConversionCodes.BGR2GRAY);
                }
                else
                {
                    image = BitmapConverter.ToMat(bitmap).CvtColor(ColorConversionCodes.BayerBG2GRAY);
                }

                if (image.Channels() != 1)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"图像非单通道！", true);
                    return (Circle, null);
                }

                // 应用高斯模糊减少噪点
                Mat blurred = new Mat();
                Cv2.GaussianBlur(image, blurred, new OpenCvSharp.Size(int.Parse(textBoxBlurSize.Text), int.Parse(textBoxBlurSize.Text)), 0);
                
                // 使用 Canny 边缘检测
                Mat edges = new Mat();
                Cv2.Canny(blurred, edges, double.Parse(textBoxThreshold1.Text), double.Parse(textBoxThreshold2.Text), 3, checkBoxUseL2.Checked);
                pictureBoxCanny.Image = BitmapConverter.ToBitmap(edges);

                //霍夫圆检测
                // 检测圆
                /*
                 * 使用霍夫圆变换检测图像中的圆。参数解释：
                    image：输入图像，通常是灰度图像。
                    method：检测方法，通常使用 HoughModes.Gradient。
                    dp：累加器分辨率与输入图像分辨率的反比。例如，如果 dp=1，累加器和输入图像具有相同的分辨率；如果 dp=2，累加器的宽度和高度是输入图像的一半。
                    minDist：检测到的圆心之间的最小距离。如果设置得太小，多个邻近的圆可能会被错误地检测为一个圆。
                    param1：传递给 Canny 边缘检测器的高阈值，低阈值是高阈值的一半。
                    param2：累加器阈值，用于检测阶段。阈值越小，检测到的圆越多（但可能包括错误的圆）；阈值越大，检测到的圆越少，但更准确。
                    minRadius：检测圆的最小半径。
                    maxRadius：检测圆的最大半径。
                 */
                var circles = Cv2.HoughCircles(edges, HoughModes.Gradient, 1, int.Parse(textBoxCount.Text), param1: int.Parse(textBox1.Text), param2: int.Parse(textBox2.Text),
                                            minRadius: int.Parse(textBoxMinR.Text), maxRadius: int.Parse(textBoxMaxR.Text));

                #region 绘制霍夫圆检测出来的所有圆

                // 绘制检测到的直线
                Mat result = image.Clone();
                Cv2.CvtColor(result, result, ColorConversionCodes.BayerBG2BGR);

                // 生成随机颜色（排除红色）
                Scalar randomColor;
                Random rand = new Random();
                do
                {
                    byte r = GetDarkColorValue(rand);
                    byte g = GetDarkColorValue(rand);
                    byte b = GetDarkColorValue(rand);
                    randomColor = new Scalar(r, g, b);
                } while (randomColor == Scalar.Red);


                //int radius = 5; // 端点半径
                foreach (var circle in circles)
                {
                    var center = new Point((int)circle.Center.X, (int)circle.Center.Y);
                    var radius = (int)circle.Radius;
                    // 绘制圆
                    Cv2.Circle(result, center, radius, Scalar.Red, 2);
                    // 绘制圆心
                    Cv2.Circle(result, center, 3, Scalar.Red, -1);
                }
                Cv2.PutText(result, $"Count:{circles.Count()}", new Point(40, 40), HersheyFonts.Italic, 0.5, Scalar.Red);
                pictureBoxResult1.Image = BitmapConverter.ToBitmap(result);

                #endregion

                #region 绘制筛选后的图像

                // 加上在原图的偏差值
                var point = imageROIEditControl1.GetImageROIRect().Location;

                // 克隆图像
                Mat result1 = image.Clone();

                // 转换颜色空间
                Cv2.CvtColor(result1, result1, ColorConversionCodes.BayerBG2BGR);
                // 合并重合度高的圆并且筛选对应输出的圆
                var singleCircle = CircleMerger.MergeCircles(circles.ToList(), curLineSelection);
                // 绘制圆
                Cv2.Circle(result1, (Point)singleCircle.Center, (int)singleCircle.Radius, randomColor, 2); // 绘制圆，线条宽度为2
                // 绘制圆心
                Cv2.Circle(result1, (Point)singleCircle.Center, 5, Scalar.Red, -1); // 绘制圆心，半径为5，填充红色
                // 显示圆的半径
                Cv2.PutText(result1, $"Radius: {singleCircle.Radius.ToString("F2")} px", new Point(40, 40), HersheyFonts.Italic, 0.5, Scalar.Red);
                // 更新PictureBox的图像
                pictureBoxResult2.Image = BitmapConverter.ToBitmap(result1);
                // 返回合并后的圆
                Circle = singleCircle;
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"检测圆失败，原因：{ex.Message}", true);
                return (Circle, null);
            }
            return (Circle, (Bitmap)pictureBoxResult2.Image);
        }
        private static byte GetDarkColorValue(Random rand)
        {
            // 随机选择一个区间
            return rand.Next(2) == 0 ? (byte)rand.Next(0, 11) : (byte)rand.Next(245, 256);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            curLineSelection = comboBox1.SelectedIndex == 0 ? CircleSelection.Largest :
                               comboBox1.SelectedIndex == 1 ? CircleSelection.Smallest :
                               comboBox1.SelectedIndex == 2 ? CircleSelection.Topmost :
                               comboBox1.SelectedIndex == 3 ? CircleSelection.Bottommost :
                               comboBox1.SelectedIndex == 4 ? CircleSelection.Leftmost : CircleSelection.Rightmost;
        }

        private void checkBoxOKEnable_CheckedChanged(object sender, EventArgs e)
        {
            textBoxOKMinR.Enabled = checkBoxOKEnable.Checked;
            textBoxOKMaxR.Enabled = checkBoxOKEnable.Checked;
        }
    }
}
