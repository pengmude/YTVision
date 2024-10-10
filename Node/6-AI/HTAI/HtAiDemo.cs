using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;   // 用 DllImport 需用此 命名空间
using System.Windows.Forms;
using System.IO;

namespace YTVisionPro.Node.AI.HTAI
{

    internal class HTAPI
    {
        public enum TeArrLen
        {
            PATH_LEN = 256,
            NAME_LEN = 16,
            MAX_DETECT_NUM = 1024,
            TEXT_LEN = 128
        };

        //error information 
        public enum ErrorCode
        {
            ErrorNone = 0,            // No error.
            ErrorGpuOom = 1,            // Gpu oom
            ErrorClassNumUnequal = 2,            // The number of model types is inconsistent with the number of area thresholds
            ErrorVideoCardDriver = 3,            // Video card driver error
            ErrorPath = 4,            // The path error
            ErrorJsonFile = 5,            // The json file error
            ErrorDeleteFolderFile = 6,            // The delete folder error
            ErrorCreateFolder = 7,            // The create folder error
            ErrorSampleNum = 8,            // Insufficient marked quantity
            ErrorParameter = 9,            // Interface parameter error
            ErrorMultiRstNum = 10,           // 多节点激活节点数跟获取的数量不一致
            ErrorInitTreeName = 11,           // 初始化多节点树，节名存在错误
            ErrorImagEmpty = 12,           // 图像错误
            ErrorNodeName = 13,           // 节点名存在错误，或者节点名未找到，区分大小写。
            ErrorUnknown = 10001,        // 未知错误.
        };

        //* 节点类型：缺陷检测、ocr、分类
        public enum NodeType
        {
            NTDefect = 0,
            NTOcr = 1,
            NTClassify = 2
        };

        //* 图像格式
        public struct Point
        {
            public int x;
            public int y;
        };

        //* OCR检测结果
        [StructLayout(LayoutKind.Sequential)]
        public struct OcrResult
        {
            public int left_x;
            public int left_y;
            public int right_x;
            public int right_y;
            //* 检出类别,对应标注
            public int class_id;
            //* 类别得分,得分越高越可信
            public float score;
        };

        //图像数据首地址
        [StructLayout(LayoutKind.Sequential)]
        public struct ImageHt
        {
            public IntPtr data;                                        // 图像的数据首地址
            public int width;                                          // 图像的宽度
            public int height;                                         // 图像的高度
            public int channels;                                       // 图像的通道数
            public int width_step;                                     // 图像每行的步长（bytes）
        };

        //图像数据 类似Hobject的数据格式
        [StructLayout(LayoutKind.Sequential)]
        public struct ImageHtH
        {
            public IntPtr data_b;                                        // 图像的数据b通道首地址
            public IntPtr data_g;                                        // 图像的数据g通道首地址 若是单通道则赋值为nullptr
            public IntPtr data_r;                                        // 图像的数据r通道首地址 若是单通道则赋值为nullptr
            public int width;                                          // 图像的宽度
            public int height;                                         // 图像的高度
            public int width_step;                                     // 图像每行的步长（bytes）
        };

