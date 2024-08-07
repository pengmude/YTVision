namespace YTVisionPro.Hardware.Camera
{
    // TODO:海康相机类
    internal class CameraHik : ICamera
    {
        public CameraHik() { }

        public CameraHik(string userName)
        {
            UserDefinedName = userName;
        }

        /// <summary>
        /// 设备是否启用
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 硬件硬件名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 用户自定义设备名
        /// </summary>
        public string UserDefinedName { get; set; }

        public DevType DevType { get; } = DevType.CAMERA;

        public bool Open()
        {
            return false;
        }

        public void Close()
        {

        }

        public bool GrabOne()
        {
            return false;
        }

        public bool GrapEncoder()
        {
            return false;
        }

        public bool Reconnect()
        {
            return false;
        }

        public void SetGain(int gainValue)
        {

        }

        public void SetExposureTime(int time)
        {

        }


        /// <summary>
        /// 获取图像的回调事件
        /// </summary>
        event GetImageDelegate ImageCallBack;
    }
}
