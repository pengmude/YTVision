using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using TDJS_Vision.Node;
using Logger;
using System.Security.Cryptography;
using System.Text;
using TDJS_Vision.Forms.GlobalSignalSettings;

namespace TDJS_Vision
{
    public class ConfigHelper
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
                SolConfig.SolVer = VersionInfo.VersionInfo.GetExeVer();
                SolConfig.SolName = solFile;
                SolConfig.RunInterval = Solution.Instance.RunInterval;
                SolConfig.Devices = Solution.Instance.AllDevices;
                SolConfig.NodeCount = Solution.Instance.NodeCount;
                SolConfig.ProcessCount = Solution.Instance.ProcessCount;

                SolConfig.ProcessInfos = new List<ProcessConfig>();
                foreach (var process in Solution.Instance.AllProcesses)
                {
                    ProcessConfig processConfig = new ProcessConfig();
                    processConfig.ProcessName = process.ProcessName;
                    processConfig.ShowLog = process.ShowLog;
                    processConfig.Enable = process.Enable;
                    processConfig.Level = process.RunLv;
                    processConfig.Group = process.Group;
                    processConfig.IsPassiveTriggered = process.IsPassiveTriggered;
                    processConfig.NodeInfos.Clear();
                    foreach (var nodeConfig in process.Nodes)
                    {
                        NodeConfig node = new NodeConfig();
                        node.NodeType = nodeConfig.NodeType;
                        node.NodeName = nodeConfig.NodeName;
                        node.ID = nodeConfig.ID;
                        node.Active = nodeConfig.Active;
                        node.Selected = nodeConfig.Selected;
                        if (nodeConfig.ParamForm != null && nodeConfig.ParamForm.Params != null)
                            node.NodeParam = nodeConfig.ParamForm.Params;
                        processConfig.NodeInfos.Add(node);
                    }
                    SolConfig.ProcessInfos.Add(processConfig);
                }
                SolConfig.GlobalSignal = Solution.Instance.GlobalSignal;

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
        /// [JsonConverter(typeof(DeviceConverter))]
        /// 列表成员前面声明为：
        /// [JsonConverter(typeof(DeviceListConverter<ICamera>))]
        /// 否则会因为无法解析派生类的Json字符串导致反序列化失败
        /// </summary>
        /// <param name="solFile"></param>
        public static void SolLoad(string solFile, bool flag)
        {
            try
            {
                string json = File.ReadAllText(solFile);
                SolConfig = JsonConvert.DeserializeObject<SolConfig>(json);
                SolConfig.SolName = solFile;

                if (SolConfig == null)
                    throw new Exception("方案配置文件已损坏！");

                #region 清理旧方案设备和流程节点

                Solution.Instance.SolReset();

                #endregion

                Solution.Instance.SolVersion = SolConfig.SolVer;
                Solution.Instance.SolFileName = solFile;
                Solution.Instance.RunInterval = SolConfig.RunInterval;
                Solution.Instance.NodeCount = SolConfig.NodeCount;
                Solution.Instance.ProcessCount = SolConfig.ProcessCount;
                Solution.Instance.GlobalSignal = SolConfig.GlobalSignal;

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

    public class SolConfig
    {
        /// <summary>
        /// 方案版本
        /// </summary>
        public string SolVer;
        public string SolName;
        public int RunInterval;     // 流程循环运行间隔时间
        public int NodeCount;       // 开始创建节点使用的ID-1
        public int ProcessCount;    // 开始创建流程使用的ID-1
        /// <summary>
        /// 方案下的所有设备(光源、相机、PLC)
        /// </summary>
        [JsonConverter(typeof(DeviceListConverter<Device.IDevice>))]
        public List<Device.IDevice> Devices;
        /// <summary>
        /// 方案下所有流程配置
        /// </summary>
        public List<ProcessConfig> ProcessInfos;
        /// <summary>
        /// 全局信号
        /// </summary>
        public GlobalSignal GlobalSignal;
    }

    public class ProcessConfig
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        public string ProcessName;
        /// <summary>
        /// 流程启用/禁用状态
        /// </summary>
        public bool Enable;
        /// <summary>
        /// 流程是否显示日志
        /// </summary>
        public bool ShowLog;
        /// <summary>
        /// 是否是被动触发流程
        /// </summary>
        public bool IsPassiveTriggered;
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
        /// <summary>
        /// 流程中所有节点的名称
        /// </summary>
        public List<NodeConfig> NodeInfos = new List<NodeConfig>();
    }

    public class NodeConfig
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
    /// 字符串加解密工具类（基于AES算法）
    /// </summary>
    public class StringCipher
    {
        private static readonly string DefaultKey = "12345678901234567890123456789012"; // 32字节 = 256位
        private static readonly string DefaultIV = "1234567890123456"; // 16字节 = 128位

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="key">密钥（32字节，Base64或32字符）</param>
        /// <param name="iv">初始化向量（16字节，Base64或16字符）</param>
        /// <returns>Base64编码的密文</returns>
        public static string Encrypt(string plainText, string key = null, string iv = null)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentException("明文不能为空");

            byte[] keyBytes = GetKeyBytes(key ?? DefaultKey);
            byte[] ivBytes = GetIVBytes(iv ?? DefaultIV);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="cipherText">Base64编码的密文</param>
        /// <param name="key">密钥（32字节）</param>
        /// <param name="iv">初始化向量（16字节）</param>
        /// <returns>解密后的明文</returns>
        public static string Decrypt(string cipherText, string key = null, string iv = null)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentException("密文不能为空");

            byte[] keyBytes = GetKeyBytes(key ?? DefaultKey);
            byte[] ivBytes = GetIVBytes(iv ?? DefaultIV);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using (MemoryStream ms = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        // 确保密钥为32字节
        private static byte[] GetKeyBytes(string key)
        {
            if (key.Length == 32)
                return Encoding.UTF8.GetBytes(key);
            else if (key.Length == 44 && key.EndsWith("=")) // 可能是Base64
            {
                try
                {
                    byte[] bytes = Convert.FromBase64String(key);
                    if (bytes.Length == 32) return bytes;
                }
                catch { }
            }
            throw new ArgumentException("密钥必须是32字节的字符串或32字节的Base64编码");
        }

        // 确保IV为16字节
        private static byte[] GetIVBytes(string iv)
        {
            if (iv.Length == 16)
                return Encoding.UTF8.GetBytes(iv);
            else if (iv.Length == 24 && iv.EndsWith("==")) // Base64 编码的16字节
            {
                try
                {
                    byte[] bytes = Convert.FromBase64String(iv);
                    if (bytes.Length == 16) return bytes;
                }
                catch { }
            }
            throw new ArgumentException("IV必须是16字节的字符串或16字节的Base64编码");
        }
    }


    /// <summary>
    /// 用于多态序列化
    /// </summary>
    public class PolyConverter : JsonConverter
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
    /// 用于多态类型IDevice列表的转化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeviceListConverter<T> : JsonConverter
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
    /// <summary>
    /// 用于多态类型ROI列表的转化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ROIListConverter<T> : JsonConverter
    {
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
            int i = 0;
            foreach (var item in values)
            {
                jObject.Add(typeof(T).Name + $"{++i}", JToken.FromObject(item));
            }
            serializer.Serialize(writer, jObject);
        }
    }
}
