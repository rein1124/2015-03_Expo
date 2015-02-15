using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.SchemaEditor
{
    [Module]
    public class ExecuteModule : IModule
    {
        private readonly IUnityContainer _container;

        public ExecuteModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
        }
    }
}