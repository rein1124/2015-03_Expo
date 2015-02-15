// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanToGrayscaleConverter.cs" company="Catel development team">
//   Copyright (c) 2008 - 2011 Catel development team. All rights reserved.
// </copyright>
// <summary>
//   Converts a boolean to a grayscale saturation value. If the input is <c>false</c>, this converter will
//   return <c>0</c>, otherwise <c>1</c>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Data;

namespace Hdc.Controls
{
    /// <summary>
    /// Converts a boolean to a grayscale saturation value. If the input is <c>false</c>, this converter will
    /// return <c>0</c>, otherwise <c>1</c>.
    /// </summary>
#if !SILVERLIGHT
    [ValueConversion(typeof(bool), typeof(double))]
#endif
    public class BooleanToGrayscaleConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is bool))
            {
                return 1d;
            }

            return ((bool) value) ? 1d : 0d;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ConverterHelper.DoNothingBindingValue;
        }
    }
}
