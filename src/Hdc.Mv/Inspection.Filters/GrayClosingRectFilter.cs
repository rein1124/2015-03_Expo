using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class GrayClosingRectFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            var domainWidth = image.GetDomain().GetWidth();
            var domainHeight = image.GetDomain().GetHeight();

            var finalHeight = MaskHeight == 0 ? domainHeight : MaskHeight;
            var finalWidth = MaskWidth == 0 ? domainWidth : MaskWidth;

            return image.GrayClosingRect(finalHeight, finalWidth);
        }

        public int MaskHeight { get; set; }
        public int MaskWidth { get; set; }
    }
}