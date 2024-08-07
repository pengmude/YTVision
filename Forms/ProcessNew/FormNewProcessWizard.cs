using System;
using System.Windows.Forms;

namespace YTVisionPro.Forms.ProcessNew
{
    public partial class FormNewProcessWizard : Form
    {
        private static int _processCount = 0;

        /// <summary>
        /// 流程数量
        /// </summary>
        public int ProcessCount { get { return _processCount; } }

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
            nodeComboBox1.AddItem("磐鑫光源");
            nodeComboBox1.AddItem("锐视光源");

            nodeComboBox2.Text = "相机节点";
            nodeComboBox2.AddItem("海康相机");
            nodeComboBox2.AddItem("巴斯勒相机");
            nodeComboBox2.AddItem("度申相机");

            nodeComboBox3.Text = "PLC节点";
            nodeComboBox3.AddItem("松下PLC");
            nodeComboBox3.AddItem("汇川PLC");

            nodeComboBox4.Text = "算子节点";
            nodeComboBox4.AddItem("找圆算法");
            nodeComboBox4.AddItem("找直线算法");

            nodeComboBox5.Text = "AI节点";
            nodeComboBox5.AddItem("汇图AI");
            nodeComboBox5.AddItem("海康AI");
        }

        /// <summary>
        /// 添加一个流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, System.EventArgs e)
        {
            _processCount++;
            TabPage tabPage = new TabPage();
            tabPage.Name = $"process{_processCount}";
            tabPage.Padding = new Padding(3);
            tabPage.Size = new System.Drawing.Size(465, 643);
            tabPage.Text = $"流程{_processCount}";
            tabPage.UseVisualStyleBackColor = true;

            NodeEditPanel nodeEditPanel = new NodeEditPanel();
            nodeEditPanel.Dock = DockStyle.Fill;
            tabPage.Controls.Add(nodeEditPanel);
            tabControl1.Controls.Add(tabPage);
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
            }
        }
    }
}
