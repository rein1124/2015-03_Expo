using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Hdc.Mvvm.Dialogs
{
    public abstract class GeneralInputOutputDialogService<TInputData, TOutputData> :
        DialogServiceBase<TOutputData>,
        IGeneralInputOutputDialogService<TInputData, TOutputData>
    {
        public IObservable<DialogEventArgs<TOutputData>> Show(TInputData data)
        {
            OnShowing(data);
            return ShowModalDialog();
        }

        protected virtual void OnShowing(TInputData inputData)
        {
        }
    }

    public abstract class GeneralInputOutputDialogService<TInputOutputData> :
        GeneralInputOutputDialogService<
            TInputOutputData,
            TInputOutputData>,
        IGeneralInputOutputDialogService<TInputOutputData>
    {
    }
}