using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Hdc.Controls
{
    public class CurveEditor : Control
    {
        public static ICommand MoveToRightPointCommand = new RoutedCommand();

        public static ICommand MoveToLeftPointCommand = new RoutedCommand();

        public static ICommand IncreaseXCommand = new RoutedCommand();

        public static ICommand DecreaseXCommand = new RoutedCommand();

        public static ICommand IncreaseYCommand = new RoutedCommand();

        public static ICommand DecreaseYCommand = new RoutedCommand();

        static CurveEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CurveEditor), new FrameworkPropertyMetadata(typeof(CurveEditor)));
        }

        public CurveEditor()
        {
            CommandBindings.AddRange(
                new[]
                    {
                        new CommandBinding(
                            MoveToLeftPointCommand, MoveToLeftPointCommandExecuted, MoveToLeftPointCommandCanExecute),
                        new CommandBinding(
                            MoveToRightPointCommand, MoveToRightPointCommandExecuted, MoveToRightPointCommandCanExecute)
                        , new CommandBinding(IncreaseXCommand, IncreaseXCommandExecuted, IncreaseXCommandCanExecute),
                        new CommandBinding(DecreaseXCommand, DecreaseXCommandExecuted, DecreaseXCommandCanExecute),
                        new CommandBinding(IncreaseYCommand, IncreaseYCommandExecuted, IncreaseYCommandCanExecute),
                        new CommandBinding(DecreaseYCommand, DecreaseYCommandExecuted, DecreaseYCommandCanExecute),
                    });

            //            Points = new PointCollection();
        }

        #region PROPERTY

        #region XMax

        public double XMax
        {
            get
            {
                return (double)GetValue(XMaxProperty);
            }
            set
            {
                SetValue(XMaxProperty, value);
            }
        }

        public static readonly DependencyProperty XMaxProperty = DependencyProperty.Register(
            "XMax", typeof(double), typeof(CurveEditor), new PropertyMetadata(200.0));

        #endregion

        #region XMin

        public double XMin
        {
            get
            {
                return (double)GetValue(XMinProperty);
            }
            set
            {
                SetValue(XMinProperty, value);
            }
        }

        public static readonly DependencyProperty XMinProperty = DependencyProperty.Register(
            "XMin", typeof(double), typeof(CurveEditor), new PropertyMetadata(0.0));

        #endregion

        #region XInterval

        public double XInterval
        {
            get
            {
                return (double)GetValue(XIntervalProperty);
            }
            set
            {
                SetValue(XIntervalProperty, value);
            }
        }

        public static readonly DependencyProperty XIntervalProperty = DependencyProperty.Register(
            "XInterval", typeof(double), typeof(CurveEditor), new PropertyMetadata(20.0));

        #endregion

        #region YMax

        public double YMax
        {
            get
            {
                return (double)GetValue(YMaxProperty);
            }
            set
            {
                SetValue(YMaxProperty, value);
            }
        }

        public static readonly DependencyProperty YMaxProperty = DependencyProperty.Register(
            "YMax", typeof(double), typeof(CurveEditor), new PropertyMetadata(200.0));

        #endregion

        #region YMin

        public double YMin
        {
            get
            {
                return (double)GetValue(YMinProperty);
            }
            set
            {
                SetValue(YMinProperty, value);
            }
        }

        public static readonly DependencyProperty YMinProperty = DependencyProperty.Register(
            "YMin", typeof(double), typeof(CurveEditor), new PropertyMetadata(0.0));

        #endregion

        #region YInterval

        public double YInterval
        {
            get
            {
                return (double)GetValue(YIntervalProperty);
            }
            set
            {
                SetValue(YIntervalProperty, value);
            }
        }

        public static readonly DependencyProperty YIntervalProperty = DependencyProperty.Register(
            "YInterval", typeof(double), typeof(CurveEditor), new PropertyMetadata(20.0));

        #endregion

        #region GridBrush

        public Brush GridBrush
        {
            get
            {
                return (Brush)GetValue(GridBrushProperty);
            }
            set
            {
                SetValue(GridBrushProperty, value);
            }
        }

        public static readonly DependencyProperty GridBrushProperty = DependencyProperty.Register(
            "GridBrush", typeof(Brush), typeof(CurveEditor), new PropertyMetadata(Brushes.Black));

        #endregion

        #region LineBrush

        public Brush LineBrush
        {
            get
            {
                return (Brush)GetValue(LineBrushProperty);
            }
            set
            {
                SetValue(LineBrushProperty, value);
            }
        }

        public static readonly DependencyProperty LineBrushProperty = DependencyProperty.Register(
            "LineBrush", typeof(Brush), typeof(CurveEditor), new PropertyMetadata(Brushes.GreenYellow));

        #endregion

        #region Points

        public PointCollection Points
        {
            get
            {
                return (PointCollection)GetValue(PointsProperty);
            }
            set
            {
                SetValue(PointsProperty, value);
            }
        }

        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register(
            "Points",
            typeof(PointCollection),
            typeof(CurveEditor),
            new PropertyMetadata(new PointCollection(), PointsPropertyChangedCallback));

        private static void PointsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newPoints = e.NewValue as PointCollection;
            var oldPoints = e.OldValue as PointCollection;

            var editor = (CurveEditor)d;
            if (newPoints != null && newPoints.Count > 0 && newPoints != oldPoints)
            {
                var currentPoint = newPoints[0];
                editor.CurrentPoint = currentPoint;
                editor.CurrentPointX = currentPoint.X;
                editor.CurrentPointY = currentPoint.Y;
            }

            if (newPoints == PointsProperty.DefaultMetadata.DefaultValue)
            {
                editor.CurrentPointX = editor.XMin;
                editor.CurrentPointY = editor.YMin;
            }
        }

        #endregion

        #region CurrentPoint

        public Point CurrentPoint
        {
            get
            {
                return (Point)GetValue(CurrentPointProperty);
            }
            set
            {
                SetValue(CurrentPointProperty, value);
            }
        }

        public static readonly DependencyProperty CurrentPointProperty = DependencyProperty.Register(
            "CurrentPoint",
            typeof(Point),
            typeof(CurveEditor),
            new PropertyMetadata(CurrentPointPropertyChangedCallback));

        private static void CurrentPointPropertyChangedCallback(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (Point)e.NewValue;
            var oldValue = (Point)e.OldValue;

            var source = (CurveEditor)d;

            if (value.X != oldValue.X)
            {
                source.CurrentPointX = value.X;
            }
            if (value.Y != oldValue.Y)
            {
                source.CurrentPointY = value.Y;
            }
        }

        #endregion

        #region CurrentPointIndex

        public int CurrentPointIndex
        {
            get
            {
                return HasPoint ? Points.IndexOf(CurrentPoint) : 0;
            }
        }

        #endregion

        public bool HasPoint
        {
            get
            {
                return Points != null && Points.Count > 0;
            }
        }

        #region CurrentPointX

        public double CurrentPointX
        {
            get
            {
                return (double)GetValue(CurrentPointXProperty);
            }
            set
            {
                SetValue(CurrentPointXProperty, value);
            }
        }

        public static readonly DependencyProperty CurrentPointXProperty = DependencyProperty.Register(
            "CurrentPointX", typeof(double), typeof(CurveEditor));

        #endregion

        #region CurrentPointY

        public double CurrentPointY
        {
            get
            {
                return (double)GetValue(CurrentPointYProperty);
            }
            set
            {
                SetValue(CurrentPointYProperty, value);
            }
        }

        public static readonly DependencyProperty CurrentPointYProperty = DependencyProperty.Register(
            "CurrentPointY", typeof(double), typeof(CurveEditor));

        #endregion

        #endregion

        private void MoveToRightPointCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!HasPoint)
            {
                return;
            }
            CurrentPoint = Points[CurrentPointIndex + 1];
        }

        private void MoveToRightPointCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!HasPoint)
            {
                return;
            }

            e.CanExecute = CurrentPointIndex < Points.Count - 1;
        }

        private void MoveToLeftPointCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!HasPoint)
            {
                return;
            }
            CurrentPoint = Points[CurrentPointIndex - 1];
        }

        private void MoveToLeftPointCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!HasPoint)
            {
                return;
            }
            e.CanExecute = CurrentPointIndex > 0;
        }

        private void IncreaseXCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentPoint = Points[CurrentPointIndex] = new Point(CurrentPointX + 1, CurrentPointY);
        }

        private void IncreaseXCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!HasPoint)
            {
                e.CanExecute = false;
                return;
            }
            if (CurrentPointIndex <= 0 || CurrentPointIndex == Points.Count - 1)
            {
                e.CanExecute = false;
                return;
            }
            if (CurrentPointX + 1 >= Points[CurrentPointIndex + 1].X)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = true;
            return;
        }

        private void DecreaseXCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentPoint = Points[CurrentPointIndex] = new Point(CurrentPointX - 1, CurrentPointY);
        }

        private void DecreaseXCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!HasPoint)
            {
                e.CanExecute = false;
                return;
            }
            if (CurrentPointIndex <= 0 || CurrentPointIndex == Points.Count - 1)
            {
                e.CanExecute = false;
                return;
            }
            if (CurrentPointX - 1 <= Points[CurrentPointIndex - 1].X)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = true;
            return;
        }

        private void IncreaseYCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentPoint = Points[CurrentPointIndex] = new Point(CurrentPointX, CurrentPointY + 1);
        }

        private void IncreaseYCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HasPoint && CurrentPointY + 1 <= YMax)
            {
                e.CanExecute = true;
            }
        }

        private void DecreaseYCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentPoint = Points[CurrentPointIndex] = new Point(CurrentPointX, CurrentPointY - 1);
        }

        private void DecreaseYCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HasPoint && CurrentPointY - 1 >= YMin)
            {
                e.CanExecute = true;
            }
        }
    }
}