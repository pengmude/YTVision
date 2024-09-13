using Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Node.PLC.Panasonic.HTDeepResultSend;

namespace YTVisionPro.Node.AI.HTAI
{
    internal partial class ParamFormHTAI : Form, INodeParamForm
    {

        // 模型句柄
        IntPtr TreePredictHandle = IntPtr.Zero;
        // 选择节点数
        int TestNum;
        // 选择初始化的节点信息
        List<NodeInfo> node_info_choose = new List<NodeInfo>();
        // 包含所有配置信息
        private List<NGTypeConfig> _allNgConfigs = new List<NGTypeConfig>();

        /// <summary>
        /// 节点参数
        /// </summary>
        public INodeParam Params { get; set; }

        public ParamFormHTAI()
        {
            InitializeComponent();
            NodeBase.NodeDeletedEvent += NodeHTAI_NodeDeletedEvent;
        }

        /// <summary>
        /// AI节点删除时候要释放句柄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeHTAI_NodeDeletedEvent(object sender, NodeBase e)
        {
            ReleaseAIHandle();
        }

        /// <summary>
        /// 用于节点参数界面需要订阅结果的情况调用
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 获取订阅的图片
        /// </summary>
        /// <returns></returns>
        public Bitmap GetImage()
        {
            return nodeSubscription1.GetValue<Bitmap>();
        }

        /// <summary>
        /// 提供主窗口等外部释放AI句柄资源
        /// </summary>
        public void ReleaseAIHandle()
        {
            if (TreePredictHandle != IntPtr.Zero)
            {
                HTAPI.ReleaseTree(TreePredictHandle);
                TreePredictHandle = IntPtr.Zero;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "tree文件|*.tree";
            openFileDialog1.Title = "选择模型文件";
            string modelOldPath = this.tbTreeFlie.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (modelOldPath != openFileDialog1.FileName)
                {
                    this.tbTreeFlie.Text = openFileDialog1.FileName;
                    InitNodeNamesToTreeView(this.tbTreeFlie.Text);
                }
            }
        }

        // 将.tree文件的节点名称填入树节点
        private void InitNodeNamesToTreeView(string config)
        {
 
            treeView1.Nodes.Clear();
            string treefile = File.ReadAllText(config);
            NodeInfos str = Newtonsoft.Json.JsonConvert.DeserializeObject<NodeInfos>(treefile);
            foreach (var item in str.NodeInfo)
            {
                TreeNode node = treeView1.Nodes.Add(item.NodeName);
            }
            foreach (TreeNode node in treeView1.Nodes)
            {
                node.Checked = true;
            }
        }

