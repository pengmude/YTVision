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
            Params = new NodeParamDataShow();
            Hide();
        }
    }
}
