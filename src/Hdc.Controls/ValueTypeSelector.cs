using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Hdc.Controls
{
    [TemplateVisualState(Name = "Active", GroupName = "SwitchStates")]
    [TemplateVisualState(Name = "Deactive", GroupName = "SwitchStates")]
    public class ValueTypeSelector : Control
    {
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof (bool), typeof (ValueTypeSelector),
            new PropertyMetadata(new PropertyChangedCallback(OnIsActiveChangedCallback)));

        public static readonly DependencyProperty ActiveCommandProperty =
            DependencyProperty.Register("ActiveCommand", typeof (ICommand), typeof (ValueTypeSelector),
                                        new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty DeactiveCommandProperty =
            DependencyProperty.Register("DeactiveCommand", typeof (ICommand), typeof (ValueTypeSelector),
                                        new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty NoIsActiveProperty =
            DependencyProperty.Register("NoIsActive", typeof (bool), typeof (ValueTypeSelector),
                                        new PropertyMetadata(new PropertyChangedCallback(OnIsActiveChangedCallback)));

        static ValueTypeSelector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ValueTypeSelector),
                                                     new FrameworkPropertyMetadata(typeof (ValueTypeSelector)));
        }

        [Category("Common Properties")]
        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        [Category("Common Properties")]
        public ICommand ActiveCommand
        {
            get { return (ICommand) GetValue(ActiveCommandProperty); }
            set { SetValue(ActiveCommandProperty, value); }
        }


        [Category("Common Properties")]
        public ICommand DeactiveCommand
        {
            get { return (ICommand) GetValue(DeactiveCommandProperty); }
            set { SetValue(DeactiveCommandProperty, value); }
        }

        public bool NoIsActive
        {
            get { return (bool) GetValue(NoIsActiveProperty); }
            set { SetValue(NoIsActiveProperty, value); }
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateItemStates(false);
        }


        protected virtual void UpdateItemStates(bool useTransitions)
        {
            NoIsActive = !IsActive;

            VisualStateManager.GoToState(this, IsActive ? "Active" : "Deactive", useTransitions);
        }

        private static void OnIsActiveChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var sw = s as ValueTypeSelector;
            if (sw == null) return;
            var newValue = (bool) e.NewValue;

            sw.UpdateItemStates(true);

            /*            sw.OnIsActiveChanged(newValue);*/
        }
    }
}