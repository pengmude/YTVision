using Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Forms.ImageViewer;
using YTVisionPro.Forms.LightAdd;
using YTVisionPro.Node.Camera.HiK;

namespace YTVisionPro.Node.Tool.ImageShow
{
    internal partial class ParamFormImageShow : Form, INodeParamForm
    {
        public ParamFormImageShow()
        {
            InitializeComponent();
            for (int i = 0; i < FrmImageViewer.CurWindowsNum; i++)
            {
                WindowNameList.Items.Add($"图像窗口{i}");
            }
            CanvasSet.WindowNumChangeEvent += CanvasSet_WindowNumChangeEvent;
        }

        private void CanvasSet_WindowNumChangeEvent(object sender, int e)
        {
            WindowNameList.Items.Clear();
            for (int i = 0; i < e; i++)
            {
                WindowNameList.Items.Add($"图像窗口{i}");
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
            NodeParamImageShow nodeParamImageShow = new NodeParamImageShow();
            nodeParamImageShow.WindowName = WindowNameList.Text;
            Params = nodeParamImageShow;
            Hide();
        }
    }
}
