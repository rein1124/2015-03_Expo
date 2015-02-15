using System;
using System.Globalization;
using System.Windows.Data;

namespace Hdc.Mvvm.Converters
{
    [ValueConversion(typeof (bool), typeof (bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return inverseBool(value);
        }

        private object inverseBool(object value)
        {
            if (!(value is bool))
            {
                return null;
            }
            var newValue = !(bool) value;
            return newValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return inverseBool(value);
        }
    }
}