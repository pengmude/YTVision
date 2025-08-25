using System;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._3_Detection.TDAI;
using TDJS_Vision.Node._5_EquipmentCommunication.ModbusRead;
using TDJS_Vision.Node._5_EquipmentCommunication.PlcRead;
using TDJS_Vision.Node._6_LogicTool.SharedVariable;

namespace TDJS_Vision.Node._6_LogicTool.If
{
    public partial class NodeParamFormIf : FormBase, INodeParamForm
    {
        public NodeParamFormIf()
        {
            InitializeComponent();
        }

        public INodeParam Params { get; set; }
        /// <summary>
        /// 获取订阅的值
        /// </summary>
        /// <returns></returns>
        public bool GetCondition()
        {
            try
            {
                bool result = false;
                switch (nodeSubscription1.GetNodeType())
                {
                    case NodeType.AITD:
                        result = nodeSubscription1.GetValue<AlgorithmResult>().IsAllOk;
                        break;
                    case NodeType.PLCRead:
                        result = (bool)nodeSubscription1.GetValue<PlcReadResult>().Data;
                        break;
                    case NodeType.ModbusRead:
                        result = (bool)nodeSubscription1.GetValue<ModbusReadResult>().Data;
                        break;
                    case NodeType.SharedVariable:
                        var sharedVar = nodeSubscription1.GetValue<SharedVarValue>();
                        if(sharedVar.Type == typeof(bool))
                            result = (bool)sharedVar.Data;
                        else if (sharedVar.Type == typeof(AlgorithmResult))
                            result = ((AlgorithmResult)sharedVar.Data).IsAllOk;
                        else
                            throw new Exception($"该共享变量类型不支持条件判断！当前类型：{sharedVar.Type}");
                        break;
                    default:
                        break;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (nodeSubscription1.GetNodeType())
                {
                    case NodeType.AITD:
                    case NodeType.PLCRead:
                    case NodeType.ModbusRead:
                    case NodeType.SharedVariable:
                        break;
                    default:
                        throw new Exception("当前节点类型不支持订阅条件！");
                }
                NodeParamElse nodeParamIf = new NodeParamElse();
                nodeParamIf.Text1 = nodeSubscription1.GetText1();
                nodeParamIf.Text2 = nodeSubscription1.GetText2();
                Params = nodeParamIf;
            }
            catch (Exception ex)
            {
                MessageBoxTD.Show($"参数设置异常原因：{ex.Message}", "异常", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Hide();
        }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if(Params is NodeParamElse param)
                nodeSubscription1.SetText(param.Text1, param.Text2);
        }
    }
}
