using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Drawing;
using System.Windows.Forms;
using Size = OpenCvSharp.Size;
using Point = OpenCvSharp.Point;

namespace YTVisionPro.Node._3_Detection.MatchTemplate
{
    internal partial class NodeParamFormMatchTemplate : Form, INodeParamForm
    {
        private Process process;//所属流程
        private NodeBase node;//所属节点
        private TemplateCreate templateCreate = null; // 模版创建对象
        private Bitmap src = null;  // 原图
        private Bitmap templateImage = null; // 模版图像
        private float scale = 8; // 默认缩小8倍
        private float score = 0; // 匹配得分


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
                    textBox1.Text = param.TemplateFileName;
                    templateImage = new Bitmap(param.TemplateFileName);
                    nodeSubscription1.SetText(param.Text1, param.Text2);
                    textBox2.Text = param.Scale.ToString();
                    scale = param.Scale;
                    score = param.MinScore;
                    textBox3.Text = param.MinScore.ToString();
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
                src = nodeSubscription1.GetValue<Bitmap>();
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
                MatchTemplate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"模版匹配异常：原因：{ex.Message}");
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
        public (Bitmap, Rect, double, bool) MatchTemplate(bool show = true)
        {
            try
            {
                Bitmap sourceImage = (Bitmap)src.Clone();
                Bitmap templateImage = this.templateImage;
                float scale = this.scale;

                // 检查图像是否为空
                if (sourceImage == null || templateImage == null)
                    throw new ArgumentException("源图像或模板图像不能为空");

                // 将Bitmap转换为Mat，并确保它们是灰度图像
                using (Mat sourceMat = BitmapToGrayMat(sourceImage))
                using (Mat templateMat = BitmapToGrayMat(templateImage))
                {
                    // 检查图像是否成功转换
                    if (sourceMat.Empty() || templateMat.Empty())
                        throw new ArgumentException("无法将图像转换为灰度图像");

                    // 缩小图像
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
                                if (maxVal*100 >= score)
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
                                return (bitmap, rect, maxVal*100, isOk);
                            }


                        }
                    }
                    }
                }
            catch (Exception)
            {
                throw;
            }
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
                    return mat;
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
                param.TemplateFileName = textBox1.Text;
                param.Text1 = nodeSubscription1.GetText1();
                param.Text2 = nodeSubscription1.GetText2();
                param.Scale = float.Parse(textBox2.Text);
                if (param.Scale > 32 || param.Scale < 1)
                    throw new Exception("图像缩小倍数设置区间为[1,32]");
                param.MinScore = float.Parse(textBox3.Text);
                if (param.MinScore > 101 || param.MinScore < 0)
                    throw new Exception("最小得分阈值区间为[0,100]");
                scale = param.Scale;
                score = param.MinScore;
                Params = param;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"参数设置异常，原因：{ex.Message}");
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
                textBox1.Text = openFileDialog1.FileName;
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
                scale = float.Parse(textBox2.Text);
            }
            catch (Exception)
            {
                if (textBox2.Text.Length != 0)
                    MessageBox.Show("参数不合法！");
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
                score = float.Parse(textBox3.Text);
            }
            catch (Exception)
            {
                if (textBox3.Text.Length != 0)
                    MessageBox.Show("参数不合法！");
            }
        }
    }
}
