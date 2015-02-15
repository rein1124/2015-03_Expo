using Hdc.Mercury.Configs;

namespace Hdc.Mercury
{
    public interface IDeviceGroupFactory
    {
        IDeviceGroup Create(DeviceGroupConfig deviceGroupConfig);
    }
}