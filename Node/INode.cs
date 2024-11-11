
using System.IO.Ports;

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
        /// <summary>
        /// 将反序列化的参数设置到界面
        /// </summary>
        void SetParam2Form();
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
        /// Stopwatch stopwatch = new Stopwatch();
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
    public enum NodeType
    {
        UNKNOWN,
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
        /// 图像显示
        /// </summary>
        ImageShow,
        /// <summary>
        /// 延迟工具
        /// </summary>
        SleepTool,
        /// <summary>
        /// 检测结果显示
        /// </summary>
        DetectResultShow,
        /// <summary>
        /// 结果总判断
        /// </summary>
        Summarize,
        /// <summary>
        /// 图像裁剪
        /// </summary>
        ImageCrop,
        /// <summary>
        /// 灰度图像
        /// </summary>
        GrayScale,
        /// <summary>
        /// Blob分析
        /// </summary>
        BlobAnalysis,
        /// <summary>
        /// 直线查找
        /// </summary>
        LineFind,
        /// <summary>
        /// 圆查找
        /// </summary>
        CircleFind,
        /// <summary>
        /// 模板匹配
        /// </summary>
        TemplateMatch,
        /// <summary>
        /// 长度测量
        /// </summary>
        LengthMeasurement,
        /// <summary>
        /// 面积测量
        /// </summary>
        AreaMeasurement,
        /// <summary>
        /// ModbusRead读取
        /// </summary>
        ModbusRead,
        /// <summary>
        /// 写入
        /// </summary>
        ModbusWrite,
        /// <summary>
        /// TCP客户端请求
        /// </summary>
        TCPClientRequest,
        /// <summary>
        /// TCP服务器响应
        /// </summary>
        TCPServerResponse,
        /// <summary>
        /// 图像旋转节点
        /// </summary>
        ImageRotate,
        /// <summary>
        /// 两直线平行度
        /// </summary>
        LineParallelism,
        /// <summary>
        /// Modbus软触发
        /// </summary>
        ModbusSoftTrigger,
        /// <summary>
        /// Modbus发送AI结果
        /// </summary>
        AIResultSendByModbus,
        /// <summary>
        /// 相机IO
        /// </summary>
        CameraIO,
        /// <summary>
        /// 注液孔检测
        /// </summary>
        InjectionHole,
        /// <summary>
        /// 图像源
        /// </summary>
        ImageSource
    }
}
