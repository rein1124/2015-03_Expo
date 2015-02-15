using System.Reflection;
using Hdc.Reactive;

namespace Hdc.Mercury
{
    public interface IDeviceValueMonitor
    {
        void Sync(IDevice device);
    }

    public class DeviceValueMonitor<T>:DisplayValueMonitor<T>, IDeviceValueMonitor
    {
        public void Sync(IDevice device)
        {
            MethodInfo mi = typeof (MonitorExtensions).GetMethod("Sync");
            mi.Invoke(this, new[] { device });
            //MonitorExtensions.Sync((dynamic)device);
        }
    }
}