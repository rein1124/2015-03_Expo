namespace Hdc.Localization
{
    public interface ILocalizationService
    {
        string TranslateFrom(string name);

        void Update(string LCIDString);
    }
}