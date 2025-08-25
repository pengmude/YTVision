using Sunny.UI;
using System;
using System.IO;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Node._6_LogicTool.CSharpScript
{
    public partial class NodeParamFormCSharpScript : FormBase, INodeParamForm
    {

        public NodeParamFormCSharpScript()
        {
            InitializeComponent();
        }

        public INodeParam Params { get; set; }

        /// <summary>
        /// 获取订阅的要写入共享变量的值
        /// </summary>
        /// <returns></returns>
        public void RunCode()
        {
            try
            {
                cSharpScriptEditor1.RunCode();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 点击保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NodeParamCSharpScript param = new NodeParamCSharpScript();
                param.FileName = cSharpScriptEditor1.GetFileName();
                Params = param;
            }
            catch (Exception)
            {
                MessageBoxTD.Show("参数设置异常！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Hide();
        }

        /// <summary>
        /// 订阅节点初始化
        /// </summary>
        /// <param name="node"></param>
        void INodeParamForm.SetNodeBelong(NodeBase node) 
        {
        }

        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if(Params is NodeParamCSharpScript param)
            {
                cSharpScriptEditor1.SetCodeText(File.ReadAllText(param.FileName));
            }
        }
    }
}
