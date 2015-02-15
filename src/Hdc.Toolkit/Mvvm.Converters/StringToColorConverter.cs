using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;

namespace Hdc.Mvvm.Converters
{
    public class StringToColorConverter : ConverterMarkupExtension<StringToColorConverter>
    {
        #region FIELD

        private static IDictionary<string, Color> _colors = new Dictionary<string, Color>();

        //        private static IDictionary<Color, string> _names = new Dictionary<Color, string>();

        //        private static IDictionary<Color, IList<string>> _colorToStringMap = new Dictionary<Color, IList<string>>();

        #endregion

        #region CONSTRUCTOR

        static StringToColorConverter()
        {
            var properties = typeof(Colors).GetProperties();
            var names = properties.Select(p => p.Name);

            foreach (var name in names)
            {
                var color = (Color)ColorConverter.ConvertFromString(name);
                _colors.Add(name, color);
            }
        }

        #endregion

        #region PROPERTY

        #endregion

        #region METHOD

        #endregion

        #region Convert

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertToColor(value as string);
        }

        public static Color ConvertToColor(string colorName)
        {
            //            if (!_colors.ContainsKey(colorName))
            //            {
            //                var tempColor = (Color)ColorConverter.ConvertFromString(colorName);
            //
            //                _names.Add(tempColor, colorName);
            //                _colors.Add(colorName, tempColor);
            //            }
            return _colors[colorName];
        }

        #endregion

        #region ConvertBack

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //            return value is Color ? ConvertToString((Color)value) : null;
            throw new NotSupportedException();
        }

        //        public static string ConvertToString(Color color)
        //        {
        //            if (!_names.ContainsKey(color))
        //            {
        //                var ps = typeof(Colors).GetProperties();
        //                foreach (var propertyInfo in ps)
        //                {
        //                    var tempColor = (Color)ColorConverter.ConvertFromString(propertyInfo.Name);
        //                    if (tempColor == color)
        //                    {
        //                        _names.Add(color, propertyInfo.Name);
        //                        _colors.Add(propertyInfo.Name, color);
        //
        //                        break;
        //                    }
        //                }
        //            }
        //            return _names[color];
        //        }

        #endregion
    }
}