using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hdc.Collections.Generic;
using Hdc.Reactive;

namespace Hdc.Mercury
{
    public static class DeviceGroupExtensions
    {
        public static IEnumerable<IDeviceGroup> GetDeviceGroupsByName(this IDeviceGroup deviceGroup,
                                                                      string deviceGroupName)
        {
            return deviceGroup.DeviceGroups.Where(x => x.Name == deviceGroupName);
        }

        public static IEnumerable<IDeviceGroup> GetDeviceGroupsByName(this IDeviceGroup deviceGroup,
                                                                      string deviceGroupName,
                                                                      int count)
        {
            return GetDeviceGroupsByName(deviceGroup, deviceGroupName).Take(count);
        }

        public static IDeviceGroup GetDeviceGroupByName(this IDeviceGroup deviceGroup, string deviceGroupName)
        {
            return GetDeviceGroupsByName(deviceGroup, deviceGroupName).FirstOrDefault();
        }

        public static IDevice<T> GetDevice<T>(this IDeviceGroup deviceGroup, object deviceName)
        {
            return deviceGroup.GetDevice<T>(deviceName.ToString());
        }

        public static IDevice GetDevice(this IDeviceGroup deviceGroup, string deviceName, Type dataType)
        {
            MethodInfo mi = typeof(IDeviceGroup).GetMethod("GetDevice");

            mi = mi.MakeGenericMethod(new Type[] { dataType });

            var o =    mi.Invoke(deviceGroup, new object[] { deviceName });
            return o as IDevice;
        }

        public static IRetainedSubject<T> GetSharedSubject<T>(this IDeviceGroup deviceGroup, object deviceName)
        {
            return deviceGroup.GetSharedSubject<T>(deviceName.ToString());
        }

        public static IDevice<bool> GetBooleanDevice(this IDeviceGroup deviceGroup, object deviceName)
        {
            return deviceGroup.GetDevice<bool>(deviceName.ToString());
        }

        public static IDevice<int> GetInt32Device(this IDeviceGroup deviceGroup, object deviceName)
        {
            return deviceGroup.GetDevice<int>(deviceName.ToString());
        }

        public static IList<IDevice> GetAllDevices(this IDeviceGroup deviceGroup)
        {
            return deviceGroup
                .GetAllNodesUsingTraverseFromTopLeft()
                .SelectMany(x => x.Devices)
                .ToList();
        }
    }
}