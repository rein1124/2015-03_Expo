using Jai_FactoryDotNET;
using MvInspection;

namespace MvInspection
{
    public static class Ex
    {
        public static ImageInfo ToImageInfo(this Jai_FactoryWrapper.ImageInfo x)
        {
            int bitsPerPixel = -1;
            switch (x.PixelFormat)
            {
                case Jai_FactoryWrapper.EPixelFormatType.GVSP_PIX_MONO8:
                    bitsPerPixel = 8;
                    break;
                case Jai_FactoryWrapper.EPixelFormatType.GVSP_PIX_BGR8_PACKED:
                    bitsPerPixel = 24;
                    break;
            }

            var imageInfo = new ImageInfo()
                            {
                                Buffer = x.ImageBuffer,
                                Width = (int) x.SizeX,
                                Height = (int) x.SizeY,
                                BitsPerPixel = bitsPerPixel,
                            };
            return imageInfo;
        }
    }
}