using Logger;
using Sunny.UI;
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
using YTVisionPro.Forms.ImageViewer;
using YTVisionPro.Node.PLC.Panasonic.HTDeepResultSend;

namespace YTVisionPro.Node.AI.HTAI
{
    internal partial class ParamFormHTAI : Form, INodeParamForm
    {
        private NodeBase _node = new NodeBase();
        const int path_len = 256;
        /// <summary>
        /// 模型句柄
        /// </summary>
        IntPtr TreePredictHandle = IntPtr.Zero;
        /// <summary>
        /// 选择节点数
        /// </summary>
        int TestNum;
        /// <summary>
        /// 选择初始化的节点信息
        /// </summary>
        List<NodeInfo> node_info_choose = new List<NodeInfo>();
        /// <summary>
        /// 包含所有配置信息
        /// </summary>
        private List<NGTypeConfig> _allNgConfigs = new List<NGTypeConfig>();
        /// <summary>
        /// 节点参数
        /// </summary>
        public INodeParam Params { get; set; }

        NodeParamHTAI nodeParamHTAI = new NodeParamHTAI();

        public ParamFormHTAI()
        {
            InitializeComponent();
            NodeBase.NodeDeletedEvent += NodeHTAI_NodeDeletedEvent;
            UpdateWinNameList(FrmImageViewer.CurWindowsNum);
            WindowNameList.SelectedIndex = 0;
            CanvasSet.WindowNumChangeEvent += CanvasSet_WindowNumChangeEvent;
        }

        private void CanvasSet_WindowNumChangeEvent(object sender, int e)
        {
            UpdateWinNameList(e);
            WindowNameList.SelectedIndex = 0;
        }

        private void UpdateWinNameList(int num)
        {
            WindowNameList.Items.Clear();
            WindowNameList.Items.Add("[未设置]");
            for (int i = 0; i < num; i++)
            {
                WindowNameList.Items.Add($"图像窗口{i + 1}");
            }
        }

        /// <summary>
        /// AI节点删除时候要释放句柄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeHTAI_NodeDeletedEvent(object sender, NodeBase e)
        {
            if (e.ParamForm.Equals(this))
                ReleaseAIHandle();
        }

