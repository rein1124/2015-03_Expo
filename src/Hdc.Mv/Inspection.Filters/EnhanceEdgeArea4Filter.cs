using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class EnhanceEdgeArea4Filter : IImageFilter
    {
        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }
        public int FirstMinGray { get; set; }
        public int FirstMaxGray { get; set; }
        public Order Order { get; set; }
        public int EmpMaskWidth { get; set; }
        public int EmpMaskHeight { get; set; }
        public double EmpMaskFactor { get; set; }
        public int LastMinGray { get; set; }
        public int LastMaxGray { get; set; }

        public HImage Process(HImage image)
        {
            var enhancedImage = image.EnhanceEdgeArea4(
                MeanMaskWidth,
                MeanMaskHeight,
                FirstMinGray,
                FirstMaxGray,
                Order,
                EmpMaskWidth,
                EmpMaskHeight,
                EmpMaskFactor,
                LastMinGray,
                LastMaxGray);
            return enhancedImage;
        }
    }
}