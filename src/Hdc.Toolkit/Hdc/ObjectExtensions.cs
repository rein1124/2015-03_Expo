//from dnpextensions-33023.zip
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Hdc.Reflection;

namespace Hdc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

  

    /// <summary>
    /// Extension methods for the root data type object
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object to be compared.</param>
        /// <param name="values">The values to compare with the object.</param>
        /// <returns></returns>
        public static bool EqualsAny<T>(this T obj, params T[] values)
        {
            return (Array.IndexOf(values, obj) != -1);
        }

        /// <summary>
        /// Determines whether the object is equal to none of the provided values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object to be compared.</param>
        /// <param name="values">The values to compare with the object.</param>
        /// <returns></returns>
        public static bool EqualsNone<T>(this T obj, params T[] values)
        {
            return (obj.EqualsAny(values) == false);
        }

        /// <summary>
        /// Converts an object to the specified target type or returns the default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo<T>(this object value)
        {
            return value.ConvertTo(default(T));
        }

        /// <summary>
        /// Converts an object to the specified target type or returns the default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo<T>(this object value, T defaultValue)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                var converter = TypeDescriptor.GetConverter(value);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                    {
                        return (T)converter.ConvertTo(value, targetType);
                    }
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(value.GetType()))
                    {
                        return (T)converter.ConvertFrom(value);
                    }
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Determines whether the value can (in theory) be converted to the specified target type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can be convert to the specified target type; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanConvertTo<T>(this object value)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                var converter = TypeDescriptor.GetConverter(value);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                    {
                        return true;
                    }
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(value.GetType()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Dynamically invokes a method using reflection
        /// </summary>
        /// <param name="obj">The object to perform on.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="parameters">The parameters passed to the method.</param>
        /// <returns>The return value</returns>
        /// <example>
        /// <code>
        /// var type = Type.GetType("System.IO.FileInfo, mscorlib");
        /// var file = type.CreateInstance(@"c:\autoexec.bat");
        /// if(file.GetPropertyValue&lt;bool&gt;("Exists")) {
        ///  var reader = file.InvokeMethod&lt;StreamReader&gt;("OpenText");
        ///  Console.WriteLine(reader.ReadToEnd());
        ///  reader.Close();
        /// }
        /// </code>
        /// </example>
        public static object InvokeMethod(this object obj, string methodName, params object[] parameters)
        {
            return InvokeMethod<object>(obj, methodName, parameters);
        }

        /// <summary>
        /// Dynamically invokes a method using reflection and returns its value in a typed manner
        /// </summary>
        /// <typeparam name="T">The expected return data types</typeparam>
        /// <param name="obj">The object to perform on.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="parameters">The parameters passed to the method.</param>
        /// <returns>The return value</returns>
        /// <example>
        /// <code>
        /// var type = Type.GetType("System.IO.FileInfo, mscorlib");
        /// var file = type.CreateInstance(@"c:\autoexec.bat");
        /// if(file.GetPropertyValue&lt;bool&gt;("Exists")) {
        ///  var reader = file.InvokeMethod&lt;StreamReader&gt;("OpenText");
        ///  Console.WriteLine(reader.ReadToEnd());
        ///  reader.Close();
        /// }
        /// </code>
        /// </example>
        public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var type = obj.GetType();
            var method = type.GetMethod(methodName);

            if (method == null)
            {
                throw new ArgumentException(string.Format("Method '{0}' not found.", methodName), methodName);
            }

            var value = method.Invoke(obj, parameters);
            return (value is T ? (T)value : default(T));
        }

        /// <summary>
        /// Dynamically retrieves a property value.
        /// </summary>
        /// <param name="obj">The object to perform on.</param>
        /// <param name="propertyName">The Name of the property.</param>
        /// <returns>The property value.</returns>
        /// <example>
        /// <code>
        /// var type = Type.GetType("System.IO.FileInfo, mscorlib");
        /// var file = type.CreateInstance(@"c:\autoexec.bat");
        /// if(file.GetPropertyValue&lt;bool&gt;("Exists")) {
        ///  var reader = file.InvokeMethod&lt;StreamReader&gt;("OpenText");
        ///  Console.WriteLine(reader.ReadToEnd());
        ///  reader.Close();
        /// }
        /// </code>
        /// </example>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return GetPropertyValue<object>(obj, propertyName, null);
        }

        /// <summary>
        /// Dynamically retrieves a property value.
        /// </summary>
        /// <typeparam name="T">The expected return data type</typeparam>
        /// <param name="obj">The object to perform on.</param>
        /// <param name="propertyName">The Name of the property.</param>
        /// <returns>The property value.</returns>
        /// <example>
        /// <code>
        /// var type = Type.GetType("System.IO.FileInfo, mscorlib");
        /// var file = type.CreateInstance(@"c:\autoexec.bat");
        /// if(file.GetPropertyValue&lt;bool&gt;("Exists")) {
        ///  var reader = file.InvokeMethod&lt;StreamReader&gt;("OpenText");
        ///  Console.WriteLine(reader.ReadToEnd());
        ///  reader.Close();
        /// }
        /// </code>
        /// </example>
        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            return GetPropertyValue<T>(obj, propertyName, default(T));
        }

        /// <summary>
        /// Dynamically retrieves a property value.
        /// </summary>
        /// <typeparam name="T">The expected return data type</typeparam>
        /// <param name="obj">The object to perform on.</param>
        /// <param name="propertyName">The Name of the property.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <returns>The property value.</returns>
        /// <example>
        /// <code>
        /// var type = Type.GetType("System.IO.FileInfo, mscorlib");
        /// var file = type.CreateInstance(@"c:\autoexec.bat");
        /// if(file.GetPropertyValue&lt;bool&gt;("Exists")) {
        ///  var reader = file.InvokeMethod&lt;StreamReader&gt;("OpenText");
        ///  Console.WriteLine(reader.ReadToEnd());
        ///  reader.Close();
        /// }
        /// </code>
        /// </example>
        public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);

            if (property == null)
            {
                throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);
            }

            var value = property.GetValue(obj, null);
            return (value is T ? (T)value : defaultValue);
        }

        /// <summary>
        /// Dynamically sets a property value.
        /// </summary>
        /// <param name="obj">The object to perform on.</param>
        /// <param name="propertyName">The Name of the property.</param>
        /// <param name="value">The value to be set.</param>
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);

            if (property == null)
            {
                throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);
            }

            property.SetValue(obj, value, null);
        }

        /// <summary>
        /// Gets the first matching attribute defined on the data type.
        /// </summary>
        /// <typeparam name="T">The attribute type tp look for.</typeparam>
        /// <param name="obj">The object to look on.</param>
        /// <returns>The found attribute</returns>
        public static T GetAttribute<T>(this object obj) where T : Attribute
        {
            return GetAttribute<T>(obj, true);
        }

        /// <summary>
        /// Gets the first matching attribute defined on the data type.
        /// </summary>
        /// <typeparam name="T">The attribute type tp look for.</typeparam>
        /// <param name="obj">The object to look on.</param>
        /// <param name="includeInherited">if set to <c>true</c> includes inherited attributes.</param>
        /// <returns>The found attribute</returns>
        public static T GetAttribute<T>(this object obj, bool includeInherited) where T : Attribute
        {
            var type = (obj as Type ?? obj.GetType());
            var attributes = type.GetCustomAttributes(typeof(T), includeInherited);
            if ((attributes != null) && (attributes.Length > 0))
            {
                return (attributes[0] as T);
            }
            return null;
        }

        /// <summary>
        /// Gets all matching attribute defined on the data type.
        /// </summary>
        /// <typeparam name="T">The attribute type tp look for.</typeparam>
        /// <param name="obj">The object to look on.</param>
        /// <returns>The found attributes</returns>
        public static IEnumerable<T> GetAttributes<T>(this object obj) where T : Attribute
        {
            return GetAttributes<T>(obj);
        }

        /// <summary>
        /// Gets all matching attribute defined on the data type.
        /// </summary>
        /// <typeparam name="T">The attribute type tp look for.</typeparam>
        /// <param name="obj">The object to look on.</param>
        /// <param name="includeInherited">if set to <c>true</c> includes inherited attributes.</param>
        /// <returns>The found attributes</returns>
        public static IEnumerable<T> GetAttributes<T>(this object obj, bool includeInherited) where T : Attribute
        {
            var type = (obj as Type ?? obj.GetType());
            foreach (var attribute in type.GetCustomAttributes(typeof(T), includeInherited))
            {
                if (attribute is T)
                {
                    yield return (T)attribute;
                }
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given object is <c>null</c>.
        /// </summary>
        /// <param name="this">The object to check.</param>
        /// <param name="parameterName">The name of the parameter in the method where <paramref name="this"/> came from.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method is primarily used to ensure a parameter to a method is not null.
        /// </remarks>
        public static void CheckParameterForNull(this object @this, string parameterName)
        {
            if (@this.IsNull())
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> with the specified message if the given object is <c>null</c>.
        /// </summary>
        /// <param name="this">The object to check.</param>
        /// <param name="parameterName">The name of the parameter in the method where <paramref name="this"/> came from.</param>
        /// <param name="message">The exception message.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method is primarily used to ensure a parameter to a method is not null.
        /// </remarks>
        public static void CheckParameterForNull(this object @this, string parameterName, string message)
        {
            if (@this.IsNull())
            {
                throw new ArgumentNullException(parameterName, message);
            }
        }

        /// <summary>
        /// Checks to see if an object is null.
        /// </summary>
        /// <param name="this">The object to check.</param>
        /// <returns>Returns <c>true</c> if <paramref name="this"/> is <c>null</c>, otherwise <c>false</c>.</returns>
        public static bool IsNull(this object @this)
        {
            return @this == null;
        }

        /// <summary>
        /// Checks to see if the object has a specific attribute.
        /// </summary>
        /// <param name="this">The object to check.</param>
        /// <param name="attributeType">The type of the custom attribute.</param>
        /// <param name="inherit">When <c>true</c>, look up the hierarchy chain for the inherited custom attribute.</param>
        /// <returns>Returns <c>true</c> if the object has the attribute, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="this"/> is <c>null</c>.
        /// </exception>
        public static bool HasAttribute(this object @this, Type attributeType, bool inherit)
        {
            @this.CheckParameterForNull("@this");
            return ICustomAttributeProviderExtensions.HasAttribute(
                @this.GetType(), attributeType, inherit);
        }


        // 2014-04-10, add by rein
        // ref: http://stackoverflow.com/questions/358835/getproperties-to-return-all-properties-for-an-interface-inheritance-hierarchy
        public static PropertyInfo[] GetPublicProperties(this Type type)
        {
            if (type.IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();

                var considered = new List<Type>();
                var queue = new Queue<Type>();
                considered.Add(type);
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces())
                    {
                        if (considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(
                        BindingFlags.FlattenHierarchy
                        | BindingFlags.Public
                        | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties
                        .Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                return propertyInfos.ToArray();
            }

            return type.GetProperties(BindingFlags.FlattenHierarchy
                | BindingFlags.Public | BindingFlags.Instance);
        }



        // 2014-04-11, add by rein
        public static IEnumerable<AttributePropertyInfo> GetAttributePropertyInfos(this Type targetType, Type attributeType, bool inherit)
        {
            foreach (var prop in targetType.GetProperties())
            {
                var attri = prop.GetCustomAttributes(attributeType, inherit).FirstOrDefault() as Attribute;
                if (attri == null)
                    continue;
                yield return new AttributePropertyInfo()
                             {
                                 PropertyInfo = prop, 
                                 Attribute = attri,
                             };
            }
        }

//        public static IEnumerable<AttributePropertyInfo> GetAttributePropertyInfos<TSourceType,TAttributeType>(bool inherit)
//        {
//            return GetAttributePropertyInfos(typeof (TSourceType), typeof (TAttributeType),false);
//        }

        public static IEnumerable<AttributePropertyInfo> GetAttributePropertyInfos<TAttributeType>(this object target, bool inherit)
        {
            return GetAttributePropertyInfos(target.GetType(), typeof(TAttributeType), false);
        }

        // 2014-04-11, add by rein
        public class AttributePropertyInfo
        {
            public PropertyInfo PropertyInfo { get; set; }
            public Attribute Attribute { get; set; }
//            public object Value { get; set; }
        }
    }
}