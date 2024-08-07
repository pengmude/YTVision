namespace YTVisionPro.Hardware
{
    public interface IDevice
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// 硬件硬件名称
        /// </summary>
        string DevName { get; set; }
        /// <summary>
        /// 用户自定义设备名
        /// </summary>
        string UserDefinedName { get; set; }

        DevType DevType { get; }
    }

    /// <summary>
    /// 设备类型：光源、相机和PLC
    /// </summary>
    public enum DevType
    {
        LIGHT,
        CAMERA,
        PLC
    }
}
