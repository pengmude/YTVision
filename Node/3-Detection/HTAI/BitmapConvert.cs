using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using static YTVisionPro.Node._3_Detection.HTAI.HTAPI;


namespace YTVisionPro.Node._3_Detection.HTAI
{
    internal class BitmapConvert
    {
        /// <summary>
        /// Bitmap图像转汇图图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static ImageHt BitmapToImageHt(Bitmap bitmap)
        {
            // 获取图片的尺寸信息
            int width = bitmap.Width;
            int height = bitmap.Height;
            int channels = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8; // 计算通道数

            // 创建ImageHt对象
            ImageHt imageHt = new ImageHt
            {
                width = width,
                height = height,
                channels = channels
            };

            // 锁定Bitmap的像素
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            imageHt.data = bitmapData.Scan0; // 获取图像数据的首地址
            imageHt.width_step = bitmapData.Stride; // 每行的步长

            // 解锁像素
            bitmap.UnlockBits(bitmapData);

            return imageHt;
        }

        /// <summary>
        /// 汇图图像转Bitmap图像
        /// </summary>
        /// <param name="imageHt"></param>
        /// <returns></returns>
        public static Bitmap ImageHtToBitmap(ImageHt imageHt)
        {
            Bitmap bitmap = null;
            BitmapData bitmapData = null;


            if (imageHt.channels == 1)
            {
                bitmap = new Bitmap(imageHt.width, imageHt.height, PixelFormat.Format8bppIndexed);
            }else if (imageHt.channels == 3)
            {
                bitmap = new Bitmap(imageHt.width, imageHt.height, PixelFormat.Format24bppRgb);
            }
            else
            {
                throw new ArgumentException("不支持的图像通道！");
            }

            // 锁定Bitmap的像素数据以获得一个指向图像像素缓冲区的指针
            bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                         ImageLockMode.WriteOnly,
                                         bitmap.PixelFormat);

            // 计算总的字节数
            int totalBytes = bitmapData.Stride * bitmapData.Height;
            // 创建一个字节数组来保存数据
            byte[] imageData = new byte[totalBytes];
            // 将 IntPtr 中的数据复制到字节数组中
            Marshal.Copy(imageHt.data, imageData, 0, totalBytes);

            // 将数据从字节数组复制到 Bitmap 的内存区域
            Marshal.Copy(imageData, 0, bitmapData.Scan0, totalBytes);

            //// 解锁 Bitmap 的像素数据
            bitmap.UnlockBits(bitmapData);

            // ch:Mono8格式，设置为标准调色板 | en:Set Standard Palette in Mono8 Format
            if (PixelFormat.Format8bppIndexed == bitmap.PixelFormat)
            {
                ColorPalette palette = bitmap.Palette;
                for (int i = 0; i < palette.Entries.Length; i++)
                {
                    palette.Entries[i] = Color.FromArgb(i, i, i);
                }
                bitmap.Palette = palette;
            }

            return bitmap;
        }
    }
}
