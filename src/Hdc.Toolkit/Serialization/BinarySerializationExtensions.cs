using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Hdc.Serialization
{
    public static class BinarySerializationExtensions
    {
        public static void SerializeToBinaryFile<T>(this T instance, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, instance);
            }
        }

        public static byte[] SerializeToBinaryBytes<T>(this T instance)
        {
            byte[] ret = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(ms, instance);
                ret = ms.ToArray();
                ms.Close();
            }
            return ret;
        }

        public static T DeserializeFromBinaryFile<T>(this string path) where T : class
        {
            if (!File.Exists(path))
                return null;

            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();

                var instance = binaryFormatter.Deserialize(fileStream);
                return (T) instance;
            }
        }

        public static T DeserializeFromBinaryBytes<T>(this byte[] bytes) where T : class
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var binaryFormatter = new BinaryFormatter();
                T ret = (T) binaryFormatter.Deserialize(ms);
                ms.Close();
                return ret;
            }
        }
    }
}