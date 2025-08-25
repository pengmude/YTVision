

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace TDJS_Vision.Node._3_Detection.TDAI.Yolo8
{
    public class Yolo8Pose : IYolo8
    {
        // 导入DLL函数
        [DllImport("td_pose.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Initialize(string modelPath, string[] classLabels, int classCount, int modelSize, float scoreThreshold, float nmsThreshold, int keyPointNum);

        [DllImport("td_pose.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Inference(IntPtr detector, byte[] imageData, int width, int height, int channels, out IntPtr boxesData, out int boxesCount, out IntPtr keypointsData, out int keypointsCount, out int keypointsPerObject);

        [DllImport("td_pose.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Cleanup(IntPtr context);

        private IntPtr _handle;

        public ModelType ModelType { get; } = ModelType.POSE;

        /// <summary>
        /// 初始化模型
        /// </summary>
        public void Init(string model_path, string[] class_names, int input_size, float score_threshold, float nms_threshold, int key_point_num)
        {
            _handle = Initialize(model_path, class_names, class_names.Length, input_size, score_threshold, nms_threshold, key_point_num);
            if (_handle == IntPtr.Zero)
            {
                throw new Exception("Failed to initialize YOLO pose model.");
            }
        }

        /// <summary>
        /// 执行推理并返回检测结果列表
        /// </summary>
        public PoseResult Detect(Mat image, int deltaX = 0, int deltaY = 0)
        {
            byte[] imageData = new byte[image.Total() * image.ElemSize()];
            Marshal.Copy(image.Data, imageData, 0, imageData.Length);

            IntPtr boxesDataPtr;
            int boxesCount;
            IntPtr keypointsDataPtr;
            int keypointsCount;
            int keypointsPerObject;

            Inference(_handle, imageData, image.Width, image.Height, image.Channels(), out boxesDataPtr, out boxesCount, out keypointsDataPtr, out keypointsCount, out keypointsPerObject);

            var result = new PoseResult();

            if (boxesDataPtr != IntPtr.Zero && keypointsDataPtr != IntPtr.Zero)
            {
                float[] boxesData = new float[boxesCount];
                float[] keypointsData = new float[keypointsCount];
                Marshal.Copy(boxesDataPtr, boxesData, 0, boxesCount);
                Marshal.Copy(keypointsDataPtr, keypointsData, 0, keypointsCount);

                result.Boxs = new List<Rect>();
                result.KeyPoints = new List<Point>();

                for (int i = 0; i < boxesCount; i += 4)
                {
                    int x = (int)boxesData[i] + deltaX;
                    int y = (int)boxesData[i + 1] + deltaY;
                    int rect_width = (int)boxesData[i + 2];
                    int rect_height = (int)boxesData[i + 3];
                    Rect rect = new Rect(x, y, rect_width, rect_height);
                    result.Boxs.Add(rect);
                }

                for (int i = 0; i < keypointsCount; i += keypointsPerObject)
                {
                    for (int j = 0; j < keypointsPerObject; j += 3)
                    {
                        int x = (int)keypointsData[i + j] + deltaX;
                        int y = (int)keypointsData[i + j + 1] + deltaY;
                        Point point = new Point(x, y);
                        result.KeyPoints.Add(point);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 销毁模型资源
        /// </summary>
        public void Destroy()
        {
            if (_handle != IntPtr.Zero)
            {
                Cleanup(_handle);
                _handle = IntPtr.Zero;
            }
        }
    }
}
