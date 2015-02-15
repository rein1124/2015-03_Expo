using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Navigation
{
    public abstract class ScreenBase : UserControl
    {
        private IScreen _screen;

        [Dependency]
        public IScreenProvider ScreenProvider { get; set; }


        public IScreen Screen
        {
            get { return _screen; }
        }

        [InjectionMethod]
        public virtual void Init()
        {
            var screenTreeName = GetScreenName();
            _screen = ScreenProvider.FindScreen(screenTreeName);
            DataContext = _screen;
            BindingOperations.SetBinding(this, VisibilityProperty,
                                         new Binding("IsActive")
                                             {
                                                 Source = _screen,
                                                 Converter = new BooleanToVisibilityConverter(),
                                                 Mode = BindingMode.OneWay
                                             });
        }

        public abstract string GetScreenName();
    }
}