/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ImageHelper.cs  
 * 文件功能描述：图片处理类
 * 
----------------------------------------------------------------*/
using System;
using System.Drawing;
using System.Drawing.Imaging;
using Afw.Core.Domain;

namespace Afw.Core.Helper
{
    public class ImageHelper
    {
        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns>成功或失败</returns>
        public static ImageInfo ReadBMP(Image image)
        {
            ImageInfo imageInfo = new ImageInfo();

            //将Image转换为Format24bppRgb格式的BMP
            Bitmap bm = new Bitmap(image);
            BitmapData data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                //位图中第一个像素数据的地址。它也可以看成是位图中的第一个扫描行
                IntPtr ptr = data.Scan0;

                //定义数组长度
                int soureBitArrayLength = data.Height * Math.Abs(data.Stride);
                byte[] sourceBitArray = new byte[soureBitArrayLength];

                //将bitmap中的内容拷贝到ptr_bgr数组中
                MemoryHelper.Copy(ptr, sourceBitArray, 0, soureBitArrayLength);

                //填充引用对象字段值
                imageInfo.width = data.Width;
                imageInfo.height = data.Height;
                imageInfo.format = ASF_ImagePixelFormat.ASVL_PAF_RGB24_B8G8R8;

                //获取去除对齐位后度图像数据
                int line = imageInfo.width * 3;
                int pitch = Math.Abs(data.Stride);
                int bgr_len = line * imageInfo.height;
                byte[] destBitArray = new byte[bgr_len];

                /*
                 * 图片像素数据在内存中是按行存储，一般图像库都会有一个内存对齐，在每行像素的末尾位置
                 * 每行的对齐位会使每行多出一个像素空间（三通道如RGB会多出3个字节，四通道RGBA会多出4个字节）
                 * 以下循环目的是去除每行末尾的对齐位，将有效的像素拷贝到新的数组
                 */
                for (int i = 0; i < imageInfo.height; ++i)
                {
                    Array.Copy(sourceBitArray, i * pitch, destBitArray, i * line, line);
                }

                imageInfo.imgData = MemoryHelper.Malloc(destBitArray.Length);
                MemoryHelper.Copy(destBitArray, 0, imageInfo.imgData, destBitArray.Length);

                return imageInfo;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.ImageHelper), e.ToString());

            }
            finally
            {
                bm.UnlockBits(data);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static ImageInfo ReadGray(Image image)
        {
            ImageInfo imageInfo = new ImageInfo();
            //将Image转换为
            Bitmap bm = RgbToGrayScale(image);
            BitmapData data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            try
            {


                //位图中第一个像素数据的地址。它也可以看成是位图中的第一个扫描行
                IntPtr ptr = data.Scan0;

                //定义数组长度
                int soureBitArrayLength = data.Height * Math.Abs(data.Stride);
                byte[] sourceBitArray = new byte[soureBitArrayLength];

                //将bitmap中的内容拷贝到ptr_bgr数组中
                MemoryHelper.Copy(ptr, sourceBitArray, 0, soureBitArrayLength);

                //填充引用对象字段值
                imageInfo.width = data.Width;
                imageInfo.height = data.Height;
                imageInfo.format = ASF_ImagePixelFormat.ASVL_PAF_GRAY;

                //获取去除对齐位后度图像数据
                int line = imageInfo.width;
                int pitch = Math.Abs(data.Stride);
                int gray_len = line * imageInfo.height;
                byte[] destBitArray = new byte[gray_len];

                /*
                 * 图片像素数据在内存中是按行存储，一般图像库都会有一个内存对齐，在每行像素的末尾位置
                 * 每行的对齐位会使每行多出一个像素空间（三通道如RGB会多出3个字节，四通道RGBA会多出4个字节）
                 * 以下循环目的是去除每行末尾的对齐位，将有效的像素拷贝到新的数组
                 */
                for (int i = 0; i < imageInfo.height; ++i)
                {
                    Array.Copy(sourceBitArray, i * pitch, destBitArray, i * line, line);
                }

                imageInfo.imgData = MemoryHelper.Malloc(destBitArray.Length);
                MemoryHelper.Copy(destBitArray, 0, imageInfo.imgData, destBitArray.Length);

                return imageInfo;


            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.ImageHelper), e.ToString());

            }
            finally
            {
                bm.UnlockBits(data);
            }

            return null;

        }

        /// <summary>
        /// 用矩形框标记图片指定区域
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="startX">矩形框左上角X坐标</param>
        /// <param name="startY">矩形框左上角Y坐标</param>
        /// <param name="width">矩形框宽度</param>
        /// <param name="height">矩形框高度</param>
        /// <returns>标记后的图片</returns>
        public static Image MarkRect(Image image, int startX, int startY, int width, int height)
        {
            Image clone = (Image)image.Clone();
            Graphics g = Graphics.FromImage(clone);

            try
            {
                Brush brush = new SolidBrush(Color.DeepPink);
                Pen pen = new Pen(brush, 2);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(pen, new Rectangle(startX, startY, width, height));
                return clone;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.ImageHelper), e.ToString());
            }
            finally
            {
                g.Dispose();
            }

            return null;
        }

        /// <summary>
        /// 用矩形框标记图片指定区域，添加年龄和性别标注
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="startX">矩形框左上角X坐标</param>
        /// <param name="startY">矩形框左上角Y坐标</param>
        /// <param name="width">矩形框宽度</param>
        /// <param name="height">矩形框高度</param>
        /// <param name="age">年龄</param>
        /// <param name="gender">性别</param>
        /// <returns>标记后的图片</returns>
        public static Image MarkRectAndString(Image image, int startX, int startY, int width, int height, int age, int gender, int showWidth)
        {
            Image clone = (Image)image.Clone();
            Graphics g = Graphics.FromImage(clone);
            try
            {
                Brush brush = new SolidBrush(Color.DeepPink);
                int penWidth = image.Width / showWidth;
                Pen pen = new Pen(brush, penWidth > 1 ? 2 * penWidth : 2);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                g.DrawRectangle(pen, new Rectangle(startX < 1 ? 0 : startX, startY < 1 ? 0 : startY, width, height));
                string genderStr = "";
                if (gender >= 0)
                {
                    if (gender == 0)
                    {
                        genderStr = "男";
                    }
                    else if (gender == 1)
                    {
                        genderStr = "女";
                    }
                }
                int fontSize = image.Width / showWidth;
                if (fontSize > 1)
                {
                    int temp = 12;
                    for (int i = 0; i < fontSize; i++)
                    {
                        temp += 6;
                    }
                    fontSize = temp;
                }
                else if (fontSize == 1)
                {
                    fontSize = 14;
                }
                else
                {
                    fontSize = 12;
                }
                g.DrawString($"年龄:约{age}岁   性别:{genderStr}", new Font(FontFamily.GenericSerif, fontSize), brush, startX < 1 ? 0 : startX, (startY - 20) < 1 ? 0 : startY - 20);

                return clone;
            }
            catch (Exception e)
            {
                SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.ImageHelper), e.ToString());
            }
            finally
            {
                g.Dispose();
            }

            return null;
        }

        /// <summary>
        /// 获取图片缩放比例
        /// </summary>
        /// <param name="oldWidth">原图片宽</param>
        /// <param name="oldHeigt">原图片高</param>
        /// <param name="newWidth">目标图片宽</param>
        /// <param name="newHeight">目标图片高</param>
        /// <returns></returns>
        public static float GetWidthAndHeight(int oldWidth, int oldHeigt, int newWidth, int newHeight)
        {
            //按比例缩放           
            float scaleRate = 0.0f;
            if (oldWidth >= newWidth && oldHeigt >= newHeight)
            {
                int widthDis = oldWidth - newWidth;
                int heightDis = oldHeigt - newHeight;
                if (widthDis > heightDis)
                {
                    scaleRate = newWidth * 1f / oldWidth;
                }
                else
                {
                    scaleRate = newHeight * 1f / oldHeigt;
                }
            }
            else if (oldWidth >= newWidth && oldHeigt < newHeight)
            {
                scaleRate = newWidth * 1f / oldWidth;
            }
            else if (oldWidth < newWidth && oldHeigt >= newHeight)
            {
                scaleRate = newHeight * 1f / oldHeigt;
            }
            else
            {
                int widthDis = newWidth - oldWidth;
                int heightDis = newHeight - oldHeigt;
                if (widthDis > heightDis)
                {
                    scaleRate = newHeight * 1f / oldHeigt;
                }
                else
                {
                    scaleRate = newWidth * 1f / oldWidth;
                }
            }
            return scaleRate;
        }

        /// <summary>
        /// 按指定宽高缩放图片
        /// </summary>
        /// <param name="image">原图片</param>
        /// <param name="dstWidth">目标图片宽</param>
        /// <param name="dstHeight">目标图片高</param>
        /// <returns></returns>
        public static Image ScaleImage(Image image, int dstWidth, int dstHeight)
        {
            Graphics g = null;
            try
            {
                //按比例缩放           
                float scaleRate = GetWidthAndHeight(image.Width, image.Height, dstWidth, dstHeight);
                int width = (int)(image.Width * scaleRate);
                int height = (int)(image.Height * scaleRate);

                //将宽度调整为4的整数倍
                if (width % 4 != 0)
                {
                    width = width - width % 4;
                }

                Bitmap destBitmap = new Bitmap(width, height);
                g = Graphics.FromImage(destBitmap);
                g.Clear(Color.Transparent);

                //设置画布的描绘质量         
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, new Rectangle((width - width) / 2, (height - height) / 2, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                //设置压缩质量     
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                EncoderParameter encoderParam = new EncoderParameter(Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;

                return destBitmap;
            }
            catch (Exception e)
            {
                SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.ImageHelper), e.ToString());
            }
            finally
            {
                if (g != null)
                {
                    g.Dispose();
                }
            }

            return null;
        }

        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="src">原图片</param>
        /// <param name="left">左坐标</param>
        /// <param name="top">顶部坐标</param>
        /// <param name="right">右坐标</param>
        /// <param name="bottom">底部坐标</param>
        /// <returns>剪裁后的图片</returns>
        public static Image CutImage(Image src, int left, int top, int right, int bottom)
        {
            try
            {
                Bitmap srcBitmap = new Bitmap(src);
                Bitmap dstBitmap = srcBitmap.Clone(new Rectangle(left, top, right - left, bottom - top), PixelFormat.DontCare);
                return dstBitmap;
            }
            catch (Exception e)
            {
                SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.ImageHelper), e.ToString());
            }
            return null;
        }

        /// <summary>  
        /// 将源图像灰度化，并转化为8位灰度图像。  
        /// </summary>  
        /// <param name="original"> 源图像。 </param>  
        /// <returns> 8位灰度图像。 </returns>  
        public static Bitmap RgbToGrayScale(Image original)
        {
            if (original != null)
            {
                Bitmap bm = new Bitmap(original);

                // 将源图像内存区域锁定  
                Rectangle rect = new Rectangle(0, 0, original.Width, original.Height);
                BitmapData bmpData = bm.LockBits(rect, ImageLockMode.ReadOnly, original.PixelFormat);

                // 获取图像参数  
                int width = bmpData.Width;
                int height = bmpData.Height;
                int stride = bmpData.Stride;  // 扫描线的宽度  
                int offset = stride - width * 3;  // 显示宽度与扫描线宽度的间隙  
                IntPtr ptr = bmpData.Scan0;   // 获取bmpData的内存起始位置  
                int scanBytes = stride * height;  // 用stride宽度，表示这是内存区域的大小  

                // 分别设置两个位置指针，指向源数组和目标数组  
                int posScan = 0, posDst = 0;
                byte[] rgbValues = new byte[scanBytes];  // 为目标数组分配内存  
                MemoryHelper.Copy(ptr, rgbValues, 0, scanBytes);  // 将图像数据拷贝到rgbValues中  
                                                                  // 分配灰度数组  
                byte[] grayValues = new byte[width * height]; // 不含未用空间。  
                                                              // 计算灰度数组  
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        double temp = rgbValues[posScan++] * 0.11 +
                            rgbValues[posScan++] * 0.59 +
                            rgbValues[posScan++] * 0.3;
                        grayValues[posDst++] = (byte)temp;
                    }
                    // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel  
                    posScan += offset;
                }

                // 内存解锁  
                MemoryHelper.Copy(rgbValues, 0, ptr, scanBytes);
                bm.UnlockBits(bmpData);  // 解锁内存区域  

                // 构建8位灰度位图  
                Bitmap retBitmap = BuiltGrayBitmap(grayValues, width, height);
                return retBitmap;
            }
            else
            {
                return null;
            }
        }

        /// <summary>  
        /// 用灰度数组新建一个8位灰度图像。  
        /// http://www.cnblogs.com/spadeq/archive/2009/03/17/1414428.html  
        /// </summary>  
        /// <param name="rawValues"> 灰度数组(length = width * height)。 </param>  
        /// <param name="width"> 图像宽度。 </param>  
        /// <param name="height"> 图像高度。 </param>  
        /// <returns> 新建的8位灰度位图。 </returns>  
        private static Bitmap BuiltGrayBitmap(byte[] rawValues, int width, int height)
        {
            // 新建一个8位灰度位图，并锁定内存区域操作  
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                 ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // 计算图像参数  
            int offset = bmpData.Stride - bmpData.Width;        // 计算每行未用空间字节数  
            IntPtr ptr = bmpData.Scan0;                         // 获取首地址  
            int scanBytes = bmpData.Stride * bmpData.Height;    // 图像字节数 = 扫描字节数 * 高度  
            byte[] grayValues = new byte[scanBytes];            // 为图像数据分配内存  

            // 为图像数据赋值  
            int posSrc = 0, posScan = 0;                        // rawValues和grayValues的索引  
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grayValues[posScan++] = rawValues[posSrc++];
                }
                // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel  
                posScan += offset;
            }

            // 内存解锁  
            MemoryHelper.Copy(grayValues, 0, ptr, scanBytes);
            bitmap.UnlockBits(bmpData);  // 解锁内存区域  

            // 修改生成位图的索引表，从伪彩修改为灰度  
            ColorPalette palette;
            // 获取一个Format8bppIndexed格式图像的Palette对象  
            using (Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                palette = bmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            // 修改生成位图的索引表  
            bitmap.Palette = palette;

            return bitmap;
        }
    }
}
