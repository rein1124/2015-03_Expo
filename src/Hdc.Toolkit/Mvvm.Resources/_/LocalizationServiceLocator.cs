/*namespace Hdc.Mvvm.Resources
{
    public static class LocalizationServiceLocator
    {
        private class MockLocalizationLoader : ILocalizationLoader
        {
            public string Load(string name)
            {
                return name;
            }
        }

        private static ILocalizationLoader _loader = new MockLocalizationLoader();

        public static ILocalizationLoader Loader
        {
            get
            {
                return _loader;
            }
            set
            {
                _loader = value;
            }
        }
    }
}*/