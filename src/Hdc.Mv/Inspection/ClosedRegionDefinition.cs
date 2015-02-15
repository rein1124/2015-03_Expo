using System;
using System.Windows;
using System.Windows.Markup;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    [ContentProperty("Rect")]
    public class ClosedRegionDefinition
    {
        public ClosedRegionDefinition()
        {
            Rect = new Rectangle2();
        }

        public string Name { get; set; }

        public Rectangle2 Rect { get; set; }

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
        public bool SaveCacheImageEnabled { get; set; }
    }
}