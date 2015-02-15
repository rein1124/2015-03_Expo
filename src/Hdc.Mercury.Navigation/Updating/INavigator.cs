using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using Hdc.Mvvm.Navigation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Navigation
{
    public interface INavigator
    {
        IObservable<IEnumerable<IDeviceUpdateObserver>> DeviceUpdateObserversChangedEvent { get; }
    }

    public class Navigator : INavigator
    {
        [Dependency]
        public IScreenProvider ScreenProvider { get; set; }

        [InjectionMethod]
        public void Init()
        {
//            _deviceUpdateObserversChangedEvent = new Subject<IList<IDeviceUpdateObserver>>() {};

            ScreenProvider.TopScreen.ActiveChangedEvent.Subscribe(
                x =>
                    {
                        var activeSubTrees = x.GetActiveSubTrees();

                        //TODO temperory commented
                        throw new NotImplementedException();
                        //var updatingObservers = activeSubTrees.SelectMany(y => y.DeviceUpdateObservers);
                        //_deviceUpdateObserversChangedEvent.OnNext(updatingObservers);
                    });


        }



        private Subject<IEnumerable<IDeviceUpdateObserver>> _deviceUpdateObserversChangedEvent =new
            Subject<IEnumerable<IDeviceUpdateObserver>>();

        public IObservable<IEnumerable<IDeviceUpdateObserver>> DeviceUpdateObserversChangedEvent
        {
            get { return _deviceUpdateObserversChangedEvent; }
        }
    }
}