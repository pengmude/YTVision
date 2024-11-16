using Logger;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Point = OpenCvSharp.Point;
using Sunny.UI;
using Size = OpenCvSharp.Size;
using System.Threading.Tasks;

namespace YTVisionPro.Node._5_Measurement.InjectionHoleMeasurement
{
    internal partial class NodeParamFormInjectionHole : Form, INodeParamForm
    {
        private Process process;//所属流程
        private NodeBase node;//所属节点
        private Mat srcMat = new Mat(); //原图

        public NodeParamFormInjectionHole(Process process, NodeBase nodeBase)
        {
            InitializeComponent();
            this.process = process;
            this.node = nodeBase;
            imageROIEditControl1.SetROIType2Draw(Forms.ShapeDraw.ROIType.Circle);
            Shown += OnShown;
        }

        /// <summary>
        /// 窗口显示时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShown(object sender, EventArgs e)
        {
            UpdateImage();
        }
        public INodeParam Params { get; set; }

        /// <summary>
        /// 节点反序列化需要执行
        /// </summary>
        public void SetParam2Form()
        {
            if (Params is NodeParamInjectionHole param)
            {
                nodeSubscription1.SetText(param.Text1, param.Text2);
                nodeSubscription2.SetText(param.Text3, param.Text4);
                checkBox1.Checked = param.UseTemplate;
                checkBoxMoreParams.Checked = param.MoreParamsEnable;
                textBoxOKMinR.Text = param.OKMinR.ToString();
                textBoxOKMaxR.Text = param.OKMaxR.ToString();
                textBoxBlurSize.Text = param.GaussianBlur.ToString();
                textBoxThreshold1.Text = param.Threshold1.ToString();
                textBoxThreshold2.Text = param.Threshold2.ToString();
                checkBoxUseL2.Checked = param.IsOpenL2;

                //还原ROI
                imageROIEditControl1.SetROI(param.ROI);

                // TODO: 修复必须得显示一下参数窗口再运行截取的图像才是正确的区域，
                // 原因未知，估计是和ROI管理类构造需要传入pictrueBox有关
                Show();
                Hide();
            }
        }

        /// <summary>
        /// 初始化订阅节点
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
            nodeSubscription2.Init(node);
        }

        /// <summary>
        /// 点击更多参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxMoreParams_CheckedChanged(object sender, EventArgs e)
        {
            textBoxBlurSize.Enabled = checkBoxMoreParams.Checked;
            textBoxThreshold1.Enabled = checkBoxMoreParams.Checked;
            textBoxThreshold2.Enabled = checkBoxMoreParams.Checked;
            checkBoxUseL2.Enabled = checkBoxMoreParams.Checked;
        }

        /// <summary>
        /// 点击刷新订阅图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button3_Click(object sender, EventArgs e)
        {
            await process.RunForUpdateImages(node);
            UpdateImage();
        }

        /// <summary>
        /// 刷新图像
        /// </summary>
        public void UpdateImage()
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = nodeSubscription1.GetValue<Bitmap>();
                srcMat =  BitmapConverter.ToMat(bitmap);

