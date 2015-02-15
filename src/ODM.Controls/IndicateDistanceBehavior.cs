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
    public class IndicateDistanceBehavior : Behavior<Canvas>
    {
        private Rectangle _StartPointRect = new Rectangle();
        private Rectangle _EndPointRect = new Rectangle();
        private Border _contentBorder = new Border();
        private Line _line = new Line();

        private Canvas _canvas;

        protected override void OnAttached()
        {
            base.OnAttached();

            _canvas = AssociatedObject as Canvas;
            if (_canvas == null) return;

            _canvas.Children.Add(_StartPointRect);
            _canvas.Children.Add(_EndPointRect);
            _canvas.Children.Add(_contentBorder);
            _canvas.Children.Add(_line);

            _canvas.SizeChanged += (sender1, e1) => Update();

            _contentBorder.SizeChanged += (sender, e) => Update();
        }

        private int beginCounter;
        private int endCounter;

        private void Update()
        {
            //            var canvas = sender as Canvas;
            if (_canvas == null) return;
            if (Math.Abs(_canvas.ActualWidth) < 0.00001) return;
            if (Math.Abs(_canvas.ActualHeight) < 0.00001) return;

            if (Visibility == Visibility.Hidden || Visibility == Visibility.Collapsed)
            {
                _StartPointRect.Visibility = Visibility.Hidden;
                _EndPointRect.Visibility = Visibility.Hidden;
                _line.Visibility = Visibility.Hidden;
                _contentBorder.Visibility = Visibility.Hidden;
                return;
            }

            _StartPointRect.Visibility = Visibility.Visible;
            _EndPointRect.Visibility = Visibility.Visible;
            _line.Visibility = Visibility.Visible;
            _contentBorder.Visibility = Visibility.Visible;

            if (ImageSource == null) return;

            beginCounter++;
            Debug.WriteLine("Update Begin: " + beginCounter);

            // _StartPointRect
            _StartPointRect.Width = Width >= 4 ? Width : 10;
            _StartPointRect.Height = Height >= 4 ? Height : 10;
            _StartPointRect.Stroke = Stroke;
            _StartPointRect.StrokeThickness = StrokeThickness;
            var startPointDisplayCenterPoint = new Point(StartPointX, StartPointY).GetDisplayPoint(_canvas, ImageSource);
            _StartPointRect.MoveCenterPointInTheCanvas(startPointDisplayCenterPoint);
            _contentBorder.MovePopupInTheCanvas(_StartPointRect, _canvas);

            // _EndPointRect
            _EndPointRect.Width = Width >= 4 ? Width : 10;
            _EndPointRect.Height = Height >= 4 ? Height : 10;
            _EndPointRect.Stroke = Stroke;
            _EndPointRect.StrokeThickness = StrokeThickness;
            var endPointDisplayCenterPoint = new Point(EndPointX, EndPointY).GetDisplayPoint(_canvas, ImageSource);
            _EndPointRect.MoveCenterPointInTheCanvas(endPointDisplayCenterPoint);
            _contentBorder.MovePopupInTheCanvas(_EndPointRect, _canvas);

            // Line
            _line.Stroke = Stroke;
            _line.StrokeThickness = StrokeThickness;
            _line.X1 = startPointDisplayCenterPoint.X;
            _line.Y1 = startPointDisplayCenterPoint.Y;
            _line.X2 = endPointDisplayCenterPoint.X;
            _line.Y2 = endPointDisplayCenterPoint.Y;

            endCounter++;
            Debug.WriteLine("Update End: " + endCounter);
        }

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("Common OnPropertyChangedCallback Begin");
            var me = s as IndicateDistanceBehavior;
            me.Update();
            Debug.WriteLine("Common OnPropertyChangedCallback End");
        }

        #region X

        public double StartPointX
        {
            get { return (double)GetValue(StartPointXProperty); }
            set { SetValue(StartPointXProperty, value); }
        }

        public static readonly DependencyProperty StartPointXProperty = DependencyProperty.Register(
            "StartPointX", typeof(double), typeof(IndicateDistanceBehavior), new PropertyMetadata(OnPropertyChangedCallback));

        #endregion

        #region Y

        public double StartPointY
        {
            get { return (double)GetValue(StartPointYProperty); }
            set { SetValue(StartPointYProperty, value); }
        }

        public static readonly DependencyProperty StartPointYProperty = DependencyProperty.Register(
            "StartPointY", typeof(double), typeof(IndicateDistanceBehavior), new PropertyMetadata(OnPropertyChangedCallback));

        #endregion

        #region ImageSource

        public BitmapSource ImageSource
        {
            get { return (BitmapSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource", typeof(BitmapSource), typeof(IndicateDistanceBehavior),
            new PropertyMetadata(OnPropertyChangedCallback));

        #endregion

        #region StrokeThickness

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness", typeof(double), typeof(IndicateDistanceBehavior), new PropertyMetadata(2.0));

        #endregion

        #region Stroke

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke", typeof(Brush), typeof(IndicateDistanceBehavior), new PropertyMetadata(Brushes.Red));

        #endregion

        #region Width

        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            "Width", typeof(double), typeof(IndicateDistanceBehavior),
            new PropertyMetadata(48.0, OnPropertyChangedCallback));

        #endregion

        #region Height

        public double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register(
            "Height", typeof(double), typeof(IndicateDistanceBehavior),
            new PropertyMetadata(48.0, OnPropertyChangedCallback));

        #endregion

        #region Content

        public UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(UIElement), typeof(IndicateDistanceBehavior),
            new PropertyMetadata(OnContentChangedCallback));

        private static void OnContentChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("Content OnContentChangedCallback Begin");
            var me = s as IndicateDistanceBehavior;
            me._contentBorder.Child = null;
            me._contentBorder.Child = e.NewValue as UIElement;
            me.Update();
            Debug.WriteLine("Content OnContentChangedCallback End");
        }

        #endregion

        #region EndPointX

        public double EndPointX
        {
            get { return (double)GetValue(EndPointXProperty); }
            set { SetValue(EndPointXProperty, value); }
        }

        public static readonly DependencyProperty EndPointXProperty = DependencyProperty.Register(
            "EndPointX", typeof(double), typeof(IndicateDistanceBehavior),
            new PropertyMetadata(OnPropertyChangedCallback));

        #endregion

        #region EndPointY

        public double EndPointY
        {
            get { return (double)GetValue(EndPointYProperty); }
            set { SetValue(EndPointYProperty, value); }
        }

        public static readonly DependencyProperty EndPointYProperty = DependencyProperty.Register(
            "EndPointY", typeof(double), typeof(IndicateDistanceBehavior),
            new PropertyMetadata(OnPropertyChangedCallback));

        #endregion

        #region Visibility

        public Visibility Visibility
        {
            get { return (Visibility)GetValue(VisibilityProperty); }
            set { SetValue(VisibilityProperty, value); }
        }

        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register(
            "Visibility", typeof(Visibility), typeof(IndicateDistanceBehavior), new PropertyMetadata(OnPropertyChangedCallback));

        #endregion
    }
}