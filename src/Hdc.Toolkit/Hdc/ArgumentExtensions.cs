using System.Diagnostics;

namespace Hdc
{
    using System;
    using System.Collections.Generic;

    public static class ArgumentExtensions
    {
        /// <include file='ArgumentHelper.doc.xml' path='doc/member[@name="AssertNotNull{T}(T,string)"]/*' />
        [DebuggerHidden]
        public static void ArgumentIsNotNull<T>(this T arg, string argName) where T : class
        {
            ArgumentHelper.AssertNotNull(arg, argName);
        }

        /// <include file='ArgumentHelper.doc.xml' path='doc/member[@name="AssertNotNull{T}(Nullable{T},string)"]/*' />
        [DebuggerHidden]
        public static void ArgumentIsNotNull<T>(this Nullable<T> arg, string argName) where T : struct
        {
            ArgumentHelper.AssertNotNull(arg, argName);
        }

        /// <include file='ArgumentHelper.doc.xml' path='doc/member[@name="AssertGenericArgumentNotNull{T}(T,string)"]/*' />
        [DebuggerHidden]
        public static void ArgumentIsNotNullAndGeneric<T>(this T arg, string argName)
        {
            ArgumentHelper.AssertGenericArgumentNotNull(arg, argName);
        }

        /// <include file='ArgumentHelper.doc.xml' path='doc/member[@name="AssertNotNull{T}(IEnumerable{T},string,bool)"]/*' />
        [DebuggerHidden]
        public static void ArgumentIsNotNull<T>(this IEnumerable<T> arg, string argName, bool assertContentsNotNull)
        {
            ArgumentHelper.AssertNotNull(arg, argName, assertContentsNotNull);
        }

        /// <include file='ArgumentHelper.doc.xml' path='doc/member[@name="AssertNotNullOrEmpty(string,string)"]/*' />
        [DebuggerHidden]
        public static void ArgumentIsNotNullOrEmpty(this string arg, string argName)
        {
            ArgumentHelper.AssertNotNullOrEmpty(arg, argName);
        }

        /// <include file='ArgumentHelper.doc.xml' path='doc/member[@name="AssertNotNullOrEmpty(string,string,bool)"]/*' />
        [DebuggerHidden]
        public static void ArgumentIsNotNullOrEmpty(this string arg, string argName, bool trim)
        {
            ArgumentHelper.AssertNotNullOrEmpty(arg, argName, trim);
        }

        /// <include file='ArgumentHelper.doc.xml' path='doc/member[@name="AssertEnumMember{TEnum}(TEnum,string)"]/*' />
        [DebuggerHidden]
        [CLSCompliant(false)]
        public static void ArgumentIsEnumMember<TEnum>(this TEnum enumValue, string argName)
                where TEnum : struct, IConvertible
        {
            ArgumentHelper.AssertEnumMember(enumValue, argName);
        }

        /// <include file='ArgumentHelper.doc.xml' path='doc/member[@name="AssertEnumMember{TEnum}(TEnum,string,TEnum[])"]/*' />
        [DebuggerHidden]
        [CLSCompliant(false)]
        public static void ArgumentIsEnumMember<TEnum>(this TEnum enumValue, string argName, params TEnum[] validValues)
                where TEnum : struct, IConvertible
        {
            ArgumentHelper.AssertEnumMember(enumValue, argName, validValues);
        }
    }
}