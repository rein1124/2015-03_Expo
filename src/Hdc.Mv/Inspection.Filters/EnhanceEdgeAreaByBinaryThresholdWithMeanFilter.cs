using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class EnhanceEdgeAreaByBinaryThresholdWithMeanFilter : IImageFilter
    {
        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }
        public int EmpWidth { get; set; }
        public int EmpHeight { get; set; }
        public double EmpFactor { get; set; }
        public LightDark EdgeAreaLightDark { get; set; }
        public double AreaMin { get; set; }
        public double AreaMax { get; set; }
//        public int OpeningWidth { get; set; }
//        public int OpeningHeight { get; set; }
        public int ClosingWidth { get; set; }
        public int ClosingHeight { get; set; }
        public double DilationRadius { get; set; }

        public HImage Process(HImage image)
        {
            HObject enhancedImage = null;
            HObject region = null;

            HDevelopExport.Singletone.EnhanceEdgeAreaByBinaryThresholdWithMean(
                image,
                out enhancedImage,
                out region,
                MeanMaskWidth,
                MeanMaskHeight,
                EmpWidth,
                EmpHeight,
                EmpFactor,
                EdgeAreaLightDark.ToHalconString(),
                AreaMin,
                AreaMax,
                ClosingWidth,
                ClosingHeight,
                DilationRadius
                );

            region.Dispose();

            return new HImage(enhancedImage);
        }
    }
}