using System;
using System.Reactive.Linq;
using Hdc.Mvvm;

namespace Hdc.Reactive
{
    public interface IDisplayValueMonitor
    {
        void RaisePropertyChangedOnViewModelUsingDispatcher(ViewModel viewModel, string propertyName);
    }

    public class DisplayValueMonitor<T> : ValueMonitor<T>, IDisplayValueMonitor
    {
        public void RaisePropertyChangedOnViewModelUsingDispatcher(ViewModel viewModel,
            string propertyName)
        {
            this.ObserveOnDispatcher()
                .Subscribe(v => viewModel.RaisePropertyChanged(propertyName));
        }
    }
}