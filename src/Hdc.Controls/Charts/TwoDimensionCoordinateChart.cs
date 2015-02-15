using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Linq;

namespace Hdc.Controls.Charts
{
    [TemplatePart(Name = "PART_XLine", Type = typeof(XLine))]
    [TemplatePart(Name = "PART_YLine", Type = typeof(YLine))]
    [TemplatePart(Name = "PART_RelativeXLine", Type = typeof(RelativeXLine))]
    [TemplatePart(Name = "PART_RelativeYLine", Type = typeof(RelativeYLine))]
    [TemplatePart(Name = "PART_ChartValue", Type = typeof(ChartValue))]
    public class TwoDimensionCoordinateChart : System.Windows.Controls.Control
    {
        private Canvas _container;
        private XLine _xLine;
        private YLine _yLine;
        private RelativeXLine _relativeXLine;
        private RelativeYLine _relativeYLine;
        private ChartValue _chartValue;

        static TwoDimensionCoordinateChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TwoDimensionCoordinateChart),
                                                     new FrameworkPropertyMetadata(typeof(TwoDimensionCoordinateChart)));
        }

        public TwoDimensionCoordinateChart()
        {
            Loaded += new RoutedEventHandler(TwoDimensionCoordinateChart_Loaded);
        }

        private void TwoDimensionCoordinateChart_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStates();
        }


        public static readonly DependencyProperty XTickProperty = DependencyProperty.Register(
            "XTick", typeof(double), typeof(TwoDimensionCoordinateChart)
            );

        public static readonly DependencyProperty YTickProperty = DependencyProperty.Register(
            "YTick", typeof(double), typeof(TwoDimensionCoordinateChart)
            );

        public static readonly DependencyProperty XMaxProperty = DependencyProperty.Register(
            "XMax", typeof(double), typeof(TwoDimensionCoordinateChart),
            new PropertyMetadata(OnPropertyPropertyChanged)
            );

        public static readonly DependencyProperty XMinProperty = DependencyProperty.Register(
            "XMin", typeof(double), typeof(TwoDimensionCoordinateChart),
            new PropertyMetadata(OnPropertyPropertyChanged)
            );

        public static readonly DependencyProperty YMaxProperty = DependencyProperty.Register(
            "YMax", typeof(double), typeof(TwoDimensionCoordinateChart)
            );

        public static readonly DependencyProperty YMinProperty = DependencyProperty.Register(
            "YMin", typeof(double), typeof(TwoDimensionCoordinateChart)
            );

        public static readonly DependencyProperty IsXInvertProperty = DependencyProperty.Register(
            "IsXInvert", typeof(bool), typeof(TwoDimensionCoordinateChart)
            );

        public static readonly DependencyProperty IsYInvertProperty = DependencyProperty.Register(
            "IsYInvert", typeof(bool), typeof(TwoDimensionCoordinateChart)
            );

        public static readonly DependencyProperty XValueProperty = DependencyProperty.Register(
            "XValue", typeof(double), typeof(TwoDimensionCoordinateChart),
            new PropertyMetadata(OnPropertyPropertyChanged)
            );

        public static readonly DependencyProperty YValueProperty = DependencyProperty.Register(
            "YValue", typeof(double), typeof(TwoDimensionCoordinateChart),
            new PropertyMetadata(OnPropertyPropertyChanged)
            );

        public static readonly DependencyProperty IsRelativeModeProperty = DependencyProperty.Register(
            "IsRelativeMode", typeof(bool), typeof(TwoDimensionCoordinateChart),
            new PropertyMetadata(OnModePropertyPropertyChanged)
            );

        public static readonly DependencyProperty RelativeXProperty = DependencyProperty.Register(
            "RelativeX", typeof(double), typeof(TwoDimensionCoordinateChart),
            new PropertyMetadata(OnRelativeValuePropertyPropertyChanged)
            );

        public static readonly DependencyProperty RelativeYProperty = DependencyProperty.Register(
            "RelativeY", typeof(double), typeof(TwoDimensionCoordinateChart),
            new PropertyMetadata(OnRelativeValuePropertyPropertyChanged)
            );

        public double XTick
        {
            get { return Convert.ToDouble(GetValue(XTickProperty)); }
            set { SetValue(XTickProperty, value); }
        }

        public double YTick
        {
            get { return Convert.ToDouble(GetValue(YTickProperty)); }
            set { SetValue(YTickProperty, value); }
        }

        public double XMax
        {
            get { return Convert.ToDouble(GetValue(XMaxProperty)); }
            set { SetValue(XMaxProperty, value); }
        }

        public double XMin
        {
            get { return Convert.ToDouble(GetValue(XMinProperty)); }
            set { SetValue(XMinProperty, value); }
        }

        public double YMax
        {
            get { return Convert.ToDouble(GetValue(YMaxProperty)); }
            set { SetValue(YMaxProperty, value); }
        }

        public double YMin
        {
            get { return Convert.ToDouble(GetValue(YMinProperty)); }
            set { SetValue(YMinProperty, value); }
        }

        public bool IsXInvert
        {
            get { return Convert.ToBoolean(GetValue(IsXInvertProperty)); }
            set { SetValue(IsXInvertProperty, value); }
        }

        public bool IsYInvert
        {
            get { return Convert.ToBoolean(GetValue(IsYInvertProperty)); }
            set { SetValue(IsYInvertProperty, value); }
        }

        public double XValue
        {
            get { return Convert.ToDouble(GetValue(XValueProperty)); }
            set { SetValue(XValueProperty, value); }
        }

        public double YValue
        {
            get { return Convert.ToDouble(GetValue(YValueProperty)); }
            set { SetValue(YValueProperty, value); }
        }

        public double RelativeX
        {
            get { return Convert.ToDouble(GetValue(RelativeXProperty)); }
            set { SetValue(RelativeXProperty, value); }
        }

        public double RelativeY
        {
            get { return Convert.ToDouble(GetValue(RelativeYProperty)); }
            set { SetValue(RelativeYProperty, value); }
        }

        public bool IsRelativeMode
        {
            get { return Convert.ToBoolean(GetValue(IsRelativeModeProperty)); }
            set { SetValue(IsRelativeModeProperty, value); }
        }

        private void UpdateStates()
        {
            //if (_container == null) return;
            //_container.Children.OfType<XTickbar>().ToList().ForEach(p => _container.Children.Remove(p));

            //_container.Children.OfType<YTickbar>().ToList().ForEach(p => _container.Children.Remove(p));

            //_container.Children.OfType<RelativeXTickbar>().ToList().ForEach(p => _container.Children.Remove(p));

            //_container.Children.OfType<RelativeYTickbar>().ToList().ForEach(p => _container.Children.Remove(p));
            //if (IsRelativeMode)
            //{
            //    _container.Children.OfType<XLine>().ToList().ForEach(p => p.Visibility = System.Windows.Visibility.Hidden);
            //    _container.Children.OfType<YLine>().ToList().ForEach(p => p.Visibility = System.Windows.Visibility.Hidden);
            //    _container.Children.OfType<RelativeXLine>().ToList().ForEach(p => p.Visibility = System.Windows.Visibility.Visible);
            //    _container.Children.OfType<RelativeYLine>().ToList().ForEach(p => p.Visibility = System.Windows.Visibility.Visible);
            //    //GenRTick();
            //}
            //else
            //{
            //    _container.Children.OfType<XLine>().ToList().ForEach(p => p.Visibility = System.Windows.Visibility.Visible);
            //    _container.Children.OfType<YLine>().ToList().ForEach(p => p.Visibility = System.Windows.Visibility.Visible);
            //    _container.Children.OfType<RelativeXLine>().ToList().ForEach(p => p.Visibility = System.Windows.Visibility.Hidden);
            //    _container.Children.OfType<RelativeYLine>().ToList().ForEach(p => p.Visibility = System.Windows.Visibility.Hidden);

            //             //   GenTick();
            //}
            if (!_templateApplied) return;

            _container.Children.OfType<XTickbar>().ToList().ForEach(p => VisualStateManager.GoToState(p,
                                                                                                      IsRelativeMode
                                                                                                          ? "RelativeMode"
                                                                                                          : "AbsoluteMode",
                                                                                                      true));

            _container.Children.OfType<YTickbar>().ToList().ForEach(p => VisualStateManager.GoToState(p,
                                                                                                      IsRelativeMode
                                                                                                          ? "RelativeMode"
                                                                                                          : "AbsoluteMode",
                                                                                                      true));

            _container.Children.OfType<RelativeXTickbar>().ToList().ForEach(p => VisualStateManager.GoToState(p,
                                                                                                              IsRelativeMode
                                                                                                                  ? "RelativeMode"
                                                                                                                  : "AbsoluteMode",
                                                                                                              true));

            _container.Children.OfType<RelativeYTickbar>().ToList().ForEach(p => VisualStateManager.GoToState(p,
                                                                                                              IsRelativeMode
                                                                                                                  ? "RelativeMode"
                                                                                                                  : "AbsoluteMode",
                                                                                                              true));

            _container.Children.OfType<XLine>().ToList().ForEach(p => VisualStateManager.GoToState(p,
                                                                                                   IsRelativeMode
                                                                                                       ? "RelativeMode"
                                                                                                       : "AbsoluteMode",
                                                                                                   true));
            _container.Children.OfType<YLine>().ToList().ForEach(p => VisualStateManager.GoToState(p,
                                                                                                   IsRelativeMode
                                                                                                       ? "RelativeMode"
                                                                                                       : "AbsoluteMode",
                                                                                                   true));
            _container.Children.OfType<RelativeXLine>().ToList().ForEach(p => VisualStateManager.GoToState(p,
                                                                                                           IsRelativeMode
                                                                                                               ? "RelativeMode"
                                                                                                               : "AbsoluteMode",
                                                                                                           true));
            _container.Children.OfType<RelativeYLine>().ToList().ForEach(p => VisualStateManager.GoToState(p,
                                                                                                           IsRelativeMode
                                                                                                               ? "RelativeMode"
                                                                                                               : "AbsoluteMode",
                                                                                                           true));
            //            GenRTick();
        }


        private static void OnRelativeValuePropertyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as TwoDimensionCoordinateChart;
            if (me == null) return;
            me.UpdateRelativeLayout();
        }

        private static void OnModePropertyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as TwoDimensionCoordinateChart;
            if (me == null) return;
            me.UpdateStates();
        }

        private static void OnPropertyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as TwoDimensionCoordinateChart;
            if (me == null) return;
            me.UpdateChartLayout();
        }

        void UpdateRelativeLayout()
        {
            if (!_templateApplied) return;
            Canvas.SetTop(_relativeXLine, ConvertY(RelativeY));
            Canvas.SetLeft(_relativeYLine, ConvertX(RelativeX));
        }


        private void UpdateChartLayout()
        {
            if (!_templateApplied) return;

            _xLine.Width = PanelWidth;
            _yLine.Height = PanelHeight;

            _relativeXLine.Width = PanelWidth;
            _relativeYLine.Height = PanelHeight;

            Canvas.SetTop(_xLine, PanelHeight / 2);
            Canvas.SetLeft(_yLine, PanelWidth / 2);



            Canvas.SetTop(_chartValue, ConvertY(YValue));
            Canvas.SetLeft(_chartValue, ConvertX(XValue));
        }

        private bool _templateApplied;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _container = GetTemplateChild("Container") as Canvas;
            _xLine = GetTemplateChild("PART_XLine") as XLine;
            _yLine = GetTemplateChild("PART_YLine") as YLine;
            _relativeXLine = GetTemplateChild("PART_RelativeXLine") as RelativeXLine;
            _relativeYLine = GetTemplateChild("PART_RelativeYLine") as RelativeYLine;
            _chartValue = GetTemplateChild("PART_ChartValue") as ChartValue;

            _templateApplied = true;

            UpdateStates();

            UpdateChartLayout();

            UpdateRelativeLayout();
        }

        private void GenTick()
        {
            if (_container == null) return;

            for (int i = 1; i < XMax / XTick - 1; i++)
            {
                var xBar = new XTickbar();
                _container.Children.Add(xBar);
                Canvas.SetLeft(xBar, 2 * i * Width / xBar.Height);
                Canvas.SetTop(xBar, Width / 2 - xBar.Height);
            }
            for (int i = 1; i < YMax / YTick - 1; i++)
            {
                var yBar = new YTickbar();
                _container.Children.Add(yBar);
                Canvas.SetLeft(yBar, Height / 2);
                Canvas.SetTop(yBar, 2 * i * Height / yBar.Width);
            }
        }

        private void GenRTick()
        {
            if (_container == null) return;


            for (int i = 1; i < XMax / XTick - 1; i++)
            {
                var xRBar = new RelativeXTickbar();
                _container.Children.Add(xRBar);
                Canvas.SetLeft(xRBar, 2 * i * Width / xRBar.Height + ConvertX(RelativeX) - Width / 2);
                Canvas.SetTop(xRBar, ConvertY(RelativeY) - xRBar.Height);
            }
            for (int i = 1; i < YMax / YTick - 1; i++)
            {
                var yRBar = new RelativeYTickbar();
                _container.Children.Add(yRBar);
                Canvas.SetLeft(yRBar, ConvertX(RelativeX));
                Canvas.SetTop(yRBar, 2 * i * Height / yRBar.Width + ConvertY(RelativeY) - Height / 2);
            }
        }

        private double ConvertX(double value)
        {
            if (XMax == XMin)
                return 0;
            else
            {
                double temp = PanelWidth / (XMax - XMin) * value;

                return temp >= 0 ? PanelWidth / 2 + Math.Abs(temp) : PanelWidth / 2 - Math.Abs(temp);

                //            return PanelWidth/2 - temp;
            }
        }

        private double ConvertY(double value)
        {
            if (YMax == YMin)
                return 0;
            else
            {
                var temp = PanelHeight / (YMax - YMin) * value;
                return temp >= 0 ? PanelHeight / 2 - Math.Abs(temp) : PanelHeight / 2 + Math.Abs(temp);

                //            return PanelHeight/2 - temp;
            }
        }

        public double PanelWidth
        {
            //            get { return _container.Width; }
            get { return Width; }
        }

        public double PanelHeight
        {
            //            get { return _container.Height; }
            get { return Height; }
        }
    }
}