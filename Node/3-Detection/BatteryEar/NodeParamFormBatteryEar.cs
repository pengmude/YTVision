using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using Point = OpenCvSharp.Point;
using TDJS_Vision.Forms.YTMessageBox;
using System.Collections.Generic;
using TDJS_Vision.Node._1_Acquisition.ImageSource;
using TDJS_Vision.Forms.ShapeDraw;
using Logger;
using System.Linq;
namespace TDJS_Vision.Node._3_Detection.BatteryEar
{
    public partial class NodeParamFormBatteryEar : FormBase, INodeParamForm
    {
        private Process process;//所属流程
        private NodeBase node;//所属节点
        private OutputImage outputImage; // 订阅的图像数据
        private Mat templateJier; // 临时存储极耳模板
        private Mat templateMark; // 临时存储Mark点模板
        private Mat src; // 原图


        public NodeParamFormBatteryEar(Process process, NodeBase nodeBase)
        {
            InitializeComponent();
            this.process = process;
            this.node = nodeBase;
            imageROIEditControl1.SetROIType2Draw(ROIType.Rectangle);
            templateJier = Cv2.ImRead("Template\\BatteryEar\\Template_JiEr.bmp", ImreadModes.Color);
            templateMark = Cv2.ImRead("Template\\BatteryEar\\Template_Mark.bmp", ImreadModes.Color);
            Shown += NodeParamFormBatteryEar_Shown;
        }

        private void NodeParamFormBatteryEar_Shown(object sender, EventArgs e)
        {
            InitDeviceComboBox();
        }
        /// <summary>
        /// 初始化Modbus下拉框
        /// </summary>
        private void InitDeviceComboBox()
        {
            string text1 = comboBoxDevice.Text;

            comboBoxDevice.Items.Clear();
            comboBoxDevice.Items.Add("[未设置]");
            // 初始化Modbus列表,只显示添加的Modbus用户自定义名称
            foreach (var device in Solution.Instance.AllDevices)
            {
                switch (device.DevType)
                {
                    case Device.DevType.PLC:
                    case Device.DevType.ModbusRTUPoll:
                    case Device.DevType.ModbusTcpPoll:
                        comboBoxDevice.Items.Add(device.UserDefinedName);
                        break;
                    default:
                        continue;
                }
            }
            int index1 = comboBoxDevice.Items.IndexOf(text1);
            comboBoxDevice.SelectedIndex = index1 == -1 ? 0 : index1;
        }

        public INodeParam Params { get; set; }

