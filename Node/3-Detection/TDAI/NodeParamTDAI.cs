using System;
using System.Collections.Generic;
using Basler.Pylon;
using Newtonsoft.Json;
using TDJS_Vision.Node._3_Detection.TDAI.Yolo8;

namespace TDJS_Vision.Node._3_Detection.TDAI
{
    public class NodeParamTDAI : INodeParam
    {
        /// <summary>
        /// 订阅的节点文本
        /// </summary>
        public string Text1 { get; set; }
        /// <summary>
        /// 订阅的节点
        /// </summary>
        public string Text2 { get; set; }
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string ConfigPath { get; set; }
        /// <summary>
        /// AI模型配置(不参与序列化)
        /// </summary>
        [JsonIgnore]
        public AIInputInfo AIInputInfo { get; set; }
        /// <summary>
        /// 是否一键学习
        /// </summary>
        public bool IsAutoStudy { get; set; }
        /// <summary>
        /// 学习次数
        /// </summary>
        public int StudyNum { get; set; }
        /// <summary>
        /// 学习上下限比例
        /// </summary>
        public float StudyPercentage { get; set; }
        /// <summary>
        /// 是否需要转换
        /// </summary>
        public bool NeedConvert { get; set; }
        /// <summary>
        /// 比例尺（毫米每像素）
        /// </summary>
        public float Scale { get; set; }

        [JsonIgnore]
        public IYolo8 Yolo8 { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 模型名称
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ModelName ModelName { get; set; }
    }
    /// <summary>
    /// AI模型名称枚举
    /// </summary>
    public enum ModelName
    {
        超日线芯,
        超日胶壳,
        超日连接器,
        超日TypeC焊锡,
        中厚12类端子,
        线芯截面,
        刺破机,
        刺破机颜色线序
    }
}
