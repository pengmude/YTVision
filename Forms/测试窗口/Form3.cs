using System.Collections.Generic;
using System.Windows.Forms;
using YTVisionPro.Node;
using YTVisionPro.Node.Camera;
using YTVisionPro.Node.Light;
using YTVisionPro.Node.NodeDemo;
using YTVisionPro.Node.NodeDemoLight;

namespace YTVisionPro.Forms.测试窗口
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            //NodeLight nodeLight = new NodeLight("test", new FormLightSettings(Hardware.Light.LightBrand.PPX), new Process());
            //NodeLight nodeLight = new NodeLight("test", new FormCameraTest(), new Process());
            //nodeLight.Show();
            //Controls.Add(nodeLight);
            //CameraNode cameraNode = new CameraNode();
            //LightNode lightNode = new LightNode();
            //lightNode.Location = new System.Drawing.Point(300, 500);
            //Controls.Add(cameraNode);
            //Controls.Add(lightNode);
            //cameraNode.ShowSettingsWindow();
            //NodeBase<NodeParametersLight, NodeResultLight> nodeBase = new NodeBase<NodeParametersLight, NodeResultLight>(new ParameterSettingsForm());

            NodeDemoLight nodeDemo = new NodeDemoLight();
            nodeDemo.ParamForm = new ParamFormLight();
            Controls.Add(nodeDemo);
        }
    }
}
