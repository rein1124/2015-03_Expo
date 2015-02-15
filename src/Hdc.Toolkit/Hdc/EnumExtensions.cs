namespace Hdc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumExtensions
    {
//        [Obsolete("MUST NOT BE USED, not safe")]
//        public static TEnum ToEnum<TEnum>(this string enumName)
//        {
//            return (TEnum)Enum.Parse(typeof(TEnum), enumName);
//        }

        public static TEnum ToEnum<TEnum>(this string enumString) where TEnum : struct
        {
            return enumString.ToEnum<TEnum>(default(TEnum));
        }

        public static TEnum ToEnum<TEnum>(this string enumString, TEnum defaultValue) where TEnum : struct
        {
            if (string.IsNullOrEmpty(enumString))
                return defaultValue;
            return (TEnum) Enum.Parse(typeof (TEnum), enumString);
        }

        //MEF_Beta_2.zip
        public static bool HasFlag(this Enum enumRef, Enum flag)
        {
            long value = Convert.ToInt64(enumRef);
            long flagVal = Convert.ToInt64(flag);

            return (value & flagVal) == flagVal;
        }
    }
}