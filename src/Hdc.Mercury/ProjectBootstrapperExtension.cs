using Hdc.Boot;
using Hdc.Mercury.Configs;
using Hdc.Unity;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class ProjectBootstrapperExtension : IBootstrapperExtension
    {
        private IUnityContainer _container;

        public void Initialize(IUnityContainer container)
        {
            _container = container;
            Container.RegisterTypeWithLifetimeManager<IDeviceGroupProvider, ProjectDeviceGroupProvider>();
        }

        public IUnityContainer Container
        {
            get { return _container; }
        }
    }
}