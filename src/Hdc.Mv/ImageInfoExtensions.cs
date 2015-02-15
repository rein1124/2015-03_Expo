using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hdc.Mv
{
    public static class ImageInfoExtensions
    {
        public static ImageInfo ToImageInfoWith8Bpp(this string fileName)
        {
            var bi = new BitmapImage(new Uri(fileName, UriKind.RelativeOrAbsolute));
            var bis = bi.ToImageInfoWith8Bpp();
            return bis;
        }

        public static void FreeMemory(this ImageInfo imageInfo)
        {
            Marshal.FreeHGlobal(imageInfo.BufferPtr);
        }

        public static Task<BitmapSource> ToBitmapSourceAsync(this ImageInfo imageInfo)
        {
            return Task.Run(() => ToBitmapSource(imageInfo));
        }

        public static BitmapSource ToBitmapSource(this ImageInfo imageInfo)
        {
            // create image from unmanaged memory pointer
            int bitsPerPixel = imageInfo.BitsPerPixel; // 8 bit or 24 bit
            var bytesPerPixel = (bitsPerPixel + 7) / 8;
            int stride = 0;
            switch (imageInfo.BitsPerPixel)
            {
                case 8:
                    stride = imageInfo.PixelWidth;
                    break;
                case 24:
                    stride = 4 * ((imageInfo.PixelWidth * bytesPerPixel + 3) / 4);
                    break;
            }
            //            var size = imageInfo.PixelWidth * imageInfo.PixelHeight * bitsPerPixel / 8;
            var size = stride * imageInfo.PixelHeight;

            var pixelFormat = new PixelFormat();
            switch (imageInfo.BitsPerPixel)
            {
                case 8:
                    pixelFormat = PixelFormats.Gray8;
                    break;
                case 24:
                    pixelFormat = PixelFormats.Bgr24;
                    break;
            }

            var bs = BitmapSource.Create(
                imageInfo.PixelWidth, imageInfo.PixelHeight,
                96, 96,
                pixelFormat,
                BitmapPalettes.Gray256,
                imageInfo.BufferPtr,
                size,
                stride);

/*            // flip image Up-Down
            var transformedBmp = new TransformedBitmap();
            transformedBmp.BeginInit();
            transformedBmp.Source = bs;
            //transformedBmp.Transform = new ScaleTransform(1, 1);
            transformedBmp.EndInit();

            transformedBmp.Freeze();*/

            return bs;
        }
    }
}