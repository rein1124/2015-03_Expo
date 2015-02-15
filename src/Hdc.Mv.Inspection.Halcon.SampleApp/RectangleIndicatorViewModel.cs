using System.Windows.Media;

namespace MeasurementTestApp
{
    public class RectangleIndicatorViewModel
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsHidden { get; set; }
        public Brush Stroke { get; set; }
        public DoubleCollection StrokeDashArray { get; set; }
        public double StrokeThickness { get; set; }
    }
}