using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Logger;
using Point = OpenCvSharp.Point;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._1_Acquisition.ImageSource;

namespace TDJS_Vision.Node._3_Detection.FindLine
{
    public partial class NodeParamFormFindLine : FormBase, INodeParamForm
    {
        private Process process;//所属流程
        private NodeBase node;//所属节点
        private LineSelection curLineSelection; // 当前选择的直线
        public NodeParamFormFindLine(Process process, NodeBase nodeBase)
        {
            InitializeComponent();
            this.process = process;
            this.node = nodeBase;
            comboBox1.SelectedIndex = 0;
            imageROIEditControl1.SetROIType2Draw(Forms.ShapeDraw.ROIType.Rectangle);
            Shown += NodeParamFormFindLine_Shown;
            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip1.SetToolTip(label3, "减少噪点,仅限奇数");
            toolTip1.SetToolTip(label1, "越小边缘噪点越多");
            toolTip1.SetToolTip(label2, "越大边缘噪点越少");
            toolTip1.SetToolTip(label4, "启用时边缘检测更精准但会增加耗时");
            toolTip1.SetToolTip(label7, "直线特征越明显值越大");
            toolTip1.SetToolTip(label8, "小于设定值时直线被忽略");
            toolTip1.SetToolTip(label9, "大于设定值时直线被忽略");
        }

        private void NodeParamFormFindLine_Shown(object sender, EventArgs e)
        {
            UpdataImage();
        }

        public INodeParam Params { get; set; }

