using Logger;
using Sunny.UI;
using System;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.Camera.HiK.WaitHardTrigger
{
    internal partial class ParamFormWaitHardTrigger : Form, INodeParamForm
    {
        public INodeParam Params { get; set; }
        
        public ParamFormWaitHardTrigger()
        {
            InitializeComponent();
        }


        public void SetNodeBelong(NodeBase node) 
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 获取硬触发的图像
        /// </summary>
        /// <returns></returns>
        public Bitmap GetImage()
        {
            return nodeSubscription1.GetValue<Bitmap>();
        }

        /// <summary>
        /// 点击保存当前参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            NodeParamWaitHardTrigger param = new NodeParamWaitHardTrigger();
            Params = param;
            Hide();
        }
    }
}
