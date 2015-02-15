using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ODM.Domain.Inspection
{
    public static class ImageInfoExtensions
    {
        public static Task<BitmapSource> ToBitmapSourceAsync(this ImageInfo imageInfo)
        {
            return Task.Run(() => ToBitmapSource(imageInfo));
        }

        public static BitmapSource ToBitmapSource(this ImageInfo imageInfo)
        {
            // create image from unmanaged memory pointer
            int bitsPerPixel = imageInfo.BitsPerPixel; // 8 bit or 24 bit
            var bytesPerPixel = (bitsPerPixel + 7) / 8;
            int stride = 4 * ((imageInfo.Width * bytesPerPixel + 3) / 4);
            var size = imageInfo.Width * imageInfo.Height * bitsPerPixel / 8;

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
                imageInfo.Width, imageInfo.Height,
                96, 96,
                pixelFormat,
                BitmapPalettes.Gray256,
                imageInfo.Buffer,
                size,
                stride);
            
            // flip image Up-Down
            var transformedBmp = new TransformedBitmap();
            transformedBmp.BeginInit();
            transformedBmp.Source = bs;
            //transformedBmp.Transform = new ScaleTransform(1, 1);
            transformedBmp.EndInit();

            transformedBmp.Freeze();

            return bs;
        }
    }
}