        //* 缺陷检测结果
        [StructLayout(LayoutKind.Sequential)]
        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct ClassNamesCharp
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)(TeArrLen.NAME_LEN))]
            public char[] class_name;
        }

        //* 缺陷检测结果
        [StructLayout(LayoutKind.Sequential)]
        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DetectResult
        {
            public int x;                          //缺陷所在外接矩形位置x
            public int y;                          //y
            public int width;                      //宽
            public int height;                     //高
            public int class_id;                   //缺陷类别（从1开始）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)(TeArrLen.NAME_LEN))]
            public byte[] class_name;
            public int area;                       //缺陷面积大小 分类和定位不会给出
            //int text_size;                  / /文本行识别的大小，考虑到结束字符，最大值是127
            //char text[128];                   //最后会有一个结束符'\0'
            //int angle;                        //角度  当前使用
            //float angle_f;                    //预留一个接口 浮点型，后续可能启用
            public int text_size;                  //文本行识别的大小，考虑到结束字符，最大值是127

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)(TeArrLen.TEXT_LEN))]
            public char[] text;                     //最后会有一个结束符'\0'               
            public int angle;                      //角度  当前使用
            public float angle_f;                  //预留一个接口 浮点型，后续可能启用
            public float score;                    //得分
                                                   //以下缺陷检测检出信息
            public int points_2d_size;             //
            public IntPtr points_2d;           //对应的缺陷像素点
            public int contour_size;               //存在多少轮廓
            public IntPtr each_contour_size;         //每个轮廓对应的像素点数量
            public IntPtr contour_2d;          //缺陷轮廓点
        };

        //* 多节点中单个节点的测试结果
        [StructLayout(LayoutKind.Sequential)]
        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NodeResult
        {
            public int node_id;                    //节点ID

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)(TeArrLen.PATH_LEN))]
            public char[] node_name;

            public int node_type;             //节点类型 分割 定位 分类 文本行识别
            public int node_aux_type;          //节点辅助类型 0是常规，1对应定位的旋转还有单字符识别。
            public int node_type_total;            //0-6分别对应缺陷检测，无旋转定位  分类，文本行识别，通用字符识别，旋转定位，单字符旋转 根据用户需要选择上面两个或者下面这个。
            public int detect_results_num;         //检出目标个数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)(TeArrLen.MAX_DETECT_NUM))]
            public DetectResult[] detect_results; //检出结果
        };

        //* 对应char[16]bye_info;
        [StructLayout(LayoutKind.Sequential)]
        public struct ByteInfoC
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)(16))]
            public char[] bye_info;                //节点名
        };

        //* 获取各个节点的信息
        [StructLayout(LayoutKind.Sequential)]
        public struct ProjNodeInfo
        {
            public int node_id;                     //节点ID

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)(TeArrLen.PATH_LEN))]
            public char[] node_name;                //节点名

            int node_type;             //节点类型 定位ocr 分割 分类
            public int class_name_len;         //类别数量
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)(TeArrLen.PATH_LEN))]
            public ByteInfoC[] class_names; //类别名称 在c中就是 char class_names[256][16]
            //public char[] class_names; //类别名称

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)(TeArrLen.PATH_LEN))]
            public int[] class_ids; //对应的类别id
        };


        //* 多节点中单个节点的测试结果
        [StructLayout(LayoutKind.Sequential)]
        public struct ProjHanlde
        {
            public int node_num;                    //输出节点数量
            public IntPtr TreePredictHandle;
        };

        //* 初始化句柄
        //[DllImport("ht_ai_test.dll", EntryPoint = "InitTreeModel", CallingConvention = CallingConvention.Cdecl)]
        [DllImport("ht_ai_test.dll", EntryPoint = "InitTreeModel", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitTreeModel(ref IntPtr pHandle, string TreePath, byte[] byteArray_name, int iNodeNum, string strDeviceType, int iDeviceID);
        //static extern int InitTreeModel(ref IntPtr pHandle, string strProjectJson, string[] strNodeNames, int iNodeNum, string strDeviceType, int iDeviceID);

        [DllImport("ht_ai_test.dll", EntryPoint = "TreePredict", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TreePredict(IntPtr pHandle, ref ImageHt pFrame, [In, Out, MarshalAs(UnmanagedType.LPArray)] NodeResult[] pstNodeRst, int iNodeNum);

        [DllImport("ht_ai_test.dll", EntryPoint = "TreePredictH", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TreePredictH(IntPtr pHandle, ref ImageHtH pFrame, [In, Out, MarshalAs(UnmanagedType.LPArray)] NodeResult[] pstNodeRst, int iNodeNum);

        [DllImport("ht_ai_test.dll", EntryPoint = "DrawNodeResultCSharp", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DrawNodeResultCSharp(ref ImageHt pFrame, [In, Out, MarshalAs(UnmanagedType.LPArray)] NodeResult[] pstNodeRst, int node_pos, bool bShow);

        //保存图片倒本地，一般用于demo查看效果
        [DllImport("ht_ai_test.dll", EntryPoint = "SaveImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SaveImage(string strImageName, ref ImageHt pFrame);

        [DllImport("ht_ai_test.dll", EntryPoint = "ReadImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadImage(string strImageName, ref ImageHt pFrame);

        //* 释放句柄
        [DllImport("ht_ai_test.dll", EntryPoint = "ReleaseTree", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReleaseTree(IntPtr pHandle);

        //* 释放句柄
        [DllImport("ht_ai_test.dll", EntryPoint = "ReleasePredictResult", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReleasePredictResult([In, Out, MarshalAs(UnmanagedType.LPArray)] NodeResult[] pstNodeRst, int iNodeNum);

        [DllImport("ht_ai_test.dll", EntryPoint = "GetNodeInfo", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetNodeInfo(string TreePath, [In, Out, MarshalAs(UnmanagedType.LPArray)] ProjNodeInfo[] node_info, out int iNodeNum);

        [DllImport("ht_ai_test.dll", EntryPoint = "ClassNameToCharp", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClassNameToCharp([In, Out, MarshalAs(UnmanagedType.LPArray)] ClassNamesCharp[] Names, byte[] class_name_c);
        public static byte[] StructToBytes(object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structObj, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        //DetectResult内的class_name转成c#的string类型
        public static string ClassNameTostring(byte[] class_name)
        {
            ClassNamesCharp[] Names = new ClassNamesCharp[1];
            ClassNameToCharp(Names, class_name);
            int nullIndex = Array.IndexOf(Names[0].class_name, '\0');
            if (nullIndex == 0)
            {
                string names = "";
                return names;
            }
            else if (nullIndex != -1)
            {
                // 截断数组，去除 '\0' 字符后的所有字符
                char[] trimmedArray = new char[nullIndex];
                Array.Copy(Names[0].class_name, trimmedArray, nullIndex);

                // 转换为 string
                string names = new string(trimmedArray);
                return names;
            }
            else
            {
                // 如果没有找到 '\0' 字符，直接使用原始数组
                string names = new string(Names[0].class_name);
                return names;
            }
        }
    }
}