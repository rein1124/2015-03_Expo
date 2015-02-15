using Hdc.Mercury;
using Microsoft.Practices.Unity;

namespace ODM.Domain
{
    public class MachineProvider : IMachineProvider
    {
        [Dependency]
        public IDeviceGroupProvider DeviceGroupProvider { get; set; }

        [Dependency]
        public IMachine Machine { get; set; }

        [InjectionMethod]
        public void Init()
        {
            Machine.Initialize(DeviceGroupProvider.RootDeviceGroup);
        }
    }
}