using System.Windows.Media;

namespace MeasurementTestApp
{
    public class RegionIndicatorViewModel
    {
        public double StartPointX { get; set; }
        public double StartPointY { get; set; }
        public double EndPointX { get; set; }
        public double EndPointY { get; set; }
        public double RegionWidth { get; set; }
        public Brush Stroke { get; set; }
        public DoubleCollection StrokeDashArray { get; set; }
        public double StrokeThickness { get; set; }
        public bool IsHidden { get; set; }
    }
}