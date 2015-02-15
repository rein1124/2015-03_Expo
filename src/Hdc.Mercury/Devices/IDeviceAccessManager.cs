using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;

namespace Hdc.Mercury
{
    public interface IDeviceAccessManager
    {
        void ReadDevices(IEnumerable<IDevice> devices);

        Task ReadDevicesAsync(IEnumerable<IDevice> devices);

        Task ReadDevicesAsync<T>(IEnumerable<IDevice<T>> devices);

        //TODO
        //IEnumerable<dynamic> ReadDevicesDirectly(IEnumerable<IDevice> devices);

        void WriteDevices(IEnumerable<Tuple<IDevice, dynamic>> deviceValuePairs);

//        Task WriteDevicesAsync(IEnumerable<Tuple<IDevice, dynamic>> deviceValuePairs);

        Task WriteDevicesAsync(IEnumerable<IDevice> devices);
    }
}