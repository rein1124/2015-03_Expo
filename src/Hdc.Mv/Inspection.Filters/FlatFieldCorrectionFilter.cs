using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class FlatFieldCorrectionFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            var domain = image.GetDomain();
            var domainWidth = domain.GetWidth();
            var domainHeight = domain.GetHeight();

            var maskWidth = MaskWidth == 0 ? domainWidth : MaskWidth;
            var maskHeight = MaskHeight == 0 ? domainHeight : MaskHeight;

            var oriImage = image.MeanImage(maskWidth, maskHeight);
            var nosiedImage = image.MeanImage(1, maskHeight);
            var correctImage = oriImage.SubImage(nosiedImage, 1.0, 0.0);
            var correctedImage = image.AddImage(correctImage, 1.0, 0);

            oriImage.Dispose();
            nosiedImage.Dispose();
            correctImage.Dispose();
            domain.Dispose();

            return correctedImage;
        }

        public int MaskWidth { get; set; }
        public int MaskHeight { get; set; }
    }
}