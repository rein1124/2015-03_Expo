using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hdc.Controls
{
    /// <summary>
    /// Interaction logic for ZoomImageViewerR2.xaml
    /// </summary>
    public partial class ZoomImageViewerR2 : UserControl
    {
        private bool isStart;
        private Point startPoint;
        private Point nowPoint;

        public ZoomImageViewerR2()
        {
            InitializeComponent();

            PreviewMouseWheel += MultiDistanceControl_PreviewMouseWheel;
            PreviewMouseDown += MultiDistanceControl_PreviewMouseDown;
            PreviewMouseMove += MultiDistanceControl_PreviewMouseMove;
            PreviewMouseUp += MultiDistanceControl_PreviewMouseUp;
            PreviewMouseDoubleClick += ZoomImageViewer_PreviewMouseDoubleClick;
            LostFocus += ZoomImageViewer_LostFocus;
            SizeChanged += ZoomImageViewer_SizeChanged;
        }

        private void ZoomImageViewer_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Zoom(1.1);
        }

        private void ZoomImageViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void ZoomImageViewer_LostFocus(object sender, RoutedEventArgs e)
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
            BeginDrag(e);
        }

        public void BeginDrag(MouseButtonEventArgs e)
        {
            isStart = true;
            startPoint = e.GetPosition(this);
            nowPoint = e.GetPosition(this);
        }

        private void MultiDistanceControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Zoom(e);
        }

        public void Zoom(MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                Zoom(1.2);
            }
            else if (e.Delta < 0)
            {
                Zoom(0.8);
            }
        }

        public void Zoom(double zoom)
        {
            var mousePositionOfImage = Mouse.GetPosition(this);
            Debug.WriteLine("Mouse.X=" + mousePositionOfImage.X + "; Y=" + mousePositionOfImage.Y);

            var mousePositionOfCanvas = TransformGroup.Inverse.Transform(mousePositionOfImage);
            Debug.WriteLine("newCenter.X=" + mousePositionOfCanvas.X + "; Y=" + mousePositionOfCanvas.Y);

            var newScale  = zoom * Scale;

            double newX = -1 * (mousePositionOfCanvas.X * newScale - mousePositionOfImage.X);
            double newY = -1 * (mousePositionOfCanvas.Y * newScale - mousePositionOfImage.Y);

            X = newX;
            Y = newY;
            Scale = newScale;
            
//            TranslateTransform.BeginAnimation(TranslateTransform.XProperty, CreateZoomAnimation(newX));
//            TranslateTransform.BeginAnimation(TranslateTransform.YProperty, CreateZoomAnimation(newY));
//
//            ScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, CreateZoomAnimation(newScale));
//            ScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, CreateZoomAnimation(newScale));
        }

        public void Zoom2(double zoom)
        {
            var mousePositionOfImage = Mouse.GetPosition(this);
            Debug.WriteLine("Mouse.X=" + mousePositionOfImage.X + "; Y=" + mousePositionOfImage.Y);

//            Point physicalPoint = e.GetPosition(this);
//            Point mousePosition = transformGroup.Inverse.Transform(physicalPoint);


            var mousePositionOfCanvas = TransformGroup.Inverse.Transform(mousePositionOfImage);
            Debug.WriteLine("newCenter.X=" + mousePositionOfCanvas.X + "; Y=" + mousePositionOfCanvas.Y);


//            double X = -1 * (mousePositionOfCanvas.X * Scale - mousePositionOfImage.X);
//            double Y = -1 * (mousePositionOfCanvas.Y * Scale - mousePositionOfImage.Y);

            ScaleTransform.BeginAnimation(ScaleTransform.CenterXProperty, CreateZoomAnimation(mousePositionOfCanvas.X));
            ScaleTransform.BeginAnimation(ScaleTransform.CenterYProperty, CreateZoomAnimation(mousePositionOfCanvas.Y));

            Scale *= zoom;

            ScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, CreateZoomAnimation(Scale));
            ScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, CreateZoomAnimation(Scale));
        }


        private static DoubleAnimation CreateZoomAnimation(double toValue)
        {
            var da = new DoubleAnimation(toValue, new Duration(TimeSpan.FromMilliseconds(0)))
                     {
                         AccelerationRatio = 0.1,
                         DecelerationRatio = 0.9,
                         FillBehavior = FillBehavior.HoldEnd
                     };
            da.Freeze();
            return da;
        }

        public void ZoomActual()
        {
            Scale = 1.0;
        }

        public void ZoomFit()
        {
            if (BitmapSource == null)
                return;

            var wRatio = ActualWidth/BitmapSource.PixelWidth;
            var hRatio = ActualHeight/BitmapSource.PixelHeight;
            if (wRatio > hRatio)
            {
                Scale = hRatio;
                X = (ActualWidth - BitmapSource.PixelWidth*hRatio)/2;
                Y = 0.0;
            }
            else
            {
                Scale = wRatio;
                X = 0.0;
                Y = (ActualHeight - BitmapSource.PixelHeight*wRatio)/2;
            }
        }

        #region BitmapSource

        public BitmapSource BitmapSource
        {
            get { return (BitmapSource) GetValue(BitmapSourceProperty); }
            set { SetValue(BitmapSourceProperty, value); }
        }

        public static readonly DependencyProperty BitmapSourceProperty = DependencyProperty.Register(
            "BitmapSource", typeof (BitmapSource), typeof (ZoomImageViewerR2), new PropertyMetadata((s, e) => { }));

        #endregion

        #region Scale

        public double Scale
        {
            get { return (double) GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof (double), typeof (ZoomImageViewerR2), new PropertyMetadata(1.0, OnPropertyChangedCallback));

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as ZoomImageViewerR2;
            if (me == null) return;
            if (me.BitmapSource == null) return;

            //            var baseScale = me.BitmapSource.PixelWidth/1920/2;
            //            if (baseScale < 1) return;

            var newScale = (double) e.NewValue;
            //            if (newScale > (baseScale/2.0))
            if (newScale >= 1)
            {
                me.Image.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.NearestNeighbor);
            }
            else
            {
                me.Image.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.Linear);
            }
        }

        #endregion

        #region X

        public double X
        {
            get { return (double) GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            "X", typeof (double), typeof (ZoomImageViewerR2));

        #endregion

        #region Y

        public double Y
        {
            get { return (double) GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y", typeof (double), typeof (ZoomImageViewerR2));

        #endregion

//        #region ScaleCenterX
//
//        public double ScaleCenterX
//        {
//            get { return (double) GetValue(ScaleCenterXProperty); }
//            set { SetValue(ScaleCenterXProperty, value); }
//        }
//
//        public static readonly DependencyProperty ScaleCenterXProperty = DependencyProperty.Register(
//            "ScaleCenterX", typeof (double), typeof (ZoomImageViewerR2));
//
//        #endregion
//
//        #region ScaleCenterY
//
//        public double ScaleCenterY
//        {
//            get { return (double) GetValue(ScaleCenterYProperty); }
//            set { SetValue(ScaleCenterYProperty, value); }
//        }
//
//        public static readonly DependencyProperty ScaleCenterYProperty = DependencyProperty.Register(
//            "ScaleCenterY", typeof (double), typeof (ZoomImageViewerR2));
//
//        #endregion
    }
}