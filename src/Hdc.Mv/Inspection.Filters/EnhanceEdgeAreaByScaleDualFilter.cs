using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class EnhanceEdgeAreaByScaleDualFilter : IImageFilter
    {
        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }
        public LightDark EdgeAreaLightDark { get; set; }
        public double SelectAreaMin { get; set; }
        public double SelectAreaMax { get; set; }
        public LightDark EdgeLightDark { get; set; }
        public double ScaleAdd1 { get; set; }
        public int EmpMaskWidth { get; set; }
        public int EmpMaskHeight { get; set; }
        public double EmpFactor { get; set; }
        public double ScaleAdd2 { get; set; }
        public int OpeningWidth { get; set; }
        public int OpeningHeight { get; set; }
        public int ClosingWidth { get; set; }
        public int ClosingHeight { get; set; }

        public HImage Process(HImage image)
        {
            HObject enhancedImage = null;
            HObject region = null;

            HDevelopExport.Singletone.EnhanceEdgeAreaByScaleDual(
                image,
                out enhancedImage,
                out region,
                MeanMaskWidth,
                MeanMaskHeight,
                EdgeAreaLightDark.ToHalconString(),
                SelectAreaMin,
                SelectAreaMax,
                EdgeLightDark.ToHalconString(),
                ScaleAdd1,
                EmpMaskWidth,
                EmpMaskHeight,
                EmpFactor,
                ScaleAdd2,
                OpeningWidth,
                OpeningHeight,
                ClosingWidth,
                ClosingHeight
                );

            region.Dispose();

            return new HImage(enhancedImage);
        }
    }
}