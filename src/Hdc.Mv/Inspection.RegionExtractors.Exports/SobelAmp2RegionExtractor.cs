using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SobelAmp2RegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject foundRegionObject;

            HDevelopExport.Singletone.GetRegionBySobelAmp2(image,
                out foundRegionObject,
                MedianRadius,
                MeanMaskWidth,
                MeanMaskHeight,
                SobelAmpSize,
                ScaleMult,
                ScaleAdd,
                AreaMin,
                AreaMax,
                OpeningWidth,
                OpeningHeight,
                ClosingWidth,
                ClosingHeight,
                ErosionWidth,
                ErosionHeight,
                DilationWidth,
                DilationHeight);

            return new HRegion(foundRegionObject);
        }

        public int MedianRadius { get; set; }
        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }
        public double ScaleMult { get; set; }
        public double ScaleAdd { get; set; }
        public int SobelAmpSize { get; set; }
        public double AreaMin { get; set; }
        public double AreaMax { get; set; }
        public int OpeningWidth { get; set; }
        public int OpeningHeight { get; set; }
        public int ClosingWidth { get; set; }
        public int ClosingHeight { get; set; }
        public int ErosionWidth { get; set; }
        public int ErosionHeight { get; set; }
        public int DilationWidth { get; set; }
        public int DilationHeight { get; set; }
    }
}