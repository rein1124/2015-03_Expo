using System;

namespace Hdc
{
    public static class NullableExtensions
    {
        public static bool? ToNullableBoolean(this int? value)
        {
            if (value == null)
                return null;

            return Convert.ToBoolean(value.Value);
        }

        public static int? ToNullableInt32(this bool? value)
        {
            if (value == null)
                return null;

            return Convert.ToInt32(value.Value);
        }
    }
}