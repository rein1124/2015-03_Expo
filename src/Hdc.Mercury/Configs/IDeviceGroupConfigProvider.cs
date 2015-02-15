namespace Hdc.Mercury.Configs
{
    public interface IDeviceGroupConfigProvider
    {
        DeviceGroupConfig RootDeviceGroupConfig { get; }
    }
}