using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class DynThresholdWithMeanExcludedDefectExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject foundRegionObject;

            HDevelopExport.Singletone.GetBlobsByDynThresholdWithMeanExcluded(image,
                out foundRegionObject,
                MedianRadius,
                DynMeanMaskWidth,
                DynMeanMaskHeight,
                DynOffset,
                DynLightDark.ToHalconString()
                );

            var hRegion = new HRegion(foundRegionObject);
            return hRegion;
        }

        public int MedianRadius { get; set; }
        public double DynMeanMaskWidth { get; set; }
        public double DynMeanMaskHeight { get; set; }
        public double DynOffset { get; set; }
        public LightDark DynLightDark { get; set; }
    }
}