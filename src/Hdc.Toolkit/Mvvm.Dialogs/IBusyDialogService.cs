using System;
using System.Reactive;

namespace Hdc.Mvvm.Dialogs
{
    public interface IBusyDialogService: IClosable
    {
        IObservable<DialogEventArgs<Unit>> Show(string message);
    }
}