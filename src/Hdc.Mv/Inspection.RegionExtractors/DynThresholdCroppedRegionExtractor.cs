using System;
using System.Windows.Markup;
using HalconDotNet;
using Hdc.Diagnostics;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    [ContentProperty("ThresholdImageFilter")]
    public class DynThresholdCroppedRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            var domain = image.GetDomain();
            var offsetRow1 = domain.GetRow1();
            var offsetColumn1 = domain.GetColumn1();
            var croppedImage = image.CropDomain();

            var swThresholdImageFilter = new NotifyStopwatch("DynThresholdCroppedRegionExtractor.ThresholdImageFilter");
            HImage thresholdImage = ThresholdImageFilter.Process(croppedImage);
            swThresholdImageFilter.Dispose();

            var swDynThreshold = new NotifyStopwatch("DynThresholdCroppedRegionExtractor.DynThreshold");
            HRegion region = croppedImage.DynThreshold(
                thresholdImage,
                Offset,
                LightDark.ToHalconString());
            swDynThreshold.Dispose();

            var movedRegion = region.MoveRegion(offsetRow1, offsetColumn1);

            croppedImage.Dispose();
            thresholdImage.Dispose();
            region.Dispose();

            return movedRegion;
        }

        public string Name { get; set; }

        public IImageFilter ThresholdImageFilter { get; set; }
        public double Offset { get; set; }
        public LightDark LightDark { get; set; }
    }
}