using System;
using System.Runtime.Serialization;

namespace Hdc.Mv.Inspection
{
    public class CreateCoordinateFailedException:Exception
    {
        public CreateCoordinateFailedException()
        {
        }

        public CreateCoordinateFailedException(string message) : base(message)
        {
        }

        public CreateCoordinateFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreateCoordinateFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}