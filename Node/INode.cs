
using System.IO.Ports;

namespace TDJS_Vision.Node
{
    /// <summary>
    /// 节点参数界面接口类
    /// </summary>
    public interface INodeParamForm
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
    public interface INodeParam { }

    /// <summary>
    /// 节点运行结果接口类
    /// </summary>
    public interface INodeResult 
    {
        int RunTime { get; set; } // 运行时间
    }

    /// <summary>
    /// 节点运行结果类
    /// </summary>
    public class NodeReturn
    {
        /// <summary>
        /// 节点运行标志，用来标记当前节点运行完后是否继续运行流程下一个节点
        /// </summary>
        public NodeRunFlag Flag;
        /// <summary>
        /// 当前流程下一个要运行的节点索引
        /// </summary>
        public int NextIndex = -1;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="nextIndex"></param>
        public NodeReturn(NodeRunFlag flag = NodeRunFlag.ContinueRun, int nextIndex = -1)
        {
            Flag = flag;
            NextIndex = nextIndex;
        }
    }

    public enum NodeStatus
    {
        /// <summary>
        /// 未运行
        /// </summary>
        Unexecuted,
        /// <summary>
        /// 运行成功
        /// </summary>
        Successful,
        /// <summary>
        /// 运行失败
        /// </summary>
        Failed
    }

    /// <summary>
    /// 用来控制当前节点运行完是否继续运行流程下一个节点
    /// </summary>
    public enum NodeRunFlag
    {
        ContinueRun,
        StopRun
    }

    /// <summary>
    /// 节点运行状态码
    /// </summary>
    public enum NodeRunStatusCode
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
        /// 瞳达AI节点
        /// </summary>
        AITD,
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
        /// Modbus软触发
        /// </summary>
        ModbusSoftTrigger,
        /// <summary>
        /// 发送AI结果
        /// </summary>
        AIResultSend,
        /// <summary>
        /// 相机IO
        /// </summary>
        CameraIO,
        /// <summary>
        /// 图像源
        /// </summary>
        ImageSource,
        /// <summary>
        /// 图像分割
        /// </summary>
        ImageSplit,
        /// <summary>
        /// 二维码识别
        /// </summary>
        QRScan,
        /// <summary>
        /// 模版匹配
        /// </summary>
        MatchTemplate,
        /// <summary>
        /// 图片定时删除
        /// </summary>
        ImageFileDelete,
        /// <summary>
        /// 共享变量
        /// </summary>
        SharedVariable,
        /// <summary>
        /// 生成Excel表格
        /// </summary>
        GenerateExcel,
        /// <summary>
        /// AI结果绘制
        /// </summary>
        DrawAIResult,
        /// <summary>
        /// 条件运行
        /// </summary>
        ConditionRun,
        /// <summary>
        /// 触发流程
        /// </summary>
        ProcessTrigger,
        /// <summary>
        /// 流程信号
        /// </summary>
        ProcessSignal,
        /// <summary>
        /// IF节点
        /// </summary>
        If,
        /// <summary>
        /// Else节点
        /// </summary>
        Else,
        /// <summary>
        /// EndIf节点
        /// </summary>
        EndIf,
        /// <summary>
        /// 锂电池极耳检测
        /// </summary>
        BatteryEar,
        /// <summary>
        /// C#脚本
        /// </summary>
        CSharpScript
    }
}
