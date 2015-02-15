using Hdc;

namespace Hdc
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// A class that makes it easy to change a property value 
    /// and reset it back to its' original value via a <c>using</c> statement.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target to change the property on.</typeparam>
    /// <typeparam name="TNew">The type of property to change.</typeparam>
    /// <remarks>
    /// <example>
    /// The canonical example of when you would use this class is to change 
    /// the cursor to an hourglass and then reset it back to its original value
    /// once some work is done.
    /// <code>
    /// using(var switcher = new ScopeSwitcher&lt;Cursor, Cursor&gt;(Cursor.WaitCursor))
    /// {
    ///    // Perform some interesting UI work...
    /// }
    /// </code>
    /// The cursor will automatically change to an hourglass within the <c>using</c> block,
    /// and return to its original value when it exits the block.
    /// </example>
    /// </remarks>
    public sealed class ScopeSwitcher<TTarget, TNew> : IDisposable
    {
        private bool isPropertyStatic;

        private TNew newValue;

        private TNew oldValue;

        private PropertyInfo property;

        private TTarget target;

        /// <summary>
        /// Creates a new <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;" /> instance with 
        /// the specified new value.
        /// </summary>
        /// <param name="newValue">The new value to change.</param>
        /// <exception cref="PropertyNotFoundException">Thrown if a property could not be found.</exception>
        /// <remarks>
        /// With this constructor, <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;"/> will try to find the
        /// first public, static readable property that matches the type of <typeparamref name="TNew"/>.
        /// If one is found, it will target that property.
        /// </remarks>
        public ScopeSwitcher(TNew newValue)
            : base()
        {
            this.FindProperty(BindingFlags.Static);
            this.isPropertyStatic = true;
            this.ExchangePropertyValue(newValue);
        }

        /// <summary>
        /// Creates a new <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;" /> instance with 
        /// the specified new value and the name of the static property to change.
        /// </summary>
        /// <param name="newValue">The new value to change.</param>
        /// <param name="propertyName">The name of the property to change.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="propertyName"/> is <c>null</c> or empty.</exception>
        /// <exception cref="PropertyNotFoundException">Thrown if a property could not be found.</exception>
        /// <remarks>
        /// With this constructor, <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;"/> will try to find the
        /// first public, static readable property that matches the type of <typeparamref name="TNew"/>,
        /// and has the same name as <paramref name="propertyName"/>.
        /// If one is found, it will target that property.
        /// </remarks>
        public ScopeSwitcher(TNew newValue, string propertyName)
            : base()
        {
            this.CheckPropertyName(propertyName);

            this.FindProperty(BindingFlags.Static, propertyName);
            this.isPropertyStatic = true;
            this.ExchangePropertyValue(newValue);
        }

        /// <summary>
        /// Creates a new <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;" /> instance with 
        /// the specified target and new value.
        /// </summary>
        /// <param name="target">The target to use.</param>
        /// <param name="newValue">The new value to change.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is <c>null</c>.</exception>
        /// <exception cref="PropertyNotFoundException">Thrown if a property could not be found.</exception>
        /// <remarks>
        /// With this constructor, <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;"/> will try to find the
        /// first public, instance readable property that matches the type of <typeparamref name="TNew"/>.
        /// If one is found, it will target that property.
        /// </remarks>
        public ScopeSwitcher(TTarget target, TNew newValue)
            : base()
        {
            target.CheckParameterForNull("target");
            this.FindProperty(BindingFlags.Instance);
            this.target = target;
            this.ExchangePropertyValue(newValue);
        }

        /// <summary>
        /// Creates a new <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;" /> instance with 
        /// the specified target, new value, and the name of the static property to change.
        /// </summary>
        /// <param name="target">The target to use.</param>
        /// <param name="newValue">The new value to change.</param>
        /// <param name="propertyName">The name of the property to change.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="propertyName"/> is <c>null</c> or empty.</exception>
        /// <exception cref="PropertyNotFoundException">Thrown if a property could not be found.</exception>
        /// <remarks>
        /// With this constructor, <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;"/> will try to find the
        /// first public, instance readable property that matches the type of <typeparamref name="TNew"/>
        /// and has the same name as <paramref name="propertyName"/>.
        /// If one is found, it will target that property.
        /// </remarks>
        public ScopeSwitcher(TTarget target, TNew newValue, string propertyName)
            : base()
        {
            target.CheckParameterForNull("target");
            this.CheckPropertyName(propertyName);

            this.FindProperty(BindingFlags.Instance, propertyName);
            this.target = target;
            this.ExchangePropertyValue(newValue);
        }

        /// <summary>
        /// Sets the target property's value back to its original value.
        /// </summary>
        public void Dispose()
        {
            if (this.property != null)
            {
                if (this.isPropertyStatic)
                {
                    this.property.SetValue(null, this.oldValue, null);
                }
                else
                {
                    this.property.SetValue(this.target, this.oldValue, null);
                }
            }
        }

        private void CheckPropertyName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("No property name was given.", "propertyName");
            }
        }

        private void ExchangePropertyValue(TNew newValue)
        {
            this.newValue = newValue;

            if (this.isPropertyStatic)
            {
                this.oldValue = (TNew)this.property.GetValue(null, null);
                this.property.SetValue(null, this.newValue, null);
            }
            else
            {
                this.oldValue = (TNew)this.property.GetValue(this.target, null);
                this.property.SetValue(this.target, this.newValue, null);
            }
        }

        private void FindProperty(BindingFlags flags)
        {
            this.property = (from property in typeof(TTarget).GetProperties(BindingFlags.Public | flags)
                             where property.PropertyType.IsAssignableFrom(typeof(TNew))
                             where
                                 property.CanRead && property.GetGetMethod() != null && property.GetGetMethod().IsPublic
                             where
                                 property.CanWrite && property.GetSetMethod() != null &&
                                 property.GetSetMethod().IsPublic
                             select property).FirstOrDefault();

            if (this.property == null)
            {
                throw new PropertyNotFoundException(
                    "A read/write property was not found that was assignable from " + typeof(TNew).FullName + ".");
            }
        }

        private void FindProperty(BindingFlags flags, string propertyName)
        {
            this.property = (from property in typeof(TTarget).GetProperties(BindingFlags.Public | flags)
                             where property.Name == propertyName
                             where property.PropertyType.IsAssignableFrom(typeof(TNew))
                             where
                                 property.CanRead && property.GetGetMethod() != null && property.GetGetMethod().IsPublic
                             where
                                 property.CanWrite && property.GetSetMethod() != null &&
                                 property.GetSetMethod().IsPublic
                             select property).FirstOrDefault();

            if (this.property == null)
            {
                throw new PropertyNotFoundException(
                    "A read/write property with name " + propertyName + " was not found that was assignable from " +
                    typeof(TNew).FullName + ".");
            }
        }
    }
}