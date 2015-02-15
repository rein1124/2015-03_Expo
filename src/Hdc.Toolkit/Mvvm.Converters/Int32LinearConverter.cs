using System;
using System.Globalization;
using System.Windows.Data;

namespace Hdc.Mvvm.Converters
{
    public class Int32LinearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int y = default(int);
            var x = System.Convert.ToDouble(value);
            var param = (double[]) parameter;
            if (param.Length != 2) return y;
            y = (int) (x*param[0] + param[1]);
            return y;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double x = default(double);
            var y = System.Convert.ToDouble(value);
            var param = (double[]) parameter;
            if (param.Length != 2) return x;
            x = (y - param[1])/param[0];
            return x;
        }
    }
}