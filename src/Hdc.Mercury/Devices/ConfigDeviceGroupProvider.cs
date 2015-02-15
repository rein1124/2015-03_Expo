using AutoMapper;
using Hdc.Mercury;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Configs
{
    public class ConfigDeviceGroupProvider : IDeviceGroupProvider
    {
        private IDeviceGroup _deviceGroup;

        [Dependency]
        public IDeviceGroupConfigRepository DeviceGroupConfigRepository { get; set; }

        [Dependency]
        public IDeviceGroupFactory DeviceGroupFactory { get; set; }

        private object locker = new object();

        public IDeviceGroup RootDeviceGroup
        {
            get
            {
                lock (locker)
                {
                    if (_deviceGroup == null)
                    {
                        var deviceGroupConfig = DeviceGroupConfigRepository.Load();

                        _deviceGroup = DeviceGroupFactory.Create(deviceGroupConfig.RootGroupConfig);
                    }
                    return _deviceGroup;
                }
            }
        }
    }
}