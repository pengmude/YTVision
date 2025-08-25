using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Markup;
using WeifenLuo.WinFormsUI.Docking;
using TDJS_Vision.Node;
using TDJS_Vision.Node._7_ResultProcessing.DataShow;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Forms.ResultView
{
    public partial class FrmResultView : DockContent
    {
        /// <summary>
        /// 图像窗口隐藏事件
        /// </summary>
        public event EventHandler HideChangedEvent;
        /// <summary>
        /// 用来实现非UI线程到UI线程的切换
        /// </summary>
        private SynchronizationContext _uiContext;

        public FrmResultView()
        {
            InitializeComponent();
            this.tabControl1.TabPages.Clear();
            NodeDataShow.DataShow += NodeDataShow_DataShow; 
            FrmNodeRename.RenameChangeEvent += RenameChangeEvent;
            Solution.Instance.RemoveResultData += RemoveResultData;
            _uiContext = SynchronizationContext.Current; // 捕获 UI 线程的同步上下文
        }

        private void RemoveResultData(object sender, EventArgs e)
        {
            tabControl1.Controls.Clear();
        }

        private void RenameChangeEvent(object sender, RenameResult e)
        {
            // 节点重命名时，只有检测结果显示的节点才可能需要同步更改TabPage页面名称
            if (e.Type != NodeType.DetectResultShow)
                return;

            // 更新tabPage名称
            TabPage tabPage = tabControl1.TabPages[$"{e.NodeId}.{e.NodeNameOld}"];
            if (tabPage != null)
            {
                tabPage.Text = $"{e.NodeId}.{e.NodeNameNew}";
                tabPage.Name = $"{e.NodeId}.{e.NodeNameNew}";
            }
        }

        private void FrmResultView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;  // 取消关闭事件，防止窗口关闭
            this.Hide(); // 隐藏窗口
            HideChangedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void NodeDataShow_DataShow(object sender, DataShowData e)
        {
            // 保证切换到ui线程访问控件
            _uiContext.Post(_ =>
            {
                #region 更新显示的数据

                // 查找或创建 TabPage
                TabPage tabPage = tabControl1.TabPages[e.TabPageName];
                if (tabPage == null)
                {
                    tabPage = new TabPage(e.TabPageName);
                    tabPage.Name = e.TabPageName;
                    // 背景颜色
                    //tabPage.BackColor = Color.DarkSlateGray;
                    tabControl1.TabPages.Add(tabPage);

                    // 创建 DataGridView 并设置它的 Dock 属性为 Fill，以便充满整个 TabPage
                    DataGridView dataGridView = new DataGridView();
                    dataGridView.ReadOnly = true;
                    dataGridView.Dock = DockStyle.Fill;

                    // 为 DataGridView 添加列
                    dataGridView.Columns.Add("DetectName", "检测项");
                    dataGridView.Columns.Add("SingleResult", "当前判定");
                    dataGridView.Columns.Add("DetectResult", "检出内容");
                    dataGridView.Columns.Add("CountNG", "NG个数");
                    dataGridView.Columns.Add("CountOK", "OK个数");
                    dataGridView.Columns.Add("Total", "总数");
                    dataGridView.Columns.Add("Yield", "良率");
                    // 设置列标题的高度
                    dataGridView.ColumnHeadersHeight = 64;
                    // 设置行的高度
                    dataGridView.RowTemplate.Height = 36;
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    // 背景颜色
                    //dataGridView.BackgroundColor = Color.DarkSlateGray;

                    // 设置行头不显示
                    dataGridView.RowHeadersVisible = false;

                    // 设置用户不可增删行
                    dataGridView.AllowUserToAddRows = false;
                    dataGridView.AllowUserToDeleteRows = false;

                    // 设置选中模式
                    dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;

                    // 将 DataGridView 添加到 TabPage 中
                    tabPage.Controls.Add(dataGridView);
                }

                // 获取 DataGridView
                DataGridView dgv = tabPage.Controls.OfType<DataGridView>().FirstOrDefault();
                if (dgv == null)
                {
                    //如果找不到 DataGridview，抛出异常或者处理错误
                    throw new InvalidOperationException("找不到指定的显示表格");
                }

                // 遍历 NGList，将内容更新到 DataGridView 中
                foreach (var pair in e.AiResultData.DetectResults)
                {
                    // 查找是否已经存在该类型
                    var row = dgv.Rows.Cast<DataGridViewRow>().FirstOrDefault(r =>
                        r.Cells["DetectName"].Value?.ToString() == pair.Key);

                    bool isOk = pair.Value.TrueForAll(r => r.IsOk); // 当前检测项是否OK
                    string resultValue = string.Join(",", pair.Value.Select(r => r.Value));
                    if (row == null) // 如果不存在，则添加一行
                    {
                        int rowIndex = dgv.Rows.Add();
                        dgv.Rows[rowIndex].Cells["DetectName"].Value = pair.Key;
                        dgv.Rows[rowIndex].Cells["SingleResult"].Value = isOk ? "OK" : "NG";
                        dgv.Rows[rowIndex].Cells["SingleResult"].Style.BackColor = isOk ? Color.Green : Color.Red;
                        dgv.Rows[rowIndex].Cells["DetectResult"].Value = resultValue;
                        dgv.Rows[rowIndex].Cells["CountNG"].Value = isOk ? 0 : 1;
                        dgv.Rows[rowIndex].Cells["CountOK"].Value = isOk ? 1 : 0;
                        dgv.Rows[rowIndex].Cells["Total"].Value = 1; // 初始总数为1
                        dgv.Rows[rowIndex].Cells["Yield"].Value = isOk ? 100.00 + "%" : 0.00 + "%";// 良率
                    }
                    else // 如果存在，则更新“个数”和其他信息
                    {
                        row.Cells["SingleResult"].Value = isOk ? "OK" : "NG";
                        row.Cells["SingleResult"].Style.BackColor = isOk ? Color.Green : Color.Red;
                        row.Cells["DetectResult"].Value = resultValue;
                        int cNG = Convert.ToInt32(row.Cells["CountNG"].Value);
                        int cOK = Convert.ToInt32(row.Cells["CountOK"].Value);
                        int TotalNum = Convert.ToInt32(row.Cells["Total"].Value);
                        if (isOk)
                            row.Cells["CountOK"].Value = cOK + 1;
                        else
                            row.Cells["CountNG"].Value = cNG + 1;
                        // 更新其他列（总数、面积、分数等）如果需要的话
                        row.Cells["Total"].Value = TotalNum + 1; // 更新总数
                        row.Cells["Yield"].Value = (Convert.ToDouble(row.Cells["CountOK"].Value) / Convert.ToDouble(row.Cells["Total"].Value) * 100).ToString("F2") + "%";
                    }
                }

                #endregion
            }, null);

        }
    }
}
