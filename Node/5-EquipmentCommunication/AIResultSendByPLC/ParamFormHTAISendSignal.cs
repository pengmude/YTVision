using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._5_EquipmentCommunication.AIResultSendByPLC
{
    internal partial class ParamFormHTAISendSignal : Form, INodeParamForm
    {
        /// <summary>
        /// 信号列表
        /// </summary>
        private DataTable dataTable;

        List<NodeToClassName> _nodeToClassName;

        NodeParamHTAISendSignal nodeParamSendSignal = new NodeParamHTAISendSignal();

        public INodeParam Params { get; set; }

        public ParamFormHTAISendSignal()
        {
            InitializeComponent();
            InitializeDataTable();
            AddDeleteButton();
            Shown += ParamFormHTAISendSignal_Shown;
        }
        /// <summary>
        /// 每次显示界面都会刷新设备下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamFormHTAISendSignal_Shown(object sender, EventArgs e)
        {
            InitComboBox();
        }

        /// <summary>
        /// 给节点参数界面类设置所属的节点
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node) 
        { 
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 获取订阅的结果
        /// </summary>
        /// <returns></returns>
        public ResultViewData GetAiResult()
        {
            return nodeSubscription1.GetValue<ResultViewData>();
        }

        private void InitComboBox()
        {
            //初始PLC
            string text1 = comboBox2.Text;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("[未设置]");
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                comboBox2.Items.Add(plc.UserDefinedName);
            }
            int index1 = comboBox2.Items.IndexOf(text1);
            if (index1 == -1)
                comboBox2.SelectedIndex = 0;
            else
                comboBox2.SelectedIndex = index1;

            //初始NG等级
            comboBox4.Items.Clear();
            comboBox4.Items.Add(1);
            comboBox4.Items.Add(2);
            comboBox4.Items.Add(3);
            comboBox4.SelectedIndex = 0;
        }

        // 初始化DataTable
        private void InitializeDataTable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("PLC", typeof(string));
            dataTable.Columns.Add("节点", typeof(string));
            dataTable.Columns.Add("检测项", typeof(string));
            dataTable.Columns.Add("信号等级", typeof(int));
            dataTable.Columns.Add("信号地址", typeof(string));

            //绑定数据源
            dataGridView1.DataSource = dataTable;
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

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 判断是否是点击了"删除"按钮
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                // 删除对应行
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRowToDataTable(false);
        }

        /// <summary>
        /// 添加OK信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            AddRowToDataTable(true);
        }

        // 向DataTable中添加新行
        /// <summary>
        /// 添加NG信号
        /// </summary>
        private void AddRowToDataTable(bool OKOrNgSignal)
        {
            string NodeName;
            string ClassName;
            string SignalAddress;
            string PLC = comboBox2.Text;
            int SingnalLevel = Convert.ToInt32(comboBox4.Text);
            if (OKOrNgSignal)
            {
                NodeName = "OK";
                ClassName = "OK";
                SignalAddress = textBox2.Text;
            }
            else
            {
                NodeName = comboBox3.Text;
                ClassName = comboBox1.Text;
                SignalAddress = textBox1.Text;
            }

            bool exists = dataTable.AsEnumerable().Any(row =>
                row.Field<string>("PLC") == PLC
                && row.Field<string>("节点") == NodeName
                && row.Field<string>("检测项") == ClassName
            );

            if(string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show($"未选择深度学习文件路径");
                return;
            }
            if (exists)
            {
                MessageBox.Show("相同节点和缺陷类型的数据已经存在，无法重复添加！");
                return;
            }
            if(string.IsNullOrEmpty(SignalAddress))
            {
                MessageBox.Show($"{ClassName}信号地址为空");
                return;
            }
            DataRow newRow = dataTable.NewRow();
            newRow["PLC"] = PLC;
            newRow["节点"] = NodeName;
            newRow["检测项"] = ClassName;
            newRow["信号等级"] = SingnalLevel;
            newRow["信号地址"] = SignalAddress;
            dataTable.Rows.Add(newRow);
            UpdateDataTable();
        }
        
        private void UpdateDataTable()
        {           
            List<SignalRowData> dataList = new List<SignalRowData>();
            foreach(DataRow row in dataTable.Rows)
            {
                dataList.Add(new SignalRowData
                {
                    DevName = row["PLC"].ToString(),
                    NodeName = row["节点"].ToString(),
                    ClassName = row["检测项"].ToString(),
                    SignalLevel = Convert.ToInt32(row["信号等级"]),
                    SignalAddress = row["信号地址"].ToString()
                });
            }
            nodeParamSendSignal.Data = dataList;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "tree文件|*.tree";
            openFileDialog1.Title = "设置深度学习配置文件路径";
            string OldPath = this.textBox3.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName != OldPath)
                {
                    this.textBox3.Text = openFileDialog1.FileName;

                    string treefile = File.ReadAllText(this.textBox3.Text);
                    NodeInfos treeNode = Newtonsoft.Json.JsonConvert.DeserializeObject<NodeInfos>(treefile);
                    _nodeToClassName = new List<NodeToClassName>();

                    //初始深度学习节点
                    comboBox3.Items.Clear();
                    foreach (var item in treeNode.NodeInfo)
                    {
                        NodeToClassName nodeToClassName = new NodeToClassName();
                        nodeToClassName.NodeName = item.NodeName;
                        comboBox3.Items.Add(item.NodeName);
                        nodeToClassName.ClassNames = item.ClassNames;
                        _nodeToClassName.Add(nodeToClassName);
                    }

                    comboBox3.SelectedIndex = 0;
                    dataTable.Clear();
                } 
            }
        }

        /// <summary>
        /// 根据选择的节点更新缺陷类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in _nodeToClassName)
            {
                if(comboBox3.Text == item.NodeName)
                {
                    comboBox1.Items.Clear();
                    for (int i = 0; i < item.ClassNames.Length; i++)
                    {
                        comboBox1.Items.Add(item.ClassNames[i]);
                    }
                    comboBox1.SelectedIndex = 0;
                }
            }
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamHTAISendSignal param)
            {
                nodeParamSendSignal = param;
                nodeSubscription1.SetText(param.Text1, param.Text2);
                this.textBox3.Text = param.Path;
                string treefile = File.ReadAllText(this.textBox3.Text);
                NodeInfos treeNode = Newtonsoft.Json.JsonConvert.DeserializeObject<NodeInfos>(treefile);
                _nodeToClassName = new List<NodeToClassName>();

                //初始PLC
                comboBox2.Items.Clear();
                comboBox2.Items.Add("[未设置]");
                foreach (var plc in Solution.Instance.PlcDevices)
                {
                    comboBox2.Items.Add(plc.UserDefinedName);
                }
                int index1 = comboBox2.Items.IndexOf(param.PlcName);
                comboBox2.SelectedIndex = index1 == -1 ? 0 : index1;

                //初始深度学习节点
                comboBox3.Items.Clear();
                foreach (var item in treeNode.NodeInfo)
                {
                    NodeToClassName nodeToClassName = new NodeToClassName();
                    nodeToClassName.NodeName = item.NodeName;
                    comboBox3.Items.Add(item.NodeName);
                    nodeToClassName.ClassNames = item.ClassNames;
                    _nodeToClassName.Add(nodeToClassName);
                }

                comboBox3.SelectedIndex = 0;
                dataTable.Clear();
                textBox2.Text = param.OKPLC;
                textBox1.Text = param.NGPLC;
                textBoxStayTime.Text = param.StayTime.ToString();

                //数据的每一行
                foreach (var item in param.Data)
                {
                    DataRow newRow = dataTable.NewRow();
                    newRow["PLC"] = item.DevName;
                    newRow["节点"] = item.NodeName;
                    newRow["检测项"] = item.ClassName;
                    newRow["信号等级"] = item.SignalLevel;
                    newRow["信号地址"] = item.SignalAddress;
                    dataTable.Rows.Add(newRow);
                    UpdateDataTable();
                }
            }
        }

        private void ParamFormHTAISendSignal_Load(object sender, EventArgs e)
        {
            InitComboBox();
        }

        private void ParamFormHTAISendSignal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                nodeParamSendSignal.Text1 = nodeSubscription1.GetText1();
                nodeParamSendSignal.Text2 = nodeSubscription1.GetText2();
                nodeParamSendSignal.Path = this.textBox3.Text;
                nodeParamSendSignal.OKPLC = this.textBox2.Text;
                nodeParamSendSignal.NGPLC = this.textBox1.Text;
                nodeParamSendSignal.PlcName = this.comboBox2.Text;
                nodeParamSendSignal.StayTime = double.Parse(this.textBoxStayTime.Text);
                Params = nodeParamSendSignal;
            }
            catch (Exception)
            {
                MessageBox.Show("不合法的参数！");
                e.Cancel = true;
            }
        }
    }
}
