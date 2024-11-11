﻿using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node._5_Measurement.InjectionHoleMeasurement
{
    internal class NodeResultInjectionHoleMeasurement : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 输出图像
        /// </summary>
        public Bitmap OutputImage { get; set; }
        /// <summary>
        /// 找到的圆半径判定结果
        /// </summary>
        public ResultViewData Result { get; set; }
    }
}
