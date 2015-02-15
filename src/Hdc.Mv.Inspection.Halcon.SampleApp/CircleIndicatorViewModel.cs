using System.Windows.Media;

namespace Hdc.Mv.Inspection.Halcon.SampleApp
{
    public class CircleIndicatorViewModel
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double Radius { get; set; }
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; } 
        public DoubleCollection StrokeDashArray { get; set; }
    }
}