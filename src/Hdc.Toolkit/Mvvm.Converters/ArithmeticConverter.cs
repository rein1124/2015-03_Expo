using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Hdc.Mvvm.Converters
{
    public class ArithmeticConverter : ConverterMarkupExtension<ArithmeticConverter>
    {
        private const string ArithmeticParseExpression = "([+\\-*/]{1,1})\\s{0,}(\\-?[\\d\\.]+)";

        private Regex arithmeticRegex = new Regex(ArithmeticParseExpression);

        #region IValueConverter Members

        public override object Convert(
            object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null)
                return null;

            if (value is double)
                return CalculateDoubleValue(parameter, value);

            if (value is int)
            {
                var doubleValue = CalculateDoubleValue(parameter, value);
                return System.Convert.ToInt32(doubleValue);
            }

            Debug.WriteLine("Arithmetic error, value type is :" + value.GetType().FullName);

            return null;
        }

        private object CalculateDoubleValue(object parameter, object doubleValue)
        {
            string param = parameter.ToString();

            if (param.Length > 0)
            {
                Match match = arithmeticRegex.Match(param);
                if (match != null && match.Groups.Count == 3)
                {
                    string operation = match.Groups[1].Value.Trim();
                    string numericValue = match.Groups[2].Value;

                    double number = 0;
                    if (double.TryParse(numericValue, out number))
                        // this should always succeed or our regex is broken
                    {
                        double valueAsDouble = System.Convert.ToDouble(doubleValue);
                        double returnValue = 0;

                        switch (operation)
                        {
                            case "+":
                                returnValue = valueAsDouble + number;
                                break;

                            case "-":
                                returnValue = valueAsDouble - number;
                                break;

                            case "*":
                                returnValue = valueAsDouble * number;
                                break;

                            case "/":
                                returnValue = valueAsDouble / number;
                                break;
                        }

                        return returnValue;
                    }
                }
            }

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}