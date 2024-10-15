using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node._4_Detection.FindLine
{
    public partial class NodeParamFormFindLine : Form, INodeParamForm
    {
        public NodeParamFormFindLine()
        {
            InitializeComponent();
        }

        INodeParam INodeParamForm.Params { get; set; }

        public void SetParam2Form()
        {
            //throw new NotImplementedException();
        }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            //throw new NotImplementedException();
        }
    }
}
