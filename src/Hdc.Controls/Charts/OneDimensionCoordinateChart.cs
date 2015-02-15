using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Hdc.Controls.Charts
{
    [TemplatePart(Name = "PART_YLine", Type = typeof (YLine))]
    [TemplatePart(Name = "PART_ChartValue", Type = typeof (ChartValue))]
    [TemplatePart(Name = "PART_RelativeXLine", Type = typeof (RelativeYLine))]
    public class OneDimensionCoordinateChart : Control
    {
        private Canvas _container;
        private XLine _xLine;
        private YLine _yLine;
        private ChartValue _chartValue;
        private RelativeXLine _relativeXLine;
        private RelativeYLine _relativeYLine;

        //        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(double), typeof(OneDimensionCoordinateChart), new FrameworkPropertyMetadata(0.00, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnPropertyPropertyChanged, CoerceValue, true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));
        //
        //        public static readonly DependencyProperty MinProperty = DependencyProperty.Register("Min", typeof(double), typeof(OneDimensionCoordinateChart), new FrameworkPropertyMetadata(0.00, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnPropertyPropertyChanged, CoerceValue, true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));
        //
        //        public static readonly DependencyProperty TickProperty = DependencyProperty.Register("Tick", typeof(double), typeof(OneDimensionCoordinateChart), new FrameworkPropertyMetadata(0.00, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnPropertyPropertyChanged, CoerceValue, true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));
        //
        //        public static readonly DependencyProperty TextBrushProperty = DependencyProperty.Register("TextBrush", typeof(Brush), typeof(OneDimensionCoordinateChart), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnPropertyPropertyChanged, CoerceValue, true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));
        //
        //        public static readonly DependencyProperty AxisBrushProperty = DependencyProperty.Register("AxisBrush", typeof(Brush), typeof(OneDimensionCoordinateChart), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnPropertyPropertyChanged, CoerceValue, true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));
        //
        //        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(OneDimensionCoordinateChart), new FrameworkPropertyMetadata(0.00, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValuePropertyChanged, CoerceValue, true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));
        //
        //        public static DependencyProperty IsInvertProperty = DependencyProperty.Register("IsInvert", typeof(bool), typeof(OneDimensionCoordinateChart), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnPropertyPropertyChanged, CoerceValue, true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof (double),
                                                                                            typeof (
                                                                                                OneDimensionCoordinateChart
                                                                                                ),
                                                                                            new PropertyMetadata(
                                                                                                OnPropertyPropertyChanged));

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register("Min", typeof (double),
                                                                                            typeof (
                                                                                                OneDimensionCoordinateChart
                                                                                                ),
                                                                                            new PropertyMetadata(
                                                                                                OnPropertyPropertyChanged));

        public static readonly DependencyProperty TickProperty = DependencyProperty.Register("Tick", typeof (double),
                                                                                             typeof (
                                                                                                 OneDimensionCoordinateChart
                                                                                                 ),
                                                                                             new PropertyMetadata(
                                                                                                 OnPropertyPropertyChanged));

        public static readonly DependencyProperty TextBrushProperty = DependencyProperty.Register("TextBrush",
                                                                                                  typeof (Brush),
                                                                                                  typeof (
                                                                                                      OneDimensionCoordinateChart
                                                                                                      ),
                                                                                                  new PropertyMetadata(
                                                                                                      OnPropertyPropertyChanged));

        public static readonly DependencyProperty AxisBrushProperty = DependencyProperty.Register("AxisBrush",
                                                                                                  typeof (Brush),
                                                                                                  typeof (
                                                                                                      OneDimensionCoordinateChart
                                                                                                      ),
                                                                                                  new PropertyMetadata(
                                                                                                      OnPropertyPropertyChanged));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (double),
                                                                                              typeof (
                                                                                                  OneDimensionCoordinateChart
                                                                                                  ),
                                                                                              new PropertyMetadata(
                                                                                                  OnPropertyPropertyChanged));

        public static readonly DependencyProperty IsInvertProperty = DependencyProperty.Register("IsInvert",
                                                                                                 typeof (bool),
                                                                                                 typeof (
                                                                                                     OneDimensionCoordinateChart
                                                                                                     ),
                                                                                                 new PropertyMetadata(
                                                                                                     OnPropertyPropertyChanged));

        public static readonly DependencyProperty IsRelativeModeProperty = DependencyProperty.Register(
            "IsRelativeMode", typeof (bool), typeof (OneDimensionCoordinateChart),
            new PropertyMetadata(OnModePropertyPropertyChanged)
            );

        public static readonly DependencyProperty RelativeValueProperty = DependencyProperty.Register(
            "RelativeValue", typeof (double), typeof (OneDimensionCoordinateChart),
            new PropertyMetadata(OnRelativeValuePropertyPropertyChanged)
            );

        static OneDimensionCoordinateChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (OneDimensionCoordinateChart),
                                                     new FrameworkPropertyMetadata(typeof (OneDimensionCoordinateChart)));
        }

        public OneDimensionCoordinateChart()
        {
            Loaded += delegate { UpdateStates(); };
        }

        public double HalfWidth
        {
            get { return Width/2; }
        }

        [Bindable(true), Category("Common Properties")]
        public double Max
        {
            get { return Convert.ToDouble(GetValue(MaxProperty)); }
            set { SetValue(MaxProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public double Min
        {
            get { return Convert.ToDouble(GetValue(MinProperty)); }
            set { SetValue(MinProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public double Tick
        {
            get { return Convert.ToDouble(GetValue(TickProperty)); }
            set { SetValue(TickProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public Brush TextBrush
        {
            get { return GetValue(TextBrushProperty) as Brush; }
            set { SetValue(TextBrushProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public Brush AxisBrush
        {
            get { return GetValue(AxisBrushProperty) as Brush; }
            set { SetValue(AxisBrushProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public bool IsInvert
        {
            get { return Convert.ToBoolean(GetValue(IsInvertProperty)); }
            set { SetValue(IsInvertProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public double Value
        {
            get { return Convert.ToDouble(GetValue(ValueProperty)); }
            set { SetValue(ValueProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public bool IsRelativeMode
        {
            get { return Convert.ToBoolean(GetValue(IsRelativeModeProperty)); }
            set { SetValue(IsRelativeModeProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public double RelativeValue
        {
            get { return Convert.ToDouble(GetValue(RelativeValueProperty)); }
            set { SetValue(RelativeValueProperty, value); }
        }

        private static void OnPropertyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as OneDimensionCoordinateChart;
            if (me == null) return;
            me.UpdateChartLayout();
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((OneDimensionCoordinateChart) d).OnApplyTemplate();
        }

        private static object CoerceValue(DependencyObject d, object value)
        {
            return value;
        }

        private static void OnModePropertyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as OneDimensionCoordinateChart;
            if (me == null) return;
            me.UpdateStates();
        }

        private static void OnRelativeValuePropertyPropertyChanged(DependencyObject d,
                                                                   DependencyPropertyChangedEventArgs e)
        {
            var me = d as OneDimensionCoordinateChart;
            if (me == null) return;
            me.UpdateRelativeLayout();
        }

        private void UpdateStates()
        {
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
        }

        private void UpdateRelativeLayout()
        {
            if (!_templateApplied) return;
            Canvas.SetTop(_relativeXLine, ConvertY(RelativeValue));
            Canvas.SetLeft(_relativeYLine, Width/2);
        }

        private void UpdateChartLayout()
        {
            if (!_templateApplied) return;

            if (_container == null) return;

            if (_yLine != null) Canvas.SetLeft(_yLine, Width/2);

            if (_xLine != null) Canvas.SetTop(_xLine, Height/2);
            if (_xLine != null) Canvas.SetLeft(_xLine, Width/2 - _xLine.Width/2);

            if (_chartValue != null)
            {
                var point = _chartValue.FindName("Point") as Ellipse;
                Canvas.SetTop(_chartValue, ConvertY(Value));
                Canvas.SetLeft(_chartValue, HalfWidth - (point == null ? 4 : point.Width/2));
            }
        }

        private bool _templateApplied;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Template == null) return;
            //            _container = Template.FindName("Container", this) as Canvas;
            _container = GetTemplateChild("Container") as Canvas;
            _yLine = GetTemplateChild("PART_YLine") as YLine;
            _xLine = GetTemplateChild("PART_XLine") as XLine;
            _relativeXLine = GetTemplateChild("PART_RelativeXLine") as RelativeXLine;
            _relativeYLine = GetTemplateChild("PART_RelativeYLine") as RelativeYLine;
            _chartValue = GetTemplateChild("PART_ChartValue") as ChartValue;

            _templateApplied = true;

            UpdateStates();

            UpdateChartLayout();

            UpdateRelativeLayout();
        }

        private double ConvertY(double value)
        {
            if (Max == Min)
                return 0;
            else
            {
                var temp = Height/(Max - Min)*value;
                //            return Height/2 - temp;
                return temp >= 0 ? Height/2 - Math.Abs(temp) : Height/2 + Math.Abs(temp);
            }
        }
    }
}