using System;

namespace Hdc.Mvvm.Dialogs
{
    public interface IPromptViewModel<TValue>
    {
        IObservable<DialogEventArgs<TValue>> ClosedEvent { get; }
    }
}