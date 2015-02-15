using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;

namespace Hdc.Mvvm.Converters
{
    public class ThicknessArithmeticConverter : ConverterMarkupExtension<ArithmeticConverter>
    {
        private const string ArithmeticParseExpression = "([+\\-*/]{1,1})\\s{0,}(\\-?[\\d\\.]+)";

        private Regex arithmeticRegex = new Regex(ArithmeticParseExpression);

        public override object Convert(
            object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double && parameter != null)
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
                        if (double.TryParse(numericValue, out number)) // this should always succeed or our regex is broken
                        {
                            double valueAsDouble = (double)value;
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

                            return new Thickness(returnValue);
                        }
                    }
                }
            }

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}