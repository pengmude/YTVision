using System;
using System.Collections.Generic;
using static YTVisionPro.Node.AI.HTAI.HTAPI;

namespace YTVisionPro.Node.AI.HTAI
{
    internal class NGTypeConfig
    {
        /// <summary>
        /// 节点名
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 节点类别
        /// </summary>
        public int NodeType { get; set; }
        /// <summary>
        /// 节点名对应的类型
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 最小面积
        /// </summary>
        public int MinArea { get; set; }
        /// <summary>
        /// 最大面积
        /// </summary>
        public long MaxArea { get; set; }
        /// <summary>
        /// 最小分数
        /// </summary>
        public float MinScore { get; set; }
        /// <summary>
        /// 最大分数
        /// </summary>
        public float MaxScore { get; set; }
        /// <summary>
        /// 最小个数
        /// </summary>
        public int MinNum { get; set; }
        /// <summary>
        /// 最大个数
        /// </summary>
        public int MaxNum { get; set; }
        /// <summary>
        /// 是否是OK项
        /// </summary>
        public bool ForceOk { get; set; }
    }

    /// <summary>
    /// 单个类别结果
    /// </summary>
    internal class AiClassResult
    {
        public string NodeName;
        public int NodeType;
        public string ClassName;
        public bool IsLocFail;
        public List<DetectResult> Results;

        public AiClassResult() { }
        public AiClassResult(string nodeName, int nodeType, string className, bool isLocFail, List<DetectResult> results)
        {
            NodeName = nodeName;
            NodeType = nodeType;
            ClassName = className;
            IsLocFail = isLocFail;
            Results = new List<DetectResult>();
        }
    }
}
