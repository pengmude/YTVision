using System;
using System.Windows.Forms;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._7_ResultProcessing.ResultSummarize
{
    internal partial class ParamFormSummarize : Form, INodeParamForm
    {
        public ParamFormSummarize()
        {
            InitializeComponent();
        }

        public INodeParam Params { get; set; }

        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
            nodeSubscription2.Init(node);
            nodeSubscription3.Init(node);
            nodeSubscription4.Init(node);
        }

        /// <summary>
        /// 获取算法结果1
        /// </summary>
        /// <returns></returns>
        public ResultViewData GetResult1()
        {
            try
            {
                return nodeSubscription1.GetValue<ResultViewData>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取算法结果2
        /// </summary>
        /// <returns></returns>
        public ResultViewData GetResult2()
        {
            try
            {
                return nodeSubscription2.GetValue<ResultViewData>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取算法结果3
        /// </summary>
        /// <returns></returns>
        public ResultViewData GetResult3()
        {
            try
            {
                return nodeSubscription3.GetValue<ResultViewData>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取算法结果1
        /// </summary>
        /// <returns></returns>
        public ResultViewData GetResult4()
        {
            try
            {
                return nodeSubscription4.GetValue<ResultViewData>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            NodeParamSummarize param = new NodeParamSummarize();
            param.Texts[0] = nodeSubscription1.GetText1();
            param.Texts[1] = nodeSubscription1.GetText2();
            param.Texts[2] = nodeSubscription2.GetText1();
            param.Texts[3] = nodeSubscription2.GetText2();
            param.Texts[4] = nodeSubscription3.GetText1();
            param.Texts[5] = nodeSubscription3.GetText2();
            param.Texts[6] = nodeSubscription4.GetText1();
            param.Texts[7] = nodeSubscription4.GetText2();
            param.Enables[0] = checkBox1.Checked;
            param.Enables[1] = checkBox2.Checked;
            param.Enables[2] = checkBox3.Checked;
            param.Enables[3] = checkBox4.Checked;
            Params = param;
            Hide();
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamSummarize param)
            {
                nodeSubscription1.SetText(param.Texts[0], param.Texts[1]);
                nodeSubscription2.SetText(param.Texts[2], param.Texts[3]);
                nodeSubscription3.SetText(param.Texts[4], param.Texts[5]);
                nodeSubscription4.SetText(param.Texts[6], param.Texts[7]);
                checkBox1.Checked = param.Enables[0];
                checkBox2.Checked = param.Enables[1];
                checkBox3.Checked = param.Enables[2];
                checkBox4.Checked = param.Enables[3];
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(sender is CheckBox checkBox) 
            {
                switch (checkBox.Name)
                {
                    case "checkBox1":
                        nodeSubscription1.Enabled = checkBox.Checked;
                        break;
                    case "checkBox2":
                        nodeSubscription2.Enabled = checkBox.Checked;
                        break;
                    case "checkBox3":
                        nodeSubscription3.Enabled = checkBox.Checked;
                        break;
                    case "checkBox4":
                        nodeSubscription4.Enabled = checkBox.Checked;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
