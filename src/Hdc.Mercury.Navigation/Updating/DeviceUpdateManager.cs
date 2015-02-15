using System;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Navigation
{
    public class DeviceUpdateManager
    {
        [Dependency]
        public INavigator Navigator { get; set; }

        [Dependency]
        public IClient Client { get; set; }

        [InjectionMethod]
        public void Init()
        {
            Navigator.DeviceUpdateObserversChangedEvent.Subscribe(
                x =>
                    {
                        var updatingDevices = x.SelectMany(y => y.UpdatingDevices);
                        Client.ClearUpdatingDevices();
                        Client.AddUpdatingDevices(updatingDevices);
                    });
        }
    }
}