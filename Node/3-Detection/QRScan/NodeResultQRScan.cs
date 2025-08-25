using System.ComponentModel;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._3_Detection.QRScan
{
    public class NodeResultQRScan : INodeResult
    {
        public int RunTime { get; set; }
        [DisplayName("第一个二维码")]
        public string FirstCode { get; set; }

        [DisplayName("所有检出二维码")]
        public string[] Codes { get; set; }

        [DisplayName("算法结果")]
        public AlgorithmResult Result { get; set; } = new AlgorithmResult();
    }
}
