namespace Hdc.Mercury.Configs
{
    public interface IDeviceDefSchemaRepository
    {
        DeviceDefSchema Load(string fileName);

        void Save(DeviceDefSchema deviceDefSchema, string fileName);
    }
}