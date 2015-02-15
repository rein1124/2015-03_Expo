using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Hdc.Controls
{
    [TemplatePart(Name = "PART_StripPanel", Type = typeof(Panel))]
    public class StripColumn : Control
    {
        public static readonly DependencyProperty IsStripLockedProperty = DependencyProperty.Register(
            "IsStripLocked", typeof(bool), typeof(StripColumn));

        public static readonly DependencyProperty LockedFillProperty = DependencyProperty.Register(
            "LockedFill", typeof(Brush), typeof(StripColumn), new UIPropertyMetadata(Brushes.Gray));

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
            "MaxValue", typeof(double), typeof(StripColumn), new UIPropertyMetadata(99.0));

        public static readonly DependencyProperty IsObjectiveValueChangedProperty =
            DependencyProperty.Register("IsObjectiveValueChanged", typeof(bool), typeof(StripColumn));

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
            "MinValue", typeof(double), typeof(StripColumn), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty MeasuredFillProperty = DependencyProperty.Register(
            "MeasuredFill", typeof(Brush), typeof(StripColumn), new UIPropertyMetadata(Brushes.GreenYellow));

        public static readonly DependencyProperty MeasuredStrokeProperty = DependencyProperty.Register(
            "MeasuredStroke", typeof(Brush), typeof(StripColumn), new UIPropertyMetadata(Brushes.GreenYellow));

        public static readonly DependencyProperty MeasuredThicknessProperty =
            DependencyProperty.Register("MeasuredThickness", typeof(double), typeof(StripColumn));


        public static readonly DependencyProperty MeasuredValueProperty = DependencyProperty.Register(
            "MeasuredValue", typeof(double), typeof(StripColumn), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty TickFrequencyProperty = DependencyProperty.Register(
            "TickFrequency", typeof(double), typeof(StripColumn), new UIPropertyMetadata(20.0));

        public static readonly DependencyProperty TickStrokeProperty = DependencyProperty.Register(
            "TickStroke", typeof(Brush), typeof(StripColumn), new UIPropertyMetadata(Brushes.Silver));

        public static readonly DependencyProperty ObjectiveValueProperty = DependencyProperty.Register(
            "ObjectiveValue", typeof(double), typeof(StripColumn), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty ObjectiveFillProperty = DependencyProperty.Register(
            "ObjectiveFill", typeof(Brush), typeof(StripColumn), new UIPropertyMetadata(Brushes.GreenYellow));

        public static readonly DependencyProperty ObjectiveStrokeProperty =
            DependencyProperty.Register(
                "ObjectiveStroke",
                typeof(Brush),
                typeof(StripColumn),
                new UIPropertyMetadata(Brushes.GreenYellow));

        public static readonly DependencyProperty ObjectiveThicknessProperty =
            DependencyProperty.Register("ObjectiveThickness", typeof(double), typeof(StripColumn));

        private Panel _partStripPanel;

        public static ICommand SetObjectiveValueToMaxCommand = new RoutedCommand();

        public static ICommand SetObjectiveValueToMinCommand = new RoutedCommand();


        static StripColumn()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(StripColumn),
                new FrameworkPropertyMetadata(typeof(StripColumn), FrameworkPropertyMetadataOptions.Inherits));
        }

        public StripColumn()
        {
            AllowDrop = true;

            CommandBindings.AddRange(
                new[]
                    {
                        new CommandBinding(
                            SetObjectiveValueToMaxCommand,
                            SetObjectiveValueToMaxCommandExecuted,
                            SetObjectiveValueToMaxCommandCanExecute),
                        new CommandBinding(
                            SetObjectiveValueToMinCommand,
                            SetObjectiveValueToMinCommandExecuted,
                            SetObjectiveValueToMinCommandCanExecute),
                    });
        }

        private void SetObjectiveValueToMaxCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ObjectiveValue = MaxValue;
        }

        private void SetObjectiveValueToMaxCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SetObjectiveValueToMinCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ObjectiveValue = MinValue;
        }

        private void SetObjectiveValueToMinCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        public bool IsObjectiveValueChanged
        {
            get { return (bool)GetValue(IsObjectiveValueChangedProperty); }
            set { SetValue(IsObjectiveValueChangedProperty, value); }
        }


        public bool IsStripLocked
        {
            get { return (bool)GetValue(IsStripLockedProperty); }
            set { SetValue(IsStripLockedProperty, value); }
        }


        public Brush LockedFill
        {
            get { return (Brush)GetValue(LockedFillProperty); }
            set { SetValue(LockedFillProperty, value); }
        }


        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }


        public Brush MeasuredFill
        {
            get { return (Brush)GetValue(MeasuredFillProperty); }
            set { SetValue(MeasuredFillProperty, value); }
        }

        public Brush MeasuredStroke
        {
            get { return (Brush)GetValue(MeasuredStrokeProperty); }
            set { SetValue(MeasuredStrokeProperty, value); }
        }


        public double MeasuredThickness
        {
            get { return (double)GetValue(MeasuredThicknessProperty); }
            set { SetValue(MeasuredThicknessProperty, value); }
        }

        public double MeasuredValue
        {
            get { return (double)GetValue(MeasuredValueProperty); }
            set { SetValue(MeasuredValueProperty, value); }
        }


        public double TickFrequency
        {
            get { return (double)GetValue(TickFrequencyProperty); }
            set { SetValue(TickFrequencyProperty, value); }
        }


        public Brush TickStroke
        {
            get { return (Brush)GetValue(TickStrokeProperty); }
            set { SetValue(TickStrokeProperty, value); }
        }


        public double ObjectiveValue
        {
            get { return (double)GetValue(ObjectiveValueProperty); }
            set { SetValue(ObjectiveValueProperty, value); }
        }


        public Brush ObjectiveFill
        {
            get { return (Brush)GetValue(ObjectiveFillProperty); }
            set { SetValue(ObjectiveFillProperty, value); }
        }


        public Brush ObjectiveStroke
        {
            get { return (Brush)GetValue(ObjectiveStrokeProperty); }
            set { SetValue(ObjectiveStrokeProperty, value); }
        }


        public double ObjectiveThickness
        {
            get { return (double)GetValue(ObjectiveThicknessProperty); }
            set { SetValue(ObjectiveThicknessProperty, value); }
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _partStripPanel = (Panel)GetTemplateChild("PART_StripPanel");
        }

        protected override void OnPreviewDragOver(DragEventArgs e)
        {
            if (IsStripLocked)
                return;
            Point pos = e.GetPosition(_partStripPanel);

            SetLevel(pos.Y);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);

            if (IsStripLocked)
                return;
            Point pos = e.GetPosition(_partStripPanel);

            SetLevel(pos.Y);

            //                        DragDrop.DoDragDrop(this, null, DragDropEffects.All);
            DragDrop.DoDragDrop(this, new object(), DragDropEffects.All);
        }

        private void SetLevel(double mousePositionY)
        {
            IsObjectiveValueChanged = true;
            if (_partStripPanel.ActualHeight == 0)
                return;

            double ratio = (_partStripPanel.ActualHeight - mousePositionY) / _partStripPanel.ActualHeight;
            double objTemp = MaxValue * ratio;
            objTemp = objTemp > MaxValue ? MaxValue : objTemp;
            objTemp = objTemp < 0 ? 0 : objTemp;

            ObjectiveValue = objTemp;
        }

        //        protected override Geometry GetLayoutClip(Size ls)
        //        {
        //            return null;
        //        } 
    }
}