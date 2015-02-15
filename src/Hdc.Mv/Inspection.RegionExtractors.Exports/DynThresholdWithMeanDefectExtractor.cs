using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class DynThresholdWithMeanDefectExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject foundRegionObject;

            HDevelopExport.Singletone.GetBlobsByDynThresholdWithMean(image,
                out foundRegionObject,
                ZoomFactor,
                MedianRadius,
                DynMeanMaskWidth,
                DynMeanMaskHeight,
                DynOffset,
                DynLightDark.ToHalconString(),
                ClosingRadius,
                AreaMin,
                AreaMax,
                Rect2Length1Min,
                Rect2Length1Max
                );

            var hRegion = new HRegion(foundRegionObject);
            return hRegion;
        }

        public double ZoomFactor { get; set; }
        public int MedianRadius { get; set; }
        public double DynMeanMaskWidth { get; set; }
        public double DynMeanMaskHeight { get; set; }
        public double DynOffset { get; set; }
        public LightDark DynLightDark { get; set; }
        public double ClosingRadius { get; set; }
        public double AreaMin { get; set; }
        public double AreaMax { get; set; }
        public double Rect2Length1Min { get; set; }
        public double Rect2Length1Max { get; set; }
    }
}