        // 初始化模型
        private void btInitTreeFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbTreeFlie.Text))
            {
                MessageBox.Show("未选择tree文件");
                LogHelper.AddLog(MsgLevel.Warn, "未选择tree文件！", true);
                return;
            }
            if (TreePredictHandle != IntPtr.Zero)
            {
                HTAPI.ReleaseTree(TreePredictHandle);
                TreePredictHandle = IntPtr.Zero;
            }

            // 项目模型配置文件
            string config = this.tbTreeFlie.Text;
            List<string> test_node_names = GetNodeNamesFromTreeView();
            // 选中节点的个数
            TestNum = test_node_names.Count();
            if (TestNum == 0)
            {
                MessageBox.Show("最少选择一个测试节点");
                LogHelper.AddLog(MsgLevel.Warn, "最少选择一个测试节点！", true);
                return;
            }
            // 节点名称数组格式的转换，转换成char[][256]的数组结构
            const int path_len = 256;
            byte[] byteArray_name = Enumerable.Repeat((byte)0x00, TestNum * path_len).ToArray();
            for (int i = 0; i < TestNum; i++)
            {
                byte[] name_byte = Encoding.Default.GetBytes(test_node_names[i]);
                for (int j = 0; j < (path_len - 1) && j < name_byte.Length; j++)
                {
                    byteArray_name[j + i * path_len] = name_byte[j];
                }
            }
            // 指定GPU
            string device = "GPU";
            // 指定GPU的ID,可参照任务管理器中的ID号
            int device_id = 0;
            int iRet = -1;
            iRet = HTAPI.InitTreeModel(ref TreePredictHandle, config, byteArray_name, TestNum, device, device_id);
            if (iRet != 0)
            {
                MessageBox.Show("加载模型出现错误,错误码: " + iRet);
                LogHelper.AddLog(MsgLevel.Warn, "加载模型出现错误,错误码:" + iRet, true);
                return;
            }
            MessageBox.Show("模型加载成功");
            LogHelper.AddLog(MsgLevel.Info, "模型加载成功", true);
            // 读取Tree文件里面的节点配置
            node_info_choose = new List<NodeInfo>();
            GetNodeInfoChoose(config, test_node_names);
            cbNodes.Items.Clear();
            cbNodes.Items.AddRange(node_info_choose.ConvertAll(n => n.NodeName).ToArray());
            cbNodes.SelectedIndex = 0; // 默认选中第一个节点
            cbClasses.DataSource = node_info_choose[0].ClassNames;
            InitializeNGConfigs();
        }

        // 得到选择的检测节点名称
        private List<string> GetNodeNamesFromTreeView()
        {
            List<string> node_names = new List<string>();
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Checked == true)
                {
                    node_names.Add(node.Text);
                }
            }
            return node_names;
        }

        // 获取选中的节点信息
        private void GetNodeInfoChoose(string config, List<string> test_node_names)
        {
            string treefile = File.ReadAllText(config);
            NodeInfos node_info = Newtonsoft.Json.JsonConvert.DeserializeObject<NodeInfos>(treefile);
            List<string> node_names = new List<string>();
            List<string[]> classnames = new List<string[]>();
            foreach (var item in node_info.NodeInfo)
            {
                classnames.Add(item.ClassNames);
                node_names.Add(item.NodeName);
            }
            int NodeNum = node_names.Count();
            for (int i = 0; i < NodeNum; i++)
            {
                // 只输出选中的节点信息
                if (test_node_names.Contains(node_names[i].ToLower()))
                {
                    node_info_choose.Add(node_info.NodeInfo[i]);
                }
            }
        }

        // 初始化NG配置表
        private void InitializeNGConfigs()
        {
            _allNgConfigs.Clear();
            foreach (var item in node_info_choose)
            {
                foreach (var classname in item.ClassNames)
                {
                    NGTypeConfig nGTypeConfig = new NGTypeConfig();
                    nGTypeConfig.NodeName = item.NodeName;
                    nGTypeConfig.ClassName = classname;
                    nGTypeConfig.MinArea = 0;
                    nGTypeConfig.MaxArea = 999999999;
                    nGTypeConfig.MinScore = 0.0f;
                    nGTypeConfig.MaxScore = 1.0f;
                    nGTypeConfig.MinNum = 1;
                    nGTypeConfig.MaxNum = 999;
                    nGTypeConfig.ForceOk = false;
                    _allNgConfigs.Add(nGTypeConfig);
                }
            }
        }

        // 节点信息下拉框选择
        private void cbNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedNode = cbNodes.SelectedItem.ToString();
            NodeInfo nodeInfo = node_info_choose.Find(n => n.NodeName == selectedNode);
            cbClasses.DataSource = nodeInfo.ClassNames;
            UpdateTbCbValues();
        }

        // 类别信息下拉框选择
        private void cbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 更新节点判别条件的值
            UpdateTbCbValues();
        }

        // 显示判别条件和ok栏
        private void UpdateTbCbValues()
        {
            string nodeName = cbNodes.SelectedItem.ToString();
            string className = cbClasses.SelectedItem.ToString();
            NGTypeConfig ngconfig = _allNgConfigs.Find(c => c.NodeName == nodeName && c.ClassName == className);

            if (ngconfig != null)
            {
                tbMinArea.Text = ngconfig.MinArea.ToString();
                tbMaxArea.Text = ngconfig.MaxArea.ToString();
                tbMinScore.Text = ngconfig.MinScore.ToString("0.000");
                tbMaxScore.Text = ngconfig.MaxScore.ToString("0.000");
                tbMinNum.Text = ngconfig.MinNum.ToString();
                tbMaxNum.Text = ngconfig.MaxNum.ToString();
                btIsOK.Checked = ngconfig.ForceOk;
            }
            else
            {
                // 如果找不到匹配的配置，可以设置默认值
                tbMinArea.Text = "0";
                tbMaxArea.Text = "999999999";
                tbMinScore.Text = "0.000";
                tbMaxScore.Text = "1.000";
                tbMinNum.Text = "1";
                tbMaxNum.Text = "999";
                btIsOK.Checked = false;
            }
        }

        // 判别条件修改时更新信息到对应的ngconfig
        private void Tb_TextChanged(object sender, EventArgs e)
        {
            string nodeName = cbNodes.SelectedItem.ToString();
            string className = cbClasses.SelectedItem.ToString();

            NGTypeConfig ngconfig = _allNgConfigs.Find(c => c.NodeName == nodeName && c.ClassName == className);

            if (ngconfig != null)
            {
                TextBox tb = (TextBox)sender;
                switch (tb.Name)
                {
                    case "tbMinArea":
                        if (int.TryParse(tb.Text, out int minArea))
                        {
                            ngconfig.MinArea = minArea;
                        }
                        break;
                    case "tbMaxArea":
                        if (long.TryParse(tb.Text, out long maxArea))
                        {
                            ngconfig.MaxArea = maxArea;
                        }
                        break;
                    case "tbMinScore":
                        if (float.TryParse(tb.Text, out float minScore))
                        {
                            ngconfig.MinScore = minScore;
                        }
                        break;
                    case "tbMaxScore":
                        if (float.TryParse(tb.Text, out float maxScore))
                        {
                            ngconfig.MaxScore = maxScore;
                        }
                        break;
                    case "tbMinNum":
                        if (int.TryParse(tb.Text, out int minNum))
                        {
                            ngconfig.MinNum = minNum;
                        }
                        break;
                    case "tbMaxNum":
                        if (int.TryParse(tb.Text, out int maxNum))
                        {
                            ngconfig.MaxNum = maxNum;
                        }
                        break;
                }
            }
        }

        //判别节点是否设置为OK
        private void cbIsOK_CheckedChanged(object sender, EventArgs e)
        {
            string nodeName = cbNodes.SelectedItem.ToString();
            string className = cbClasses.SelectedItem.ToString();

            NGTypeConfig ngconfig = _allNgConfigs.Find(c => c.NodeName == nodeName && c.ClassName == className);
            if (ngconfig != null)
            {
                if(btIsOK.Checked)
                    ngconfig.ForceOk = true;
                else
                    ngconfig.ForceOk = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(TreePredictHandle == null)
            {
                MessageBox.Show("模型尚未初始化！");
                return;
            }
            NodeParamHTAI nodeParamHTAI = new NodeParamHTAI();
            nodeParamHTAI.TreePredictHandle = TreePredictHandle;
            nodeParamHTAI.AllNgConfigs = _allNgConfigs;
            nodeParamHTAI.TestNum = TestNum;
            Params = nodeParamHTAI;
            Hide();
        }
    }
}
