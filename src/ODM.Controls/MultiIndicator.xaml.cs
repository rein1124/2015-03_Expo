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
using Hdc.Collections;

namespace Hdc.Controls
{
    /// <summary>
    /// Interaction logic for MultiIndicator.xamlBitmapSource
    /// </summary>
    public partial class MultiIndicator : UserControl
    {
        private readonly List<Rectangle> _startPointRectangles = new List<Rectangle>();
        private readonly List<Rect> _startPointRects = new List<Rect>();
        private readonly List<Rectangle> _endPointRectangles = new List<Rectangle>();
        private readonly List<Rect> _endPointRects = new List<Rect>();
        private readonly List<Line> _measurementLines = new List<Line>();
        private readonly List<ContentControl> _measurementTips = new List<ContentControl>();
        private readonly List<Line> _measurementTipLines = new List<Line>();
        private const double MeasurementTipLineLength = 48.0;

        private readonly List<Rectangle> _defectRectangles = new List<Rectangle>();
        private readonly List<Rect> _defectRects = new List<Rect>();
        private readonly List<ContentControl> _defectTips = new List<ContentControl>();
        private readonly List<Line> _defectTipLines = new List<Line>();
        private const double DefectTipLineLength = 24.0;

        public static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public MultiIndicator()
        {
            InitializeComponent();

            InteractionPanel.PreviewMouseDown += (s, e) => ZoomImageViewer.BeginDrag(e);
            InteractionPanel.PreviewMouseMove += (s, e) => ZoomImageViewer.Drag(e);
            InteractionPanel.PreviewMouseUp += (s, e) => ZoomImageViewer.EndDrag();
            InteractionPanel.PreviewMouseWheel += (s, e) => ZoomImageViewer.Zoom(e);
            InteractionPanel.PreviewMouseDoubleClick += (s, e) => ZoomImageViewer.ZoomIn();

            SizeChanged += MultiIndicator_SizeChanged;

            BindingOperations.SetBinding(this, XProperty,
                new Binding("X") { Source = ZoomImageViewer, Mode = BindingMode.TwoWay });
            BindingOperations.SetBinding(this, YProperty,
                new Binding("Y") { Source = ZoomImageViewer, Mode = BindingMode.TwoWay });
            BindingOperations.SetBinding(this, ScaleProperty,
                new Binding("Scale") { Source = ZoomImageViewer, Mode = BindingMode.TwoWay });
            //            BindingOperations.SetBinding(this, BitmapSourceProperty,
            //                new Binding("BitmapSource") {Source = ZoomImageViewer, Mode = BindingMode.TwoWay});
        }

        private void MultiIndicator_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePositions();
        }


