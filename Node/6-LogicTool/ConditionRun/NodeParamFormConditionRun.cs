using System;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Node._6_LogicTool.ConditionRun
{
    public partial class NodeParamFormConditionRun : FormBase, INodeParamForm
    {
        public NodeParamFormConditionRun()
        {
            InitializeComponent();
        }

        public INodeParam Params { get; set; }
        /// <summary>
        /// 获取订阅的值
        /// </summary>
        /// <returns></returns>
        public bool GetSubValue()
        {
            try
            {
                return nodeSubscription1.GetValue<bool>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NodeParamConditionRun nodeParamConditionRun = new NodeParamConditionRun();
                nodeParamConditionRun.Text1 = nodeSubscription1.GetText1();
                nodeParamConditionRun.Text2 = nodeSubscription1.GetText2();
                Params = nodeParamConditionRun;
            }
            catch (Exception)
            {
                MessageBoxTD.Show("参数设置异常！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Hide();
        }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if(Params is NodeParamConditionRun param)
                nodeSubscription1.SetText(param.Text1, param.Text2);
        }
    }
}
