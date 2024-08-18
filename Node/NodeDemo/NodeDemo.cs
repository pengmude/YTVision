using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Node.Light.PPX;

namespace YTVisionPro.Node.NodeDemo
{
    public class NodeDemo : NodeBase
    {
        /// <summary>
        /// 创建一个指定名称的节点
        /// </summary>
        /// <param name="nodeText"></param>
        public NodeDemo(string nodeText) : base(new NodeParamSetDemo())
        {
            SetNodeText(nodeText);
            Result = new NodeResultLight();
        }
        /// <summary>
        /// 节点运行
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Run()
        {
            // 写执行的节点操作
        }
    }
}
