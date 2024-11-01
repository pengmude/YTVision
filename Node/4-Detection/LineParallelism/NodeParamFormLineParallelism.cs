using Newtonsoft.Json.Linq;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Node.ImagePreprocessing.ImageCrop;

namespace YTVisionPro.Node._4_Detection.DetectionLineParallelism
{
    internal partial class NodeParamFormLineParallelism : Form, INodeParamForm
    {
        private double StraightLineAngle;
        public double _straightLineAngle
        {
            get { return StraightLineAngle; }
            set
            {
                this.label2.Text = $"两直线夹角为：{value.ToString()}°";
                StraightLineAngle = value;
            }
        }

        private double LinearDistance;
        public double _linearDistance
        {
            get { return LinearDistance; }
            set
            {
                this.label3.Text = $"直线两端点到另一条直线的距离差为：{value.ToString()}";
                LinearDistance = value;
            }
        }


        public NodeParamFormLineParallelism()
        {
            InitializeComponent();
        }

        public INodeParam Params { get; set; }

        public void SetParam2Form()
        {
            if (Params is NodeParamLineParallelism param)
            {
                nodeSubscription1.SetText(param.Text1, param.Text2);
                Show();
                Hide();
            }
        }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NodeParamLineParallelism nodeParamLineParallelism = new NodeParamLineParallelism();
            nodeParamLineParallelism.Text1 = nodeSubscription1.GetText1();
            nodeParamLineParallelism.Text2 = nodeSubscription1.GetText2();
            Params = nodeParamLineParallelism;
            Hide();
        }

        /// <summary>
        /// 获取两直线端点
        /// </summary>
        /// <returns></returns>
        public List<Point[]> GetLineEndpoints()
        {
            return nodeSubscription1.GetValue<List<Point[]>>();
        }
    }
}
