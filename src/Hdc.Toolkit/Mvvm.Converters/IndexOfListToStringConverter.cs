using System;
using System.Globalization;
using System.Windows.Data;

namespace Hdc.Mvvm.Converters
{
    public class IndexOfListToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
//            var offset = 0;
//            var list = values[0] as IList;
//            var element = values[1] as object;
//            if(values.Length>2)
//            {
//                offset = (int)values[2];
//            }
//            if (list == null) return -1;
//            var index = list.IndexOf(element);
//            return (index+offset).ToString();

            var index =  IndexOfListConverter.GetIndex(values);
            return index!=null ? index.ToString() : null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}