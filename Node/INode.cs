using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node
{
    /// <summary>
    /// 节点参数界面接口类
    /// </summary>
    internal interface INodeParamForm
    {
        /// <summary>
        /// 节点运行参数
        /// </summary>
        INodeParam Params { get; set; }
        /// <summary>
        /// 给节点参数界面类设置所属的节点,需要订阅结果必须调用
        /// </summary>
        /// <param name="node"></param>
        void SetNodeBelong(NodeBase node);
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
        /// 节点运行状态
        /// </summary>
        NodeStatus Status { get; set; }
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
        /// 未运行
        /// </summary>
        UNEXECUTED = 0x00099,
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
        /// 光源控制
        /// </summary>
        LightSourceControl,
        /// <summary>
        /// 软触发等待
        /// </summary>
        WaitSoftTrigger,
        /// <summary>
        /// 相机拍照
        /// </summary>
        CameraShot,
        /// <summary>
        /// 硬触发等待
        /// </summary>
        WaitHardTrigger,
        /// <summary>
        /// 本地图像
        /// </summary>
        LocalPicture,
        /// <summary>
        /// PLC寄存器读取
        /// </summary>
        PLCRead,
        /// <summary>
        /// PLC寄存器写入
        /// </summary>
        PLCWrite,
        /// <summary>
        /// PLC寄存器写入AI检测结果
        /// </summary>
        PLCHTAIResultSend,
        /// <summary>
        /// 汇图AI节点
        /// </summary>
        AIHT,
        /// <summary>
        /// 存图节点
        /// </summary>
        ImageSave,
        /// <summary>
        /// 延迟工具
        /// </summary>
        SleepTool
    }
}
