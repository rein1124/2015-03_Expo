using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class BinaryThresholdDualRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            int usedThreshold;
            var region1 = image.BinaryThreshold("max_separability", LightDark1.ToHalconString(), out usedThreshold);
            var reducedImage = image.ReduceDomain(region1);

            int usedThreshold2;
            var region2 = reducedImage.BinaryThreshold("max_separability", LightDark2.ToHalconString(), out usedThreshold2);

            reducedImage.Dispose();

            return region2;
        }

        public LightDark LightDark1 { get; set; }
        public LightDark LightDark2 { get; set; }
    }
}