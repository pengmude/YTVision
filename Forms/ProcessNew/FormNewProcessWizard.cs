using gCursorLib;
using Logger;
using Sunny.UI;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TDJS_Vision.Forms.TCPAdd;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node;

namespace TDJS_Vision.Forms.ProcessNew
{
    public partial class FormNewProcessWizard : FormBase
    {
        // 编辑流程时通过快捷键保存方案的事件
        public event EventHandler OnShotKeySavePressed;

        public FormNewProcessWizard()
        {
            InitializeComponent();
            FrmTCPListView.OnTCPDeserializationCompletionEvent += Deserialization;
            this.KeyPreview = true;
            Init();
            Shown += FormNewProcessWizard_Shown;
            FormProcessRename.ProcessRenameChanged += FormProcessRename_ProcessRenameChanged;
        }
        /// <summary>
        /// 流程重命名事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormProcessRename_ProcessRenameChanged(object sender, (string, string) e)
        {
            RenameTabPage(tabControl1, e.Item1, e.Item2);
        }
        /// <summary>
        /// 重命名选项卡
        /// </summary>
        /// <param name="tabControl"></param>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        public static void RenameTabPage(TabControl tabControl, string oldName, string newName)
        {
            // 遍历 TabPages 集合
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                // 检查 TabPage 的 Text 属性是否等于 oldName
                if (tabPage.Text == oldName)
                {
                    // 更改 TabPage 的名称
                    tabPage.Text = newName;
                    return; // 找到并重命名后退出循环
                }
            }

            // 如果没有找到指定名称的 TabPage，则输出提示信息
            LogHelper.AddLog(MsgLevel.Exception, $"未找到名为 {oldName} 的 TabPage。",true);
        }

        private void FormNewProcessWizard_Shown(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        /// <summary>
        /// 初始化算法工具箱
        /// </summary>
        private void Init()
        {
            string filePath = "ToolTreeView.xml";
            if (File.Exists(filePath))
            {
                try
                {
                    TreeViewSerializer.DeserializeTreeView(treeView1, filePath);
                    treeView1.ImageList = imageList1;
                    treeView1.ExpandAll();
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Exception, "流程编辑窗口工具箱配置文件ToolTreeView.xml反序列化失败！原因：" + ex.Message, true);
                }
            }
            else
            {
                LogHelper.AddLog(MsgLevel.Exception, "流程编辑窗口工具箱配置文件ToolTreeView.xml缺失！", true);
            }
        }

        bool m_MouseDown = false;
        TreeNode nodeToDrag = new TreeNode();
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 获取鼠标点击位置处的节点
                nodeToDrag = treeView1.GetNodeAt(e.X, e.Y);
                treeView1.SelectedNode = nodeToDrag;

                if (nodeToDrag != null && nodeToDrag.Level == 1)
                {
                    m_MouseDown = true;
                }
            }
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MouseDown && nodeToDrag.Tag != null)
            {
                DataObject dragData = new DataObject(DragDataFormat, new DragData(nodeToDrag.Text, (NodeType)nodeToDrag.Tag));
                DoDragDrop(dragData, DragDropEffects.Move);
                m_MouseDown = false;
            }
        }


        /// <summary>
        /// 拖拽的数据
        /// </summary>
        public struct DragData
        {
            public string Text;
            public NodeType NodeType;
            public DragData(string text, NodeType type)
            {
                Text = text;
                NodeType = type;
            }
        }

        /// <summary>
        /// 自定义的拖拽数据格式
        /// </summary>
        internal const string DragDataFormat = "DragData";

        /// <summary>
        /// 为了拖拽时实时显示效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;

            gCursor1.gText = nodeToDrag.Text;
            gCursor1.gEffect = gCursor.eEffect.Move;
            gCursor1.gImage = (Bitmap)imageList1.Images[nodeToDrag.ImageIndex];
            gCursor1.gImageBox = new Size(22, 22);
            gCursor1.gTextAlignment = ContentAlignment.TopLeft;
            gCursor1.gType = gCursor.eType.Both;
            gCursor1.MakeCursor();
            Cursor.Current = gCursor1.gCursor;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deserialization(object sender, bool e)
        {
            try
            {
                // 清空流程控件
                tabControl1.Controls.Clear();

                // 根据加载的配置重新添加流程控件
                foreach (var processInfo in ConfigHelper.SolConfig.ProcessInfos)
                {
                    TabPage tabPage = new TabPage();
                    tabPage.Name = processInfo.ProcessName;
                    tabPage.Padding = new Padding(3);
                    tabPage.Size = new Size(465, 643);
                    tabPage.Text = processInfo.ProcessName;
                    tabPage.UseVisualStyleBackColor = true;

                    ProcessEditPanel nodeEditPanel = new ProcessEditPanel(tabPage.Text, e, processInfo);
                    nodeEditPanel.Dock = DockStyle.Fill;
                    tabPage.Controls.Add(nodeEditPanel);
                    tabControl1.Controls.Add(tabPage);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 快捷键保存方案
        /// </summary>
        /// <param name="e"></param>
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
            tabControl1.SelectedTab = tabPage;
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
            var res = MessageBoxTD.Show("删除当前流程无法找回，确定删除？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.No || res == DialogResult.None)
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
            }
        }

        private void 全部展开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void 全部折叠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }
    }
}
