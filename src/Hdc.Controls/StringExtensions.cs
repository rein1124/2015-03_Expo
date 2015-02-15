using System;
using System.Reflection;

namespace Hdc.Controls
{
    public static class StringExtensions
    {

        /// <summary>
        /// Get if string is null or the empty string.
        /// </summary>
        public static bool IsNullOrEmpty(this string @string)
        {
            return String.IsNullOrEmpty(@string);
        }

        #region FormatWith

        /// <summary>
        /// Replaces the format items in the <see cref="format"/> string
        /// with the text equivalents of the corresponding arguments.
        /// </summary>
        public static string FormatWith(this string format, params object[] args)
        {
            return String.Format(format, args);
        }

        /// <summary>
        /// Replaces the format item in the <see cref="format"/> string
        /// with the text equivalent of the corresponding argument.
        /// </summary>
        public static string FormatWith(this string format, object arg0)
        {
            return String.Format(format, arg0);
        }

        /// <summary>
        /// Replaces the format items in the <see cref="format"/> string
        /// with the text equivalents of the corresponding arguments.
        /// </summary>
        public static string FormatWith(this string format, object arg0, object arg1)
        {
            return String.Format(format, arg0, arg1);
        }

        /// <summary>
        /// Replaces the format items in the <see cref="format"/> string
        /// with the text equivalents of the corresponding arguments.
        /// </summary>
        public static string FormatWith(this string format, object arg0, object arg1, object arg2)
        {
            return String.Format(format, arg0, arg1, arg2);
        }

        #endregion

        #region PackUri

        public static Uri PackUri(this string relativeFile)
        {
            var uriString = "/{0};component/{1}".FormatWith(AssemblyShortName, relativeFile);
            return new Uri(uriString, UriKind.Relative);
        }

        private static string AssemblyShortName
        {
            get
            {
                if (_assemblyShortName == null)
                {
                    Assembly asm = typeof(StringExtensions).Assembly;

                    // Pull out the short name.
                    _assemblyShortName = asm.ToString().Split(',')[0];
                }

                return _assemblyShortName;
            }
        }

        private static string _assemblyShortName;

        #endregion

    }
}