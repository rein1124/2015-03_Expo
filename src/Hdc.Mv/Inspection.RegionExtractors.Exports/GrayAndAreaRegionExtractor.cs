using System;
using System.Windows.Markup;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class GrayAndAreaRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            var foundRegion = image.GetRegionByGrayAndArea(
                MedianRadius,
                EmpWidth,
                EmpHeight,
                EmpFactor,
                ThresholdMinGray,
                ThresholdMaxGray,
                AreaMin,
                AreaMax,
                ClosingRadius,
                DilationRadius);
            return foundRegion;
        }

        public int MedianRadius { get; set; }
        public int EmpWidth { get; set; }
        public int EmpHeight { get; set; }
        public double EmpFactor { get; set; }
        public int ThresholdMinGray { get; set; }
        public int ThresholdMaxGray { get; set; }
        public int AreaMin { get; set; }
        public int AreaMax { get; set; }
        public double ClosingRadius { get; set; }
        public double DilationRadius { get; set; }
    }
}