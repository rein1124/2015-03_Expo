using System;
using System.Runtime.Serialization;

namespace Hdc.Mvvm.Resources
{
    public class ResourceLoadingFailedException:Exception
    {
        public ResourceLoadingFailedException()
        {
        }

        public ResourceLoadingFailedException(string message) : base(message)
        {
        }

        public ResourceLoadingFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResourceLoadingFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}