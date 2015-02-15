using System.IO;

namespace Hdc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using IO;

    public static class SystemExtensions
    {
        public static TTarget ChangeType<TSource, TTarget>(this TSource source)
        {
           return (TTarget)System.Convert.ChangeType(source, typeof(TTarget)); 
        }

        public static T Convert<T>(this object source) where T : class
        {
            return source as T;
        }

        public static T ConvertValueType<T>(this object source)
        {
            return (T)source;
        }

        public static TEnum ParseEnum<TEnum>(this string str)
        {
            var obj = Enum.Parse(typeof(TEnum), str);
            return (TEnum)obj;
        }

        public static int ToInt32(this string str)
        {
            return System.Convert.ToInt32(str);
        }
        public static double ToDouble(this string str)
        {
            return System.Convert.ToDouble(str);
        }

        public static string Combine(this string pathFirst, string pathSecond)
        {
            return Path.Combine(pathFirst, pathSecond);
        }

        // replaced
/*        public static TEnum ToEnum<TEnum>(this string enumName)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), enumName);
        }*/

    }
}