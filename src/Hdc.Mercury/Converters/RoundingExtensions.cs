using System;

namespace Hdc.Mercury.Converters
{
    public static class RoundingExtensions
    {
        public static bool CheckCanRounding(this object value)
        {
            if (value is byte) return true;
            if (value is sbyte) return true;
            if (value is short) return true;
            if (value is ushort) return true;
            if (value is int) return true;
            if (value is uint) return true;
            if (value is long) return true;
            if (value is ulong) return true;
            if (value is float) return true;
            if (value is double) return true;

            return false;
        }

        public static T RoundValue<T>(this decimal dValue, RoundingMode mode)
        {
            object resultO = null;
            switch (mode)
            {
                case RoundingMode.None:
                    resultO = CutOffValue<T>(dValue);
                    break;
                case RoundingMode.Rounded:
                    resultO = CutOffValue<T>(dValue + 0.5m);
                    break;
                case RoundingMode.RoundedWithEven:
                    resultO = System.Convert.ChangeType(dValue, typeof (T));
                    break;
            }

            T resultT = (T) resultO;
            return resultT;
        }

        public static T CutOffValue<T>(this decimal originalValue)
        {
            object resultO = null;
            var type = typeof (T);
            T vT = default(T);

            if (vT is byte)
                resultO = (byte) (originalValue);
            else if (vT is sbyte)
                resultO = (sbyte) (originalValue);
            else if (vT is short)
                resultO = (short) (originalValue);
            else if (vT is ushort)
                resultO = (ushort) (originalValue);
            else if (vT is int)
                resultO = (int) (originalValue);
            else if (vT is uint)
                resultO = (uint) (originalValue);
            else if (vT is long)
                resultO = (long) (originalValue);
            else if (vT is ulong)
                resultO = (ulong) (originalValue);
            else if (vT is float)
                resultO = (float) (originalValue);
            else if (vT is double)
                resultO = (double) (originalValue);
            else
            {
                resultO = (int)(originalValue);
//                throw new InvalidCastException("cannot support this ValueConvertion: " + type.Name);
            }
            return (T) resultO;
        }

        public static object CutOffValue(this decimal originalValue, Type valueType)
        {
            object resultO = null;
            var type = valueType;

            if (type == typeof(byte))
                resultO = (byte) (originalValue);
            else if (type == typeof(sbyte))
                resultO = (sbyte) (originalValue);
            else if (type == typeof(short))
                resultO = (short) (originalValue);
            else if (type == typeof(ushort))
                resultO = (ushort) (originalValue);
            else if (type == typeof(int))
                resultO = (int) (originalValue);
            else if (type == typeof(uint))
                resultO = (uint) (originalValue);
            else if (type == typeof(long))
                resultO = (long) (originalValue);
            else if (type == typeof(ulong))
                resultO = (ulong) (originalValue);
            else if (type == typeof(float))
                resultO = (float) (originalValue);
            else if (type == typeof(double))
                resultO = (double) (originalValue);
            else
            {
                throw new InvalidCastException("cannot support this ValueConvertion: " + type.Name);
            }
            return resultO;
        }

        public static object RoundValue(this decimal dValue, Type valueType, RoundingMode mode)
        {
            object resultO = null;
            switch (mode)
            {
                case RoundingMode.None:
                    resultO = CutOffValue(dValue, valueType);
                    break;
                case RoundingMode.Rounded:
                    resultO = CutOffValue(dValue + 0.5m, valueType);
                    break;
                case RoundingMode.RoundedWithEven:
                    resultO = Convert.ChangeType(dValue, valueType);
                    break;
            }

            return resultO;
        }
    }
}