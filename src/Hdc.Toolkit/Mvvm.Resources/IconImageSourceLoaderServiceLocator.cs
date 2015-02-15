namespace Hdc.Mvvm.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IconImageSourceLoaderServiceLocator
    {
        private static IIconImageSourceLoader _iconImageSourceLoader = new MockIconImageSourceLoader();

        public static IIconImageSourceLoader IconImageSourceLoader
        {
            get
            {
                return _iconImageSourceLoader;
            }
            set
            {
                _iconImageSourceLoader = value;
            }
        }
    }
}