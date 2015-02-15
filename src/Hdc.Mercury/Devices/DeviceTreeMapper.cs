using AutoMapper;
using Hdc.Collections;
using Hdc.Mercury.Configs;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class DeviceTreeMapper
    {
        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        [Dependency]
        public IDeviceGroupFactory DeviceGroupFactory { get; set; }

        [Dependency]
        public IDeviceFactory DeviceFactory { get; set; }

        [InjectionMethod]
        public void Init()
        {
            Mapper.CreateMap<DeviceGroupConfig, IDeviceGroup>()
                .ForMember(x => x.DeviceGroups, x => x.MapFrom(y => y.GroupConfigCollection))
                .ForMember(x => x.Devices, x => x.MapFrom(y => y.DeviceConfigCollection))
                .ForMember(x => x.Parent, x=>x.Ignore())
                .ConstructUsing(x => DeviceGroupFactory.Create(x))
                ;

            Mapper.CreateMap<DeviceConfig, IDevice>()
                .ConstructUsing(x => DeviceFactory.Create(x))
                ;
        }

        public IDeviceGroup Map(DeviceGroupConfig deviceGroupConfig)
        {
            var deviceGroup = Mapper.Map<DeviceGroupConfig, IDeviceGroup>(deviceGroupConfig);

            AppendParent(deviceGroup);

            return deviceGroup;

            
        }


        void AppendParent(IDeviceGroup deviceGroup,IDeviceGroup parent = null)
        {
            deviceGroup.Parent = parent;
            foreach (var @group in deviceGroup.DeviceGroups)
            {
                AppendParent(group,deviceGroup);
            }
        }

        private IDeviceGroup Get(DeviceGroupConfig deviceGroupConfig)
        {
            return TraverseEx.TraverseMap(deviceGroupConfig,
                                          x => DeviceGroupFactory.Create(x),
                                          x => x.GroupConfigCollection,
                                          (s, t) => s.DeviceGroups.Add(t));
        }
    }
}