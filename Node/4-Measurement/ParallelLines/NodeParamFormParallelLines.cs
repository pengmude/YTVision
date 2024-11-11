using Logger;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace YTVisionPro.Node._4_Measurement.ParallelLines
{
    internal partial class NodeParamFormParallelLines : Form, INodeParamForm
    {
        public NodeParamFormParallelLines()
        {
            InitializeComponent();
            textBox2.Enabled = false;
        }

        public INodeParam Params { get; set; }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
            nodeSubscription2.Init(node);
            nodeSubscription3.Init(node);
        }

        public LineSegmentPoint GetLinesSrc1()
        {
            try
            {
                return nodeSubscription1.GetValue<LineSegmentPoint>();
            }
            catch (Exception)
            {
                return default(LineSegmentPoint);
            }
        }

        public LineSegmentPoint GetLinesSrc2()
        {
            try
            {
                return nodeSubscription2.GetValue<LineSegmentPoint>();
            }
            catch (Exception ex)
            {
                return default(LineSegmentPoint);
            }
        }

        public Bitmap DrawLines(LineSegmentPoint line1, LineSegmentPoint line2, string isOk) 
        {
            try
            {
                Bitmap bitmap = nodeSubscription3.GetValue<Bitmap>();
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
                    throw new Exception("图像非单通道！");
                }

                // 绘制检测到的直线
                Mat result = image.Clone();
                Cv2.CvtColor(result, result, ColorConversionCodes.BayerBG2BGR);

                // 画笔颜色
                Scalar color;
                if(isOk == "平行")
                {
                    color = new Scalar(10, 255, 5);
                }
                else
                {
                    color = Scalar.Red;
                }

                // 绘制线段
                Cv2.Line(result, line1.P1, line1.P2, color, 5);
                Cv2.Line(result, line2.P1, line2.P2, color, 5);
                // 在线段的两端绘制红色端点
                int radius = 10; // 端点半径
                Cv2.Circle(result, line1.P1, radius, color, -1); // -1 表示填充圆
                Cv2.Circle(result, line1.P2, radius, color, -1);
                Cv2.Circle(result, line2.P1, radius, color, -1); // -1 表示填充圆
                Cv2.Circle(result, line2.P2, radius, color, -1);

                return BitmapConverter.ToBitmap(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static byte GetDarkColorValue(Random rand)
        {
            // 随机选择一个区间
            return rand.Next(2) == 0 ? (byte)rand.Next(0, 11) : (byte)rand.Next(245, 256);
        }

        /// <summary>
        /// 反序列化恢复参数
        /// </summary>
        public void SetParam2Form()
        {
            if(Params is NodeParamParallelLines param)
            {
                nodeSubscription1.SetText(param.Text1, param.Text2);
                nodeSubscription2.SetText(param.Text3, param.Text4);
                nodeSubscription3.SetText(param.Text5, param.Text6);
                textBox1.Text = param.MaxAngle.ToString();
                checkBox1.Checked = param.MaxAngleEnable;
                textBox2.Text = param.MaxDistanceDeviation.ToString();
                checkBox2.Checked = param.MaxDistanceDeviationEnable;
            }
        }
        /// <summary>
        /// 保存运行参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(double.Parse(textBox1.Text) < 0 || double.Parse(textBox2.Text) < 0)
                    throw new Exception("值不能小于0");
                NodeParamParallelLines nodeParamParallelLines = new NodeParamParallelLines();
                nodeParamParallelLines.Text1 = nodeSubscription1.GetText1();
                nodeParamParallelLines.Text2 = nodeSubscription1.GetText2();
                nodeParamParallelLines.Text3 = nodeSubscription2.GetText1();
                nodeParamParallelLines.Text4 = nodeSubscription2.GetText2();
                nodeParamParallelLines.Text5 = nodeSubscription3.GetText1();
                nodeParamParallelLines.Text6 = nodeSubscription3.GetText2();
                nodeParamParallelLines.MaxAngle = double.Parse(textBox1.Text);
                nodeParamParallelLines.MaxAngleEnable = checkBox1.Checked;
                nodeParamParallelLines.MaxDistanceDeviation = double.Parse(textBox2.Text);
                nodeParamParallelLines.MaxDistanceDeviationEnable = checkBox2.Checked;
                Params = nodeParamParallelLines;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"参数设置有误，原因：{ex.Message}");
                return;
            }
            Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox2.Checked;
        }
    }
}