                // 将原始图像转换为灰度图像 (CV_8UC1)
                Cv2.CvtColor(srcMat, srcMat, ColorConversionCodes.BGR2GRAY);
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
                DetectInjectionHole();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"注液孔检测算法执行失败,原因:{ex.Message}");
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (SaveParams())
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
                NodeParamInjectionHole param = new NodeParamInjectionHole();
                param.Text1 = nodeSubscription1.GetText1();
                param.Text2 = nodeSubscription1.GetText2();
                param.Text3 = nodeSubscription2.GetText1();
                param.Text4 = nodeSubscription2.GetText2();
                param.UseTemplate = checkBox1.Checked;
                param.MoreParamsEnable = checkBoxMoreParams.Checked;
                param.OKMinR = double.Parse(textBoxOKMinR.Text);
                param.OKMaxR = double.Parse(textBoxOKMaxR.Text);
                param.GaussianBlur = int.Parse(textBoxBlurSize.Text);
                param.Threshold1 = double.Parse(textBoxThreshold1.Text);
                param.Threshold2 = double.Parse(textBoxThreshold2.Text);
                param.IsOpenL2 = checkBoxUseL2.Checked;
                param.ROI = imageROIEditControl1.GetROI();
                Params = param;
            }
            catch (Exception)
            {
                MessageBox.Show("参数设置异常，请检查参数设置是否合理！");
                return false;
            }
            return true;
        }

        public Rect UpdateLocation()
        {
            try
            {
                return nodeSubscription2.GetValue<Rect>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Point GetCenterPoint(Rect rect)
        {
            int centerX = rect.X + rect.Width / 2;
            int centerY = rect.Y + rect.Height / 2;
            return new Point(centerX, centerY);
        }

        public (CircleSegment, Bitmap) DetectInjectionHole()
        {
            CircleSegment circle = new CircleSegment();
            try
            {
                UpdateImage();

                // 是否启用模版定位注液孔位置
                Point center;
                if (checkBox1.Checked)
                {
                    var rect = UpdateLocation();

                    center = GetCenterPoint(rect);

                }
                else
                {
                    var cen = imageROIEditControl1.GetImageROIRect().Center();
                    center = new Point(cen.X, cen.Y);
                }

                // 裁剪待检测区域
                int radius3 = 300;
                Rect roi = new Rect(
                    Math.Max(0, center.X - radius3),     // 确保不超出图像左边界
                    Math.Max(0, center.Y - radius3),     // 确保不超出图像上边界
                    radius3 * 2,                         // 宽度
                    radius3 * 2                          // 高度
                );
                Mat croppedImage = new Mat(srcMat, roi);

                // 应用高斯模糊减少噪点
                Mat blurred = new Mat();
                Cv2.GaussianBlur(croppedImage, blurred, new Size(int.Parse(textBoxBlurSize.Text), int.Parse(textBoxBlurSize.Text)), 0);

                // 使用 Canny 边缘检测
                Mat edges = new Mat();
                Cv2.Canny(blurred, edges, double.Parse(textBoxThreshold1.Text), double.Parse(textBoxThreshold2.Text), 3, checkBoxUseL2.Checked);
                pictureBoxCanny.Image = BitmapConverter.ToBitmap(edges);

                // 转换颜色空间
                Mat result1 = croppedImage.Clone();
                Cv2.CvtColor(result1, result1, ColorConversionCodes.BayerBG2BGR);

                int radius = 20;
                int numPoints = 18;
                center = new Point(edges.Width / 2, edges.Height / 2);
                List<Point> circlePoints = GetCirclePoints(center.X, center.Y, radius, numPoints);
                List<Point2d> allEdgePoints = new List<Point2d>();

                // 并行处理
                Parallel.ForEach(circlePoints, point =>
                {
                    Point endPoint = GetLineEndPoint(center.X, center.Y, point.X, point.Y, edges.Width, edges.Height);
                    var linePixel = GetLinePixels(edges, new Point(center.X, center.Y), endPoint);
                    Point? firstEdgePoint = FindFirstEdgePoint(edges, center, endPoint);

                    if (firstEdgePoint.HasValue)
                    {
                        lock (allEdgePoints)
                        {
                            allEdgePoints.Add(firstEdgePoint.Value);
                        }
                    }
                });

                var (center2, radius2) = CircleLeastSquares(allEdgePoints);

                // 在结果图像上绘制拟合的圆
                Cv2.Circle(result1, (Point)center2, (int)radius2, new Scalar(0, 255, 0), 2);
                Cv2.PutText(result1, $"Radius:{radius2.ToString("F2")}px", new Point(40, 40), HersheyFonts.HersheyTriplex, 1, Scalar.Red);
                pictureBoxResult1.Image = BitmapConverter.ToBitmap(result1);
                circle = new CircleSegment(new Point2f((float)center2.X, (float)center2.Y), (float)radius2);
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"检测圆失败，原因：{ex.Message}", true);
                return (circle, null);
            }

            return (circle, (Bitmap)pictureBoxResult1.Image);
        }

        #region 计算边缘特征点

        /// <summary>
        /// 获取直线终点
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="numPoints"></param>
        /// <returns></returns>
        public static List<Point> GetCirclePoints(int centerX, int centerY, int radius, int numPoints)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < numPoints; i++)
            {
                double angle = 2 * Math.PI * i / numPoints; int x = centerX + (int)(radius * Math.Cos(angle));
                int y = centerY + (int)(radius * Math.Sin(angle)); points.Add(new Point(x, y));
            }
            return points;
        }

        /// <summary>
        /// 找直线终点(终点在图的边界上)
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <returns></returns>
        public static Point GetLineEndPoint(int centerX, int centerY, int pointX, int pointY, int imageWidth, int imageHeight)
        {
            double angle = Math.Atan2(pointY - centerY, pointX - centerX);
            double slope = Math.Tan(angle);

            Point endPoint;
            if (Math.Abs(slope) <= imageHeight / (double)imageWidth)
            {
                // 直线与左右边界相交
                endPoint = new Point(pointX > centerX ? imageWidth : 0, centerY + (int)(slope * (pointX > centerX ? (imageWidth - centerX) : -centerX)));
            }
            else
            {
                // 直线与上下边界相交
                endPoint = new Point(centerX + (int)((pointY > centerY ? (imageHeight - centerY) : -centerY) / slope), pointY > centerY ? imageHeight : 0);
            }

            return endPoint;
        }

        /// <summary>
        /// 获取直线上的像素
        /// </summary>
        /// <param name="image"></param>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <returns></returns>
        private Dictionary<Point, byte> GetLinePixels(Mat image, Point lineStart, Point lineEnd)
        {
            Dictionary<Point, byte> linePixels = new Dictionary<Point, byte>();
            // 使用 LineIterator 遍历直线上的所有像素
            LineIterator lineIterator = new LineIterator(image, lineStart, lineEnd, PixelConnectivity.Connectivity8);
            foreach (var _ in lineIterator)
            {
                // 获取当前像素的坐标
                Point pos = _.Pos;
                // 获取图像中对应坐标的灰度值 (灰度图像只有一个通道)
                byte pixelValue = image.At<byte>(pos.Y, pos.X);
                // 将该像素值及其坐标加入字典
                linePixels[pos] = pixelValue;
            }
            return linePixels; // 返回所有的像素值及其位置
        }

        /// <summary>
        /// 检查是否为边缘点
        /// </summary>
        /// <param name="edges"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsEdgePoint(Mat edges, int x, int y)
        {
            // 边界检查
            if (x < 0 || x >= edges.Width || y < 0 || y >= edges.Height)
                return false;

            // 对于二值化图像，白色像素值通常是255
            return edges.At<byte>(y, x) == 255;
        }

        /// <summary>
        /// 查找从起点到终点的第一个边缘点的方法
        /// </summary>
        /// <param name="edges"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private Point? FindFirstEdgePoint(Mat edges, Point startPoint, Point endPoint)
        {
            // 计算直线的单位向量
            double dx = endPoint.X - startPoint.X;
            double dy = endPoint.Y - startPoint.Y;
            double length = Math.Sqrt(dx * dx + dy * dy);

            // 单位化向量
            dx /= length;
            dy /= length;

            // 沿直线逐点检查
            for (double t = 0; t < length; t += 1.0)
            {
                int x = (int)(startPoint.X + t * dx);
                int y = (int)(startPoint.Y + t * dy);

                // 检查是否为边缘点
                if (IsEdgePoint(edges, x, y))
                {
                    return new Point(x, y);
                }
            }

            return null;
        }

        #endregion

        #region 拟合圆

        /// <summary>
        /// 调用其他方法计算并优化圆的圆心和半径
        /// </summary>
        /// <param name="allEdgePoints"></param>
        /// <returns></returns>
        public static (Point2d Center, double Radius) CircleLeastSquares(List<Point2d> allEdgePoints)
        {
            // 确保点数不少于3个，少于3个无法确定一个圆
            if (allEdgePoints.Count < 3)
                return (new Point2d(0, 0), 0);

            // 计算初始圆心和半径
            var (center, radius) = CalculateInitialCircle(allEdgePoints);

            // 优化圆心和半径
            (center, radius) = OptimizeCircle(allEdgePoints, center, radius);

            // 调整点集，使其更贴近初步计算的圆
            allEdgePoints = AdjustPoints(allEdgePoints, center, radius);

            // 返回优化后的圆心和半径，如果调整后的点数仍然不少于3个，则再优化一次
            return allEdgePoints.Count >= 3 ? OptimizeCircle(allEdgePoints, center, radius) : (center, radius);
        }

        /// <summary>
        /// 计算初始圆心和半径。
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static (Point2d, double) CalculateInitialCircle(List<Point2d> points)
        {
            int pt_len = points.Count;
            Mat A = new Mat(pt_len, 3, MatType.CV_64FC1);
            Mat b = new Mat(pt_len, 1, MatType.CV_64FC1);

            // 构建矩阵A和向量b，用于线性系统的求解
            for (int r = 0; r < pt_len; r++)
            {
                A.Set(r, 0, points[r].X * 2.0);
                A.Set(r, 1, points[r].Y * 2.0);
                A.Set(r, 2, 1.0);
                b.Set(r, 0, points[r].X * points[r].X + points[r].Y * points[r].Y);
            }

            // 求解线性系统得到x向量，包含圆心坐标和常数项
            Mat x = SolveLinearSystem(A, b);

            // 圆心坐标
            Point2d center = new Point2d(x.Get<double>(0, 0), x.Get<double>(1, 0));
            // 半径计算
            double radius = Math.Sqrt(center.X * center.X + center.Y * center.Y + x.Get<double>(2, 0));

            return (center, radius);
        }

        /// <summary>
        /// 求解线性系统，返回一个包含圆心坐标和常数项的向量。
        /// </summary>
        /// <param name="A"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static Mat SolveLinearSystem(Mat A, Mat b)
        {
            Mat A_Trans = A.T();
            Mat Inv_A = A_Trans * A;
            Cv2.Invert(Inv_A, Inv_A); // 矩阵求逆
            return Inv_A * A_Trans * b;
        }

        /// <summary>
        /// 优化圆的圆心和半径,通过迭代计算损失和梯度，调整圆心和半径，使点集尽可能接近圆
        /// </summary>
        /// <param name="points"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private static (Point2d, double) OptimizeCircle(List<Point2d> points, Point2d center, double radius)
        {
            const int lr = 1; // 学习率
            int iters = points.Count;

            for (int i = 0; i < iters; i++)
            {
                double[] losses = new double[points.Count];
                double loop_loss = CalculateLoss(points, center, radius, losses);

                double[] min_loss = new double[iters];
                min_loss[i] = loop_loss;
                if (i > 0 && min_loss[i] > min_loss[i - 1])
                    break; // 损失没有减少则跳出循环

                // 计算梯度
                (double gx, double gy, double gr) = CalculateGradients(points, center, radius, losses);

                // 根据梯度更新圆心坐标和半径
                center = new Point2d(center.X - (lr * gx), center.Y - (lr * gy));
                radius -= (lr * gr);
            }

            return (center, radius);
        }

        /// <summary>
        /// 计算损失值,它计算每个点到圆心的距离，并计算这些距离与半径的差值，返回损失值的累加和
        /// </summary>
        /// <param name="points"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="losses"></param>
        /// <returns></returns>
        private static double CalculateLoss(List<Point2d> points, Point2d center, double radius, double[] losses)
        {
            double loop_loss = 0;
            for (int j = 0; j < points.Count; j++)
            {
                // 计算每个点到圆心的距离
                double root_val = Math.Sqrt(Math.Pow(points[j].X - center.X, 2) + Math.Pow(points[j].Y - center.Y, 2));
                double loss = root_val - radius; // 损失值为距离减去半径
                losses[j] = loss;
                loop_loss += Math.Abs(loss); // 累加所有损失值的绝对值
            }
            return loop_loss;
        }

        /// <summary>
        /// 计算梯度, 它计算每个点的梯度，返回所有点的平均梯度值，用于更新圆心和半径
        /// </summary>
        /// <param name="points"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="losses"></param>
        /// <returns></returns>
        private static (double, double, double) CalculateGradients(List<Point2d> points, Point2d center, double radius, double[] losses)
        {
            double gx = 0, gy = 0, gr = 0;
            for (int j = 0; j < points.Count; j++)
            {
                // 计算每个点的梯度
                double root_val = Math.Sqrt(Math.Pow(points[j].X - center.X, 2) + Math.Pow(points[j].Y - center.Y, 2));
                double gxi = (center.X - points[j].X) / root_val;
                double gyi = (center.Y - points[j].Y) / root_val;
                double gri = -1;
                if (losses[j] < 0)
                {
                    gxi *= -1;
                    gyi *= -1;
                    gri = 1;
                }
                gx += gxi;
                gy += gyi;
                gr += gri;
            }
            // 返回平均梯度值
            return (gx / points.Count, gy / points.Count, gr / points.Count);
        }

        /// <summary>
        /// 调整点集,它根据计算出的初步圆心和半径，筛选出那些更贴近圆的点，返回调整后的点集。
        /// </summary>
        /// <param name="points"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private static List<Point2d> AdjustPoints(List<Point2d> points, Point2d center, double radius)
        {
            // 调整点集，使其更贴近初步计算的圆
            return points.Where(p =>
            {
                double dist = CalculateDistance(p, center);
                return dist >= radius * 0.90 && dist <= radius * 1.10;
            }).ToList();
        }

        /// <summary>
        /// 计算点到圆心的距离
        /// </summary>
        /// <param name="p"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        private static double CalculateDistance(Point2d p, Point2d center)
        {
            // 计算点到圆心的距离
            return Math.Sqrt(Math.Pow(p.X - center.X, 2) + Math.Pow(p.Y - center.Y, 2));
        }

        #endregion

        /// <summary>
        /// 是否勾选模版定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            nodeSubscription2.Enabled = checkBox1.Checked;
        }
    }
}
