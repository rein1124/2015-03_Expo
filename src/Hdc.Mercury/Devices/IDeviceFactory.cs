using Hdc.Mercury.Configs;

namespace Hdc.Mercury
{
    public interface IDeviceFactory
    {
        IDevice Create(DeviceConfig deviceConfig);
    }
}