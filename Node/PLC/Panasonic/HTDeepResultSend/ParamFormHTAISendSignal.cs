using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.PLC.Panasonic.HTDeepResultSend
{
    internal partial class ParamFormHTAISendSignal : Form, INodeParamForm
    {
        /// <summary>
        /// 信号列表
        /// </summary>
        private DataTable dataTable;

        List<NodeToClassName> _nodeToClassName;

        public INodeParam Params { get; set; }

        public ParamFormHTAISendSignal()
        {
            InitializeComponent();
            InitComboBox();
            InitializeDataTable();
            AddDeleteButton();
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
        public AiResult GetAiResult()
        {
            return nodeSubscription1.GetValue<AiResult>();
        }

        private void InitComboBox()
        {
            //初始PLC
            comboBox2.Items.Clear();
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                comboBox2.Items.Add(plc.UserDefinedName);
            }
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
            //初始NG等级
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
            NodeParamHTAISendSignal nodeParamSendSignal = new NodeParamHTAISendSignal();

            List<SignalRowData> dataList = new List<SignalRowData>();
            foreach(DataRow row in dataTable.Rows)
            {
                dataList.Add(new SignalRowData
                {
                    UserNamePlc = row["PLC"].ToString(),
                    NodeName = row["节点"].ToString(),
                    ClassName = row["检测项"].ToString(),
                    SignalLevel = Convert.ToInt32(row["信号等级"]),
                    SignalAddress = row["信号地址"].ToString()
                });
            }
            nodeParamSendSignal.Data = dataList;
            Params = nodeParamSendSignal;
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
    }
}
