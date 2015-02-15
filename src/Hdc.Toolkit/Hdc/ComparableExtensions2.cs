using System;

namespace Hdc
{
    public static class ComparableExtensions2
    {
        public static T Limit<T>(this T value, T minValue, T maxValue) where T : IComparable
        {
            if (-1 == value.CompareTo(minValue))
                return minValue;
            if (1 == value.CompareTo(maxValue))
                return maxValue;
            return value;
        }
    }
}