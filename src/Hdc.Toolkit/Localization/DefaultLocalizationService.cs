namespace Hdc.Localization
{
    public class DefaultLocalizationService : ILocalizationService
    {
        public string TranslateFrom(string name)
        {
            return name;
        }

        public void Update(string LCIDString)
        {
                
        }
    }
}