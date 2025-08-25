using System.ComponentModel;
using System.Drawing;
using TDJS_Vision.Node._1_Acquisition.ImageSource;

namespace TDJS_Vision.Node._7_ResultProcessing.ImageDraw
{
    public class NodeResultImageDraw : INodeResult
    {
        public int RunTime { get; set; }
        [DisplayName("输出图像")]
        public OutputImage OutputImage { get; set; } = new OutputImage();
    }
}
