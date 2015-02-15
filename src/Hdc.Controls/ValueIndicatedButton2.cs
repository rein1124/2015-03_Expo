using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Hdc.Windows;

namespace Hdc.Controls
{
    [TemplateVisualState(Name = "ValueNormal", GroupName = "ValueStates")]
    [TemplateVisualState(Name = "ValueOffline", GroupName = "ValueStates")]
    [TemplateVisualState(Name = "ValueLocked", GroupName = "ValueStates")]
    [TemplateVisualState(Name = "ValueObjectiveValueChanged", GroupName = "ValueStates")]
    [TemplateVisualState(Name = "ValueFaulted", GroupName = "ValueStates")]
//    [TemplatePart(Name = "PART_ActualValueIndicator", Type = typeof(RangeBase))]
//    [TemplatePart(Name = "PART_ActualValueTextBlock", Type = typeof(TextBlock))]
//    [TemplatePart(Name = "PART_ObjectiveValueIndicator", Type = typeof(RangeBase))]
//    [TemplatePart(Name = "PART_ObjectiveValueTextBlock", Type = typeof(TextBlock))]
//    [TemplatePart(Name = "PART_OfflineValueIndicator", Type = typeof(RangeBase))]
//    [TemplatePart(Name = "PART_OfflineValueTextBlock", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_ValueIndicator", Type = typeof(RangeBase))]
    [TemplatePart(Name = "PART_ValueTextBlock", Type = typeof(TextBlock))]
    public class ValueIndicatedButton2 : SwitchButton
    {
//        private RangeBase _actualValueIndicator;
//        private TextBlock _actualValueTextBlock;
//        private RangeBase _objectiveValueIndicator;
//        private TextBlock _objectiveValueTextBlock;
        private RangeBase _valueIndicator;
        private TextBlock _valueTextBlock;

        public static readonly DependencyProperty IsLockedProperty = DependencyProperty.Register(
            "IsLocked", typeof (bool), typeof (ValueIndicatedButton2), new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof (double), typeof (ValueIndicatedButton2), new UIPropertyMetadata(99.0));

        public static readonly DependencyProperty IsObjectiveValueChangedProperty =
            DependencyProperty.Register("IsObjectiveValueChanged", typeof (bool), typeof (ValueIndicatedButton2),
                                        new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", typeof (double), typeof (ValueIndicatedButton2), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty ActualValueProperty = DependencyProperty.Register(
            "ActualValue", typeof (double), typeof (ValueIndicatedButton2), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty ObjectiveValueProperty = DependencyProperty.Register(
            "ObjectiveValue", typeof (double), typeof (ValueIndicatedButton2), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty IsFaultedProperty = DependencyProperty.Register(
            "IsFaulted", typeof (bool), typeof (ValueIndicatedButton2), new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty IsOfflineProperty = DependencyProperty.Register(
            "IsOffline", typeof (bool), typeof (ValueIndicatedButton2), new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty OfflineValueProperty = DependencyProperty.Register(
            "OfflineValue", typeof (double), typeof (ValueIndicatedButton2));

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as ValueIndicatedButton2;
            if (me == null) return;
            me.UpdateStates(true);
        }

        public override void OnApplyTemplate()
        {
//            _actualValueIndicator = (RangeBase)GetTemplateChild("PART_ActualValueIndicator");
//            _actualValueTextBlock = (TextBlock)GetTemplateChild("PART_ActualValueTextBlock");
//            _objectiveValueIndicator = (RangeBase)GetTemplateChild("PART_ObjectiveValueIndicator");
//            _objectiveValueTextBlock = (TextBlock)GetTemplateChild("PART_ObjectiveValueTextBlock");
            _valueIndicator = (RangeBase)GetTemplateChild("PART_ValueIndicator");
            _valueTextBlock = (TextBlock)GetTemplateChild("PART_ValueTextBlock");

            base.OnApplyTemplate();
        }

        public static ICommand SetObjectiveValueToMaxCommand = new RoutedCommand();

        public static ICommand SetObjectiveValueToMinCommand = new RoutedCommand();

        static ValueIndicatedButton2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof (ValueIndicatedButton2),
                new FrameworkPropertyMetadata(typeof (ValueIndicatedButton2), FrameworkPropertyMetadataOptions.Inherits));
        }

        protected override void UpdateStates(bool useTransitions)
        {
            base.UpdateStates(useTransitions);

            if (IsOffline)
            {
//                _actualValueIndicator.Binding(RangeBase.ValueProperty, this, x => x.OfflineValue);
                VisualStateManager.GoToState(this, "ValueOffline", useTransitions);
                BindingTo("OfflineValue");
            }
            else
            {
//                _actualValueIndicator.Binding(RangeBase.ValueProperty, this, x => x.ObjectiveValue);
//                _actualValueIndicator.Binding(RangeBase.ValueProperty, this, x => x.ActualValue);
                if (IsFaulted)
                {
                    VisualStateManager.GoToState(this, "ValueFaulted", useTransitions);
                    BindingTo("ActualValue");
                }
                else
                {
                    if (IsLocked)
                    {
                        VisualStateManager.GoToState(this, "ValueLocked", useTransitions);
                        BindingTo("ActualValue");
                    }
                    else
                    {
                        if (IsObjectiveValueChanged)
                        {
                            VisualStateManager.GoToState(this, "ValueObjectiveValueChanged", useTransitions);
                            BindingTo("ObjectiveValue");
                        }
                        else
                        {
                            VisualStateManager.GoToState(this, "ValueNormal", useTransitions);
                            BindingTo("ActualValue");
                        }
                    }
                }
            }
        }

        private void BindingTo(string propertyName)
        {
//            if (_actualValueIndicator == null) return;
//            if (_actualValueTextBlock == null) return;
//            if(_objectiveValueIndicator==null)return;
//            if(_objectiveValueTextBlock==null)return;
            if(_valueIndicator==null)return;
            if(_valueTextBlock==null)return;
//            BindingOperations.SetBinding(_actualValueIndicator,
//                                         RangeBase.ValueProperty,
//                                         new Binding(propertyName) {Source = this, Mode = BindingMode.OneWay});
//            BindingOperations.SetBinding(_actualValueTextBlock,
//                                         TextBlock.TextProperty,
//                                         new Binding(propertyName) {Source = this, Mode = BindingMode.OneWay});
//            BindingOperations.SetBinding(_objectiveValueIndicator,
//                                         RangeBase.ValueProperty,
//                                         new Binding(propertyName) {Source = this, Mode = BindingMode.OneWay});
//            BindingOperations.SetBinding(_objectiveValueTextBlock,
//                                         TextBlock.TextProperty,
//                                         new Binding(propertyName) {Source = this, Mode = BindingMode.OneWay});
            BindingOperations.SetBinding(_valueIndicator,
                                         RangeBase.ValueProperty,
                                         new Binding(propertyName) {Source = this, Mode = BindingMode.OneWay});
            BindingOperations.SetBinding(_valueTextBlock,
                                         TextBlock.TextProperty,
                                         new Binding(propertyName) {Source = this, Mode = BindingMode.OneWay});
        }


        public ValueIndicatedButton2()
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