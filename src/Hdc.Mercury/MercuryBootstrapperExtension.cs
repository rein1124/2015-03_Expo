using System;
using Hdc.Boot;
using Hdc.Mercury.Communication;
using Hdc.Mercury.Communication.OPC;
using Hdc.Mercury.Configs;
using Hdc.Mercury.Converters;
using Hdc.Reactive;
using Hdc.Unity;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class MercuryBootstrapperExtension : IBootstrapperExtension
    {
        private IUnityContainer _container;

        public void Initialize(IUnityContainer container)
        {
            _container = container;

            Container.RegisterType<IDataConverter<int, bool>, Int32BooleanConverter>();
            Container.RegisterType<IDataConverter<int, short>, Int32Int16Converter>();
            Container.RegisterType<IDataConverter<int, ushort>, Int32UInt16Converter>();
            Container.RegisterType<IDataConverter<short, int>, HSBCDInt32Converter>();
            Container.RegisterType<IDataConverter<short, int>, SBCD10Int32Converter>();
            Container.RegisterType<IDataConverter<int, double>, Int32DoubleConverter>();
            Container.RegisterType<IDataConverter<int, byte>, Int32ByteConverter>();
            Container.RegisterType<IDataConverter<Int16[], Int32[]>, Int16ArrayToInt32ArrayConverter>();
            //            Container.RegisterType<IDataConverter<UInt32, Int32>, UInt32Int32Converter>();
            //            Container.RegisterType<IDataConverter<Boolean, Int16>, BooleanInt16Converter>();
            //            Container.RegisterType<typeof(IDataConverter<,>), Int32ByteConverter>();


            Container.RegisterType<ISimOpcServer, SimOpcServer>();
            Container.RegisterType<ISimTagFactory, SimTagFactory>();
            Container.RegisterType<ISimTag, SimTag>();


            //TODO these times does not use LifeTimeManager??? -rein
            Container.RegisterTypeWithLifetimeManager<IDeviceAccessManager, DeviceAccessManager>();


            Container.RegisterTypeWithLifetimeManager<IDeviceGroupFactory, DeviceGroupFactory>();
            Container.RegisterType<IDeviceGroup, DeviceGroup>();
            Container.RegisterTypeWithLifetimeManager<IDeviceGroupProvider, ConfigDeviceGroupProvider>();
            Container.RegisterTypeWithLifetimeManager<IDeviceFactory, DeviceFactory>();
            Container.RegisterType(typeof (ISimDevice<>), typeof (SimDevice<>));
            Container.RegisterType(typeof (ISimpleDevice<>), typeof (SimpleDevice<>));


            Container.RegisterTypeWithLifetimeManager<ICommunicationTracking, CommunicationTracking>();
            Container.RegisterTypeWithLifetimeManager<IAccessChannelManager, AccessChannelManager>();

            Container.RegisterType(typeof (IDevice<>), typeof (Device<>));
            Container.RegisterType(typeof (IValueMonitor<>), typeof (DeviceValueMonitor<>));

            Container.RegisterTypeWithLifetimeManager<
                IDeviceGroupConfigRepository,
                XamlDeviceGroupConfigRepository>();

            Container.RegisterTypeWithLifetimeManager<
                IAccessChannelController,
                AccessChannelController>();
        }

        public IUnityContainer Container
        {
            get { return _container; }
        }
    }
}