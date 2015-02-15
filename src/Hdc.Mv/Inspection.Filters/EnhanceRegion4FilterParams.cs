using System;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class EnhanceRegion4FilterParams
    {
        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }
        public int FirstMinGray { get; set; }
        public int FirstMaxGray { get; set; }
        public int EmpMaskWidth { get; set; }
        public int EmpMaskHeight { get; set; }
        public double EmpMaskFactor { get; set; }
        public int LastMinGray { get; set; }
        public int LastMaxGray { get; set; }
    }
}