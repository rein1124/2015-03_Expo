using System;
using System.Globalization;

namespace Hdc.Mvvm.Converters
{
    public class Int32ToRomaConverter : ConverterMarkupExtension<Int32ToRomaConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (int)value;
            if (parameter != null)
                index += System.Convert.ToInt32(parameter);

            switch (index)
            {
                case 0:
                    return "I";
                case 1:
                    return "II";
                case 2:
                    return "III";
                case 3:
                    return "IV";
                case 4:
                    return "V";
                case 5:
                    return "VI";
                case 6:
                    return "VII";
                case 7:
                    return "VIII";
                case 8:
                    return "IX";
                case 9:
                    return "X";
                case 10:
                    return "XI";
            }
            return "";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}