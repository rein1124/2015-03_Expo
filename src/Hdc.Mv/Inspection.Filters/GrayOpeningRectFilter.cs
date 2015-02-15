using System;
using System.Threading;
using HalconDotNet;
using Hdc.Diagnostics;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class GrayOpeningRectFilter: IImageFilter
    {
        public HImage Process(HImage image)
        {
            var domainWidth = image.GetDomain().GetWidth();
            var domainHeight = image.GetDomain().GetHeight();

            var finalHeight = MaskHeight == 0 ? domainHeight : MaskHeight;
            var finalWidth = MaskWidth == 0 ? domainWidth : MaskWidth;

//            var swGrayOpeningRect = new NotifyStopwatch("GrayOpeningRectFilter.GrayOpeningRect");
            var hImage = image.GrayOpeningRect(finalHeight, finalWidth);
//            swGrayOpeningRect.Dispose();
            return hImage;
        }

        public int MaskHeight { get; set; }
        public int MaskWidth { get; set; }
    }
}