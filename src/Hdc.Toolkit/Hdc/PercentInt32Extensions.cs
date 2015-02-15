namespace Hdc
{
    public static class PercentInt32Extensions
    {
        public static int ToPercentInt32FromDecimal(this decimal value)
        {
            return (int)(value * 100);
        }

        public static int ToPercentInt32FromDouble(this double value)
        {
            return (int)(value * 100);
        }

        public static decimal ToDecimalFromPercentInt32(this int value)
        {
            return value / 100.0m;
        }

        public static double ToDoubleFromPercentInt32(this int value)
        {
            return value / 100.0;
        }

        public static double ToQuadrupleFromPercentInt32(this int value)
        {
            return value / 10000.0;
        }
    }
}