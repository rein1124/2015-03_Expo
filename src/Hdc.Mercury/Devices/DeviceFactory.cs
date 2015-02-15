using System;
using Hdc.Mercury.Communication;
using Hdc.Mercury.Configs;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class DeviceFactory : IDeviceFactory
    {
        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        [Dependency]
        public IAccessChannelManager AccessChannelManager { get; set; }

        public IDevice Create(DeviceConfig deviceConfig)
        {
            string channelName = deviceConfig.IsSimulated
                                     ? AccessChannelNames.SimAccessChannelFactory
                                     : AccessChannelNames.AccessChannelFactory;

            var channel = AccessChannelManager.GetChannel(channelName);

//            var accessItemConfig = new AccessItemConfig
//                                       {
//                                           Tag = deviceConfig.Tag,
//                                           DataType = deviceConfig.DataType,
//                                           IsSimulated = deviceConfig.IsSimulated
//                                       };

            var dvc = channel.Register(deviceConfig);

//            var accessDataType = deviceConfig.DataType.ToAccessDataType();
//            var deviceType = typeof (IDevice<>).MakeGenericType(accessDataType);
//            var dvc = ServiceLocator.GetInstance(deviceType) as IDevice;

            if (dvc == null)
            {
                throw new InvalidOperationException("cannot resolve device");
            }

//            dvc.Init(registration);
//            dvc.Name = deviceConfig.Name;

            return dvc;
        }
    }
}