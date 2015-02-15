using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Hdc.Controls
{
    [TemplateVisualState(Name = "ValueNormal", GroupName = "ValueStates")]
    [TemplateVisualState(Name = "ValueOffline", GroupName = "ValueStates")]
    [TemplateVisualState(Name = "ValueObjectiveValueChanged", GroupName = "ValueStates")]
    [TemplatePart(Name = "PART_ValueIndicator", Type = typeof (RangeBase))]
    [TemplatePart(Name = "PART_ValueTextBlock", Type = typeof (TextBlock))]
    public class ValueEditor : RangeBase
    {
        private RangeBase _valueIndicator;
        private TextBlock _valueTextBlock;

        public static readonly DependencyProperty IncreaseCommandProperty = DependencyProperty.Register(
            "IncreaseCommand", typeof (ICommand), typeof (ValueEditor));

        public static readonly DependencyProperty BeginIncreaseCommandProperty = DependencyProperty.Register(
            "BeginIncreaseCommand", typeof (ICommand), typeof (ValueEditor));

        public static readonly DependencyProperty EndIncreaseCommandProperty = DependencyProperty.Register(
            "EndIncreaseCommand", typeof (ICommand), typeof (ValueEditor));

        public static readonly DependencyProperty DecreaseCommandProperty = DependencyProperty.Register(
            "DecreaseCommand", typeof (ICommand), typeof (ValueEditor));

        public static readonly DependencyProperty BeginDecreaseCommandProperty = DependencyProperty.Register(
            "BeginDecreaseCommand", typeof (ICommand), typeof (ValueEditor));

        public static readonly DependencyProperty EndDecreaseCommandProperty = DependencyProperty.Register(
            "EndDecreaseCommand", typeof (ICommand), typeof (ValueEditor));

        public static readonly DependencyProperty SetCommandProperty = DependencyProperty.Register(
            "SetCommand", typeof (ICommand), typeof (ValueEditor));

        public static readonly DependencyProperty IncreaseContentProperty = DependencyProperty.Register(
            "IncreaseContent", typeof (object), typeof (ValueEditor));

        public static readonly DependencyProperty DecreaseContentProperty = DependencyProperty.Register(
            "DecreaseContent", typeof (object), typeof (ValueEditor));

        public static readonly DependencyProperty SetContentProperty = DependencyProperty.Register(
            "SetContent", typeof (object), typeof (ValueEditor));

        public static readonly DependencyProperty IsOfflineProperty = DependencyProperty.Register(
            "IsOffline", typeof(bool), typeof(ValueEditor), new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty OfflineValueProperty = DependencyProperty.Register(
            "OfflineValue", typeof (double), typeof (ValueEditor));

        public static readonly DependencyProperty ObjectiveValueProperty = DependencyProperty.Register(
            "ObjectiveValue", typeof (double), typeof (ValueEditor));

        public static readonly DependencyProperty IsObjectiveValueChangedProperty = DependencyProperty.Register(
            "IsObjectiveValueChanged", typeof(bool), typeof(ValueEditor), new PropertyMetadata(OnPropertyChangedCallback));

        static ValueEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ValueEditor),
                                                     new FrameworkPropertyMetadata(typeof (ValueEditor)));
        }

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as ValueEditor;
            if (me == null) return;
            me.UpdateStates(true);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


            _valueIndicator = (RangeBase)GetTemplateChild("PART_ValueIndicator");
            _valueTextBlock = (TextBlock)GetTemplateChild("PART_ValueTextBlock");

            UpdateStates(false);
        }

        protected virtual void UpdateStates(bool useTransitions)
        {
            if (IsOffline)
            {
                VisualStateManager.GoToState(this, "ValueOffline", useTransitions);
                BindingTo("OfflineValue");
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
                    BindingTo("Value");
                }
            }
        }

        private void BindingTo(string propertyName)
        {
            if (_valueIndicator == null) return;
            if (_valueTextBlock == null) return;
            BindingOperations.SetBinding(_valueIndicator,
                                         RangeBase.ValueProperty,
                                         new Binding(propertyName) {Source = this, Mode = BindingMode.OneWay});
            BindingOperations.SetBinding(_valueTextBlock,
                                         TextBlock.TextProperty,
                                         new Binding(propertyName) {Source = this, Mode = BindingMode.OneWay});
        }

        [Bindable(true), Category("Commands")]
        public ICommand IncreaseCommand
        {
            get { return (ICommand) GetValue(IncreaseCommandProperty); }
            set { SetValue(IncreaseCommandProperty, value); }
        }

        [Bindable(true), Category("Commands")]
        public ICommand BeginIncreaseCommand
        {
            get { return (ICommand) GetValue(BeginIncreaseCommandProperty); }
            set { SetValue(BeginIncreaseCommandProperty, value); }
        }

        [Bindable(true), Category("Commands")]
        public ICommand EndIncreaseCommand
        {
            get { return (ICommand) GetValue(EndIncreaseCommandProperty); }
            set { SetValue(EndIncreaseCommandProperty, value); }
        }

        [Bindable(true), Category("Commands")]
        public ICommand DecreaseCommand
        {
            get { return (ICommand) GetValue(DecreaseCommandProperty); }
            set { SetValue(DecreaseCommandProperty, value); }
        }

        [Bindable(true), Category("Commands")]
        public ICommand BeginDecreaseCommand
        {
            get { return (ICommand) GetValue(BeginDecreaseCommandProperty); }
            set { SetValue(BeginDecreaseCommandProperty, value); }
        }

        [Bindable(true), Category("Commands")]
        public ICommand EndDecreaseCommand
        {
            get { return (ICommand) GetValue(EndDecreaseCommandProperty); }
            set { SetValue(EndDecreaseCommandProperty, value); }
        }

        [Bindable(true), Category("Commands")]
        public ICommand SetCommand
        {
            get { return (ICommand) GetValue(SetCommandProperty); }
            set { SetValue(SetCommandProperty, value); }
        }

        [Bindable(true), Category("Content")]
        public object SetContent
        {
            get { return (object) GetValue(SetContentProperty); }
            set { SetValue(SetContentProperty, value); }
        }

        [Bindable(true), Category("Content")]
        public object IncreaseContent
        {
            get { return (object) GetValue(IncreaseContentProperty); }
            set { SetValue(IncreaseContentProperty, value); }
        }

        [Bindable(true), Category("Content")]
        public object DecreaseContent
        {
            get { return (object) GetValue(DecreaseContentProperty); }
            set { SetValue(DecreaseContentProperty, value); }
        }
         
//        #region ActualValue
//
//        public double ActualValue
//        {
//            get { return (double) GetValue(ActualValueProperty); }
//            set { SetValue(ActualValueProperty, value); }
//        }
//
//        public static readonly DependencyProperty ActualValueProperty = DependencyProperty.Register(
//            "ActualValue", typeof (double), typeof (ValueEditor));
//
//        #endregion
         

        [Bindable(true), Category("Common Properties")]
        public bool IsOffline
        {
            get { return (bool) GetValue(IsOfflineProperty); }
            set { SetValue(IsOfflineProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public double OfflineValue
        {
            get { return (double) GetValue(OfflineValueProperty); }
            set { SetValue(OfflineValueProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public double ObjectiveValue
        {
            get { return (double) GetValue(ObjectiveValueProperty); }
            set { SetValue(ObjectiveValueProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public bool IsObjectiveValueChanged
        {
            get { return (bool) GetValue(IsObjectiveValueChangedProperty); }
            set { SetValue(IsObjectiveValueChangedProperty, value); }
        }
    }
}