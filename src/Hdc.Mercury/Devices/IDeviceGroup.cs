using System;
using System.Collections.Generic;
using Hdc.Collections.Generic;
using Hdc.Mercury.Configs;
using Hdc.Reactive;

namespace Hdc.Mercury
{
    public interface IDeviceGroup : INode<IDeviceGroup>
    {
        string Name { get; set; }

        IList<IDeviceGroup> DeviceGroups { get; }

        IList<IDevice> Devices { get; set; }

        IDeviceGroup Parent { get; set; }

        IDevice<T> GetDevice<T>(string deviceName);

        IRetainedSubject<T> GetSharedSubject<T>(string deviceName);
    }
}