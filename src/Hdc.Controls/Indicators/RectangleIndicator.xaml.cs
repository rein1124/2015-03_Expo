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
    /// Interaction logic for RectangleIndicator.xaml
    /// </summary>
    public partial class RectangleIndicator : IndicatorBase
    {
        public RectangleIndicator()
        {
            InitializeComponent();
        }

        #region RectCenterX

        public double RectCenterX
        {
            get { return (double)GetValue(RectCenterXProperty); }
            set { SetValue(RectCenterXProperty, value); }
        }

        public static readonly DependencyProperty RectCenterXProperty = DependencyProperty.Register(
            "RectCenterX", typeof(double), typeof(RectangleIndicator));

        #endregion

        #region RectCenterY

        public double RectCenterY
        {
            get { return (double)GetValue(RectCenterYProperty); }
            set { SetValue(RectCenterYProperty, value); }
        }

        public static readonly DependencyProperty RectCenterYProperty = DependencyProperty.Register(
            "RectCenterY", typeof(double), typeof(RectangleIndicator));

        #endregion

        #region RectWidth

        public double RectWidth
        {
            get { return (double)GetValue(RectWidthProperty); }
            set { SetValue(RectWidthProperty, value); }
        }

        public static readonly DependencyProperty RectWidthProperty = DependencyProperty.Register(
            "RectWidth", typeof(double), typeof(RectangleIndicator));

        #endregion

        #region RectHeight

        public double RectHeight
        {
            get { return (double)GetValue(RectHeightProperty); }
            set { SetValue(RectHeightProperty, value); }
        }

        public static readonly DependencyProperty RectHeightProperty = DependencyProperty.Register(
            "RectHeight", typeof(double), typeof(RectangleIndicator));

        #endregion

        public void UpdatePositions()
        {
            var displayWidth = RectWidth * Scale;
            var displayHeight = RectHeight * Scale;

            var topLeftX = X + RectCenterX * Scale - displayWidth / 2.0;
            var topLeftY = Y + RectCenterY * Scale - displayHeight / 2.0;

            RegionIndicatorMarker.SetValue(Canvas.LeftProperty, topLeftX);
            RegionIndicatorMarker.SetValue(Canvas.TopProperty, topLeftY);
            RegionIndicatorMarker.Width = displayWidth;
            RegionIndicatorMarker.Height = displayHeight;

            RegionIndicatorMarker.Stroke = Stroke ?? Brushes.Magenta;
            RegionIndicatorMarker.StrokeThickness = StrokeThickness <= 0 ? 2 : StrokeThickness;
            RegionIndicatorMarker.StrokeDashArray = StrokeDashArray;
        }
    }
}
