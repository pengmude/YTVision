using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using OpenCvSharp;
using RotatedRect = TDJS_Vision.Node._3_Detection.TDAI.Yolo8.RotatedRect;
using System.Linq;

namespace TDJS_Vision.Node._3_Detection.TDAI
{
    public class NodeResultTDAI : INodeResult
    {
        public int RunTime { get; set; }

        [DisplayName("AI输出结果")]
        public AlgorithmResult AlgorithmResult { get; set; } = new AlgorithmResult();
    }

    /// <summary>
    /// AI检出结果
    /// </summary>
    public class AlgorithmResult
    {
        /// <summary>
        /// 所有检测项的OK/NG
        /// </summary>
        public Dictionary<string, List<SingleDetectResult>> DetectResults { get; set; } = new Dictionary<string, List<SingleDetectResult>>();
        /// <summary>
        /// DetectResults所有检测项都OK
        /// </summary>
        public bool IsAllOk => DetectResults.Values.SelectMany(list => list).All(result => result.IsOk);
        /// <summary>
        /// 带颜色的当前结果检测框
        /// </summary>
        public List<ColorRotatedRect> Rects { get; set; } = new List<ColorRotatedRect>();
        /// <summary>
        /// 带颜色的缓存上次NG结果的检测框
        /// </summary>
        public Dictionary<string, List<ColorRotatedRect>> RectsNgMap { get; set; } = new Dictionary<string, List<ColorRotatedRect>>();
        /// <summary>
        /// 带颜色的文本
        /// </summary>
        public List<ColorText> Texts { get; set; } = new List<ColorText>();
        /// <summary>
        /// 带颜色的线段
        /// </summary>
        public List<ColorLine> Lines { get; set; } = new List<ColorLine>();
        /// <summary>
        /// 带颜色的圆
        /// </summary>
        public List<ColorCircle> Circles { get; set; } = new List<ColorCircle>();

        public void Clear()
        {
            DetectResults.Clear();
            Rects.Clear();
            RectsNgMap.Clear();
            Texts.Clear();
            Lines.Clear();
            Circles.Clear();
        }
    }

    /// <summary>
    /// 单个检测项结果
    /// </summary>
    public class SingleDetectResult
    {
        /// <summary>
        /// 检测值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 检测项是否OK
        /// </summary>
        public bool IsOk { get; set; }
        /// <summary>
        /// 检测项名称
        /// </summary>
        public string Name { get; set; }
        public SingleDetectResult() { }

        public SingleDetectResult(string name, string value, bool isOk)
        {
            Name = name;
            Value = value;
            IsOk = isOk;
        }
    }
    /// <summary>
    /// 带有颜色标记的旋转矩形框
    /// </summary>
    public class ColorRotatedRect
    {
        public ColorRotatedRect(RotatedRect rotatedRect)
        {
            this.RotatedRect = rotatedRect;
        }
        public ColorRotatedRect(Rect Rect)
        {
            this.RotatedRect = new RotatedRect(Rect);
        }
        public ColorRotatedRect(Rect Rect, Color color)
        {
            this.RotatedRect = new RotatedRect(Rect);
            this.Color = color;
        }
        public ColorRotatedRect(int x, int y, int width, int height)
        {
            RotatedRect rect = new RotatedRect();
            rect.center.x = x + width / 2.0f;
            rect.center.y = y + height / 2.0f;
            rect.size.width = width;
            rect.size.height = height;
            rect.angle = 0.0f;
            this.RotatedRect = rect;
        }

        private List<bool> _flags = new List<bool>();
        /// <summary>
        /// 旋转矩形
        /// </summary>
        public RotatedRect RotatedRect {  get; set; }
        /// <summary>
        /// 绘制的颜色
        /// </summary>
        public Color Color
        {
            get
            {
                return _flags.TrueForAll(item => item == true) ? Color.Green : Color.Red;
            }
            set { }
        }
        /// <summary>
        /// 添加的条件决定绘制的颜色
        /// </summary>
        /// <param name="flag"></param>
        public void AddFlag(bool flag) 
        {
            _flags.Add(flag);
        }
    }

    /// <summary>
    /// 带颜色的文本
    /// </summary>
    public class ColorText
    {
        public string Text { get; set; }
        public Color Color { get; set; }
        public ColorText(string text, Color color)
        {
            Text = text;
            Color = color;
        }
    }

    /// <summary>
    /// 带颜色的线段
    /// </summary>
    public class ColorLine
    {
        public PointF P1 { get; set; }
        public PointF P2 { get; set; }
        public Color Color { get; set; }

        public ColorLine(PointF p1, PointF p2, Color color)
        {
            P1 = p1;
            P2 = p2;
            Color = color;
        }

        public ColorLine(LineSegmentPoint line, Color color)
        {
            P1 = new PointF(line.P1.X, line.P1.Y);
            P2 = new PointF(line.P2.X, line.P2.Y);
            Color = color;
        }
    }

    /// <summary>
    /// 带颜色的圆
    /// </summary>
    public class ColorCircle
    {
        public PointF Center { get; set; }
        public float Radius { get; set; }
        public Color Color { get; set; }

        public ColorCircle(PointF center, int radius, Color color)
        {
            Center = center;
            Radius = radius;
            Color = color;
        }
        public ColorCircle(CircleSegment circle, Color color)
        {
            Center = new PointF(circle.Center.X, circle.Center.Y);
            Radius = circle.Radius;
            Color = color;
        }
    }
}
