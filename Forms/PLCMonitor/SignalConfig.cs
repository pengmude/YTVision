using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Node;

namespace YTVisionPro.Forms.PLCMonitor
{
    internal partial class SignalConfig : UserControl
    {
        public static List<string> StartPlcs = new List<string>();
        /// <summary>
        /// PLC监控对象
        /// </summary>
        PlcMonitor PLCMonitor = new PlcMonitor();
        /// <summary>
        /// 所有信号数据表
        /// </summary>
        DataTable DataTable = new DataTable();
        // 保存更新后的地址范围
        Dictionary<string, AddressRange> AddressRangesDic = new Dictionary<string, AddressRange>();
        /// <summary>
        /// 信号对应的字节索引
        /// </summary>
        Dictionary<string, List<SingleSignal>> SignalNameIndexDic = new Dictionary<string, List<SingleSignal>>();
        /// <summary>
        /// 绑定的plc
        /// </summary>
        IPlc _plc = null;

        public SignalConfig()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            DataTable.Columns.Add("信号地址", typeof(string));
            DataTable.Columns.Add("类型", typeof(string));
            DataTable.Columns.Add("长度", typeof(int));
            DataTable.Columns.Add("信号名称", typeof(string));

            //绑定数据源
            dataGridView1.DataSource = DataTable;
            AddDeleteButton();

            // 自动调整列宽以填充整个 DataGridView 宽度
            dataGridView1.ColumnAdded += DataGridView_ColumnAdded;


            SignalNameIndexDic["D"] = new List<SingleSignal>();
            SignalNameIndexDic["R"] = new List<SingleSignal>();
        }

        public void SetPlc(IPlc plc)
        {
            _plc = plc;
            PLCMonitor.SetPlc(plc);
        }

