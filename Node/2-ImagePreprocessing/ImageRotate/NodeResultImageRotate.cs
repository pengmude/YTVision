using System.ComponentModel;
using System.Drawing;
using TDJS_Vision.Node._1_Acquisition.ImageSource;

namespace TDJS_Vision.Node._2_ImagePreprocessing.ImageRotate
{
    public class NodeResultImageRotate : INodeResult
    {
        public int RunTime { get; set; }
        /// <summary>
        /// 旋转后的图片
        /// </summary>
        [DisplayName("旋转后图像")]
        public OutputImage OutputImage { get; set; } = new OutputImage();
    }
}
