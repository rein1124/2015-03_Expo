//from compositeextensions-16590

using Hdc.Patterns;
using Microsoft.Practices.Unity;

namespace Hdc.Patterns
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provides a simple Inversion of Control container façade
    /// that uses the Unity Container.
    /// </summary>
    public class UnityApplicationContainer : IApplicationContainer
    {
        private IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityApplicationContainer"/> class.
        /// </summary>
        /// <param name="container">The unit container which is wrapped by this class.</param>
        public UnityApplicationContainer(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Resolve an instance of the requested type from the container.
        /// </summary>
        /// <param name="type">The type of object to get from the container.</param>
        /// <returns>An instance of <paramref name="type"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        /// <summary>
        /// Tries to resolve an instance of the requested type from the container.
        /// </summary>
        /// <param name="type">The type of object to get from the container.</param>
        /// <returns>
        /// An instance of <paramref name="type"/>. 
        /// If the type cannot be resolved it will return a <see langword="null" /> value.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public T TryResolve<T>()
        {
            T resolved;
            try
            {
                resolved = container.Resolve<T>();
            }
            catch
            {
                resolved = default(T);
            }
            return resolved;
        }

        /// <summary>
        /// Registers a type mapping with the container, where the created instance is a singleton
        /// or it is recreated with every call of Resolve.
        /// </summary>
        /// <typeparam name="TFrom">The abstract type of the type to create.</typeparam>
        /// <typeparam name="TTo">The concrete type that should be created.</typeparam>
        /// <param name="singleton">if set to <c>true</c> then the type is created only once (singleton).
        /// Otherwise, the type is created every time the method Resolve is called for this type.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public void RegisterType<TFrom, TTo>(bool singleton) where TTo : TFrom
        {
            if (singleton)
                container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
            else
                container.RegisterType<TFrom, TTo>();
        }

        /// <summary>
        /// Registers the instance with the container.
        /// </summary>
        /// <typeparam name="TInterface">The abstract type of the instance.</typeparam>
        /// <param name="instance">The instance.</param>
        public void RegisterInstance<TInterface>(TInterface instance)
        {
            container.RegisterInstance(instance);
        }
    }
}