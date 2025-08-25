using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using Logger;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Forms.SelectDetectItem
{
    public partial class FormSelectDetectItem : FormBase
    {
        public EventHandler<List<string>> SendDetectItems;
        public FormSelectDetectItem()
        {
            InitializeComponent();
            Shown += FormSelectDetectItem_Shown;
        }

        private void FormSelectDetectItem_Shown(object sender, EventArgs e)
        {
            InitData(TDAI.DetectItemMap);
        }

        /// <summary>
        /// 获取当前选中的检测项列表
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 获取所有 TabPage 中 CheckedListBox 的选中项文本
        /// </summary>
        /// <returns>所有选中项的文本列表</returns>
        public List<string> GetSelectedItems()
        {
            List<string> selectedItems = new List<string>();

            // 遍历每个 TabPage
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                // 遍历 TabPage 中的控件
                foreach (Control control in tabPage.Controls)
                {
                    if (control is CheckedListBox checkedListBox)
                    {
                        // 遍历所有选中的项
                        for (int i = 0; i < checkedListBox.CheckedItems.Count; i++)
                        {
                            selectedItems.Add(checkedListBox.CheckedItems[i].ToString());
                        }
                    }
                }
            }

            return selectedItems;
        }

        /// <summary>
        /// 初始化并绑定数据
        /// </summary>
        public void InitData(Dictionary<string, List<DetectItemInfo>> dataMap)
        {
            // 清空原有 TabPages（避免重复添加）
            tabControl1.TabPages.Clear();

            foreach (var kvp in dataMap)
            {
                string nodeName = kvp.Key;
                List<DetectItemInfo> detectItems = kvp.Value;

                // 创建新的 TabPage
                TabPage tabPage = new TabPage(nodeName);

                // 创建 CheckedListBox
                CheckedListBox checkedListBox = new CheckedListBox();
                checkedListBox.Dock = DockStyle.Fill; // 填满 TabPage
                checkedListBox.CheckOnClick = true;   // 点击即切换选中状态

                // 填充数据（调用你已实现的方法）
                UpdateListBoxView(checkedListBox, detectItems);

                // 添加到 TabPage
                tabPage.Controls.Add(checkedListBox);

                // 添加 TabPage 到 TabControl
                tabControl1.TabPages.Add(tabPage);
            }
        }

        /// <summary>
        /// 更新检测项列表视图
        /// </summary>
        /// <param name="aIInputInfo"></param>
        private void UpdateListBoxView(CheckedListBox clb, List<DetectItemInfo> items)
        {
            try
            {
                clb.Items.Clear();

                foreach (var item in items)
                {
                    // 添加项到 CheckedListBox
                    clb.Items.Add(item.Name, item.Enable); // 添加名称并设置初始选中状态
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"刷新检测项配置失败！原因：{ex.Message}", true);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            SendDetectItems?.Invoke(this, GetSelectedItems());
            Hide();
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            // 遍历 TabPage 中的控件
            foreach (Control control in tabControl1.SelectedTab.Controls)
            {
                if (control is CheckedListBox checkedListBox)
                {
                    // 全选
                    for (int i = 0; i < checkedListBox.Items.Count; i++)
                    {
                        checkedListBox.SetItemChecked(i, true);
                    }
                }
            }
        }
        /// <summary>
        /// 全部取消选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // 遍历 TabPage 中的控件
            foreach (Control control in tabControl1.SelectedTab.Controls)
            {
                if (control is CheckedListBox checkedListBox)
                {
                    // 全选
                    for (int i = 0; i < checkedListBox.Items.Count; i++)
                    {
                        checkedListBox.SetItemChecked(i, false);
                    }
                }
            }
        }
    }
}
