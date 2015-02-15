using System;
using System.Collections.Generic;
using System.Linq;
using Hdc.Linq;

namespace Hdc
{
    public static class StringExtensions3
    {
        /// <summary>
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        ///   Indicates if the current <see cref = "string" /> is NOT <c>null</c> or <see cref = "string.Empty" />.
        /// </summary>
        public static bool IsNotNullOrEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        ///   Indicates if the current <see cref = "string" /> is <c>null</c> or <see cref = "string.Empty" />.
        /// </summary>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        ///   Indicates if the current <see cref = "string" /> is <c>null</c>, <see cref = "string.Empty" /> or just whitespace.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        ///   Indicates if the current <see cref = "string" /> is <b>not</b> <c>null</c>, <see cref = "string.Empty" /> or just whitespace.
        /// </summary>
        public static bool IsNotNullOrWhiteSpace(this string source)
        {
            return !string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        ///   Indicates if all characters in the supplied <paramref name = "value" /> are UPPER case.
        /// </summary>
        public static bool IsUpper(this IEnumerable<char> value)
        {
            return !value.Any(c => char.IsLower(c));
        }

        /// <summary>
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        ///   Indicates if all characters in the supplied <paramref name = "value" /> are lower case.
        /// </summary>
        public static bool IsLower(this IEnumerable<char> value)
        {
            return !value.Any(c => char.IsUpper(c));
        }

        /// <summary>
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        /// Joins an arbitrary collection of strings using the specified separator.
        /// </summary>
        public static string Join(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values.ToArray());
        }
    }
}