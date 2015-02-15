using System;
using System.Linq;

namespace Hdc
{
    public static class TypeExtensions2
    {
        /// <summary>
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        /// </summary>
        public static bool HasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return (type.GetCustomAttributes(typeof (TAttribute), true).Length > 0);
        }

        /// <summary>
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        /// </summary>
        public static TAttribute FindAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return (TAttribute) type.GetCustomAttributes(typeof (TAttribute), true).SingleOrDefault();
        }

        public static bool IsImplementationOf<T>(this Type type)
        {
            return type.GetInterface(typeof (T).FullName) != null;
        }
    }
}