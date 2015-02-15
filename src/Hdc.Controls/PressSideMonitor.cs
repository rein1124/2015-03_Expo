using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hdc.Controls
{
    [TemplateVisualState(Name = "UpperSide", GroupName = "SideStates")]
    [TemplateVisualState(Name = "LowerSide", GroupName = "SideStates")]
    public class PressSideMonitor : SwitchButton
    {
        public static readonly DependencyProperty SideIndexProperty = DependencyProperty.Register(
            "SideIndex", typeof (int), typeof (PressSideMonitor), new PropertyMetadata(OnSideIndexChangedCallback));

        public static readonly DependencyProperty DisplaySideIndexProperty = DependencyProperty.Register(
            "DisplaySideIndex", typeof (int), typeof (PressSideMonitor));

        public static readonly DependencyProperty SideNameProperty = DependencyProperty.Register(
            "SideName", typeof (string), typeof (PressSideMonitor));

        public static readonly DependencyProperty IsLowerSideProperty = DependencyProperty.Register(
            "IsLowerSide", typeof (bool), typeof (PressSideMonitor));

        static PressSideMonitor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (PressSideMonitor),
                                                     new FrameworkPropertyMetadata(typeof (PressSideMonitor)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateStates(false);
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
        public bool IsLowerSide
        {
            get { return (bool) GetValue(IsLowerSideProperty); }
            private set { SetValue(IsLowerSideProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public int DisplaySideIndex
        {
            get { return (int) GetValue(DisplaySideIndexProperty); }
            private set { SetValue(DisplaySideIndexProperty, value); }
        }

        protected override void UpdateStates(bool useTransitions)
        {
            base.UpdateStates(useTransitions);

            switch (SideIndex)
            {
                case 0:
                    VisualStateManager.GoToState(this, "UpperSide", useTransitions);
                    break;
                case 1:
                    VisualStateManager.GoToState(this, "LowerSide", useTransitions);
                    break;
            }
        }

        private static void OnSideIndexChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as PressSideMonitor;
            if (me == null) return;
            var newValue = (int) e.NewValue;

            me.UpdateStates(true);

            me.DisplaySideIndex = newValue + 1;
        }
    }
}