using System;
using System.Globalization;
using System.Windows;

namespace Hdc.Mvvm.Converters
{
    public class BooleanToVisibilityConverter : ConverterMarkupExtension<BooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (!(value is bool))
                return null;
            var boolValue = System.Convert.ToBoolean(value);
            var isInvert = false;
            if (parameter != null && System.Convert.ToBoolean(parameter))
                isInvert = true;

            if (isInvert)
                return boolValue ? Visibility.Hidden : Visibility.Visible;
            else
                return boolValue ? Visibility.Visible : Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}