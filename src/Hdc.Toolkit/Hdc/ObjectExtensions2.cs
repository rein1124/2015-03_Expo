using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hdc
{
    public static class ObjectExtensions2
    {
        public static bool IsNull<T>(this T myObject) where T : class
        {
            return myObject == null;
        }

        public static bool IsNotNull<T>(this T myObject) where T : class
        {
            return myObject != null;
        }

        public static T DeepClone<T>(this T source) where T : class
        {
            var t = DeepClone((object)source);
            return t as T;
        }

        public static object DeepClone(object source)
        {
            if (source == null)
                return null;
            
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, source);
            stream.Position = 0;
            return formatter.Deserialize(stream);
        }

        // replaced
        /*public static TEnum ToEnum<TEnum>(this string enumName)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), enumName);
        }*/

        // 2014-04-13, rein
        // ref: http://stackoverflow.com/questions/1196991/get-property-value-from-string-using-reflection-in-c-sharp
        public static object GetPropertyValueByPropertyName(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}