        private void UpdateMeasurements(IEnumerable enumerable)
        {
            if (enumerable == null)
                return;

            for (int i = 0; i < _startPointRectangles.Count; i++)
            {
                var startPointRectangle = _startPointRectangles[i];
                var endPointRectangle = _endPointRectangles[i];
                var measurementLine = _measurementLines[i];
                var measurementTip = _measurementTips[i];
                var measurementTipLine = _measurementTipLines[i];

                Canvas.Children.Remove(startPointRectangle);
                Canvas.Children.Remove(endPointRectangle);
                Canvas.Children.Remove(measurementLine);
                Canvas.Children.Remove(measurementTip);
                Canvas.Children.Remove(measurementTipLine);
            }

            _startPointRectangles.Clear();
            _startPointRects.Clear();
            _endPointRectangles.Clear();
            _endPointRects.Clear();
            _measurementLines.Clear();
            _measurementTips.Clear();
            _measurementTipLines.Clear();

            foreach (var element in enumerable)
            {
                //
                var startX = GetPropertyValue(element, MeasurementStartPointXPath);
                var startY = GetPropertyValue(element, MeasurementStartPointYPath);
                var startRect = new Rect((double)startX, (double)startY, 16, 16);
                _startPointRects.Add(startRect);
                var startRectangle = new Rectangle()
                {
                    Stroke = MeasurementStroke,
                    StrokeThickness = MeasurementStrokeThickness
                };
                _startPointRectangles.Add(startRectangle);
                Canvas.Children.Add(startRectangle);

                //
                var endX = GetPropertyValue(element, MeasurementEndPointXPath);
                var endY = GetPropertyValue(element, MeasurementEndPointYPath);
                var endRect = new Rect((double)endX, (double)endY, 16, 16);
                _endPointRects.Add(endRect);
                var endRectangle = new Rectangle()
                {
                    Stroke = MeasurementStroke,
                    StrokeThickness = MeasurementStrokeThickness
                };
                _endPointRectangles.Add(endRectangle);
                Canvas.Children.Add(endRectangle);

                //
                var measurementLine = new Line()
                {
                    Stroke = MeasurementStroke,
                    StrokeThickness = MeasurementStrokeThickness
                };
                _measurementLines.Add(measurementLine);
                Canvas.Children.Add(measurementLine);

                //
                var measurementTipLine = new Line()
                {
                    Stroke = MeasurementStroke,
                    StrokeThickness = MeasurementStrokeThickness,
                    StrokeDashArray = new DoubleCollection(new double[] { 2, 2 })
                };
                _measurementTipLines.Add(measurementTipLine);
                Canvas.Children.Add(measurementTipLine);

                //
                var measurementTip = new ContentControl
                {
                    ContentTemplate = MeasurementItemTemplate,
                    Content = element
                };
                _measurementTips.Add(measurementTip);
                Canvas.Children.Add(measurementTip);
            }

            UpdatePositions();
        }


        private void UpdateDefects(IEnumerable enumerable)
        {
            for (int i = 0; i < _defectRectangles.Count; i++)
            {
                var defectRectangle = _defectRectangles[i];
                var defectContentControl = _defectTips[i];
                var defectLine = _defectTipLines[i];
                Canvas.Children.Remove(defectRectangle);
                Canvas.Children.Remove(defectContentControl);
                Canvas.Children.Remove(defectLine);
            }

            _defectRectangles.Clear();
            _defectRects.Clear();
            _defectTips.Clear();
            _defectTipLines.Clear();

            if (enumerable == null)
                return;

            foreach (var element in enumerable)
            {
                var x = GetPropertyValue(element, DefectXPath);
                var y = GetPropertyValue(element, DefectYPath);
                var w = GetPropertyValue(element, DefectWidthPath);
                var h = GetPropertyValue(element, DefectHeightPath);

                //
                var defectRect = new Rect((double)x, (double)y, (double)w, (double)h);
                _defectRects.Add(defectRect);

                //
                var defectRectangle = new Rectangle()
                {
                    Stroke = DefectStroke,
                    StrokeThickness = DefectStrokeThickness
                };
                _defectRectangles.Add(defectRectangle);
                Canvas.Children.Add(defectRectangle);

                //
                var defectTip = new ContentControl
                {
                    ContentTemplate = DefectItemTemplate,
                    Content = element
                };
                _defectTips.Add(defectTip);
                Canvas.Children.Add(defectTip);

                //
                var defectLine = new Line()
                {
                    Stroke = DefectStroke,
                    StrokeThickness = DefectStrokeThickness,
                    StrokeDashArray = new DoubleCollection(new double[] { 2, 2 })
                };
                _defectTipLines.Add(defectLine);
                Canvas.Children.Add(defectLine);
            }

            UpdatePositions();
        }

        private static void OnChangedCallback_UpdatePositions(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as MultiIndicator;
            me.UpdatePositions();
        }

        private void UpdatePositions()
        {
            UpdatePositions_Measurements();

            UpdatePositions_Defects();
        }

