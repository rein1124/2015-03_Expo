using System;
using Microsoft.Practices.Unity;

namespace Hdc.Unity
{
    public static class UnityExtensions
    {
        public static T ResolveWith<T>(this IUnityContainer container, Action<T> action)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            var instance = container.Resolve<T>();
            action(instance);
            return instance;
        }
    }
}