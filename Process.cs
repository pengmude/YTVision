using System;
using System.Collections.Generic;
using System.Xml.Linq;
using YTVisionPro.Hardware.Camera;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Node;

namespace YTVisionPro
{
    /// <summary>
    /// 检测流程类
    /// 一个方案可以拥有多个流程
    /// 每个流程也可以单独执行
    /// </summary>
    public class Process
    {
        private List<IPlc> _plcList;
        private List<ICamera> _cameraList;
        private List<ILight> _light;
        private List<NodeBase> _nodes = new List<NodeBase>();

        private static int _countInstance = 0;

        /// <summary>
        /// 类实例id
        /// </summary>
        private int _id = 0;

        /// <summary>
        /// 流程ID
        /// </summary>
        public int ID { get { return _id; } }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 流程包含的节点
        /// </summary>
        public List<NodeBase> Nodes { get => _nodes;}


        /// <summary>
        /// 流程运行时间
        /// </summary>
        public TimeSpan RunTime { get; private set; }

        ///// <summary>
        ///// 流程所属方案
        ///// </summary>
        //public Solution Solution { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="solution"></param>
        public Process(/*Solution solution*/)
        {
            //Solution = solution;
            _id = _countInstance++;
        }

        public void AddNode(NodeBase node)
        {
            _nodes.Add(node);
        }

        /// <summary>
        /// 流程开始运行
        /// </summary>
        public void Run()
        {
            foreach (var item in _nodes)
            {
                if(item is NodeBase node)
                {
                    node.Run();
                }
            }
        }

        /// <summary>
        /// 流程停止运行
        /// </summary>
        public void Stop()
        {

        }
    }
}
