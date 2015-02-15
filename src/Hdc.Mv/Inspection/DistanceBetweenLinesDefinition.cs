using System;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class DistanceBetweenLinesDefinition
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string GroupName { get; set; }
        public string DisplayGroupName { get; set; }
        public string Comment { get; set; }
        public string DisplayComment { get; set; }

        public string Line1Name { get; set; }
        public string Line2Name { get; set; }
        public double ExpectValue { get; set; } // in mm
        public double PositiveTolerance { get; set; } // in mm
        public double NegativeTolerance { get; set; } // in mm
    }
}