using System;
using System.Linq;
using System.Reactive.Linq;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Dialogs
{
    //TODO to verified
    public abstract class DialogService<TValue, TPromptViewModel> where TPromptViewModel : IPromptViewModel<TValue>
    {
        public IObservable<DialogEventArgs<TValue>> Show()
        {
            ModalDialogViewModel.Show();

            var closedEvent = PromptViewModel.ClosedEvent.Take(1);
            closedEvent.Subscribe(x => ModalDialogViewModel.Close());
            return closedEvent;
        }

        [Dependency]
        public TPromptViewModel PromptViewModel { get; set; }

//        [Dependency(RegionNames.JobSelectingSelectJobDialog)]
        public abstract IModalDialogViewModel ModalDialogViewModel { get; set; }
    }
}