using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace Hdc.Mvvm.Converters
{
    public abstract class ConverterMarkupExtension<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        private static T _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
            {
                _converter = new T();
            }
            return _converter;
        }

        public abstract object Convert(
            object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);

        public abstract object ConvertBack(
            object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);
    }
}