using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hdc.Controls
{
    /// <summary>
    /// Interaction logic for LineIndicator.xaml
    /// </summary>
    public partial class LineIndicator : IndicatorBase
    {
        private Line _line = new Line();

        public LineIndicator()
        {
            InitializeComponent();

            _line.Stroke = Brushes.Red;
            _line.StrokeThickness = 2;

            Canvas.Children.Add(_line);
        }

        #region StartPointX

        public double StartPointX
        {
            get { return (double)GetValue(StartPointXProperty); }
            set { SetValue(StartPointXProperty, value); }
        }

        public static readonly DependencyProperty StartPointXProperty = DependencyProperty.Register(
            "StartPointX", typeof(double), typeof(LineIndicator));

        #endregion

        #region StartPointY

        public double StartPointY
        {
            get { return (double)GetValue(StartPointYProperty); }
            set { SetValue(StartPointYProperty, value); }
        }

        public static readonly DependencyProperty StartPointYProperty = DependencyProperty.Register(
            "StartPointY", typeof(double), typeof(LineIndicator));

        #endregion

        #region EndPointX

        public double EndPointX
        {
            get { return (double)GetValue(EndPointXProperty); }
            set { SetValue(EndPointXProperty, value); }
        }

        public static readonly DependencyProperty EndPointXProperty = DependencyProperty.Register(
            "EndPointX", typeof(double), typeof(LineIndicator));

        #endregion

        #region EndPointY

        public double EndPointY
        {
            get { return (double)GetValue(EndPointYProperty); }
            set { SetValue(EndPointYProperty, value); }
        }

        public static readonly DependencyProperty EndPointYProperty = DependencyProperty.Register(
            "EndPointY", typeof(double), typeof(LineIndicator));

        #endregion

        public void UpdatePositions()
        {
            var displayStartPointX = X + StartPointX * Scale;
            var displayStartPointY = Y + StartPointY * Scale;

            var displayEndPointX = X + EndPointX * Scale;
            var displayEndPointY = Y + EndPointY * Scale;

            _line.X1 = displayStartPointX;
            _line.Y1 = displayStartPointY;

            _line.X2 = displayEndPointX;
            _line.Y2 = displayEndPointY;

            var leftCornerX = displayStartPointX < displayEndPointX ? displayStartPointX : displayEndPointX;
            var leftCornerY = displayStartPointY < displayEndPointY ? displayStartPointY : displayEndPointY;
            //            var displayRegionWidth = RegionWidth * Scale;

            var startVector = new Vector(displayStartPointX, displayStartPointY);
            var endVector = new Vector(displayEndPointX, displayEndPointY);
            Vector linkVector = (startVector - endVector);
            var length = linkVector.Length;

            var angle = Vector.AngleBetween(new Vector(0, 100), linkVector);

            LineIndicatorMarker.SetValue(Canvas.LeftProperty, leftCornerX);
            LineIndicatorMarker.SetValue(Canvas.TopProperty, leftCornerY);
//            LineIndicatorMarker.Width = displayRegionWidth;
            LineIndicatorMarker.Height = length;
            var renderTransform = new TransformGroup();
//            renderTransform.Children.Add(new TranslateTransform(0 - (int)displayRegionWidth / 2, 0));
            renderTransform.Children.Add(new RotateTransform(angle));
            LineIndicatorMarker.RenderTransform = renderTransform;


            _line.Stroke = Stroke ?? Brushes.Magenta;
            _line.StrokeThickness = StrokeThickness <= 0 ? 2 : StrokeThickness;
            _line.StrokeDashArray = StrokeDashArray;
        }
    }
}
