using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace Hdc.Mvvm.Resources
{
    public static class Extensions
    {
        public static ResourceDictionary LoadResourceDictionary(this string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                var resourceDictionary = XamlReader.Load(fileStream) as ResourceDictionary;
                return resourceDictionary;
            }
        }
    }
}