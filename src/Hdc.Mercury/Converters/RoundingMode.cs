namespace Hdc.Mercury.Converters
{
    public enum RoundingMode
    {
        /// <summary>
        /// Not use rounding
        /// </summary>
        None,


        /// <summary>
        /// + 0.5m and remove fractional part
        /// </summary>
        Rounded,

        /// <summary>
        /// If value is halfway between two whole numbers, the even number is returned; 
        /// that is, 4.5 is converted to 4, and 5.5 is converted to 6.
        /// </summary>
        RoundedWithEven,
    }
}