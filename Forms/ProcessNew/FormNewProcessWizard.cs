using System;
using System.Windows.Forms;
using YTVisionPro.Node;

namespace YTVisionPro.Forms.ProcessNew
{
    internal partial class FormNewProcessWizard : Form
    {
        public FormNewProcessWizard()
        {
            InitializeComponent();
            InitNodeComboBox();
        }

        /// <summary>
        /// 初始化节点下拉框
        /// </summary>
        private void InitNodeComboBox()
        {
            nodeComboBox1.Text = "光源节点";
            nodeComboBox1.AddItem("光源控制", NodeType.LightSourceControl);

            nodeComboBox2.Text = "图像采集";
            nodeComboBox2.AddItem("相机拍照", NodeType.CameraShot);
            nodeComboBox2.AddItem("本地图片", NodeType.LocalPicture);

            nodeComboBox3.Text = "PLC信号";
            nodeComboBox3.AddItem("AI结果发送", NodeType.PLCHTAIResultSend);
            nodeComboBox2.AddItem("获取软触发信号", NodeType.WaitSoftTrigger);
            nodeComboBox3.AddItem("松下寄存器写", NodeType.PLCWrite);
            nodeComboBox3.AddItem("松下寄存器读", NodeType.PLCRead);

            nodeComboBox5.Text = "工具箱";
            nodeComboBox5.AddItem("延迟执行", NodeType.SleepTool); 
            nodeComboBox5.AddItem("结果显示", NodeType.DetectResultShow);
            nodeComboBox5.AddItem("结果总判断", NodeType.Summarize);
            nodeComboBox5.AddItem("保存图像", NodeType.ImageSave);
            nodeComboBox5.AddItem("AI检测", NodeType.AIHT);
        }

        /// <summary>
        /// 添加一个流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Solution.ProcessCount++;
            TabPage tabPage = new TabPage();
            tabPage.Name = $"process{Solution.ProcessCount}";
            tabPage.Padding = new Padding(3);
            tabPage.Size = new System.Drawing.Size(465, 643);
            tabPage.Text = $"流程{Solution.ProcessCount}";
            tabPage.UseVisualStyleBackColor = true;

            ProcessEditPanel nodeEditPanel = new ProcessEditPanel(tabPage.Text);
            nodeEditPanel.Dock = DockStyle.Fill;
            tabPage.Controls.Add(nodeEditPanel);
            tabControl1.Controls.Add(tabPage);

            #region 调试代码，上线请注释

            //string res = "";
            //foreach (var name in Solution.Instance.GetAllProcessName())
            //{
            //    res += name + "\n";
            //}
            //MessageBox.Show($"方案中流程名称：\n{res}");

            #endregion
        }

        /// <summary>
        /// 删除选中的流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            if(tabControl1.Controls.Count == 0)
                return;
            if(MessageBox.Show("删除当前流程无法找回，确定删除？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            TabPage tabPageToDelete = null;

            // 遍历所有选项卡，找到选中的选项卡
            foreach (Control control in tabControl1.Controls)
            {
                if (control is TabPage page && page == tabControl1.SelectedTab)
                {
                    tabPageToDelete = page;
                    break; // 找到后立即退出循环
                }
            }

            // 检查是否找到了要删除的选项卡，然后删除
            if (tabPageToDelete != null)
            {
                tabControl1.Controls.Remove(tabPageToDelete);
                Solution.Instance.RemoveProcess(tabPageToDelete.Text);

                #region 调试代码，上线请注释

                //string res = "";
                //foreach (var name in Solution.Instance.GetAllProcessName())
                //{
                //    res += name + "\n";
                //}
                //MessageBox.Show($"方案中流程名称：\n{res}");

                //string res = "";
                //foreach (var node in Solution.Nodes)
                //{
                //    res += node.ID + "\n";
                //}
                //MessageBox.Show($"方案中节点：\n{res}");

                #endregion
            }
        }
    }
}
