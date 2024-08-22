using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node
{
    /// <summary>
    /// 泛型节点接口类
    /// </summary>
    /// <typeparam name="TParam">节点参数</typeparam>
    /// <typeparam name="TResult">节点运行结果</typeparam>
    internal interface INode<TNodeParamForm, TNodeParam, TNodeResult>
    {
        TNodeParamForm ParamForm { get; set; }
        /// <summary>
        /// 节点参数
        /// </summary>
        TNodeParam Param { get; set; }

        /// <summary>
        /// 节点运行
        /// </summary>
        void Run();

        /// <summary>
        /// 获取节点运行结果
        /// </summary>
        /// <returns></returns>
        TNodeResult Result { get; }

        /// <summary>
        /// 设置节点文本
        /// </summary>
        /// <param name="text"></param>
        void SetNodeText(string text);
    }

    /// <summary>
    /// 节点参数界面接口类
    /// </summary>
    internal interface INodeParamForm
    {
        event EventHandler<INodeParam> OnNodeParamChange;
    }

    /// <summary>
    /// 节点参数接口类
    /// </summary>
    internal interface INodeParam { }

    /// <summary>
    /// 节点运行结果接口类
    /// </summary>
    internal interface INodeResult 
    {
        /// <summary>
        /// 节点运行是否成功
        /// </summary>
        bool Success { get; set; }
        /// <summary>
        /// 节点运行时间ms，计算方法：
        /// DateTime startTime = DateTime.Now;
        /// DateTime endTime = DateTime.Now;
        /// TimeSpan elapsed = endTime - startTime;
        /// long elapsedMilliseconds = elapsed.TotalMilliseconds;
        /// </summary>
        long RunTime {  get; set; }
        /// <summary>
        /// 运行状态码
        /// </summary>
        NodeRunStatusCode RunStatusCode { get; set; }

    }

    /// <summary>
    /// 节点运行状态码
    /// </summary>
    internal enum NodeRunStatusCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        OK,
        /// <summary>
        /// 参数有误
        /// </summary>
        PARAM_ERROR,
        /// <summary>
        /// 超时
        /// </summary>
        TIMEOUT,
        /// <summary>
        /// 未知错误
        /// </summary>
        UNKNOW_ERROR
    }

    /// <summary>
    /// 节点类型
    /// </summary>
    internal enum NodeType
    {
        /// <summary>
        /// 磐鑫光源节点
        /// </summary>
        LightPPX,
        /// <summary>
        /// 锐视光源节点
        /// </summary>
        LightRsee,
        /// <summary>
        /// 相机节点
        /// </summary>
        Camera,
        /// <summary>
        /// 读取PLC寄存器节点
        /// </summary>
        PLCRead,
        /// <summary>
        /// 写入PLC寄存器节点
        /// </summary>
        PLCWrite,
        /// <summary>
        /// 汇图AI节点
        /// </summary>
        AIHT
    }
}
