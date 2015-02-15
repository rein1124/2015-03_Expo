using System;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public interface IAccessChannelController
    {
        IUnityContainer Container { get; }

        void Start(string channelName);

        Task StartAsync(string channelName);

        void Stop();
    }
}