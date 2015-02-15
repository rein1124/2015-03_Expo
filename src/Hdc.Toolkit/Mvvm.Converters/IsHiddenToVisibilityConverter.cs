using System;
using System.Globalization;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Data;

namespace Hdc.Mvvm.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class IsHiddenToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = System.Convert.ToBoolean(value);
            return boolValue ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vis = (Visibility) value;
            if (vis == Visibility.Visible)
                return false;
            if (vis == Visibility.Collapsed)
                return true;
            if (vis == Visibility.Hidden)
                return true;
            throw new InvalidOperationException();
        }
    }
}