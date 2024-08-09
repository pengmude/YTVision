using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node
{
    public partial class ParamSetFormBase : Form, IParamSetForm
    {
        public virtual event EventHandler<INodeParam> NodeParamChanged;
    }
}
