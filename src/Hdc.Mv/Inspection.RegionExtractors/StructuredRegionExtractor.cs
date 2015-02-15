using System;
using System.Windows.Markup;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    [ContentProperty("RegionExtractor")]
    public class StructuredRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HImage targetFilterImage = ImageFilter != null ? ImageFilter.Process(image) : image;

            HRegion targetRegion = RegionExtractor != null ? RegionExtractor.Extract(targetFilterImage) : targetFilterImage.GetDomain();

            HRegion targetProcessedRegion = RegionProcessor != null ? RegionProcessor.Process(targetRegion) : targetRegion;

            if (ImageFilter!=null)
                targetFilterImage.Dispose();

            if (RegionProcessor != null)
                targetRegion.Dispose();

            return targetProcessedRegion;
        }

        public IImageFilter ImageFilter { get; set; }
        public IRegionExtractor RegionExtractor { get; set; }
        public IRegionProcessor RegionProcessor { get; set; }
    }
}