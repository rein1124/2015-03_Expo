using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;

namespace Hdc.Prism.Modularity
{
    public abstract class ApplicationModule : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        [Dependency]
        public IEventAggregator EventAggregator { get; set; }

        public abstract void Initialize();
    }
}