using System;
using System.Windows.Markup;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Obsolete]
    [Serializable]
    [ContentProperty("RegionExtractor")]
    public class ProcessedRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HRegion region;
            if (RegionExtractor != null)
            {
                region = RegionExtractor.Extract(image);
            }
            else
            {
                region = image.GetDomain();
            }

            HRegion processedRegion;
            if (RegionProcessor != null)
            {
                processedRegion = RegionProcessor.Process(region);
                region.Dispose();
            }
            else
            {
                processedRegion = region;
            }
            return processedRegion;
        }

        public IRegionExtractor RegionExtractor { get; set; }
        public IRegionProcessor RegionProcessor { get; set; }
    }
}