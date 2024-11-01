using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node._4_Detection.Caliper
{
    /// <summary>
    /// 直线类(对应一个矩形内的一条直线)
    /// </summary>
    public class LineInfo
    {
        public List<Dictionary<Point, byte>> LinePixels = new List<Dictionary<Point, byte>>(); //直线的像素值，key为坐标 value为该坐标像素值      
        public Point lineStart; //线段开始点  
        public Point lineEnd; //线段结束点
    }
}
