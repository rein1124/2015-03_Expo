using System;
using System.Reactive;

namespace Hdc.Mvvm.Dialogs
{
    public interface IMessageDialogService: IClosable
    {
        IObservable<DialogEventArgs<Unit>> Show(string message);
    }
}