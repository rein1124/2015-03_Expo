using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Hdc.Controls
{
    //DisplayNameStates
    [TemplateVisualState(
        GroupName = StateGroups.DisplayNameStates,
        Name = DisplayNameState.DisplayUnitIndex)]
    [TemplateVisualState(
        GroupName = StateGroups.DisplayNameStates,
        Name = DisplayNameState.DisplayUnitName)]
    [TemplateVisualState(
        GroupName = StateGroups.DisplayNameStates,
        Name = DisplayNameState.DisplayUnitIndexAndName)]
    //PressUnitStateStates
    [TemplateVisualState(
        GroupName = StateGroups.PressUnitStateStates,
        Name = PressUnitStateState.PressUnitStateNormal)]
    [TemplateVisualState(
        GroupName = StateGroups.PressUnitStateStates,
        Name = PressUnitStateState.PressUnitStateUnitSuspended)]
    [TemplateVisualState(
        GroupName = StateGroups.PressUnitStateStates,
        Name = PressUnitStateState.PressUnitStatePressSuspended)]
    [TemplateVisualState(
        GroupName = StateGroups.PressUnitStateStates,
        Name = PressUnitStateState.PressUnitStateCommunicationError)]
    //PressUnitTypeStates
    [TemplateVisualState(
        GroupName = StateGroups.PressUnitTypeStates,
        Name = PressUnitTypeState.PressUnitTypePress)]
    [TemplateVisualState(
        GroupName = StateGroups.PressUnitTypeStates,
        Name = PressUnitTypeState.PressUnitTypeCoating)]
    [TemplateVisualState(
        GroupName = StateGroups.PressUnitTypeStates,
        Name = PressUnitTypeState.PressUnitTypePressWithoutWater)]
    [TemplateVisualState(
        GroupName = StateGroups.PressUnitTypeStates,
        Name = PressUnitTypeState.PressUnitTypeDisabled)]
    public class PressUnitMonitor : PressSideMonitor
    {
        public static class StateGroups
        {
            public const string DisplayNameStates = "DisplayNameStates";
            public const string PressUnitStateStates = "PressUnitStateStates";
            public const string PressUnitStateVisibilityStates = "PressUnitStateVisibilityStates";
            public const string PressUnitTypeStates = "PressUnitTypeStates";
            public const string PressUnitTypeVisibilityStates = "PressUnitTypeVisibilityStates";
        }

        public static class DisplayNameState
        {
            public const string DisplayUnitIndex = "DisplayUnitIndex";
            public const string DisplayUnitName = "DisplayUnitName";
            public const string DisplayUnitIndexAndName = "DisplayUnitIndexAndName";
        }

        public static class PressUnitStateState
        {
            public const string PressUnitStateNormal = "PressUnitStateNormal";
            public const string PressUnitStateUnitSuspended = "PressUnitStateUnitSuspended";
            public const string PressUnitStatePressSuspended = "PressUnitStatePressSuspended";
            public const string PressUnitStateCommunicationError = "PressUnitStateCommunicationError";
        }

        public static class PressUnitStateVisibilityState
        {
            public const string PressUnitStateVisible = "PressUnitStateVisible";
            public const string PressUnitStateHidden = "PressUnitStateHidden";
        }

        public static class PressUnitTypeState
        {
            public const string PressUnitTypeDisabled = "PressUnitTypeDisabled";
            public const string PressUnitTypePress = "PressUnitTypePress";
            public const string PressUnitTypePressWithoutWater = "PressUnitTypePressWithoutWater";
            public const string PressUnitTypeCoating = "PressUnitTypeCoating";
        }

        public static class PressUnitTypeVisibilityState
        {
            public const string PressUnitTypeVisible = "PressUnitTypeVisible";
            public const string PressUnitTypeHidden = "PressUnitTypeHidden";
        }

        public static readonly DependencyProperty UnitIndexProperty = DependencyProperty.Register(
            "UnitIndex", typeof (int), typeof (PressUnitMonitor),
            new PropertyMetadata(OnUnitIndexPropertyChangedCallback));

        private static void OnUnitIndexPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as PressUnitMonitor;
            if (me == null) return;
            me.DisplayUnitIndex = me.UnitIndex + 1;
        }

        public static readonly DependencyProperty UnitNameProperty = DependencyProperty.Register(
            "UnitName", typeof(string), typeof(PressUnitMonitor));

        public static readonly DependencyProperty IsDisplayUnitNameProperty = DependencyProperty.Register(
            "IsDisplayUnitName", typeof(bool), typeof(PressUnitMonitor),
            new PropertyMetadata(false, UpdateDisplayNameStatePropertyChangedCallback));

        public static readonly DependencyProperty UnitBrushProperty = DependencyProperty.Register(
            "UnitBrush", typeof (Brush), typeof (PressUnitMonitor));

        public static readonly DependencyProperty DisplayUnitIndexProperty = DependencyProperty.Register(
            "DisplayUnitIndex", typeof (int), typeof (PressUnitMonitor), new PropertyMetadata(1));

        public static readonly DependencyProperty IsDisplayUnitIndexProperty = DependencyProperty.Register(
            "IsDisplayUnitIndex", typeof (bool), typeof (PressUnitMonitor),
            new PropertyMetadata(true, UpdateDisplayNameStatePropertyChangedCallback));

        public static readonly DependencyProperty SuspendingStateProperty = DependencyProperty.Register(
            "SuspendingState", typeof (PressUnitSuspendedState), typeof (PressUnitMonitor),
            new PropertyMetadata(UpdatePressUnitStateStatePropertyChangedCallback));

        public static readonly DependencyProperty IsCommunicationErrorProperty = DependencyProperty.Register(
            "IsCommunicationError", typeof (bool), typeof (PressUnitMonitor),
            new PropertyMetadata(UpdatePressUnitStateStatePropertyChangedCallback));

        public static readonly DependencyProperty IsDisplayUnitStateProperty = DependencyProperty.Register(
            "IsDisplayUnitState", typeof(bool), typeof(PressUnitMonitor),
            new PropertyMetadata(UpdatePressUnitStateVisibilityStatePropertyChangedCallback));

        public static readonly DependencyProperty UnitTypeProperty = DependencyProperty.Register(
            "UnitType", typeof (PressUnitType), typeof (PressUnitMonitor),
            new PropertyMetadata(UpdatePressUnitTypeStatePropertyChangedCallback));

        public static readonly DependencyProperty IsDisplayUnitTypeProperty = DependencyProperty.Register(
            "IsDisplayUnitType", typeof(bool), typeof(PressUnitMonitor),
            new PropertyMetadata(UpdatePressUnitTypeVisibilityStatePropertyChangedCallback));

        private static void UpdatePressUnitStateStatePropertyChangedCallback(DependencyObject s,
                                                                        DependencyPropertyChangedEventArgs e)
        {
            var me = s as PressUnitMonitor;
            if (me == null) return;

            me.UpdatePressUnitStateState(true);
        }
        private static void UpdatePressUnitTypeStatePropertyChangedCallback(DependencyObject s,
                                                                        DependencyPropertyChangedEventArgs e)
        {
            var me = s as PressUnitMonitor;
            if (me == null) return;

            me.UpdatePressUnitTypeState(true);
        }

        private static void UpdatePressUnitTypeVisibilityStatePropertyChangedCallback(DependencyObject s,
                                                                        DependencyPropertyChangedEventArgs e)
        {
            var me = s as PressUnitMonitor;
            if (me == null) return;

            me.UpdatePressUnitTypeVisibilityState(true);
        }

        private static void UpdatePressUnitStateVisibilityStatePropertyChangedCallback(DependencyObject s,
                                                                        DependencyPropertyChangedEventArgs e)
        {
            var me = s as PressUnitMonitor;
            if (me == null) return;

            me.UpdatePressUnitStateVisibilityState(true);
        }

        private static void UpdateDisplayNameStatePropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as PressUnitMonitor;
            if (me == null) return;

            me.UpdateDisplayNameState(true);
        }

        static PressUnitMonitor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (PressUnitMonitor),
                                                     new FrameworkPropertyMetadata(typeof (PressUnitMonitor)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateDisplayNameState(false);
            UpdatePressUnitStateState(false);
            UpdatePressUnitStateVisibilityState(false);
            UpdatePressUnitTypeState(false);
            UpdatePressUnitTypeVisibilityState(false);
            
        }

