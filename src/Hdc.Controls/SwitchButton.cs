using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Hdc.Controls
{
    [TemplateVisualState(Name = "Active", GroupName = "SwitchStates")]
    [TemplateVisualState(Name = "Deactive", GroupName = "SwitchStates")]
    public class SwitchButton : Button
    {
/*        public event RoutedEventHandler Activated;

        public event RoutedEventHandler Dectivated;*/

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof (bool), typeof (SwitchButton),
            new PropertyMetadata(new PropertyChangedCallback(OnIsActiveChangedCallback)));

        static SwitchButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (SwitchButton),
                                                     new FrameworkPropertyMetadata(typeof (SwitchButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateStates(false);
        }

        [Category("Common Properties")]
        //[DisplayName(@"IsActive")]
            //[Description("Gets or sets the value of active state")]
            public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private static void OnIsActiveChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var sw = s as SwitchButton;
            if (sw == null) return;
            var newValue = (bool) e.NewValue;

            sw.UpdateStates(true);

/*            sw.OnIsActiveChanged(newValue);*/
        }

        protected virtual void UpdateStates(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsActive ? "Active" : "Deactive", useTransitions);
        }

/*        protected virtual void OnIsActiveChanged(bool isActive)
        {
            if (isActive)
            {
                if (Activated != null)
                {
                    //TODO Activated(this, e);
                }
            }
            else
            {
                if (Dectivated != null)
                {
                    //TODO Dectivated(this, e);
                }
            }
        }*/
    }
}