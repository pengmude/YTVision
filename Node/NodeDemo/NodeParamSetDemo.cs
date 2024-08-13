using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node.NodeDemo
{
    public partial class NodeParamSetDemo : Form, INodeParamForm
    {
        public NodeParamSetDemo()
        {
            InitializeComponent();
        }

        public event EventHandler<INodeParam> OnNodeParamChange;
    }
}
