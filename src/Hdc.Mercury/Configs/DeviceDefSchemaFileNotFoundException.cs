using System;
using System.Runtime.Serialization;

namespace Hdc.Mercury.Configs
{
    public class DeviceDefSchemaFileNotFoundException : Exception
    {
        public DeviceDefSchemaFileNotFoundException()
        {
        }

        public DeviceDefSchemaFileNotFoundException(string message) : base(message)
        {
        }

        public DeviceDefSchemaFileNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DeviceDefSchemaFileNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string FileName { get; set; }
    }
}