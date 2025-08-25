using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using TDJS_Vision.Forms.ShapeDraw;
using TDJS_Vision.Node._1_Acquisition.ImageSource;

namespace TDJS_Vision.Node._2_ImagePreprocessing.ImageCrop
{
    public partial class NodeParamFormImageCrop : FormBase, INodeParamForm
    {
        private Process process;//所属流程
        private NodeBase node;//所属节点
        public NodeParamFormImageCrop(Process process, NodeBase nodeBase)
        {
            InitializeComponent();
            imageROIEditControl1.SetROIType2Draw(ROIType.Rectangle);
            this.process = process;
            this.node = nodeBase;
        }

        public INodeParam Params { get; set; }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 反序列化恢复参数
        /// </summary>
        public void SetParam2Form()
        {
            if(Params is NodeParamImageCrop param)
            {
                nodeSubscription1.SetText(param.Text1, param.Text2);
                imageROIEditControl1.SetROIs(param.ROIs);
            }
        }

        /// <summary>
        /// 获取ROI图像
        /// </summary>
        public List<Mat> GetROIImages() 
        {
            var img = imageROIEditControl1.GetROIImages();
            return img;
        }

        public List<Rect> GetImageROIRects()
        {
            try
            {
                return imageROIEditControl1.GetImageROIRects();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取订阅的图像设置到显示控件中
        /// </summary>
        public Mat UpdataImage()
        {
            OutputImage bitmap = new OutputImage();
            try
            {
                bitmap = nodeSubscription1.GetValue<OutputImage>();
            }
            catch (Exception) 
            {
                bitmap.Bitmaps = new List<Mat>() { null };
            }
            imageROIEditControl1.SetImage(bitmap.Bitmaps[0].ToBitmap());
            return bitmap.Bitmaps[0];
        }

        /// <summary>
        /// 确定ROI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            NodeParamImageCrop nodeParamImageCrop = new NodeParamImageCrop();
            nodeParamImageCrop.Text1 = nodeSubscription1.GetText1();
            nodeParamImageCrop.Text2 = nodeSubscription1.GetText2();
            nodeParamImageCrop.ROIs = imageROIEditControl1.GetROIs();
            Params = nodeParamImageCrop;
            Hide();
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
    }
}
