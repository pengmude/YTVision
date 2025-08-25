using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace TDJS_Vision.Node._3_Detection.TDAI.Yolo8
{
    /// <summary>
    /// Yolo8 Seg 类
    /// </summary>
    public class Yolo8Seg : IYolo8
    {
        // 导入DLL函数
        [DllImport("td_seg.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Initialize(
            string modelPath,
            string[] classNames,
            int classCount,
            int modelSize,
            float scoreThreshold,
            float nmsThreshold);

        [DllImport("td_seg.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Infer(
            IntPtr context,
            byte[] imageData,
            int width,
            int height,
            int channels,
            out int boxCount,
            out IntPtr boxData,
            out IntPtr scoreData,
            out IntPtr classIdData,
            out int maskCount,
            out IntPtr maskWidth,
            out IntPtr maskHeight,
            out IntPtr maskData);

        [DllImport("td_seg.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Release(IntPtr context);

        private IntPtr _handle;
        /// <summary>
        /// 模型类型
        /// </summary>
        public ModelType ModelType { get; } = ModelType.SEG;

        /// <summary>
        /// 初始化模型
        /// </summary>
        public void Init(string model_path, string[] class_names, int input_size, float score_threshold, float nms_threshold, int key_point_num = 0)
        {
            _handle = Initialize(model_path, class_names, class_names.Length, input_size, score_threshold, nms_threshold);
            if (_handle == IntPtr.Zero)
            {
                throw new Exception("Failed to initialize YOLO segmentation model.");
            }
        }

        /// <summary>
        /// 执行推理并返回检测结果列表
        /// </summary>
        public List<SegResult> Detect(Mat image, int deltaX = 0, int deltaY = 0)
        {
            byte[] imageData = new byte[image.Total() * image.ElemSize()];
            Marshal.Copy(image.Data, imageData, 0, imageData.Length);

            int boxCount;
            IntPtr boxDataPtr, scoreDataPtr, classIdDataPtr;
            int maskCount;
            IntPtr maskWidthPtr, maskHeightPtr, maskDataPtr;

            Infer(_handle, imageData, image.Width, image.Height, image.Channels(),
                   out boxCount, out boxDataPtr, out scoreDataPtr, out classIdDataPtr,
                   out maskCount, out maskWidthPtr, out maskHeightPtr, out maskDataPtr);

            var results = new List<SegResult>();

            if (boxDataPtr != IntPtr.Zero)
            {
                for (int i = 0; i < boxCount; i++)
                {
                    int left = Marshal.ReadInt32(boxDataPtr, i * 4 * sizeof(int));
                    int top = Marshal.ReadInt32(boxDataPtr, (i * 4 + 1) * sizeof(int));
                    int width = Marshal.ReadInt32(boxDataPtr, (i * 4 + 2) * sizeof(int));
                    int height = Marshal.ReadInt32(boxDataPtr, (i * 4 + 3) * sizeof(int));

                    float score = BitConverter.ToSingle(BitConverter.GetBytes(Marshal.ReadInt32(scoreDataPtr, i * sizeof(float))), 0);
                    int classId = Marshal.ReadInt32(classIdDataPtr, i * sizeof(int));

                    results.Add(new SegResult
                    {
                        ClassId = classId,
                        Score = score,
                        Box = new Rect(left + deltaX, top + deltaY, width, height)
                    });
                }
            }

            return results;
        }

        /// <summary>
        /// 销毁模型资源
        /// </summary>
        public void Destroy()
        {
            if (_handle != IntPtr.Zero)
            {
                Release(_handle);
                _handle = IntPtr.Zero;
            }
        }
    }
}

