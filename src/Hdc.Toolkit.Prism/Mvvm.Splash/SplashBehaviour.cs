using System.Windows;

namespace Hdc.Mvvm
{

    public class SplashBehaviour
    {
        #region Dependency Properties
        public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
          "Enabled", typeof(bool), typeof(SplashBehaviour), new PropertyMetadata(OnEnabledChanged));

        public static bool GetEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledProperty);
        }

        public static void SetEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledProperty, value);
        }
        #endregion

        #region Event Handlers
        private static void OnEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var splash = obj as Window;
            if (splash != null && args.NewValue is bool && (bool)args.NewValue)
            {
                splash.Closed += (s, e) =>
                {
                    splash.DataContext = null;
                    splash.Dispatcher.InvokeShutdown();
                };
                splash.MouseDoubleClick += (s, e) => splash.Close();
                splash.MouseLeftButtonDown += (s, e) => splash.DragMove();
            }
        }
        #endregion
    }
}