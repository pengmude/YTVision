using System.ComponentModel;
using TDJS_Vision.Node._1_Acquisition.ImageSource;

namespace TDJS_Vision.Node._2_ImagePreprocessing.ImageCrop
{
    public class NodeResultImageCrop : INodeResult
    {
        public int RunTime { get; set; }

        [DisplayName("输出图像")]
        public OutputImage OutputImage { get; set; } = new OutputImage();
    }
}
