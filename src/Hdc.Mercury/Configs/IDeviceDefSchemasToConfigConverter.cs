using System.Collections.Generic;

namespace Hdc.Mercury.Configs
{
    public interface IDeviceDefSchemasToConfigConverter
    {
        DeviceGroupConfig Convert(IEnumerable<DeviceDefSchema> deviceDefSchemas);
    }
}