        // 添加删除按钮列
        private void AddDeleteButton()
        {
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.Name = "Delete";
            deleteButton.HeaderText = "操作";
            deleteButton.Text = "删除";
            deleteButton.UseColumnTextForButtonValue = true; // 设置为按钮显示文本
            dataGridView1.Columns.Add(deleteButton);

            // 绑定按钮点击事件
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        /// <summary>
        /// 点击删除一行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 判断是否是点击了"删除"按钮
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0 && e.RowIndex <= dataGridView1.Rows.Count - 1)
            {
                // 删除对应行
                dataGridView1.Rows.RemoveAt(e.RowIndex);

                // 更新地址范围
                CalculateAddressRanges(DataTable);
            }
        }

        /// <summary>
        /// 点击添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.IsNullOrEmpty() || textBox2.Text.IsNullOrEmpty())
            {
                MessageBox.Show("信号地址和名称不能为空！");
                return;
            }
            try
            {
                int.Parse(textBox1.Text);
                if(comboBox2.Text == "字符串")
                    int.Parse(textBox4.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("无效的数值！");
                return;
            }
            // 检查信号地址是否已存在于 DataTable 中
            bool exists = DataTable.AsEnumerable()
                .Any(row => row.Field<string>("信号地址") == comboBox1.Text + textBox1.Text || row.Field<string>("信号名称") == textBox2.Text);
            if (exists)
            {
                MessageBox.Show("信号地址或名称已存在！");
                return;
            }

            // TODO:检查地址是否被与已经添加的地址存在冲突


            DataRow newRow = DataTable.NewRow();
            newRow["信号地址"] = comboBox1.Text + textBox1.Text; 
            newRow["类型"] = comboBox2.Text;
            newRow["长度"] = comboBox2.Text == "布尔" ? 1 : comboBox2.Text == "整型" ? 2 : int.Parse(textBox4.Text);
            newRow["信号名称"] = textBox2.Text;
            DataTable.Rows.Add(newRow);

            try
            {
                // 更新结果
                UpdateSignals();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 更新信号
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void UpdateSignals()
        {
            CalculateAddressRanges(DataTable);
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            // 如果这是最后一列，则设置其 AutoSizeMode 为 Fill
            if (e.Column.Index == dataGridView1.Columns.Count - 1)
            {
                e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        /// <summary>
        /// 点击监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if(PLCMonitor.Tasks.Count == 0)
            {
                MessageBox.Show("请先添加监听的数据地址！");
                return;
            }
            SetEnable(true);
            StartPlcs.Add(_plc.UserDefinedName);
            PLCMonitor.Start();
        }
        /// <summary>
        /// 停止监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            SetEnable(false);
            StartPlcs.Remove(_plc.UserDefinedName);
            PLCMonitor.StopAllMonitoring();
        }

        /// <summary>
        /// 设置控件禁用状态
        /// </summary>
        /// <param name="start"></param>
        private void SetEnable(bool start)
        {
            dataGridView1.Enabled = !start;
            buttonAdd.Enabled = !start;
            buttonStop.Enabled = start;
            buttonStart.Enabled = !start;
        }

        /// <summary>
        /// 计算连续地址
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public void CalculateAddressRanges(DataTable dataTable)
        {
            AddressRangesDic.Clear();
            SignalNameIndexDic.Clear();
            List<SingleSignal> dSignals = new List<SingleSignal>();
            List<SingleSignal> rSignals = new List<SingleSignal>();

            foreach (DataRow row in dataTable.Rows)
            {
                string address = row.Field<string>("信号地址");
                string prefix = address.Substring(0, 1); // 获取前缀 R 或 D
                int number = int.Parse(address.Substring(1)); // 提取数字部分
                string dataType = row.Field<string>("类型");
                string name = row.Field<string>("信号名称");

                int length = 0;
                switch (dataType)
                {
                    case "布尔":
                        length = 1; // 1个单位长度可以存储16个bool值
                        break;
                    case "整型":
                        length = 2; // int占据2个单位长度
                        break;
                    case "字符串":
                        length = int.Parse(textBox4.Text); // 字符串长度转换为单位长度
                        break;
                    default:
                        throw new ArgumentException("未知的数据类型：" + dataType);
                }

                if (prefix == "D")
                    dSignals.Add(new SingleSignal(address, DataType.Bool, name, length));
                else if(prefix == "R")
                    rSignals.Add(new SingleSignal(address, DataType.Bool, name, length));

                // 如果D/R类型寄存器还没添加过
                if (!AddressRangesDic.ContainsKey(prefix))
                {
                    AddressRangesDic[prefix] = new AddressRange();
                    AddressRangesDic[prefix].Prefix = prefix;
                    AddressRangesDic[prefix].StartAddress = number;
                    AddressRangesDic[prefix].EndAddress = number + length - 1;
                    continue;
                }
                else
                {
                    // 添加的地址在已有的地址区间内,但长度超出最大地址
                    if (AddressRangesDic[prefix].StartAddress < number && number < AddressRangesDic[prefix].EndAddress && number + length > AddressRangesDic[prefix].EndAddress)
                    {
                        // TODO:不需要更新连续地址，但要做防呆处理——新添加的地址在现有的连续地址范围内，需要判断会不会与内部的地址重合
                        
                        // 更新最大地址
                        AddressRangesDic[prefix].EndAddress = number + length - 1;
                    }
                    // 添加的地址比最小地址小
                    else if (AddressRangesDic[prefix].StartAddress > number)
                    {
                        AddressRangesDic[prefix].StartAddress = number;
                    }
                    // 添加的地址比最大地址大
                    else if (AddressRangesDic[prefix].EndAddress < number)
                    {
                        AddressRangesDic[prefix].EndAddress = number + length - 1;
                    }
                }
            }

            SignalNameIndexDic["D"] = dSignals;
            SignalNameIndexDic["R"] = rSignals;

            //排序,为了按顺序解析数据
            SortAddresses(SignalNameIndexDic["D"]);
            SortAddresses(SignalNameIndexDic["R"]);

            // 添加监听任务
            PLCMonitor.Tasks.Clear();
            foreach (var range in AddressRangesDic)
            {
                PLCMonitor.AddMonitorTask(range.Key, range.Value);
                // 以下是调试信息
                MessageBox.Show($"类型: {range.Key}, \n首地址: {range.Value.StartAddress}, \n结束地址: {range.Value.EndAddress}, \n长度: {range.Value.Length}");
            }
        }

        public static int ExtractNumber(string address)
        {
            return int.Parse(address.Substring(1));
        }

        public static List<SingleSignal> SortAddresses(List<SingleSignal> addresses)
        {
            // 使用 LINQ 对列表进行排序
            var sortedAddresses = addresses.OrderBy(address => ExtractNumber(address.Adress)).ToList();
            return sortedAddresses;
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "字符串")
                textBox4.Enabled = true;
            else
                textBox4.Enabled = false;
        }

        /// <summary>
        /// 设置轮询时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                PLCMonitor.PollTime = int.Parse(textBox3.Text);
                MessageBox.Show($"成功设置轮询时间为{PLCMonitor.PollTime}ms！");
            }
            catch (Exception)
            {
                MessageBox.Show("无效的轮询时间！");
                return;
            }
        }
    }

    /// <summary>
    /// 单个信号
    /// </summary>
    public struct SingleSignal
    {
        /// <summary>
        /// 信号地址
        /// </summary>
        public string Adress;
        /// <summary>
        /// 信号类型
        /// </summary>
        public DataType DataType;
        /// <summary>
        /// 信号名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 数据长度（字符串有效）
        /// </summary>
        public int Length;

        public SingleSignal(string adress, DataType dataType, string name, int length = 0)
        {
            Adress = adress;
            DataType = dataType;
            Name = name;
            Length = length;
        }
    }
}
