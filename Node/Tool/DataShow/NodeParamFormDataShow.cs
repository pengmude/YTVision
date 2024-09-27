using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Node.AI.HTAI;
using YTVisionPro.Node.Camera.HiK;
using YTVisionPro.Node.PLC.Panasonic.Wirte;

namespace YTVisionPro.Node.Tool.DataShow
{
    internal partial class NodeParamFormDataShow : Form, INodeParamForm
    {
        public NodeParamFormDataShow()
        {
            InitializeComponent();
        }

        public INodeParam Params { get; set; }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 获取订阅的结果
        /// </summary>
        /// <returns></returns>
        public AiResult GetAiResult()
        {
            return nodeSubscription1.GetValue<AiResult>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var param = new NodeParamDataShow();
            param.Text1 = nodeSubscription1.GetText1();
            param.Text2 = nodeSubscription1.GetText2();
            Params = param;
            Hide();
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamDataShow param)
            {
                // 还原界面显示
                nodeSubscription1.SetText(param.Text1, param.Text2);
            }
        }
    }
}
