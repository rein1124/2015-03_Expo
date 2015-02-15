using System;

namespace Hdc.Mvvm.Dialogs
{
    public interface IGeneralInputOutputDialogService<in TInputData, TOutputData>
    {
        IObservable<DialogEventArgs<TOutputData>> Show(TInputData inputData);
    }

    public interface IGeneralInputOutputDialogService<TInputOutputData> : IGeneralInputOutputDialogService<
                                                                              TInputOutputData,
                                                                              TInputOutputData>
    {
    }
}