namespace Hdc
{


    public class ValueRange<T>
    {
        public T Minimum { get; set; }

        public T Maximum { get; set; }

        public ValueRange()
        {
        }

        public ValueRange(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
    }

    public class DecimalValueRange : ValueRange<decimal>
    {
        public int DecimalPlaces { get; set; }

        public DecimalValueRange()
        {
        }

        public DecimalValueRange(decimal minimum, decimal maximum)
            : base(minimum, maximum)
        {
        }

        public DecimalValueRange(decimal minimum, decimal maximum, int decimalPlaces)
            : base(minimum, maximum)
        {
            DecimalPlaces = decimalPlaces;
        }

//        public DecimalValueRange()
//        {
//        }
//
//        public DecimalValueRange(decimal minimum, decimal maximum) : base(minimum, maximum)
//        {
//        }
    }

    public class Int32ValueRange : ValueRange<int>
    {
        public Int32ValueRange()
        {
        }

        public Int32ValueRange(int minimum, int maximum)
            : base(minimum, maximum)
        {
        }
    }
}