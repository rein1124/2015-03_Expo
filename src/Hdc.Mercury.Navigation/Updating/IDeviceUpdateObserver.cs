using System.Collections.Generic;
using Hdc.Mercury;

namespace Hdc.Mercury.Navigation
{
    public interface IDeviceUpdateObserver
    {
        IList<IDevice> UpdatingDevices { get; }
    }
}