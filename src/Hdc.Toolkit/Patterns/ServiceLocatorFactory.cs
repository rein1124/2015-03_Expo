using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Patterns
{
    public class ServiceLocatorFactory<T> : IFactory<T>
    {
        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        public T Create()
        {
            var instance = ServiceLocator.GetInstance<T>();
            OnInitializing(instance);
            return instance;
        }

        protected virtual void OnInitializing(T instance)
        {
        }
    }
}