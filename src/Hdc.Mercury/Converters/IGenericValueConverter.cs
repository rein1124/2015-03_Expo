namespace Hdc.Mercury.Converters
{
    public interface IGenericValueConverter
    {
        T Convert<T>(T originalValue);

        T ConvertBack<T>(T convertedValue);
    }
}