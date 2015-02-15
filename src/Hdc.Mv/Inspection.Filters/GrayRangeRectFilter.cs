using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class GrayRangeRectFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            return image.GrayRangeRect(MaskHeight, MaskWidth);
        }

        public int MaskHeight { get; set; }
        public int MaskWidth { get; set; }
    }
}