using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class MedianFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            HImage enhancedImage = image.MedianImage(
                MaskType.ToHalconString(),
                Radius,
                Margin.ToHalconString());

            return enhancedImage;
        }

        public MedianMaskType MaskType { get; set; }
        public int Radius { get; set; }
        public MedianMargin Margin { get; set; }
    }
}