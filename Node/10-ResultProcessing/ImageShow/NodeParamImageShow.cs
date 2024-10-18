using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.Tool.ImageShow
{
    internal class NodeParamImageShow: INodeParam
    {
        /// <summary>
        /// 图像窗口名称
        /// </summary>
        public string WindowName { get; set; }

        public string Text1 {  get; set; }

        public string Text2 { get; set; }
    }
}
