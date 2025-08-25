using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCvSharp;
using Point = OpenCvSharp.Point;

namespace TDJS_Vision.Node._3_Detection.TDAI.Yolo8
{
    // 定义DetResult结构体
    [StructLayout(LayoutKind.Sequential)]
    public struct DetResult
    {
        public int ClassId;
        public float Score;
        public Rect Box;
    }
    // 定义 ObbResult 结构体
    [StructLayout(LayoutKind.Sequential)]
    public struct ObbResult
    {
        public RotatedRect RotateBox;
        public int ClassId;
        public float Score;
    }

    // 定义 PoseResult 结构体
    [StructLayout(LayoutKind.Sequential)]
    public struct PoseResult
    {
        public List<Rect> Boxs;
        public List<Point> KeyPoints;
    }

    // 定义 SegResult 结构体
    [StructLayout(LayoutKind.Sequential)]
    public struct SegResult
    {
        public int ClassId;
        public float Score;
        public Rect Box;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RotatedRect
    {
        public Point2f center;
        public Size2f size;
        public float angle;

        // 构造函数，从Rect对象初始化RotatedRect
        public RotatedRect(Rect rect, float angle = 0)
        {
            // 初始化中心点为Rect的中心
            this.center = new Point2f(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

            // 初始化尺寸为Rect的宽度和高度
            this.size = new Size2f(rect.Width, rect.Height);

            // 使用传入的角度，或者默认为0度
            this.angle = angle;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point2f
    {
        public float x;
        public float y;
        public Point2f(float x, float y) 
        {
            this.x = x;
            this.y = y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Size2f
    {
        public float width;
        public float height;
        public Size2f(float w, float h)
        {
            this.width = w;
            this.height = h;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RotatedRectResult
    {
        public float center_x;
        public float center_y;
        public float width;
        public float height;
        public float angle;
        public int class_id;
        public float confidence;
    }
}
