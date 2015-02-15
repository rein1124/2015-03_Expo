using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class MinMaxGrayScaleFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            double min;
            double max;
            double range;
            var domain = image.GetDomain();
            image.MinMaxGray(domain, Percent, out min, out max, out range);

            var mult = 255/(max - min);
            var add = -mult*min;
            var scaledImage = image.ScaleImage(mult, add);

            return scaledImage;
        }

        public double Percent { get; set; }
    }
}