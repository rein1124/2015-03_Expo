using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hdc.Mercury.Communication;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Communication
{
    public class XiAccessChannelFactory : IAccessChannelFactory
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public IAccessChannel Create(AccessChannelConfig accessChannelConfig)
        {
            string id = Guid.NewGuid().ToString();
//            Container.RegisterInstance<AccessChannelConfig>(id, accessChannelConfig);
//            Container.RegisterType<IAccessChannel, XiAccessChannel>(id);
            var accessChannel = Container.Resolve<XiAccessChannel>(id);
            accessChannel.Initialize(accessChannelConfig);
            return accessChannel;
        }
    }
}