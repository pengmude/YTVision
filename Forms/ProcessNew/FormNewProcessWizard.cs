using System;
using System.Windows.Forms;
using YTVisionPro.Forms.PLCAdd;
using YTVisionPro.Node;

namespace YTVisionPro.Forms.ProcessNew
{
    internal partial class FormNewProcessWizard : Form
    {
        // 编辑流程时通过快捷键保存方案的事件
        public event EventHandler OnShotKeySavePressed;

        public FormNewProcessWizard()
        {
            InitializeComponent();
            InitNodeComboBox();
            FrmPLCListView.OnPLCDeserializationCompletionEvent += Deserialization;
            this.KeyPreview = true;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deserialization(object sender, bool e)
        {
            // 清空流程控件
            tabControl1.Controls.Clear();

            // 根据加载的配置重新添加流程控件
            foreach (var processInfo in ConfigHelper.SolConfig.ProcessInfos)
            {
                Solution.Instance.ProcessCount++;
                TabPage tabPage = new TabPage();
                tabPage.Name = processInfo.ProcessName;
                tabPage.Padding = new Padding(3);
                tabPage.Size = new System.Drawing.Size(465, 643);
                tabPage.Text = processInfo.ProcessName;
                tabPage.UseVisualStyleBackColor = true;

                ProcessEditPanel nodeEditPanel = new ProcessEditPanel(tabPage.Text, e, processInfo);
                nodeEditPanel.Dock = DockStyle.Fill;
                tabPage.Controls.Add(nodeEditPanel);
                tabControl1.Controls.Add(tabPage);
            }
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Control && e.KeyCode == Keys.S)
            {
                // 触发保存方案事件
                OnShotKeySavePressed?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 添加一个流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Solution.Instance.ProcessCount++;
            TabPage tabPage = new TabPage();
            tabPage.Name = $"process{Solution.Instance.ProcessCount}";
            tabPage.Padding = new Padding(3);
            tabPage.Size = new System.Drawing.Size(465, 643);
            tabPage.Text = $"流程{Solution.Instance.ProcessCount}";
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
                //foreach (var node in Solution.Instance.Nodes)
                //{
                //    res += node.ID + "\n";
                //}
                //MessageBox.Show($"方案中节点：\n{res}");

                #endregion
            }
        }
    }
}
