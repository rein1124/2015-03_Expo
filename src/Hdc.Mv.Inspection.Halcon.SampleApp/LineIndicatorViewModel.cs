using System.Windows.Media;
using Hdc.Mv;

namespace MeasurementTestApp
{
    public class LineIndicatorViewModel
    {
        public double StartPointX { get; set; }
        public double StartPointY { get; set; }
        public double EndPointX { get; set; }
        public double EndPointY { get; set; }
        public Brush Stroke { get; set; }
        public DoubleCollection StrokeDashArray { get; set; }
        public double StrokeThickness { get; set; }

        public LineIndicatorViewModel()
        {
        }

        public LineIndicatorViewModel(Line line)
        {
            StartPointX = line.X1;
            StartPointY = line.Y1;
            EndPointX = line.X2;
            EndPointY = line.Y2;
        }

        public LineIndicatorViewModel(Line line,  Brush stroke, DoubleCollection strokeDashArray, double strokeThickness)
        {
            StartPointX = line.X1;
            StartPointY = line.Y1;
            EndPointX = line.X2;
            EndPointY = line.Y2;
            Stroke = stroke;
            StrokeDashArray = strokeDashArray;
            StrokeThickness = strokeThickness;
        }
    }
}