using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using YTVisionPro.Device.Camera;

namespace YTVisionPro.Node._1_Acquisition.ImageSource
{
    internal class NodeParamImageSoucre : INodeParam
    {
        /// <summary>
        /// 图像源
        /// </summary>
        public string ImageSource { get; set; }
        /// <summary>
        /// 要显示的窗口名称
        /// </summary>
        public string WindowName { get; set; }

        #region 本地图像参数
        /// <summary>
        /// 文件或文件夹路径
        /// </summary>
        public string PathText { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 触发遍历下一张图片
        /// </summary>
        public bool IsAutoLoop { get; set; }
        /// <summary>
        /// 图片路径列表
        /// </summary>
        public List<string> ImagePaths { get; set; }
        #endregion

        #region 相机参数

        /// <summary>
        /// 使用的相机
        /// </summary>
        [JsonIgnore]
        public ICamera Camera { get; set; }
        /// <summary>
        /// 使用的相机名称
        /// </summary>
        public string CameraName { get; set; }
        /// <summary>
        /// 触发方式
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TriggerSource TriggerSource { get; set; }
        /// <summary>
        /// 触发沿
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TriggerEdge TriggerEdge { get; set; }
        /// <summary>
        /// 触发延迟
        /// </summary>
        public int TriggerDelay { get; set; }
        /// <summary>
        /// 曝光时间
        /// </summary>
        public float ExposureTime { get; set; }
        /// <summary>
        /// 增益
        /// </summary>
        public float Gain { get; set; }
        /// <summary>
        /// 是否频闪
        /// </summary>
        public bool IsStrobing { get; set; }

        #endregion
    }
}
