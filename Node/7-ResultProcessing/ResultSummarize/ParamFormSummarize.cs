using System;
using System.Windows.Forms;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._7_ResultProcessing.ResultSummarize
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
        public ResultViewData GetAiResult()
        {
#if DEBUG
            ResultViewData result = new ResultViewData();
            List<SingleResultViewData> singleResultViewDatas = new List<SingleResultViewData>();
            SingleResultViewData singleResultViewData = new SingleResultViewData();
            singleResultViewData.NodeName = "a";
            singleResultViewData.ClassName = "b";
            singleResultViewData.DetectResult = "c";
            singleResultViewData.IsOk = true;
            singleResultViewDatas.Add(singleResultViewData);
            result.SingleRowDataList = singleResultViewDatas;
            return result;
#else
            return nodeSubscription1.GetValue<ResultViewData>();
#endif
        }

        /// <summary>
        /// 获取传统算法的结果
        /// </summary>
        /// <returns></returns>
        public ResultViewData GetDetectResult()
        {
#if DEBUG
            ResultViewData result = new ResultViewData();
            List<SingleResultViewData> singleResultViewDatas = new List<SingleResultViewData>();
            SingleResultViewData singleResultViewData = new SingleResultViewData();
            singleResultViewData.DetectName = "1";
            singleResultViewData.DetectResult = "2";
            singleResultViewData.IsOk = false;
            singleResultViewDatas.Add(singleResultViewData);
            result.SingleRowDataList = singleResultViewDatas;
            return result;
#else
            return nodeSubscription2.GetValue<ResultViewData>();
#endif
        }
        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
#if DEBUG
            NodeParamSummarize param = new NodeParamSummarize();
            param.AiResult = GetAiResult();
            param.NonAiResult = GetDetectResult();
            param.AiText1 = nodeSubscription1.GetText1();
            param.AiText2 = nodeSubscription1.GetText2();
            param.NonAiText1 = nodeSubscription2.GetText1();
            param.NonAiText2 = nodeSubscription2.GetText2();
            Params = param;
#else
            NodeParamSummarize param = new NodeParamSummarize();
            param.AiText1 = nodeSubscription1.GetText1();
            param.AiText2 = nodeSubscription1.GetText2();
            param.NonAiText1 = nodeSubscription2.GetText1();
            param.NonAiText2 = nodeSubscription2.GetText2();
            Params = new NodeParamSummarize();
#endif
            Hide();
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamSummarize param)
            {
                nodeSubscription1.SetText(param.AiText1, param.AiText2);
                nodeSubscription2.SetText(param.NonAiText1, param.NonAiText2);
            }
        }
    }
}