        private void UpdatePositions_Measurements()
        {
            if (MeasurementItemsSource == null)
                return;

            var elementList = MeasurementItemsSource.ToObjectList();

            for (int i = 0; i < _startPointRectangles.Count; i++)
            {
                var element = elementList[i];
                var startPointRectangle = _startPointRectangles[i];
                var endPointRectangle = _endPointRectangles[i];
                var line = _measurementLines[i];
                var measurementTip = _measurementTips[i];
                var measurementTipLine = _measurementTipLines[i];

                if (!IsMeasurementEnabled)
                {
                    startPointRectangle.Visibility = Visibility.Hidden;
                    endPointRectangle.Visibility = Visibility.Hidden;
                    line.Visibility = Visibility.Hidden;
                    measurementTip.Visibility = Visibility.Hidden;
                    measurementTipLine.Visibility = Visibility.Hidden;
                    continue;
                }
                else
                {
                    startPointRectangle.Visibility = Visibility.Visible;
                    endPointRectangle.Visibility = Visibility.Visible;
                    line.Visibility = Visibility.Visible;
                    measurementTip.Visibility = Visibility.Visible;
                    measurementTipLine.Visibility = Visibility.Visible;

                    if (!ShowDefectTip)
                    {
                        measurementTip.Visibility = Visibility.Hidden;
                        measurementTipLine.Visibility = Visibility.Hidden;
                    }
                }


                var startPointRect = _startPointRects[i];
                double spX = X + startPointRect.X * Scale;
                double spY = Y + startPointRect.Y * Scale;
                startPointRectangle.SetValue(Canvas.LeftProperty, spX - 8);
                startPointRectangle.SetValue(Canvas.TopProperty, spY - 8);
                startPointRectangle.Width = startPointRect.Width; //*Scale;
                startPointRectangle.Height = startPointRect.Height; //*Scale;

                var endPointRect = _endPointRects[i];
                double epX = X + endPointRect.X * Scale;
                double epY = Y + endPointRect.Y * Scale;
                endPointRectangle.SetValue(Canvas.LeftProperty, epX - 8);
                endPointRectangle.SetValue(Canvas.TopProperty, epY - 8);
                endPointRectangle.Width = endPointRect.Width; //*Scale;
                endPointRectangle.Height = endPointRect.Height; //*Scale;

                //
                line.X1 = spX;
                line.Y1 = spY;
                line.X2 = epX;
                line.Y2 = epY;


                //
                //                var targetX = spX > epX ? spX : epX;
                //                var targetY = spX > epX ? spY : epY;
                var targetX = (spX + epX) / 2;
                var targetY = (spY + epY) / 2;
                var width = Math.Abs(spX - epX);
                var height = Math.Abs(spY - epY);

                measurementTip.ContentTemplate = MeasurementItemTemplate;
                double cx;
                if (targetX + measurementTip.ActualWidth + MeasurementTipLineLength > Canvas.ActualWidth)
                {
                    // left
                    cx = targetX - measurementTip.ActualWidth - MeasurementTipLineLength;
                    measurementTipLine.X1 = targetX;
                    measurementTipLine.X2 = targetX - MeasurementTipLineLength;
                }
                else
                {
                    // right
                    cx = targetX + MeasurementTipLineLength;
                    measurementTipLine.X1 = targetX;
                    measurementTipLine.X2 = cx;
                }
                double cy;
                if (targetY + measurementTip.ActualHeight + MeasurementTipLineLength > Canvas.ActualHeight)
                {
                    // top
                    cy = targetY - measurementTip.ActualHeight - MeasurementTipLineLength;
                    measurementTipLine.Y1 = targetY;
                    measurementTipLine.Y2 = targetY - MeasurementTipLineLength;
                }
                else
                {
                    // bottom
                    cy = targetY + MeasurementTipLineLength;
                    measurementTipLine.Y1 = targetY;
                    measurementTipLine.Y2 = cy;
                }
                measurementTip.SetValue(Canvas.LeftProperty, cx);
                measurementTip.SetValue(Canvas.TopProperty, cy);

                //
                if (!DisplayAllMeasurements && !Equals(element, SelectedMeasurementItem))
                {
                    startPointRectangle.Visibility = Visibility.Hidden;
                    endPointRectangle.Visibility = Visibility.Hidden;
                    line.Visibility = Visibility.Hidden;
                    measurementTip.Visibility = Visibility.Hidden;
                    measurementTipLine.Visibility = Visibility.Hidden;
                    continue;
                }
            }
        }

