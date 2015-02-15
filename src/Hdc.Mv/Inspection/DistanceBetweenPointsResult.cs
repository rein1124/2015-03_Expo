using System;
using System.Windows;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class DistanceBetweenPointsResult
    {
        public DistanceBetweenLinesDefinition Definition { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public bool HasError { get; set; }
        public bool IsNotFound { get; set; }
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public double DistanceInPixel { get; set; }
        public double DistanceInWorld { get; set; }
        public double Angle { get; set; }
    }
}