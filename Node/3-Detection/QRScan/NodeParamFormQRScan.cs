using Logger;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Size = OpenCvSharp.Size;

namespace YTVisionPro.Node._3_Detection.QRScan
{
    internal partial class NodeParamFormQRScan : Form, INodeParamForm
    {
        private Process process;//所属流程
        private NodeBase node;//所属节点
        private WeChatQRCode weChatQRCode = null;

        public NodeParamFormQRScan(Process process, NodeBase nodeBase)
        {
            InitializeComponent();
            this.process = process;
            this.node = nodeBase;
            imageROIEditControl1.SetROIType2Draw(Forms.ShapeDraw.ROIType.Rectangle);
            Shown += NodeParamFormQRCodeIdentification_Shown;
            toolTip1.SetToolTip(label1, "减少噪点,仅限奇数,影响边缘检测图像");
            toolTip1.SetToolTip(label2, "较小的值会导致较少的对比度增强，而较大的值会允许更多的对比度增强但是噪声提高。通常，在 1.0 到 4.0 之间是常见的选择");
            toolTip1.SetToolTip(label3, "适用于光照不均的图像");
            toolTip1.SetToolTip(label4, "较小的块会导致更细致的对比度调整，而较大的块则会导致更平滑的结果");
            // 加载模型
            Task.Run(() =>
            {
                string detect_caffe_model = ".\\model\\detect.caffemodel";
                string detect_prototxt = ".\\model\\detect.prototxt";
                string sr_caffe_model = ".\\model\\sr.caffemodel";
                string sr_prototxt = ".\\model\\sr.prototxt";

                weChatQRCode = WeChatQRCode.Create(
                    detect_prototxt,
                    detect_caffe_model,
                    sr_prototxt,
                    sr_caffe_model
                );
            });
        }

        private void NodeParamFormQRCodeIdentification_Shown(object sender, EventArgs e)
        {
            UpdataImage();
        }

        public INodeParam Params { get; set; }

        public void SetParam2Form()
        {
            if (Params is NodeParamQRScan param)
            {
                nodeSubscription1.SetText(param.Text1, param.Text2);
                checkBoxMoreParams.Checked = param.MoreParamsEnable;
                textBoxBlurSize.Text = param.GaussianBlur.ToString();
                checkBox1.Checked = param.ISHistogramEqualization;
                this.textBox2.Text = param.ClipLimit.ToString();
                this.textBox3.Text = param.TileGridSize.Width.ToString();
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

        private async void button3_Click(object sender, EventArgs e)
        {
            await process.RunForUpdateImages(node);
            UpdataImage();
        }

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

        private async void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                await QRCodeDetect();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"检测异常：原因：{ex.Message}");
            }
        }

        public async Task<string[]> QRCodeDetect(bool show = true)
        {
            try
            {
                if (weChatQRCode == null)
                    throw new Exception("模型未加载完成！");

                // 更新输入图像和获取ROI图像
                pictureBoxCanny.Image = null;
                UpdataImage();
                Mat image = imageROIEditControl1.GetROIImages().ToMat();

                // 处理图像
                Mat blurred = await ImageProcessingasync(image);

                // 设置参数界面点击运行才需要刷新，节点正常运行调用时不需要刷新，降低耗时
                if (show)
                    pictureBoxCanny.Image = blurred.ToBitmap();

                // 检测二维码
                string[] Information = IdentifyQRCodesAndBarcodes(blurred, weChatQRCode);

                if (Information.Length == 0) 
                    throw new Exception($"二维码为空！");

                return Information;
            }
            catch (Exception ex)
            {
                throw new Exception($"检测二维码失败，原因：{ex.Message}");
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
                NodeParamQRScan nodeParamQRCodeIdentification = new NodeParamQRScan();
                nodeParamQRCodeIdentification.Text1 = nodeSubscription1.GetText1();
                nodeParamQRCodeIdentification.Text2 = nodeSubscription1.GetText2();
                nodeParamQRCodeIdentification.MoreParamsEnable = checkBoxMoreParams.Checked;
                nodeParamQRCodeIdentification.GaussianBlur = int.Parse(textBoxBlurSize.Text);
                nodeParamQRCodeIdentification.ROI = imageROIEditControl1.GetROI();
                nodeParamQRCodeIdentification.ISHistogramEqualization = this.checkBox1.Checked;
                nodeParamQRCodeIdentification.ClipLimit = double.Parse(this.textBox2.Text);
                nodeParamQRCodeIdentification.TileGridSize = new Size(int.Parse(this.textBox3.Text), int.Parse(this.textBox3.Text));
                Params = nodeParamQRCodeIdentification;
            }
            catch (Exception)
            {
                MessageBox.Show("参数设置异常，请检查参数设置是否合理！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测二维码
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private string[] IdentifyQRCodesAndBarcodes(Mat map, WeChatQRCode QRCode)
        {
            try
            {
                Mat[] bbox;  // 存放二维码检测矩形
                string[] results;  // 存放二维码解码内容

                QRCode.DetectAndDecode(map, out bbox, out results);

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void checkBoxMoreParams_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanel2.Visible = checkBoxMoreParams.Checked;
        }

        public async Task<Mat> ImageProcessingasync(Mat image)
        {
            return await Task.Run(() =>
            {
                // 图像格式转换
                Cv2.CvtColor(image, image, ColorConversionCodes.BGR2GRAY);

                //直方图均衡化               
                if (this.checkBox1.Checked)
                {
                    var claheLimited = Cv2.CreateCLAHE(double.Parse(this.textBox2.Text), new Size(int.Parse(this.textBox3.Text), int.Parse(this.textBox3.Text)));
                    var claheEq = new Mat();
                    claheLimited.Apply(image, image);
                }

                // 应用高斯模糊减少噪点
                Mat blurred = new Mat();
                Cv2.GaussianBlur(image, blurred, new Size(int.Parse(textBoxBlurSize.Text), int.Parse(textBoxBlurSize.Text)), 0);

                return blurred;
            });

        }
    }
}