        private void UpdatePositions_Defects()
        {
            if (DefectItemsSource == null)
                return;

            var elementList = DefectItemsSource.ToObjectList();

            for (int i = 0; i < _defectRectangles.Count; i++)
            {
                var element = elementList[i];
                var defectRectangle = _defectRectangles[i];
                var defectContentControl = _defectTips[i];
                var defectLine = _defectTipLines[i];

                if (!IsDefectEnabled)
                {
                    defectRectangle.Visibility = Visibility.Hidden;
                    defectContentControl.Visibility = Visibility.Hidden;
                    defectLine.Visibility = Visibility.Hidden;
                    continue;
                }
                else
                {
                    defectRectangle.Visibility = Visibility.Visible;
                    defectContentControl.Visibility = Visibility.Visible;
                    defectLine.Visibility = Visibility.Visible;

                    if (!ShowDefectTip)
                    {
                        defectContentControl.Visibility = Visibility.Hidden;
                        defectLine.Visibility = Visibility.Hidden;
                    }
                }

                //
                var defectRect = _defectRects[i];
                double x = X + defectRect.X * Scale;
                double y = Y + defectRect.Y * Scale;
                defectRectangle.SetValue(Canvas.LeftProperty, x);
                defectRectangle.SetValue(Canvas.TopProperty, y);
                double width = defectRect.Width * Scale;
                double height = defectRect.Height * Scale;
                defectRectangle.Width = width;
                defectRectangle.Height = height;

                //
                defectContentControl.ContentTemplate = DefectItemTemplate;
                double cx;
                if (x + width + defectContentControl.ActualWidth + DefectTipLineLength > Canvas.ActualWidth)
                {
                    // left
                    cx = x - defectContentControl.ActualWidth - DefectTipLineLength;
                    defectLine.X1 = x;
                    defectLine.X2 = x - DefectTipLineLength;
                }
                else
                {
                    // right
                    cx = x + width + DefectTipLineLength;
                    defectLine.X1 = x + width;
                    defectLine.X2 = cx;
                }
                double cy;
                if (y + height + defectContentControl.ActualHeight + DefectTipLineLength > Canvas.ActualHeight)
                {
                    // top
                    cy = y - defectContentControl.ActualHeight - DefectTipLineLength;
                    defectLine.Y1 = y;
                    defectLine.Y2 = y - DefectTipLineLength;
                }
                else
                {
                    // bottom
                    cy = y + height + DefectTipLineLength;
                    defectLine.Y1 = y + height;
                    defectLine.Y2 = cy;
                }
                defectContentControl.SetValue(Canvas.LeftProperty, cx);
                defectContentControl.SetValue(Canvas.TopProperty, cy);

                //
                if (!DisplayAllDefects && !Equals(element, SelectedDefectItem))
                {
                    defectRectangle.Visibility = Visibility.Hidden;
                    defectContentControl.Visibility = Visibility.Hidden;
                    defectLine.Visibility = Visibility.Hidden;
                    continue;
                }
            }
        }

        #region DefectItemsSource

