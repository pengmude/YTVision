using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._7_ResultProcessing.DataShow
{
    public partial class NodeParamFormDataShow : FormBase, INodeParamForm
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
        public AlgorithmResult GetAiResult()
        {
            return nodeSubscription1.GetValue<AlgorithmResult>();
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
