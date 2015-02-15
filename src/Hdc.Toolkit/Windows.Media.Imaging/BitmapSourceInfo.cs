using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hdc.Windows.Media.Imaging
{
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
        public BitmapPalette BitmapPalette { get; set; }
    }
}