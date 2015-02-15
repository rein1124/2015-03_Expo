using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class BinaryThresholdRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            int usedThreshold;
            var region = image.BinaryThreshold("max_separability", LightDark.ToHalconString(), out usedThreshold);
            return region;
        }

        public LightDark LightDark { get; set; }
        public string Name { get; set; }
    }
}