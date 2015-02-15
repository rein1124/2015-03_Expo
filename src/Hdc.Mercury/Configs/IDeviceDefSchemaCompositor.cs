using System;
using System.Collections.Generic;

namespace Hdc.Mercury.Configs
{
    public interface IDeviceDefSchemaCompositor
    {
        DeviceDefSchema Composite(IEnumerable<DeviceDefSchema> deviceDefSchemas);
    }
}