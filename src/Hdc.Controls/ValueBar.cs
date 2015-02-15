using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Hdc.Controls
{
    // OrientationStates
    [TemplateVisualState(GroupName = StateGroups.OrientationStates, Name = DirectionState.OrientationStateUp)]
    [TemplateVisualState(GroupName = StateGroups.OrientationStates, Name = DirectionState.OrientationStateDown)]
    [TemplateVisualState(GroupName = StateGroups.OrientationStates, Name = DirectionState.OrientationStateLeft)]
    [TemplateVisualState(GroupName = StateGroups.OrientationStates, Name = DirectionState.OrientationStateRight)]
    // TickBarPositionStates
    [TemplateVisualState(GroupName = StateGroups.TickBarPositionStates,
        Name = TickBarPositionState.TickBarPositionStateUp)]
    [TemplateVisualState(GroupName = StateGroups.TickBarPositionStates,
        Name = TickBarPositionState.TickBarPositionStateDown)]
    [TemplateVisualState(GroupName = StateGroups.TickBarPositionStates,
        Name = TickBarPositionState.TickBarPositionStateLeft)]
    [TemplateVisualState(GroupName = StateGroups.TickBarPositionStates,
        Name = TickBarPositionState.TickBarPositionStateRight)]
    // Parts
    [TemplatePart(Name = PartName.PART_Track, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PartName.PART_Indicator, Type = typeof(FrameworkElement))]
    public class ValueBar : RangeBase
    {
        public static class StateGroups
        {
            public const string OrientationStates = "OrientationStates";
            public const string TickBarPositionStates = "TickBarPositionStates";
        }

        public static class DirectionState
        {
            public const string OrientationStateUp = "OrientationStateUp";
            public const string OrientationStateDown = "OrientationStateDown";
            public const string OrientationStateLeft = "OrientationStateLeft";
            public const string OrientationStateRight = "OrientationStateRight";
        }

        public static class TickBarPositionState
        {
            public const string TickBarPositionStateUp = "TickBarPositionStateUp";
            public const string TickBarPositionStateDown = "TickBarPositionStateDown";
            public const string TickBarPositionStateLeft = "TickBarPositionStateLeft";
            public const string TickBarPositionStateRight = "TickBarPositionStateRight";
        }

        public static class PartName
        {
            public const string PART_Track = "PART_Track";
            public const string PART_Indicator = "PART_Indicator";
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation", typeof(Orientation4), typeof(ValueBar),
            new UIPropertyMetadata(Orientation4.Right, (o, args) =>
                                                           {
                                                               var me = o as ValueBar;
                                                               if (me == null) return;

                                                               me.UpdateOrientationState(true);
                                                           }));

        public static readonly DependencyProperty TickBarPositionProperty = DependencyProperty.Register(
            "TickBarPosition", typeof(Orientation4), typeof(ValueBar),
            new UIPropertyMetadata(Orientation4.Down, (o, args) => { }));

        public static readonly DependencyProperty BarBrushProperty = DependencyProperty.Register(
            "BarBrush", typeof(Brush), typeof(ValueBar),
            new UIPropertyMetadata(Brushes.YellowGreen, (o, args) => { }));

        public static readonly DependencyProperty IsTickBarEnabledProperty = DependencyProperty.Register(
            "IsTickBarEnabled", typeof(bool), typeof(ValueBar),
            new UIPropertyMetadata(false, (o, args) => { }));

        public static readonly DependencyProperty BarMarginProperty = DependencyProperty.Register(
            "BarMargin", typeof(double), typeof(ValueBar),
            new UIPropertyMetadata(0.0, (o, args) =>
                                              {
                                                  var me = o as ValueBar;
                                                  if (me == null) return;

                                                  me.UpdateOrientationState(true);
                                              }));

        static ValueBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ValueBar),
                                                     new FrameworkPropertyMetadata(typeof(ValueBar)));
        }

        private FrameworkElement _track;
        private FrameworkElement _indicator;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _track = (FrameworkElement)GetTemplateChild(PartName.PART_Track);
            _indicator = (FrameworkElement)GetTemplateChild(PartName.PART_Indicator);

            _track.SizeChanged += new SizeChangedEventHandler(this.OnTrackSizeChanged);

            UpdateOrientationState(false);
        }

        private void OnTrackSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateIndicatorLength();
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            UpdateIndicatorLength();
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            UpdateIndicatorLength();
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            UpdateIndicatorLength();
        }

        private void UpdateIndicatorLength()
        {
            if (_indicator == null)
                return;

            if (_track == null)
                return;

            switch (Orientation)
            {
                case Orientation4.Left:
                    _indicator.HorizontalAlignment = HorizontalAlignment.Right;
                    _indicator.VerticalAlignment = VerticalAlignment.Stretch;
                    _indicator.Margin = new Thickness(0, BarMargin, 0, BarMargin);
                    _indicator.Width = _track.ActualWidth * Value / (Maximum - Minimum);
                    break;
                case Orientation4.Right:
                    _indicator.HorizontalAlignment = HorizontalAlignment.Left;
                    _indicator.VerticalAlignment = VerticalAlignment.Stretch;
                    _indicator.Margin = new Thickness(0, BarMargin, 0, BarMargin);
                    _indicator.Width = _track.ActualWidth * Value / (Maximum - Minimum);
                    break;
                case Orientation4.Up:
                    _indicator.HorizontalAlignment = HorizontalAlignment.Stretch;
                    _indicator.VerticalAlignment = VerticalAlignment.Bottom;
                    _indicator.Margin = new Thickness(BarMargin, 0, BarMargin, 0);
                    _indicator.Height = _track.ActualHeight * Value / (Maximum - Minimum);
                    break;
                case Orientation4.Down:
                    _indicator.HorizontalAlignment = HorizontalAlignment.Stretch;
                    _indicator.VerticalAlignment = VerticalAlignment.Top;
                    _indicator.Margin = new Thickness(BarMargin, 0, BarMargin, 0);
                    _indicator.Height = _track.ActualHeight * Value / (Maximum - Minimum);
                    break;
            }
        }

        protected void UpdateOrientationState(bool useTransitions)
        {
            switch (Orientation)
            {
                case Orientation4.Left:
                    VisualStateManager.GoToState(this, DirectionState.OrientationStateLeft, useTransitions);
                    break;
                case Orientation4.Right:
                    VisualStateManager.GoToState(this, DirectionState.OrientationStateRight, useTransitions);
                    break;
                case Orientation4.Up:
                    VisualStateManager.GoToState(this, DirectionState.OrientationStateUp, useTransitions);
                    break;
                case Orientation4.Down:
                    VisualStateManager.GoToState(this, DirectionState.OrientationStateDown, useTransitions);
                    break;
            }

            UpdateIndicatorLength();
        }

        [Bindable(true), Category("Common Properties")]
        public Orientation4 Orientation
        {
            get { return (Orientation4)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }


        [Bindable(true), Category("Common Properties")]
        public Orientation4 TickBarPosition
        {
            get { return (Orientation4)GetValue(TickBarPositionProperty); }
            set { SetValue(TickBarPositionProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public Brush BarBrush
        {
            get { return (Brush)GetValue(BarBrushProperty); }
            set { SetValue(BarBrushProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public bool IsTickBarEnabled
        {
            get { return (bool)GetValue(IsTickBarEnabledProperty); }
            set { SetValue(IsTickBarEnabledProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public double BarMargin
        {
            get { return (double)GetValue(BarMarginProperty); }
            set { SetValue(BarMarginProperty, value); }
        }
    }
}