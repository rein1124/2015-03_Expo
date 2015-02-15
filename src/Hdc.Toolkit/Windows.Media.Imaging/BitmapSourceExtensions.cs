using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Hdc.Windows.Media.Imaging
{
    public static class BitmapSourceExtensions
    {
        public static BitmapSource GetBitmapSourceFromBytes(this byte[] bytes)
        {
            if (bytes == null)
                return null;

            if (bytes.Length == 0)
                return null;

            var binaryStream = new MemoryStream(bytes);
            return BitmapFrame.Create(binaryStream);
        }

        public static byte[] GetBytesFromBitmapSource(this BitmapSource bitmapSource)
        {
            if (bitmapSource == null)
                return null;

            BitmapEncoder encoder = new JpegBitmapEncoder();

            using (var stream = new MemoryStream())
            {
                byte[] bit = new byte[0];
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);
                bit = stream.ToArray();
                stream.Close();
                return bit;
            }
        }

        public static BitmapSource GetBitmapSource(this string fileName)
        {
            if (fileName.IsNullOrEmpty())
                return null;

            var bi = new BitmapImage(new Uri(fileName,UriKind.RelativeOrAbsolute));

            return bi;
        }

        public static byte[] GetBytesFromImage(this string fileName)
        {
            if (fileName.IsNullOrEmpty())
                return null;

            var image = GetBitmapSource(fileName);
            return GetBytesFromBitmapSource(image);
        }

        public static BitmapSource CropTop(this BitmapSource source)
        {
            return new CroppedBitmap(source, new Int32Rect(0, 0, source.PixelWidth, source.PixelHeight / 2));
        }

        public static BitmapSource CropBottom(this BitmapSource source)
        {
            return new CroppedBitmap(source,
                                     new Int32Rect(0, source.PixelHeight / 2, source.PixelWidth, source.PixelHeight / 2));
        }

        public static void SaveToPng(this BitmapSource image, string fileName)
        {
            var ec = new PngBitmapEncoder();
            ec.Frames.Add(BitmapFrame.Create(image));
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                ec.Save(fileStream);
            }
        }

        public static void SaveToJpeg(this BitmapSource image, string fileName)
        {
            var ec = new JpegBitmapEncoder();
            ec.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                ec.Save(fileStream);
            }
        }

        public static void SaveToBmp(this BitmapSource image, string fileName)
        {
            var ec = new BmpBitmapEncoder();
            ec.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                ec.Save(fileStream);
            }
        }

        public static Task SaveToBmpAsync(this BitmapSource image, string fileName)
        {
            return Task.Run(() => SaveToBmp(image, fileName));
        }

        public static void SaveToTiff(this BitmapSource image, string fileName, TiffCompressOption tiffCompressOption = TiffCompressOption.Default)
        {
            var ec = new TiffBitmapEncoder { Compression = tiffCompressOption };
            ec.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                ec.Save(fileStream);
            }
        }

        public static Task SaveToTiffAsync(this BitmapSource image, string fileName, TiffCompressOption tiffCompressOption = TiffCompressOption.Default)
        {
            return Task.Run(() => SaveToTiff(image, fileName, tiffCompressOption));
        }

        public static BitmapSourceInfo ToGray8BppBitmapSourceInfo(this BitmapSource bitmapSource)
        {
            var info = new BitmapSourceInfo()
            {
                DpiX = bitmapSource.DpiX,
                DpiY = bitmapSource.DpiY,
                Stride = bitmapSource.PixelWidth,
                PixelWidth = bitmapSource.PixelWidth,
                PixelHeight = bitmapSource.PixelHeight,
                PixelFormat = bitmapSource.Format,
                BitmapPalette = bitmapSource.Palette,
            };

            var buffer = new byte[info.PixelWidth * info.PixelHeight];
            bitmapSource.CopyPixels(buffer, info.Stride, 0);
            info.Buffer = buffer;
            return info;
        }

//        public static BitmapSourceInfo GetBitmapSourceInfoWithRgb32(BitmapSource bitmapSource)
//        {
//            var info = new BitmapSourceInfo()
//            {
//                DpiX = bitmapSource.DpiX,
//                DpiY = bitmapSource.DpiY,
//                Stride = bitmapSource.PixelWidth * 3,
//                PixelWidth = bitmapSource.PixelWidth,
//                PixelHeight = bitmapSource.PixelHeight,
//                PixelFormat = bitmapSource.Format,
//            };
//
//            var buffer = new byte[info.Stride * info.PixelHeight];
//            bitmapSource.CopyPixels(buffer, info.Stride, 0);
//            info.Buffer = buffer;
//            return info;
//        }

        public static CroppedBitmap Crop(this BitmapSource source, int x, int y, int width, int height)
        {
            return new CroppedBitmap(source, new Int32Rect(x, y, width, height));
        }
        
        public static CroppedBitmap Crop(this BitmapSource source, Int32Rect rect)
        {
            return new CroppedBitmap(source, rect);
        }
    }
}