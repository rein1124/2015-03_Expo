using System;

namespace Hdc.Localization
{
    public static class LocalizationServiceLocator
    {
        private static ILocalizationService _service = new DefaultLocalizationService();

        public static ILocalizationService Service
        {
            get { return _service; }
            set { _service = value; }
        }
    }
}