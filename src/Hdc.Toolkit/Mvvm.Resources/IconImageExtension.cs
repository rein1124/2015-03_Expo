using System.Windows.Controls;
using System;
using System.Windows.Markup;

namespace Hdc.Mvvm.Resources
{
    [MarkupExtensionReturnTypeAttribute(typeof(Image))]
    public class IconImageExtension : MarkupExtension
    {
        private readonly string _iconName;

        public IconImageExtension(string iconName)
        {
            _iconName = iconName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var iconImageSource =
                IconImageSourceLoaderServiceLocator.IconImageSourceLoader.Load(Convert.ToString(_iconName));
            if (iconImageSource == null)
            {
                return _iconName;
            }

            return new Image() { Source = iconImageSource, };
        }
    }
}