using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hdc.Controls
{
    [ContentProperty("Content")]
    public class IndicateRegionBehavior : Behavior<Canvas>
    {
        private Rectangle _Rect = new Rectangle();
        private Border _contentBorder = new Border();

        public Canvas Canvas
        {
            get { return AssociatedObject; }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            Canvas.Children.Add(_Rect);
            Canvas.Children.Add(_contentBorder);
            Canvas.SizeChanged += OnCanvasOnSizeChanged;

            _contentBorder.SizeChanged += OnContentBorderOnSizeChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            Canvas.Children.Remove(_Rect);
            Canvas.Children.Remove(_contentBorder);
            Canvas.SizeChanged -= OnCanvasOnSizeChanged;

            _contentBorder.SizeChanged -= OnContentBorderOnSizeChanged;
        }

        private void OnContentBorderOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Update();
        }

        private void OnCanvasOnSizeChanged(object sender1, SizeChangedEventArgs e1)
        {
            Update();
        }

        private int beginCounter;
        private int endCounter;

        private void Update()
        {
            //            var canvas = sender as Canvas;
            if (Canvas == null) return;
            if (Math.Abs(Canvas.ActualWidth) < 0.00001) return;
            if (Math.Abs(Canvas.ActualHeight) < 0.00001) return;

            if (Visibility == Visibility.Hidden || Visibility == Visibility.Collapsed)
            {
                _Rect.Visibility = Visibility.Hidden;
                _contentBorder.Visibility = Visibility.Hidden;
                return;
            }

//            if ((Math.Abs(X) < 0.00001) && (Math.Abs(Y) < 0.00001))
//            {
//                _Rect.Visibility = Visibility.Hidden;
//                _contentBorder.Visibility = Visibility.Hidden;
//                return;
//            }

            _Rect.Visibility = Visibility.Visible;
            _contentBorder.Visibility = Visibility.Visible;

            if (ImageSource == null) 
                return;

            beginCounter++;
            Debug.WriteLine("Update Begin: " + beginCounter);

            // _StartPointRect
            _Rect.Width = Width >= 4 ? Width : 10;
            _Rect.Height = Height >= 4 ? Height : 10;
            _Rect.Stroke = Stroke;
            _Rect.StrokeThickness = StrokeThickness;
            var startPointDisplayCenterPoint = new Point(X, Y).GetDisplayPoint(Canvas, ImageSource);
            _Rect.MoveCenterPointInTheCanvas(startPointDisplayCenterPoint);
            _contentBorder.MovePopupInTheCanvas(_Rect, Canvas);

            endCounter++;
            Debug.WriteLine("Update End: " + endCounter);
        }

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("Common OnPropertyChangedCallback Begin");
            var me = s as IndicateRegionBehavior;
            me.Update();
            Debug.WriteLine("Common OnPropertyChangedCallback End");
        }

        #region X

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            "X", typeof(double), typeof(IndicateRegionBehavior), new PropertyMetadata(OnPropertyChangedCallback));

        #endregion

        #region Y

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y", typeof(double), typeof(IndicateRegionBehavior), new PropertyMetadata(OnPropertyChangedCallback));

        #endregion

        #region ImageSource

        public BitmapSource ImageSource
        {
            get { return (BitmapSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource", typeof(BitmapSource), typeof(IndicateRegionBehavior),
            new PropertyMetadata(OnPropertyChangedCallback));

        #endregion

        #region StrokeThickness

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness", typeof(double), typeof(IndicateRegionBehavior), new PropertyMetadata(2.0));

        #endregion

        #region Stroke

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke", typeof(Brush), typeof(IndicateRegionBehavior), new PropertyMetadata(Brushes.Red));

        #endregion

        #region Width

        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            "Width", typeof(double), typeof(IndicateRegionBehavior),
            new PropertyMetadata(48.0, OnPropertyChangedCallback));

        #endregion

        #region Height

        public double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register(
            "Height", typeof(double), typeof(IndicateRegionBehavior),
            new PropertyMetadata(48.0, OnPropertyChangedCallback));

        #endregion

        #region Content

        public UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(UIElement), typeof(IndicateRegionBehavior),
            new PropertyMetadata(OnContentChangedCallback));

        private static void OnContentChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("Content OnContentChangedCallback Begin");
            var me = s as IndicateRegionBehavior;
            me._contentBorder.Child = null;
            me._contentBorder.Child = e.NewValue as UIElement;
            me.Update();
            Debug.WriteLine("Content OnContentChangedCallback End");
        }

        #endregion

        #region Visibility

        public Visibility Visibility
        {
            get { return (Visibility)GetValue(VisibilityProperty); }
            set { SetValue(VisibilityProperty, value); }
        }

        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register(
            "Visibility", typeof(Visibility), typeof(IndicateRegionBehavior), new PropertyMetadata(OnPropertyChangedCallback));

        #endregion
    }
}