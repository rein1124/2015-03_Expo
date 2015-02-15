using System;
using System.Windows.Markup;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    [ContentProperty("ThresholdImageFilter")]
    public class DynThresholdToBinFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
//            image.WriteImage("tiff", 0, @"B:\Test_Ori");

            var preprocessImage = PreprocessFilter.Process(image);
//            preprocessImage.WriteImage("tiff", 0, @"B:\Test_preprocessImage");

            var thresholdImage = ThresholdImageFilter.Process(preprocessImage);
//            thresholdImage.WriteImage("tiff", 0, @"B:\Test_thresholdImage");

            HRegion region = preprocessImage.DynThreshold(
                thresholdImage,
                Offset,
                LightDark.ToHalconString());
            var complement = region.Complement();

            thresholdImage.OverpaintRegion(region, (double) ForegroundGray, "fill");
            thresholdImage.OverpaintRegion(complement, (double)BackgroundGray, "fill");
//            thresholdImage.WriteImage("tiff", 0, @"B:\Test_OverpaintRegion");

            region.Dispose();
            complement.Dispose();
            preprocessImage.Dispose();

            return thresholdImage;
        }

        public string Name { get; set; }

        public IImageFilter PreprocessFilter { get; set; }
        public IImageFilter ThresholdImageFilter { get; set; }
        public double Offset { get; set; }
        public LightDark LightDark { get; set; }
        public int ForegroundGray { get; set; }
        public int BackgroundGray { get; set; }
    }
}