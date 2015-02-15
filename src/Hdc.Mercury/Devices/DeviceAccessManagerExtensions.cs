using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Hdc.Collections.Generic;
using Hdc.Linq;

namespace Hdc.Mercury
{
    public static class DeviceAccessManagerExtensions
    {
        public static IDeviceAccessManager WriteDevice<T>(this IDeviceAccessManager deviceAccessManager,
                                                          IDevice<T> device)
        {
            IEnumerable<IDevice<T>> deviceValuePairs = device.ToEnumerable();
            return deviceAccessManager.WriteDevices(deviceValuePairs);
        }

        public static IDeviceAccessManager WriteDeviceTrue(this IDeviceAccessManager deviceAccessManager,
                                                           IDevice<bool> device)
        {
            deviceAccessManager.WriteDevicesTrue(device.ToEnumerable());

            return deviceAccessManager;
        }

        public static IDeviceAccessManager WriteDeviceFalse(this IDeviceAccessManager deviceAccessManager,
                                                            IDevice<bool> device)
        {
            deviceAccessManager.WriteDevicesFalse(device.ToEnumerable());

            return deviceAccessManager;
        }

        public static IDeviceAccessManager WriteDevices<T>(this IDeviceAccessManager deviceAccessManager,
                                                           IEnumerable<IDevice<T>> devices,
                                                           IEnumerable<T> values)
        {
            var pairs = GetPairs(devices, values);

            deviceAccessManager.WriteDevices(pairs);
            return deviceAccessManager;
        }

        public static IDeviceAccessManager WriteDevicesTrue(this IDeviceAccessManager deviceAccessManager,
                                                            IEnumerable<IDevice<bool>> devices)
        {
            var devices2 = devices.ToList();

            var values = devices2.Select(x => true);
            deviceAccessManager.WriteDevices(devices2, values);
            return deviceAccessManager;
        }

        public static IDeviceAccessManager WriteDevicesTrue(this IDeviceAccessManager deviceAccessManager,
                                                            params IDevice<bool>[] devices)
        {
            deviceAccessManager.WriteDevicesTrue(devices as IEnumerable<IDevice<bool>>);

            return deviceAccessManager;
        }

        public static void WriteDevicesFalse(this IDeviceAccessManager deviceAccessManager,
                                             IEnumerable<IDevice<bool>> devices)
        {
            var devices2 = devices.ToList();
            var values = devices2.Select(x => false);

            deviceAccessManager.WriteDevices(devices2, values);
        }

        public static IDeviceAccessManager WriteDevices<T>(this IDeviceAccessManager deviceAccessManager,
                                                           IEnumerable<IDevice<T>> devices)
        {
            var pairs = devices.Select(item1 => new Tuple<IDevice, dynamic>(item1, item1.Stage));

            deviceAccessManager.WriteDevices(pairs);

            return deviceAccessManager;
        }

        public static IDeviceAccessManager WriteDevices<T>(this IDeviceAccessManager deviceAccessManager,
                                                           params IDevice<T>[] devices)
        {
            return deviceAccessManager.WriteDevices(devices as IEnumerable<IDevice<T>>);
        }


        //        private static IEnumerable<Tuple<IDevice, object>> GetValuePairs<T>(IEnumerable<IDevice<T>> devices)
//        {
//            var tuples = from IDevice<T> item1 in devices
//                select new Tuple<IDevice, dynamic>(item1, item1.Value);
//
//            return tuples;
//        }

//        public static Task WriteDevicesAsync<T>(this IDeviceAccessManager deviceAccessManager,
//                                                IEnumerable<IDevice<T>> devices)
//        {
//            var devices2 = devices.ToList();
//
//            var values = devices2.Select(x => x.Stage).ToList();
//
//            return deviceAccessManager.WriteDevicesAsync(devices2, values);
//        }


/*        public static Task WriteDevicesAsync<T>(this IDeviceAccessManager deviceAccessManager,
                                                IEnumerable<IDevice<T>> devices)
        {
            var pairs = devices.Select(item1 => new Tuple<IDevice, dynamic>(item1, item1.Stage));

            return deviceAccessManager.WriteDevicesAsync(pairs);
        }*/

        public static Task WriteDevicesAsync<T>(this IDeviceAccessManager deviceAccessManager,
                                                IEnumerable<Tuple<IDevice<T>, T>> deviceValuePairs)
        {
            var devices = deviceValuePairs.Do(x => { x.Item1.Stage = x.Item2; }).Select(x => x.Item1);

            return deviceAccessManager.WriteDevicesAsync(devices);
        }

        public static Task WriteDevicesAsync<T>(this IDeviceAccessManager deviceAccessManager,
                                                IEnumerable<IDevice<T>> devices,
                                                IEnumerable<T> values)
        {
            var enumerable = devices as IList<IDevice<T>> ?? devices.ToList();
            enumerable.CopyValuesFrom(values, (d, v) => d.Stage = v);

            return deviceAccessManager.WriteDevicesAsync(enumerable);
        }

        private static IEnumerable<Tuple<IDevice, object>> GetPairs<T>(IEnumerable<IDevice<T>> devices,
                                                                       IEnumerable<T> values)
        {
            List<IDevice<T>> dvcs = devices.ToList();
            var vs = values.ToList();
            var dvcCount = dvcs.Count;
            var valueCount = vs.Count;

            if (dvcCount != valueCount)
                throw new NotSupportedException("devices count should equal values count");

            var pairs = new List<Tuple<IDevice, dynamic>>();
            for (int i = 0; i < dvcCount; i++)
            {
                var item1 = dvcs[i];
                var t = new Tuple<IDevice, dynamic>(item1, vs[i]);
                pairs.Add(t);
            }
            return pairs;
        }

/*        public static Task WriteDevicesAsync<T>(this IDeviceAccessManager deviceAccessManager,
                                                IEnumerable<IDevice<T>> devices,
                                                IEnumerable<T> values)
        {
            var deviceList = devices.ToList();

            var valueList = values.ToList();

            var pairs = new List<Tuple<IDevice, dynamic>>();

            for (int i = 0; i < deviceList.Count; i++)
            {
                var d = deviceList[i];
                var v = valueList[i];

                pairs.Add(new Tuple<IDevice, dynamic>(d, v));
            }

            return deviceAccessManager.WriteDevicesAsync(pairs);
        }*/
    }
}