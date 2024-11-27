using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using YTVisionPro.Node;
using Logger;

namespace YTVisionPro
{
    internal class ConfigHelper
    {
        public static SolConfig SolConfig = new SolConfig();
        public static EventHandler<bool> DeserializationCompletionEvent;
        /// <summary>
        /// 【方案保存】
        /// 只需要保存反序列化后能够还原软件的必要配置
        /// 即可，其他内部对象通过参数还原
        /// </summary>
        public static void SolSave(string solFile)
        {
            try
            {
                SolConfig.SolVer = Solution.Instance.SolVersion;
                SolConfig.SolName = solFile;
                SolConfig.RunInterval = Solution.Instance.RunInterval;
                SolConfig.Devices = Solution.Instance.AllDevices;
                SolConfig.NodeCount = Solution.Instance.NodeCount;
                SolConfig.ProcessCount = Solution.Instance.ProcessCount;
                SolConfig.SolAiModelNum = Solution.Instance.SolAiModelNum;

                SolConfig.ProcessInfos = new List<ProcessConfig>();
                foreach (var process in Solution.Instance.AllProcesses)
                {
                    ProcessConfig processConfig = new ProcessConfig();
                    processConfig.ProcessName = process.ProcessName;
                    processConfig.Enable = process.Enable;
                    processConfig.Level = process.RunLv;
                    processConfig.Group = process.processGroup;
                    processConfig.NodeInfos.Clear();
                    foreach (var nodeConfig in process.Nodes)
                    {
                        NodeConfig node = new NodeConfig();
                        node.NodeType = nodeConfig.NodeType;
                        node.NodeName = nodeConfig.NodeName;
                        node.ID = nodeConfig.ID;
                        node.Active = nodeConfig.Active;
                        node.Selected = nodeConfig.Selected;
                        node.NodeParam = nodeConfig.ParamForm.Params;
                        processConfig.NodeInfos.Add(node);
                    }
                    SolConfig.ProcessInfos.Add(processConfig);
                }

                ////TODO: 除了方案、流程、节点信息，可能还需要保存其他配置

                // 配置持久化到本地文件
                string json = JsonConvert.SerializeObject(SolConfig, Formatting.Indented);
                File.WriteAllText(solFile, json);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 【方案加载】
        /// 配置中涉及到接口类、抽象类等多态成员
        /// 能够反序列化为对应派生类型对象，首先得保证
        /// 此类成员前面声明为:
        /// [JsonConverter(typeof(PolyConverter))]
        /// 列表成员前面声明为：
        /// [JsonConverter(typeof(PolyListConverter<ICamera>))]
        /// 否则会因为无法解析派生类的Json字符串导致反序列化失败
        /// </summary>
        /// <param name="solFile"></param>
        public static void SolLoad(string solFile, bool flag)
        {
            try
            {
                string json = File.ReadAllText(solFile);
                SolConfig = JsonConvert.DeserializeObject<SolConfig>(json);

                if (SolConfig == null)
                    throw new Exception("方案配置文件已损坏！");

                #region 清理旧方案设备和流程节点

                Solution.Instance.SolReset();

                #endregion

                #region 测试代码

                // 测试反序列化后的设备是否可用
                //foreach (var dev in SolConfig.Devices)
                //{
                //    if (dev is ICamera hik)
                //    {
                //        // 反序列化完必须调用CreateDevice才能还原相机对象
                //        hik.CreateDevice();

                //        Solution.Instance.CameraDevices[0] = hik;

                //        hik.Open();

                //        MessageBox.Show($"相机是否打开：{hik.IsOpen}\n 设备类型：{hik.DevType}\n SN：{hik.SN}\n 品牌：{hik.Brand}\n" +
                //            $"设备名称：{hik.DevName}\n 用户自定义名称：{hik.UserDefinedName}");

                //    }
                //    else if(dev is ILight light)
                //    {
                //        light.CreateDevice();
                //        MessageBox.Show($"光源是否打开：{light.IsOpen}\n 设备类型：{light.DevType}\n 品牌：{light.Brand}\n" +
                //            $"设备名称：{light.DevName}\n 用户自定义名称：{light.UserDefinedName}");
                //    }
                //    else if(dev is IPlc plc)
                //    {
                //        plc.CreateDevice();
                //        MessageBox.Show($"PLC是否连接：{plc.IsConnect}\n 设备类型：{plc.DevType}\n 品牌：{plc.Brand}\n" +
                //            $"设备名称：{plc.DevName}\n 用户自定义名称：{plc.UserDefinedName}");
                //    }
                //}

                // 打印反序列化节点结果
                //foreach (var processConfig in SolConfig.ProcessInfos)
                //{
                //    string ss = $"流程名称：{processConfig.ProcessName} \n流程启用：{processConfig.Enable}\n";
                //    foreach (var nodeInfo in processConfig.NodeInfos)
                //    {
                //        ss += $"\n节点参数：{nodeInfo.NodeParam}\n";
                //    }
                //    MessageBox.Show(ss);
                //}

                #endregion

                Solution.Instance.SolVersion = SolConfig.SolVer;
                Solution.Instance.SolFileName = SolConfig.SolName;
                Solution.Instance.RunInterval = SolConfig.RunInterval;
                Solution.Instance.NodeCount = SolConfig.NodeCount;
                Solution.Instance.ProcessCount = SolConfig.ProcessCount;
                Solution.Instance.SolAiModelNum = SolConfig.SolAiModelNum;

                // 发送反序列化完成事件
                // 目的：
                // 1.先触发光源管理窗口还原所有光源，还原完成触发光源完成事件
                // 2.相机管理窗口订阅光源完成事件进行相机还原，完成后触发相机完成事件 移除旧方案的设备管理窗口的设备控件
                // 3.PLC管理窗口订阅相机完成事件进行PLC还原 完成后触发PLC完成事件，Modbus-》TCP也是如此
                // 4.流程管理窗口订阅PLC完成事件进行流程还原，因为流程还原需要用到设备，这样保证了在还原流程时设备可用
                DeserializationCompletionEvent?.Invoke(null, flag);
                Solution.Instance.NodeCount = SolConfig.NodeCount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    internal class SolConfig
    {
        /// <summary>
        /// 方案版本
        /// </summary>
        public string SolVer;
        public string SolName;
        public int RunInterval;     // 流程循环运行间隔时间
        public int NodeCount;       // 开始创建节点使用的ID-1
        public int ProcessCount;    // 开始创建流程使用的ID-1
        public int SolAiModelNum;   // 方案AI模型计数
        /// <summary>
        /// 方案下的所有设备(光源、相机、PLC)
        /// </summary>
        [JsonConverter(typeof(PolyListConverter<Device.IDevice>))]
        public List<Device.IDevice> Devices;
        /// <summary>
        /// 方案下所有流程配置
        /// </summary>
        public List<ProcessConfig> ProcessInfos;
    }

    internal class ProcessConfig
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        public string ProcessName;
        /// <summary>
        /// 流程中所有节点的名称
        /// </summary>
        public List<NodeConfig> NodeInfos = new List<NodeConfig>();
        /// <summary>
        /// 流程启用/禁用状态
        /// </summary>
        public bool Enable;
        /// <summary>
        /// 流程运行优先级
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ProcessLvEnum Level;

        /// <summary>
        /// 流程运行组别
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ProcessGroup Group;
    }

    internal class NodeConfig
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public NodeType NodeType;
        public string NodeName;
        public int ID;
        public bool Active;
        public bool Selected;
        [JsonConverter(typeof(PolyConverter))]
        public INodeParam NodeParam;
    }

 
    /// <summary>
    /// 用于多态序列化
    /// </summary>
    internal class PolyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        #pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
        #pragma warning disable CS8765 // 参数类型的为 Null 性与重写成员不匹配(可能是由于为 Null 性特性)。 
        #pragma warning disable CS8604 // 引用类型参数可能为 null。
        #pragma warning disable CS8603 // 可能返回 null 引用。
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var jObject = JObject.Load(reader);
                foreach (var item in jObject.Properties())
                {

                    Type type = Type.GetType(item.Name);

                    var value = item.Value.ToObject(type);

                    return value;

                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, ex.Message, true);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)

        {
            JObject jObject = new JObject();

            jObject.Add(value.GetType().FullName, JToken.FromObject(value));

            serializer.Serialize(writer, jObject);
        }
        #pragma warning restore CS8603 // 可能返回 null 引用。
        #pragma warning restore CS8604 // 引用类型参数可能为 null。
        #pragma warning restore CS8765 // 参数类型的为 Null 性与重写成员不匹配(可能是由于为 Null 性特性)。
        #pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
    }

    /// <summary>
    /// 用于多态列表的转化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PolyListConverter<T> : JsonConverter
    {
        #pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
        #pragma warning disable CS8765 // 参数类型的为 Null 性与重写成员不匹配(可能是由于为 Null 性特性)。 
        #pragma warning disable CS8604 // 引用类型参数可能为 null。
        #pragma warning disable CS8603 // 可能返回 null 引用。
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var jObject = JObject.Load(reader);
                List<T> values = new List<T>();
                foreach (var item in jObject.Properties())
                {
                    Type type = Type.GetType((string)item.Value["ClassName"]);
                    var value = item.Value.ToObject(type);
                    values.Add((T)value);
                }
                return values;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var values = (List<T>)value;
            JObject jObject = new JObject();
            foreach (var item in values)
            {
                jObject.Add(((Device.IDevice)item).UserDefinedName, JToken.FromObject(item));
            }
            serializer.Serialize(writer, jObject);
        }
        #pragma warning restore CS8603 // 可能返回 null 引用。
        #pragma warning restore CS8604 // 引用类型参数可能为 null。
        #pragma warning restore CS8765 // 参数类型的为 Null 性与重写成员不匹配(可能是由于为 Null 性特性)。
        #pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
    }
}
