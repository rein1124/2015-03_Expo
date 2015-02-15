using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Hdc.Controls
{
    [TemplateVisualState(Name = DirectionState.DirectionStateNone, GroupName = StateGroups.DirectionStates)]
    [TemplateVisualState(Name = DirectionState.DirectionStateLeft, GroupName = StateGroups.DirectionStates)]
    [TemplateVisualState(Name = DirectionState.DirectionStateRight, GroupName = StateGroups.DirectionStates)]
    [TemplateVisualState(Name = DirectionState.DirectionStateUp, GroupName = StateGroups.DirectionStates)]
    [TemplateVisualState(Name = DirectionState.DirectionStateDown, GroupName = StateGroups.DirectionStates)]
    public class WebPressSideSelector : SwitchButton
    {
        public static class StateGroups
        {
            public const string DirectionStates = "DirectionStates";
        }

        public static class DirectionState
        {
            public const string DirectionStateNone = "DirectionStateNone";
            public const string DirectionStateLeft = "DirectionStateLeft";
            public const string DirectionStateRight = "DirectionStateRight";
            public const string DirectionStateUp = "DirectionStateUp";
            public const string DirectionStateDown = "DirectionStateDown";
        }

        public static readonly DependencyProperty ColorBrushProperty = DependencyProperty.Register(
            "ColorBrush", typeof (Brush), typeof (WebPressSideSelector));

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction", typeof (Direction), typeof (WebPressSideSelector),
            new PropertyMetadata(PropertyChangedCallback));

        public static readonly DependencyProperty SideIndexProperty = DependencyProperty.Register(
            "SideIndex", typeof(int), typeof(WebPressSideSelector), new PropertyMetadata(PropertyChangedCallback));

        public static readonly DependencyProperty DisplaySideIndexProperty = DependencyProperty.Register(
            "DisplaySideIndex", typeof (int), typeof (WebPressSideSelector));

        public static readonly DependencyProperty SideNameProperty = DependencyProperty.Register(
            "SideName", typeof (string), typeof (WebPressSideSelector));

        public static readonly DependencyProperty UseIndexDirectionProperty = DependencyProperty.Register(
            "UseIndexDirection", typeof (bool), typeof (WebPressSideSelector),
            new PropertyMetadata(false, PropertyChangedCallback));

        public static readonly DependencyProperty UnitIndexProperty = DependencyProperty.Register(
            "UnitIndex", typeof(int), typeof(WebPressSideSelector), new PropertyMetadata(PropertyChangedCallback));

        public static readonly DependencyProperty DisplayUnitIndexProperty = DependencyProperty.Register(
            "DisplayUnitIndex", typeof (int), typeof (WebPressSideSelector));

        static WebPressSideSelector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (WebPressSideSelector),
                                                     new FrameworkPropertyMetadata(typeof (WebPressSideSelector)));
        }

        // OnDirectionChangedCallback
        // UseIndexDirectionChangedCallback
        // OnUnitIndexChangedCallback
        // OnSideIndexChangedCallback
        private static void PropertyChangedCallback(DependencyObject s,
                                                    DependencyPropertyChangedEventArgs e)
        {
            var me = s as WebPressSideSelector;
            if (me == null) return;

            me.UpdateStates(true);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateStates(false);
        }

        protected override void UpdateStates(bool useTransitions)
        {
            base.UpdateStates(useTransitions);

            DisplayUnitIndex = UnitIndex + 1;
            DisplaySideIndex = SideIndex + 1;

            if (UseIndexDirection)
            {
                switch (SideIndex)
                {
                    case 0:
                        VisualStateManager.GoToState(this, DirectionState.DirectionStateLeft, useTransitions);
                        break;
                    case 1:
                        VisualStateManager.GoToState(this, DirectionState.DirectionStateRight, useTransitions);
                        break;
                }
            }
            else
            {
                var stateString = typeof (DirectionState).Name + Direction.ToString();
                VisualStateManager.GoToState(this, stateString, useTransitions);
//                switch (Direction)
//                {
//                    case 0:
//                        
//                        break;
//                    case 1:
//                        VisualStateManager.GoToState(this, "Right", useTransitions);
//                        break;
//                }
            }
        }

        [Bindable(true), Category("Common Properties")]
        public int SideIndex
        {
            get { return (int) GetValue(SideIndexProperty); }
            set { SetValue(SideIndexProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public string SideName
        {
            get { return (string) GetValue(SideNameProperty); }
            set { SetValue(SideNameProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public int DisplaySideIndex
        {
            get { return (int) GetValue(DisplaySideIndexProperty); }
            private set { SetValue(DisplaySideIndexProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public Direction Direction
        {
            get { return (Direction) GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public Brush ColorBrush
        {
            get { return (Brush) GetValue(ColorBrushProperty); }
            set { SetValue(ColorBrushProperty, value); }
        }


        [Bindable(true), Category("Common Properties")]
        public bool UseIndexDirection
        {
            get { return (bool) GetValue(UseIndexDirectionProperty); }
            set { SetValue(UseIndexDirectionProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public int UnitIndex
        {
            get { return (int) GetValue(UnitIndexProperty); }
            set { SetValue(UnitIndexProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public int DisplayUnitIndex
        {
            get { return (int) GetValue(DisplayUnitIndexProperty); }
            private set { SetValue(DisplayUnitIndexProperty, value); }
        }
    }
}