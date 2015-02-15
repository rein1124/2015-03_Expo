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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hdc.Windows;

namespace Hdc.Controls
{
    /// <summary>
    /// Interaction logic for CroppedImage.xaml
    /// </summary>
    public partial class CroppedImage : UserControl
    {
        private WriteableBitmap _writeableBmp;

        public static RoutedCommand MoveCroppedRegionUpCommand = new RoutedCommand();
        public static RoutedCommand MoveCroppedRegionDownCommand = new RoutedCommand();
        public static RoutedCommand MoveCroppedRegionLeftCommand = new RoutedCommand();
        public static RoutedCommand MoveCroppedRegionRightCommand = new RoutedCommand();
        public static RoutedCommand ResetCroppedRegionRectCommand = new RoutedCommand();
        public static RoutedCommand ZoomInCommand = new RoutedCommand();
        public static RoutedCommand ZoomOutCommand = new RoutedCommand();

        public CroppedImage()
        {
            InitializeComponent();

            CommandBindings.AddRange(
                new[]
                    {
                        new CommandBinding(MoveCroppedRegionUpCommand, MoveCroppedRegionUpCommandExecuted,
                                           MoveCroppedRegionUpCommandCanExecute),
                        new CommandBinding(MoveCroppedRegionDownCommand, MoveCroppedRegionDownCommandExecuted,
                                           MoveCroppedRegionDownCommandCanExecute),
                        new CommandBinding(MoveCroppedRegionLeftCommand, MoveCroppedRegionLeftCommandExecuted,
                                           MoveCroppedRegionLeftCommandCanExecute),
                        new CommandBinding(MoveCroppedRegionRightCommand, MoveCroppedRegionRightCommandExecuted,
                                           MoveCroppedRegionRightCommandCanExecute),
                        new CommandBinding(ResetCroppedRegionRectCommand, ResetCroppedRegionRectCommandExecuted,
                                           ResetCroppedRegionRectCommandCanExecute),
                        new CommandBinding(ZoomInCommand, ZoomInCommandExecuted,
                                           ZoomInCommandCanExecute),
                        new CommandBinding(ZoomOutCommand, ZoomOutCommandExecuted,
                                           ZoomOutCommandCanExecute),
                    });

            Root.PreviewMouseLeftButtonDown += Root_PreviewMouseLeftButtonDown;
            Root.DragOver += Root_DragOver;
            Root.Drop += Root_Drop;
        }

        private void Root_Drop(object sender, DragEventArgs e)
        {
        }

        private void Root_DragOver(object sender, DragEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(this);
            Vector diff = mousePos - lastPoint;
            lastPoint = mousePos;

            var ratio = Source.Width/OriginalImageControl.ActualWidth;

            MoveCroppedImage(diff.X*ratio, diff.Y*ratio);
        }

        private Point lastPoint;

        private void Root_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lastPoint = e.GetPosition(this);
            DragDrop.DoDragDrop(this, new object(), DragDropEffects.Move);
        }

        private void ZoomOutCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ActualZoom++;

            if (ActualZoom > MaxZoom)
            {
                ActualZoom = MaxZoom;
            }

