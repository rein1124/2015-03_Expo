using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Hdc.Mvvm.Navigation
{
    public class SetScreenNameBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            ScreenManager.SetScreenName(AssociatedObject, ScreenName);
        }

        #region ScreenName

        public string ScreenName
        {
            get { return (string) GetValue(ScreenNameProperty); }
            set { SetValue(ScreenNameProperty, value); }
        }

        public static readonly DependencyProperty ScreenNameProperty = DependencyProperty.Register(
            "ScreenName", typeof (string), typeof (SetScreenNameBehavior));

        #endregion
    }
}