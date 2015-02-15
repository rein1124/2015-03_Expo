using System;
using Hdc;

namespace Hdc
{
    /// <summary>
    /// Contains helper methods to use <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;"/> with
    /// C# static classes.
    /// </summary>
    /// <remarks>
    /// In C#, static classes cannot be used in generic parameters (compiler error CS0718).
    /// These methods provide a workaround to this restriction.
    /// </remarks>
    public static class ScopeSwitcherHelpers
    {
        private static void Switch<TNew>(Type target, Action code, params object[] args)
        {
            target.CheckParameterForNull("target");
            code.CheckParameterForNull("code");

            IDisposable switcher = null;

            try
            {
                switcher =
                    Activator.CreateInstance(typeof(ScopeSwitcher<,>).MakeGenericType(target, typeof(TNew)), args) as
                    IDisposable;
                code();
            }
            finally
            {
                if (switcher != null)
                {
                    switcher.Dispose();
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;" /> instance with 
        /// the specified new value and the name of the static property to change,
        /// and runs <paramref name="code"/> within the switch.
        /// </summary>
        /// <param name="target">The type of the static class.</param>
        /// <param name="newValue">The new value to change.</param>
        /// <param name="propertyName">The name of the property to change.</param>
        /// <param name="code">The <see cref="Action"/> to run within the switch.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if either <paramref name="target"/> or <paramref name="code"/> are <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="propertyName"/> is <c>null</c> or empty.
        /// </exception>
        /// <exception cref="PropertyNotFoundException">
        /// Thrown if a property could not be found.</exception>
        /// <remarks>
        /// With this constructor, <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;"/> will try to find the
        /// first public, static readable property that matches the type of <typeparamref name="TNew"/>,
        /// and has the same name as <paramref name="propertyName"/>.
        /// If one is found, it will target that property.
        /// </remarks>
        public static void Switch<TNew>(Type target, TNew newValue, string propertyName, Action code)
        {
            ScopeSwitcherHelpers.Switch<TNew>(target, code, newValue, propertyName);
        }

        /// <summary>
        /// Creates a new <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;" /> instance with 
        /// the specified new value,
        /// and runs <paramref name="code"/> within the switch.
        /// </summary>
        /// <param name="target">The type of the static class.</param>
        /// <param name="newValue">The new value to change.</param>
        /// <param name="code">The <see cref="Action"/> to run within the switch.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if either <paramref name="target"/> or <paramref name="code"/> are <c>null</c>.
        /// </exception>
        /// <exception cref="PropertyNotFoundException">
        /// Thrown if a property could not be found.</exception>
        /// <remarks>
        /// With this constructor, <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;"/> will try to find the
        /// first public, static readable property that matches the type of <typeparamref name="TNew"/>.
        /// If one is found, it will target that property.
        /// </remarks>
        public static void Switch<TNew>(Type target, TNew newValue, Action code)
        {
            ScopeSwitcherHelpers.Switch<TNew>(target, code, newValue);
        }
    }
}