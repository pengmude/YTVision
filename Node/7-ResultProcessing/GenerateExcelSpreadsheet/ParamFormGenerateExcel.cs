using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace TDJS_Vision.Node._7_ResultProcessing.GenerateExcelSpreadsheet
{
    public partial class ParamFormGenerateExcel : FormBase, INodeParamForm
    {
        public Dictionary<int, bool> Dictionary = new Dictionary<int, bool>();

        public ParamFormGenerateExcel()
        {
            InitializeComponent();
            Dictionary.Add(1, false);
            Dictionary.Add(2, false);
            Dictionary.Add(3, false);
            Dictionary.Add(4, false);
            Dictionary.Add(5, false);
            Dictionary.Add(6, false);
            Dictionary.Add(7, false);
            Dictionary.Add(8, false);
            Dictionary.Add(9, false);
            Dictionary.Add(10, false);
        }

        public INodeParam Params { get; set; }

        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
            nodeSubscription2.Init(node);
            nodeSubscription3.Init(node);
            nodeSubscription4.Init(node);
            nodeSubscription5.Init(node);
            nodeSubscription6.Init(node);
            nodeSubscription7.Init(node);
            nodeSubscription8.Init(node);
            nodeSubscription9.Init(node);
            nodeSubscription10.Init(node);
        }

        /// <summary>
        /// 获取算法结果1
        /// </summary>
        /// <returns></returns>
        public object GetResult1()
        {
            try
            {
                return nodeSubscription1.GetValue<object>();
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
        public object GetResult2()
        {
            try
            {
                return nodeSubscription2.GetValue<object>();
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
        public object GetResult3()
        {
            try
            {
                return nodeSubscription3.GetValue<object>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取算法结果4
        /// </summary>
        /// <returns></returns>
        public object GetResult4()
        {
            try
            {
                return nodeSubscription4.GetValue<object>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取算法结果5
        /// </summary>
        /// <returns></returns>
        public object GetResult5()
        {
            try
            {
                return nodeSubscription5.GetValue<object>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取算法结果6
        /// </summary>
        /// <returns></returns>
        public object GetResult6()
        {
            try
            {
                return nodeSubscription6.GetValue<object>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取算法结果7
        /// </summary>
        /// <returns></returns>
        public object GetResult7()
        {
            try
            {
                return nodeSubscription7.GetValue<object>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取算法结果8
        /// </summary>
        /// <returns></returns>
        public object GetResult8()
        {
            try
            {
                return nodeSubscription8.GetValue<object>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取算法结果9
        /// </summary>
        /// <returns></returns>
        public object GetResult9()
        {
            try
            {
                return nodeSubscription9.GetValue<object>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取算法结果10
        /// </summary>
        /// <returns></returns>
        public object GetResult10()
        {
            try
            {
                return nodeSubscription10.GetValue<object>();
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
            NodeParamGenerateExcel param = new NodeParamGenerateExcel();
            param.Texts[0] = nodeSubscription1.GetText1();
            param.Texts[1] = nodeSubscription1.GetText2();

            param.Texts[2] = nodeSubscription2.GetText1();
            param.Texts[3] = nodeSubscription2.GetText2();

            param.Texts[4] = nodeSubscription3.GetText1();
            param.Texts[5] = nodeSubscription3.GetText2();

            param.Texts[6] = nodeSubscription4.GetText1();
            param.Texts[7] = nodeSubscription4.GetText2();

            param.Texts[8] = nodeSubscription5.GetText1();
            param.Texts[9] = nodeSubscription5.GetText2();

            param.Texts[10] = nodeSubscription6.GetText1();
            param.Texts[11] = nodeSubscription6.GetText2();

            param.Texts[12] = nodeSubscription7.GetText1();
            param.Texts[13] = nodeSubscription7.GetText2();

            param.Texts[14] = nodeSubscription8.GetText1();
            param.Texts[15] = nodeSubscription8.GetText2();

            param.Texts[16] = nodeSubscription9.GetText1();
            param.Texts[17] = nodeSubscription9.GetText2();

            param.Texts[18] = nodeSubscription10.GetText1();
            param.Texts[19] = nodeSubscription10.GetText2();

            param.Enables[0] = checkBox1.Checked;
            param.Enables[1] = checkBox2.Checked;
            param.Enables[2] = checkBox3.Checked;
            param.Enables[3] = checkBox4.Checked;
            param.Enables[4] = checkBox5.Checked;
            param.Enables[5] = checkBox6.Checked;
            param.Enables[6] = checkBox7.Checked;
            param.Enables[7] = checkBox8.Checked;
            param.Enables[8] = checkBox9.Checked;
            param.Enables[9] = checkBox10.Checked;
            param.filePath = textBox1.Text;
            Params = param;
            Hide();
        }

        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        public void SetParam2Form()
        {
            if (Params is NodeParamGenerateExcel param)
            {
                nodeSubscription1.SetText(param.Texts[0], param.Texts[1]);
                nodeSubscription2.SetText(param.Texts[2], param.Texts[3]);
                nodeSubscription3.SetText(param.Texts[4], param.Texts[5]);
                nodeSubscription4.SetText(param.Texts[6], param.Texts[7]);
                nodeSubscription5.SetText(param.Texts[8], param.Texts[9]);
                nodeSubscription6.SetText(param.Texts[10], param.Texts[11]);
                nodeSubscription7.SetText(param.Texts[12], param.Texts[13]);
                nodeSubscription8.SetText(param.Texts[14], param.Texts[15]);
                nodeSubscription9.SetText(param.Texts[16], param.Texts[17]);
                nodeSubscription10.SetText(param.Texts[18], param.Texts[19]);
                checkBox1.Checked = param.Enables[0];
                checkBox2.Checked = param.Enables[1];
                checkBox3.Checked = param.Enables[2];
                checkBox4.Checked = param.Enables[3];
                checkBox5.Checked = param.Enables[4];
                checkBox6.Checked = param.Enables[5];
                checkBox7.Checked = param.Enables[6];
                checkBox8.Checked = param.Enables[7];
                checkBox9.Checked = param.Enables[8];
                checkBox10.Checked = param.Enables[9];
                textBox1.Text = param.filePath;
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                switch (checkBox.Name)
                {
                    case "checkBox1":
                        nodeSubscription1.Enabled = checkBox.Checked;
                        Dictionary[1] = checkBox.Checked;
                        break;
                    case "checkBox2":
                        nodeSubscription2.Enabled = checkBox.Checked;
                        Dictionary[2] = checkBox.Checked;
                        break;
                    case "checkBox3":
                        nodeSubscription3.Enabled = checkBox.Checked;
                        Dictionary[3] = checkBox.Checked;
                        break;
                    case "checkBox4":
                        nodeSubscription4.Enabled = checkBox.Checked;
                        Dictionary[4] = checkBox.Checked;
                        break;
                    case "checkBox5":
                        nodeSubscription5.Enabled = checkBox.Checked;
                        Dictionary[5] = checkBox.Checked;
                        break;
                    case "checkBox6":
                        nodeSubscription6.Enabled = checkBox.Checked;
                        Dictionary[6] = checkBox.Checked;
                        break;
                    case "checkBox7":
                        nodeSubscription7.Enabled = checkBox.Checked;
                        Dictionary[7] = checkBox.Checked;
                        break;
                    case "checkBox8":
                        nodeSubscription8.Enabled = checkBox.Checked;
                        Dictionary[8] = checkBox.Checked;
                        break;
                    case "checkBox9":
                        nodeSubscription9.Enabled = checkBox.Checked;
                        Dictionary[9] = checkBox.Checked;
                        break;
                    case "checkBox10":
                        nodeSubscription10.Enabled = checkBox.Checked;
                        Dictionary[10] = checkBox.Checked;
                        break;
                    default:
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 显示文件夹浏览对话框
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // 获取选择的文件夹路径
                string selectedPath = folderBrowserDialog1.SelectedPath;

                // 获取当前日期和时间，并格式化为字符串
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

                // 将 filePath 设置为 selectedPath 加上当前日期和时间
                string filePath = Path.Combine(selectedPath, $"{currentTime}.csv");

                textBox1.Text = filePath;
            }
        }
    }
}
