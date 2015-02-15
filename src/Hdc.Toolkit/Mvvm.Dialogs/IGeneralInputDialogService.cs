using System;
using System.Reactive;

namespace Hdc.Mvvm.Dialogs
{
    public interface IGeneralInputDialogService<in TInputData> : IGeneralInputOutputDialogService<TInputData, Unit>
    {
        //        IObservable<DialogEventArgs<Unit>> Show(TInputData inputData);
    }
}