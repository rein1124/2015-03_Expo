using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class EnhanceEdgeAreaH1403Filter : IImageFilter
    {
        public LightDark AreaLightDark { get; set; }
        public double WidthMin { get; set; }
        public double WidthMax { get; set; }
        public double HeightMin { get; set; }
        public double HeightMax { get; set; }
        public SortMode SortMode { get; set; }
        public Order Order { get; set; }
        public RowOrCol RowOrCol { get; set; }
        public int SelectIndex { get; set; }
        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }

        public HImage Process(HImage image)
        {
            HObject enhancedImage = null;
            HObject region = null;

            HDevelopExport.Singletone.EnhanceEdgeAreaH1403(
                image,
                out enhancedImage,
                out region,
                AreaLightDark.ToHalconString(),
                WidthMin,
                WidthMax,
                HeightMin,
                HeightMax,
                SortMode.ToHalconString(),
                Order.ToHalconString(),
                RowOrCol.ToHalconString(),
                SelectIndex,
                MeanMaskWidth,
                MeanMaskHeight
                );

            region.Dispose();

            return new HImage(enhancedImage);
        }
    }
}