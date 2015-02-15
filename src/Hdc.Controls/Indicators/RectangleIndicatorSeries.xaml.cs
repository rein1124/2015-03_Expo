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
    /// Interaction logic for RectangleIndicatorSeries.xaml
    /// </summary>
    public partial class RectangleIndicatorSeries : IndicatorSeriesBase
    {
        private List<RectangleIndicator> _indicators = new List<RectangleIndicator>();

        public RectangleIndicatorSeries()
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
            "ItemsSource", typeof (IEnumerable), typeof (RectangleIndicatorSeries),
            new PropertyMetadata(OnMeasurementItemsSourcePropertyChangedCallback));

        private static void OnMeasurementItemsSourcePropertyChangedCallback(DependencyObject s,
                                                                            DependencyPropertyChangedEventArgs e)
        {
            var me = s as RectangleIndicatorSeries;
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

        #region RectCenterXPath

        public string RectCenterXPath
        {
            get { return (string) GetValue(RectCenterXPathProperty); }
            set { SetValue(RectCenterXPathProperty, value); }
        }

        public static readonly DependencyProperty RectCenterXPathProperty = DependencyProperty.Register(
            "RectCenterXPath", typeof (string), typeof (RectangleIndicatorSeries));

        #endregion

        #region RectCenterYPath

        public string RectCenterYPath
        {
            get { return (string) GetValue(RectCenterYPathProperty); }
            set { SetValue(RectCenterYPathProperty, value); }
        }

        public static readonly DependencyProperty RectCenterYPathProperty = DependencyProperty.Register(
            "RectCenterYPath", typeof (string), typeof (RectangleIndicatorSeries));

        #endregion

        #region RectWidthPath

        public string RectWidthPath
        {
            get { return (string) GetValue(RectWidthPathProperty); }
            set { SetValue(RectWidthPathProperty, value); }
        }

        public static readonly DependencyProperty RectWidthPathProperty = DependencyProperty.Register(
            "RectWidthPath", typeof (string), typeof (RectangleIndicatorSeries));

        #endregion

        #region RectHeightPath

        public string RectHeightPath
        {
            get { return (string) GetValue(RectHeightPathProperty); }
            set { SetValue(RectHeightPathProperty, value); }
        }

        public static readonly DependencyProperty RectHeightPathProperty = DependencyProperty.Register(
            "RectHeightPath", typeof (string), typeof (RectangleIndicatorSeries));

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
                var rectCenterXPath = GetPropertyValue(element, RectCenterXPath);
                var rectCenterYPath = GetPropertyValue(element, RectCenterYPath);
                var rectWidthPath = GetPropertyValue(element, RectWidthPath);
                var rectHeightPath = GetPropertyValue(element, RectHeightPath);

                Brush stroke = StrokePath != null ? (Brush) GetPropertyValue(element, StrokePath) : Brushes.Red;
                double strokeThickness = StrokeThicknessPath != null
                    ? (double) GetPropertyValue(element, StrokeThicknessPath)
                    : 2;
                DoubleCollection strokeDashArray = StrokeDashArrayPath != null
                    ? (DoubleCollection) GetPropertyValue(element, StrokeDashArrayPath)
                    : null;

                var ri = new RectangleIndicator
                         {
                             RectCenterX = (double) rectCenterXPath,
                             RectCenterY = (double) rectCenterYPath,
                             RectWidth = (double) rectWidthPath,
                             RectHeight = (double) rectHeightPath,
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

//                BindingOperations.SetBinding(ri, RectangleIndicator.RectCenterXProperty,
//                    new Binding("RectCenterX")
//                    {
//                        Source = IndicatorViewer,
//                        Mode = BindingMode.OneWay,
//                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
//                    });
//                BindingOperations.SetBinding(ri, RectangleIndicator.RectCenterYProperty,
//                    new Binding("RectCenterY")
//                    {
//                        Source = IndicatorViewer,
//                        Mode = BindingMode.OneWay,
//                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
//                    });
//                BindingOperations.SetBinding(ri, RectangleIndicator.RectWidthProperty,
//                    new Binding("RectWidth")
//                    {
//                        Source = IndicatorViewer,
//                        Mode = BindingMode.OneWay,
//                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
//                    });
//                BindingOperations.SetBinding(ri, RectangleIndicator.RectHeightProperty,
//                    new Binding("RectHeight")
//                    {
//                        Source = IndicatorViewer,
//                        Mode = BindingMode.OneWay,
//                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
//                    });
//                BindingOperations.SetBinding(ri, RectangleIndicator.ScaleProperty,
//                    new Binding("Scale")
//                    {
//                        Source = IndicatorViewer,
//                        Mode = BindingMode.OneWay,
//                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
//                    });

                _indicators.Add(ri);
                Canvas.Children.Add(ri);
            }

            UpdatePositions();
        }

        private void UpdatePositions()
        {
            foreach (var indicator in _indicators)
            {
                indicator.UpdatePositions();
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