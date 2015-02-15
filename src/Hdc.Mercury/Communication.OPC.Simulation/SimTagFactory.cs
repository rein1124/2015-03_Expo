using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Communication.OPC
{
    public class SimTagFactory : ISimTagFactory
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public ISimTag Build(DeviceDataType dataType)
        {
            return Container.Resolve<ISimTag>();
        }
    }
}