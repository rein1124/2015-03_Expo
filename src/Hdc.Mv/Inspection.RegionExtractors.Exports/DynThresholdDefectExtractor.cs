using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class DynThresholdDefectExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject foundRegionObject;

            HDevelopExport.Singletone.GetBlobsByDynThreshold(image,
                out foundRegionObject,
                MeanMaskWidth,
                MeanMaskHeight,
                DynOffset,
                DynLightDark.ToHalconString());

            var hRegion = new HRegion(foundRegionObject);
            return hRegion;
        }

        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }
        public double DynOffset { get; set; }
        public LightDark DynLightDark { get; set; }
    }
}