using System;
using System.Collections;

namespace Hdc.Mercury
{
    public static class SBCDHelper
    {
        public static int ConvertFrom(short value, int signIndex, int length)
        {
            var array = new BitArray(BitConverter.GetBytes(value));
            var sign = array[signIndex];
            array[signIndex] = false;
            var temp = new int[1];
            array.CopyTo(temp, 0);
            var ret = Convert.ToInt32(Convert.ToString(temp[0], 16));
            return sign ? 0 - ret : ret;
        }

        public static short ConvertTo(int value, int signIndex)
        {
            var retArray = new BitArray(BitConverter.GetBytes(Convert.ToInt16(Math.Abs((short)value).ToString(), 16)));
            retArray[signIndex] = value < 0;
            var temp = new int[1];
            retArray.CopyTo(temp, 0);
            return (short)temp[0];
        }
    }
}
