using System;
using System.Globalization;
using System.Windows.Data;

namespace Hdc.Controls
{
    public class PressUnitSuspendedStateConverter :  IMultiValueConverter 
    {
//        public PressUnitSuspendedStateConverter(bool isUnitSuspended, bool isPressSuspended)
//        {
//
//        }
// 

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isUnitSuspended;
            bool isPressSuspended;

            isUnitSuspended = System.Convert.ToBoolean(values[0]);
            isPressSuspended = System.Convert.ToBoolean(values[1]);

            if (isPressSuspended)
            {
                return PressUnitSuspendedState.PressSuspended;
            }

            if(isUnitSuspended)
            {
                return PressUnitSuspendedState.UnitSuspended;
            }

            return PressUnitSuspendedState.Unsuspended;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}