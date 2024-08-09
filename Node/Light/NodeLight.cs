using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;
using YTVisionPro.Node;

namespace YTVisionPro.Node.Light
{
    /// <summary>
    /// 光源节点类，包含成员：
    /// 1.光源专用参数设置窗口（双击节点时显示不同设置窗口用，需要订阅参数设置完成的事件）
    /// 2.光源节点参数（提供Run函数执行用）
    /// 3.Run()节点运行函数（执行节点的操作）
    /// 3.光源节点结果（外部获取节点结果）
    /// </summary>
    public partial class NodeLight : NodeBase
    {
        /// <summary>
        /// 节点构造函数
        /// </summary>
        /// <param name="text">节点名称（不用带id）</param>
        /// <param name="formSettings">节点参数设置界面</param>
        /// <param name="process">节点所属流程</param>
        public NodeLight(string text, Form formSettings, Process process) : base(text, formSettings, process)
        {
        }
    }
}
