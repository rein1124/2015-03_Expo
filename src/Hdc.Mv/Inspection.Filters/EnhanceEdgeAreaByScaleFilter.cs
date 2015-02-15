using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class EnhanceEdgeAreaByScaleFilter : IImageFilter
    {
        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }
        public LightDark EdgeAreaLightDark { get; set; }
        public double ScaleAdd { get; set; }
        public int OpeningWidth { get; set; }
        public int OpeningHeight { get; set; }
        public int ClosingWidth { get; set; }
        public int ClosingHeight { get; set; }

        public HImage Process(HImage image)
        {
            HObject enhancedImage = null;
            HObject region = null;

            HDevelopExport.Singletone.EnhanceEdgeAreaByScale(
                image,
                out enhancedImage,
                out region,
                MeanMaskWidth,
                MeanMaskHeight,
                EdgeAreaLightDark.ToHalconString(),
                ScaleAdd,
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
