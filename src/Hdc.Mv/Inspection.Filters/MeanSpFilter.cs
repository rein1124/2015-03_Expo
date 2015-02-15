using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class MeanSpFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            var domain = image.GetDomain();
            var domainWidth = domain.GetWidth();
            var domainHeight = domain.GetHeight();

            var maskWidth = MaskWidth == 0 ? domainWidth : MaskWidth;
            var maskHeight = MaskHeight == 0 ? domainHeight : MaskHeight;

            HImage enhancedImage = image.MeanSp(maskHeight, maskWidth, MinThresh, MaxThresh);

            return enhancedImage;
        }

        public int MaskWidth { get; set; }
        public int MaskHeight { get; set; }
        public int MinThresh { get; set; }
        public int MaxThresh { get; set; }
    }
}