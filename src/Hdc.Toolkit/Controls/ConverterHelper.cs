// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConverterHelper.cs" company="Catel development team">
//   Copyright (c) 2008 - 2011 Catel development team. All rights reserved.
// </copyright>
// <summary>
//    Converter helper class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if SILVERLIGHT
using System.Windows;
#else
using System.Windows.Data;
#endif

namespace Hdc.Controls
{
    /// <summary>
    /// Converter helper class.
    /// </summary>
    public static class ConverterHelper
    {
        /// <summary>
        /// The generic <c>DoNothing</c> value, compatible with WPF and Silverlight.
        /// </summary>
        public static readonly object DoNothingBindingValue =
#if SILVERLIGHT
        DependencyProperty.UnsetValue;
#else
        Binding.DoNothing;
#endif
    }
}
