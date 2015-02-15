using System;
using System.ComponentModel;
using Hdc.Mercury.Configs;

namespace Hdc.Mercury.Converters
{
    [Serializable]
    public class LinearValueConverter : IGenericValueConverter, IValueConverter
    {
        public decimal Factor { get; set; }

        [DefaultValue(RoundingMode.None)]
        public RoundingMode RoundingMode { get; set; }

        public LinearValueConverter()
        {
        }

        public LinearValueConverter(decimal factor)
        {
            Factor = factor;
        }

        public LinearValueConverter(decimal factor, RoundingMode roundingMode)
        {
            Factor = factor;
            RoundingMode = roundingMode;
        }

        public T Convert<T>(T originalValue)
        {
            if (!originalValue.CheckCanRounding())
                return originalValue;

            decimal valueD = System.Convert.ToDecimal(originalValue);
            decimal resultD = valueD / Factor;

            T resultT = resultD.RoundValue<T>(RoundingMode);
            return resultT;
        }

        public T ConvertBack<T>(T convertedValue)
        {
            if (!convertedValue.CheckCanRounding())
                return convertedValue;

            decimal valueD = System.Convert.ToDecimal(convertedValue);
            decimal resultD = valueD * Factor;

            T resultT = resultD.RoundValue<T>(RoundingMode);
            return resultT;
        }

        public object Convert(object originalValue)
        {
            if (!originalValue.CheckCanRounding())
                return originalValue;

            var t = originalValue.GetType();

            decimal valueD = System.Convert.ToDecimal(originalValue);
            decimal resultD = valueD * Factor;

            var resultO = resultD.RoundValue(t, RoundingMode);
            return resultO;
        }

        public object ConvertBack(object convertedValue)
        {
            if (!convertedValue.CheckCanRounding())
                return convertedValue;

            var t = convertedValue.GetType();

            decimal valueD = System.Convert.ToDecimal(convertedValue);
            decimal resultD = valueD / Factor;

            var resultO = resultD.RoundValue(t, RoundingMode);
            return resultO;
        }
    }
}