using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class ExpandDomainGrayFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            HImage enhancedImage = image.ExpandDomainGray(ExpansionRange);

            return enhancedImage;
        }

        public int ExpansionRange { get; set; }
    }
}