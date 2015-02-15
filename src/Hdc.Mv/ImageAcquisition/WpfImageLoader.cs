using System;
using System.Windows.Media.Imaging;
using Hdc.Mv.Inspection;

namespace Hdc.Mv.ImageAcquisition
{
    public class WpfImageLoader : IImageLoader
    {
        public ImageInfo LoadFromFile(string fileName)
        {
            var bi = new BitmapImage(new Uri(fileName, UriKind.RelativeOrAbsolute));
            ImageInfo ii = bi.ToImageInfoWith8Bpp();
            return ii;
        }
    }
}