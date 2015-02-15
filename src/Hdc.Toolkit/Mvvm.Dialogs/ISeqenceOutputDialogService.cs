using System;

namespace Hdc.Mvvm.Dialogs
{
    public interface ISeqenceOutputDialogService<out TData>
    {
        IObservable<TData> Show();
    }
}