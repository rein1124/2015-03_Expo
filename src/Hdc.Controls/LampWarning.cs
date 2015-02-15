using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
    [TemplateVisualState(Name="Active",GroupName = "ShowState")]
    [TemplateVisualState(Name="Deactive",GroupName = "ShowState")]
    public class LampWarning : Control
    {

        public static readonly DependencyProperty IsShowProperty = DependencyProperty.Register(
            "IsShow", typeof(bool), typeof(LampWarning),
            new PropertyMetadata(new PropertyChangedCallback(OnIsShowChangedCallback)));

        static LampWarning()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (LampWarning),
                                                     new FrameworkPropertyMetadata(typeof (LampWarning)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateStates(false);
        }

        [Category("Common Properties")]
        //[DisplayName(@"IsActive")]
        //[Description("Gets or sets the value of active state")]
        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        private static void OnIsShowChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var sw = s as LampWarning;
            if (sw == null) return;
            var newValue = (bool)e.NewValue;

            sw.UpdateStates(true);

            /*            sw.OnIsActiveChanged(newValue);*/
        }

        protected virtual void UpdateStates(bool useTransitions)
        {
            if (IsShow)
            {
                VisualStateManager.GoToState(this, "Active", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Deactive", useTransitions);
            }
        }
    }
}