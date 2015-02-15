using System;

namespace Hdc.Mvvm.Dialogs
{
    public interface IGeneralOutputDialogService<TData>
    {
        IObservable<DialogEventArgs<TData>> Show();
    }
}