        /// <summary>
        /// 反序列化还原界面参数设置
        /// </summary>
        public void SetParam2Form()
        {
            if (Params is NodeParamBatteryEar param)
            {
                try
                {
                    nodeSubscription1.SetText(param.Text1, param.Text2);
                    imageROIEditControl1.SetROIs(param.ROIs);
                    textBoxFixtureWidthMM.Text = param.FixtureWidthMM.ToString();
                    labelPixNum.Text = param.FixtureWidthPix.ToString();
                    textBoxScale.Text = param.Scale.ToString();
                    textBoxScore.Text = param.Score.ToString();
                    textBoxDeltaMM.Text = param.DeltaMM.ToString();

                    // 通信
                    InitDeviceComboBox();
                    comboBoxDevice.SelectedItem = param.DeviceName;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 初始化订阅节点
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 刷新图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button3_Click(object sender, EventArgs e)
        {
            await process.RunForUpdateImages(node);
            UpdataImage();
        }

        /// <summary>
        /// 更新函数
        /// </summary>
        public void UpdataImage()
        {
            OutputImage ret = new OutputImage();
            try
            {
                outputImage = nodeSubscription1.GetValue<OutputImage>();
            }
            catch (Exception)
            {
                outputImage = new OutputImage();
            }
            src = outputImage.Bitmaps[0] == null ? src : outputImage.Bitmaps[0];
            imageROIEditControl1.SetImage(src.ToBitmap());
        }

        /// <summary>
        /// 点击执行锂电池极耳检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                var (vals, rects, lines, img) = CalculateJierToMarkVerticalDistancesOnOriginal();
                // 绘制图像
                for (int i = 0; i < rects.Count; ++i)
                    Cv2.Rectangle(img, rects[i], Scalar.Blue, 5);
                foreach (var line in lines)
                    Cv2.Line(img, line.P1, line.P2, Scalar.Red, 5);
                // 单位转换、补偿
                var deltaX = double.Parse(textBoxDeltaMM.Text);
                vals = vals.Select(x => x * double.Parse(textBoxScale.Text) + deltaX).ToList();
                Cv2.PutText(img, $"L1:{vals[0]:0.000}mm  L2:{vals[1]:0.000}mm  L3:{vals[2]:0.000}mm  L4:{vals[3]:0.000}mm", new Point(100, 100), HersheyFonts.Italic, 3, Scalar.Green, 3);
                imageROIEditControl1.SetImage(img.ToBitmap());
            }
            catch (Exception ex)
            {
                MessageBoxTD.Show($"锂电池极耳异常：原因：{ex.Message}");
            }
        }

        /// <summary>
        /// 解析ROI图像和位置，区分左右Mark点和极耳
        /// </summary>
        /// <param name="roiImages"></param>
        /// <param name="roiLocations"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public (List<(Mat, Rect)>, List<(Mat, Rect)>) ParseROI(List<Mat> roiImages, List<Rect> roiLocations)
        {
            if (roiImages.Count != roiLocations.Count || roiImages.Count != 6)
                throw new ArgumentException("确保Mark点和极耳总共6个ROI图像及其位置！");

            // 分别存储左、右mark点和极耳的列表
            var leftMarkEars = new List<(Mat, Rect)>();
            var rightMarkEars = new List<(Mat, Rect)>();

            // 按X坐标对ROI进行排序，以区分左右侧
            var sortedROIs = new List<(Mat, Rect, int)>(); // 添加索引以保留原始对应关系
            for (int i = 0; i < roiImages.Count; i++)
            {
                sortedROIs.Add((roiImages[i], roiLocations[i], i));
            }
            sortedROIs.Sort((a, b) => a.Item2.X.CompareTo(b.Item2.X)); // 根据X坐标升序排序

            foreach (var item in sortedROIs)
            {
                if (item.Item2.X < 1500) // 这里假设X=1500为左右侧的分界线，请根据实际情况调整
                {
                    // 左侧处理
                    leftMarkEars.Add((item.Item1, item.Item2));
                }
                else
                {
                    // 右侧处理
                    rightMarkEars.Add((item.Item1, item.Item2));
                }
            }

            // 对两侧分别按Y坐标排序，以确保从上到下的正确顺序
            leftMarkEars.Sort((a, b) => a.Item2.Y.CompareTo(b.Item2.Y));
            rightMarkEars.Sort((a, b) => a.Item2.Y.CompareTo(b.Item2.Y));

            // 返回结果
            return (leftMarkEars, rightMarkEars);
        }

        public (List<double> distances, List<Rect> rects, List<LineSegmentPoint> lines, Mat resultImage) CalculateJierToMarkVerticalDistancesOnOriginal()
        {
            List<double> distances = new List<double>(new double[4] { 0, 0, 0, 0 }); // 初始化为0
            List<Rect> rects = new List<Rect>(); // 矩形框
            List<LineSegmentPoint> lines = new List<LineSegmentPoint>();
            Mat resultImage = src.Clone(); // 在原图副本上绘制
            try
            {
                var matchThreshold = double.Parse(textBoxScore.Text);
                var roiImages = imageROIEditControl1.GetROIImages();
                var roiLocations = imageROIEditControl1.GetImageROIRects();

                // 去掉最高的那个治具框
                roiImages.RemoveAll(m => m.Height > 1000);
                roiLocations.RemoveAll(r => r.Height > 1000);

                // 解析出左右的极耳和Mark点
                var (leftRois, rightRois) = ParseROI(roiImages, roiLocations);

                Mat templateJier = this.templateJier;
                Mat templateMark = this.templateMark;

                foreach (var (side, isLeft) in new[] { (leftRois, true), (rightRois, false) })
                {
                    if (side.Count != 3) continue; // 每边应有3个元素：mark点、上极耳、下极耳

                    // 获取mark点的位置
                    Point markCenter = GetTemplateCenter(resultImage, side[0].Item2, templateMark, matchThreshold);
                    rects.Add(new Rect(markCenter.X - templateMark.Width / 2, markCenter.Y - templateMark.Height / 2, templateMark.Width, templateMark.Height));

                    for (int i = 1; i < side.Count; i++)
                    {
                        Point jierCenter = GetTemplateCenter(resultImage, side[i].Item2, templateJier, matchThreshold);

                        if (jierCenter.X == -1 || markCenter.X == -1) continue; // 如果未能成功找到中心点

                        // 计算极耳中心于Mark点中心的垂直距离并保存
                        distances[(isLeft ? 0 : 2) + (i - 1)] = Math.Abs(jierCenter.Y - markCenter.Y);

                        rects.Add(new Rect(jierCenter.X - templateJier.Width / 2, jierCenter.Y - templateJier.Height / 2, templateJier.Width, templateJier.Height));
                        lines.Add(new LineSegmentPoint(jierCenter, markCenter));

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return (distances, rects, lines, resultImage);
        }

        private Point GetTemplateCenter(Mat originalImage, Rect roiRect, Mat template, double matchThreshold)
        {
            Mat roiImage = new Mat(originalImage, roiRect);
            Mat result = new Mat();
            Cv2.MatchTemplate(roiImage, template, result, TemplateMatchModes.CCoeffNormed);

            double minVal, maxVal;
            Point minLoc, maxLoc;
            Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

            if (maxVal >= matchThreshold)
            {
                return new Point(maxLoc.X + template.Width / 2 + roiRect.X, maxLoc.Y + template.Height / 2 + roiRect.Y);
            }
            return new Point(-1, -1); // 表示未找到
        }

        /// <summary>
        /// 点击保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                NodeParamBatteryEar param = new NodeParamBatteryEar();
                param.Text1 = nodeSubscription1.GetText1();
                param.Text2 = nodeSubscription1.GetText2();
                param.Score = double.Parse(textBoxScore.Text);
                param.ROIs = imageROIEditControl1.GetROIs();
                if (param.Score < 0 || param.Score > 1)
                    throw new Exception("分数设置范围[0,1]！");
                param.FixtureWidthMM = double.Parse(textBoxFixtureWidthMM.Text);
                param.FixtureWidthPix = double.Parse(labelPixNum.Text);
                param.Scale = double.Parse(textBoxScale.Text);
                param.DeltaMM = double.Parse(textBoxDeltaMM.Text);
                // 通信
                param.DeviceName = comboBoxDevice.SelectedItem.ToString();
                Params = param;
            }
            catch (Exception ex)
            {
                MessageBoxTD.Show($"参数设置异常，原因：{ex.Message}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取治具宽度像素数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetPixNum_Click(object sender, EventArgs e)
        {
            Mat srcCoppy = src.Clone();
            var roiImgs = imageROIEditControl1.GetROIImages();
            var rects = imageROIEditControl1.GetImageROIRects();

            if (rects.Count < 1)
            {
                LogHelper.AddLog(MsgLevel.Exception, "没有绘制任何ROI！", true);
                return;
            }

            // 找到最高的ROI图像和矩形，就是治具的宽度
            var tallestImage = roiImgs.Where(img => img != null).OrderByDescending(img => img.Height).FirstOrDefault();
            var tallestRect = rects.Where(rect => rect != null).OrderByDescending(rect => rect.Height).FirstOrDefault();

            Mat gray = new Mat();
            Cv2.CvtColor(tallestImage, gray, ColorConversionCodes.BGR2GRAY);
            // 二值化：使用固定阈值 + Otsu 自动优化
            Mat binary = new Mat();
            double threshold = Cv2.Threshold(gray, binary, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
            //MatViewer.ShowImage("二值化", binary);

            // 定义结构元素（例如 5x5 矩形）
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(11, 11));

            // 执行开运算：去除小的亮斑
            Mat cleaned = new Mat();
            Cv2.MorphologyEx(binary, cleaned, MorphTypes.Open, kernel);
            //MatViewer.ShowImage("开运算", cleaned);

            // 求得治具上下宽的点
            var (p1, p2) = MarkTransitionPoints(cleaned);
            // 加上治具矩形的偏移量
            p1 = new Point(p1.X + tallestRect.X, p1.Y + tallestRect.Y);
            p2 = new Point(p2.X + tallestRect.X, p2.Y + tallestRect.Y);

            // 绘制蓝色圆点（5像素直径）
            if (p1.Y != -1)
            {
                Cv2.Circle(srcCoppy, p1, 5, Scalar.Blue, -1); // 实心圆
            }

            if (p2.Y != -1)
            {
                Cv2.Circle(srcCoppy, p2, 5, Scalar.Blue, -1); // 实心圆
            }

            // 绘制绿色线段连接两点
            if (p1.Y != -1 && p2.Y != -1)
            {
                Cv2.Line(srcCoppy, p1, p2, Scalar.Green, 2, LineTypes.AntiAlias);
            }
            var length = Math.Abs(p1.Y - p2.Y); // 计算治具像素宽度
            imageROIEditControl1.SetImage(srcCoppy.ToBitmap());

            labelPixNum.Text = length.ToString();

            textBoxScale.Text = (double.Parse(textBoxFixtureWidthMM.Text) / length).ToString("F5"); // 更新缩放比例
        }
        /// <summary>
        /// 在二值化图像的竖直中线上找到第一次白→黑和最后一次黑→白的跳变点，并绘制标记和连线
        /// </summary>
        /// <param name="binaryImage">输入的二值化图像 (单通道, 0 或 255)</param>
        /// <returns>绘制后的图像</returns>
        public (Point, Point) MarkTransitionPoints(Mat binaryImage)
        {
            if (binaryImage.Channels() != 1)
                LogHelper.AddLog(MsgLevel.Exception, "输入图像必须是单通道二值图像。", true);

            // 确保输出是彩色图像以便绘制颜色
            Mat result = new Mat();
            if (binaryImage.Type() == MatType.CV_8UC1)
            {
                Cv2.CvtColor(binaryImage, result, ColorConversionCodes.GRAY2BGR);
            }
            else
            {
                result = binaryImage.Clone();
            }

            int height = binaryImage.Rows;
            int width = binaryImage.Cols;
            int centerX = width / 2; // 竖直中线

            int firstWhiteToBlackY = -1;  // 第一次 白 → 黑
            int lastBlackToWhiteY = -1;   // 最后一次 黑 → 白

            byte previousPixel = 255;

            // 从上到下扫描中线
            for (int y = 0; y < height; y++)
            {
                byte currentPixel = binaryImage.At<byte>(y, centerX);

                // 检测 白 → 黑 跳变（下降沿）
                if (previousPixel == 255 && currentPixel == 0 && firstWhiteToBlackY == -1)
                {
                    firstWhiteToBlackY = y;
                }

                // 检测 黑 → 白 跳变（上升沿），不断更新，保留最后一次
                if (previousPixel == 0 && currentPixel == 255)
                {
                    lastBlackToWhiteY = y;
                }

                previousPixel = currentPixel;
            }

            Point p1 = new Point(centerX, firstWhiteToBlackY);
            Point p2 = new Point(centerX, lastBlackToWhiteY);

            return (p1, p2);
        }
    }
}
