using Hdc.Mv.Inspection;
using Jai_FactoryDotNET;

namespace Hdc.Mv.ImageAcquisition
{
    internal static class Ex
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
                                BufferPtr = x.ImageBuffer,
                                PixelWidth = (int) x.SizeX,
                                PixelHeight = (int) x.SizeY,
                                BitsPerPixel = bitsPerPixel,
                            };
            return imageInfo;
        }
    }
}