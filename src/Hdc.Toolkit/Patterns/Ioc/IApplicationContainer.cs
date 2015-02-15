//from compositeextensions-16590

using System.Diagnostics.CodeAnalysis;

namespace Hdc.Patterns
{
    /// <summary>
    /// Defines a simple Inversion of Control container façade
    /// to be used by the application.
    /// </summary>
    public interface IApplicationContainer
    {
        /// <summary>
        /// Resolve an instance of the requested type from the container.
        /// </summary>
        /// <param name="type">The type of object to get from the container.</param>
        /// <returns>An instance of <paramref name="type"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        T Resolve<T>();

        /// <summary>
        /// Tries to resolve an instance of the requested type from the container.
        /// </summary>
        /// <param name="type">The type of object to get from the container.</param>
        /// <returns>
        /// An instance of <paramref name="type"/>. 
        /// If the type cannot be resolved it will return a <see langword="null" /> value.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        T TryResolve<T>();

        /// <summary>
        /// Registers a type mapping with the container, where the created instance is a singleton
        /// or it is recreated with every call of Resolve.
        /// </summary>
        /// <typeparam name="TFrom">The abstract type of the type to create.</typeparam>
        /// <typeparam name="TTo">The concrete type that should be created.</typeparam>
        /// <param name="singleton">if set to <c>true</c> then the type is created only once (singleton).
        /// Otherwise, the type is created every time the method Resolve is called for this type.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        void RegisterType<TFrom, TTo>(bool singleton) where TTo : TFrom;

        /// <summary>
        /// Registers the instance with the container.
        /// </summary>
        /// <typeparam name="TInterface">The abstract type of the instance.</typeparam>
        /// <param name="instance">The instance.</param>
        void RegisterInstance<TInterface>(TInterface instance);
    }
}