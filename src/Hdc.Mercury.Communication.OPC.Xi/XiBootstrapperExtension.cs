using Hdc.Boot;
using Hdc.Unity;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public class XiBootstrapperExtension:IBootstrapperExtension
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<IXiServerProvider, XiServerProvider>("XiServerProvider");
            container.RegisterType<IXiServerProvider, SimXiServerProvider>("SimXiServerProvider");
            container.RegisterTypeWithLifetimeManager<IXiServerConfigProvider, XiServerConfigProvider>();

            container.RegisterType<IAccessChannelFactory, XiAccessChannelFactory>("AccessChannelFactory");
            container.RegisterType<IAccessChannelFactory, SimAccessChannelFactory>("SimAccessChannelFactory");

            container.RegisterType<ICommunicationState, CommunicationState>();
        }
    }
}