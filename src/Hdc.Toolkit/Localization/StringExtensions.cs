namespace Hdc.Localization
{
    public static class StringExtensions
    {
        public static string Translate(this string key)
        {
            var translation = LocalizationServiceLocator.Service.TranslateFrom(key);
            return translation;
        }
    }
}