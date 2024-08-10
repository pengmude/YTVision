using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node.Camera
{
    //public partial class FormCameraTest : Form, IParamSetForm
        public partial class FormCameraTest : Form
    {
        public FormCameraTest()
        {
            InitializeComponent();
        }

        public event EventHandler<INodeParam> NodeParamChanged;
    }
}
