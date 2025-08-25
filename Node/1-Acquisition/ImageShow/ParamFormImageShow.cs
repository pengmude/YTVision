using Logger;
using OpenCvSharp.Extensions;
using Sunny.UI;
using System;
using System.Drawing;
using System.Windows.Forms;
using TDJS_Vision.Forms.ImageViewer;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Node._1_Acquisition.ImageSource
{
    public partial class ParamFormImageShow : FormBase, INodeParamForm
    {
        public ParamFormImageShow()
        {
            InitializeComponent();
            for (int i = 0; i < FrmImageViewer.CurWindowsNum; i++)
            {
                WindowNameList.Items.Add($"图像窗口{i+1}");
            }
            CanvasSet.WindowNumChangeEvent += CanvasSet_WindowNumChangeEvent;
        }

        private void CanvasSet_WindowNumChangeEvent(object sender, int e)
        {
            WindowNameList.Items.Clear();
            for (int i = 0; i < e; i++)
            {
                WindowNameList.Items.Add($"图像窗口{i+1}");
            }
        }

        public INodeParam Params { get; set; }

        /// <summary>
        /// 用于节点参数界面需要订阅结果的情况调用
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }

        public void SetParam2Form()
        {
            if(Params is NodeParamImageShow param)
            {
                nodeSubscription1.SetText(param.Text1, param.Text2);
                int index = WindowNameList.Items.IndexOf(param.WindowName);
                if(index == -1)
                    throw new Exception("找不到对应图像窗口名称！");
                WindowNameList.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 获取订阅的图片结果
        /// </summary>
        /// <returns></returns>
        public Bitmap GetImage()
        {
            return nodeSubscription1.GetValue<OutputImage>().Bitmaps[0].ToBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nodeSubscription1.GetText1().IsNullOrEmpty())
            {
                MessageBoxTD.Show("未订阅任何结果！");
                LogHelper.AddLog(MsgLevel.Fatal, "未订阅任何结果！", true);
                return;
            }
            if (WindowNameList.Text.IsNullOrEmpty())
            {
                MessageBoxTD.Show("图像窗口名称不能为空！");
                LogHelper.AddLog(MsgLevel.Fatal, "图像窗口名称不能为空！", true);
                return;
            }

            NodeParamImageShow nodeParamImageShow = new NodeParamImageShow();
            nodeParamImageShow.WindowName = WindowNameList.Text;
            nodeParamImageShow.Text1 = nodeSubscription1.GetText1();
            nodeParamImageShow.Text2 = nodeSubscription1.GetText2();
            Params = nodeParamImageShow;
            Hide();
        }
    }
}