        public void SetParam2Form()
        {
            if(Params is NodeParamFindLine param)
            {
                nodeSubscription1.SetText(param.Text1, param.Text2);
                checkBoxMoreParams.Checked = param.MoreParamsEnable;
                textBoxBlurSize.Text = param.GaussianBlur.ToString();
                textBoxThreshold1.Text = param.Threshold1.ToString();
                textBoxThreshold2.Text = param.Threshold2.ToString();
                checkBoxUseL2.Checked = param.IsOpenL2;
                textBoxCount.Text = param.Count.ToString();
                textBoxMinLength.Text = param.MinLength.ToString();
                textBoxMaxDistance.Text = param.MaxDistance.ToString();
                switch (param.LineSelection)
                {
                    case LineSelection.Longest:
                        comboBox1.SelectedIndex = 0;
                        break;
                    case LineSelection.Shortest:
                        comboBox1.SelectedIndex = 1;
                        break;
                    case LineSelection.Topmost:
                        comboBox1.SelectedIndex = 2;
                        break;
                    case LineSelection.Bottommost:
                        comboBox1.SelectedIndex = 3;
                        break;
                    case LineSelection.Leftmost:
                        comboBox1.SelectedIndex = 4;
                        break;
                    case LineSelection.Rightmost:
                        comboBox1.SelectedIndex = 5;
                        break;
                    default:
                        break;
                }
                //还原ROI
                imageROIEditControl1.SetROIs(param.ROIs);
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
                bitmap = nodeSubscription1.GetValue<OutputImage>().Bitmaps[0].ToBitmap();
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
                DetectLine();
            }
            catch (Exception ex)
            {
                MessageBoxTD.Show($"检测异常：{ex.Message}");
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
                NodeParamFindLine nodeParamFindLine = new NodeParamFindLine();
                nodeParamFindLine.Text1 = nodeSubscription1.GetText1();
                nodeParamFindLine.Text2 = nodeSubscription1.GetText2();
                nodeParamFindLine.MoreParamsEnable = checkBoxMoreParams.Checked;
                nodeParamFindLine.GaussianBlur = int.Parse(textBoxBlurSize.Text);
                nodeParamFindLine.Threshold1 = double.Parse(textBoxThreshold1.Text);
                nodeParamFindLine.Threshold2 = double.Parse(textBoxThreshold2.Text);
                nodeParamFindLine.IsOpenL2 = checkBoxUseL2.Checked;
                nodeParamFindLine.Count = int.Parse(textBoxCount.Text);
                nodeParamFindLine.MinLength = int.Parse(textBoxMinLength.Text);
                nodeParamFindLine.MaxDistance = int.Parse(textBoxMaxDistance.Text);
                nodeParamFindLine.LineSelection = curLineSelection;
                nodeParamFindLine.ROIs = imageROIEditControl1.GetROIs();
                Params = nodeParamFindLine;
            }
            catch (Exception)
            {
                MessageBoxTD.Show("参数设置异常，请检查参数设置是否合理！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测直线
        /// </summary>
        public (LineSegmentPoint, Bitmap) DetectLine()
        {
            LineSegmentPoint line = new LineSegmentPoint();
            try
            {
                // 更新输入图像和获取ROI图像
                pictureBoxCanny.Image = null;
                pictureBoxResult1.Image = null;
                pictureBoxResult2.Image = null;
                UpdataImage();
                Mat bitmap = imageROIEditControl1.GetROIImages()[0];

                if (bitmap.Channels() != 1)
                    Cv2.CvtColor(bitmap, bitmap, ColorConversionCodes.BGR2GRAY);

                // 应用高斯模糊减少噪点
                Mat blurred = new Mat();
                Cv2.GaussianBlur(bitmap, blurred, new OpenCvSharp.Size(int.Parse(textBoxBlurSize.Text), int.Parse(textBoxBlurSize.Text)), 0);
                
                // 使用 Canny 边缘检测
                Mat edges = new Mat();
                Cv2.Canny(blurred, edges, double.Parse(textBoxThreshold1.Text), double.Parse(textBoxThreshold2.Text), 3, checkBoxUseL2.Checked);
                pictureBoxCanny.Image = BitmapConverter.ToBitmap(edges);

                //霍夫直线检测
                var lineSegmentPoints = Cv2.HoughLinesP(edges, 1.0, Math.PI / 180, int.Parse(textBoxCount.Text), double.Parse(textBoxMinLength.Text), 
                                        double.Parse(textBoxMaxDistance.Text)).ToList();

                #region 绘制霍夫直线检测出来的所有直线

                // 绘制检测到的直线
                Mat result = bitmap.Clone();
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


                int radius = 5; // 端点半径
                foreach (var points in lineSegmentPoints)
                {
                    // 绘制线段
                    Cv2.Line(result, points.P1, points.P2, randomColor, 2);
                    // 在线段的两端绘制红色端点
                    Cv2.Circle(result, points.P1, radius, Scalar.Red, -1); // -1 表示填充圆
                    Cv2.Circle(result, points.P2, radius, Scalar.Red, -1);
                }
                Cv2.PutText(result, $"Count:{lineSegmentPoints.Count}", new Point(40, 40), HersheyFonts.Italic, 1, Scalar.Red);
                pictureBoxResult1.Image = BitmapConverter.ToBitmap(result);

                #endregion

                #region 绘制筛选后的图像

                // 加上在原图的偏差值
                var point = imageROIEditControl1.GetImageROIRects()[0].Location;

                // 绘制直线
                Mat result1 = bitmap.Clone();
                Cv2.CvtColor(result1, result1, ColorConversionCodes.BayerBG2BGR);
                var singleLine = LineMerger.MergeLines(lineSegmentPoints, curLineSelection);// 合并重合度高的直线并且筛选对应输出的直线
                                // 绘制线段
                Cv2.Line(result1, singleLine.P1, singleLine.P2, randomColor, 2);
                Cv2.Circle(result1, singleLine.P1, radius, Scalar.Red, -1); // -1 表示填充圆
                Cv2.Circle(result1, singleLine.P2, radius, Scalar.Red, -1);
                Cv2.PutText(result1, $"Lenth:{LineMerger.PointDistance(singleLine.P1, singleLine.P2).ToString("F2")} px", new Point(40, 40), HersheyFonts.Italic, 1, Scalar.Red);
                pictureBoxResult2.Image = BitmapConverter.ToBitmap(result1);
                singleLine.Offset((int)point.X, (int)point.Y);
                line = singleLine;

                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"检测直线失败，原因：{ex.Message}", true);
                return (line, null);
            }
            return (line, (Bitmap)pictureBoxResult2.Image);
        }
        private static byte GetDarkColorValue(Random rand)
        {
            // 随机选择一个区间
            return rand.Next(2) == 0 ? (byte)rand.Next(0, 11) : (byte)rand.Next(245, 256);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            curLineSelection = comboBox1.SelectedIndex == 0 ? LineSelection.Longest :
                               comboBox1.SelectedIndex == 1 ? LineSelection.Shortest :
                               comboBox1.SelectedIndex == 2 ? LineSelection.Topmost :
                               comboBox1.SelectedIndex == 3 ? LineSelection.Bottommost :
                               comboBox1.SelectedIndex == 4 ? LineSelection.Leftmost : LineSelection.Rightmost;
            }
    }
}
