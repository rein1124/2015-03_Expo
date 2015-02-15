using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class LightOrDarkRect2RegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject regionHObject;
            HDevelopExport.Singletone.GetRegionOfLightOrDarkRect2(image, out regionHObject,
                LightDark.ToHalconString(), WidthMin, WidthMax, HeightMin, HeightMax, Rect2Len1, Rect2Len2);
            return new HRegion(regionHObject);
        }

        public LightDark LightDark { get; set; }
        public double WidthMin { get; set; }
        public double WidthMax { get; set; }
        public double HeightMin { get; set; }
        public double HeightMax { get; set; }
        public int Rect2Len1 { get; set; }
        public int Rect2Len2 { get; set; }
    }
}