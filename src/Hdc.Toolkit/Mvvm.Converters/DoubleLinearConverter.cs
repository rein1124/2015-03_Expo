using System;
using System.Globalization;
using System.Windows.Data;

namespace Hdc.Mvvm.Converters
{
    public class DoubleLinearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var y = default(double);
            var x = System.Convert.ToDouble(value);
            var param = (double[]) parameter;
            if (param.Length != 3) return y;
            y = Math.Round((x*param[0] + param[1]), (int) param[2]);
            return y;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var x = default(double);
            var y = System.Convert.ToDouble(value);
            var param = (double[]) parameter;
            if (param.Length != 3) return x;
            x = (y - param[1])/param[0];
            return x;
        }
    }
}