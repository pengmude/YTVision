using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.AI.HTAI
{
    internal class NodeParamHTAI : INodeParam
    {
        // 预测句柄
        public IntPtr TreePredictHandle { get; set; }
        // NGType类，包含配置信息和输出结果
        public List<NGTypeConfig> AllNgConfigs { get; set; }
        // 检测出来的节点结果数
        public int TestNum { get; set; }

        public string WindowName {  get; set; }
    }
}
