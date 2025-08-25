using System;
using System.ComponentModel;

namespace TDJS_Vision.Node._6_LogicTool.SharedVariable
{
    public class NodeResultSharedVariable : INodeResult
    {
        public int RunTime { get; set; }
        [DisplayName("读到的共享变量")]
        public SharedVarValue Value { get; set; }
    }
    /// <summary>
    /// 共享变量值类
    /// </summary>
    public class SharedVarValue
    {
        public object Data;
        public Type Type;
        public SharedVarValue()
        {
            Data = default(object);
            Type = Data?.GetType();
        }
        public SharedVarValue(object data, Type type)
        {
            Data = data;
            Type = type;
        }
    }
}
