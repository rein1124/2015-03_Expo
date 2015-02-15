using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class AntennaOfS1423RegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject regionHObject;
            HDevelopExport.Singletone.GetRegionOfAntennaOfS1423(image, out regionHObject,
                GrayOpeningMaskHeight, GrayOpeningMaskWidth, 
                GrayDilationMaskHeight, GrayDilationMaskWidth,
                LightDark.ToHalconString(),
                AreaMin, AreaMax, OpeningWidth, OpeningHeight, ClosingWidth, ClosingHeight);
            return new HRegion(regionHObject);
        }

        public int GrayOpeningMaskHeight { get; set; }
        public int GrayOpeningMaskWidth { get; set; }

        public int GrayDilationMaskHeight { get; set; }
        public int GrayDilationMaskWidth { get; set; }

        public LightDark LightDark { get; set; }

        public double AreaMin { get; set; }
        public double AreaMax { get; set; }
        public int OpeningWidth { get; set; }
        public int OpeningHeight { get; set; }
        public int ClosingWidth { get; set; }
        public int ClosingHeight { get; set; }
    }
}