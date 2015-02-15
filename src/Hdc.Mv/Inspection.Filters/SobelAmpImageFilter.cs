using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SobelAmpImageFilter : IImageFilter
    {
        public HImage Process(HImage image)
        {
            return image.SobelAmp(FilterType.ToHalconString(), Size);
        }

        public int Size { get; set; }
        public SobelAmpFilterType FilterType { get; set; }
    }
}