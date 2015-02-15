using System;
using System.Globalization;
using System.Windows.Data;

namespace Hdc.Mv.Inspection.Halcon.SampleApp
{
    public class InvertConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var value2 = System.Convert.ToDouble(value);
            var invert = value2*-1;
            return invert;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var value2 = System.Convert.ToDouble(value);
            var invert = value2 * -1;
            return invert;
        }
    }
}