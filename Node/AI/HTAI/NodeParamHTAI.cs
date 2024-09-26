using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;

namespace YTVisionPro.Node.AI.HTAI
{
    internal class NodeParamHTAI : INodeParam
    {
        /// <summary>
        /// 模型句柄
        /// </summary>
        [JsonIgnore]
        public IntPtr TreePredictHandle { get; set; }
        /// <summary>
        /// 订阅得节点文本（反序列化用）
        /// </summary>
        public string Text1 { get; set; }
        /// <summary>
        /// 订阅得节点属性文本（反序列化用）
        /// </summary>
        public string Text2 { get; set; }
        /// <summary>
        /// tree文件文本（反序列化用）
        /// </summary>
        public string TreePath { get; set; }
        /// <summary>
        /// 反序列化加载模型用
        /// </summary>
        public ModelInitParams ModelInitParams { get; set; }
        /// <summary>
        /// NGType类，包含配置信息和输出结果
        /// </summary>
        public List<NGTypeConfig> AllNgConfigs { get; set; }
        /// <summary>
        /// 检测出来的节点结果数
        /// </summary>
        public int TestNum { get; set; }
        /// <summary>
        /// 显示渲染图窗口名称
        /// </summary>
        public string WindowName {  get; set; }
    }
    /// <summary>
    /// 加载模型的参数
    /// </summary>
    internal struct ModelInitParams 
    {
        public string TreePath;
        public List<string> NodeNames;
        public int TestNodeNum;
        public string DeviceType;
        public int DeviceID;
        public ModelInitParams(string treeFile, List<string> nodeNames, int testNodeNum, string deviceType, int deviceID)
        {
            TreePath = treeFile;
            NodeNames = nodeNames;
            TestNodeNum = testNodeNum;
            DeviceType = deviceType;
            DeviceID = deviceID;
        }
    }

}
