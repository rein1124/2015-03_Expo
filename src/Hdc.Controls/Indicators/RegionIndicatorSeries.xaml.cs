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
    /// Interaction logic for RegionIndicatorSeries.xaml
    /// </summary>
    public partial class RegionIndicatorSeries : IndicatorSeriesBase
    {
        private List<RegionIndicator> _indicators = new List<RegionIndicator>();

        public RegionIndicatorSeries()
        {
            InitializeComponent();
        }

        #region ItemsSource

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof (IEnumerable), typeof (RegionIndicatorSeries),
            new PropertyMetadata(OnMeasurementItemsSourcePropertyChangedCallback));

        private static void OnMeasurementItemsSourcePropertyChangedCallback(DependencyObject s,
                                                                            DependencyPropertyChangedEventArgs e)
        {
            var me = s as RegionIndicatorSeries;
            me.UpdateMeasurements(e.NewValue as IEnumerable);

            //
            var ds = e.NewValue as INotifyCollectionChanged;
            NotifyCollectionChangedEventHandler dsOnCollectionChanged =
                (x, y) => me.UpdateMeasurements(ds as IEnumerable);
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

        #region StartPointXPath

        public string StartPointXPath
        {
            get { return (string) GetValue(StartPointXPathProperty); }
            set { SetValue(StartPointXPathProperty, value); }
        }

        public static readonly DependencyProperty StartPointXPathProperty = DependencyProperty.Register(
            "StartPointXPath", typeof (string), typeof (RegionIndicatorSeries));

        #endregion

        #region StartPointYPath

        public string StartPointYPath
        {
            get { return (string) GetValue(StartPointYPathProperty); }
            set { SetValue(StartPointYPathProperty, value); }
        }

        public static readonly DependencyProperty StartPointYPathProperty = DependencyProperty.Register(
            "StartPointYPath", typeof (string), typeof (RegionIndicatorSeries));

        #endregion

        #region EndPointXPath

        public string EndPointXPath
        {
            get { return (string) GetValue(EndPointXPathProperty); }
            set { SetValue(EndPointXPathProperty, value); }
        }

        public static readonly DependencyProperty EndPointXPathProperty = DependencyProperty.Register(
            "EndPointXPath", typeof (string), typeof (RegionIndicatorSeries));

        #endregion

        #region EndPointYPath

        public string EndPointYPath
        {
            get { return (string) GetValue(EndPointYPathProperty); }
            set { SetValue(EndPointYPathProperty, value); }
        }

        public static readonly DependencyProperty EndPointYPathProperty = DependencyProperty.Register(
            "EndPointYPath", typeof (string), typeof (RegionIndicatorSeries));

        #endregion

        #region RegionWidthPath

        public string RegionWidthPath
        {
            get { return (string) GetValue(RegionWidthPathProperty); }
            set { SetValue(RegionWidthPathProperty, value); }
        }

        public static readonly DependencyProperty RegionWidthPathProperty = DependencyProperty.Register(
            "RegionWidthPath", typeof (string), typeof (RegionIndicatorSeries));

        #endregion

        private void UpdateMeasurements(IEnumerable enumerable)
        {
            if (enumerable == null)
                return;

            for (int i = 0; i < _indicators.Count; i++)
            {
                var startPointRectangle = _indicators[i];

                Canvas.Children.Remove(startPointRectangle);
            }

            _indicators.Clear();

            foreach (var element in enumerable)
            {
                //
                var startX = GetPropertyValue(element, StartPointXPath);
                var startY = GetPropertyValue(element, StartPointYPath);
                var endX = GetPropertyValue(element, EndPointXPath);
                var endY = GetPropertyValue(element, EndPointYPath);
                var regionWidth = GetPropertyValue(element, RegionWidthPath);

                Brush stroke = StrokePath != null ? (Brush)GetPropertyValue(element, StrokePath) : Brushes.Red;
                double strokeThickness = StrokeThicknessPath != null ? (double)GetPropertyValue(element, StrokeThicknessPath) : 2;
                DoubleCollection strokeDashArray = StrokeDashArrayPath != null ? (DoubleCollection)GetPropertyValue(element, StrokeDashArrayPath) : null;

                var ri = new RegionIndicator
                         {
                             StartPointX = (double) startX,
                             StartPointY = (double)startY,
                             EndPointX = (double)endX,
                             EndPointY = (double)endY,
                             RegionWidth = (double)regionWidth,

                             Stroke = stroke,
                             StrokeThickness = strokeThickness,
                             StrokeDashArray = strokeDashArray,
                         };
                ri.DataContext = element;

                BindingOperations.SetBinding(ri, IndicatorBase.XProperty,
                    new Binding("X")
                    {
                        Source = IndicatorViewer,
                        Mode = BindingMode.OneWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });
                BindingOperations.SetBinding(ri, IndicatorBase.YProperty,
                    new Binding("Y")
                    {
                        Source = IndicatorViewer,
                        Mode = BindingMode.OneWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });
                BindingOperations.SetBinding(ri, IndicatorBase.ScaleProperty,
                    new Binding("Scale")
                    {
                        Source = IndicatorViewer,
                        Mode = BindingMode.OneWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });

                _indicators.Add(ri);
                Canvas.Children.Add(ri);
            }

            UpdatePositions();
        }

        private void UpdatePositions()
        {
            foreach (var regionIndicator in _indicators)
            {
                regionIndicator.UpdatePositions();
            }
        }

        public override void Refresh()
        {
            UpdateMeasurements(ItemsSource);
        }

        public static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}