using System;
using System.Windows.Markup;

namespace Hdc.Mvvm
{
    public class ViewModelExtensions : MarkupExtension
    {
        private readonly string _viewModelName;

        public ViewModelExtensions(string viewModelName)
        {
            _viewModelName = viewModelName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ViewModelManager.Singleton.ViewModelProvider.GetViewModel(_viewModelName);
        }
    }
}