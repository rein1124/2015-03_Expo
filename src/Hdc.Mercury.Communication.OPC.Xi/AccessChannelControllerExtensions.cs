using System;
using Hdc.Unity;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public static class AccessChannelControllerExtensions
    {
/*        public static IXiConfigurator UseXi(this IAccessChannelController boot)
        {
            boot.Container.RegisterType<IXiServerProvider, XiServerProvider>("XiServerProvider");
            boot.Container.RegisterType<IXiServerProvider, SimXiServerProvider>("SimXiServerProvider");
            boot.Container.RegisterTypeWithLifetimeManager<IXiServerConfigProvider, XiServerConfigProvider>();

            boot.Container.RegisterType<IAccessChannelFactory, XiAccessChannelFactory>("AccessChannelFactory");
            boot.Container.RegisterType<IAccessChannelFactory, SimAccessChannelFactory>("SimAccessChannelFactory");

            boot.Container.RegisterType<ICommunicationState, CommunicationState>();

            return new XiConfigurator(boot);
        }*/

        public static IAccessChannelController Config(this IAccessChannelController boot, Action<XiServerConfig> configAction)
        {
            new XiConfigurator(boot).Config(configAction);
            return boot;
        }
    }

    public interface IXiConfigurator
    {
        IAccessChannelController Config(Action<XiServerConfig> configAction);
    }

    internal class XiConfigurator : IXiConfigurator
    {
        private readonly IAccessChannelController _boot;

        public XiConfigurator(IAccessChannelController boot)
        {
            _boot = boot;
        }

        public IAccessChannelController Config(Action<XiServerConfig> configAction)
        {
            var p = _boot.Container.Resolve<IXiServerConfigProvider>();
            configAction(p.XiServerConfig);
            return _boot;
        }
    }
}