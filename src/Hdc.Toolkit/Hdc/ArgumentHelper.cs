using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Hdc
{
    public static class ArgumentHelper
    {
        [DebuggerHidden]
        public static void AssertNotNull<T>(T arg, string argName) where T : class
        {
            if (arg == null)
                throw new ArgumentNullException(argName);
        }

        [DebuggerHidden]
        public static void AssertNotNull<T>(Nullable<T> arg, string argName) where T : struct
        {
            if (!arg.HasValue)
                throw new ArgumentNullException(argName);
        }

        [DebuggerHidden]
        public static void AssertGenericArgumentNotNull<T>(T arg, string argName)
        {
            Type type = typeof(T);

            if (!type.IsValueType || (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>))))
                AssertNotNull((object)arg, argName);
        }

        [DebuggerHidden]
        public static void AssertNotNull<T>(IEnumerable<T> arg, string argName, bool assertContentsNotNull)
        {
            //make sure the enumerable item itself isn't null
            AssertNotNull(arg, argName);

            if (assertContentsNotNull && typeof(T).IsClass)
            {
                //make sure each item in the enumeration isn't null
                foreach (T item in arg)
                {
                    if (item == null)
                        throw new ArgumentException("An item inside the enumeration was null.", argName);
                }
            }
        }

        [DebuggerHidden]
        public static void AssertNotNullOrEmpty(string arg, string argName)
        {
            AssertNotNullOrEmpty(arg, argName, false);
        }

        [DebuggerHidden]
        public static void AssertNotNullOrEmpty(string arg, string argName, bool trim)
        {
            if (string.IsNullOrEmpty(arg) || (trim && IsOnlyWhitespace(arg)))
                throw new ArgumentException("Cannot be null or empty.", argName);
        }

        [DebuggerHidden]
        [CLSCompliant(false)]
        public static void AssertEnumMember<TEnum>(TEnum enumValue, string argName) where TEnum : struct, IConvertible
        {
            if (Attribute.IsDefined(typeof(TEnum), typeof(FlagsAttribute), false))
            {
                //flag enumeration - we can only get here if TEnum is a valid enumeration type, since the FlagsAttribute can
                //only be applied to enumerations
                bool throwEx;
                long longValue = enumValue.ToInt64(CultureInfo.InvariantCulture);

                if (longValue == 0)
                {
                    //only throw if zero isn't defined in the enum - we have to convert zero to the underlying type of the enum
                    throwEx =
                            !Enum.IsDefined(
                                     typeof(TEnum),
                                     ((IConvertible)0).ToType(
                                             Enum.GetUnderlyingType(typeof(TEnum)), CultureInfo.InvariantCulture));
                }
                else
                {
                    foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
                    {
                        longValue &= ~value.ToInt64(CultureInfo.InvariantCulture);
                    }

                    //throw if there is a value left over after removing all valid values
                    throwEx = (longValue != 0);
                }

                if (throwEx)
                {
                    throw new ArgumentException(
                            string.Format(
                                    CultureInfo.InvariantCulture,
                                    "Enum value '{0}' is not valid for flags enumeration '{1}'.",
                                    enumValue,
                                    typeof(TEnum).FullName),
                            argName);
                }
            }
            else
            {
                //not a flag enumeration
                if (!Enum.IsDefined(typeof(TEnum), enumValue))
                {
                    throw new ArgumentException(
                            string.Format(
                                    CultureInfo.InvariantCulture,
                                    "Enum value '{0}' is not defined for enumeration '{1}'.",
                                    enumValue,
                                    typeof(TEnum).FullName),
                            argName);
                }
            }
        }

        [DebuggerHidden]
        [CLSCompliant(false)]
        public static void AssertEnumMember<TEnum>(TEnum enumValue, string argName, params TEnum[] validValues)
                where TEnum : struct, IConvertible
        {
            AssertNotNull(validValues, "validValues");

            if (Attribute.IsDefined(typeof(TEnum), typeof(FlagsAttribute), false))
            {
                //flag enumeration
                bool throwEx;
                long longValue = enumValue.ToInt64(CultureInfo.InvariantCulture);

                if (longValue == 0)
                {
                    //only throw if zero isn't permitted by the valid values
                    throwEx = true;

                    foreach (TEnum value in validValues)
                    {
                        if (value.ToInt64(CultureInfo.InvariantCulture) == 0)
                        {
                            throwEx = false;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (TEnum value in validValues)
                    {
                        longValue &= ~value.ToInt64(CultureInfo.InvariantCulture);
                    }

                    //throw if there is a value left over after removing all valid values
                    throwEx = (longValue != 0);
                }

                if (throwEx)
                {
                    throw new ArgumentException(
                            string.Format(
                                    CultureInfo.InvariantCulture,
                                    "Enum value '{0}' is not allowed for flags enumeration '{1}'.",
                                    enumValue,
                                    typeof(TEnum).FullName),
                            argName);
                }
            }
            else
            {
                //not a flag enumeration
                foreach (TEnum value in validValues)
                {
                    if (enumValue.Equals(value))
                        return;
                }

                //at this point we know an exception is required - however, we want to tailor the message based on whether the
                //specified value is undefined or simply not allowed
                if (!Enum.IsDefined(typeof(TEnum), enumValue))
                {
                    throw new ArgumentException(
                            string.Format(
                                    CultureInfo.InvariantCulture,
                                    "Enum value '{0}' is not defined for enumeration '{1}'.",
                                    enumValue,
                                    typeof(TEnum).FullName),
                            argName);
                }
                else
                {
                    throw new ArgumentException(
                            string.Format(
                                    CultureInfo.InvariantCulture,
                                    "Enum value '{0}' is defined for enumeration '{1}' but it is not permitted in this context.",
                                    enumValue,
                                    typeof(TEnum).FullName),
                            argName);
                }
            }
        }

        private static bool IsOnlyWhitespace(string arg)
        {
            Debug.Assert(arg != null);

            foreach (char c in arg)
            {
                if (!char.IsWhiteSpace(c))
                    return false;
            }

            return true;
        }
    }
}