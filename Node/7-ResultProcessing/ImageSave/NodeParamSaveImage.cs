using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._7_ResultProcessing.ImageSave
{
    public class NodeParamSaveImage : INodeParam
    {
        /// <summary>
        /// 存图路径
        /// </summary>
        public string SavePath = string.Empty;
        [JsonIgnore]
        /// <summary>
        /// 要保存的图片
        /// </summary>
        public Bitmap Image;
        /// <summary>
        /// 是否需要订阅检测的OK/NG结果来创建子目录
        /// </summary>
        public bool NeedOkNg;
        [JsonIgnore]
        /// <summary>
        /// AI检测结果
        /// </summary>
        public AlgorithmResult AlgorithmResult;
        /// <summary>
        /// 图片是否是以条码命名
        /// </summary>
        public bool IsBarCode  = false;
        [JsonIgnore]
        /// <summary>
        /// 条码
        /// </summary>
        public string Barcode = "";
        /// <summary>
        /// 是否使用早晚班存图
        /// </summary>
        public bool IsDayNight = false;
        /// <summary>
        /// 早班时间
        /// </summary>
        public DateTime DayDataTime;
        /// <summary>
        /// 晚班时间
        /// </summary>
        public DateTime NightDataTime;
        /// <summary>
        /// 是否压缩
        /// </summary>
        public bool NeedCompress;
        /// <summary>
        /// 压缩阈值
        /// </summary>
        public ushort CompressValue;
        /// <summary>
        /// 需要保存什么类型图片
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ImageTypeToSave ImageTypeToSave;

        #region 反序列化还原参数设置界面用

        /// <summary>
        /// 图像订阅节点的文本1
        /// </summary>
        public string ImgSubText1;
        /// <summary>
        /// 图像订阅控件的文本2
        /// </summary>
        public string ImgSubText2;
        /// <summary>
        /// AI检测结果订阅控件的文本1
        /// </summary>
        public string AiResSubText1;
        /// <summary>
        /// AI检测结果订阅控件的文本2
        /// </summary>
        public string AiResSubText2;
        /// <summary>
        /// 条码订阅控件的文本1
        /// </summary>
        public string BarCodeSubText1;
        /// <summary>
        /// 条码订阅控件的文本2
        /// </summary>
        public string BarCodeSubText2;

        

        #endregion
    }

    public struct AllNGResult
    {
        /// <summary>
        /// AI检出缺陷列表
        /// </summary>
        public List<string> AINGList;
        /// <summary>
        /// 传统算法NG列表
        /// </summary>
        public List<string> GeneralNGList;
    }
    /// <summary>
    /// 保存什么类型的图片
    /// </summary>
    public enum ImageTypeToSave
    {
        OkAndNg,
        OnlyOk,
        OnlyNg
    }
}
