using Hdc.Mercury;

namespace Hdc.Mercury
{
    public interface IFacilityNodeBase
    {
        IDevice<T> GetDevice<T>(string deviceName);

//        IObjectWrapper<T> GetSharedObject<T>(string deviceName);
    }
}