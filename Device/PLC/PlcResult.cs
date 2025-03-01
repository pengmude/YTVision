﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Device.PLC
{
    internal class PlcResult
    {
        /// <summary>
        /// 指示本次操作是否成功。
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 失败信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    internal class PlcResult<T> : PlcResult
    {
        /// <summary>
        /// 用户自定义的泛型数据
        /// </summary>
        public T Content { get; set; }
    }


    internal class PlcResult<T1, T2, T3, T4> : PlcResult
    {
        /// <summary>
        /// 用户自定义的泛型数据1
        /// </summary>
        public T1 Content1 { get; set; }

        /// <summary>
        /// 用户自定义的泛型数据2
        /// </summary>
        public T2 Content2 { get; set; }

        /// <summary>
        /// 用户自定义的泛型数据3 
        /// </summary>
        public T3 Content3 { get; set; }

        /// <summary>
        /// 用户自定义的泛型数据4
        /// </summary>
        public T4 Content4 { get; set; }
    }

}
