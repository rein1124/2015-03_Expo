using System;
using System.Runtime.Serialization;

namespace Hdc.Mercury.Configs
{
    public class DeviceConfigSchemaFileNotFoundException:Exception
    {
        public DeviceConfigSchemaFileNotFoundException()
        {
        }

        public DeviceConfigSchemaFileNotFoundException(string message) : base(message)
        {
        }

        public DeviceConfigSchemaFileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeviceConfigSchemaFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string FileName { get; set; }
    }
}