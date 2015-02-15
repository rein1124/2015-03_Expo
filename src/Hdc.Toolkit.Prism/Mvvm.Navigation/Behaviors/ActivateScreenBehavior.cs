using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Mvvm.Navigation
{
    public class ActivateScreenBehavior : Behavior<ButtonBase>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            ButtonBase b = AssociatedObject as ButtonBase;

            var cmd = new DelegateCommand(
                () =>
                {
                    var p = ServiceLocator.Current.GetInstance<IScreenProvider>();
                    var s = p.FindScreen(ScreenName);

                    if (s != null)
                    {
                        s.Activate();
                    }
                });
            b.Command = cmd;
        }

        #region ScreenName

        public string ScreenName
        {
            get { return (string)GetValue(ScreenNameProperty); }
            set { SetValue(ScreenNameProperty, value); }
        }

        public static readonly DependencyProperty ScreenNameProperty = DependencyProperty.Register(
            "ScreenName", typeof(string), typeof(SetScreenNameBehavior));

        #endregion

        #region Parameter

        public object Parameter
        {
            get { return (object)GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }

        public static readonly DependencyProperty ParameterProperty = DependencyProperty.Register(
            "Parameter", typeof(object), typeof(ActivateScreenBehavior));

        #endregion
    }
}