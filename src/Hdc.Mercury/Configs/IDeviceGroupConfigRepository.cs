using System.Collections.Generic;

namespace Hdc.Mercury.Configs
{
    public interface IDeviceGroupConfigRepository
    {
        DeviceConfigSchema Load();

        void Save(DeviceConfigSchema deviceGroupConfig);

        void Save(IEnumerable<DeviceConfigSchema> deviceGroupConfigs);
    }
}