using System;
using System.Globalization;
using System.Windows.Media;

namespace Hdc.Mvvm.Converters
{
    public class StringToBrushConverter : ConverterMarkupExtension<StringToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            return ConvertToBrush(value as string);
        }

        public static Brush ConvertToBrush(string name)
        {
            return new SolidColorBrush(StringToColorConverter.ConvertToColor(name));
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw  new NotSupportedException();
        }

    }
}