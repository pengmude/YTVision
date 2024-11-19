
namespace YTVisionPro.Node._6_LogicTool.SharedVariable
{
    internal class NodeParamSharedVariable : INodeParam
    {
        /// <summary>
        /// 是否是读取变量
        /// </summary>
        public bool IsRead;
        /// <summary>
        /// 读取的变量名称
        /// </summary>
        public string ReadName;
        /// <summary>
        /// 读取转换的类型
        /// </summary>
        public SharedVarTypeEnum Type;
        /// <summary>
        /// 是否指定输出数组某个值
        /// </summary>
        public bool Flag;
        /// <summary>
        /// 指定值的索引
        /// </summary>
        public int Index;
        /// <summary>
        /// 订阅节点文本1
        /// </summary>
        public string Text1;
        /// <summary>
        /// 订阅节点文本2
        /// </summary>
        public string Text2;
        /// <summary>
        /// 写入变量名称
        /// </summary>
        public string WriteName;
    }

    /// <summary>
    /// 共享变量类型枚举
    /// </summary>
    internal enum SharedVarTypeEnum
    {
        /// <summary>
        /// 任意类型
        /// </summary>
        AllType,
        /// <summary>
        /// 布尔类型
        /// </summary>
        Bool,
        /// <summary>
        /// 整型
        /// </summary>
        Int,
        /// <summary>
        /// 字符串类型
        /// </summary>
        String,
        /// <summary>
        /// 单精度浮点
        /// </summary>
        Float,
        /// <summary>
        /// 双精度浮点
        /// </summary>
        Double,
        /// <summary>
        /// Bitmap图像类型
        /// </summary>
        Bitmap,
        /// <summary>
        /// Bitmap图像数组类型
        /// </summary>
        BitmapArr,
        /// <summary>
        /// 算法结果类型
        /// </summary>
        ResultViewData
    }
}