        /// <summary>
        /// 用于节点参数界面需要订阅结果的情况调用
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
            _node = node;
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
        private void InitNodeNamesToTreeView(string config, bool isDeserialize = false)
        {
 
            treeView1.Nodes.Clear();
            string treefile = File.ReadAllText(config);
            NodeInfos str = Newtonsoft.Json.JsonConvert.DeserializeObject<NodeInfos>(treefile);
            foreach (var item in str.NodeInfo)
            {
                TreeNode node = treeView1.Nodes.Add(item.NodeName);
                // 不是反序列化时初始化的树节点就默认设置选中
                // 如果是反序列化的话就要根据反序列化的配置去设置选中状态
                if(!isDeserialize)
                    node.Checked = true;
                else
                {
                    if(Params is NodeParamHTAI param)
                    {
                        // 加载的配置是选中的才选中
                        if(param.ModelInitParams.NodeNames.Contains(item.NodeName))
                            node.Checked = true;
                    }
                }
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
            byte[] byteArray_name = StringList2ByteArr(test_node_names);
            // 指定GPU
            string device = "GPU";
            // 指定GPU的ID,可参照任务管理器中的ID号
            int device_id = 0;
            int iRet = -1;
            nodeParamHTAI.ModelInitParams = new ModelInitParams(config, test_node_names, TestNum, device, device_id);
            iRet = HTAPI.InitTreeModel(ref TreePredictHandle, config, byteArray_name, TestNum, device, device_id);
            if (iRet != 0)
            {
                MessageBox.Show($"节点({_node.NodeName})加载模型出现错误!错误码: " + iRet);
                LogHelper.AddLog(MsgLevel.Exception, "加载模型出现错误!错误码:" + iRet, true);
                return;
            }
            LogHelper.AddLog(MsgLevel.Info, $"节点({_node.NodeName})模型加载成功!", true);
            // 读取Tree文件里面的节点配置
            node_info_choose = new List<NodeInfo>();
            GetNodeInfoChoose(config, test_node_names);
            cbNodes.Items.Clear();
            cbNodes.Items.AddRange(node_info_choose.ConvertAll(n => n.NodeName).ToArray());
            cbNodes.SelectedIndex = 0; // 默认选中第一个节点
            cbClasses.DataSource = node_info_choose[0].ClassNames;
            InitializeNGConfigs();
        }

        private byte[] StringList2ByteArr(List<string> test_node_names)
        {
            byte[] byteArray_name = Enumerable.Repeat((byte)0x00, TestNum * path_len).ToArray();
            for (int i = 0; i < TestNum; i++)
            {
                byte[] name_byte = Encoding.Default.GetBytes(test_node_names[i]);
                for (int j = 0; j < (path_len - 1) && j < name_byte.Length; j++)
                {
                    byteArray_name[j + i * path_len] = name_byte[j];
                }
            }
            return byteArray_name;
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

        /// <summary>
        /// 根据节点名字节数组转为节点字符串列表
        /// </summary>
        /// <param name="byteArray_name"></param>
        /// <param name="TestNum"></param>
        /// <param name="path_len"></param>
        /// <returns></returns>
        private List<string> ConvertByteArrayToList(byte[] byteArray_name, int TestNum, int path_len)
        {
            List<string> test_node_names = new List<string>();

            for (int i = 0; i < TestNum; i++)
            {
                // 提取当前字符串的字节数组
                byte[] name_byte = new byte[path_len];
                Array.Copy(byteArray_name, i * path_len, name_byte, 0, path_len);

                // 找到第一个空字符的位置，以确定实际字符串长度
                int nullIndex = Array.IndexOf(name_byte, (byte)0x00);
                if (nullIndex == -1)
                {
                    nullIndex = path_len; // 如果没有找到空字符，则使用整个路径长度
                }

                // 将字节数组转换为字符串
                string nodeName = Encoding.Default.GetString(name_byte, 0, nullIndex);
                test_node_names.Add(nodeName);
            }

            return test_node_names;
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
            if (cbNodes.SelectedItem == null)
                return;
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
            if (cbNodes.SelectedItem == null)
                return;
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
            if(nodeSubscription1.GetText1().IsNullOrEmpty() || nodeSubscription1.GetText2().IsNullOrEmpty())
            {
                MessageBox.Show("尚未订阅图像！");
                return;
            }
            if (TreePredictHandle == IntPtr.Zero)
            {
                MessageBox.Show("模型尚未初始化！");
                return;
            }
            
            nodeParamHTAI.TreePredictHandle = TreePredictHandle;
            nodeParamHTAI.Text1 = nodeSubscription1.GetText1().Split('.')[1];
            nodeParamHTAI.Text2 = nodeSubscription1.GetText2();
            nodeParamHTAI.TreePath = tbTreeFlie.Text;
            nodeParamHTAI.AllNgConfigs = _allNgConfigs;
            nodeParamHTAI.TestNum = TestNum;
            nodeParamHTAI.WindowName = WindowNameList.Text;
            Params = nodeParamHTAI;
            Hide();
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamHTAI param)
            {
                // 还原界面显示
                nodeSubscription1.SetText1(param.Text1);  // TODO:这里设置订阅的文本不成功
                nodeSubscription1.SetText2(param.Text2);
                tbTreeFlie.Text = param.TreePath;
                InitNodeNamesToTreeView(param.TreePath, true);
                UpdateWinNameList(FrmImageViewer.CurWindowsNum);
                WindowNameList.Text = param.WindowName;

                // 还原私有成员或序列化忽略的属性
                nodeParamHTAI = param;
                TestNum = param.TestNum;
                _allNgConfigs = param.AllNgConfigs;
                //采取非阻塞方式加载模型（加载模型耗时长）
                Task.Run(() =>
                {
                    TreePredictHandle = InitModel(param.ModelInitParams.TreePath, param.ModelInitParams.NodeNames,
                                        param.ModelInitParams.TestNodeNum, param.ModelInitParams.DeviceType, param.ModelInitParams.DeviceID);
                    param.TreePredictHandle = TreePredictHandle;
                });

            }
        }

        private IntPtr InitModel(string treePath, List<string> byteArray_name, int testNum, string deviceType, int deviceID)
        {
            int iRet = HTAPI.InitTreeModel(ref TreePredictHandle, treePath, StringList2ByteArr(byteArray_name), testNum, deviceType, deviceID);
            if (iRet != 0)
            {
                MessageBox.Show($"节点({_node.ID}.{_node.NodeName})加载模型出现错误!错误码: " + iRet);
                LogHelper.AddLog(MsgLevel.Exception, $"节点({_node.ID}.{_node.NodeName})加载模型出现错误!错误码:" + iRet, true);
                return IntPtr.Zero;
            }
            LogHelper.AddLog(MsgLevel.Info, $"节点({_node.ID}.{_node.NodeName})模型加载成功!", true);

            // 读取Tree文件里面的节点配置
            node_info_choose = new List<NodeInfo>();
            GetNodeInfoChoose(treePath, byteArray_name);

            // 更新节点名称和类别名称下拉框
            try
            {
                cbNodes.Items.Clear();
                cbNodes.Items.AddRange(node_info_choose.ConvertAll(n => n.NodeName).ToArray());
                cbNodes.SelectedIndex = 0; // 默认选中第一个节点
                cbClasses.DataSource = node_info_choose[0].ClassNames;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({_node.ID}.{_node.NodeName})设置参数失败！请手动加载模型！原因：{ex.Message}");
            }

            return TreePredictHandle;
        }
    }
}
