using System.Windows.Media;
using System;
using System.Windows.Markup;

namespace Hdc.Mvvm.Resources
{
    [MarkupExtensionReturnTypeAttribute(typeof(ImageBrush))]
    public class IconBrushExtension : MarkupExtension
    {
        private readonly string _iconName;

        public IconBrushExtension(string iconName)
        {
            _iconName = iconName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var imageSource = IconImageSourceLoaderServiceLocator.IconImageSourceLoader.Load(_iconName);
            var imageBrush = new ImageBrush() {ImageSource = imageSource};
            return imageBrush;
        }
    }
}