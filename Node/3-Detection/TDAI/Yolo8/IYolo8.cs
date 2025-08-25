namespace TDJS_Vision.Node._3_Detection.TDAI.Yolo8
{
    /// <summary>
    /// YOLO8泛型模版类接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IYolo8
    {
        ModelType ModelType { get; }
        /// <summary>
        /// 初始化模型
        /// </summary>
        /// <param name="model_path"></param>
        /// <param name="class_names"></param>
        /// <param name="input_size"></param>
        /// <param name="score_threshold"></param>
        /// <param name="nms_threshold"></param>
        void Init(string model_path, string[] class_names, int input_size, float score_threshold, float nms_threshold, int key_point_num = 0);

        /// <summary>
        /// 销毁模型资源
        /// </summary>
        void Destroy();
    }
}