/*        protected override void UpdateStates(bool useTransitions)
        {
            base.UpdateStates(useTransitions);

//            if (IsDisplayUnitType)
//            {
//                VisualStateManager.GoToState(this, PressUnitStateVisibilityState.PressUnitStateVisible, useTransitions);
//            }
//            else
//            {
//                VisualStateManager.GoToState(this, PressUnitStateVisibilityState.PressUnitStateHidden, useTransitions);
//            }
//
//            if (IsDisplayUnitType)
//            {
//                VisualStateManager.GoToState(this, PressUnitTypeVisibilityState.PressUnitTypeVisible, useTransitions);
//            }
//            else
//            {
//                VisualStateManager.GoToState(this, PressUnitTypeVisibilityState.PressUnitTypeHidden, useTransitions);
//            }
        }*/

        protected void UpdateDisplayNameState(bool useTransitions)
        {
            if (IsDisplayUnitIndex && IsDisplayUnitName)
            {
                VisualStateManager.GoToState(this, DisplayNameState.DisplayUnitIndexAndName, useTransitions);
            }
            else if (IsDisplayUnitIndex)
            {
                VisualStateManager.GoToState(this, DisplayNameState.DisplayUnitIndex, useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, DisplayNameState.DisplayUnitName, useTransitions);
            }
        }

        protected void UpdatePressUnitStateState(bool useTransitions)
        {
            if (IsCommunicationError)
            {
                VisualStateManager.GoToState(this, PressUnitStateState.PressUnitStateCommunicationError, useTransitions);
            }
            else
            {
                switch (SuspendingState)
                {
                    case PressUnitSuspendedState.Unsuspended:
                        VisualStateManager.GoToState(this, PressUnitStateState.PressUnitStateNormal, useTransitions);
                        break;
                    case PressUnitSuspendedState.UnitSuspended:
                        VisualStateManager.GoToState(this, PressUnitStateState.PressUnitStateUnitSuspended,
                                                     useTransitions);
                        break;
                    case PressUnitSuspendedState.PressSuspended:
                        VisualStateManager.GoToState(this, PressUnitStateState.PressUnitStatePressSuspended,
                                                     useTransitions);
                        break;
                    default:
                        VisualStateManager.GoToState(this, PressUnitStateState.PressUnitStateNormal, useTransitions);
                        break;
                }
            }
        }

        protected void UpdatePressUnitStateVisibilityState(bool useTransitions)
        {
            if (IsDisplayUnitState)
            {
                VisualStateManager.GoToState(this, PressUnitStateVisibilityState.PressUnitStateVisible, useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, PressUnitStateVisibilityState.PressUnitStateHidden, useTransitions);
            }
        }

        protected void UpdatePressUnitTypeState(bool useTransitions)
        {
            switch (UnitType)
            {
                case PressUnitType.Press:
                    VisualStateManager.GoToState(this, PressUnitTypeState.PressUnitTypePress, useTransitions);
                    break;
                case PressUnitType.Coating:
                    VisualStateManager.GoToState(this, PressUnitTypeState.PressUnitTypeCoating,
                                                 useTransitions);
                    break;
                case PressUnitType.PressWithoutWater:
                    VisualStateManager.GoToState(this, PressUnitTypeState.PressUnitTypePressWithoutWater,
                                                 useTransitions);
                    break;
                case PressUnitType.Disabled:
                    VisualStateManager.GoToState(this, PressUnitTypeState.PressUnitTypeDisabled, useTransitions);
                    break;
            }
        }

        protected void UpdatePressUnitTypeVisibilityState(bool useTransitions)
        {
            if (IsDisplayUnitType)
            {
                VisualStateManager.GoToState(this, PressUnitTypeVisibilityState.PressUnitTypeVisible, useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, PressUnitTypeVisibilityState.PressUnitTypeHidden, useTransitions);
            }
        }


        [Bindable(true), Category("Common Properties")]
        public int UnitIndex
        {
            get { return (int) GetValue(UnitIndexProperty); }
            set { SetValue(UnitIndexProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public string UnitName
        {
            get { return (string) GetValue(UnitNameProperty); }
            set { SetValue(UnitNameProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public Brush UnitBrush
        {
            get { return (Brush) GetValue(UnitBrushProperty); }
            set { SetValue(UnitBrushProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public int DisplayUnitIndex
        {
            get { return (int) GetValue(DisplayUnitIndexProperty); }
            private set { SetValue(DisplayUnitIndexProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public bool IsDisplayUnitIndex
        {
            get { return (bool) GetValue(IsDisplayUnitIndexProperty); }
            set { SetValue(IsDisplayUnitIndexProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public bool IsDisplayUnitName
        {
            get { return (bool) GetValue(IsDisplayUnitNameProperty); }
            set { SetValue(IsDisplayUnitNameProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public PressUnitSuspendedState SuspendingState
        {
            get { return (PressUnitSuspendedState) GetValue(SuspendingStateProperty); }
            set { SetValue(SuspendingStateProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public bool IsCommunicationError
        {
            get { return (bool) GetValue(IsCommunicationErrorProperty); }
            set { SetValue(IsCommunicationErrorProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public bool IsDisplayUnitState
        {
            get { return (bool) GetValue(IsDisplayUnitStateProperty); }
            set { SetValue(IsDisplayUnitStateProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public bool IsDisplayUnitType
        {
            get { return (bool) GetValue(IsDisplayUnitTypeProperty); }
            set { SetValue(IsDisplayUnitTypeProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public PressUnitType UnitType
        {
            get { return (PressUnitType) GetValue(UnitTypeProperty); }
            set { SetValue(UnitTypeProperty, value); }
        }
    }
}