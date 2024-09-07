using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.AI.HTAI
{
    public class NGTypeConfig
    {
        /// <summary>
        /// 节点名
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 节点名对应的类型
        /// </summary>
        public string CLassName { get; set; }
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
}
