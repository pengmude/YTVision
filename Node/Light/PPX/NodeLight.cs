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
        /// 创建一个指定名称的节点
        /// </summary>
        /// <param name="nodeText"></param>
        public NodeLight(string nodeText, INodeParamForm nodeParamForm)
        {
            SetNodeText(nodeText);
            ParamForm = nodeParamForm;
            ParamForm.OnNodeParamChange += ParamForm_OnNodeParamChange;
        }

        /// <summary>
        /// 光源参数改变事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ParamForm_OnNodeParamChange(object sender, INodeParam e)
        {
            Param = (NodeParamLight)e;
        }


        /// <summary>
        /// 节点运行
        /// </summary>
        public override void Run()
        {
            DateTime startTime = DateTime.Now;

            if (!Active)
                return;

            Result = new NodeResultLight();

            // 打开操作
            if (Param.Open) 
            {
                try
                {
                    Param.Light.TurnOn();
                    Result.Success = true;
                    Result.RunStatusCode = NodeRunStatusCode.OK;
                }
                catch (Exception)
                {
                    Result.Success = false;
                    Result.RunStatusCode = NodeRunStatusCode.UNKNOW_ERROR;
                }
            }
            else
            {
                try
                {
                    Param.Light.TurnOff();
                    Result.Success = true;
                    Result.RunStatusCode = NodeRunStatusCode.OK;
                }
                catch (Exception)
                {
                    Result.Success = false;
                    Result.RunStatusCode = NodeRunStatusCode.UNKNOW_ERROR;
                }
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
    }
}
