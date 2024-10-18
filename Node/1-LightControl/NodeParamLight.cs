using JsonSubTypes;
using Newtonsoft.Json;
using YTVisionPro.Device.Light;

namespace YTVisionPro.Node.LightControl
{
    internal class NodeParamLight : INodeParam
    {
        [JsonConstructor]
        public NodeParamLight() { }
        public NodeParamLight(ILight light, int brightness, int time)
        {
            this.Light = light;
            this.LightName = light.UserDefinedName;
            this.Brightness = brightness;
            this.Time = time;
        }
        [JsonIgnore]
        public ILight Light { get; set; }
        /// <summary>
        /// 光源名称，反序列化使用
        /// </summary>
        public string LightName { get; set; }
        public int Brightness { get; set; } //光源值
        public int Time { get; set; } //打开时长
    }
}
