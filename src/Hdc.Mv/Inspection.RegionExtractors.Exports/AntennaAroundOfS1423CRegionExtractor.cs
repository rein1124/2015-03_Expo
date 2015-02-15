using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class AntennaAroundOfS1423CRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject regionHObject;
            HDevelopExport.Singletone.GetRegionOfAntennaAroundOfS1423C(image, out regionHObject,
                LightDark.ToHalconString(), WidthMin, WidthMax, HeightMin, HeightMax,
                SortMode.ToHalconString(), Order.ToHalconString(), RowOrCol.ToHalconString(), SelectIndex,
                DilationWidth, DilationHeight);
            return new HRegion(regionHObject);
        }

        public LightDark LightDark { get; set; }
        public double WidthMin { get; set; }
        public double WidthMax { get; set; }
        public double HeightMin { get; set; }
        public double HeightMax { get; set; }
        public SortMode SortMode { get; set; }
        public Order Order { get; set; }
        public RowOrCol RowOrCol { get; set; }
        public int SelectIndex { get; set; }
        public int DilationWidth { get; set; }
        public int DilationHeight { get; set; }
    }
}