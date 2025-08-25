using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logger;
using Sunny.UI;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Forms.SolRunParam
{
    public partial class MyDataGridViewForm : UserControl
    {
        public MyDataGridViewForm()
        {
            InitializeComponent();
            Init();
        }
        /// <summary>
        /// 初始化数据表格
        /// </summary>
        private void Init()
        {
            // 清空已有列
            dataGridView1.Columns.Clear();

            // 第一列：是否启用该检测项
            var checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "是否启用",
                Name = "Enable",
                Width = 32,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            };
            dataGridView1.Columns.Add(checkBoxColumn);

            // 第二列：检测项名称
            var label1Column = new DataGridViewTextBoxColumn
            {
                HeaderText = "检测项",
                ReadOnly = true,
                Name = "Name",
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            };
            dataGridView1.Columns.Add(label1Column);

            // 第三列：检测项的下限值
            var textBox1Column = new DataGridViewTextBoxColumn
            {
                HeaderText = "下限值",
                Name = "MinValue",
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            };
            dataGridView1.Columns.Add(textBox1Column);

            // 第四列：检测项的当前值
            var label2Column = new DataGridViewTextBoxColumn
            {
                ReadOnly = true,
                HeaderText = "当前值",
                Name = "Value",
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            };
            dataGridView1.Columns.Add(label2Column);

            // 第五列：检测项的上限值
            var textBox2Column = new DataGridViewTextBoxColumn
            {
                HeaderText = "上限值",
                Name = "MaxValue",
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            };
            dataGridView1.Columns.Add(textBox2Column);

            // 第六列：是否属于统计个数类的检测项
            var checkBox2Column = new DataGridViewCheckBoxColumn
            {
                HeaderText = "数量型",
                Name = "Enable",
                Width = 32,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            };
            dataGridView1.Columns.Add(checkBox2Column);

            // 设置列标题的高度
            dataGridView1.ColumnHeadersHeight = 64;
            // 设置行的高度
            dataGridView1.RowTemplate.Height = 36;
            //等分表格列宽
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        /// <summary>
        /// 加载检测项
        /// </summary>
        /// <param name="info"></param>
        public void LoadConfig(List<DetectItemInfo> infos)
        {
            dataGridView1.Rows.Clear();
            if (infos == null) return;
            foreach (var info in infos)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView1);

                row.Cells[0].Value = info.Enable;
                row.Cells[1].Value = info.Name;
                row.Cells[2].Value = info.MinValue;
                row.Cells[3].Value = info.CurValue;
                row.Cells[4].Value = info.MaxValue;
                row.Cells[5].Value = info.IsCountItem;

                dataGridView1.Rows.Add(row);
            }
        }

        /// <summary>
        /// 从表格中获取检测项配置
        /// </summary>
        /// <returns></returns>
        public List<DetectItemInfo> GetConfig()
        {
            dataGridView1.EndEdit();
            var infos = new List<DetectItemInfo>();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;
                    var info = new DetectItemInfo
                    {
                        Enable = Convert.ToBoolean(row.Cells[0].Value),
                        Name = row.Cells[1].Value.ToString(),
                        MinValue = row.Cells[2].Value.ToString(),
                        CurValue = row.Cells[3].Value.ToString(),
                        MaxValue = row.Cells[4].Value.ToString(),
                        IsCountItem = Convert.ToBoolean(row.Cells[5].Value)
                    };
                    infos.Add(info);
                }
            }
            catch (Exception e)
            {
                LogHelper.AddLog(MsgLevel.Exception, e.Message, true);
                return null;
            }
            return infos;
        }
    }
}
