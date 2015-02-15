using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Hdc.Controls
{
    [TemplateVisualState(Name = "ValueNormal", GroupName = "ValueStates")]
    [TemplateVisualState(Name = "ValueOffline", GroupName = "ValueStates")]
    [TemplateVisualState(Name = "ValueLocked", GroupName = "ValueStates")]
    [TemplateVisualState(Name = "ValueObjectiveValueChanged", GroupName = "ValueStates")]
    [TemplateVisualState(Name = "ValueFaulted", GroupName = "ValueStates")]
    public class ValueIndicatedButton : SwitchButton
    {
        public static readonly DependencyProperty IsLockedProperty = DependencyProperty.Register(
            "IsLocked", typeof (bool), typeof (ValueIndicatedButton), new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof (double), typeof (ValueIndicatedButton), new UIPropertyMetadata(99.0));

        public static readonly DependencyProperty IsObjectiveValueChangedProperty =
            DependencyProperty.Register("IsObjectiveValueChanged", typeof (bool), typeof (ValueIndicatedButton),
                                        new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", typeof (double), typeof (ValueIndicatedButton), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty ActualValueProperty = DependencyProperty.Register(
            "ActualValue", typeof (double), typeof (ValueIndicatedButton), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty ObjectiveValueProperty = DependencyProperty.Register(
            "ObjectiveValue", typeof (double), typeof (ValueIndicatedButton), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty IsFaultedProperty = DependencyProperty.Register(
            "IsFaulted", typeof (bool), typeof (ValueIndicatedButton), new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty IsOfflineProperty = DependencyProperty.Register(
            "IsOffline", typeof(bool), typeof(ValueIndicatedButton), new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty OfflineValueProperty = DependencyProperty.Register(
            "OfflineValue", typeof(double), typeof(ValueIndicatedButton));

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as ValueIndicatedButton;
            if (me == null) return;
            me.UpdateStates(true);
        }

        public static ICommand SetObjectiveValueToMaxCommand = new RoutedCommand();

        public static ICommand SetObjectiveValueToMinCommand = new RoutedCommand();

        static ValueIndicatedButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof (ValueIndicatedButton),
                new FrameworkPropertyMetadata(typeof (ValueIndicatedButton), FrameworkPropertyMetadataOptions.Inherits));
        }

        protected override void UpdateStates(bool useTransitions)
        {
            base.UpdateStates(useTransitions);

            if (IsOffline)
            {
                VisualStateManager.GoToState(this, "ValueOffline", useTransitions);
            }
            else
            {
                if (IsFaulted)
                {
                    VisualStateManager.GoToState(this, "ValueFaulted", useTransitions);
                }
                else
                {
                    if (IsLocked)
                    {
                        VisualStateManager.GoToState(this, "ValueLocked", useTransitions);
                    }
                    else
                    {
                        if (IsObjectiveValueChanged)
                        {
                            VisualStateManager.GoToState(this, "ValueObjectiveValueChanged", useTransitions);
                        }
                        else
                        {
                            VisualStateManager.GoToState(this, "ValueNormal", useTransitions);
                        }
                    }
                }
            }
        }

        public ValueIndicatedButton()
        {
            AllowDrop = true;

            CommandBindings.AddRange(
                new[]
                    {
                        new CommandBinding(
                            SetObjectiveValueToMaxCommand,
                            (sender, e) => { ObjectiveValue = Maximum; },
                            (sender1, e1) => { e1.CanExecute = true; }),
                        new CommandBinding(
                            SetObjectiveValueToMinCommand,
                            (sender2, e2) => { ObjectiveValue = Minimum; },
                            (sender3, e3) => { e3.CanExecute = true; }),
                    });
        }

        [Category("Common Properties")]
        public bool IsObjectiveValueChanged
        {
            get { return (bool) GetValue(IsObjectiveValueChangedProperty); }
            set { SetValue(IsObjectiveValueChangedProperty, value); }
        }

        [Category("Common Properties")]
        public bool IsLocked
        {
            get { return (bool) GetValue(IsLockedProperty); }
            set { SetValue(IsLockedProperty, value); }
        }

        [Category("Common Properties")]
        public double Maximum
        {
            get { return (double) GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        [Category("Common Properties")]
        public double Minimum
        {
            get { return (double) GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        [Category("Common Properties")]
        public double ActualValue
        {
            get { return (double) GetValue(ActualValueProperty); }
            set { SetValue(ActualValueProperty, value); }
        }

        [Category("Common Properties")]
        public double ObjectiveValue
        {
            get { return (double) GetValue(ObjectiveValueProperty); }
            set { SetValue(ObjectiveValueProperty, value); }
        }

        [Category("Common Properties")]
        public bool IsFaulted
        {
            get { return (bool) GetValue(IsFaultedProperty); }
            set { SetValue(IsFaultedProperty, value); }
        }

        [Category("Common Properties")]
        public bool IsOffline
        {
            get { return (bool) GetValue(IsOfflineProperty); }
            set { SetValue(IsOfflineProperty, value); }
        }

        [Category("Common Properties")]
        public double OfflineValue
        {
            get { return (double) GetValue(OfflineValueProperty); }
            set { SetValue(OfflineValueProperty, value); }
        }

    }
}