using System.Windows;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm
{
    using System.Windows.Controls;

    public abstract class ViewBase<TView, TViewModel> : Control
        where TView : ViewBase<TView, TViewModel> where TViewModel : class
    {
        static ViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (TView), new FrameworkPropertyMetadata(typeof (TView)));
        }

        [Dependency]
        public TViewModel ViewModel
        {
            get { return DataContext as TViewModel; }

            set { DataContext = value; }
        }
    }
}