using System;
using Microsoft.Practices.ServiceLocation;

namespace Hdc
{
    public static class ServiceLocatorExtensions
    {
        public static Lazy<T> GetLazy<T>(this IServiceLocator serviceLocator)
        {
            var lazy = new Lazy<T>(serviceLocator.GetInstance<T>);
            return lazy;
        }

        public static Lazy<T> GetLazy<T>(this IServiceLocator serviceLocator, string key)
        {
            var lazy = new Lazy<T>(() => serviceLocator.GetInstance<T>(key));
            return lazy;
        }
    }
}