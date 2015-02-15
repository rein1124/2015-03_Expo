using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Hdc.Mvvm.Converters
{
    public class BooleansToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;

            if (!(values[0] is bool))
                return null;

            if (!(values[1] is bool))
                return null;

            var isVisible = System.Convert.ToBoolean(values[0]);
            var isCollapsed = System.Convert.ToBoolean(values[1]);

            if (isCollapsed)
                return Visibility.Collapsed;

            if (isVisible)
                return Visibility.Visible;

            return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}