using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hdc.Controls
{
    /// <summary>
    /// Interaction logic for MultiDistanceUserControl.xaml
    /// </summary>
    [ContentProperty("Series")]
    public partial class IndicatorViewer : UserControl
    {
        private bool isStart;
        private Point startPoint;
        private Point nowPoint;

        private readonly ObservableCollection<IndicatorSeriesBase> _series =
            new ObservableCollection<IndicatorSeriesBase>();

        public IndicatorViewer()
        {
            InitializeComponent();

            PreviewMouseWheel += MultiDistanceControl_PreviewMouseWheel;
            PreviewMouseDown += MultiDistanceControl_PreviewMouseDown;
            PreviewMouseMove += MultiDistanceControl_PreviewMouseMove;
            PreviewMouseUp += MultiDistanceControl_PreviewMouseUp;
            PreviewMouseDoubleClick += MultiIndicatorViewer_PreviewMouseDoubleClick;
            LostFocus += MultiIndicatorViewer_LostFocus;
            SizeChanged += MultiIndicatorViewer_SizeChanged;

            _series.CollectionChanged += _series_CollectionChanged;
        }

        private void _series_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var newItem in e.NewItems)
            {
                var indicatorSeriesBase = newItem as IndicatorSeriesBase;
                if (indicatorSeriesBase == null) continue;

                indicatorSeriesBase.Canvas = Canvas;
                indicatorSeriesBase.IndicatorViewer = this;

                //                BindingOperations.SetBinding(indicatorSeriesBase, IndicatorSeriesBase.XProperty, new Binding("X") { Source = this, Mode = BindingMode.OneWay, UpdateSourceTrigger = UpdateSourceTrigger .PropertyChanged});
                //                BindingOperations.SetBinding(indicatorSeriesBase, IndicatorSeriesBase.YProperty, new Binding("Y") { Source = this, Mode = BindingMode.OneWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                //                BindingOperations.SetBinding(indicatorSeriesBase, IndicatorSeriesBase.ScaleProperty, new Binding("Scale") { Source = this, Mode = BindingMode.OneWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

                Canvas.Children.Add(indicatorSeriesBase);
            }
        }

        private void MultiIndicatorViewer_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(this);
            Zoom(pos, 2.0);
        }

        private void MultiIndicatorViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void MultiIndicatorViewer_LostFocus(object sender, RoutedEventArgs e)
        {
            isStart = false;
        }

        private void MultiDistanceControl_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            EndDrag();
        }

        public void EndDrag()
        {
            isStart = false;
        }

        private void MultiDistanceControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Drag(e);
        }

        public void Drag(MouseEventArgs e)
        {
            if (!isStart)
                return;

            e.GetPosition(this);
            nowPoint = e.GetPosition(this);
            var differ = startPoint - nowPoint;
            X -= differ.X;
            Y -= differ.Y;
            startPoint = nowPoint;
        }

        private void MultiDistanceControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                BeginDrag(e);

            if (e.ChangedButton == MouseButton.Middle)
                ZoomFit();
        }

        public void BeginDrag(MouseButtonEventArgs e)
        {
            isStart = true;
            startPoint = e.GetPosition(this);
            nowPoint = e.GetPosition(this);
        }

        private void MultiDistanceControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ZoomByMouseWheel(e);
        }

        public void ZoomByMouseWheel(MouseWheelEventArgs e)
        {
            var pos = e.GetPosition(this);

            if (e.Delta > 0)
            {
                Zoom(pos, 2.0);
//                ZoomIn();
            }
            else if (e.Delta < 0)
            {
                Zoom(pos, 0.5);
//                ZoomOut();
            }
        }

        public void ZoomIn()
        {
            var centerX = (ActualWidth / 2.0);
            var centerY = (ActualHeight / 2.0);

            Zoom(new Point(centerX, centerY), 2.0);

            Refresh();
        }

        public void ZoomOut()
        {
            var centerX = (ActualWidth / 2.0);
            var centerY = (ActualHeight / 2.0);

            Zoom(new Point(centerX, centerY), 0.5);

            Refresh();
        }

        public void Zoom(Point zoomCenterPoint, double scale)
        {
            Scale *= scale;
            X *= scale;
            Y *= scale;

            X -= zoomCenterPoint.X * (scale - 1);
            Y -= zoomCenterPoint.Y * (scale - 1);

            Refresh();
        }

        public void ZoomActual()
        {
            Scale = 1.0;

            Refresh();
        }

        public void ZoomFit()
        {
            if (BitmapSource == null)
                return;

            var wRatio = ActualWidth / BitmapSource.PixelWidth;
            var hRatio = ActualHeight / BitmapSource.PixelHeight;
            if (wRatio > hRatio)
            {
                Scale = hRatio;
                X = (ActualWidth - BitmapSource.PixelWidth * hRatio) / 2;
                Y = 0.0;
            }
            else
            {
                Scale = wRatio;
                X = 0.0;
                Y = (ActualHeight - BitmapSource.PixelHeight * wRatio) / 2;
            }

            Refresh();
        }

        #region BitmapSource

        public BitmapSource BitmapSource
        {
            get { return (BitmapSource)GetValue(BitmapSourceProperty); }
            set { SetValue(BitmapSourceProperty, value); }
        }

        public static readonly DependencyProperty BitmapSourceProperty = DependencyProperty.Register(
            "BitmapSource", typeof(BitmapSource), typeof(IndicatorViewer), new PropertyMetadata((s, e) => { }));

        #endregion

        #region Scale

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof(double), typeof(IndicatorViewer),
            new PropertyMetadata(1.0, OnChangedCallback_UpdatePositions));

        //        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        //        {
        //            var me = s as IndicatorViewer;
        //            if (me == null) return;
        //            if (me.BitmapSource == null) return;
        //
        //            //            var baseScale = me.BitmapSource.PixelWidth/1920/2;
        //            //            if (baseScale < 1) return;
        //
        //            var newScale = (double)e.NewValue;
        //            //            if (newScale > (baseScale/2.0))
        //            if (newScale >= 1)
        //            {
        //                me.Image.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.NearestNeighbor);
        //            }
        //            else
        //            {
        //                me.Image.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.Linear);
        //            }
        //        }

        #endregion

        #region X

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            "X", typeof(double), typeof(IndicatorViewer), new PropertyMetadata(OnChangedCallback_UpdatePositions));

        #endregion

        #region Y

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y", typeof(double), typeof(IndicatorViewer), new PropertyMetadata(OnChangedCallback_UpdatePositions));

        #endregion

        public void Refresh()
        {
            foreach (var s in _series)
            {
                s.Refresh();
            }
        }

        public Collection<IndicatorSeriesBase> Series
        {
            get { return _series; }
            //            set { _series = value; }
        }

        private static void OnChangedCallback_UpdatePositions(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as IndicatorViewer;
            me.Refresh();
        }
    }
}