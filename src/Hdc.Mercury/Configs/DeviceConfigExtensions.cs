using Hdc.Mercury.Communication;

namespace Hdc.Mercury.Configs
{
    public static class DeviceConfigExtensions
    {
        public static object Convert(this object value, IAccessItemRegistration reg)
        {
            return GetConvertValue(reg.Config, value);
        }

        public static object Convert(this object value, DeviceConfig deviceConfig)
        {
            return GetConvertValue(deviceConfig, value);
        }

        public static object ConvertBack(this object value, DeviceConfig deviceConfig)
        {
            return GetConvertBackValue(deviceConfig, value);
        }

        public static object GetConvertValue(this DeviceConfig deviceConfig, object value)
        {
            if (!deviceConfig.IsConversionEnabled)
                return value;

            var v = value;

            foreach (var converter in deviceConfig.Converters)
            {
                v = converter.Convert(v);
            }

            return v;
        }

        public static object GetConvertBackValue(this DeviceConfig deviceConfig, object value)
        {
            if (!deviceConfig.IsConversionEnabled)
                return value;

            var v = value;

            for (int i = deviceConfig.Converters.Count - 1; i >= 0; i--)
            {
                var converter = deviceConfig.ConverterCollection[i];
                v = converter.ConvertBack(v);
            }

            return v;
        }
    }
}