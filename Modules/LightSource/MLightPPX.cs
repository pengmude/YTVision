using System.Diagnostics;
using YTVisionPro.Hardware.Light;

namespace YTVisionPro.Modules.LightSource
{
    /// <summary>
    /// 磐鑫光源模块
    /// </summary>
    internal class MLightPPX : IModule
    {
        //TODO: 要实现每个算子模块直接绑定一个算法对象
        // 硬件模块就绑定硬件对象，模块抽象为参数、运行、结果三大主要部分


        private LightPPX _lightPPX;

        /// <summary>
        /// 模块ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模块参数
        /// </summary>
        public IModuleParam Param { get; set; }

        /// <summary>
        /// 模块结果
        /// </summary>
        public IModuleResult Result { get; set; }

        /// <summary>
        /// 模块是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 模块运行耗时ms
        /// </summary>
        public double RunTime { get; private set; } = 0.0;

        /// <summary>
        /// 执行光源打开或者关闭
        /// </summary>
        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            // 开始计时
            stopwatch.Start();


            if (Enable)
            {
                _lightPPX.Brightness = ((ModuleLightParam)Param).Brightness;
                if (((ModuleLightParam)Param).Open)
                    _lightPPX.TurnOn();
                else
                    _lightPPX.TurnOff();
            }

            // 停止计时
            stopwatch.Stop();
            // 获取总的经过时间（毫秒）
            RunTime = stopwatch.Elapsed.TotalMilliseconds;
        }
    }
}
