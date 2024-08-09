using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node
{
    /// <summary>
    /// 参数设置界面接口类
    /// </summary>
    public interface IParamSetForm
    {
        event EventHandler<INodeParam> NodeParamChanged;
    }
}
