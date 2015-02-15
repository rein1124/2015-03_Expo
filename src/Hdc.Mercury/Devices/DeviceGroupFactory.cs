using System;
using Hdc.Mercury;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Configs
{
    public class DeviceGroupFactory : IDeviceGroupFactory
    {
        [Dependency]
        public IDeviceFactory DeviceFactory { get; set; }

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        public IDeviceGroup Create(DeviceGroupConfig deviceGroupConfig)
        {
            var deviceGroup = ServiceLocator.GetInstance<IDeviceGroup>();

            deviceGroup.Name = deviceGroupConfig.Name;

            foreach (var deviceConfig in deviceGroupConfig.DeviceConfigCollection)
            {
                var device = DeviceFactory.Create(deviceConfig);
                deviceGroup.Devices.Add(device);
            }

            foreach (var subDeviceGroupConfig in deviceGroupConfig.GroupConfigCollection)
            {
                var subDeviceGroup = Create(subDeviceGroupConfig);
                deviceGroup.DeviceGroups.Add(subDeviceGroup);
                subDeviceGroup.Parent = deviceGroup;
            }

            return deviceGroup;
        }
    }
}