using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Node.Tool.ResultSummarize;

namespace YTVisionPro.Node.Tool.SleepTool
{
    internal partial class NodeParamFormSleepTool : Form, INodeParamForm
    {
        public NodeParamFormSleepTool()
        {
            InitializeComponent();
        }

        public INodeParam Params { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NodeParamSleepTool nodeParamSleepTool = new NodeParamSleepTool();
                nodeParamSleepTool.Time = int.Parse(numericUpDown1.Value.ToString());
                Params = nodeParamSleepTool;
            }
            catch (Exception)
            {
                MessageBox.Show("参数设置异常！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Hide();
        }

        void INodeParamForm.SetNodeBelong(NodeBase node) { }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if(Params is NodeParamSleepTool param)
                numericUpDown1.Value = param.Time;
        }
    }
}
