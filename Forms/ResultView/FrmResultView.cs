using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using YTVisionPro.Node.Tool.DataShow;

namespace YTVisionPro.Forms.ResultView
{
    internal partial class FrmResultView : DockContent
    {
        /// <summary>
        /// 图像窗口隐藏事件
        /// </summary>
        public event EventHandler HideChangedEvent;

        public FrmResultView()
        {
            InitializeComponent();
            this.tabControl1.TabPages.Clear();
            NodeDataShow.DataShow += NodeDataShow_DataShow;
        }

        private void FrmResultView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;  // 取消关闭事件，防止窗口关闭
            this.Hide(); // 隐藏窗口
            HideChangedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void NodeDataShow_DataShow(object sender, DatashowData e)
        {
            // 查找或创建 TabPage
            TabPage tabPage = tabControl1.TabPages[e.TabPageName];        
            if (tabPage == null)
            {
                tabPage = new TabPage(e.TabPageName);
                tabPage.Name = e.TabPageName;
                tabControl1.TabPages.Add(tabPage);

                // 创建 DataGridView 并设置它的 Dock 属性为 Fill，以便充满整个 TabPage
                DataGridView dataGridView = new DataGridView();
                dataGridView.ReadOnly = true;
                dataGridView.Name = "dataGridView_" + e.TabPageName; // 为 DataGridView 设置唯一的 Name
                dataGridView.Dock = DockStyle.Fill;

                // 为 DataGridView 添加列
                dataGridView.Columns.Add("NodeName", "节点(AI)");
                dataGridView.Columns.Add("ClassName", "类型(AI)");
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

                // 将 DataGridView 添加到 TabPage 中
                tabPage.Controls.Add(dataGridView);
            }

            // 获取 DataGridView
            DataGridView dgv = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(c => c.Name == "dataGridView_" + e.TabPageName);
            if (dgv == null)
            {
                //如果找不到 DataGridview，抛出异常或者处理错误
                throw new InvalidOperationException("找不到指定的显示表格");
            }

            // 遍历 NGList，将内容更新到 DataGridView 中
            foreach (var SingleDetectResult in e.AiResultData.SingleRowDataList)
            {
                // 查找是否已经存在该类型
                var row = dgv.Rows.Cast<DataGridViewRow>().FirstOrDefault(r =>
                    r.Cells["NodeName"].Value?.ToString() == SingleDetectResult.NodeName &&
                    r.Cells["ClassName"].Value?.ToString() == SingleDetectResult.ClassName &&
                    r.Cells["DetectName"].Value?.ToString() == SingleDetectResult.DetectName);

                if (row == null) // 如果不存在，则添加一行
                {
                    int rowIndex = dgv.Rows.Add();
                    dgv.Rows[rowIndex].Cells["NodeName"].Value = SingleDetectResult.NodeName;
                    dgv.Rows[rowIndex].Cells["ClassName"].Value = SingleDetectResult.ClassName;
                    dgv.Rows[rowIndex].Cells["DetectName"].Value = SingleDetectResult.DetectName;
                    dgv.Rows[rowIndex].Cells["SingleResult"].Value = SingleDetectResult.IsOk ? "OK" : "NG";
                    dgv.Rows[rowIndex].Cells["SingleResult"].Style.BackColor = SingleDetectResult.IsOk ? Color.Green : Color.Red;
                    dgv.Rows[rowIndex].Cells["DetectResult"].Value = SingleDetectResult.DetectResult;
                    dgv.Rows[rowIndex].Cells["CountNG"].Value = SingleDetectResult.IsOk ? 0 : 1;
                    dgv.Rows[rowIndex].Cells["CountOK"].Value = SingleDetectResult.IsOk ? 1 : 0;
                    dgv.Rows[rowIndex].Cells["Total"].Value = 1; // 初始总数为1
                    dgv.Rows[rowIndex].Cells["Yield"].Value = SingleDetectResult.IsOk ? 100.00 + "%" :  0.00 + "%";// 良率
                } 
                else // 如果存在，则更新“个数”和其他信息
                {
                    row.Cells["SingleResult"].Value = SingleDetectResult.IsOk ? "OK" : "NG";
                    row.Cells["SingleResult"].Style.BackColor = SingleDetectResult.IsOk ? Color.Green : Color.Red;
                    row.Cells["DetectResult"].Value = SingleDetectResult.DetectResult;
                    int cNG = Convert.ToInt32(row.Cells["CountNG"].Value);
                    int cOK = Convert.ToInt32(row.Cells["CountOK"].Value);
                    int TotalNum = Convert.ToInt32(row.Cells["Total"].Value);
                    if (SingleDetectResult.IsOk)
                        row.Cells["CountOK"].Value = cOK + 1;
                    else
                        row.Cells["CountNG"].Value = cNG + 1;
                    // 更新其他列（总数、面积、分数等）如果需要的话
                    row.Cells["Total"].Value = TotalNum + 1; // 更新总数
                    row.Cells["Yield"].Value = (Convert.ToDouble(row.Cells["CountOK"].Value) / Convert.ToDouble(row.Cells["Total"].Value) * 100).ToString("F2") + "%";
                }
            }
        }
    }
}
