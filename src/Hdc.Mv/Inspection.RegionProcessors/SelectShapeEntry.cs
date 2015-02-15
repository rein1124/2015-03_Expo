using System;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SelectShapeEntry
    {
        public SelectShapeEntry()
        {
        }

        public SelectShapeEntry(SharpFeature feature, double min, double max)
        {
            Feature = feature;
            Min = min;
            Max = max;
        }

        public SharpFeature Feature { get; set; }

        public double Min { get; set; }
        public double Max { get; set; }
    }
}