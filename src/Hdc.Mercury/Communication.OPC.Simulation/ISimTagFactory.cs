namespace Hdc.Mercury.Communication.OPC
{
    public interface ISimTagFactory
    {
        ISimTag Build(DeviceDataType dataType);
    }
}