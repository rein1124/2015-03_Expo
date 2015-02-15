/*using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public abstract class DefectExtractor : IDefectExtractor
    {
        public HRegion GetDefectRegion(HImage image, HRegion domain)
        {
            var blobs = GetBlobsInner(image, domain);
            if (Selector != null)
            {
                var selectedRegion = Selector.Process(blobs);
                return selectedRegion;
            }

            return blobs;
        }

        public abstract HRegion GetBlobsInner(HImage image, HRegion domain);

        public string Name { get; set; }

        public bool SaveCacheImageEnabled { get; set; }

        public IRegionProcessor Selector { get; set; }
    }
}*/