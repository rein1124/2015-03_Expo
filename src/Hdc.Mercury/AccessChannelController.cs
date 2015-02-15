using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Hdc.Mercury.Communication;
using Hdc.Reactive.Linq;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    internal class AccessChannelController : IAccessChannelController
    {
        private IAccessChannel _channel;

        [Dependency]
        public IUnityContainer Container { get; set; }
        
        public void Start(string channelName)
        {
            var dg = Container.Resolve<IDeviceGroupProvider>();
            var am = Container.Resolve<IAccessChannelManager>();

            var devices = dg.RootDeviceGroup.GetAllDevices().Where(d => !d.Registration.Config.IsSimulated);

            _channel = am.GetChannel(channelName);

            _channel.Start();
            _channel.AddToUpdateList(devices);
        }

        public async Task StartAsync(string channelName)
        {
            var dg = Container.Resolve<IDeviceGroupProvider>();
            var am = Container.Resolve<IAccessChannelManager>();

            var devices = dg.RootDeviceGroup.GetAllDevices().Where(d => !d.Registration.Config.IsSimulated);

            _channel = am.GetChannel(channelName);

            await _channel.StartAsync();
            await _channel.AddToUpdateListAsync(devices);
        }

        public void Stop()
        {
            _channel.Stop();
        }
    }
}