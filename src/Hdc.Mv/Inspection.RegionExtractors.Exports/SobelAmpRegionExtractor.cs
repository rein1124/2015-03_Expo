using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SobelAmpRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject foundRegionObject;

            HDevelopExport.Singletone.GetRegionBySobelAmp(image,
                out foundRegionObject,
                MeanMaskWidth,
                MeanMaskHeight,
                EmpMaskWidth,
                EmpMaskHeight,
                EmpFactor,
                SobelAmpSize,
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
        public int SobelAmpSize { get; set; }
        public double ScaleMult { get; set; }
        public double ScaleAdd { get; set; }
        public int ThresholdMinGray { get; set; }
        public int ThresholdMaxGray { get; set; }
        public double ErosionRadius { get; set; }
        public double ClosingCircleRadius { get; set; }
        public double ClosingRectWidth { get; set; }
        public double ClosingRectHeight { get; set; }
        public double DilationRadius { get; set; }
        public double AreaMin { get; set; }
        public double AreaMax { get; set; }
    }
}