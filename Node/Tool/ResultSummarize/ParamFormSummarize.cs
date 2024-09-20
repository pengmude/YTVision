using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Forms.ResultView;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.Tool.ResultSummarize
{
    internal partial class ParamFormSummarize : Form, INodeParamForm
    {
        public ParamFormSummarize()
        {
            InitializeComponent();
        }

        public INodeParam Params { get; set; }

        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
            nodeSubscription2.Init(node);
        }

        /// <summary>
        /// 获取AI结果
        /// </summary>
        /// <returns></returns>
        public AiResult GetAiResult()
        {
            //return nodeSubscription1.GetValue<AiResult>();
            AiResult result = new AiResult();
            List<SingleResultViewData> singleResultViewDatas = new List<SingleResultViewData>();
            SingleResultViewData singleResultViewData = new SingleResultViewData();
            singleResultViewData.NodeName = "a";
            singleResultViewData.ClassName = "b";
            singleResultViewData.DetectResult = "c";
            singleResultViewData.IsOk = true;
            singleResultViewDatas.Add(singleResultViewData);
            result.DeepStudyResult = singleResultViewDatas;
            return result;
        }

        /// <summary>
        /// 获取传统算法的结果
        /// </summary>
        /// <returns></returns>
        public AiResult GetDetectResult()
        {
            //return nodeSubscription2.GetValue<AiResult>();
            AiResult result = new AiResult();
            List<SingleResultViewData> singleResultViewDatas = new List<SingleResultViewData>();
            SingleResultViewData singleResultViewData = new SingleResultViewData();
            singleResultViewData.DetectName = "1";
            singleResultViewData.DetectResult = "2";
            singleResultViewData.IsOk = false;
            singleResultViewDatas.Add(singleResultViewData);
            result.DeepStudyResult = singleResultViewDatas;
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NodeParamSummarize nodeParamSummarize = new NodeParamSummarize();
            nodeParamSummarize.AiResult = GetAiResult();
            nodeParamSummarize.DetectResult = GetDetectResult();
            Params = nodeParamSummarize;
            Hide();
        }
    }
}
