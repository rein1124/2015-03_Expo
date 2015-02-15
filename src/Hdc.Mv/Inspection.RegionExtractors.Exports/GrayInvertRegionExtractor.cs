using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class GrayInvertRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject foundRegionObject;

            HDevelopExport.Singletone.GetRegionByGrayInvert(image,
                out foundRegionObject,
                MeanMaskWidth, // Halcon 12, mean_sp has a bug that height and width are invert
                MeanMaskHeight, // 
                EmpMaskWidth,
                EmpMaskHeight,
                EmpFactor1,
                EmpFactor2,
                ThresholdMinGray,
                ThresholdMaxGray,
                AreaMin,
                AreaMax);

            var hRegion = new HRegion(foundRegionObject);
            return hRegion;
        }

        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }
        public int EmpMaskWidth { get; set; }
        public int EmpMaskHeight { get; set; }
        public double EmpFactor1 { get; set; }
        public double EmpFactor2 { get; set; }
        public double ThresholdMinGray { get; set; }
        public double ThresholdMaxGray { get; set; }
        public double AreaMin { get; set; }
        public double AreaMax { get; set; }
    }
}