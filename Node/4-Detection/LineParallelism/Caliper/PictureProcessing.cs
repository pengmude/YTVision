using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node._4_Detection.Caliper
{
    /// <summary>
    /// 图像处理工具类
    /// </summary>
    public static class PictureProcessing
    {
        #region 拟合直线

        /// <summary>
        /// 确定拟合直线起点和终点
        /// </summary>
        /// <param name="src"></param>
        /// <param name="points"></param>
        /// <param name="line"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public static void GetStartEndPoint(Mat src, List<Point> points, Line2D line, out Point p1, out Point p2)
        {
            if (Math.Abs(1 - line.Vx) < 0.0001)
            {
                // 水平线
                var sortedPoints = points.OrderBy(z => z.X).ToList();
                p1 = new Point(sortedPoints[0].X, line.Y1);
                p2 = new Point(sortedPoints[sortedPoints.Count - 1].X, line.Y1);
            }
            else if (Math.Abs(1 - line.Vy) < 0.0001)
            {
                // 垂直线
                var sortedPoints = points.OrderBy(z => z.Y).ToList();
                p1 = new Point(line.X1, sortedPoints[0].Y);
                p2 = new Point(line.X1, sortedPoints[sortedPoints.Count - 1].Y);
            }
            else
            {
                // 使用RANSAC拟合直线
                (double k1, double b1) = FitLineRANSAC(points);

                IOrderedEnumerable<Point> sortedPoints;
                if (k1 > 0)
                {
                    // 左上到右下
                    sortedPoints = points.OrderBy(z => z.Y).ThenBy(z => z.X);
                }
                else
                {
                    // 左下到右上
                    sortedPoints = points.OrderBy(z => z.X).ThenBy(z => z.Y);
                }

                p1 = GetCrossPoint(sortedPoints.First(), k1, b1);
                p2 = GetCrossPoint(sortedPoints.Last(), k1, b1);

                Cv2.Line(src, p1, sortedPoints.First(), Scalar.Yellow);
                Cv2.Line(src, p2, sortedPoints.Last(), Scalar.Yellow);
            }
        }

        /// <summary>
        /// 排除异常点
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<Point> RemoveOutliers(List<Point> points)
        {
            // 使用四分位数范围（IQR）方法排除异常点
            var xValues = points.Select(p => p.X).OrderBy(x => x).ToList();
            var yValues = points.Select(p => p.Y).OrderBy(y => y).ToList();

            int q1Index = xValues.Count / 4;
            int q3Index = q1Index * 3;

            double xIQR = xValues[q3Index] - xValues[q1Index];
            double yIQR = yValues[q3Index] - yValues[q1Index];

            double xLowerBound = xValues[q1Index] - 1.5 * xIQR;
            double xUpperBound = xValues[q3Index] + 1.5 * xIQR;
            double yLowerBound = yValues[q1Index] - 1.5 * yIQR;
            double yUpperBound = yValues[q3Index] + 1.5 * yIQR;

            return points.Where(p => p.X >= xLowerBound && p.X <= xUpperBound && p.Y >= yLowerBound && p.Y <= yUpperBound).ToList();
        }

        /// <summary>
        /// 返回直线的斜率 k 和截距 b
        /// </summary>
        /// <param name="points"></param>
        /// <param name="iterations"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static (double k, double b) FitLineRANSAC(List<Point> points, int iterations = 1000, double threshold = 2.0)
        {
            Random random = new Random();
            double bestK = 0, bestB = 0;
            int maxInliers = 0;

            for (int i = 0; i < iterations; i++)
            {
                // 随机选择两个点
                Point p1 = points[random.Next(points.Count)];
                Point p2 = points[random.Next(points.Count)];

                if (p1.X == p2.X) continue; // 避免除以零

                // 计算直线参数
                double k = (double)(p2.Y - p1.Y) / (p2.X - p1.X);
                double b = p1.Y - k * p1.X;

                // 计算内点数量
                int inliers = points.Count(p => Math.Abs(p.Y - (k * p.X + b)) < threshold);

                if (inliers > maxInliers)
                {
                    maxInliers = inliers;
                    bestK = k;
                    bestB = b;
                }
            }

            return (bestK, bestB);
        }


        /// <summary>
        /// 获取经过point的与直线(斜率为k1,截距为b1)的垂直直线的交点
        /// </summary>
        /// <param name="point"></param>
        /// <param name="k1">直线斜率</param>
        /// <param name="b1">直线截距</param>
        /// <returns></returns>
        private static Point GetCrossPoint(Point point, double k1, double b1)
        {
            //与原直线垂直的直线的斜率 。斜率相乘等于-1
            var k2 = -1.0D / k1;

            //与原直线垂直的直线的截距
            var b2 = point.Y - k2 * point.X;

            //交点
            Point crossPoint = new Point();
            crossPoint.X = (int)((b2 - b1) / (k1 - k2));
            crossPoint.Y = (int)((b2 * k1 - b1 * k2) / (k1 - k2));
            return crossPoint;
        }

        /// <summary>
        /// 拟合直线
        /// </summary>
        /// <param name="gFraph"></param>
        /// <param name="MasterMap2"></param>
        /// <param name="points"></param>
        /// <param name="line"></param>
        public static void FittingStraightLine(GFraph gFraph, Mat MasterMap2, List<Point> points, Line2D line)
        {
            PictureProcessing.GetStartEndPoint(MasterMap2, points, line, out Point startPoint, out Point endPoint);
            Cv2.Line(MasterMap2, startPoint, endPoint, Scalar.Red, lineType: LineTypes.AntiAlias);

            Point[] points1 = new Point[] { startPoint, endPoint };
            GFraph.LinePoint.Add(points1);

            //显示结果图像
            Cv2.ImShow("绘制的图像", MasterMap2);
        }

        #endregion

        /// <summary>
        /// 找边缘点(找一条边的边缘点)
        /// </summary>
        /// <param name="pixelDict"></param>
        /// <returns>返回找到的边缘点</returns>
        public static Point2d FindEdgePoint(Dictionary<Point, byte> pixelDict)
        {
            int maxGradient = 0;
            Point initialEdgePoint = new Point();

            var sortedPoints = pixelDict.Keys.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

            for (int i = 1; i < sortedPoints.Count; i++)
            {
                int gradient = Math.Abs(pixelDict[sortedPoints[i]] - pixelDict[sortedPoints[i - 1]]);

                if (gradient > maxGradient)
                {
                    maxGradient = gradient;
                    initialEdgePoint = sortedPoints[i];
                }
            }

            // 使用亚像素级边缘检测进行精确定位
            return RefineEdgeLocation(pixelDict, initialEdgePoint);
        }

        /// <summary>
        /// 亚像素级边缘检测进行边缘点的检测
        /// </summary>
        /// <param name="pixelDict"></param>
        /// <param name="initialEdgePoint"></param>
        /// <returns></returns>
        private static Point2d RefineEdgeLocation(Dictionary<Point, byte> pixelDict, Point initialEdgePoint)
        {
            var sortedPoints = pixelDict.Keys.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            int index = sortedPoints.IndexOf(initialEdgePoint);

            if (index > 0 && index < sortedPoints.Count - 1)
            {
                Point prev = sortedPoints[index - 1];
                Point next = sortedPoints[index + 1];

                double i1 = pixelDict[prev];
                double i2 = pixelDict[initialEdgePoint];
                double i3 = pixelDict[next];

                // 使用抛物线拟合进行亚像素级精确定位
                double offset = (i1 - i3) / (2 * (i1 - 2 * i2 + i3));

                return new Point2d(
                    initialEdgePoint.X + offset * (next.X - prev.X) / 2,
                    initialEdgePoint.Y + offset * (next.Y - prev.Y) / 2
                );
            }

            return new Point2d(initialEdgePoint.X, initialEdgePoint.Y);
        }

        /// <summary>
        /// 处理像素集合
        /// </summary>
        /// <param name="region"></param>
        public static void FitAndDrawLines(Mat MasterMap, GFraph gFraph)
        {
            Mat MasterMap2 = MasterMap.Clone();

            //存储所有边缘点
            List<Point> allEdgePoints = new List<Point>();

            //处理每一条直线，找到所有边缘点
            foreach (var item in gFraph.rectangles)
            {
                var lineInfo = item.lineInfo;
                var pixelDict = lineInfo.LinePixels[0];

                //找到边缘点
                Point edgePoint = new Point(PictureProcessing.FindEdgePoint(pixelDict).X, PictureProcessing.FindEdgePoint(pixelDict).Y);

                //将边缘点添加到列表中
                allEdgePoints.Add(edgePoint);

                //在原图上标记边缘点
                //Cv2.Circle(mat2, edgePoint, 3, new Scalar(0, 255, 0), -1);
            }

            if (allEdgePoints.Count < 20)
            {
                MessageBox.Show("识别直线失败");
                return;
            }

            gFraph.allEdgePoints = allEdgePoints;
        }

        /// <summary>
        /// 图像预处理
        /// </summary>
        /// <param name="colorMat"></param>
        /// <returns></returns>
        public static Mat ImagePreprocessing(Mat colorMat)
        {
            // 转换为灰度图
            Mat grayMat = new Mat();
            Cv2.CvtColor(colorMat, grayMat, ColorConversionCodes.BGR2GRAY);

            #region 拉普拉斯
            // 应用拉普拉斯算子进行高频增强
            Mat laplacianImage = new Mat();
            Cv2.Laplacian(grayMat, laplacianImage, MatType.CV_8U);
            #endregion

            return laplacianImage;
        }

        /// <summary>
        /// 获取直线上的所有像素值
        /// </summary>
        /// <param name="image"></param>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <returns></returns>
        public static Dictionary<Point, byte> GetLinePixels(Mat image, Point lineStart, Point lineEnd)
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
    }
}
