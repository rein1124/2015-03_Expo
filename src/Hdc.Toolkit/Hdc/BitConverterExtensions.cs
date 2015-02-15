using System;

namespace Hdc
{
    public static class BitConverterExtensions
    {
        public static int Get4Bit(this int intValue, int index)
        {
            if (index > 3)
                throw new NotSupportedException("this methed cannot convert value bigger than 16 bit");

            var offsetValue = intValue >> (index*4);
            var mask = 15;

            var v = offsetValue & mask;
            return v;
        }

        public static int Reset4Bit(this int intValue, int index)
        {
            if (index > 3)
                throw new NotSupportedException("this methed cannot convert value bigger than 16 bit");

            var mask = 0;
            Switch
                .On(index)
                .Case(0, () => mask = 65520)
                .Case(1, () => mask = 65295)
                .Case(2, () => mask = 61695)
                .Case(3, () => mask = 4095)
                ;

            var v = intValue & mask;
            return v;
        }

        public static int Insert4Bit(this int intValue, int insertValue, int index)
        {
            if (index > 3)
                throw new NotSupportedException("this methed cannot convert value bigger than 16 bit");

            var insert4BitValue = insertValue.Get4Bit(0);
            var reset4BitValue = intValue.Reset4Bit(index);

            var mask = 0;
            Switch
                .On(index)
                .Case(0, () => mask = insert4BitValue)
                .Case(1, () => mask = insert4BitValue*16)
                .Case(2, () => mask = insert4BitValue*16*16)
                .Case(3, () => mask = insert4BitValue*16*16*16)
                ;

            var v = reset4BitValue | mask;
            return v;
        }

        public static int Insert4BitWith(this int oriValue, int value0)
        {
            return oriValue.Insert4Bit(value0, 0);
        }

        public static int Insert4BitWith(this int oriValue, int value0, int value1)
        {
            var v = oriValue
                .Insert4BitWith(value0)
                .Insert4Bit(value1, 1);
            return v;
        }

        public static int Insert4BitWith(this int oriValue, int value0, int value1, int value2)
        {
            var v = oriValue
                .Insert4BitWith(value0, value1)
                .Insert4Bit(value2, 2);
            return v;
        }
    }
}