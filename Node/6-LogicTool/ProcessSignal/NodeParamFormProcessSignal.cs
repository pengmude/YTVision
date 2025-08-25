using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sunny.UI;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Node._6_LogicTool.ProcessSignal
{
    public partial class NodeParamFormProcessSignal : FormBase, INodeParamForm
    {
        /// <summary>
        /// 新建信号事件，参数为信号名称
        /// </summary>
        public static EventHandler<string> NewSignalEvent;
        /// <summary>
        /// 删除信号事件，参数为信号名称
        /// </summary>
        public static EventHandler<string> DeleteSignalEvent;

        public NodeParamFormProcessSignal()
        {
            InitializeComponent();
            NewSignalEvent += NodeParamFormProcessSignal_NewSignalEvent;
            DeleteSignalEvent += NodeParamFormProcessSignal_DeleteSignalEvent;
            Shown += NodeParamFormProcessSignal_Shown;
        }

        private void NodeParamFormProcessSignal_Shown(object sender, EventArgs e)
        {
            // 保存刷新前的选中项（如果有的话）
            string selectedSend = comboBoxSendSignal.SelectedItem as string;
            string selectedWait = comboBoxWaitSignal.SelectedItem as string;

            // 清空并重新加载
            comboBoxSendSignal.Items.Clear();
            comboBoxWaitSignal.Items.Clear();

            foreach (var signal in Solution.Instance.ProcessSignalDic)
            {
                comboBoxSendSignal.Items.Add(signal.Key);
                comboBoxWaitSignal.Items.Add(signal.Key);
            }

            // 恢复选中项（仅当项仍然存在时）
            if (!string.IsNullOrEmpty(selectedSend) && comboBoxSendSignal.Items.Contains(selectedSend))
            {
                comboBoxSendSignal.SelectedItem = selectedSend;
            }
            // 否则保持未选中（不需要 else 设置为 null，因为 Clear 后默认就是 null）

            if (!string.IsNullOrEmpty(selectedWait) && comboBoxWaitSignal.Items.Contains(selectedWait))
            {
                comboBoxWaitSignal.SelectedItem = selectedWait;
            }
        }

        private void NodeParamFormProcessSignal_DeleteSignalEvent(object sender, string e)
        {
            if (comboBoxSendSignal.Items.Contains(e))
                comboBoxSendSignal.Items.Remove(e);
            if (comboBoxWaitSignal.Items.Contains(e))
                comboBoxWaitSignal.Items.Remove(e);
        }

        /// <summary>
        /// 刷新信号列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void NodeParamFormProcessSignal_NewSignalEvent(object sender, string e)
        {
            if (!comboBoxSendSignal.Items.Contains(e))
                comboBoxSendSignal.Items.Add(e);
            if (!comboBoxWaitSignal.Items.Contains(e))
                comboBoxWaitSignal.Items.Add(e);
        }

        public INodeParam Params { get; set; }

        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NodeParamProcessSignal nodeParamProcessSignal = new NodeParamProcessSignal();
                nodeParamProcessSignal.NewSignalName = textBoxSignalName.Text;
                nodeParamProcessSignal.NewSignalSendTimes = textBoxSendTimes.Text.IsNullOrEmpty() ? 0 : int.Parse(textBoxSendTimes.Text);
                nodeParamProcessSignal.SendSignalName = textBoxSignalName.Text;
                nodeParamProcessSignal.IsSendSignal = radioButtonSendSignal.Checked;
                nodeParamProcessSignal.SendSignalName = comboBoxSendSignal.Text;
                nodeParamProcessSignal.WaitSignalName = comboBoxWaitSignal.Text;
                Params = nodeParamProcessSignal;
            }
            catch (Exception)
            {
                MessageBoxTD.Show("参数设置异常！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Hide();
        }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if(Params is NodeParamProcessSignal param)
            {
                if (!param.NewSignalName.IsNullOrEmpty() && !Solution.Instance.ProcessSignalDic.ContainsKey(param.NewSignalName))
                {
                    Solution.Instance.ProcessSignalDic.Add(param.NewSignalName, new System.Threading.CountdownEvent(param.NewSignalSendTimes));
                }
                // 清空并重新加载
                comboBoxSendSignal.Items.Clear();
                comboBoxWaitSignal.Items.Clear();

                foreach (var signal in Solution.Instance.ProcessSignalDic)
                {
                    comboBoxSendSignal.Items.Add(signal.Key);
                    comboBoxWaitSignal.Items.Add(signal.Key);
                }
                textBoxSignalName.Text = param.NewSignalName ?? string.Empty;
                textBoxSendTimes.Text = param.NewSignalSendTimes.ToString();
                radioButtonSendSignal.Checked = param.IsSendSignal;
                radioButtonWaitSignal.Checked = !param.IsSendSignal;
                comboBoxSendSignal.SelectedItem = param.SendSignalName ?? string.Empty;
                comboBoxWaitSignal.SelectedItem = param.WaitSignalName ?? string.Empty;
            }
        }
        /// <summary>
        /// 创建新的信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewSignal_Click(object sender, EventArgs e)
        {
            if(textBoxSignalName.Text.IsNullOrEmpty() || textBoxSendTimes.Text.IsNullOrEmpty())
            {
                MessageBoxTD.Show("信号名称或发送次数不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Solution.Instance.ProcessSignalDic.ContainsKey(textBoxSignalName.Text))
            {
                var result = MessageBoxTD.Show(
                    "信号名称已存在，是否覆盖？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    var signal = Solution.Instance.ProcessSignalDic[textBoxSignalName.Text];
                    Solution.Instance.ProcessSignalDic.Remove(textBoxSignalName.Text);
                    signal.Dispose();
                }
            }
            try
            {
                Solution.Instance.ProcessSignalDic.Add(textBoxSignalName.Text,
                            new System.Threading.CountdownEvent(int.Parse(textBoxSendTimes.Text)));
                NewSignalEvent?. Invoke(this, textBoxSignalName.Text);
                MessageBoxTD.Show("信号创建成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBoxTD.Show($"信号创建失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 删除信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteSignal_Click(object sender, EventArgs e)
        {
            if (textBoxSignalName.Text.IsNullOrEmpty())
                return;
            if (Solution.Instance.ProcessSignalDic.ContainsKey(textBoxSignalName.Text))
            {
                var result = MessageBoxTD.Show(
                    $"是否删除信号“{textBoxSignalName.Text}”？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var signal = Solution.Instance.ProcessSignalDic[textBoxSignalName.Text];
                    Solution.Instance.ProcessSignalDic.Remove(textBoxSignalName.Text);
                    signal.Dispose();
                    DeleteSignalEvent?.Invoke(this, textBoxSignalName.Text);
                    MessageBoxTD.Show($"信号{comboBoxSendSignal.Text}删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void radioButtonSendSignal_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSendSignal.Enabled = radioButtonSendSignal.Checked;
            comboBoxWaitSignal.Enabled = !radioButtonSendSignal.Checked;
        }

        private void radioButtonWaitSignal_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSendSignal.Enabled = !radioButtonWaitSignal.Checked;
            comboBoxWaitSignal.Enabled = radioButtonWaitSignal.Checked;
        }
    }
}
