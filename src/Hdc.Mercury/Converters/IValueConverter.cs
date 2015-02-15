namespace Hdc.Mercury.Converters
{
    public interface IValueConverter
    {
        object Convert(object originalValue);

        object ConvertBack(object convertedValue); 
    }
}