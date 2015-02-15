using System;
using System.Runtime.Serialization;

namespace Hdc.Mvvm
{
    public class ViewModelNotFoundExcepton : Exception
    {
        public ViewModelNotFoundExcepton()
        {
        }

        public ViewModelNotFoundExcepton(string message) : base(message)
        {
        }

        public ViewModelNotFoundExcepton(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ViewModelNotFoundExcepton(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}