using System.Windows.Media.Imaging;

namespace Hdc.Windows.Media.Imaging
{
    public static class BitmapSourceInfoExtensions
    {
        public static BitmapSource GetBitmapSource(this BitmapSourceInfo info)
        {
            var bs = BitmapSource.Create(
                info.PixelWidth,
                info.PixelHeight,
                info.DpiX,
                info.DpiY,
                info.PixelFormat,
                info.BitmapPalette,
                info.Buffer,
                info.Stride);
            return bs;
        }
    }
}