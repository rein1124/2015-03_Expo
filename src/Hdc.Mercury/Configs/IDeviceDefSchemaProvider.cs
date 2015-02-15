using System.Collections.Generic;

namespace Hdc.Mercury.Configs
{
    public interface IDeviceDefSchemaProvider
    { 
        DeviceDefSchema Schema { get; }
    }
}