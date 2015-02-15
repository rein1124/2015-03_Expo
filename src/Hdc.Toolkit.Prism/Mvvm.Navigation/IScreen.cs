using System;
using System.Collections.Generic;
using Hdc.Collections.Generic;
using Hdc.Reactive;
using Microsoft.Practices.Prism.Commands;

namespace Hdc.Mvvm.Navigation
{
    public interface IScreen : ITree<IScreen>
    {
//        bool IsActived { get; set; }

//        bool IsDefaultActive { get; set; }

        bool IsActive { get; set; }

        IList<IScreen> Screens { get; }

        IScreen this[string screenName] { get; }

        string Name { get; set; }

        string GroupName { get; set; }

        IScreen ParentScreen { get; }

        bool IsMutual { get; set; }

        void ActivateChildScreen(IScreen screen);

        void Activate();

        void DeactivateScreen(IScreen screen);

        bool IsEnabled { get; set; }

        IRetainedSubject<bool> IsEnabledSubject { get; }

        int ActiveIndex { get; set; }

//        int DefaultIndex { get; set; }

//        event Action<IScreenTree> ActiveChanged;

        IObservable<IScreen> ActiveChangedEvent { get; }

        void Initial();

        IObservable<IScreen> OnInitialEvent { get; }

        IObservable<IScreen> OnEnterEvent { get; }

        IObservable<IScreen> OnExitEvent { get; }

        DelegateCommand<IScreen> ActivateScreenCommand { get; }

        DelegateCommand<string> ActivateScreenCommandWithName { get; }

        DelegateCommand<IScreen> DeactivateScreenCommand { get; }

        DelegateCommand<string> DeactivateScreenCommandWithName { get; }

        //TODO temperory commented
        //IList<IDeviceUpdateObserver> DeviceUpdateObservers { get; }

        IEnumerable<IScreen> GetActiveSubTrees();

        IObservable<ScreenChangingEventArgs> ScreenChangingEvent { get; }
    }
}