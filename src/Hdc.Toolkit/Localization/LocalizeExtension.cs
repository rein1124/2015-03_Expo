//from localizationsupport-36534
namespace Hdc.Localization
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
     

    [MarkupExtensionReturnType(typeof(string))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class LocalizeExtension : MarkupExtension
    {
        private readonly string _key;

        public LocalizeExtension()
        {
            
        }

        public LocalizeExtension(String key)
        {
            _key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if(LocalizationServiceLocator.Service==null)
                return _key;

            if(string.IsNullOrEmpty(_key))
                return null;

            var value = LocalizationServiceLocator.Service.TranslateFrom(_key);
            return value;
        }
    }
}