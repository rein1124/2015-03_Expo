using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Hdc.Mvvm.Converters
{
    public class IndexOfListConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return GetIndex(values);
        }

        public static object GetIndex(object[] values)
        {
            if (values == null) return -1;
            if (values.Length < 2) return -1;
            var offset = 0;
            var list = values[0] as IList;
            if (list == null) return -1;
            
            var element = values[1] as object;
            if (element == null) return -1;
            if(values.Length>2)
            {
                offset = System.Convert.ToInt32(values[2]);
            } 
            var index = list.IndexOf(element);
            return (index + offset);

            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        
    }
}