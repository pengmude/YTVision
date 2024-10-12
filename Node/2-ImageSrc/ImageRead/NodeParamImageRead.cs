﻿using JsonSubTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Node.ResultProcessing.DataShow;

namespace YTVisionPro.Node.ImageSrc.ImageRead
{
    internal class NodeParamImageRead : INodeParam
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImagePath;
        /// <summary>
        /// 要显示的窗口名称
        /// </summary>
        public string WindowName;
    }
}