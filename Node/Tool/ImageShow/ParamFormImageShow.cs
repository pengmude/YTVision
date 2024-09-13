using Logger;
using Sunny.UI;
using System;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Forms.ImageViewer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YTVisionPro.Node.Tool.ImageShow
{
    internal partial class ParamFormImageShow : Form, INodeParamForm
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

        /// <summary>
        /// 获取订阅的图片结果
        /// </summary>
        /// <returns></returns>
        public Bitmap GetImage()
        {
            return nodeSubscription1.GetValue<Bitmap>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nodeSubscription1.GetText1().IsNullOrEmpty())
            {
                MessageBox.Show("未订阅任何结果！");
                LogHelper.AddLog(MsgLevel.Fatal, "未订阅任何结果！", true);
                return;
            }
            if (WindowNameList.Text.IsNullOrEmpty())
            {
                MessageBox.Show("图像窗口名称不能为空！");
                LogHelper.AddLog(MsgLevel.Fatal, "图像窗口名称不能为空！", true);
                return;
            }

            NodeParamImageShow nodeParamImageShow = new NodeParamImageShow();
            nodeParamImageShow.WindowName = WindowNameList.Text;
            Params = nodeParamImageShow;
            Hide();
        }
    }
}
