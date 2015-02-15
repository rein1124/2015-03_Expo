using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hdc.Mvvm.Navigation
{
    public sealed class ScreenManager : DependencyObject
    {
        private static readonly ScreenManager _singleton = new ScreenManager();

        private ScreenManager()
        {
        }

        public static ScreenManager Singleton
        {
            get { return _singleton; }
        }

        #region ScreenName

        public static string GetScreenName(DependencyObject obj)
        {
            return (string) obj.GetValue(ScreenNameProperty);
        }

        public static void SetScreenName(DependencyObject obj, string value)
        {
            obj.SetValue(ScreenNameProperty, value);
        }

        public IScreenProvider ScreenProvider { get; set; }

        public static readonly DependencyProperty ScreenNameProperty = DependencyProperty.RegisterAttached(
            "ScreenName",typeof (string),typeof (ScreenManager),new PropertyMetadata(OnPropertyChangedCallback));

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var target = s as FrameworkElement;
            if (target == null)
            {
                return;
            }

            var vmName = e.NewValue as string;

            if (string.IsNullOrEmpty(vmName)) return;
            if (_singleton.ScreenProvider==null)
            {
                Debug.WriteLine("ScreenProvider is null!");
                return;
            }
            var vm = _singleton.ScreenProvider.FindScreen(vmName);

            target.DataContext = vm;

            BindingOperations.SetBinding(target,UIElement.VisibilityProperty,
                                         new Binding("IsActive")
                                         {
                                             Source = vm,
                                             Converter = new BooleanToVisibilityConverter(),
                                             Mode = BindingMode.OneWay
                                         });
        }

        #endregion
    }
}