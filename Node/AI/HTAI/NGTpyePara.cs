using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.AI.HTAI
{
    internal class NGTypePara
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
            /// 面积
            /// </summary>
            public int[] Area { get; set; }
            /// <summary>
            /// 最小分数
            /// </summary>
            public float MinScore { get; set; }
            /// <summary>
            /// 最大分数
            /// </summary>
            public float MaxScore { get; set; }
            /// <summary>
            /// 分数
            /// </summary>
            public float[] Score { get; set; }
            /// <summary>
            /// 最小个数
            /// </summary>
            public int MinNum { get; set; }
            /// <summary>
            /// 最大个数
            /// </summary>
            public int MaxNum { get; set; }
            /// <summary>
            /// 当前节点名对应类型的缺陷个数
            /// </summary>
            public int Num { get; set; }
            /// <summary>
            /// 是否是OK项
            /// </summary>
            public bool ForceOk { get; set; }
            /// <summary>
            /// 该项结果
            /// </summary>
            public bool  IsOk 
            { 
                get
                {
                    if(Num >= MinNum && Num < MaxNum) return false;
                    else return true;
                }
            }
        }

        public class NGType
        {
            public List<NGTypeConfig> NGTypeConfigs = new List<NGTypeConfig>();
        }   
        
        public class ClassNameResult
        {
            public string NodeName { get; set; }
            public string CLassName { get; set; }
            public int Area { get; set; }
            public float Score { get; set; }
            public int Num { get; set; }
        }
    }
}
