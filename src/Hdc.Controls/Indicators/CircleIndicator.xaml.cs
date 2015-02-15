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
    /// Interaction logic for CircleIndicator.xaml
    /// </summary>
    public partial class CircleIndicator : IndicatorBase
    {
        private Ellipse _ellipse = new Ellipse();

        public CircleIndicator()
        {
            InitializeComponent();

//            _ellipse.Stroke = Brushes.Orange;
//            _ellipse.StrokeThickness = 2;

            Canvas.Children.Add(_ellipse);
        }

        #region CenterX

        public double CenterX
        {
            get { return (double) GetValue(CenterXProperty); }
            set { SetValue(CenterXProperty, value); }
        }

        public static readonly DependencyProperty CenterXProperty = DependencyProperty.Register(
            "CenterX", typeof (double), typeof (CircleIndicator));

        #endregion

        #region CenterY

        public double CenterY
        {
            get { return (double) GetValue(CenterYProperty); }
            set { SetValue(CenterYProperty, value); }
        }

        public static readonly DependencyProperty CenterYProperty = DependencyProperty.Register(
            "CenterY", typeof (double), typeof (CircleIndicator));

        #endregion

        #region Radius

        public double Radius
        {
            get { return (double) GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof (double), typeof (CircleIndicator));

        #endregion

        public void UpdatePositions()
        {
            var displayStartPointX = X + CenterX*Scale - Radius*Scale;
            var displayStartPointY = Y + CenterY*Scale - Radius*Scale;

            var displayRadius = Radius*Scale;

            _ellipse.SetValue(Canvas.LeftProperty, displayStartPointX);
            _ellipse.SetValue(Canvas.TopProperty, displayStartPointY);
            _ellipse.Width = displayRadius*2;
            _ellipse.Height = displayRadius*2;
            _ellipse.Stroke = Stroke ?? Brushes.Magenta;
            _ellipse.StrokeThickness = StrokeThickness <= 0 ? 2 : StrokeThickness;
            _ellipse.StrokeDashArray = StrokeDashArray;
        }
    }
}