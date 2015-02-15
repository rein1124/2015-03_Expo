using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class GetDomainRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            var region = image.GetDomain();

            return region;
        }
    }
}