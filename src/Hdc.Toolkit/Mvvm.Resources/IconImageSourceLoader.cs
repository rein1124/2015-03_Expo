namespace Hdc.Mvvm.Resources
{
    using System;
    using System.Reflection;
    using System.Windows.Media;

    using Resources;

    public abstract class IconImageSourceLoader : IIconImageSourceLoader
    {
        private readonly ResourceLoaderManager _iconResourceLoaderManager;

        protected IconImageSourceLoader()
        {
            var IconsPath = GetPath();
            _iconResourceLoaderManager = new ResourceLoaderManager(IconsPath);
        }

        public abstract string GetPath();

        public ImageSource Load(string iconName)
        {
            if (string.IsNullOrEmpty(iconName))
            {
                return null;
            }
            return _iconResourceLoaderManager.LoadXaml(iconName) as DrawingImage;
        }
    }
}