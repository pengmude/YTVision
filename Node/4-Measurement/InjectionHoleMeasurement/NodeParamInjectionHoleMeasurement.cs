using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Forms.ShapeDraw;

namespace YTVisionPro.Node._5_Measurement.InjectionHoleMeasurement
{
    internal class NodeParamInjectionHoleMeasurement : INodeParam
    {
        /// <summary>
        /// 订阅节点的名称
        /// </summary>
        public string Text1 { get; set; }
        /// <summary>
        /// 订阅节点的属性
        /// </summary>
        public string Text2 { get; set; }
        /// <summary>
        /// 显示更多参数开关
        /// </summary>
        public bool MoreParamsEnable { get; set; }
        /// <summary>
        /// 判断圆半径OK的开关
        /// </summary>
        public bool OKEnable { get; set; }
        /// <summary>
        /// 判断圆半径OK的下限
        /// </summary>
        public double OKMinR { get; set; }
        /// <summary>
        /// 判断圆半径OK的上限
        /// </summary>
        public double OKMaxR { get; set; }
        /// <summary>
        /// 高斯模糊值
        /// </summary>
        public int GaussianBlur { get; set; }
        /// <summary>
        /// 边缘检测弱边缘阈值
        /// </summary>
        public double Threshold1 { get; set; }
        /// <summary>
        /// 边缘检测强边缘阈值
        /// </summary>
        public double Threshold2 { get; set; }
        /// <summary>
        /// 是否启用边缘检测L2系数
        /// </summary>
        public bool IsOpenL2 { get; set; }
        /// <summary>
        /// ROI（反序列化用）
        /// </summary>
        [JsonConverter(typeof(PolyConverter))]
        public ROI ROI { get; set; }
    }
}
