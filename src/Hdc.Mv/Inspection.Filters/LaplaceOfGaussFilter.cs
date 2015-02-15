using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class LaplaceOfGaussFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            HImage enhancedImage = image.LaplaceOfGauss(Sigma);

            return enhancedImage;
        }

        public double Sigma { get; set; }
    }
}