using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Drawing;
using System.Windows.Forms;
using Size = OpenCvSharp.Size;
using Point = OpenCvSharp.Point;
using TDJS_Vision.Forms.YTMessageBox;
using System.Collections.Generic;
using System.Linq;
using TDJS_Vision.Node._1_Acquisition.ImageSource;

namespace TDJS_Vision.Node._3_Detection.MatchTemplate
{
    public partial class NodeParamFormMatchTemplate : FormBase, INodeParamForm
    {
        private Process process;//所属流程
        private NodeBase node;//所属节点
        private static TemplateCreate templateCreate = null; // 模版创建对象
        private Bitmap src = null;  // 原图
        private Bitmap templateImage = null; // 模版图像
        private float scale = 8; // 默认缩小8倍
        private float score = 0; // 匹配得分
        private int resultNum = 1;// 匹配个数，默认一个


        public NodeParamFormMatchTemplate(Process process, NodeBase nodeBase)
        {
            InitializeComponent();
            this.process = process;
            this.node = nodeBase;
            imageROIEditControl1.SetROIType2Draw(Forms.ShapeDraw.ROIType.Rectangle);
            Shown += NodeParamFormQRCodeIdentification_Shown;
            toolTip1.SetToolTip(label2, "值取1-32，注意：值越大模版图像和原图尺寸越小，匹配耗时就越短，但容易匹配不到目标！");
            toolTip1.SetToolTip(label4, "作为判断匹配到目标的可靠性衡量，高于该设定分数值才认为是正确匹配，用作过滤低于该值的误匹配目标");
        }
        /// <summary>
        /// 窗口显示时更新订阅的图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeParamFormQRCodeIdentification_Shown(object sender, EventArgs e)
        {
            UpdataImage();
        }

        public INodeParam Params { get; set; }

        /// <summary>
        /// 反序列化还原界面参数设置
        /// </summary>
        public void SetParam2Form()
        {
            if (Params is NodeParamMatchTemplate param)
            {
                try
                {
                    textBoxModelPath.Text = param.TemplateFileName;
                    templateImage = new Bitmap(param.TemplateFileName);
                    nodeSubscription1.SetText(param.Text1, param.Text2);
                    textBoxScale.Text = param.Scale.ToString();
                    scale = param.Scale;
                    score = param.MinScore;
                    resultNum = param.ResultNum;
                    textBoxMinScore.Text = param.MinScore.ToString();
                    textBoxResultNum.Text = param.ResultNum.ToString();
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
            templateCreate = new TemplateCreate();
            templateCreate.SetNodeBelong(node);
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
            try
            {
                src = nodeSubscription1.GetValue<OutputImage>().Bitmaps[0].ToBitmap();
            }
            catch (Exception)
            {
                src = null;
            }
            imageROIEditControl1.SetImage(src);
        }

        /// <summary>
        /// 点击执行模板匹配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                scale = float.Parse(textBoxScale.Text);
                score = float.Parse(textBoxMinScore.Text);
                resultNum = int.Parse(textBoxResultNum.Text);
                MatchTemplate();
            }
            catch (Exception ex)
            {
                MessageBoxTD.Show($"模版匹配异常：原因：{ex.Message}");
            }
        }