            UpdateCroppedImage();
        }

        private void ZoomOutCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ZoomInCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ActualZoom--;

            if (ActualZoom < MinZoom)
            {
                ActualZoom = MinZoom;
            }

            UpdateCroppedImage();
        }

        private void ZoomInCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ResetCroppedRegionRectCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ResetCroppedRect();
        }

        private void ResetCroppedRegionRectCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MoveCroppedRegionRightCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MoveCroppedImage(1, 0);
        }

        private void MoveCroppedRegionRightCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MoveCroppedRegionLeftCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MoveCroppedImage(-1, 0);
        }

        private void MoveCroppedRegionLeftCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MoveCroppedRegionDownCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MoveCroppedImage(0, 1);
        }

        private void MoveCroppedRegionDownCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MoveCroppedRegionUpCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MoveCroppedImage(0, -1);
        }

        private void MoveCroppedRegionUpCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #region CroppedRegionStroke

        public Brush CroppedRegionStroke
        {
            get { return (Brush) GetValue(CroppedRegionStrokeProperty); }
            set { SetValue(CroppedRegionStrokeProperty, value); }
        }

        public static readonly DependencyProperty CroppedRegionStrokeProperty = DependencyProperty.Register(
            "CroppedRegionStroke", typeof (Brush), typeof (CroppedImage), new PropertyMetadata(Brushes.Red));

        #endregion

        #region CroppedRegionStrokeThickness

        public double CroppedRegionStrokeThickness
        {
            get { return (double) GetValue(CroppedRegionStrokeThicknessProperty); }
            set { SetValue(CroppedRegionStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty CroppedRegionStrokeThicknessProperty = DependencyProperty.Register(
            "CroppedRegionStrokeThickness", typeof (double), typeof (CroppedImage), new PropertyMetadata(2.0));

        #endregion

        #region CroppedRegionRect

        public Rect CroppedRegionRect
        {
            get { return (Rect) GetValue(CroppedRegionRectProperty); }
            set { SetValue(CroppedRegionRectProperty, value); }
        }

        public static readonly DependencyProperty CroppedRegionRectProperty = DependencyProperty.Register(
            "CroppedRegionRect", typeof (Rect), typeof (CroppedImage),
            new PropertyMetadata(Rect.Empty, (s, e) =>
                                                 {
                                                     var me = s as CroppedImage;
                                                     if (me != null)
                                                         me.Init();
                                                 }));

        #endregion

        #region ActualCroppedRegionRect

        public Rect ActualCroppedRegionRect
        {
            get { return (Rect) GetValue(ActualCroppedRegionRectProperty); }
            private set { SetValue(ActualCroppedRegionRectProperty, value); }
        }

        public static readonly DependencyProperty ActualCroppedRegionRectProperty = DependencyProperty.Register(
            "ActualCroppedRegionRect", typeof (Rect), typeof (CroppedImage), new PropertyMetadata(Rect.Empty));

        #endregion

        #region Zoom

        public int Zoom
        {
            get { return (int) GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(
            "Zoom", typeof (int), typeof (CroppedImage),
            new PropertyMetadata(0, (s, e) =>
                                        {
                                            var me = s as CroppedImage;
                                            if (me != null)
                                                me.Init();
                                        }));

        #endregion

        #region CroppedBitmapSource

        public BitmapSource CroppedBitmapSource
        {
            get { return (BitmapSource) GetValue(CroppedBitmapSourceProperty); }
            private set { SetValue(CroppedBitmapSourceProperty, value); }
        }

        public static readonly DependencyProperty CroppedBitmapSourceProperty = DependencyProperty.Register(
            "CroppedBitmapSource", typeof (BitmapSource), typeof (CroppedImage));

        #endregion

        #region Source

        public BitmapSource Source
        {
            get { return (BitmapSource) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof (BitmapSource), typeof (CroppedImage),
            new PropertyMetadata((s, e) =>
                                     {
                                         var me = s as CroppedImage;
                                         if (me != null)
                                             me.Init();
                                     }));

        #endregion

        private void Init()
        {
            if (Source == null)
                return;

            var originalBitmapSource = Source;
            OriginalImageControl.Stretch = Stretch.Uniform;

            _writeableBmp = BitmapFactory.ConvertToPbgra32Format(originalBitmapSource);
            OriginalImageControl.Source = _writeableBmp;
            //            CroppedRegionRect = new Rect(0, 0, _writeableBmp.Width, _writeableBmp.Height);
            CroppedCanvas.Width = _writeableBmp.Width;
            CroppedCanvas.Height = _writeableBmp.Height;

            ResetCroppedRect();
        }

        public void ResetCroppedRect()
        {
            ActualCroppedRegionRect = CroppedRegionRect;
            ActualZoom = Zoom;

            UpdateCroppedImage();
        }

        private void UpdateCroppedImage()
        {
            if (CroppedRegionRect == Rect.Empty)
                return;

            if (ActualCroppedRegionRect == Rect.Empty)
                return;

            if (Source == null)
                return;

            if (ActualZoom > MaxZoom)
            {
                ActualZoom = MaxZoom;
            }

            if (ActualZoom < MinZoom)
            {
                ActualZoom = MaxZoom;
            }

            var ratio = ActualZoomRatio;
            var widthInflate = (CroppedRegionRect.Width*ratio - CroppedRegionRect.Width)/2;
            var heightInflate = (CroppedRegionRect.Height*ratio - CroppedRegionRect.Height)/2;

            var objectRect = Rect.Inflate(CroppedRegionRect,
                                          widthInflate,
                                          heightInflate);

            var actualCenter = ActualCroppedRegionRect.GetCenterPoint();
            var oriCenter = CroppedRegionRect.GetCenterPoint();
            var offset = actualCenter - oriCenter;
            objectRect.Offset(offset);


            if (objectRect.Width < 0)
                objectRect.Width = 0;
            if (objectRect.Height < 0)
                objectRect.Height = 0;

            if (objectRect.X + objectRect.Width < 0)
                objectRect.X = 0 - objectRect.Width;
            if (objectRect.Y + objectRect.Height < 0)
                objectRect.Y = 0 - objectRect.Height;

            if (objectRect.X > Source.Width)
                objectRect.X = Source.Width;
            if (objectRect.Y > Source.Height)
                objectRect.Y = Source.Height;

            ActualCroppedRegionRect = objectRect;

            Debug.WriteLine(ActualCroppedRegionRect.ToString());

            CroppedRectange.SetValue(Canvas.LeftProperty, ActualCroppedRegionRect.X);
            CroppedRectange.SetValue(Canvas.TopProperty, ActualCroppedRegionRect.Y);
            CroppedRectange.Width = ActualCroppedRegionRect.Width;
            CroppedRectange.Height = ActualCroppedRegionRect.Height;

//            if (Math.Abs(ActualCroppedRegionRect.Width - 0) < 0.00001f
//                || Math.Abs(ActualCroppedRegionRect.Height - 0) < 0.00001f)
//                return;
            try
            {
                CroppedBitmapSource = _writeableBmp.Crop(ActualCroppedRegionRect);
            }
            catch (Exception)
            {
                //throw;
            }
            
        }

        private void MoveCroppedImage(double xDistance, double yDistance)
        {
            if (CroppedRegionRect == Rect.Empty)
                return;

            if (ActualCroppedRegionRect == Rect.Empty)
                return;

            var ratio = 1;

            var xMovement = xDistance*ratio;
            var yMovement = yDistance*ratio;

            var oriRect = SourceRect;
            var objectRect = Rect.Offset(ActualCroppedRegionRect, xMovement, yMovement);

            if (objectRect.X < 0)
                objectRect.X = 0;
            if (objectRect.X + objectRect.Width > oriRect.Width)
                objectRect.X = oriRect.Width - objectRect.Width;
            if (objectRect.Y < 0)
                objectRect.Y = 0;
            if (objectRect.Y + objectRect.Height > oriRect.Height)
                objectRect.Y = oriRect.Height - objectRect.Height;

            ActualCroppedRegionRect = objectRect;

            UpdateCroppedImage();
        }

        private Rect SourceRect
        {
            get { return new Rect(0, 0, Source.Width, Source.Height); }
        }

        #region MaxZoom

        public int MaxZoom
        {
            get { return (int) GetValue(MaxZoomProperty); }
            set { SetValue(MaxZoomProperty, value); }
        }

        public static readonly DependencyProperty MaxZoomProperty = DependencyProperty.Register(
            "MaxZoom", typeof (int), typeof (CroppedImage), new PropertyMetadata(4));

        #endregion

        #region MinZoom

        public int MinZoom
        {
            get { return (int) GetValue(MinZoomProperty); }
            set { SetValue(MinZoomProperty, value); }
        }

        public static readonly DependencyProperty MinZoomProperty = DependencyProperty.Register(
            "MinZoom", typeof (int), typeof (CroppedImage), new PropertyMetadata(-4));

        #endregion

        #region ActualZoom

        public int ActualZoom
        {
            get { return (int) GetValue(ActualZoomProperty); }
            private set { SetValue(ActualZoomProperty, value); }
        }

        public static readonly DependencyProperty ActualZoomProperty = DependencyProperty.Register(
            "ActualZoom", typeof (int), typeof (CroppedImage),
            new PropertyMetadata(0, (s, e) =>
                                        {
                                            var me = s as CroppedImage;
                                            if (me != null)
                                                me.ActualZoomRatio = Math.Pow(2, me.ActualZoom);
                                        })
            );

        #endregion

        #region ActualZoomRatio

        public double ActualZoomRatio
        {
            get { return (double) GetValue(ActualZoomRatioProperty); }
            private set { SetValue(ActualZoomRatioProperty, value); }
        }

        public static readonly DependencyProperty ActualZoomRatioProperty = DependencyProperty.Register(
            "ActualZoomRatio", typeof (double), typeof (CroppedImage), new PropertyMetadata(1.0));

        #endregion
    }
}