        public IEnumerable DefectItemsSource
        {
            get { return (IEnumerable)GetValue(DefectItemsSourceProperty); }
            set { SetValue(DefectItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty DefectItemsSourceProperty = DependencyProperty.Register(
            "DefectItemsSource", typeof(IEnumerable), typeof(MultiIndicator),
            new PropertyMetadata(OnDefectItemsSourcePropertyChangedCallback));

        private static void OnDefectItemsSourcePropertyChangedCallback(DependencyObject s,
            DependencyPropertyChangedEventArgs e)
        {
            var me = s as MultiIndicator;

            me.UpdateDefects(e.NewValue as IEnumerable);

            //
            var ds = e.NewValue as INotifyCollectionChanged;
            NotifyCollectionChangedEventHandler dsOnCollectionChanged =
                (sender, e1) => me.UpdateDefects(ds as IEnumerable);
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

        #region MeasurementItemsSource

        public IEnumerable MeasurementItemsSource
        {
            get { return (IEnumerable)GetValue(MeasurementItemsSourceProperty); }
            set { SetValue(MeasurementItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty MeasurementItemsSourceProperty = DependencyProperty.Register(
            "MeasurementItemsSource", typeof(IEnumerable), typeof(MultiIndicator),
            new PropertyMetadata(OnMeasurementItemsSourcePropertyChangedCallback));

        private static void OnMeasurementItemsSourcePropertyChangedCallback(DependencyObject s,
            DependencyPropertyChangedEventArgs e)
        {
            var me = s as MultiIndicator;
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

        #region MeasurementStartPointXPath

        public string MeasurementStartPointXPath
        {
            get { return (string)GetValue(MeasurementStartPointXPathProperty); }
            set { SetValue(MeasurementStartPointXPathProperty, value); }
        }

        public static readonly DependencyProperty MeasurementStartPointXPathProperty = DependencyProperty.Register(
            "MeasurementStartPointXPath", typeof(string), typeof(MultiIndicator));

        #endregion

        #region MeasurementStartPointYPath

        public string MeasurementStartPointYPath
        {
            get { return (string)GetValue(MeasurementStartPointYPathProperty); }
            set { SetValue(MeasurementStartPointYPathProperty, value); }
        }

        public static readonly DependencyProperty MeasurementStartPointYPathProperty = DependencyProperty.Register(
            "MeasurementStartPointYPath", typeof(string), typeof(MultiIndicator));

        #endregion

        #region MeasurementEndPointXPath

        public string MeasurementEndPointXPath
        {
            get { return (string)GetValue(MeasurementEndPointXPathProperty); }
            set { SetValue(MeasurementEndPointXPathProperty, value); }
        }

        public static readonly DependencyProperty MeasurementEndPointXPathProperty = DependencyProperty.Register(
            "MeasurementEndPointXPath", typeof(string), typeof(MultiIndicator));

        #endregion

        #region MeasurementEndPointYPath

        public string MeasurementEndPointYPath
        {
            get { return (string)GetValue(MeasurementEndPointYPathProperty); }
            set { SetValue(MeasurementEndPointYPathProperty, value); }
        }

        public static readonly DependencyProperty MeasurementEndPointYPathProperty = DependencyProperty.Register(
            "MeasurementEndPointYPath", typeof(string), typeof(MultiIndicator));

        #endregion


        #region X

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            "X", typeof(double), typeof(MultiIndicator), new PropertyMetadata(OnChangedCallback_UpdatePositions));

        #endregion

        #region Y

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y", typeof(double), typeof(MultiIndicator), new PropertyMetadata(OnChangedCallback_UpdatePositions));

        #endregion

        #region Scale

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof(double), typeof(MultiIndicator), new PropertyMetadata(OnChangedCallback_UpdatePositions));

        #endregion

        #region DefectXPath

        public string DefectXPath
        {
            get { return (string)GetValue(DefectXPathProperty); }
            set { SetValue(DefectXPathProperty, value); }
        }

        public static readonly DependencyProperty DefectXPathProperty = DependencyProperty.Register(
            "DefectXPath", typeof(string), typeof(MultiIndicator));

        #endregion

        #region DefectYPath

        public string DefectYPath
        {
            get { return (string)GetValue(DefectYPathProperty); }
            set { SetValue(DefectYPathProperty, value); }
        }

        public static readonly DependencyProperty DefectYPathProperty = DependencyProperty.Register(
            "DefectYPath", typeof(string), typeof(MultiIndicator));

        #endregion

        #region DefectWidthPath

        public string DefectWidthPath
        {
            get { return (string)GetValue(DefectWidthPathProperty); }
            set { SetValue(DefectWidthPathProperty, value); }
        }

        public static readonly DependencyProperty DefectWidthPathProperty = DependencyProperty.Register(
            "DefectWidthPath", typeof(string), typeof(MultiIndicator));

        #endregion

        #region DefectHeightPath

        public string DefectHeightPath
        {
            get { return (string)GetValue(DefectHeightPathProperty); }
            set { SetValue(DefectHeightPathProperty, value); }
        }

        public static readonly DependencyProperty DefectHeightPathProperty = DependencyProperty.Register(
            "DefectHeightPath", typeof(string), typeof(MultiIndicator));

        #endregion

        #region BitmapSource

        public BitmapSource BitmapSource
        {
            get { return (BitmapSource)GetValue(BitmapSourceProperty); }
            set { SetValue(BitmapSourceProperty, value); }
        }

        public static readonly DependencyProperty BitmapSourceProperty = DependencyProperty.Register(
            "BitmapSource", typeof(BitmapSource), typeof(MultiIndicator),
            new PropertyMetadata(OnBitmapSourcePropertyChangedCallback));

        private static void OnBitmapSourcePropertyChangedCallback(DependencyObject s,
            DependencyPropertyChangedEventArgs e)
        {
            var me = s as MultiIndicator;
            me.ZoomImageViewer.BitmapSource = e.NewValue as BitmapSource;
            if (me.AutoZoomFitWhenBitmapSourceChanged)
                me.ZoomFit();
        }

        #endregion

        public void ZoomIn()
        {
            ZoomImageViewer.ZoomIn();
        }

        public void ZoomOut()
        {
            ZoomImageViewer.ZoomOut();
        }

        public void ZoomFit()
        {
            ZoomImageViewer.ZoomFit();
        }

        public void ZoomActual()
        {
            ZoomImageViewer.ZoomActual();
        }

        public void ZoomFitDiplayArea()
        {
            if (DisplayAreaElement == null)
                return;

            var myPosition = this.PointToScreen(new Point());
            var myWidth = this.ActualWidth;
            var myHeight = this.ActualHeight;
            var myCenter = new Point(myPosition.X + myWidth / 2, myPosition.Y + myHeight / 2);

            var areaPosition = DisplayAreaElement.PointFromScreen(new Point());
            var areaWidth = DisplayAreaElement.ActualWidth;
            var areaHeight = DisplayAreaElement.ActualHeight;
            var areaCenter = new Point(areaPosition.X + areaWidth / 2, areaPosition.Y + areaHeight / 2);

            var bmWidth = BitmapSource.PixelWidth;
            var bmHeight = BitmapSource.PixelHeight;

            //            double ratio = areaWidth / areaHeight > myWidth / myHeight ? areaWidth / myWidth : areaHeight / myHeight;
            double ratio = areaWidth / areaHeight > bmWidth / bmHeight ? areaWidth / bmWidth : areaHeight / bmHeight;

            var newWidth = bmWidth * ratio;
            var newHeight = bmHeight * ratio;
            var newPosition = new Point(myPosition.X, myPosition.Y);
            var newCenter = new Point(newPosition.X + (newWidth / 2), newPosition.Y + (newHeight / 2));

            //            var offsetVector = newPosition - myPosition;
            //            var offsetVector = areaPosition - myPosition;
            var offsetVector = areaCenter - newCenter;

            //            var offsetX = (areaPosition.X - myPosition.X) / 2;
            //            var offsetY = (areaPosition.Y - myPosition.Y) / 2;
            Scale = ratio;
            X = offsetVector.X;
            Y = offsetVector.Y;
        }

        #region MeasurementStroke

        public Brush MeasurementStroke
        {
            get { return (Brush)GetValue(MeasurementStrokeProperty); }
            set { SetValue(MeasurementStrokeProperty, value); }
        }

        public static readonly DependencyProperty MeasurementStrokeProperty = DependencyProperty.Register(
            "MeasurementStroke", typeof(Brush), typeof(MultiIndicator), new PropertyMetadata(Brushes.DeepSkyBlue));

        #endregion

        #region MeasurementStrokeThickness

        public double MeasurementStrokeThickness
        {
            get { return (double)GetValue(MeasurementStrokeThicknessProperty); }
            set { SetValue(MeasurementStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty MeasurementStrokeThicknessProperty = DependencyProperty.Register(
            "MeasurementStrokeThickness", typeof(double), typeof(MultiIndicator), new PropertyMetadata(2.0));

        #endregion

        #region DefectStroke

        public Brush DefectStroke
        {
            get { return (Brush)GetValue(DefectStrokeProperty); }
            set { SetValue(DefectStrokeProperty, value); }
        }

        public static readonly DependencyProperty DefectStrokeProperty = DependencyProperty.Register(
            "DefectStroke", typeof(Brush), typeof(MultiIndicator), new PropertyMetadata(Brushes.Red));

        #endregion

        #region DefectStrokeThickness

        public double DefectStrokeThickness
        {
            get { return (double)GetValue(DefectStrokeThicknessProperty); }
            set { SetValue(DefectStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty DefectStrokeThicknessProperty = DependencyProperty.Register(
            "DefectStrokeThickness", typeof(double), typeof(MultiIndicator), new PropertyMetadata(2.0));

        #endregion

        #region IsDefectEnabled

        public bool IsDefectEnabled
        {
            get { return (bool)GetValue(IsDefectEnabledProperty); }
            set { SetValue(IsDefectEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsDefectEnabledProperty = DependencyProperty.Register(
            "IsDefectEnabled", typeof(bool), typeof(MultiIndicator),
            new PropertyMetadata(true, OnChangedCallback_UpdatePositions));

        #endregion

        #region IsMeasurementEnabled

        public bool IsMeasurementEnabled
        {
            get { return (bool)GetValue(IsMeasurementEnabledProperty); }
            set { SetValue(IsMeasurementEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsMeasurementEnabledProperty = DependencyProperty.Register(
            "IsMeasurementEnabled", typeof(bool), typeof(MultiIndicator),
            new PropertyMetadata(true, OnChangedCallback_UpdatePositions));

        #endregion

        #region AutoZoomFitWhenBitmapSourceChanged

        public bool AutoZoomFitWhenBitmapSourceChanged
        {
            get { return (bool)GetValue(AutoZoomFitWhenBitmapSourceChangedProperty); }
            set { SetValue(AutoZoomFitWhenBitmapSourceChangedProperty, value); }
        }

        public static readonly DependencyProperty AutoZoomFitWhenBitmapSourceChangedProperty = DependencyProperty
            .Register(
                "AutoZoomFitWhenBitmapSourceChanged", typeof(bool), typeof(MultiIndicator), new PropertyMetadata(true));

        #endregion

        #region MeasurementItemTemplate

        public DataTemplate MeasurementItemTemplate
        {
            get { return (DataTemplate)GetValue(MeasurementItemTemplateProperty); }
            set { SetValue(MeasurementItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty MeasurementItemTemplateProperty = DependencyProperty.Register(
            "MeasurementItemTemplate", typeof(DataTemplate), typeof(MultiIndicator),
            new PropertyMetadata(OnMeasurementItemTemplatePropertyChangedCallback));

        private static void OnMeasurementItemTemplatePropertyChangedCallback(DependencyObject s,
            DependencyPropertyChangedEventArgs e)
        {
            var me = s as MultiIndicator;
            me.UpdateMeasurements(e.NewValue as IEnumerable);
        }

        #endregion

        #region DefectItemTemplate

        public DataTemplate DefectItemTemplate
        {
            get { return (DataTemplate)GetValue(DefectItemTemplateProperty); }
            set { SetValue(DefectItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty DefectItemTemplateProperty = DependencyProperty.Register(
            "DefectItemTemplate", typeof(DataTemplate), typeof(MultiIndicator),
            new PropertyMetadata(OnDefectItemTemplatePropertyChangedCallback));

        private static void OnDefectItemTemplatePropertyChangedCallback(DependencyObject s,
            DependencyPropertyChangedEventArgs e)
        {
            var me = s as MultiIndicator;
            me.UpdateDefects(e.NewValue as IEnumerable);
        }

        #endregion

        #region ShowDefectTip

        public bool ShowDefectTip
        {
            get { return (bool)GetValue(ShowDefectTipProperty); }
            set { SetValue(ShowDefectTipProperty, value); }
        }

        public static readonly DependencyProperty ShowDefectTipProperty = DependencyProperty.Register(
            "ShowDefectTip", typeof(bool), typeof(MultiIndicator), new PropertyMetadata(true));

        #endregion

        #region ShowMeasurementTip

        public bool ShowMeasurementTip
        {
            get { return (bool)GetValue(ShowMeasurementTipProperty); }
            set { SetValue(ShowMeasurementTipProperty, value); }
        }

        public static readonly DependencyProperty ShowMeasurementTipProperty = DependencyProperty.Register(
            "ShowMeasurementTip", typeof(bool), typeof(MultiIndicator), new PropertyMetadata(true));

        #endregion

        #region ClickCommand

        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register(
            "ClickCommand", typeof(ICommand), typeof(MultiIndicator));

        #endregion

        #region ClickCommandParameter

        public object ClickCommandParameter
        {
            get { return (object)GetValue(ClickCommandParameterProperty); }
            set { SetValue(ClickCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandParameterProperty = DependencyProperty.Register(
            "ClickCommandParameter", typeof(object), typeof(MultiIndicator));

        #endregion

        #region SelectedDefectItem

        public object SelectedDefectItem
        {
            get { return (object)GetValue(SelectedDefectItemProperty); }
            set { SetValue(SelectedDefectItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedDefectItemProperty = DependencyProperty.Register(
            "SelectedDefectItem", typeof(object), typeof(MultiIndicator), new PropertyMetadata(true, OnChangedCallback_UpdatePositions));

        #endregion

        #region DisplayAllDefects

        public bool DisplayAllDefects
        {
            get { return (bool)GetValue(DisplayAllDefectsProperty); }
            set { SetValue(DisplayAllDefectsProperty, value); }
        }

        public static readonly DependencyProperty DisplayAllDefectsProperty = DependencyProperty.Register(
            "DisplayAllDefects", typeof(bool), typeof(MultiIndicator), new PropertyMetadata(OnChangedCallback_UpdatePositions));

        #endregion

        #region DisplayAllMeasurements

        public bool DisplayAllMeasurements
        {
            get { return (bool)GetValue(DisplayAllMeasurementsProperty); }
            set { SetValue(DisplayAllMeasurementsProperty, value); }
        }

        public static readonly DependencyProperty DisplayAllMeasurementsProperty = DependencyProperty.Register(
            "DisplayAllMeasurements", typeof(bool), typeof(MultiIndicator), new PropertyMetadata(true, OnChangedCallback_UpdatePositions));

        #endregion

        #region SelectedMeasurementItem

        public object SelectedMeasurementItem
        {
            get { return (object)GetValue(SelectedMeasurementItemProperty); }
            set { SetValue(SelectedMeasurementItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedMeasurementItemProperty = DependencyProperty.Register(
            "SelectedMeasurementItem", typeof(object), typeof(MultiIndicator), new PropertyMetadata(OnChangedCallback_UpdatePositions));

        #endregion


        #region DisplayAreaElement

        public FrameworkElement DisplayAreaElement
        {
            get { return (FrameworkElement)GetValue(DisplayAreaElementProperty); }
            set { SetValue(DisplayAreaElementProperty, value); }
        }

        public static readonly DependencyProperty DisplayAreaElementProperty = DependencyProperty.Register(
            "DisplayAreaElement", typeof(FrameworkElement), typeof(MultiIndicator));

        #endregion
    }
}