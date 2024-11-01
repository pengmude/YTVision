using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node._4_Detection.Caliper
{
    /// <summary>
    /// 矩形类(对应一个卡尺内的矩形)
    /// </summary>
    public class Rectangle
    {
        public LineInfo lineInfo = new LineInfo(); //直线信息
        public int rectWidth = 15;  //矩形宽
        public int rectHeight = 35; //矩形长
        public Point center; //矩形中心点
        public List<Point[]> points = new List<Point[]>(); //矩形的角点集合
    }
}
