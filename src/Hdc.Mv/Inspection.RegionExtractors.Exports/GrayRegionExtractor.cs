using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class GrayRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject foundRegionObject;

            HDevelopExport.Singletone.GetRegionByGray(image,
                out foundRegionObject,
                MeanMaskWidth,
                MeanMaskHeight,
                EmpMaskWidth,
                EmpMaskHeight,
                EmpFactor,
                ScaleMult,
                ScaleAdd,
                ThresholdMinGray,
                ThresholdMaxGray,
                ErosionRadius,
                ClosingCircleRadius,
                ClosingRectWidth,
                ClosingRectHeight,
                DilationRadius,
                AreaMin,
                AreaMax);

            return new HRegion(foundRegionObject);
        }

        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }
        public int EmpMaskWidth { get; set; }
        public int EmpMaskHeight { get; set; }
        public double EmpFactor { get; set; }
        public double ScaleMult { get; set; }
        public double ScaleAdd { get; set; }
        public double ThresholdMinGray { get; set; }
        public double ThresholdMaxGray { get; set; }
        public double ErosionRadius { get; set; }
        public double ClosingCircleRadius { get; set; }
        public int ClosingRectWidth { get; set; }
        public int ClosingRectHeight { get; set; }
        public double DilationRadius { get; set; }
        public double AreaMin { get; set; }
        public double AreaMax { get; set; }
    }
}