        /// <summary>
        /// 模版匹配
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="templateImage"></param>
        /// <param name="scale"></param>
        /// <param name="show">是否更新显示，节点运行时设为false降低耗时</param>
        /// <returns></returns>
        public (Bitmap, List<Rect>, List<double>, bool) MatchTemplate(bool show = true)
        {
            try
            {
                if (resultNum <= 0)
                    throw new ArgumentException("resultNum 必须大于0", nameof(resultNum));

                Bitmap sourceImage = (Bitmap)src.Clone();
                Bitmap templateImage = this.templateImage;
                float scale = this.scale;
                double threshold = this.score / 100.0; // score 是百分制，转为 0~1

                if (sourceImage == null || templateImage == null)
                    throw new ArgumentException("源图像或模板图像不能为空");

                using (Mat sourceMat = BitmapToGrayMat(sourceImage))
                using (Mat templateMat = BitmapToGrayMat(templateImage))
                {
                    if (sourceMat.Empty() || templateMat.Empty())
                        throw new ArgumentException("无法将图像转换为灰度图像");

                    // 缩放图像
                    Size scaledSourceSize = new Size((int)(sourceMat.Cols / scale), (int)(sourceMat.Rows / scale));
                    Size scaledTemplateSize = new Size((int)(templateMat.Cols / scale), (int)(templateMat.Rows / scale));

                    using (Mat scaledSource = new Mat())
                    using (Mat scaledTemplate = new Mat())
                    {
                        Cv2.Resize(sourceMat, scaledSource, scaledSourceSize);
                        Cv2.Resize(templateMat, scaledTemplate, scaledTemplateSize);

                        // 执行模板匹配
                        using (Mat result = new Mat())
                        {
                            Cv2.MatchTemplate(scaledSource, scaledTemplate, result, TemplateMatchModes.CCoeffNormed);

                            // 匹配结果只需要输出一个结果，单独处理会快很多
                            if (resultNum == 1)
                            {
                                // 获取最佳匹配位置
                                Point matchLocation;
                                double minVal, maxVal;
                                Cv2.MinMaxLoc(result, out minVal, out maxVal, out _, out matchLocation);


                                // 计算未缩放前的矩形区域
                                int top = (int)(matchLocation.Y * scale);
                                int left = (int)(matchLocation.X * scale);
                                int width = (int)(scaledTemplate.Cols * scale);
                                int height = (int)(scaledTemplate.Rows * scale);
                                Rect rect = new Rect(left, top, width, height);

                                // 根据得分判断是否真正匹配到目标，而不是误匹配
                                using (Mat originalSourceMat = BitmapToColorMat(sourceImage))
                                {
                                    Scalar color = Scalar.Red;
                                    bool isOk = false;
                                    if (maxVal * 100 >= score)
                                    {
                                        isOk = true;
                                        color = Scalar.Green;
                                    }
                                    else
                                    {
                                        isOk = false;
                                        color = Scalar.Red;
                                    }
                                    // 在原始图像上绘制矩形
                                    Cv2.Rectangle(originalSourceMat, rect, color, 3);
                                    var bitmap = BitmapConverter.ToBitmap(originalSourceMat);
                                    if (show)
                                    {
                                        imageROIEditControl1.SetImage(bitmap);
                                    }
                                    return (bitmap, new List<Rect>() { rect }, new List<double>() { maxVal * 100 }, isOk);
                                }
                            }
                            // 多个结果输出要去重处理，慢一些
                            else
                            {
                                // 存储所有候选匹配点（位置 + 得分）
                                var candidates = new List<(Point location, double score)>();

                                // 手动遍历 Mat 的每个像素
                                for (int y = 0; y < result.Rows; y++)
                                {
                                    for (int x = 0; x < result.Cols; x++)
                                    {
                                        double scoreVal = result.At<float>(y, x); // 获取 (x,y) 位置的匹配得分
                                        if (scoreVal >= threshold)
                                        {
                                            candidates.Add((new Point(x, y), scoreVal));
                                        }
                                    }
                                }

                                // 替换开始：去重并取最多4个不重合高分点
                                var uniqueResults = new List<(Point location, double score)>();
                                int maxResults = Math.Min(resultNum, 4); // 防止越界
                                int radius = 50;

                                foreach (var (location, score) in candidates.OrderByDescending(c => c.score))
                                {
                                    bool isTooClose = uniqueResults.Any(exist =>
                                    {
                                        int dx = location.X - exist.location.X;
                                        int dy = location.Y - exist.location.Y;
                                        return Math.Sqrt(dx * dx + dy * dy) < radius;
                                    });

                                    if (!isTooClose)
                                    {
                                        uniqueResults.Add((location, score));
                                        if (uniqueResults.Count >= maxResults) break;
                                    }
                                }

                                // 转为 rects 和 scores
                                var rects = new List<Rect>();
                                var scores = new List<double>();
                                bool isOk = false;

                                foreach (var (location, score) in uniqueResults)
                                {
                                    int left = (int)(location.X * scale);
                                    int top = (int)(location.Y * scale);
                                    int width = (int)(scaledTemplate.Cols * scale);
                                    int height = (int)(scaledTemplate.Rows * scale);

                                    rects.Add(new Rect(left, top, width, height));
                                    scores.Add(score * 100);
                                }

                                isOk = rects.Count == resultNum && scores.All(s => s >= this.score);


                                // 在原图上绘制所有匹配框
                                using (Mat originalSourceMat = BitmapToColorMat(sourceImage))
                                {
                                    foreach (var rect in rects)
                                    {
                                        Scalar color = isOk ? Scalar.Green : Scalar.Yellow;
                                        Cv2.Rectangle(originalSourceMat, rect, color, 3);
                                    }

                                    // 如果一个都没找到
                                    if (rects.Count == 0)
                                    {
                                        Cv2.PutText(originalSourceMat, "No Match", new Point(50, 50),
                                            HersheyFonts.HersheySimplex, 3, Scalar.Red, 2);
                                    }

                                    Bitmap resultBitmap = BitmapConverter.ToBitmap(originalSourceMat);

                                    if (show)
                                    {
                                        imageROIEditControl1.SetImage(resultBitmap);
                                    }

                                    return (resultBitmap, rects, scores, isOk);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("模板匹配失败", ex);
            }
        }
        private double ComputeIoU(Rect a, Rect b)
        {
            Rect intersection = a.Intersect(b);
            int intersectionArea = intersection.Width * intersection.Height;
            if (intersectionArea == 0) return 0;

            int unionArea = a.Width * a.Height + b.Width * b.Height - intersectionArea;
            return (double)intersectionArea / unionArea;
        }

        /// <summary>
        /// Bitmap转灰度Mat
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private static Mat BitmapToGrayMat(Bitmap bitmap)
        {
            using (Mat mat = BitmapConverter.ToMat(bitmap))
            {
                if(mat.Channels() == 1)
                    return mat.Clone();
                Mat grayMat = new Mat();
                Cv2.CvtColor(mat, grayMat, ColorConversionCodes.BGR2GRAY);
                return grayMat;
            }
        }

        /// <summary>
        /// Bitmap转彩色Mat
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private static Mat BitmapToColorMat(Bitmap bitmap)
        {
            return BitmapConverter.ToMat(bitmap);
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
                NodeParamMatchTemplate param = new NodeParamMatchTemplate();
                param.TemplateFileName = textBoxModelPath.Text;
                param.Text1 = nodeSubscription1.GetText1();
                param.Text2 = nodeSubscription1.GetText2();
                param.ResultNum = int.Parse(textBoxResultNum.Text);
                param.Scale = float.Parse(textBoxScale.Text);
                if (param.Scale > 32 || param.Scale < 1)
                    throw new Exception("图像缩小倍数设置区间为[1,32]");
                param.MinScore = float.Parse(textBoxMinScore.Text);
                if (param.MinScore > 101 || param.MinScore < 0)
                    throw new Exception("最小得分阈值区间为[0,100]");
                scale = param.Scale;
                score = param.MinScore;
                resultNum = param.ResultNum;
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
        /// 点击选择模版图像文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxModelPath.Text = openFileDialog1.FileName;
                templateImage = new Bitmap(openFileDialog1.FileName);
            }
        }

        /// <summary>
        /// 点击创建模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            templateCreate.ShowDialog();
        }

        /// <summary>
        /// 缩小倍数输入改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                scale = float.Parse(textBoxScale.Text);
            }
            catch (Exception)
            {
                if (textBoxScale.Text.Length != 0)
                    MessageBoxTD.Show("参数不合法！");
            }
        }

        /// <summary>
        /// 输入得分改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                score = float.Parse(textBoxMinScore.Text);
            }
            catch (Exception)
            {
                if (textBoxMinScore.Text.Length != 0)
                    MessageBoxTD.Show("参数不合法！");
            }
        }
    }
}
