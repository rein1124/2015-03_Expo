namespace Hdc
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Thrown in <see cref="ScopeSwitcher&lt;TTarget, TNew&gt;"/> if a property could not be found on the target.
    /// </summary>
    [Serializable]
    public sealed class PropertyNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="PropertyNotFoundException"/> instance.
        /// </summary>
        public PropertyNotFoundException()
            : base()
        {
        }

        private PropertyNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Creates a new <see cref="PropertyNotFoundException"/> instance
        /// with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public PropertyNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="PropertyNotFoundException"/> instance
        /// with a specified error message and a reference to the inner exception that is the cause of this exception. 
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, 
        /// or a null reference if no inner exception is specified. 
        /// </param>
        public PropertyNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}