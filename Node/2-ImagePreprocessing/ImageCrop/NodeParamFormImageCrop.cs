﻿using System;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Forms.ShapeDraw;

namespace YTVisionPro.Node._2_ImagePreprocessing.ImageCrop
{
    internal partial class NodeParamFormImageCrop : Form, INodeParamForm
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
                imageROIEditControl1.SetROI(param.ROI);

                // TODO: 修复必须得显示一下参数窗口再运行截取的图像才是正确的区域，
                // 原因未知，估计是和ROI管理类构造需要传入pictrueBox有关
                Show();
                Hide();
            }
        }

        /// <summary>
        /// 获取ROI图像
        /// </summary>
        public Bitmap GetROIImage() 
        {
            var img = imageROIEditControl1.GetROIImages();
            return img;
        }

        public Rectangle GetImageROIRect()
        {
            try
            {
                return imageROIEditControl1.GetImageROIRect();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取订阅的图像设置到显示控件中
        /// </summary>
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

        /// <summary>
        /// 确定ROI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            NodeParamImageCrop nodeParamImageCrop = new NodeParamImageCrop();
            nodeParamImageCrop.Image  = imageROIEditControl1.GetROIImages();
            nodeParamImageCrop.Text1 = nodeSubscription1.GetText1();
            nodeParamImageCrop.Text2 = nodeSubscription1.GetText2();
            nodeParamImageCrop.ROI = imageROIEditControl1.GetROI();
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
