using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System;
using System.Xml;

namespace Hdc.Serialization
{
    public static class XmlSerializationExtensions
    {
        #region ToFile FromFile

        public static void SerializeToXmlFile<T>(this T instance, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(fileStream, instance);
            }
        }

        public static T DeserializeFromXmlFile<T>(this string path) where T : class
        {
            if (!File.Exists(path))
                return null;
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                var instance = xmlSerializer.Deserialize(fileStream);
                return (T)instance;
            }
        }

        #endregion

        #region ToString FromString

        public static string SerializeToString(this object instance)
        {
            var type = instance.GetType();
            var xmlSerializer = new XmlSerializer(type);
            var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, instance);
            return stringWriter.ToString();
        }

        public static T DeserializeFromString<T>(this string str) where T : class
        {
            var stringReader = new StringReader(str);
            var xmlSerializer = new XmlSerializer(typeof(T));
            var instance = xmlSerializer.Deserialize(stringReader);
            return (T)instance;
        }

        public static object DeserializeFromString(this string str, Type objectType)
        {
            var stringReader = new StringReader(str);
            var xmlSerializer = new XmlSerializer(objectType);
            var instance = xmlSerializer.Deserialize(stringReader);
            return instance;
        }

        #endregion

        #region ToXDocument  FromXDocument

        public static XDocument SerializeToXDocument(this object instance)
        {
            return XDocument.Parse(instance.SerializeToString());
        }

        public static T DeserializeFromXDocument<T>(this XDocument xDocument) where T : class
        {
            return DeserializeFromString<T>(xDocument.ToString());
        }

        #endregion

        #region ToXElement FromXElement

        public static XElement SerializeToXElement(this object instance)
        {
            return instance.SerializeToXDocument().Root;
        }

        public static T DeserializeFromXElement<T>(this XElement xElement) where T : class
        {
            return DeserializeFromString<T>(xElement.ToString());
        }

        public static object DeserializeFromXElement(this XElement xElement, Type objectType)
        {
            return DeserializeFromString(xElement.ToString(), objectType);
        }

        #endregion

        #region ToXmlWriter FromXmlReader

        public static void SerializeToXmlWriter(this object instance, XmlWriter writer)
        {
            var xmlSerializer = new XmlSerializer(instance.GetType());
            xmlSerializer.Serialize(writer, instance);
        }

        public static T DeserializeFromXmlReader<T>(this XmlReader reader) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var instance = xmlSerializer.Deserialize(reader);
            return (T)instance;
        }

        public static object DeserializeFromXmlReader(this XmlReader reader, Type objectType)
        {
            var xmlSerializer = new XmlSerializer(objectType);
            var instance = xmlSerializer.Deserialize(reader);
            return instance;
        }

        #endregion
    }
}