namespace Hdc.Unity
{
    using System;
    using Microsoft.Practices.Unity;

    public static class UnityContainerExtensions
    {
        public static IUnityContainer RegisterTypes<TFrom, TTo>(
            this IUnityContainer container,
            params string[] names) where TTo : TFrom
        {
            if (names.Length == 0)
            {
                container.RegisterType<TFrom, TTo>();
            }
            else
            {
                foreach (var name in names)
                {
                    container.RegisterType<TFrom, TTo>(name);
                }
            }
            return container;
        }

        public static IUnityContainer RegisterTypesWithLifetimeManager<TFrom, TTo>(
            this IUnityContainer container,
            params string[] names) where TTo : TFrom
        {
            if (names.Length == 0)
            {
                container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
                return container;
            }

            foreach (var name in names)
            {
                container.RegisterType<TFrom, TTo>(name, new ContainerControlledLifetimeManager());
            }

            return container;
        }

        public static IUnityContainer RegisterTypeWithLifetimeManager<TFrom, TTo>(
            this IUnityContainer container, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager(), injectionMembers);
            return container;
        }

        public static IUnityContainer RegisterTypeWithLifetimeManager<T>(
            this IUnityContainer container, params InjectionMember[] injectionMembers)
        {
            container.RegisterType<T>(new ContainerControlledLifetimeManager(), injectionMembers);
            return container;
        }

        public static IUnityContainer RegisterTypeWithLifetimeManager<TFrom, TTo>(
            this IUnityContainer container, string name, params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            container.RegisterType<TFrom, TTo>(name, new ContainerControlledLifetimeManager(), injectionMembers);
            return container;
        }

        public static IUnityContainer RegisterTypeWithLifetimeManager<T>(
            this IUnityContainer container, string name, params InjectionMember[] injectionMembers)
        {
            container.RegisterType<T>(name, new ContainerControlledLifetimeManager(), injectionMembers);
            return container;
        }

        public static IUnityContainer RegisterInstanceWithLifetimeManager<T>(
            this IUnityContainer container, string name, T instance)
        {
            container.RegisterInstance<T>(name, instance, new ContainerControlledLifetimeManager());
            return container;
        }

        public static IUnityContainer RegisterInstanceWithLifetimeManager<T>(
            this IUnityContainer container, T instance)
        {
            container.RegisterInstance<T>(instance, new ContainerControlledLifetimeManager());
            return container;
        }

        public static T ResolveWith<T>(this IUnityContainer container, Action<T> action)
        {
            Guard.ArgumentNotNull(container, "container");
            var instance = container.Resolve<T>();
            action(instance);
            return instance;
        }

        public static Lazy<T> GetLazy<T>(this IUnityContainer container)
        {
            var lazy = new Lazy<T>(() => container.Resolve<T>());
            return lazy;
        }

        public static Lazy<T> GetLazy<T>(this IUnityContainer container, string key)
        {
            var lazy = new Lazy<T>(() => container.Resolve<T>(key));
            return lazy;
        }

        public static IUnityAction Fluent(this IUnityContainer container)
        {
            return new UnityAction(container);
        }

        public interface IUnityAction
        {
            IUnityAction RegisterType<T, TImpl>() where TImpl : T;
        }

        private class UnityAction : IUnityAction
        {
            private readonly IUnityContainer _container;

            public UnityAction(IUnityContainer container)
            {
                _container = container;
            }

            public IUnityAction RegisterType<T, T2>() where T2 : T
            {
                _container.RegisterType<T, T2>();
                return this;
            }
        }
    }
}