using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Forms.LightAdd;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Node.Light;
using YTVisionPro.Node.Light.PPX;
using YTVisionPro.Node.NodeDemo;

namespace YTVisionPro.Node.NodeLight.PPX
{
    public class NodeLight : NodeBase, INode<ParamFormLight, NodeParamLight, NodeResultLight>
    {
        LightPPX lightPPX;

        /// <summary>
        /// 创建一个指定名称的节点
        /// </summary>
        /// <param name="nodeText"></param>
        public NodeLight(string nodeText)
        {
            SetNodeText(nodeText);
        }

        public void ParamForm_OnNodeParamChange(object sender, INodeParam e)
        {
            Param = (NodeParamLight)e;
            
            foreach (var light in Solution.Instance.LightDevices)
            {
                if (light.SerialStructure.SerialNumber == Param.SerialNumber && light.SerialStructure.ChannelValue == Param.ChannelValue)
                {
                    lightPPX = (LightPPX)light;
                    SerialStructure serialStructure = lightPPX.SerialStructure;
                    serialStructure.ChannelValue = (byte)Param.ChannelValue;
                    lightPPX.SerialStructure= serialStructure;
                    Logger.LogHelper.AddLog(Logger.MsgLevel.Debug, "保存成功", true);
                    return;
                }
            }
            MessageBox.Show("没有该光源");
            Logger.LogHelper.AddLog(Logger.MsgLevel.Debug, "没有该光源", true);

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
        public override void Run()
        {
            DateTime startTime = DateTime.Now;

            if (!Active)
            {
                return;
            }

            if (lightPPX == null)
            {
                Logger.LogHelper.AddLog(Logger.MsgLevel.Warn,"光源为空",true);
                return;
            }

            Result = new NodeResultLight();
            string SerialNumber = Param.SerialNumber;
            int ChannelValue = Param.ChannelValue;
            int Brightness = Param.Brightness;

            if (Param.Open) // 打开操作
            {
                bool flag = lightPPX.Connenct(SerialNumber, lightPPX.SerialStructure.Baudrate, lightPPX.SerialStructure.DataBits, lightPPX.SerialStructure.StopBits, lightPPX.SerialStructure.Parity);
                Result.Success = flag;
                lightPPX.SetValue(Brightness);
                lightPPX.Brightness = Brightness;
                Result.RunStatusCode = flag ? NodeRunStatusCode.OK : NodeRunStatusCode.UNKNOW_ERROR;
            }
            else
            {
                lightPPX.Connenct(SerialNumber, lightPPX.SerialStructure.Baudrate, lightPPX.SerialStructure.DataBits, lightPPX.SerialStructure.StopBits, lightPPX.SerialStructure.Parity);
                lightPPX.SetValue(0);
                Result.Success = true;
                Result.RunStatusCode = NodeRunStatusCode.OK;
            }

            DateTime endTime = DateTime.Now;
            TimeSpan elapsed = endTime - startTime;
            long elapsedMi11iseconds = (long)elapsed.TotalMilliseconds;
            Result.RunTime = elapsedMi11iseconds;
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
