using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_light_controller;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Node.Light;
using YTVisionPro.Node.NodeDemo;

namespace YTVisionPro.Node.NodeDemoLight
{
    public class NodeDemoLight : NodeBase, INode<ParamFormLight, NodeParamLight, NodeResultLight>
    {
        LightPPX lightPPX = new LightPPX();


        /// <summary>
        /// 创建一个指定名称的节点
        /// </summary>
        /// <param name="nodeText"></param>
        public NodeDemoLight(string nodeText)
        {
            SetNodeText(nodeText);
            ParamForm.OnNodeParamChange += ParamForm_OnNodeParamChange;
        }

        public void ParamForm_OnNodeParamChange(object sender, INodeParam e)
        {
            foreach (var light in Solution.Instance.LightDevices)
            {
                if (light.SerialStructure.SerialNumber == Param.SerialNumber && light.SerialStructure.ChannelValue == Param.ChannelValue)
                {
                    lightPPX = (LightPPX)light;
                    SerialStructure serialStructure = lightPPX.SerialStructure;
                    serialStructure.ChannelValue = (byte)Param.ChannelValue;
                    lightPPX.SerialStructure= serialStructure;
                    break;
                }
            }
        }

        /// <summary>
        /// 参数设置窗口
        /// </summary>
        ParamFormLight INode<ParamFormLight, NodeParamLight, NodeResultLight>.ParamForm
        {
            get => (ParamFormLight)base.ParamForm;
            set => base.ParamForm = value;
        }

        /// <summary>
        /// 节点参数
        /// </summary>
        public NodeParamLight Param { get; set; }

        /// <summary>
        /// 节点结果
        /// </summary>
        public NodeResultLight Result { get; private set; }

        /// <summary>
        /// 节点运行
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Run()
        {
            if (lightPPX == null)
            {
                Logger.LogHelper.AddLog(Logger.MsgLevel.Warn,"光源为空",true);
                return;
            }

            Result = new NodeResultLight();
            string SerialNumber = Param.SerialNumber;
            int ChannelValue = Param.ChannelValue;
            int Brightness = Param.Brightness;

            if (Param.Open == true) // 打开操作
            {
                lightPPX.Connenct(SerialNumber, int.Parse(lightPPX.SerialStructure.brand), lightPPX.SerialStructure.DataBits, lightPPX.SerialStructure.StopBits, lightPPX.SerialStructure.Parity);
                lightPPX.SetValue(Brightness);
            }
            else
            {
                lightPPX.Disconnect();
            }

        }

        /// <summary>
        /// 给节点设置文本
        /// </summary>
        /// <param name="text"></param>
        void INode<ParamFormLight, NodeParamLight, NodeResultLight>.SetNodeText(string text)
        {
            base.SetNodeText(text);
        }

        //TODO: 目前 方案保存List<NodeBase>就能够兼容保存所有节点
        // 节点运行时间、是否成功标志、运行状态码等必要结果，设在INodeResult接口类中必须实现

    }
}
