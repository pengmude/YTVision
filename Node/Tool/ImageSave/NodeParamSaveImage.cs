using System;
using System.Collections.Generic;
using System.Drawing;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.Tool.ImageSave
{
    internal class NodeParamSaveImage : INodeParam
    {
        /// <summary>
        /// 存图路径
        /// </summary>
        public string SavePath = string.Empty;
        /// <summary>
        /// 要保存的图片
        /// </summary>
        public Bitmap Image;
        /// <summary>
        /// 是否需要订阅检测的OK/NG结果来创建子目录
        /// </summary>
        public bool NeedOkNg;
        /// <summary>
        /// AI检测结果
        /// </summary>
        public AiResult AiResult;
        /// <summary>
        /// 图片是否是以条码命名
        /// </summary>
        public bool IsBarCode  = false;
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
}
