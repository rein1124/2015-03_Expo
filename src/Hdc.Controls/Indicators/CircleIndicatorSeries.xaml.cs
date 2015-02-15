using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Interaction logic for CircleIndicatorSeries.xaml
    /// </summary>
    public partial class CircleIndicatorSeries : IndicatorSeriesBase
    {
        public CircleIndicatorSeries()
        {
            InitializeComponent();
        }

        private readonly List<CircleIndicator> _circleIndicators = new List<CircleIndicator>();

        #region ItemsSource

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable), typeof(CircleIndicatorSeries),
            new PropertyMetadata(OnMeasurementItemsSourcePropertyChangedCallback));

        private static void OnMeasurementItemsSourcePropertyChangedCallback(DependencyObject s,
            DependencyPropertyChangedEventArgs e)
        {
            var me = s as CircleIndicatorSeries;
            me.UpdateMeasurements(e.NewValue as IEnumerable);

            //
            var ds = e.NewValue as INotifyCollectionChanged;
            NotifyCollectionChangedEventHandler dsOnCollectionChanged = (x, y) => me.UpdateMeasurements(ds as IEnumerable);
            if (ds != null)
            {
                ds.CollectionChanged += dsOnCollectionChanged;
            }

            var oldDs = e.OldValue as INotifyCollectionChanged;
            if (oldDs != null)
            {
                oldDs.CollectionChanged -= dsOnCollectionChanged;
            }
        }

        #endregion

        #region CenterXPath

        public string CenterXPath
        {
            get { return (string)GetValue(CenterXPathProperty); }
            set { SetValue(CenterXPathProperty, value); }
        }

        public static readonly DependencyProperty CenterXPathProperty = DependencyProperty.Register(
            "CenterXPath", typeof(string), typeof(CircleIndicatorSeries));

        #endregion

        #region CenterYPath

        public string CenterYPath
        {
            get { return (string)GetValue(CenterYPathProperty); }
            set { SetValue(CenterYPathProperty, value); }
        }

        public static readonly DependencyProperty CenterYPathProperty = DependencyProperty.Register(
            "CenterYPath", typeof(string), typeof(CircleIndicatorSeries));

        #endregion

        #region RadiusPath

        public string RadiusPath
        {
            get { return (string) GetValue(RadiusPathProperty); }
            set { SetValue(RadiusPathProperty, value); }
        }

        public static readonly DependencyProperty RadiusPathProperty = DependencyProperty.Register(
            "RadiusPath", typeof (string), typeof (CircleIndicatorSeries));

        #endregion

        private void UpdateMeasurements(IEnumerable enumerable)
        {
            if (enumerable == null)
                return;

            for (int i = 0; i < _circleIndicators.Count; i++)
            {
                var startPointRectangle = _circleIndicators[i];

                Canvas.Children.Remove(startPointRectangle);
            }

            _circleIndicators.Clear();

            foreach (var element in enumerable)
            {
                //
                var startX = GetPropertyValue(element, CenterXPath);
                var startY = GetPropertyValue(element, CenterYPath);
                var radius = GetPropertyValue(element, RadiusPath);

                Brush stroke = StrokePath != null ? (Brush)GetPropertyValue(element, StrokePath) : Brushes.Magenta;
                double strokeThickness = StrokeThicknessPath != null ? (double)GetPropertyValue(element, StrokeThicknessPath) : 2;
                DoubleCollection strokeDashArray = StrokeDashArrayPath != null ? (DoubleCollection)GetPropertyValue(element, StrokeDashArrayPath) : null;

                var ri = new CircleIndicator()
                {
                    CenterX = (double)startX,
                    CenterY = (double)startY,
                    Radius = (double)radius,
                    Stroke = stroke,
                    StrokeThickness = strokeThickness,
                    StrokeDashArray = strokeDashArray,
                };

                BindingOperations.SetBinding(ri, IndicatorBase.XProperty, new Binding("X") { Source = IndicatorViewer, Mode = BindingMode.OneWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                BindingOperations.SetBinding(ri, IndicatorBase.YProperty, new Binding("Y") { Source = IndicatorViewer, Mode = BindingMode.OneWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                BindingOperations.SetBinding(ri, IndicatorBase.ScaleProperty, new Binding("Scale") { Source = IndicatorViewer, Mode = BindingMode.OneWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

                _circleIndicators.Add(ri);
                Canvas.Children.Add(ri);
            }

            UpdatePositions();
        }

        void UpdatePositions()
        {
            foreach (var indicator in _circleIndicators)
            {
                indicator.UpdatePositions();
            }
        }

        public override void Refresh()
        {
            UpdateMeasurements(ItemsSource);
        }
    }
}
