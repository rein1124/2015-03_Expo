using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hdc
{
    public static class BooleanExtensions
    {
        public static Int16 ToInt16(this IEnumerable<Boolean> booleans)
        {
            return BitConverter.ToInt16(GetBytes(booleans, 2), 0);
        }

        public static Int32 ToInt32(this IEnumerable<Boolean> booleans)
        {
            return BitConverter.ToInt32(GetBytes(booleans, 4), 0);
        }

        public static Int64 ToInt64(this IEnumerable<Boolean> booleans)
        {
            return BitConverter.ToInt64(GetBytes(booleans, 8), 0);
        }

        public static UInt16 ToUInt16(this IEnumerable<Boolean> booleans)
        {
            return BitConverter.ToUInt16(GetBytes(booleans, 2), 0);
        }

        public static UInt32 ToUInt32(this IEnumerable<Boolean> booleans)
        {
            return BitConverter.ToUInt32(GetBytes(booleans, 4), 0);
        }

        public static UInt64 ToUInt64(this IEnumerable<Boolean> booleans)
        {
            return BitConverter.ToUInt64(GetBytes(booleans, 8), 0);
        }

        public static IEnumerable<bool> ToBooleans(this Byte value)
        {
            return GetBooleans(8, value);
        }

        public static IEnumerable<bool> ToBooleans(this SByte value)
        {
            return GetBooleans(8, value);
        }

        public static IEnumerable<bool> ToBooleans(this Int16 value)
        {
            return GetBooleans(16, value);
        }

        public static IEnumerable<bool> ToBooleans(this UInt16 value)
        {
            return GetBooleans(16, value);
        }

        public static IEnumerable<bool> ToBooleans(this Int32 value)
        {
            return GetBooleans(32, value);
        }

        public static IEnumerable<bool> ToBooleans(this UInt32 value)
        {
            return GetBooleans(32, value);
        }

        public static IEnumerable<bool> ToBooleans(this Int64 value)
        {
            return GetBooleans(64, value);
        }

        public static IEnumerable<bool> ToBooleans(this UInt64 value)
        {
            return GetBooleans(64, value);
        }

        private static Boolean[] GetBooleans(int length, dynamic value)
        {
            var bools = new Boolean[length];
            new BitArray(BitConverter.GetBytes(value)).CopyTo(bools,0);
            return bools;
        }

        private static byte[] GetBytes(IEnumerable<bool> booleans, int byteLength)
        {
            var bytes = new Byte[byteLength];
            new BitArray(booleans.Take(byteLength*8).ToArray()).CopyTo(bytes, 0);
            return bytes;
        }
    }
}