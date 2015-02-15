using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class MedianRectFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            HImage enhancedImage = image.MedianRect(
                MaskWidth,MaskHeight);

            return enhancedImage;
        }
        public int MaskWidth { get; set; }
        public int MaskHeight { get; set; }
    }
}