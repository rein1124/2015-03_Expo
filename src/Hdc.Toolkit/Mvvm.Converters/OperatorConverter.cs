using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Hdc.Mvvm.Converters
{
    public class OperatorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var count = values.Count();
            var f1 = (double)values[0];
            for (var i = 1; i < count; i += 2)
            {
                var op = (string)(values[i]);
                var f2 = (double)(values[i + 1]);

                f1 = Calculate(f1, f2, op);
            }

            return f1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        private static double Calculate(double f1, double f2, string operatorString)
        {
            var result = f1;
            switch (operatorString)
            {
                case "+":
                    result += f2;

                    break;
                case "-":

                    result -= f2;

                    break;
                case "*":

                    result *= f2;

                    break;
                case "/":

                    result /= f2;

                    break;
            }
            return result;
        }
    }
}