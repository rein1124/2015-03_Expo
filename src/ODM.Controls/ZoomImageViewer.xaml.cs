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
    /// Interaction logic for MultiDistanceUserControl.xaml
    /// </summary>
    public partial class ZoomImageViewer : UserControl
    {
        private bool isStart;
        private Point startPoint;
        private Point nowPoint;

        public ZoomImageViewer()
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

        void ZoomImageViewer_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ZoomIn();
        }

        void ZoomImageViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }

        void ZoomImageViewer_LostFocus(object sender, RoutedEventArgs e)
        {
            isStart = false;
        }

        private void MultiDistanceControl_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            EndDrag();
        }

        public  void EndDrag()
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

        public  void Zoom(MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomIn();
            }
            else if (e.Delta < 0)
            {
                ZoomOut();
            }
        }

        public void ZoomIn()
        {
            Scale *= 2.0;
            X *= 2.0;
            Y *= 2.0;

            X -=  (ActualWidth/2.0);
            Y -=  (ActualHeight/2.0);
        }

        public void ZoomOut()
        {
            Scale /= 2.0;
            X /= 2.0;
            Y /= 2.0;

            X -=  ActualWidth/4.0*(-1.0);
            Y -=  ActualHeight/4.0*(-1.0);
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
            "BitmapSource", typeof (BitmapSource), typeof (ZoomImageViewer),new PropertyMetadata((s, e) =>
                                                                                                 {
                                                                                                     
                                                                                                 }));

        #endregion

        #region Scale

        public double Scale
        {
            get { return (double) GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof (double), typeof (ZoomImageViewer), new PropertyMetadata(1.0, OnPropertyChangedCallback));

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as ZoomImageViewer;
            if (me == null) return;
            if (me.BitmapSource == null) return;

//            var baseScale = me.BitmapSource.PixelWidth/1920/2;
//            if (baseScale < 1) return;

            var newScale = (double)e.NewValue;
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
            "X", typeof (double), typeof (ZoomImageViewer));

        #endregion

        #region Y

        public double Y
        {
            get { return (double) GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y", typeof (double), typeof (ZoomImageViewer));

        #endregion
    }
}