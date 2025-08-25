using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Logger;
using TDJS_Vision.Forms.SelectDetectItem;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._5_EquipmentCommunication.AIResultSend
{
    public partial class ParamFormSignalSend : FormBase, INodeParamForm
    {
        private NodeBase nodeBase;
        private Dictionary<string, DetectItemAddress> mapping;
        FormSelectDetectItem formSelectDetectItem = new FormSelectDetectItem();
        public ParamFormSignalSend()
        {
            InitializeComponent();
            InitDeviceComboBox();
            InitializeDataGridView();
            formSelectDetectItem.SendDetectItems += FormSelectDetectItem_SendDetectItems;
            toolTip1.SetToolTip(checkBoxReSet, "发送信号后在保持时间结束自动重置信号");
            toolTip1.SetToolTip(buttonAuto1, "清空信号列表，对当前勾选的每一项检测项，在信号列表中生成一行信号配置");
            toolTip1.SetToolTip(buttonAuto2, "清空信号列表，对当前勾选的所有检测项，在信号列表中仅生成一行信号配置");
            toolTip1.SetToolTip(buttonAdd1, "不清空信号列表，在原有信号列表上新增，被勾选的每一项检测项均在信号列表中生成一行信号配置");
            toolTip1.SetToolTip(buttonAdd2, "不清空信号列表，在原有信号列表上新增，被勾选的所有检测项只在信号列表中生成一行信号配置");
        }
        /// <summary>
        /// 更新检测项
        /// </summary>
        private void FormSelectDetectItem_SendDetectItems(object sender, List<string> e)
        {
            UpdataListBoxView(e);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            InitDeviceComboBox();
            try
            {
                UpdataListBoxView(formSelectDetectItem.GetSelectedItems());
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, $"获取订阅的AI配置失败！原因：{ex.Message}", true);
            }
        }

        public INodeParam Params { get; set; }
        /// <summary>
        /// 获取订阅的AI结果
        /// </summary>
        /// <returns></returns>
        public AlgorithmResult GetAiResult()
        {
            return nodeSubscription1.GetValue<AlgorithmResult>();
        }

        /// <summary>
        /// 构建“检测项-（Modbus设备，Modbus地址）”的映射字典
        /// </summary>
        /// <param name="bindingData"></param>
        /// <returns></returns>
        private Dictionary<string, DetectItemAddress> BuildMapping(BindingList<DetectItemRow> bindingData)
        {
            var mapping = new Dictionary<string, DetectItemAddress>(StringComparer.OrdinalIgnoreCase);

            foreach (var row in bindingData)
            {
                if (string.IsNullOrWhiteSpace(row.DetectItems))
                    continue;
                // .NET Framework 4.8 的 string.Split 方法不支持带 StringSplitOptions 的重载，只能用 char[] 作为分隔符。
                string[] items = row.DetectItems.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in items)
                {
                    string detectItem = item.Trim();
                    if (string.IsNullOrEmpty(detectItem))
                        continue;

                    mapping[detectItem] = new DetectItemAddress
                    {
                        DeviceName = row.DeviceName,
                        SignalAddress = row.SignalAddress,
                        OkValue = row.OkValue,
                        NgValue = row.NgValue,
                    };
                }
            }
            return mapping;
        }
        /// <summary>
        /// 获取检测项与Modbus设备地址的映射字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, DetectItemAddress> GetMapping()
        {
            return mapping;
        }
        /// <summary>
        /// 方案加载时节点参数界面数据初始化
        /// </summary>
        public void SetParam2Form()
        {
            if (Params is NodeParamSignalSend param)
            {
                // AI结果配置
                nodeSubscription1.SetText(param.Text1, param.Text2);
                checkBoxUseRegister.Checked = param.IsRegister;

                // 自动重置信号
                checkBoxReSet.Checked = param.IsAutoReset;

                // 信号保持时间
                textBoxHoldTime.Text = param.SignalHoldTime.ToString();

                // 设置信号列表绑定数据
                dataGridView1.DataSource = param.BindingData;
                mapping = BuildMapping(param.BindingData);
            }

        }
        /// <summary>
        /// 初始化Modbus下拉框
        /// </summary>
        private void InitDeviceComboBox()
        {
            string text1 = comboBoxDeviceList.Text;

            comboBoxDeviceList.Items.Clear();
            comboBoxDeviceList.Items.Add("[未设置]");
            // 初始化Modbus列表,只显示添加的Modbus用户自定义名称
            foreach (var device in Solution.Instance.AllDevices)
            {
                switch (device.DevType)
                {
                    case Device.DevType.PLC:
                    case Device.DevType.ModbusRTUPoll:
                    case Device.DevType.ModbusTcpPoll:
                        comboBoxDeviceList.Items.Add(device.UserDefinedName);
                        break;
                    default:
                        continue;
                }
            }
            int index1 = comboBoxDeviceList.Items.IndexOf(text1);
            comboBoxDeviceList.SelectedIndex = index1 == -1 ? 0 : index1;
        }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
            nodeSubscription1.Init(node);
            nodeBase = node;
        }

        /// <summary>
        /// 更新检测项列表视图
        /// </summary>
        /// <param name="aIInputInfo"></param>
        private void UpdataListBoxView(List<string> detectItems)
        {
            try
            {
                checkedListBox1.Items.Clear();

                foreach (var item in detectItems)
                {
                    // 添加项到 CheckedListBox
                    checkedListBox1.Items.Add(item, true); // 添加名称并设置初始选中状态
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"刷新检测项配置失败！原因：{ex.Message}", true);
            }
        }
        /// <summary>
        /// 自动配置1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            GenerateDataGridViewData1();
        }
        /// <summary>
        /// 自动配置2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            GenerateDataGridViewData2();
        }
        /// <summary>
        /// 增量配置1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            GenerateDataGridViewDataInc1();
        }
        /// <summary>
        /// 增量配置2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd2_Click(object sender, EventArgs e)
        {
            GenerateDataGridViewDataInc2();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                var param = new NodeParamSignalSend();
                param.Text1 = nodeSubscription1.GetText1();
                param.Text2 = nodeSubscription1.GetText2();
                //param.ModbusName = comboBoxModBusList.Text;
                param.IsRegister = checkBoxUseRegister.Checked;
                //param.ModbusDevice = Solution.Instance.ModbusDevices
                //    .FirstOrDefault(dev => dev.UserDefinedName == param.ModbusName);
                param.IsAutoReset = checkBoxReSet.Checked;
                param.SignalHoldTime = int.Parse(textBoxHoldTime.Text);
                param.BindingData = dataGridView1.DataSource as BindingList<DetectItemRow>;
                mapping = BuildMapping(param.BindingData);
                Params = param;
                Hide();
            }
            catch (Exception ex)
            {
                MessageBoxTD.Show($"参数保存异常！原因:{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 初始化DataGridView控件
        /// </summary>
        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            var color = Color.SteelBlue;

            // 列1：检测项（只读）
            var detectItemsColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DetectItems",
                HeaderText = "检测项",
                Name = "DetectItemsColumn",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true
            };
            detectItemsColumn.HeaderCell.Style.BackColor = color;
            dataGridView1.Columns.Add(detectItemsColumn);

            // 列2：通信设备（只读）
            var commModbusColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DeviceName",
                HeaderText = "通信设备",
                Name = "DeviceNameColumn",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true
            };
            commModbusColumn.HeaderCell.Style.BackColor = color;
            dataGridView1.Columns.Add(commModbusColumn);

            // 列3：信号地址（可编辑）
            var signalAddressColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SignalAddress",
                HeaderText = "信号地址",
                Name = "SignalAddressColumn",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = false
            };
            signalAddressColumn.HeaderCell.Style.BackColor = color;
            dataGridView1.Columns.Add(signalAddressColumn);

            // 列4：OK发送值（可编辑文本框）
            var okValueColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OkValue",
                HeaderText = "OK发送值",
                Name = "OkValueColumn",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = false,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            okValueColumn.HeaderCell.Style.BackColor = color;
            dataGridView1.Columns.Add(okValueColumn);

            // 列5：NG发送值（可编辑文本框）
            var ngValueColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgValue",
                HeaderText = "NG发送值",
                Name = "NgValueColumn",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = false,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            ngValueColumn.HeaderCell.Style.BackColor = color;
            dataGridView1.Columns.Add(ngValueColumn);
        }

        /// <summary>
        /// 自动配置1的数据源构造
        /// </summary>
        private void GenerateDataGridViewData1()
        {
            BindingList<DetectItemRow> dataSource = new BindingList<DetectItemRow>();

            int signalAddressCounter = 0;

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    var itemText = checkedListBox1.Items[i].ToString();

                    dataSource.Add(new DetectItemRow
                    {
                        DetectItems = itemText,
                        DeviceName = comboBoxDeviceList.Text,
                        SignalAddress = signalAddressCounter.ToString(),
                        OkValue = 1,
                        NgValue = 0,
                    });

                    signalAddressCounter++;
                }
            }

            dataGridView1.DataSource = dataSource;
        }
        /// <summary>
        /// 自动配置2的数据源构造
        /// </summary>
        private void GenerateDataGridViewData2()
        {
            BindingList<DetectItemRow> dataSource = new BindingList<DetectItemRow>();
            string names = "";
            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                var itemText = checkedListBox1.CheckedItems[i].ToString();
                names += itemText;
                if (i != (checkedListBox1.CheckedItems.Count - 1))
                    names += ";";
            }
            dataSource.Add(new DetectItemRow
            {
                DetectItems = names,
                DeviceName = comboBoxDeviceList.Text,
                SignalAddress = "0",
                OkValue = 1,
                NgValue= 0,
            });
            dataGridView1.DataSource = dataSource;
        }
        /// <summary>
        /// 增量配置1的数据源构造
        /// </summary>
        private void GenerateDataGridViewDataInc1()
        {
            var dataSource = dataGridView1.DataSource as BindingList<DetectItemRow>;
            int signalAddressCounter = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    var itemText = checkedListBox1.Items[i].ToString();

                    dataSource.Add(new DetectItemRow
                    {
                        DetectItems = itemText,
                        DeviceName = comboBoxDeviceList.Text,
                        SignalAddress = signalAddressCounter.ToString(),
                        OkValue = 1,
                        NgValue = 0,
                    });

                    signalAddressCounter++;
                }
            }
            dataGridView1.DataSource = dataSource;
        }
        /// <summary>
        /// 增量配置2的数据源构造
        /// </summary>
        private void GenerateDataGridViewDataInc2()
        {
            var dataSource = dataGridView1.DataSource as BindingList<DetectItemRow>;
            string names = "";
            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                var itemText = checkedListBox1.CheckedItems[i].ToString();
                names += itemText;
                if (i != (checkedListBox1.CheckedItems.Count - 1))
                    names += ";";
            }
            dataSource.Add(new DetectItemRow
            {
                DetectItems = names,
                DeviceName = comboBoxDeviceList.Text,
                SignalAddress = "0",
                OkValue = 1,
                NgValue = 0,
            });
            dataGridView1.DataSource = dataSource;
        }
        /// <summary>
        /// 一键全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAutoSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoSelect.Checked)
            {
                // 全选
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            else
            {
                // 取消全选
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        /// <summary>
        /// 选择检测项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                formSelectDetectItem.ShowDialog();
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"获取订阅的AI配置失败！原因：{ex.Message}", true);
            }
        }
    }

    /// <summary>
    /// 信号列表数据源类
    /// </summary>
    public class DetectItemRow
    {
        public string DetectItems { get; set; }      // 检测项名称（多个用分号隔开）
        public string DeviceName { get; set; }       // 设备名
        public string SignalAddress { get; set; }    // 信号地址
        public ushort OkValue { get; set; }          // OK时发送的值
        public ushort NgValue { get; set; }          // NG时发送的值
    }
}
