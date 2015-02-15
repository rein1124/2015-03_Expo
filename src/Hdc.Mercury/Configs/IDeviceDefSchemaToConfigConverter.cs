using System.Collections.Generic;

namespace Hdc.Mercury.Configs
{
    public interface IDeviceDefSchemaToConfigConverter
    {
        IEnumerable<DeviceGroupConfig> Convert(DeviceDefSchema deviceDefSchema);
    }
}