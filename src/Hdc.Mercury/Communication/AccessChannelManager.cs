using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Communication
{
    public class AccessChannelManager : IAccessChannelManager
    {
        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        private readonly ConcurrentDictionary<string, IAccessChannel> _channels =
            new ConcurrentDictionary<string, IAccessChannel>();

        public IAccessChannel GetChannel(string channelName)
        {
            var accessChannel = _channels.GetOrAdd(
                channelName, arg =>
                                 {
                                     var channelConfig = new AccessChannelConfig
                                                             {
                                                                 Interval = 100,
                                                                 AccessMode = AccessMode.ReadWrite,
                                                                 SubscriptionMode = SubscriptionMode.Callback, // Poll, Callback
                                                             };

                                     var factory = ServiceLocator.GetInstance<IAccessChannelFactory>(channelName);
                                     var channel = factory.Create(channelConfig);
                                     return channel;
                                 });
            return accessChannel;
        }
    }
}