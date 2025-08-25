using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._1_Acquisition.ImageSource;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._7_ResultProcessing.ImageDraw
{
    public partial class NodeParamFormImageDraw : FormBase, INodeParamForm
    {
        public NodeParamFormImageDraw()
        {
            InitializeComponent();
        }

        public INodeParam Params { get; set; }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
            nodeSubscription2.Init(node);
        }

        /// <summary>
        /// 获取订阅的图像
        /// </summary>
        /// <returns></returns>
        public Mat GetImage()
        {
            Mat bitmap = null;
            try
            {
                var output = nodeSubscription1.GetValue<OutputImage>();
                bitmap = output.Bitmaps[0];
            }
            catch (Exception)
            {
                throw new Exception("订阅的图像为null！");
            }
            return bitmap;
        }
        /// <summary>
        /// 获取订阅的AI结果
        /// </summary>
        /// <returns></returns>
        public AlgorithmResult GetAIResults()
        {
            AlgorithmResult res = null;
            try
            {
                res = nodeSubscription2.GetValue<AlgorithmResult>();
            }
            catch (Exception)
            {
                throw new Exception("订阅的结果为null！");
            }
            return res;
        }
        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nodeSubscription1.GetText1())
                || string.IsNullOrEmpty(nodeSubscription1.GetText2())
                || string.IsNullOrEmpty(nodeSubscription2.GetText1())
                || string.IsNullOrEmpty(nodeSubscription2.GetText2())
                )
            {
                MessageBoxTD.Show("参数设置不完整！");
                return;
            }
            try
            {
                var param = new NodeParamImageDraw();
                param.Text1 = nodeSubscription1.GetText1();
                param.Text2 = nodeSubscription1.GetText2();
                param.Text3 = nodeSubscription2.GetText1();
                param.Text4 = nodeSubscription2.GetText2();
                param.FontSize = int.Parse(textBox_FontSize.Text);
                param.LineWidth = int.Parse(textBox_LineWidth.Text);
                param.TextLoc = radioButtonLeftUp.Checked ? TextLoc.LeftUp : TextLoc.RightDown;
                Params = param;
                Hide();
            }
            catch (Exception)
            {
                MessageBoxTD.Show("参数设置不合理！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamImageDraw param)
            {
                // 还原界面显示
                nodeSubscription1.SetText(param.Text1, param.Text2);
                nodeSubscription2.SetText(param.Text3, param.Text4);
                textBox_FontSize.Text = param.FontSize.ToString();
                textBox_LineWidth.Text = param.LineWidth.ToString();
                switch (param.TextLoc)
                {
                    case TextLoc.LeftUp:
                        radioButtonLeftUp.Checked = true;
                        break;
                    case TextLoc.RightDown:
                        radioButtonRightDown.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
