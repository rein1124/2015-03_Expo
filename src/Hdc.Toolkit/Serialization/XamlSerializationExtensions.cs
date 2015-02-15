using System.IO;
using System.Xaml;
using System.Windows.Markup;
using System.Xml;

namespace Hdc.Serialization
{
    public static class XamlSerializationExtensions
    {
//        [Obsolete]
//        public static void SaveToXamlFile<T>(this T instance, string fileName)
//        {
//            instance.SerializeToXamlFile(fileName);
//        }

        public static void SerializeToXamlFile<T>(this T instance, string fileName)
        {
            XamlServices.Save(fileName, instance);
        }

//        [Obsolete]
//        public static T LoadFromXamlFile<T>(this string fileName)
//        {
//            return fileName.DeserializeFromXamlFile<T>();
//        }

        public static T DeserializeFromXamlFile<T>(this string fileName)
        {
            return (T) XamlServices.Load(fileName);
        }

        public static object CloneUsingXamlSerialization(this object source)
        {
            if (source == null)
            {
                return null;
            }

            string s = System.Windows.Markup.XamlWriter.Save(source);

//            var stringReader = new StringReader(s);
//
//            XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings());

            return System.Windows.Markup.XamlReader.Parse(s);
        }
    }
}