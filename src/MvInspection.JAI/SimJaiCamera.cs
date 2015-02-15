using System;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Hdc.Windows.Media.Imaging;

namespace MvInspection.ImageAcquisition
{
    public class SimJaiCamera : ICamera
    {
        private int indexCounter = 0;
        string fileName01 = @"sample\sample8bpp-01.tif";
        string fileName02 = @"sample\sample8bpp-02.tif";

        public void Dispose()
        {
        }

        public bool Init()
        {
            return true;
        }

        public ImageInfo Acquisition()
        {
            string fileName = indexCounter%2 == 0 ? fileName01 : fileName02;

            var bs = fileName.GetBitmapSource();

            var bsi = GetBitmapSourceInfoWithRgb32(bs);

            IntPtr unmanagedPointer = Marshal.AllocHGlobal(bsi.Buffer.Length);
            Marshal.Copy(bsi.Buffer, 0, unmanagedPointer, bsi.Buffer.Length);

            var imageInfo = new ImageInfo()
                            {
                                BitsPerPixel = bs.Format.BitsPerPixel,
                                Buffer = unmanagedPointer,
                                Height = bsi.PixelHeight,
                                Width = bsi.PixelWidth,
                                Index = indexCounter,
                            };

            indexCounter++;
            return imageInfo;
        }

        /// <summary>
        /// only for Gray Bitmap
        /// </summary>
        public class BitmapSourceInfo
        {
            public double DpiX { get; set; }
            public double DpiY { get; set; }
            public int Stride { get; set; }
            public int PixelWidth { get; set; }
            public int PixelHeight { get; set; }
            public byte[] Buffer { get; set; }
            public PixelFormat PixelFormat { get; set; }
        }

        public static BitmapSourceInfo GetBitmapSourceInfoWithRgb32(BitmapSource bitmapSource)
        {
            var info = new BitmapSourceInfo()
                       {
                           DpiX = bitmapSource.DpiX,
                           DpiY = bitmapSource.DpiY,
                           Stride = bitmapSource.PixelWidth*1,
                           PixelWidth = bitmapSource.PixelWidth,
                           PixelHeight = bitmapSource.PixelHeight,
                           PixelFormat = bitmapSource.Format,
                       };

            var buffer = new byte[info.Stride*info.PixelHeight];
            bitmapSource.CopyPixels(buffer, info.Stride, 0);
            info.Buffer = buffer;
            return info;
        }
    }
}