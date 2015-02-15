using System.ComponentModel;
using System.Windows;

namespace Hdc.Mvvm.DesignTime
{
    public abstract class DesignTimeViewModelProvider<T>
    {
        public T ViewModel
        {
            get
            {
                if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                {
                    return GetDesignTimeViewModel();
                }

                return GetRunTimeViewModel();
            }
        }

        protected abstract T GetRunTimeViewModel();
        protected abstract T GetDesignTimeViewModel();
    }

    public abstract class DesignTimeViewModelProvider : DesignTimeViewModelProvider<object>
    {
    }
}