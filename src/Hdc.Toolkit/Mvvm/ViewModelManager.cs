using System.ComponentModel;
using System.Windows;

namespace Hdc.Mvvm
{
    public sealed class ViewModelManager : DependencyObject
    {
        private static readonly ViewModelManager _singleton = new ViewModelManager();

        private ViewModelManager()
        {
        }

        private IViewModelProvider _viewModelProvider;

        public IViewModelProvider ViewModelProvider
        {
            get { return _viewModelProvider; }
            set { _viewModelProvider = value; }
        }

        public static ViewModelManager Singleton
        {
            get { return _singleton; }
        }

        public static string GetViewModelName(DependencyObject obj)
        {
            return (string) obj.GetValue(ViewModelNameProperty);
        }

        public static void SetViewModelName(DependencyObject obj, string value)
        {
            obj.SetValue(ViewModelNameProperty, value);
        }

        public static readonly DependencyProperty ViewModelNameProperty =
            DependencyProperty.RegisterAttached("ViewModelName",
                                                typeof (string),
                                                typeof (ViewModelManager),
                                                new PropertyMetadata(OnPropertyChangedCallback));

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(s))
                return;

            var fe = s as FrameworkElement;
            if (fe == null) return;
            var vmName = e.NewValue as string;
            if (string.IsNullOrEmpty(vmName)) return;

            var vm = _singleton.ViewModelProvider.GetViewModel(vmName);
            fe.DataContext = vm;
        }
         
        

    }
}