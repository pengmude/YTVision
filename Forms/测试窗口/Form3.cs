using System.Collections.Generic;
using System.Windows.Forms;

namespace YTVisionPro.Forms.测试窗口
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加一个名为processName的流程结果
        /// </summary>
        public void AddProcessResultView(string processName) { }

        /// <summary>
        /// 移除一个名为processName的流程结果
        /// </summary>
        public void RemoveProcessResultView(string processName) { }

        /// <summary>
        /// 在名为processName的流程结果中添加名为itemName的检测项
        /// </summary>
        /// <param name="processName"></param>
        public void AddItem(string processName, string itemName) { }


        /// <summary>
        /// 在名为processName的流程结果中移除名为itemName的检测项
        /// </summary>
        /// <param name="processName"></param>
        public void RemoveItem(string processName, string itemName) { }

        /// <summary>
        /// 指定流程指定检测项的结果加1
        /// </summary>
        /// <param name="processName"></param>
        /// <param name=""></param>
        public void IncreaseOneItemResult(string processName, string itemName) { }

        /// <summary>
        /// 指定流程指定检测项的结果清空
        /// </summary>
        /// <param name="processName"></param>
        /// <param name=""></param>
        public void ClearItemResult(string processName, string itemName) { }

        /// <summary>
        /// 清空单个流程的所有检测项结果
        /// </summary>
        /// <param name="processName"></param>
        public void ClearProcessResult(string processName) { }

        /// <summary>
        /// 清空所有流程的所有检测项结果
        /// </summary>
        /// <param name="processName"></param>
        public void ClearAllProcessResult(List<string> processNameList) { }
    }
}
