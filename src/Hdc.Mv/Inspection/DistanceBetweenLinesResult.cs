using System.Windows;

namespace Hdc.Mv.Inspection
{
    public class DistanceBetweenLinesResult
    {
        public string Name { get; set; }
        public Point FootPoint1 { get; set; }
        public Point FootPoint2 { get; set; }
        public double DistanceInPixel { get; set; }
        public double DistanceInWorld { get; set; }
        public double Angle { get; set; }
    }
}