using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class GrayErosionShapeFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            var domainWidth = image.GetDomain().GetWidth();
            var domainHeight = image.GetDomain().GetHeight();

            var finalHeight = Math.Abs(MaskHeight) < 0.000001 ? domainHeight : MaskHeight;
            var finalWidth = Math.Abs(MaskWidth) < 0.000001 ? domainWidth : MaskWidth;
            return image.GrayErosionShape(finalHeight, finalWidth, MaskShape.ToHalconString());
        }

        public double MaskHeight { get; set; }
        public double MaskWidth { get; set; }
        public MaskShape MaskShape { get; set; }
    }
}